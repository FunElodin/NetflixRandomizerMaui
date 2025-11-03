using NetflixRandomizer.Views;

namespace NetflixRandomizer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ErrorPage), typeof(ErrorPage));
            Routing.RegisterRoute(nameof(PopupPage), typeof(PopupPage));
            Routing.RegisterRoute(nameof(FilmsView), typeof(FilmsView));
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
            ClearNavigation(args);
            // HACK: Cancel any back navigation.
            //if (args.Source == ShellNavigationSource.Pop)
            //{
            //    args.Cancel();
            //}
        }

        private void ClearNavigation(ShellNavigatingEventArgs args)
        {
            if (args.Source == ShellNavigationSource.ShellSectionChanged)
            {
                var navigation = Shell.Current.Navigation;
                var pages = navigation.NavigationStack;
                for (var i = pages.Count - 1; i >= 1; i--)
                {
                    navigation.RemovePage(pages[i]);
                }
            }
        }
    }
}
