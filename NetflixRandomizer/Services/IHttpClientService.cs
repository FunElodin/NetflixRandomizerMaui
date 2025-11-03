using System.Net.Http.Headers;

namespace NetflixRandomizer.Services
{
    public interface IHttpClientService
    {
        HttpClient GetHttpClient();
    }

    public class HttpClientService : IHttpClientService
    {
        private HttpClient _client;

        public HttpClientService()
        {
            InitHttpClient();
        }


        private void InitHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.None,
                UseCookies = false
            };

            //To avoid problems with SSL connection in android
            //if (GlobalSettings.BaseEndPoint.StartsWith("https"))
            //{
            handler.ServerCertificateCustomValidationCallback += (message, cert, chain, errors) => true;
            //}
            _client = new HttpClient(handler, false);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("X-Version", "1.0");
            _client.DefaultRequestHeaders.Add("x-rapidapi-key", "be98e1af17msh41f6cf01b9b29f8p193904jsn7dcbc6a1b142");
            _client.DefaultRequestHeaders.Add("x-rapidapi-host", "streaming-availability.p.rapidapi.com");
        }       

        public HttpClient GetHttpClient()
        {
            return _client;
        }       
    }
}
