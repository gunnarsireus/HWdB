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
    class AdministrationViewModel : ViewModelBase
    {
        public override string ButtonName { get; set; }
        public ObservableCollection<User> Users
        {
            get { return GetValue(() => Users); }
            set { SetValue(() => Users, value); InitListBox(); }
        }

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
                if (_currentUser != value)
                {
                    _currentUser = value;
                    ShowPassword = "";
                    OnPropertyChanged("CurrentUser");
                }
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
            CreateNewCurrentUser(new object());
            this.ButtonName = "Administration";
            SaveCommand = new RelayCommand(Save);
            NewUserCommand = new RelayCommand(CreateNewCurrentUser);
            DeleteUserCommand = new RelayCommand(Delete);
            InitListBox();
        }

        private void InitListBox()
        {
            User tmp = new User();
            tmp.Clone(CurrentUser);
            using (var context = new DataContext())
            {
                if (Users == null) Users = new ObservableCollection<User>();
                Users.Clear();
                {
                    context.Users.ToList().ForEach(i => Users.Add(i));
                }
            }
            CurrentUser = tmp;
        }

        private void Delete(object parameter)
        {
            if (CurrentUser.ID == 0)
            {
                System.Windows.MessageBox.Show("Cannot delete unsaved data");
                return;
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;
            using (var context = new DataContext())
            {
                User stored = context.Users.Where(a => (a.UserName == CurrentUser.UserName)).FirstOrDefault();
                if (stored == null)
                {
                    UserLogs.Instance.UserErrorLog("Error: Could not find User : " + CurrentUser.UserName);
                    return;
                }
                else
                {
                    UserLogs.Instance.UserErrorLog("Deleted User : " + CurrentUser.UserName);
                    context.Users.Remove(stored);
                    context.SaveChanges();
                    User firstItem = context.Users.FirstOrDefault();
                    if (firstItem == null)
                    {
                        CreateNewCurrentUser(new object());
                    }
                    else
                    {
                        CurrentUser = firstItem;
                    }
                }
                InitListBox();
            }
        }
        private void Save(object parameter)
        {
            if (CurrentUser.HasErrors.Count > 0)
            {
                var first = CurrentUser.HasErrors.First();
                System.Windows.MessageBox.Show(first.Value);
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
                User stored = context.Users.Where(a => (a.UserName == user.UserName)).FirstOrDefault();
                if (stored == null)
                {
                    if (ShowPassword == "")
                    {
                        System.Windows.MessageBox.Show("Password cannot be empty!");
                        return;

                    }
                    UserLogs.Instance.UserErrorLog("Saved new User : " + user.UserName);
                    user.LastLogin = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                    string hash = PasswordEncoder.GetMd5Encoding(ShowPassword);
                    user.Password = hash;
                    SaveUser(user);
                    context.Users.Add(user);
                }
                else
                {
                    UserLogs.Instance.UserErrorLog("Updated User : " + user.UserName);
                    user.ID = stored.ID;
                    user.LastLogin = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                    if (ShowPassword != "")
                    {
                        string hash = PasswordEncoder.GetMd5Encoding(ShowPassword);
                        user.Password = hash;
                    }
                    context.Entry(stored).CurrentValues.SetValues(user);
                    context.Entry(stored).State = System.Data.EntityState.Modified;
                }
                context.SaveChanges();
                InitListBox();
            }
        }

        private void CreateNewCurrentUser(object parameter)
        {
            CurrentUser = new User()
            {
                UserName = "John Doe",
                Password = "2c50afa5e6b08724001e9495f86de171",
                Email = "john.doe@gmail.com",
                Rights = "Administrator",
                Role = "Administrator",
                active = "1",
                LastLogin = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
            };
        }
    }
}
