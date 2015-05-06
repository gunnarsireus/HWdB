using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using HWdB.Model;

namespace HWdB.ViewModels
{
    class ApplicationViewModel : ViewModelBase
    {

        string _buttonName;
        bool _userLoggedIn;

        public void Logout(){
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
                _userLoggedIn = value;
                CurrentPageViewModel = new ProductsViewModel();
                OnPropertyChanged("UserLoggedIn");
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
        private ViewModelBase _currentPageViewModel;
        private ObservableCollection<ViewModelBase> _pageViewModels;

        public ApplicationViewModel()
        {
            UserLoggedIn = false;
            PageViewModels.Add(new LoginViewModel(this));
            PageViewModels.Add(new ProductsViewModel());
            PageViewModels.Add(new ProductGroupsViewModel());
            PageViewModels.Add(new ExportViewModel());
            PageViewModels.Add(new ImportViewModel());
            PageViewModels.Add(new RepairViewModel());
            PageViewModels.Add(new SupplyViewModel());
            PageViewModels.Add(new StrategyViewModel());
            PageViewModels.Add(new LTBViewModel());
            PageViewModels.Add(new AdministrationViewModel());

            // Set starting page

            CurrentPageViewModel = PageViewModels[0];
        }

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((ViewModelBase)p),
                        p => p is ViewModelBase);
                }

                return _changePageCommand;
            }
        }

        public ObservableCollection<ViewModelBase> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new ObservableCollection<ViewModelBase>();

                return _pageViewModels;
            }
        }

        public ViewModelBase CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        private void ChangeViewModel(ViewModelBase viewModel)
        {
            if (viewModel.GetType().Equals(typeof(LoginViewModel)))
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
