using HWdB.DataAccess;
using HWdB.Model;
using HWdB.MVVMFramework;
using HWdB.Properties;
using HWdB.Utils;
using MHWdB.CustomValidationAttributes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HWdB.ViewModels
{
    class AdministrationViewModel : BaseViewModel
    {
        public override sealed string ButtonName { get; set; }
        public ObservableCollection<User> UsersObs
        {
            get { return GetValue(() => UsersObs); }
            set { SetValue(() => UsersObs, value); }
        }

        public int UserId //Needed for PasswordMinLenghtOrEmptyAttribute validation
        {
            get
            {
                { return CurrentUser == null ? 0 : CurrentUser.Id; }
            }
        }
        [ExcludeChar("/.,!#$%", ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "AdministrationViewModel_ShowPassword_Password_contains_invalid_letters")]
        [PasswordMinLenghtOrEmpty(5)]
        public string ShowPassword
        {
            get { return GetValue(() => ShowPassword); }
            set
            {
                SetValue(() => ShowPassword, value);
            }
        }

        User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (_currentUser == value) return;
                if (_currentUser != null)
                {
                    _currentUser.IsSelected = false;
                }
                _currentUser = value;
                _currentUser.IsSelected = true;
                OnPropertyChanged("ShowPassword");
                OnPropertyChanged("CurrentUser");
            }
        }

        public User SelectedListBoxItem
        {
            get { return GetValue(() => SelectedListBoxItem); }
            set
            {
                SetValue(() => SelectedListBoxItem, value);
                CurrentUser = value;
            }
        }

        public int SelectedIndex
        {
            get { return GetValue(() => SelectedIndex); }
            set { SetValue(() => SelectedIndex, value); }
        }
        public ICommand NextCommand
        {
            get;
            private set;
        }
        public ICommand PreviousCommand
        {
            get;
            private set;
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        public ICommand NewUserCommand
        {
            get;
            private set;
        }
        public ICommand DeleteUserCommand
        {
            get;
            private set;
        }

        public AdministrationViewModel()
        {
            ButtonName = "Administration";
            SaveCommand = new RelayCommand(Save);
            NewUserCommand = new RelayCommand(CreateNewCurrentUser);
            DeleteUserCommand = new RelayCommand(Delete);
            NextCommand = new RelayCommand(Next);
            PreviousCommand = new RelayCommand(Previous);
            InitListBox();
            if (UsersObs.Any())
            {
                SelectedListBoxItem = UsersObs[0];
                SelectedIndex = 0;
            }
            else
            {
                CreateNewCurrentUser(new object());
            }
        }

        private void InitListBox()
        {
            using (var context = new DataContext())
            {
                if (UsersObs == null) UsersObs = new ObservableCollection<User>();
                UsersObs.Clear();
                {
                    context.Users.ToList().ForEach(i => UsersObs.Add(i));
                }
            }
        }

        private void Next(object parameter)
        {
            if (UsersObs.Count <= 1) return;
            SelectedIndex = (SelectedIndex + 1) % UsersObs.Count;
            SelectedListBoxItem = UsersObs[SelectedIndex];
        }
        private void Previous(object parameter)
        {
            if (UsersObs.Count <= 1) return;
            SelectedIndex = (SelectedIndex - 1);
            if (SelectedIndex < 0)
            {
                SelectedIndex = UsersObs.Count - 1;
            }
            SelectedListBoxItem = UsersObs[SelectedIndex];
        }
        private void Delete(object parameter)
        {
            if (CurrentUser.Id == 0)
            {
                MessageBox.Show("Cannot delete unsaved data");
                return;
            }
            if (CurrentUser.UserName == LoggedInUser.Instance.UserLoggedin.UserName)
            {
                MessageBox.Show("Cannot delete own account");
                return;
            }
            UiServices.SetBusyState();
            var messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;
            using (var context = new DataContext())
            {
                var oldUser = context.Users.FirstOrDefault(a => (a.UserName == CurrentUser.UserName));
                if (NotStoredInDb(oldUser))
                {
                    UserLogs.Instance.UserErrorLog("Error: Could not find User : " + CurrentUser.UserName);
                    return;
                }

                UserLogs.Instance.UserErrorLog("Deleted User : " + CurrentUser.UserName);
                var list = context.LtbDataSets.Where(l => l.CreatedBy == oldUser.UserName).ToList();
                foreach (var ltbdataset in list)
                {
                    context.LtbDataSets.Remove(ltbdataset);
                }
                context.SaveChanges();
                context.Users.Remove(oldUser);
                context.SaveChanges();

                if (!context.Users.Any())
                {
                    CreateNewCurrentUser(new object());
                    UsersObs.Clear();
                    return;
                }
                UsersObs.Remove(CurrentUser);
                SelectedListBoxItem = UsersObs[0];
                SelectedIndex = 0;
            }
        }
        private void Save(object parameter)
        {
            if (CurrentUser.HasErrors.Count > 0)
            {
                var first = CurrentUser.HasErrors.First();
                MessageBox.Show(first.Value);
            }
            else
                if (HasErrors.Count > 0)
                {
                    var first = this.HasErrors.First();
                    MessageBox.Show(first.Value);
                }
                else
                {
                    UiServices.SetBusyState();
                    SaveUser(CurrentUser);
                }
        }

        private void SaveUser(User user)
        {
            using (var context = new DataContext())
            {
                var oldUser = context.Users.FirstOrDefault(a => (a.UserName == user.UserName));
                if (NotStoredInDb(oldUser))
                {
                    if ((ShowPassword == null) || (ShowPassword.Trim() == ""))
                    {
                        MessageBox.Show("Password cannot be empty!");
                        return;

                    }
                    UserLogs.Instance.UserErrorLog("Saved new User : " + user.UserName);
                    user.LastLogin = "Never logged in";
                    var hash = PasswordEncoder.GetMd5Encoding(ShowPassword);
                    user.Password = hash;
                    ShowPassword = "";
                    context.Users.Add(user);
                    context.SaveChanges();
                    UsersObs.Add(user);
                    SelectedIndex = UsersObs.Count - 1;
                    SelectedListBoxItem = UsersObs[SelectedIndex];
                    return;
                }

                UserLogs.Instance.UserErrorLog("Updated User : " + user.UserName);
                //user.Id = oldUser.Id;
                if (!string.IsNullOrEmpty(ShowPassword))
                {
                    var hash = PasswordEncoder.GetMd5Encoding(ShowPassword);
                    user.Password = hash;
                    ShowPassword = "";
                }
                context.Entry(oldUser).CurrentValues.SetValues(user);
                context.Entry(oldUser).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static bool NotStoredInDb(User stored)
        {
            return stored == null;
        }

        private void CreateNewCurrentUser(object parameter)
        {
            CurrentUser = new User()
            {
                UserName = "johndoe",
                Password = "2c50afa5e6b08724001e9495f86de171",
                Email = "john.doe@gmail.com",
                Rights = "Administrator",
                Role = "Administrator",
                Active = "1",
                LastLogin = "Press 'Save'"
            };
        }
    }
}
