﻿using HWdB.DataAccess;
using HWdB.Model;
using HWdB.MVVMFramework;
using HWdB.Utils;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HWdB.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        readonly ApplicationViewModel _applikationViewModel;
        public LoginViewModel(ApplicationViewModel applikationViewModel)
        {
            DbLocation = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + @"\HWdB.mdf";
            LogoutCommand = new RelayCommand(Logout);
            LoginCommand = new RelayCommand(Login);
            ReturnCommand = new RelayCommand(Login);
            EnterCommand = new RelayCommand(Login);
            _applikationViewModel = applikationViewModel;
            Title = "Logout";
        }

        private string _dbLocation;

        public ICommand LoginCommand
        {
            get;
            private set;
        }

        public ICommand LogoutCommand
        {
            get;
            private set;
        }
        public ICommand ReturnCommand
        {
            get;
            private set;
        }
        public ICommand EnterCommand
        {
            get;
            private set;
        }
        public string UserName
        {
            get;
            set;
        }

        public string DbLocation
        {
            get
            {
                return _dbLocation;
            }
            set
            {
                if (_dbLocation == value) return;
                _dbLocation = value;
                OnPropertyChanged("DbLocation");
            }
        }

        private void Login(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer == null) return;
            var password = "";
            var secureString = passwordContainer.Password;
            password = ConvertToUnsecureString(secureString);

            var usernamePassword = new UsernamePassword(UserName, password);
            if (UserOk(usernamePassword))
            {
                _applikationViewModel.UserLoggedIn = true;  //Shows menu at the bottom
            }
        }

        private static void Logout(object parameter)
        {
            using (var context = new DataContext())
            {
                if (LoggedInUser.Instance.UserLoggedin != null)
                {
                    var storedUser = context.Users.FirstOrDefault(a => (a.Id == LoggedInUser.Instance.UserLoggedin.Id));
                    if (storedUser != null)
                    {
                        storedUser.LogedIn = false;
                        context.SaveChanges();
                        LoggedInUser.Instance.UserLoggedin.LogedIn = false;
                    }
                }
            }
            Application.Current.Shutdown();
        }

        private static string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public override sealed string Title { get; set; }

        public bool UserOk(UsernamePassword usernamePassword)
        {
            using (var context = new DataContext())
            {
                if (!context.Users.Any())
                {
                    context.Users.Add(User.CreateUser("admin", "2c50afa5e6b08724001e9495f86de171", "admin@gmail.com", "Administrator", "Administrator"));
                    context.SaveChanges();
                }
            }
            var status = new StatusMessage();
            var hash = PasswordEncoder.GetMd5Encoding(usernamePassword.Password);
            using (var context = new DataContext())
            {
                var stored = context.Users.FirstOrDefault(a => (a.UserName == usernamePassword.UserName) && (a.Password == hash));
                if (stored == null || (stored.IsActive() == false))
                {
                    UserLogs.Instance.UserErrorLog("ValidateUser() user " + usernamePassword.UserName + " not found or inactive");
                    status.Success = false;
                    status.Message = Properties.Strings.Username_or_password_wrong;
                    MessageBox.Show(status.Message);
                    return false;
                }
                UserLogs.Instance.UserInfoLog("User " + usernamePassword.UserName + " logged in " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                stored.LogedIn = true;
                stored.LastLogin = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                context.Entry(stored).State = System.Data.EntityState.Modified;
                context.SaveChanges();
                LoggedInUser.Instance.UserLoggedin = stored;
                return true;
            }
        }
    }
}
