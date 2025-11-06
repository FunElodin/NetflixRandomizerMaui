
using NetflixRandomizer.Models;
using NetflixRandomizer.Services;
using NetflixRandomizer.Services.Base;
using System.Collections.ObjectModel;

namespace NetflixRandomizer.ViewModels
{

    public class FilmsViewModel : BaseViewModel
    {
        private Films films;
        public Films Films
        {
            get
            {
                return films;
            }
            set
            {
                films = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Show> listFilms;
        public ObservableCollection<Show> ListFilms
        {
            get
            {
                return listFilms;
            }
            set
            {
                listFilms = value;
                NotifyPropertyChanged();
            }
        }

        private readonly IFilmsService _filmsService;
        public FilmsViewModel(INavigationService navigationService, IFilmsService filmsService, ICrashlyticsService crashlyticsService) : base(navigationService, crashlyticsService)
        {
            _filmsService = filmsService;
        }

        public void InitValues()
        {
            ListFilms = new ObservableCollection<Show>();
            Task.Run(async () =>
            {
                var filmsResult = await _filmsService.GetFilms();
                return filmsResult;
            })
            .ContinueWith(task =>
            {
                var films = task.Result;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    foreach (var show in films.shows)
                    {
                        ListFilms.Add(show);
                    }
                });
            });            
        }
    }
}
