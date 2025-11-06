using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using NetflixRandomizer.Handlers;
using NetflixRandomizer.Services;
using Syncfusion.Maui.Toolkit.Hosting;

namespace NetflixRandomizer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionToolkit()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews()
                .ConfigureMauiHandlers((handlers) => { AddCustomHandlers(handlers); })
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android.OnCreate((activity, bundle) =>
                    {
                        Firebase.FirebaseApp.InitializeApp(activity);
                    }));
#endif
                })         
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                ;

            ConfigureiOSSettings();
            ConfigureAndroidSettings();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        private static void ConfigureAndroidSettings()
        {
#if ANDROID

#endif
        }

        private static void ConfigureiOSSettings()
        {
#if IOS
            
#endif
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            return ServicesModule.RegisterServices(builder);
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            return ViewModelsModule.RegisterViewModels(builder);
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            return ViewModelsModule.RegisterViews(builder);
        }

        private static void AddCustomHandlers(IMauiHandlersCollection handlers)
        {
#if ANDROID
            handlers.AddHandler(typeof(CustomButton), typeof(Platforms.Android.CustomButtonHandler));
#elif IOS
        handlers.AddHandler(typeof(CustomButton), typeof(Platforms.iOS.CustomButtonHandler));
#endif
        }
    }
}
