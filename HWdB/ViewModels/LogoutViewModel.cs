using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HWdB.Utils;
namespace HWdB.ViewModels
{
    class LogoutViewModel : ViewModelBase
    {
        ApplicationViewModel _applikationViewModel;
        public LogoutViewModel(ApplicationViewModel applikationViewModel)
        {
            LogoutCommand = new RelayCommand(Logout);
            LoginCommand = new RelayCommand(Login);
            ButtonName = "Logout";
            _applikationViewModel = applikationViewModel;
        }
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
        private void Login(object parameter)
        {
            _applikationViewModel.Login();
        }

        private void Logout(object parameter)
        {
            _applikationViewModel.Logout();
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
    }
}
