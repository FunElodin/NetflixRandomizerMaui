
using CommunityToolkit.Mvvm.Messaging;
using NetflixRandomizer.Localizations;
using NetflixRandomizer.Services;
using NetflixRandomizer.Services.Base;
using NetflixRandomizer.Views;
using System.Windows.Input;

namespace NetflixRandomizer.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties                
        private ILoginService _loginService;
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
                ErrorLbl = string.Empty;
                NotifyPropertyChanged();
                NotifyPropertyChanged("LoginEnabled");
            }
        }

        private string? pass;
        public string? Pass
        {
            get => pass;
            set
            {
                pass = value;
                ErrorLbl = string.Empty;
                NotifyPropertyChanged();
                NotifyPropertyChanged("LoginEnabled");
            }
        }

        public bool LoginEnabled
        {
            get 
            { 
                return !string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(user); 
            }            
        }
        #endregion

        public LoginViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            _loginService = loginService;

            this.LoginCommand = new Command((obj) => this.LoginClicked(obj));
            
            Task.Run(async () => { await LoginRemember(); });
            
            //Este ya permite levantar la pantalla de error en toda la app
            WeakReferenceMessenger.Default.Register<ErrorSendItemMessage>(this, (r, m) => { OnMessageReceived(m.Value); });

#if DEBUG
            User = "Admin";
            Pass = "Admin";
#endif
        }

        private async Task LoginRemember()
        {
            await Task.Delay(100);
            //Async para simular una llamada a API para ver si esta el usuario almacenado con su clave de refresco
        }

        private void OnMessageReceived(string value)
        {
            this.SetError(value);
        }

        private async void LoginClicked(object obj)
        {
            try
            {                
                IsLoading = true;
                bool userValid = await _loginService.CheckUser(User, Pass);
                if (!userValid)
                {
                    ErrorLbl = LoginResource.UserInvalid;
                    IsLoading = false;
                    return;
                }                

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    Application.Current.MainPage = new AppShell();

                    //var navigationParameter = new Dictionary<string, object> { { "logindata", User } };
                    //// Permitir que la UI se actualice antes de navegar
                    ////await Task.Yield();
                    //await _navigationService.PushAsync($"{nameof(MainPage)}", navigationParameter);
                    await _navigationService.PushAsync($"///{nameof(FilmsView)}");
                    //como acabo de reemplazar toda la MainPage, el Shell aún no tiene un contexto de navegación asociado, por eso necesita una ruta absoluta

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
