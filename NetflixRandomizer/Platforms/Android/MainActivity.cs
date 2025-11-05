using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using NetflixRandomizer.Platforms.Android;

namespace NetflixRandomizer
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        public RFIDtools rfidtools;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            rfidtools = new RFIDtools(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            rfidtools.OnResume();
        }


        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);
            rfidtools.OnNewIntent(intent);

        }
    }
}
