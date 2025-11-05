#if ANDROID
using NetflixRandomizer.Platforms.Android;
using NetflixRandomizer.Services;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
#endif
namespace NetflixRandomizer
{

    public partial class RFIDToolsGetter
    {


#if ANDROID
        private static RFIDtools getRfidTools()
        {
            var acti = (MainActivity)Platform.CurrentActivity;
            return acti.rfidtools;
        }
#endif
#if ANDROID
        public static void SetOnRFidReceive(NFCDataReceive cb)
        {

            var rfidToosl = getRfidTools();
            rfidToosl.OnNFCDataReceive = cb;

        }
#endif

        public static Task WriteRFIDrun(string text)
        {
            var task = new TaskCompletionSource<string>();

#if ANDROID



            var rftools = getRfidTools();
            rftools.onAfterNfcWrite = () =>
            {
                rftools.onAfterNfcWrite = null;
                task.SetResult("");
            };
            rftools.recordString = text;

#endif

            return task.Task;

        }

        public static void WriteRFIDCancel()
        {

#if ANDROID
            var rftools = getRfidTools();
            rftools.onAfterNfcWrite = null;
#endif
        }
    }
}