using NetflixRandomizer.Services.Base;
using NetflixRandomizer.ViewModels;

namespace NetflixRandomizer.Views;

public partial class ErrorPage : ContentPage, IQueryAttributable
{
    ErrorViewModel _errorViewModel;
    public ErrorPage(ErrorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _errorViewModel = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Error", out var errorObj) && errorObj is string error)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                errorLblDebug.IsVisible = true;
                errorLblDebug.Text = error;
            });
        }
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await _errorViewModel.Back();       
    }
}