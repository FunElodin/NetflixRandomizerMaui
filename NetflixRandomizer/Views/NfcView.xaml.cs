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

    private async void OnWriteClicked(object sender, EventArgs e)
    {
#if ANDROID
        await RFIDToolsGetter.WriteRFIDrun("TEST");
        MainThread.BeginInvokeOnMainThread(() =>
        {
            OutputLabel.Text = "Escribiendo";
        });
#endif

    }
}