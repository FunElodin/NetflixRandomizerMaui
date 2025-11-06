using NetflixRandomizer.Models;
using NetflixRandomizer.Services;
using NetflixRandomizer.Services.Base;
using System.Windows.Input;

namespace NetflixRandomizer.ViewModels
{

    public class RandomFilmsViewModel : BaseViewModel
    {
        public ICommand OpenStreamingCommand { get; }

        private Show randomShow;
        public Show RandomShow
        {
            get
            {
                return randomShow;
            }
            set
            {
                randomShow = value;
                NotifyPropertyChanged();
            }
        }

        private string genrers;
        public string Genrers
        {
            get
            {
                return genrers;
            }
            set
            {
                genrers = value;
                NotifyPropertyChanged();
            }
        }



        private readonly IFilmsService _filmsService;
        public RandomFilmsViewModel(INavigationService navigationService, IFilmsService filmsService, ICrashlyticsService crashlyticsService) : base(navigationService, crashlyticsService)
        {
            _filmsService = filmsService;
            OpenStreamingCommand = new Command(OpenStreaming);
        }

        private async void OpenStreaming()
        {
            var link = RandomShow.streamingOptions?.es?.FirstOrDefault()?.link;
            if (!string.IsNullOrEmpty(link))
            {
                await Launcher.OpenAsync(new Uri(link));
            }
        }

        public void InitValues()
        {           
            Task.Run(async () =>
            {
                var filmsResult = await _filmsService.GetRandomFilm();
                return filmsResult;
            })
            .ContinueWith(task =>
            {
                var films= task.Result;
                RandomShow = films.shows.FirstOrDefault();
                foreach (var genres in RandomShow.genres)
                {
                    Genrers += genres.name + ", ";
                }
            });
        }
    }
}
