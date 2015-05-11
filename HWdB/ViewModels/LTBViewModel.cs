using HWdB.Model;
using HWdB.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HWdB.ViewModels
{
    class LTBViewModel : ViewModelBase
    {
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

        private void Calculate(object parameter)
        {
            LtbCalculation.Calculate(CurrentLtbDataSet);

            //Visa som 3D
            CurrentLtbDataSet.LtbChart = GetChart(CurrentLtbDataSet);
        }
        private void Clear(object parameter)
        {
            LtbCalculation.ClearResult(CurrentLtbDataSet);
            LtbCalculation.ClearChartData(CurrentLtbDataSet);
            CurrentLtbDataSet.LtbChart = GetChart(CurrentLtbDataSet);
        }

        private void Init(object parameter)
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
                TotalStock = "TotalStock",
                Lost = "Lost",
                Stock = "Stock",
                Failed = "Failed",
                Repaired = "Repaired",
                Safety = "Safety",
                InfoText = "InfoText"
            };
            LtbCalculation.ClearChartData(CurrentLtbDataSet);
            CurrentLtbDataSet.LtbChart = GetChart(CurrentLtbDataSet);
        }

        public override string ButtonName { get; set; }
        //public string ConfidenceLevel { get; set; }
        //public string RepairLeadTime { get; set; }
        //[RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        //public string IB0 { get; set; }
        //public string IB1 { get; set; }
        //public string IB2 { get; set; }
        //public string IB3 { get; set; }
        //public string IB4 { get; set; }
        //public string IB5 { get; set; }
        //public string IB6 { get; set; }
        //public string IB7 { get; set; }
        //public string IB8 { get; set; }
        //public string IB9 { get; set; }

        //string ltbDate = DateTime.Now.ToString();
        //public string LTBDate
        //{
        //    get { return this.ltbDate; }
        //    set
        //    {
        //        if (this.ltbDate == value)
        //            return;

        //        this.ltbDate = value;
        //        OnPropertyChanged("LTBDate");
        //    }
        //}

        //string eosDate = DateTime.Now.AddYears(10).ToString();
        //public string EOSDate
        //{
        //    get { return this.eosDate; }
        //    set
        //    {
        //        if (this.eosDate == value)
        //            return;

        //        this.eosDate = value;
        //        OnPropertyChanged("EOSDate");
        //    }
        //}
        public LTBViewModel()
        {
            Init(new object());
            this.ButtonName = "LTB";
            isYes = CurrentLtbDataSet.RepairPossible;
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(Clear);
            InitCommand = new RelayCommand(Init);
        }

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

        bool isYes;

        public bool IsYes
        {
            get { return this.isYes; }
            set
            {
                if (this.isYes == value)
                    return;

                this.isYes = value;
                CurrentLtbDataSet.RepairPossible = value;
                OnPropertyChanged("IsYes");
                OnPropertyChanged("IsNo");
                OnPropertyChanged("RepairPossible");
            }
        }

        public bool IsNo
        {
            get { return !IsYes; }
            set { IsYes = !value; }
        }

        public string RepairPossible
        {
            get { return this.IsYes ? "Repair Possible Yes!!" : "Repair Possible No!!"; }
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
