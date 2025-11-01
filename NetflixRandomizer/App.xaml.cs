using NetflixRandomizer.Views;

namespace NetflixRandomizer
{
    public partial class App : Application
    {
        private readonly LoginView loginView;
        public App(LoginView login)
        {
            InitializeComponent();
            loginView = login;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(loginView);
        }
    }
}