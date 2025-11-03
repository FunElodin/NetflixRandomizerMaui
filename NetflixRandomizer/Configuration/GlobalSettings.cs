
namespace NetflixRandomizer
{
    public class GlobalSettings
    {
#if DEBUG
        public const bool IsMockup = true;
#else
        public const bool IsMockup = false;
#endif
        public static string BaseApiUri => "https://streaming-availability.p.rapidapi.com";
    }
}
