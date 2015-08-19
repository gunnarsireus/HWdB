using HWdB.DataAccess;
using HWdB.Model;
using HWdB.MVVMFramework;
using HWdB.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HWdB.ViewModels
{
    class LtbViewModel : BaseViewModel
    {
        public override sealed string Title { get; set; }
        public ObservableCollection<LtbDataSet> LtbDataSetsObs
        {
            get { return GetValue(() => LtbDataSetsObs); }
            set { SetValue(() => LtbDataSetsObs, value); }
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
                if (value == null) return;
                if (_currentLtbDataSet != null)
                {
                    _currentLtbDataSet.IsSelected = false;
                }
                _currentLtbDataSet = value;
                _currentLtbDataSet.IsSelected = true;
                OnPropertyChanged("CurrentLtbDataSet");
                OnPropertyChanged("RepairIsPossible");
                OnPropertyChanged("RepairNotPossible");
                OnPropertyChanged("ShowRepairPossible");
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
        public LtbViewModel()
        {
            Title = "LTB";
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(ClearResultChartErrors);
            NewLtbDataSetCommand = new RelayCommand(CreateNewCurrentLtbDataSet);
            DeleteCommand = new RelayCommand(Delete);
            NextCommand = new RelayCommand(Next);
            PreviousCommand = new RelayCommand(Previous);
            InitListBox(new LtbDataSet());
            if (LtbDataSetsObs.Any())
            {
                SelectedListBoxItem = LtbDataSetsObs[0];
                SelectedIndex = 0;
            }
            else
            {
                CreateNewCurrentLtbDataSet(new object());
            }
            Presenter.InitLabels(CurrentLtbDataSet);
        }
        public void InitListBox(LtbDataSet oldSet)
        {
            using (var context = new DataContext())
            {
                if (LtbDataSetsObs == null) LtbDataSetsObs = new ObservableCollection<LtbDataSet>();
                var tmpList = new ObservableCollection<LtbDataSet>();
                foreach (var item in LtbDataSetsObs)
                {
                    tmpList.Add(item);
                }
                LtbDataSetsObs.Clear();
                context.LtbDataSets.OrderBy(l => l.Customer).ThenBy(l => l.Version).ToList().ForEach(i => LtbDataSetsObs.Add(i));
                foreach (var tmpItem in tmpList)
                {
                    foreach (var item in LtbDataSetsObs)
                    {
                        if (item.Customer == oldSet.Customer && item.Version == oldSet.Version)
                            if (item.Id == tmpItem.Id)
                            {
                                item.LtbChart = tmpItem.LtbChart;
                            }
                    }
                }
            }
            SelectedIndex = 0;
            foreach (var item in LtbDataSetsObs)
            {
                if (item.Customer == oldSet.Customer && item.Version == oldSet.Version)
                {
                    SelectedListBoxItem = LtbDataSetsObs[SelectedIndex];
                    break;
                }
                SelectedIndex = (SelectedIndex + 1) % LtbDataSetsObs.Count;
            }
        }

        private void Next(object parameter)
        {
            if (LtbDataSetsObs.Count <= 1) return;
            SelectedIndex = (SelectedIndex + 1) % LtbDataSetsObs.Count;
            SelectedListBoxItem = LtbDataSetsObs[SelectedIndex];
        }
        private void Previous(object parameter)
        {
            if (LtbDataSetsObs.Count <= 1) return;
            SelectedIndex = (SelectedIndex - 1);
            if (SelectedIndex < 0)
            {
                SelectedIndex = LtbDataSetsObs.Count - 1;
            }
            SelectedListBoxItem = LtbDataSetsObs[SelectedIndex];
        }

        private void Delete(object parameter)
        {
            if (CurrentLtbDataSet.Id == 0)
            {
                MessageBox.Show("Cannot delete unsaved data");
                return;
            }
            UiServices.SetBusyState();
            var messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;
            using (var context = new DataContext())
            {
                var oldLtbDataSet = context.LtbDataSets.FirstOrDefault(a => (a.Id == CurrentLtbDataSet.Id));
                if (NotStoredInDb(oldLtbDataSet))
                {
                    UserLogs.Instance.UserErrorLog("Error: Could not find LtbDataSet for Customer : " + CurrentLtbDataSet.Customer + " " + CurrentLtbDataSet.Version);
                    return;
                }

                UserLogs.Instance.UserErrorLog("Deleted LtbDataSet for Customer : " + CurrentLtbDataSet.Customer + " " + CurrentLtbDataSet.Version);
                context.LtbDataSets.Remove(oldLtbDataSet);
                context.SaveChanges();

                if (!context.LtbDataSets.Any())
                {
                    CreateNewCurrentLtbDataSet(new object());
                    LtbDataSetsObs.Clear();
                    return;
                }
                LtbDataSetsObs.Remove(CurrentLtbDataSet);
                SelectedListBoxItem = LtbDataSetsObs[0];
                SelectedIndex = 0;
            }
        }
        private void Calculate(object parameter)
        {
            if (CurrentLtbDataSet.HasErrors.Count > 0)
            {
                var first = CurrentLtbDataSet.HasErrors.First();
                MessageBox.Show(first.Value);
            }
            else
            {
                UiServices.SetBusyState();
                Calculator.CalculateLtb(CurrentLtbDataSet);

                //Visa som 3D
                CurrentLtbDataSet.LtbChart= Presenter.GetChart(CurrentLtbDataSet);
                SaveLtbDataSet(CurrentLtbDataSet);
            }
        }
        private void SaveLtbDataSet(LtbDataSet ltbDataSet)
        {
            using (var context = new DataContext())
            {
                var oldLtbDataSet = context.LtbDataSets.FirstOrDefault(a => (a.Customer == ltbDataSet.Customer) && (a.Version == ltbDataSet.Version));
                if (NotStoredInDb(oldLtbDataSet))
                {
                    UserLogs.Instance.UserErrorLog("Saved new LtbDataSet for Customer : " + ltbDataSet.Customer + " " + ltbDataSet.Version);
                    ltbDataSet.Saved = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                    if (ltbDataSet.Id == 0)
                    {
                        context.LtbDataSets.Add(ltbDataSet);
                        context.SaveChanges();
                        LtbDataSetsObs.Add(ltbDataSet);
                    }
                    else
                    {
                        var tmpLtbDataSet = new LtbDataSet();
                        tmpLtbDataSet.Clone(ltbDataSet);
                        tmpLtbDataSet.LtbChart = ltbDataSet.LtbChart;
                        tmpLtbDataSet.CreatedBy = LoggedInUser.Instance.UserLoggedin.UserName;
                        context.LtbDataSets.Add(tmpLtbDataSet);
                        context.SaveChanges();
                        LtbDataSetsObs.Add(tmpLtbDataSet);
                        InitListBox(ltbDataSet);
                    }
                    return;
                }
                UserLogs.Instance.UserErrorLog("Updated LtbDataSet for Customer : " + ltbDataSet.Customer + " " + ltbDataSet.Version);
                //ltbDataSet.Id = oldLtbDataSet.Id;
                ltbDataSet.Saved = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                context.Entry(oldLtbDataSet).CurrentValues.SetValues(ltbDataSet);
                context.Entry(oldLtbDataSet).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static bool NotStoredInDb(LtbDataSet stored)
        {
            return stored == null;
        }

        private void ClearResultChartErrors(object parameter)
        {
            Presenter.ClearResult(CurrentLtbDataSet);
            Presenter.ClearChartData(CurrentLtbDataSet);
            CurrentLtbDataSet.LtbChart= Presenter.GetChart(CurrentLtbDataSet);
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
