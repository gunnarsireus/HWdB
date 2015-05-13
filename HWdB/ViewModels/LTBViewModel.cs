using HWdB.DataAccess;
using HWdB.Model;
using HWdB.Utils;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HWdB.ViewModels
{
    class LTBViewModel : ViewModelBase
    {
        public ObservableCollection<LtbDataSet> LtbDataSets
        {
            get { return GetValue(() => LtbDataSets); }
            set { SetValue(() => LtbDataSets, value); InitListBox(); }
        }

        public LTBViewModel()
        {
            CreateNewCurrentLtbDataSet(new object());
            this.ButtonName = "LTB";
            LtbCalculation.InitLabels(CurrentLtbDataSet);
            repairIsPossible = CurrentLtbDataSet.RepairPossible;
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(Clear);
            InitCommand = new RelayCommand(CreateNewCurrentLtbDataSet);
            DeleteCommand = new RelayCommand(Delete);
            InitListBox();
        }

        private void InitListBox()
        {
            using (var context = new DataContext())
            {
                if (LtbDataSets == null) LtbDataSets = new ObservableCollection<LtbDataSet>();
                LtbDataSets.Clear();
                {
                    context.LtbDataSets.ToList().ForEach(i => LtbDataSets.Add(i));
                }
            }
        }

        public LtbDataSet SelectedListBoxItem
        {
            get { return GetValue(() => SelectedListBoxItem); }
            set
            {
                SetValue(() => SelectedListBoxItem, value);
                CurrentLtbDataSet = SelectedListBoxItem;
            }
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
                }
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

        public ICommand InitCommand
        {
            get;
            private set;
        }
        public ICommand DeleteCommand
        {
            get;
            private set;
        }

        private void Delete(object parameter)
        {
            if (CurrentLtbDataSet.ID == 0)
            {
                CurrentLtbDataSet.InfoText = "Cannot delete not saved data!";
            }
            using (var context = new DataContext())
            {
                LtbDataSet stored = context.LtbDataSets.Where(a => (a.ID == CurrentLtbDataSet.ID)).FirstOrDefault();
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
            CleanupDateErrors();
            if (CurrentLtbDataSet.HasErrors.Count > 0)
            {
                var first = CurrentLtbDataSet.HasErrors.First();
                CurrentLtbDataSet.InfoText = first.Value;
            }
            else
            {
                UiServices.SetBusyState();
                LtbCalculation.Calculate(CurrentLtbDataSet);

                //Visa som 3D
                CurrentLtbDataSet.LtbChart = GetChart(CurrentLtbDataSet);
                SaveLtbDataSet(CurrentLtbDataSet);
            }
        }

        private void CleanupDateErrors()
        {
            CurrentLtbDataSet.EOSDate = CurrentLtbDataSet.EOSDate;
            CurrentLtbDataSet.LTBDate = CurrentLtbDataSet.LTBDate;
            CurrentLtbDataSet.RepairLeadTime = CurrentLtbDataSet.RepairLeadTime;
        }

        private void SaveLtbDataSet(LtbDataSet ltbDataSet)
        {
            using (var context = new DataContext())
            {
                LtbDataSet stored = context.LtbDataSets.Where(a => (a.Customer == ltbDataSet.Customer) && (a.Version == ltbDataSet.Version)).FirstOrDefault();
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
        private void Clear(object parameter)
        {
            CleanupDateErrors();
            LtbCalculation.ClearResult(CurrentLtbDataSet);
            LtbCalculation.ClearChartData(CurrentLtbDataSet);
            CurrentLtbDataSet.LtbChart = GetChart(CurrentLtbDataSet);
        }

        private void CreateNewCurrentLtbDataSet(object parameter)
        {
            CurrentLtbDataSet = new LtbDataSet()
            {
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
                ServiceDays = "3652",
                Lost = string.Empty,
                Stock = string.Empty,
                Failed = string.Empty,
                Repaired = string.Empty,
                Safety = string.Empty,
                InfoText = "Enter values and press 'Calculate'"
            };
            RepairIsPossible = true;
            LtbCalculation.ClearChartData(CurrentLtbDataSet);
            CurrentLtbDataSet.LtbChart = GetChart(CurrentLtbDataSet);
        }

        public override string ButtonName { get; set; }

        public BitmapImage GetChart(LtbDataSet ltb)
        {
            Chart chart = new Chart()
            {
                Height = 300,
                Width = 900,
                ImageType = ChartImageType.Png
            };
            ChartArea chartArea = chart.ChartAreas.Add("Stock");
            chartArea.Area3DStyle.Enable3D = true;

            Series RS = chart.Series.Add("0");
            Series Stock = chart.Series.Add("1");
            Series Safety = chart.Series.Add("2");
            RS.ChartType = SeriesChartType.StackedColumn;
            Stock.ChartType = SeriesChartType.StackedColumn;
            Safety.ChartType = SeriesChartType.StackedColumn;

            chart.Series["0"].Points.DataBindXY(xValues, ltb.RSYearArray);
            chart.Series["0"].Color = Color.Green;
            chart.Series["1"].Points.DataBindXY(xValues, ltb.StockYearArray);
            chart.Series["1"].Color = Color.Blue;
            chart.Series["2"].Points.DataBindXY(xValues, ltb.SafetyYearArray);
            chart.Series["2"].Color = Color.Red;
            MemoryStream ms = new MemoryStream();

            chart.SaveImage(ms);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            return image;

        }

        bool repairIsPossible;

        public bool RepairIsPossible
        {
            get { return this.repairIsPossible; }
            set
            {
                if (this.repairIsPossible == value)
                    return;
                CurrentLtbDataSet.RepairPossible = value;
                this.repairIsPossible = value;
                CurrentLtbDataSet.RepairPossible = value;
                OnPropertyChanged("RepairIsPossible");
                OnPropertyChanged("RepairNotPossible");
                OnPropertyChanged("RepairPossible");
            }
        }

        public bool RepairNotPossible
        {
            get { return !RepairIsPossible; }
            set { RepairIsPossible = !value; }
        }

        public string RepairPossible
        {
            get { return this.RepairIsPossible ? "Repair possible, please set Repair Loss" : "Repair not possible, Repair Loss = 100"; }
        }
        static string[] xValues = {
		"LTB",
		"+1Year",
		"+2Year",
		"+3Year",
		"+4Year",
		"+5Year",
		"+6Year",
		"+7Year",
		"+8Year",
		"+9Year",
		"EoS"
	};

    }
}
