using Android.Graphics.Drawables;
using Google.Android.Material.Button;  
using Microsoft.Maui.Handlers;
using Color = Android.Graphics.Color;

namespace NetflixRandomizer.Platforms.Android
{
    public class CustomButtonHandler : ButtonHandler
    {
        protected override void ConnectHandler(MaterialButton platformView) 
        {
            base.ConnectHandler(platformView);
            ApplyCustomStyle(platformView);
        }

        void ApplyCustomStyle(MaterialButton nativeButton)
        {
            var bg = new GradientDrawable();
            bg.SetColor(Color.ParseColor("#E50914"));
            bg.SetCornerRadius(60f);

            nativeButton.Background = bg;
            nativeButton.SetTextColor(Color.White);
            nativeButton.SetAllCaps(false);            
            nativeButton.StateListAnimator = null;
            nativeButton.Elevation = 0;
        }
    }
}
