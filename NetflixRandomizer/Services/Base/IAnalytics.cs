
#if ANDROID
using Firebase.Analytics;
#endif

namespace NetflixRandomizer.Services
{
    public interface IAnalyticsService
    {
        void Log(string eventName);
    }

    public class AnalyticsService : IAnalyticsService
    {
        public void Log(string eventName)
        {
#if ANDROID
            var firebaseAnalytics = FirebaseAnalytics.GetInstance(Platform.CurrentActivity);
            firebaseAnalytics.LogEvent(eventName, null);
#endif
        }
    }
}
