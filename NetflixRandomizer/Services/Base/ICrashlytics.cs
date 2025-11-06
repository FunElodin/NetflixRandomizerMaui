#if ANDROID
using Firebase.Crashlytics;
#endif
namespace NetflixRandomizer
{
    public interface ICrashlyticsService
    {
        void LogError(Exception ex);
        void LogCrashlytics(string msg);
    }
    public class CrashlyticsService : ICrashlyticsService
    {
        public void LogCrashlytics(string msg)
        {
#if ANDROID
            FirebaseCrashlytics.Instance.Log(msg);
#endif
            Console.WriteLine(msg);
        }

        public void LogError(Exception ex)
        {
#if ANDROID
            FirebaseCrashlytics.Instance.RecordException(Java.Lang.Throwable.FromException(ex));
#endif
            Console.WriteLine(ex.ToString());
            Console.Write(ex.StackTrace);
        }
    }

}
