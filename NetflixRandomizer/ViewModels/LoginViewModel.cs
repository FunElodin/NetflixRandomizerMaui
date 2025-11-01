
using CommunityToolkit.Mvvm.Messaging;
using NetflixRandomizer.Services.Base;
using System.Windows.Input;

namespace NetflixRandomizer.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties                
        public ICommand LoginCommand { get; set; }


        private string? errorLbl;
        public string? ErrorLbl
        {
            get => errorLbl;
            set
            {
                errorLbl = value;
                NotifyPropertyChanged();
            }
        }

        private string? user;
        public string? User
        {
            get => user;
            set
            {
                user = value;
                NotifyPropertyChanged();
            }
        }

        private string? pass;
        public string? Pass
        {
            get => pass;
            set
            {
                pass = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.LoginCommand = new Command((obj) => this.LoginClicked(obj));
            Task.Run(async () => { await LoginRemember(); });
            //Este ya permite levantar la pantalla de error en toda la app
            WeakReferenceMessenger.Default.Register<ErrorSendItemMessage>(this, (r, m) => { OnMessageReceived(m.Value); });
        }

        private async Task LoginRemember()
        {
            await Task.Delay(100);
            //Async para simular una llamada a API para ver si esta el usuario almacenado con su clave de refresco
        }

        private void OnMessageReceived(string value)
        {
            Task.Run(async () =>
            {
                await _navigationService.ShowErrorAsync(value);
            });
        }

        private async void LoginClicked(object obj)
        {
            try
            {
                IsLoading = true;
                //loggedUser = await _googleAuthService.GetCurrentUserAsync();

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    Application.Current.MainPage = new AppShell();                     

                    var navigationParameter = new Dictionary<string, object> { { "logindata", User } };
                    // Permitir que la UI se actualice antes de navegar
                    //await Task.Yield();
                    await _navigationService.PushAsync($"{nameof(MainPage)}", navigationParameter);                    

                });
            }
            catch (Exception ex)
            {
                await _navigationService.ShowErrorAsync(ex.Message);                
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
