using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using NetflixRandomizer;
using System.Text;

namespace NetflixRandomizer
{
    public class RFID_Simple
    {
        public static string GetSNCRecord(Intent intent)
        {
            try
            {
                var rfidMsg = intent.GetParcelableArrayExtra(Android.Nfc.NfcAdapter.ExtraNdefMessages);

                if (rfidMsg == null) return "";

                foreach (var msg in rfidMsg)
                {
                    var ndefMessage = (NdefMessage)msg;
                    var records = ndefMessage.GetRecords();
                    if (records == null) continue;

                    foreach (var r in records)
                    {
                        var payload = r.GetPayload();
                        if (payload == null || payload.Length == 0) continue;

                        var text = Encoding.UTF8.GetString(payload, 1, payload.Length - 1);
                        if (text.StartsWith("en"))
                        {
                            text = text.Substring(2);
                        }

                        return text;

                    }
                }


            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine("GetSNCRecord Error : " + e.Message);
            }


            return "";

        }

        public static string GetRfID(Intent intent)
        {

            string result = "";

            try
            {

                var myTag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag);
                var id = myTag.GetId();
                result = BitConverter.ToString(id);

                result = result.Replace("-", "");

            }
            catch (Exception e)
            {
                var x = e.Message;
            }

            return result;

        }

        public static void WriteData(Intent intent, string txtstring)
        {
            try
            {

                var myTag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag);
                var ndef = Ndef.Get(myTag);

                if (ndef == null || !ndef.IsWritable) { return; }
                var message = new NdefMessage(
                    [
                        NdefRecord.CreateTextRecord("en", txtstring)
                    ]

                );


                ndef.Connect();
                ndef.WriteNdefMessage(message);
                ndef.Close();

            }
            catch (Exception e)
            {
                var x = e.Message;
            }

        }
    }
}
