
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
            }
            else
            {
                builder.Services.AddSingleton<ILoginService, LoginService>();
            }

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder;
        }
    }
}
