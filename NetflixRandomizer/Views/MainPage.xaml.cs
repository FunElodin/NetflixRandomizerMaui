using NetflixRandomizer.ViewModels;
using NetflixRandomizer.Views;

namespace NetflixRandomizer
{
    public partial class MainPage : ContentPage
    {
        MainViewModel vm;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = vm = viewModel;
        }

        private void ErrorClicked(object? sender, EventArgs e)
        {
            vm.SetError("TEST ERROR");
        }

        private void PopupClicked(object? sender, EventArgs e)
        {
            var popupView = new PopupPage("Test popup");                        
            
            this.Content = new Grid
            {
                Children = { this.Content, popupView }
            };
        }

        private void ActionSheetClicked(object? sender, EventArgs e)
        {
            List<string> options = new List<string>() { "test 1", "test 2", "test 3" };
            vm.SetActionSheet("TEST Action Sheet", options.ToArray());
        }

        private void AlertClicked(object? sender, EventArgs e)
        {
            vm.SetAlert("TEST Alert");
        }

    }
}
