using NetflixRandomizer.Services.Base;

namespace NetflixRandomizer.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {        
        public ErrorViewModel(INavigationService navigationService, ICrashlyticsService crashlyticsService) : base(navigationService, crashlyticsService)
        {
            
        }

        public async Task Back()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
