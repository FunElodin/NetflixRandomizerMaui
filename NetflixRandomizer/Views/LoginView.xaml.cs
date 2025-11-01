using NetflixRandomizer.ViewModels;

namespace NetflixRandomizer.Views;

public partial class LoginView : ContentPage
{
    LoginViewModel vm;

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
}