using NetflixRandomizer.Services;
using NetflixRandomizer.Services.Base;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NetflixRandomizer.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        protected readonly INavigationService _navigationService;
        private readonly ICrashlyticsService _crashlyticsService;
        public BaseViewModel(INavigationService navigationService, ICrashlyticsService crashlyticsService)
        {
            _navigationService = navigationService;
            _crashlyticsService = crashlyticsService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                NotifyPropertyChanged();
            }
        }

        private string? error;
        public string? Error
        {
            get => error;
            set
            {
                error = value;
                NotifyPropertyChanged();
            }
        }

        public void TrackError(string msg)
        {
            _crashlyticsService.LogCrashlytics(msg);
        }

        public void TrackError(Exception ex)
        {
            _crashlyticsService.LogError(ex);
        }

        public void SetError(string error)
        {
            Task.Run(async () =>
            {
                await _navigationService.ShowErrorAsync(error);
            });
        }

        public void SetActionSheet(string msg, string[] options)
        {
            Task.Run(async () =>
            {                
                await _navigationService.DisplayActionSheet(msg, options);
            });
        }

        public void SetAlert(string msg)
        {
            Task.Run(async () =>
            {
                await _navigationService.DisplayAlert("title", msg, "Vale");
            });
        }

        public void SetPopup(string msg)
        {
            Task.Run(async () =>
            {                
                await _navigationService.ShowPopupAsync(msg);
            });
        }

    }
}
