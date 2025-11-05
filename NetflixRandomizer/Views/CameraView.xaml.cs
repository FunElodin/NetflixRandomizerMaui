using NetflixRandomizer.ViewModels;

namespace NetflixRandomizer.Views;

public partial class CameraView : ContentPage
{
	private CameraViewModel vm;

    public CameraView(CameraViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = vm = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}