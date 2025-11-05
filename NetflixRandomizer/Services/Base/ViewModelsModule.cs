using NetflixRandomizer.ViewModels;
using NetflixRandomizer.Views;

namespace NetflixRandomizer.Services
{
    public static class ViewModelsModule
    {
        //Check if singleton or transient, best aproximation is singleton
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ErrorViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<FilmsViewModel>();
            builder.Services.AddTransient<RandomFilmsViewModel>();

            return builder;
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            //LOGIN
            builder.Services.AddTransient<LoginView>();

            builder.Services.AddTransient<ErrorPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<FilmsView>();
            builder.Services.AddTransient<RandomFilmView>();
            builder.Services.AddTransient<NfcView>();

            return builder;
        }
    }
}
