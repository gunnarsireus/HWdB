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
    class LTBViewModel : ViewModelBase
    {
        public override string ButtonName { get; set; }
        public ObservableCollection<LtbDataSet> LtbDataSets
        {
            get { return GetValue(() => LtbDataSets); }
            set { SetValue(() => LtbDataSets, value); InitListBox(); }
        }

        LtbDataSet _currentLtbDataSet;
        public LtbDataSet CurrentLtbDataSet
        {
            get
            {
                return _currentLtbDataSet;
            }
            set
            {
                if (_currentLtbDataSet != value)
                {
                    _currentLtbDataSet = value;
                    OnPropertyChanged("CurrentLtbDataSet");
                    OnPropertyChanged("RepairIsPossible");
                    OnPropertyChanged("RepairNotPossible");
                    OnPropertyChanged("ShowRepairPossible");
                }
            }
        }

        public LtbDataSet SelectedListBoxItem
        {
            get { return GetValue(() => SelectedListBoxItem); }
            set
            {
                SetValue(() => SelectedListBoxItem, value);
                CurrentLtbDataSet = value;
            }
        }

        public ICommand CalculateCommand
        {
            get;
            private set;
        }

        public ICommand ClearCommand
        {
            get;
            private set;
        }

        public ICommand NewLtbDataSetCommand
        {
            get;
            private set;
        }
        public ICommand DeleteCommand
        {
            get;
            private set;
        }
        public bool RepairIsPossible
        {
            get { return CurrentLtbDataSet.RepairPossible; }
            set
            {
                CurrentLtbDataSet.RepairPossible = value;
                OnPropertyChanged("RepairNotPossible");
                OnPropertyChanged("ShowRepairPossible");
            }
        }
        public bool RepairNotPossible
        {
            get { return !CurrentLtbDataSet.RepairPossible; }
            set
            {
                RepairIsPossible = !value;
            }
        }
        public string ShowRepairPossible
        {
            get { return CurrentLtbDataSet.RepairPossible ? "Repair Loss OK?" : "Repair not possible"; }
        }
        public LTBViewModel()
        {
            this.ButtonName = "LTB";
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(ClearResultChartErrors);
            NewLtbDataSetCommand = new RelayCommand(CreateNewCurrentLtbDataSet);
            DeleteCommand = new RelayCommand(Delete);
            InitListBox();
            if (LtbDataSets.Any())
            {
                CurrentLtbDataSet = LtbDataSets[0];
            }
            else
            {
                CreateNewCurrentLtbDataSet(new object());
            }
            CurrentLtbDataSet.InitLabels();

        }

        public void InitListBox()
        {
            LtbDataSet tmp = new LtbDataSet();
            if (CurrentLtbDataSet != null) { tmp.Clone(CurrentLtbDataSet); }
            using (var context = new DataContext())
            {
                if (LtbDataSets == null) LtbDataSets = new ObservableCollection<LtbDataSet>();
                LtbDataSets.Clear();
                {
                    context.LtbDataSets.ToList().ForEach(i => LtbDataSets.Add(i));
                }
            }
            if (CurrentLtbDataSetWasNotNull(tmp))
            {
                CurrentLtbDataSet = tmp;  //Restore old value, if existed
            }
        }

        private static bool CurrentLtbDataSetWasNotNull(LtbDataSet tmp)
        {
            return tmp.ID > 0;
        }

        private void Delete(object parameter)
        {
            if (CurrentLtbDataSet.ID == 0)
            {
                System.Windows.MessageBox.Show("Cannot delete unsaved data");
                return;
            }
            UiServices.SetBusyState();
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;
            using (var context = new DataContext())
            {
                LtbDataSet stored = context.LtbDataSets.FirstOrDefault(a => (a.ID == CurrentLtbDataSet.ID));
                if (stored == null)
                {
                    UserLogs.Instance.UserErrorLog("Error: Could not find LtbDataSet for Customer : " + CurrentLtbDataSet.Customer + " " + CurrentLtbDataSet.Version);
                    return;
                }
                else
                {
                    UserLogs.Instance.UserErrorLog("Deleted LtbDataSet for Customer : " + CurrentLtbDataSet.Customer + " " + CurrentLtbDataSet.Version);
                    context.LtbDataSets.Remove(stored);
                    context.SaveChanges();
                    LtbDataSet firstItem = context.LtbDataSets.FirstOrDefault();
                    if (firstItem == null)
                    {
                        CreateNewCurrentLtbDataSet(new object());
                    }
                    else
                    {
                        CurrentLtbDataSet = firstItem;
                    }
                }
                InitListBox();
            }
        }
        private void Calculate(object parameter)
        {
            if (CurrentLtbDataSet.HasErrors.Count > 0)
            {
                var first = CurrentLtbDataSet.HasErrors.First();
                System.Windows.MessageBox.Show(first.Value);
            }
            else
            {
                UiServices.SetBusyState();
                CurrentLtbDataSet.Calculate();

                //Visa som 3D
                CurrentLtbDataSet.GetChart();
                SaveLtbDataSet(CurrentLtbDataSet);
            }
        }
        private void SaveLtbDataSet(LtbDataSet ltbDataSet)
        {
            using (var context = new DataContext())
            {
                LtbDataSet stored = context.LtbDataSets.FirstOrDefault(a => (a.Customer == ltbDataSet.Customer) && (a.Version == ltbDataSet.Version));
                if (stored == null)
                {
                    UserLogs.Instance.UserErrorLog("Saved new LtbDataSet for Customer : " + ltbDataSet.Customer + " " + ltbDataSet.Version);
                    ltbDataSet.Saved = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                    context.LtbDataSets.Add(ltbDataSet);
                }
                else
                {
                    UserLogs.Instance.UserErrorLog("Updated LtbDataSet for Customer : " + ltbDataSet.Customer + " " + ltbDataSet.Version);
                    ltbDataSet.ID = stored.ID;
                    ltbDataSet.Saved = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                    context.Entry(stored).CurrentValues.SetValues(ltbDataSet);
                    context.Entry(stored).State = System.Data.EntityState.Modified;
                }
                context.SaveChanges();
                InitListBox();
            }
        }
        private void ClearResultChartErrors(object parameter)
        {
            CurrentLtbDataSet.ClearResult();
            CurrentLtbDataSet.ClearChartData();
            CurrentLtbDataSet.GetChart();
        }

        private void CreateNewCurrentLtbDataSet(object parameter)
        {
            CurrentLtbDataSet = new LtbDataSet()
            {
                CreatedBy = LoggedInUser.Instance.UserLoggedin.UserName,
                Customer = "Der Kunde GmbH",
                Version = "pa1",
                Saved = "Press 'Calculate'",
                LTBDate = DateTime.Now.ToString(),
                EOSDate = DateTime.Now.AddDays(3652).ToString(),
                RepairLeadTime = 182,
                RepairPossible = true,
                ConfidenceLevel = "95%",
                IB0 = "50",
                IB1 = "50",
                IB2 = "50",
                IB3 = "50",
                IB4 = "50",
                IB5 = "50",
                IB6 = "50",
                IB7 = "50",
                IB8 = "50",
                IB9 = "50",
                IB1IsEnabled = true,
                IB2IsEnabled = true,
                IB3IsEnabled = true,
                IB4IsEnabled = true,
                IB5IsEnabled = true,
                IB6IsEnabled = true,
                IB7IsEnabled = true,
                IB8IsEnabled = true,
                IB9IsEnabled = true,
                IB10 = "EoS",
                FR0 = "0,1",
                FR1 = "0,1",
                FR2 = "0,1",
                FR3 = "0,1",
                FR4 = "0,1",
                FR5 = "0,1",
                FR6 = "0,1",
                FR7 = "0,1",
                FR8 = "0,1",
                FR9 = "0,1",
                RS0 = "0",
                RS1 = "0",
                RS2 = "0",
                RS3 = "0",
                RS4 = "0",
                RS5 = "0",
                RS6 = "0",
                RS7 = "0",
                RS8 = "0",
                RS9 = "0",
                RL0 = "10",
                RL1 = "10",
                RL2 = "10",
                RL3 = "10",
                RL4 = "10",
                RL5 = "10",
                RL6 = "10",
                RL7 = "10",
                RL8 = "10",
                RL9 = "10",
                RL0IsEnabled = true,
                RL1IsEnabled = true,
                RL2IsEnabled = true,
                RL3IsEnabled = true,
                RL4IsEnabled = true,
                RL5IsEnabled = true,
                RL6IsEnabled = true,
                RL7IsEnabled = true,
                RL8IsEnabled = true,
                RL9IsEnabled = true,
                TotalStock = string.Empty,
                //ServiceDays = 3652,
                Lost = string.Empty,
                Stock = string.Empty,
                Failed = string.Empty,
                Repaired = string.Empty,
                Safety = string.Empty,
                InfoText = "Enter values and press 'Calculate'"
            };
        }
    }
}
