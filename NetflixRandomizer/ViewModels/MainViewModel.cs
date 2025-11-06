using NetflixRandomizer.Services.Base;

namespace NetflixRandomizer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService, ICrashlyticsService crashlyticsService) : base(navigationService, crashlyticsService)
        {
        }
    }
}
