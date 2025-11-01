
using NetflixRandomizer.Services.Base;

namespace NetflixRandomizer.Services
{
    public static class ServicesModule
    {
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            if (GlobalSettings.IsMockup)
            {                

            }
            else
            {
            }

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder;
        }
    }
}
