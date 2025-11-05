using Android.App;
using Android.Content;
using Android.Nfc;
using NetflixRandomizer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixRandomizer
{
    public class RFIDtools
    {
        private NfcAdapter _nfcAdapter;
        private Activity act;


        public delegate void AfterNFCwrite();
        public AfterNFCwrite onAfterNfcWrite;

        public string recordString;
        public RFIDtools(Activity actIn)
        {
            act = actIn;
            _nfcAdapter = NfcAdapter.GetDefaultAdapter(act);
        }


        public NFCDataReceive OnNFCDataReceive;

        public void OnResume()
        {
            if (_nfcAdapter == null)
            {
                var alert = new Android.App.AlertDialog.Builder(act).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.Show();
            }
            else
            {
                //Set events and filters
                var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
                var ndefDetected = new IntentFilter(NfcAdapter.ActionNdefDiscovered);
                var techDetected = new IntentFilter(NfcAdapter.ActionTechDiscovered);
                var filters = new[] { ndefDetected, tagDetected, techDetected };

                var intent = new Intent(act, act.GetType()).AddFlags(ActivityFlags.SingleTop);

                var pendingIntent = PendingIntent.GetActivity(act, 0, intent, PendingIntentFlags.Mutable);

                // Gives your current foreground activity priority in receiving NFC events over all other activities.
                _nfcAdapter.EnableForegroundDispatch(act, pendingIntent, filters, null);
            }
        }

        public void OnNewIntent(Intent intent)
        {

            if (intent.Action != NfcAdapter.ActionTagDiscovered) return;

            if (onAfterNfcWrite != null)
            {
                RFID_Simple.WriteData(intent, recordString);
                onAfterNfcWrite();
                return;
            }

            var snc = RFID_Simple.GetSNCRecord(intent);
            var rfid = RFID_Simple.GetRfID(intent);

            if (OnNFCDataReceive != null)
            {
                OnNFCDataReceive(
                    rfid: rfid,
                    sncRfid: snc

                );
            }
        }
    }
}
