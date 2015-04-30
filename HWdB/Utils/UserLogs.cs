using System;

namespace HWdB.Utils
{
    public class UserLogs
    {
        private static readonly object mutex = new object();

        /* Singleton */
        private static readonly Lazy<UserLogs> lazy = new Lazy<UserLogs>(() => new UserLogs());
        public static UserLogs Instance { get { return lazy.Value; } }

        private UserLogs()
        {
        }

        public void UserInfoLog(string info)
        {
            if (AppConfig.LoggingOn)
            {
                lock (mutex)
                {
                    WriteLog(" --- " + info);
                }
            }
        }

        public void UserErrorLog(string error)
        {
            lock (mutex)
            {
                WriteLog(" *** ERROR: " + error);
            }
        }

        private void WriteLog(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";
            using (var file = new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(DateTime.Now.ToLocalTime().ToString() + message);
            }
        }
    }
}
