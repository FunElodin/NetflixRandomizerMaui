using NetflixRandomizer.Services;

namespace NetflixRandomizer.Views;

public partial class NfcView : ContentPage
{
    public NfcView()
    {
        InitializeComponent();
#if ANDROID
        RFIDToolsGetter.SetOnRFidReceive(async (rfid, sncRfid) =>
        {
            OutputLabel.Text = rfid;
            var salida = sncRfid;
        });
#endif
    }

    private async void OnReadClicked(object sender, EventArgs e)
    {
    }

    private async void OnWriteClicked(object sender, EventArgs e)
    {
        await RFIDToolsGetter.WriteRFIDrun("TEST");
        MainThread.BeginInvokeOnMainThread(() =>
        {
            OutputLabel.Text = "Escribiendo";           
        });
    }
}