using NetflixRandomizer.ViewModels;

namespace NetflixRandomizer.Views;

public partial class LoginView : ContentPage
{
    LoginViewModel vm;
    double imageWidth = 800;
    double speed = 0.25; 
    bool isAnimating = true;

    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = vm = viewModel;
        PrepareAnimations();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = MainThread.InvokeOnMainThreadAsync(Animate);
        StartInfiniteScroll();
    }

    private void PrepareAnimations()
    {
        user.TranslationY = 800;
        background.TranslationY = 500;        
        user.TranslationX = 500;
        pass.TranslationX = 500;
    }

    private async void Animate()
    {
        uint animationTimeInMs = 1000;

        //Delay Animation for appearing
        await Task.Delay(400);
        await Task.WhenAll(new List<Task>
        {
            user.TranslateTo(0, 0, animationTimeInMs, Easing.SinInOut),
            background.TranslateTo(0, 0, animationTimeInMs, Easing.SinInOut),            
            user.TranslateTo(0, 0, animationTimeInMs, Easing.SinInOut),
            pass.TranslateTo(0, 0, animationTimeInMs, Easing.SinInOut),
        });

        await button.ScaleTo(1, animationTimeInMs, Easing.BounceOut);

    }
    async void StartInfiniteScroll()
    {
        while (isAnimating)
        {
            // Mover las imágenes hacia la izquierda
            Image1.TranslationX -= speed;
            Image2.TranslationX -= speed;

            // Si una imagen se sale completamente del área visible, la mandamos al final
            if (Image1.TranslationX <= -imageWidth)
            {
                Image1.TranslationX = Image2.TranslationX + imageWidth*2;
            }

            if (Image2.TranslationX <= -imageWidth*2)
            {
                Image2.TranslationX = Image1.TranslationX + imageWidth;
            }

            await Task.Delay(16); // ~60 FPS (1000ms / 60 ≈ 16ms)
        }
    }


}