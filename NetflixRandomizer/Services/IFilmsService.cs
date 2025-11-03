using NetflixRandomizer.Models;

namespace NetflixRandomizer.Services
{
    public interface IFilmsService
    {
        Task<Films> GetFilms();
    }

    public class FilmsServiceMockup : RestService, IFilmsService
    {
        public FilmsServiceMockup(IHttpClientService httpClientService) : base(httpClientService)
        {

        }

        public async Task<Films> GetFilms()
        {
            try
            {
                List<QueryArgument> arguments = new List<QueryArgument>()
                {
                    new QueryArgument("country","ES"),
                    new QueryArgument("series_granularity","show"),
                    new QueryArgument("order_direction","asc"),
                    new QueryArgument("order_by","original_title"),
                    new QueryArgument("genres_relation","and"),
                    new QueryArgument("output_language","es"),
                    new QueryArgument("show_type","movie"),
                };
                Films films = await GetAsync<Films>(default, "shows/search/filters", "", arguments.ToArray());

                return films;
            }
            catch
            {
                throw;
            }
        }
    }

    public class FilmsService : RestService, IFilmsService
    {

        public FilmsService(IHttpClientService httpClientService) : base(httpClientService)
        {

        }

        //Como es una demo sobre una API abierta, dejo esto, sino generamos una lista de mockup a mano
        public async Task<Films> GetFilms()
        {
            try
            {                
                List<QueryArgument> arguments = new List<QueryArgument>()
                {
                    new QueryArgument("country","ES"),
                    new QueryArgument("series_granularity","show"),
                    new QueryArgument("order_direction","asc"),
                    new QueryArgument("order_by","original_title"),
                    new QueryArgument("genres_relation","and"),
                    new QueryArgument("output_language","es"),
                    new QueryArgument("show_type","movie"),
                };
                Films films = await GetAsync<Films>(default, "shows/search/filters", "", arguments.ToArray());

                return films;
            }
            catch
            {
                throw;
            }
        }
    }
}
