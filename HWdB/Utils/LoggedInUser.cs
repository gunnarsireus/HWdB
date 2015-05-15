using HWdB.Model;
using System;

namespace HWdB.Utils
{
    public class LoggedInUser
    {
        private static readonly object mutex = new object();

        /* Singleton */
        private static readonly Lazy<LoggedInUser> lazy = new Lazy<LoggedInUser>(() => new LoggedInUser());
        public static LoggedInUser Instance { get { return lazy.Value; } }

        private LoggedInUser()
        {
        }

        public User UserLoggedin;
    }
}
