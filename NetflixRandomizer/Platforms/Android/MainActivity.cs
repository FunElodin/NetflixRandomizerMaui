using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Annotations;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace NetflixRandomizer
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        static readonly int REQUEST_CAMERA = 0;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            string cameraPermission = Android.Manifest.Permission.Camera;
            if(!(ContextCompat.CheckSelfPermission(this, cameraPermission) == (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera, Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, REQUEST_CAMERA);
            }
            base.OnCreate(savedInstanceState);

        }
    }
}
