using HWdB.MVVMFramework;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HWdB.ViewModels
{
    class ApplicationViewModel : BaseViewModel
    {
        public override sealed string Title { get; set; }
        public void Logout()
        {
            Application.Current.Shutdown();
        }

        public void Login()
        {
            CurrentPageViewModel = new LoginViewModel(this);
        }

        bool _userLoggedIn;
        public bool UserLoggedIn
        {
            get
            {
                return _userLoggedIn;
            }
            set
            {
                if (_userLoggedIn == value) return;
                _userLoggedIn = value;
                if (_userLoggedIn)
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

        public ApplicationViewModel()
        {
            UserLoggedIn = false;
            CurrentPageViewModel = new LoginViewModel(this);
        }


        private ICommand _changePageCommand;
        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand ?? (_changePageCommand = new RelayCommand(
                    p => ChangeViewModel((BaseViewModel)p),
                    p => p is BaseViewModel));
            }
        }

        private ObservableCollection<BaseViewModel> _pageViewModels;
        public ObservableCollection<BaseViewModel> PageViewModels
        {
            get { return _pageViewModels ?? (_pageViewModels = new ObservableCollection<BaseViewModel>()); }
        }

        private BaseViewModel _currentPageViewModel;
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
            }
        }

        private void ChangeViewModel(BaseViewModel viewModel)
        {
            if (viewModel.GetType() == typeof(LoginViewModel))
            {
                UserLoggedIn = false;
            }

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }
    }
}
