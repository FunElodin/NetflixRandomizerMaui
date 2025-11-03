using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace NetflixRandomizer.Services
{
    public interface IRestService
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(string defaultUri = "", string queryPath = "", params QueryArgument[] queryArguments);
        Task<TEntity> GetAsync<TEntity>(string defaultUri = "", string queryPath = "", string id = "", params QueryArgument[] queryArguments);
        Task<TEntity> PostAsync<TEntity>(TEntity entity = default, string queryPath = "");
        Task<bool> PostAsync(string queryPath = "", params QueryArgument[] arguments);
    }

    public class RestService : IRestService
    {
        protected static IHttpClientService? _httpClientService;
        public HttpClient Client { get; set; }

        private string _defaultApiUrl;
        protected string DefaultApiUrl
        {
            get => GlobalSettings.BaseApiUri;
            set => _defaultApiUrl = value;
        }

        protected RestService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            Client = _httpClientService.GetHttpClient();
        }

        protected Uri GetUri(string defaultUri = "", string queryPath = "", string id = "", params QueryArgument[] arguments)
        {
            var url = (string.IsNullOrEmpty(defaultUri) ? DefaultApiUrl : defaultUri) + (string.IsNullOrEmpty(queryPath) ? "" : $"/{queryPath}") + (string.IsNullOrEmpty(id) ? "" : $"/{id}");

            if (arguments != null && arguments.Length > 0)
            {
                url += "?";
                foreach (var argument in arguments)
                {
                    url += string.Format("{0}={1}", argument.Name, argument.Value);
                    if (arguments.Last() != argument)
                    {
                        url += "&";
                    }
                }
            }

            return new Uri(url);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(string defaultUri = "", string queryPath = "", params QueryArgument[] queryArguments)
        {
            return await InternalGetAsync<IEnumerable<TEntity>>(defaultUri, queryPath, default, queryArguments);
        }


        public async Task<TEntity> GetAsync<TEntity>(string defaultUri = "", string queryPath = "", string id = "", params QueryArgument[] queryArguments)
        {
            return await InternalGetAsync<TEntity>(defaultUri, queryPath, id, queryArguments);
        }

        private async Task<TEntity> InternalGetAsync<TEntity>(string defaultUri = "", string queryPath = "", string id = "", params QueryArgument[] queryArguments)
        {
            string mainUri = string.IsNullOrEmpty(defaultUri) ? GlobalSettings.BaseApiUri : defaultUri;

            Uri uri = null;
            HttpResponseMessage response = null;
            try
            {
                uri = GetUri(mainUri, queryPath, id, queryArguments);
                response = await Client.GetAsync(uri);
                var responseContent = await response.Content.ReadAsStringAsync();
                var entity = JsonConvert.DeserializeObject<TEntity>(responseContent);
                return entity;
            }
            catch (Exception e)
            {
                ManagementResponseError(e, uri, response);
                return default;
            }
        }

        public virtual async Task<TEntity> PostAsync<TEntity>(TEntity bodyContent = default, string queryPath = "")
        {
            Uri uri = null;
            HttpResponseMessage response = null;
            try
            {
                uri = GetUri(GlobalSettings.BaseApiUri, queryPath);

                StringContent content = new StringContent(string.Empty);
                string json = null;
                if (bodyContent != null)
                {
                    json = JsonConvert.SerializeObject(bodyContent, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                response = await Client.PostAsync(uri, content).ConfigureAwait(false);

                var responseContent = await response.Content.ReadAsStringAsync();
                var entity = JsonConvert.DeserializeObject<TEntity>(responseContent);

                return entity;

            }
            catch (Exception e)
            {
                ManagementResponseError(e, uri, response);
                throw;
            }
        }

        public virtual async Task<V> PostAsync<U, V>(U bodyContent = default, string queryPath = "")
        {
            Uri uri = null;
            string responseContent = null;
            HttpResponseMessage response = null;
            try
            {
                uri = GetUri(GlobalSettings.BaseApiUri, queryPath);

                StringContent content = new StringContent(string.Empty);
                string json = null;
                if (bodyContent != null)
                {
                    json = JsonConvert.SerializeObject(bodyContent, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                response = await Client.PostAsync(uri, content).ConfigureAwait(false);
                responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode) // Devuelve true si el código es 200-299
                {
                    var entity = JsonConvert.DeserializeObject<V>(responseContent);
                    return entity;
                }
                else
                {
                    throw new Exception(responseContent);
                }
            }
            catch (Exception e)
            {
                ManagementResponseError(e, uri, response, responseContent);
                throw;
            }
        }

        public async Task<bool> PostAsync(string queryPath = "", params QueryArgument[] arguments)
        {
            Uri uri = null;
            HttpResponseMessage response = null;
            try
            {
                uri = GetUri(default, queryPath, default, arguments);
                response = await Client.PostAsync(uri, new StringContent(string.Empty)).ConfigureAwait(false);

                return true;

            }
            catch (Exception e)
            {
                ManagementResponseError(e, uri, response);
                return false;
            }

        }


        public void ManagementResponseError(Exception exception, Uri uri, HttpResponseMessage response = null, string content = null)
        {

            //Aqui podemos agregar AppCenter(obsoleto9, Crashlytics o Insight de Azure           
            Console.WriteLine($"Error in uri {uri?.AbsolutePath} -> Exception: {exception?.Message}");
            Console.WriteLine($"response {response?.RequestMessage?.RequestUri} {response?.RequestMessage?.Headers?.Authorization}");

            //Enviamos a LoginViewModel(primera vista) que levante la pantalla de error de lo devuelto por la api
            if (string.IsNullOrEmpty(content))
            {
                WeakReferenceMessenger.Default.Send(new ErrorSendItemMessage($"Error in uri {uri?.AbsolutePath} -> Exception: {exception?.Message}" + $"response {response?.RequestMessage?.RequestUri} {response?.RequestMessage?.Headers?.Authorization}" + " -> " + content));
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new ErrorSendItemMessage(content.Replace("{", "").Replace("}", "")));
            }
        }
    }

    public class QueryArgument
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public QueryArgument(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
