using HWdB.Model;
using HWdB.MVVMFramework;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HWdB.ViewModels
{
    class ApplicationViewModel : BaseViewModel
    {

        string _buttonName;
        bool _userLoggedIn;

        public void Logout()
        {
            Application.Current.Shutdown();
        }

        public void Message(string message)
        {
            MessageBox.Show(message);
        }

        public void Login()
        {
            CurrentPageViewModel = new LoginViewModel(this);
        }

        public bool UserLoggedIn
        {
            get
            {
                return _userLoggedIn;
            }
            set
            {
                if (_userLoggedIn != value)
                {
                    _userLoggedIn = value;
                    if (_userLoggedIn == true)
                    {
                        PageViewModels.Clear();
                        PageViewModels.Add(new LoginViewModel(this));
                        //PageViewModels.Add(new ProductsViewModel());
                        //PageViewModels.Add(new ProductGroupsViewModel());
                        //PageViewModels.Add(new ExportViewModel());
                        //PageViewModels.Add(new ImportViewModel());
                        //PageViewModels.Add(new RepairViewModel());
                        //PageViewModels.Add(new SupplyViewModel());
                        //PageViewModels.Add(new StrategyViewModel());
                        PageViewModels.Add(new LtbViewModel());
                        PageViewModels.Add(new AdministrationViewModel());
                        CurrentPageViewModel = PageViewModels[1];
                    }
                    OnPropertyChanged("UserLoggedIn");
                }
            }
        }

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

        private ICommand _changePageCommand;
        private BaseViewModel _currentPageViewModel;
        private ObservableCollection<BaseViewModel> _pageViewModels;

        public ApplicationViewModel()
        {
            UserLoggedIn = false;
            PageViewModels.Add(new LoginViewModel(this));


            // Set starting page

            CurrentPageViewModel = PageViewModels[0];
        }

        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand ?? (_changePageCommand = new RelayCommand(
                    p => ChangeViewModel((BaseViewModel)p),
                    p => p is BaseViewModel));
            }
        }

        public ObservableCollection<BaseViewModel> PageViewModels
        {
            get { return _pageViewModels ?? (_pageViewModels = new ObservableCollection<BaseViewModel>()); }
        }

        public BaseViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel == value) return;
                _currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
                if (value.ButtonName != "LTB") return;
                var ltbViewModel = (LtbViewModel)PageViewModels[PageViewModels.Count - 2];
                ltbViewModel.InitListBox();
            }
        }

        private void ChangeViewModel(BaseViewModel viewModel)
        {
            if (viewModel.GetType() == typeof(LoginViewModel))
            {
                UserLoggedIn = false;
            }
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }
    }
}
