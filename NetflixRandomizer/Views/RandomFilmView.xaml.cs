using NetflixRandomizer.ViewModels;

namespace NetflixRandomizer.Views;

public partial class RandomFilmView : ContentPage
{	

    RandomFilmsViewModel vm;

    public RandomFilmView(RandomFilmsViewModel viewModel)
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