using NetflixRandomizer.ViewModels;

namespace NetflixRandomizer.Views;

public partial class FilmsView : ContentPage
{
    FilmsViewModel vm;

    public FilmsView(FilmsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = vm = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.InitValues();
    }
}
