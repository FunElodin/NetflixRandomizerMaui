using NetflixRandomizer.Models;

namespace NetflixRandomizer.Services
{
    public interface IFilmsService
    {
        Task<Films> GetFilms();
        Task<Films> GetRandomFilm();

    }

    public class FilmsService : RestService, IFilmsService
    {
        public FilmsService(IHttpClientService httpClientService) : base(httpClientService)
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

        public async Task<Films> GetRandomFilm()
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
                Random rnd = new Random();
                List<Show> shows = new List<Show>();
                shows.Add(films.shows[rnd.Next(films.shows.Count() - 1)]);
                films.shows = shows;
                return films;
            }
            catch
            {
                throw;
            }
        }
    }

    public class FilmsServiceMockup : RestService, IFilmsService
    {

        public FilmsServiceMockup(IHttpClientService httpClientService) : base(httpClientService)
        {

        }

        //Como es una demo sobre una API abierta, dejo esto, sino generamos una lista de mockup a mano
        public async Task<Films> GetFilms()
        {
            try
            {
                return new Films
                {
                    hasMore = false,
                    nextCursor = null,
                    shows = new List<Show>
                    {
                        new Show
                        {
                            itemType = "movie",
                            showType = "feature",
                            id = "1",
                            imdbId = "tt0111161",
                            tmdbId = "278",
                            title = "The Shawshank Redemption",
                            overview = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                            releaseYear = 1994,
                            originalTitle = "The Shawshank Redemption",
                            genres = new List<Genre> { new Genre { id = "drama", name = "Drama" } },
                            directors = new List<string> { "Frank Darabont" },
                            cast = new List<string> { "Tim Robbins", "Morgan Freeman" },
                            rating = 95,
                            runtime = 142,
                            imageSet = new ImageSet
                            {
                                verticalPoster = new VerticalPoster
                                {
                                    w720 = "https://static.wikia.nocookie.net/doblaje/images/5/56/The-Shawshank-Redemption-Latino1994.jpg/revision/latest?cb=20240224231124&path-prefix=es"
                                }
                            },
                            streamingOptions = new StreamingOptions
                            {
                                es = new List<E>
                                {
                                    new E
                                    {
                                        service = new Service
                                        {
                                            id = "netflix",
                                            name = "Netflix",
                                            homePage = "https://www.netflix.com",
                                            themeColorCode = "#E50914"
                                        },
                                        link = "https://www.netflix.com/title/70005379",
                                        quality = "HD"
                                    }
                                }
                            }
                        },
                        new Show
                        {
                            itemType = "movie",
                            showType = "feature",
                            id = "2",
                            imdbId = "tt0068646",
                            tmdbId = "238",
                            title = "The Godfather",
                            overview = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                            releaseYear = 1972,
                            originalTitle = "The Godfather",
                            genres = new List<Genre> { new Genre { id = "crime", name = "Crime" }, new Genre { id = "drama", name = "Drama" } },
                            directors = new List<string> { "Francis Ford Coppola" },
                            cast = new List<string> { "Marlon Brando", "Al Pacino" },
                            rating = 98,
                            runtime = 175,
                            imageSet = new ImageSet
                            {
                                verticalPoster = new VerticalPoster
                                {
                                    w720 = "https://storage.googleapis.com/pod_public/1300/262788.jpg"
                                }
                            },
                            streamingOptions = new StreamingOptions
                            {
                                es = new List<E>
                                {
                                    new E
                                    {
                                        service = new Service
                                        {
                                            id = "paramount",
                                            name = "Paramount+",
                                            homePage = "https://www.paramountplus.com",
                                            themeColorCode = "#0064FF"
                                        },
                                        link = "https://www.paramountplus.com/movies/the-godfather",
                                        quality = "4K"
                                    }
                                }
                            }
                        },
                        new Show
                        {
                            itemType = "movie",
                            showType = "feature",
                            id = "3",
                            imdbId = "tt0468569",
                            tmdbId = "155",
                            title = "The Dark Knight",
                            overview = "When the menace known as the Joker wreaks havoc, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                            releaseYear = 2008,
                            originalTitle = "The Dark Knight",
                            genres = new List<Genre> { new Genre { id = "action", name = "Action" }, new Genre { id = "crime", name = "Crime" } },
                            directors = new List<string> { "Christopher Nolan" },
                            cast = new List<string> { "Christian Bale", "Heath Ledger" },
                            rating = 94,
                            runtime = 152,
                            imageSet = new ImageSet
                            {
                                verticalPoster = new VerticalPoster
                                {
                                    w720 = "https://media.posterstore.com/site_images/68631bce25436f8361d76852_1908700297_WB0037-5.jpg?auto=compress%2Cformat&fit=max&w=3840"
                                }
                            },
                            streamingOptions = new StreamingOptions
                            {
                                es = new List<E>
                                {
                                    new E
                                    {
                                        service = new Service
                                        {
                                            id = "hbomax",
                                            name = "HBO Max",
                                            homePage = "https://www.max.com",
                                            themeColorCode = "#5A2E98"
                                        },
                                        link = "https://www.max.com/movies/the-dark-knight",
                                        quality = "4K"
                                    }
                                }
                            }
                        },
                        new Show
                        {
                            itemType = "movie",
                            showType = "feature",
                            id = "4",
                            imdbId = "tt0137523",
                            tmdbId = "550",
                            title = "Fight Club",
                            overview = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into something much more.",
                            releaseYear = 1999,
                            originalTitle = "Fight Club",
                            genres = new List<Genre> { new Genre { id = "drama", name = "Drama" } },
                            directors = new List<string> { "David Fincher" },
                            cast = new List<string> { "Edward Norton", "Brad Pitt" },
                            rating = 93,
                            runtime = 139,
                            imageSet = new ImageSet
                            {
                                verticalPoster = new VerticalPoster
                                {
                                    w720 = "https://m.media-amazon.com/images/I/31CauYmKHlL._AC_UF894,1000_QL80_.jpg"
                                }
                            },
                            streamingOptions = new StreamingOptions
                            {
                                es = new List<E>
                                {
                                    new E
                                    {
                                        service = new Service
                                        {
                                            id = "hulu",
                                            name = "Hulu",
                                            homePage = "https://www.hulu.com",
                                            themeColorCode = "#1CE783"
                                        },
                                        link = "https://www.hulu.com/movie/fight-club",
                                        quality = "HD"
                                    }
                                }
                            }
                        },
                        new Show
                        {
                            itemType = "movie",
                            showType = "feature",
                            id = "5",
                            imdbId = "tt0109830",
                            tmdbId = "13",
                            title = "Forrest Gump",
                            overview = "The presidencies of Kennedy and Johnson, the Vietnam War, and other history unfold through the perspective of an Alabama man with an IQ of 75.",
                            releaseYear = 1994,
                            originalTitle = "Forrest Gump",
                            genres = new List<Genre> { new Genre { id = "drama", name = "Drama" }, new Genre { id = "romance", name = "Romance" } },
                            directors = new List<string> { "Robert Zemeckis" },
                            cast = new List<string> { "Tom Hanks", "Robin Wright" },
                            rating = 91,
                            runtime = 142,
                            imageSet = new ImageSet
                            {
                                verticalPoster = new VerticalPoster
                                {
                                    w720 = "https://m.media-amazon.com/images/I/71Kih9pBDyL._AC_UF894,1000_QL80_.jpg"
                                }
                            },
                            streamingOptions = new StreamingOptions
                            {
                                es = new List<E>
                                {
                                    new E
                                    {
                                        service = new Service
                                        {
                                            id = "amazon",
                                            name = "Amazon Prime Video",
                                            homePage = "https://www.primevideo.com",
                                            themeColorCode = "#00A8E1"
                                        },
                                        link = "https://www.primevideo.com/detail/Forrest-Gump",
                                        quality = "HD"
                                    }
                                }
                            }
                        }
                    }
                };

            }
            catch
            {
                throw;
            }
        }
        public async Task<Films> GetRandomFilm()
        {
            try
            {
                Films films = await GetFilms();
                
                Random rnd = new Random();
                List<Show> shows = new List<Show>();
                shows.Add(films.shows[rnd.Next(films.shows.Count() - 1)]);
                films.shows = shows;
                return films;
            }
            catch
            {
                throw;
            }
        }
    }


}
