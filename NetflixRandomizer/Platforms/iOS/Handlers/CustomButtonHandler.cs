
using Microsoft.Maui.Handlers;
using UIKit;

namespace NetflixRandomizer.Platforms.iOS
{
    public class CustomButtonHandler : ButtonHandler
    {
        protected override void ConnectHandler(UIButton platformView)
        {
            base.ConnectHandler(platformView);
            ApplyCustomStyle(platformView);
        }

        void ApplyCustomStyle(UIButton nativeButton)
        {
            nativeButton.BackgroundColor = UIColor.FromRGB(229, 9, 20);
            nativeButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            nativeButton.Layer.CornerRadius = 10;
            nativeButton.ClipsToBounds = true;
        }
    }
}
