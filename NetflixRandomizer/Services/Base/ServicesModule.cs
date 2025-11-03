
using NetflixRandomizer.Services.Base;

namespace NetflixRandomizer.Services
{
    public static class ServicesModule
    {
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            if (GlobalSettings.IsMockup)
            {                
                builder.Services.AddSingleton<ILoginService, LoginServiceMockup>();
                builder.Services.AddSingleton<IFilmsService, FilmsServiceMockup>();
            }
            else
            {
                builder.Services.AddSingleton<ILoginService, LoginService>();
                builder.Services.AddSingleton<IFilmsService, FilmsService>();            
            }

            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IHttpClientService, HttpClientService>();

            return builder;
        }
    }
}
