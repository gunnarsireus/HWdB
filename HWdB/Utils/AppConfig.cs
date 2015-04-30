
namespace HWdB.Utils
{
    public static class AppConfig
    {
        public static bool LoggingOn
        {
            get
            {
                bool loggingon = Properties.Settings.Default.LoggingOn;
                if (loggingon)
                {
                   return true;
                }
                return false;
            }
        }
    }
}
