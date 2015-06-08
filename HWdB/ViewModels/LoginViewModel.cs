using HWdB.DataAccess;
using HWdB.Model;
using HWdB.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HWdB.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        ApplicationViewModel _applikationViewModel;
        public LoginViewModel(ApplicationViewModel applikationViewModel)
        {
            DbLocation = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + @"\HWdB.mdf";
            LogoutCommand = new RelayCommand(Logout);
            LoginCommand = new RelayCommand(Login);
            ReturnCommand = new RelayCommand(Login);
            EnterCommand = new RelayCommand(Login);
            _applikationViewModel = applikationViewModel;
            ButtonName = "Logout";
        }

        private string _DbLocation;

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
                return _DbLocation;
            }
            set
            {
                if (_DbLocation != value)
                {
                    _DbLocation = value;
                    OnPropertyChanged("DbLocation");
                }
            }
        }

        private void Login(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                string password = "";
                var secureString = passwordContainer.Password;
                password = ConvertToUnsecureString(secureString);

                UsernamePassword usernamePassword = new UsernamePassword(UserName, password);
                if (UserOk(usernamePassword))
                {
                    _applikationViewModel.UserLoggedIn = true;
                }
            }
        }

        private void Logout(object parameter)
        {
            using (var context = new DataContext())
            {
                if (LoggedInUser.Instance.UserLoggedin != null)
                {
                    User stored = context.Users.FirstOrDefault(a => (a.ID == LoggedInUser.Instance.UserLoggedin.ID));
                    if (stored != null)
                    {
                        stored.LogedIn = false;
                        context.SaveChanges();
                        LoggedInUser.Instance.UserLoggedin.LogedIn = false;
                    }
                }
            }
            Application.Current.Shutdown();
        }

        private string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
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

        string _buttonName;

        public override string ButtonName
        {
            get
            {
                return _buttonName;
            }
            set
            {
                _buttonName = value;
            }
        }
        public ObservableCollection<User> AllUsers
        {
            get;
            private set;
        }

        protected override void OnDispose()
        {
            this.AllUsers.Clear();
        }

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
            StatusMessage status = new StatusMessage();
            string hash = PasswordEncoder.GetMd5Encoding(usernamePassword.Password);
            using (var context = new DataContext())
            {
                User stored = context.Users.FirstOrDefault(a => (a.UserName == usernamePassword.UserName) && (a.Password == hash));
                if (stored == null || (stored.IsActive() == false))
                {
                    UserLogs.Instance.UserErrorLog("ValidateUser() user " + usernamePassword.UserName + " not found or inactive");
                    status.success = false;
                    status.message = Properties.Strings.Username_or_password_wrong;
                    _applikationViewModel.Message(status.message);
                    return false;
                }
                else
                {
                    UserLogs.Instance.UserInfoLog("User " + usernamePassword.UserName + " logged in " + DateTime.Now.ToString());
                    stored.LogedIn = true;
                    stored.LastLogin = DateTime.Now.ToString();
                    context.Entry(stored).State = System.Data.EntityState.Modified;
                    context.SaveChanges();
                    LoggedInUser.Instance.UserLoggedin = stored;
                    return true;
                }
            }
        }
    }
}
