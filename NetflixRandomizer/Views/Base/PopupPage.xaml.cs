namespace NetflixRandomizer.Views;

public partial class PopupPage : ContentView
{
    public PopupPage(string message)
    {
        InitializeComponent();
        popup.Show();
    }    

    public void Hide()
    {
        //popup.();
    }
}