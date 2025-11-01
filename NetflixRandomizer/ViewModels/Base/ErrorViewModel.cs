using NetflixRandomizer.Services.Base;

namespace NetflixRandomizer.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {        
        public ErrorViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }

        public async Task Back()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
