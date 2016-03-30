using CenterSpace.NMath.Core;
using HWdB.CustomValidationAttributes;
using HWdB.MVVMFramework;
using HWdB.Utils;
using LTBCore;
using MHWdB.CustomValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows.Media.Imaging;

namespace HWdB.Model
{
    public class LtbDataSet : PropertyChangedNotification
    {
        public LtbDataSet()
        {
            Customer = "";
            Version = "";
            ClearChart();
        }

        public void ClearChart()
        {
            LtbChart = ConvertToBitmapImage(LtbCommon.GetEmptyChart(900, 15));
        }
        public BitmapImage ConvertToBitmapImage(MemoryStream stream)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            return image;
        }

        bool _loopOk;

        [NotMapped]
        public bool IsSelected
        {
            get { return GetValue(() => IsSelected); }
            set { SetValue(() => IsSelected, value); }
        }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        [MaxLength(100, ErrorMessage = "Customer exceeded 100 letters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Name contains invalid letters")]
        [CustomerUniqueAttribute]
        public string Customer
        {
            get { return GetValue(() => Customer); }
            set
            {
                _loopOk = (value != Customer);
                SetValue(() => Customer, value);
                if (_loopOk)
                {
                    OnPropertyChanged("Version");
                }
            }
        }

        [MaxLength(40, ErrorMessage = "Version exceeded 40 letters")]
        [VersionUniqueAttribute]
        public string Version
        {
            get { return GetValue(() => Version); }
            set
            {
                _loopOk = (value != Version);
                SetValue(() => Version, value);
                if (_loopOk)
                {
                    OnPropertyChanged("Customer");
                }
            }
        }

        public string Saved
        {
            get { return GetValue(() => Saved); }
            set { SetValue(() => Saved, value); }
        }

        [LTBDateWithinRangeAttribute(ErrorMessage = "Not valid LTB date")]
        public string LTBDate
        {
            get { return GetValue(() => LTBDate); }
            set
            {
                _loopOk = (value != LTBDate);
                SetValue(() => LTBDate, value);
                if (_loopOk)
                {
                    OnPropertyChanged("RepairLeadTime");
                    OnPropertyChanged("EOSDate");
                    OnPropertyChanged("ServiceDays");
                }

                Presenter.UpdateInputViewModel(this);
            }
        }

        [EOSDateWithinRangeAttribute(ErrorMessage = "Not valid EOS date")]
        public string EOSDate
        {
            get { return GetValue(() => EOSDate); }
            set
            {
                _loopOk = (value != EOSDate);
                SetValue(() => EOSDate, value);
                if (_loopOk)
                {
                    OnPropertyChanged("RepairLeadTime");
                    OnPropertyChanged("LTBDate");
                    OnPropertyChanged("ServiceDays");
                }
                Presenter.UpdateInputViewModel(this);
            }
        }

        [RepairLeadTimeEOSLTBRangeAttribute(ErrorMessage = "Service period cannot be shorter than Repair Lead Time")]
        [Range(2, 365, ErrorMessage = "Reapir Lead Time must be within 2 and 365")]
        public int RepairLeadTime
        {
            get { return GetValue(() => RepairLeadTime); }
            set
            {
                _loopOk = (value != RepairLeadTime);
                SetValue(() => RepairLeadTime, value);
                if (_loopOk)
                {
                    OnPropertyChanged("EOSDate");
                    OnPropertyChanged("LTBDate");
                }
                Presenter.UpdateInputViewModel(this);
            }
        }
        public string ConfidenceLevel { get; set; }
        public bool RepairPossible
        {
            get { return GetValue(() => RepairPossible); }
            set
            {
                if (RepairPossible != value)
                {
                    SetValue(() => RepairPossible, value);
                    Presenter.UpdateInputViewModel(this);
                }
            }
        }

        public bool MtbfSelected
        {
            get { return GetValue(() => MtbfSelected); }
            set
            {
                ShowMtbfOrFr = value ? "MTBF [0.01 - 100000]:" : "Failure Rate [0.00001 - 100]:";
                if (MtbfSelected != value)
                {
                    SetValue(() => MtbfSelected, value);
                    OnPropertyChanged("MtbfIsSelected");
                }
            }
        }

        [NotMappedAttribute]
        public string ShowMtbfOrFr
        {
            get { return GetValue(() => ShowMtbfOrFr); }
            set { SetValue(() => ShowMtbfOrFr, value); }

        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB0
        {
            get { return GetValue(() => IB0); }
            set { SetValue(() => IB0, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB1
        {
            get { return GetValue(() => IB1); }
            set { SetValue(() => IB1, value); }
        }
        [NotMappedAttribute]
        public bool IB1IsEnabled
        {
            get { return GetValue(() => IB1IsEnabled); }
            set { SetValue(() => IB1IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB2
        {
            get { return GetValue(() => IB2); }
            set { SetValue(() => IB2, value); }
        }
        [NotMappedAttribute]
        public bool IB2IsEnabled
        {
            get { return GetValue(() => IB2IsEnabled); }
            set { SetValue(() => IB2IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB3
        {
            get { return GetValue(() => IB3); }
            set { SetValue(() => IB3, value); }
        }
        [NotMappedAttribute]
        public bool IB3IsEnabled
        {
            get { return GetValue(() => IB3IsEnabled); }
            set { SetValue(() => IB3IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB4
        {
            get { return GetValue(() => IB4); }
            set { SetValue(() => IB4, value); }
        }
        [NotMappedAttribute]
        public bool IB4IsEnabled
        {
            get { return GetValue(() => IB4IsEnabled); }
            set { SetValue(() => IB4IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB5
        {
            get { return GetValue(() => IB5); }
            set { SetValue(() => IB5, value); }
        }
        [NotMappedAttribute]
        public bool IB5IsEnabled
        {
            get { return GetValue(() => IB5IsEnabled); }
            set { SetValue(() => IB5IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB6
        {
            get { return GetValue(() => IB6); }
            set { SetValue(() => IB6, value); }
        }
        [NotMappedAttribute]
        public bool IB6IsEnabled
        {
            get { return GetValue(() => IB6IsEnabled); }
            set { SetValue(() => IB6IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB7
        {
            get { return GetValue(() => IB7); }
            set { SetValue(() => IB7, value); }
        }
        [NotMappedAttribute]
        public bool IB7IsEnabled
        {
            get { return GetValue(() => IB7IsEnabled); }
            set { SetValue(() => IB7IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB8
        {
            get { return GetValue(() => IB8); }
            set { SetValue(() => IB8, value); }
        }
        [NotMappedAttribute]
        public bool IB8IsEnabled
        {
            get { return GetValue(() => IB8IsEnabled); }
            set { SetValue(() => IB8IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Installed Base must be within 0 and 99999")]
        public string IB9
        {
            get { return GetValue(() => IB9); }
            set { SetValue(() => IB9, value); }
        }
        [NotMappedAttribute]
        public bool IB9IsEnabled
        {
            get { return GetValue(() => IB9IsEnabled); }
            set { SetValue(() => IB9IsEnabled, value); }
        }
        public string IB10
        {
            get { return GetValue(() => IB10); }
            set { SetValue(() => IB10, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR0
        {
            get { return GetValue(() => FR0); }
            set { SetValue(() => FR0, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR1
        {
            get { return GetValue(() => FR1); }
            set { SetValue(() => FR1, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR2
        {
            get { return GetValue(() => FR2); }
            set { SetValue(() => FR2, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR3
        {
            get { return GetValue(() => FR3); }
            set { SetValue(() => FR3, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR4
        {
            get { return GetValue(() => FR4); }
            set { SetValue(() => FR4, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR5
        {
            get { return GetValue(() => FR5); }
            set { SetValue(() => FR5, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR6
        {
            get { return GetValue(() => FR6); }
            set { SetValue(() => FR6, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR7
        {
            get { return GetValue(() => FR7); }
            set { SetValue(() => FR7, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR8
        {
            get { return GetValue(() => FR8); }
            set { SetValue(() => FR8, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.][0-9]{0,4}[1-9]|[1-9][0-9][.][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR9
        {
            get { return GetValue(() => FR9); }
            set { SetValue(() => FR9, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS0
        {
            get { return GetValue(() => RS0); }
            set { SetValue(() => RS0, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS1
        {
            get { return GetValue(() => RS1); }
            set { SetValue(() => RS1, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS2
        {
            get { return GetValue(() => RS2); }
            set { SetValue(() => RS2, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS3
        {
            get { return GetValue(() => RS3); }
            set { SetValue(() => RS3, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS4
        {
            get { return GetValue(() => RS4); }
            set { SetValue(() => RS4, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS5
        {
            get { return GetValue(() => RS5); }
            set { SetValue(() => RS5, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS6
        {
            get { return GetValue(() => RS6); }
            set { SetValue(() => RS6, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS7
        {
            get { return GetValue(() => RS7); }
            set { SetValue(() => RS7, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS8
        {
            get { return GetValue(() => RS8); }
            set { SetValue(() => RS8, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 9999")]
        public string RS9
        {
            get { return GetValue(() => RS9); }
            set { SetValue(() => RS9, value); }
        }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Nbr of Regional Stocks must be within 0 and 100")]
        public string RL0
        {
            get { return GetValue(() => RL0); }
            set { SetValue(() => RL0, value); }
        }
        [NotMappedAttribute]
        public bool RL0IsEnabled
        {
            get { return GetValue(() => RL0IsEnabled); }
            set { SetValue(() => RL0IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL1
        {
            get { return GetValue(() => RL1); }
            set { SetValue(() => RL1, value); }
        }
        [NotMappedAttribute]
        public bool RL1IsEnabled
        {
            get { return GetValue(() => RL1IsEnabled); }
            set { SetValue(() => RL1IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL2
        {
            get { return GetValue(() => RL2); }
            set { SetValue(() => RL2, value); }
        }
        [NotMappedAttribute]
        public bool RL2IsEnabled
        {
            get { return GetValue(() => RL2IsEnabled); }
            set { SetValue(() => RL2IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL3
        {
            get { return GetValue(() => RL3); }
            set { SetValue(() => RL3, value); }
        }
        [NotMappedAttribute]
        public bool RL3IsEnabled
        {
            get { return GetValue(() => RL3IsEnabled); }
            set { SetValue(() => RL3IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL4
        {
            get { return GetValue(() => RL4); }
            set { SetValue(() => RL4, value); }
        }
        [NotMappedAttribute]
        public bool RL4IsEnabled
        {
            get { return GetValue(() => RL4IsEnabled); }
            set { SetValue(() => RL4IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL5
        {
            get { return GetValue(() => RL5); }
            set { SetValue(() => RL5, value); }
        }
        [NotMappedAttribute]
        public bool RL5IsEnabled
        {
            get { return GetValue(() => RL5IsEnabled); }
            set { SetValue(() => RL5IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL6
        {
            get { return GetValue(() => RL6); }
            set { SetValue(() => RL6, value); }
        }
        [NotMappedAttribute]
        public bool RL6IsEnabled
        {
            get { return GetValue(() => RL6IsEnabled); }
            set { SetValue(() => RL6IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL7
        {
            get { return GetValue(() => RL7); }
            set { SetValue(() => RL7, value); }
        }
        [NotMappedAttribute]
        public bool RL7IsEnabled
        {
            get { return GetValue(() => RL7IsEnabled); }
            set { SetValue(() => RL7IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL8
        {
            get { return GetValue(() => RL8); }
            set { SetValue(() => RL8, value); }
        }
        [NotMappedAttribute]
        public bool RL8IsEnabled
        {
            get { return GetValue(() => RL8IsEnabled); }
            set { SetValue(() => RL8IsEnabled, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Repair Loss must be within 0 and 100")]
        public string RL9
        {
            get { return GetValue(() => RL9); }
            set { SetValue(() => RL9, value); }
        }
        [NotMappedAttribute]
        public bool RL9IsEnabled
        {
            get { return GetValue(() => RL9IsEnabled); }
            set { SetValue(() => RL9IsEnabled, value); }
        }

        public int ServiceDays
        {
            get { return Convert.ToInt32(DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, Convert.ToDateTime(LTBDate), Convert.ToDateTime(EOSDate))); }
            set { SetValue(() => ServiceDays, value); }

        }
        public string TotalStock
        {
            get { return GetValue(() => TotalStock); }
            set { SetValue(() => TotalStock, value); }
        }
        public string Stock
        {
            get { return GetValue(() => Stock); }
            set { SetValue(() => Stock, value); }
        }
        public string Safety
        {
            get { return GetValue(() => Safety); }
            set { SetValue(() => Safety, value); }
        }
        public string Failed
        {
            get { return GetValue(() => Failed); }
            set { SetValue(() => Failed, value); }
        }
        public string Repaired
        {
            get { return GetValue(() => Repaired); }
            set { SetValue(() => Repaired, value); }
        }
        public string Lost
        {
            get { return GetValue(() => Lost); }
            set { SetValue(() => Lost, value); }
        }
        [NotMappedAttribute]
        public string InfoText
        {
            get { return GetValue(() => InfoText); }
            set { SetValue(() => InfoText, value); }
        }
        [NotMappedAttribute]
        public long[] StockYearArray = new long[LtbCommon.MaxYear + 1];
        [NotMappedAttribute]
        public long[] RSYearArray = new long[LtbCommon.MaxYear + 1];
        [NotMappedAttribute]
        public long[] SafetyYearArray = new long[LtbCommon.MaxYear + 1];

        [NotMappedAttribute]
        public BitmapImage LtbChart
        {
            get { return GetValue(() => LtbChart); }
            set { SetValue(() => LtbChart, value); }
        }

        [NotMappedAttribute]
        public string YearLabel0
        {
            get { return GetValue(() => YearLabel0); }
            set { SetValue(() => YearLabel0, value); }
        }
        [NotMappedAttribute]
        public string YearLabel1
        {
            get { return GetValue(() => YearLabel1); }
            set { SetValue(() => YearLabel1, value); }
        }
        [NotMappedAttribute]
        public string YearLabel2
        {
            get { return GetValue(() => YearLabel2); }
            set { SetValue(() => YearLabel2, value); }
        }
        [NotMappedAttribute]
        public string YearLabel3
        {
            get { return GetValue(() => YearLabel3); }
            set { SetValue(() => YearLabel3, value); }
        }
        [NotMappedAttribute]
        public string YearLabel4
        {
            get { return GetValue(() => YearLabel4); }
            set { SetValue(() => YearLabel4, value); }
        }
        [NotMappedAttribute]
        public string YearLabel5
        {
            get { return GetValue(() => YearLabel5); }
            set { SetValue(() => YearLabel5, value); }
        }
        [NotMappedAttribute]
        public string YearLabel6
        {
            get { return GetValue(() => YearLabel6); }
            set { SetValue(() => YearLabel6, value); }
        }
        [NotMappedAttribute]
        public string YearLabel7
        {
            get { return GetValue(() => YearLabel7); }
            set { SetValue(() => YearLabel7, value); }
        }
        [NotMappedAttribute]
        public string YearLabel8
        {
            get { return GetValue(() => YearLabel8); }
            set { SetValue(() => YearLabel8, value); }
        }
        [NotMappedAttribute]
        public string YearLabel9
        {
            get { return GetValue(() => YearLabel9); }
            set { SetValue(() => YearLabel9, value); }
        }

        public void Clone(LtbDataSet that)
        {
            Customer = that.Customer;
            //Id = that.Id;
            CreatedBy = that.CreatedBy;
            Version = that.Version;
            Saved = that.Saved;
            LTBDate = that.LTBDate;
            EOSDate = that.EOSDate;
            RepairLeadTime = that.RepairLeadTime;
            ConfidenceLevel = that.ConfidenceLevel;
            RepairPossible = that.RepairPossible;
            MtbfSelected = that.MtbfSelected;

            IB0 = that.IB0;
            IB1 = that.IB1;
            IB1IsEnabled = that.IB1IsEnabled;
            IB2 = that.IB2;
            IB2IsEnabled = that.IB2IsEnabled;
            IB3 = that.IB3;
            IB3IsEnabled = that.IB3IsEnabled;
            IB4 = that.IB4;
            IB4IsEnabled = that.IB4IsEnabled;
            IB5 = that.IB5;
            IB5IsEnabled = that.IB5IsEnabled;
            IB6 = that.IB6;
            IB6IsEnabled = that.IB6IsEnabled;
            IB7 = that.IB7;
            IB7IsEnabled = that.IB7IsEnabled;
            IB8 = that.IB8;
            IB8IsEnabled = that.IB8IsEnabled;
            IB9 = that.IB9;
            IB9IsEnabled = that.IB9IsEnabled;
            IB10 = that.IB10;

            FR0 = that.FR0;
            FR1 = that.FR1;
            FR2 = that.FR2;
            FR3 = that.FR3;
            FR4 = that.FR4;
            FR5 = that.FR5;
            FR6 = that.FR6;
            FR7 = that.FR7;
            FR8 = that.FR8;
            FR9 = that.FR9;
            RL0 = that.RL0;

            RS0 = that.RS0;
            RS1 = that.RS1;
            RS2 = that.RS2;
            RS3 = that.RS3;
            RS4 = that.RS4;
            RS5 = that.RS5;
            RS6 = that.RS6;
            RS7 = that.RS7;
            RS8 = that.RS8;
            RS9 = that.RS9;

            RL0 = that.RL0;
            RL0IsEnabled = that.RL0IsEnabled;
            RL1 = that.RL0;
            RL1IsEnabled = that.RL1IsEnabled;
            RL2 = that.RL2;
            RL2IsEnabled = that.RL2IsEnabled;
            RL3 = that.RL3;
            RL3IsEnabled = that.RL3IsEnabled;
            RL4 = that.RL4;
            RL4IsEnabled = that.RL4IsEnabled;
            RL5 = that.RL5;
            RL5IsEnabled = that.RL5IsEnabled;
            RL6 = that.RL6;
            RL6IsEnabled = that.RL6IsEnabled;
            RL7 = that.RL7;
            RL7IsEnabled = that.RL7IsEnabled;
            RL8 = that.RL8;
            RL8IsEnabled = that.RL8IsEnabled;
            RL9 = that.RL9;
            RL9IsEnabled = that.RL9IsEnabled;

            TotalStock = that.TotalStock;
            Stock = that.Stock;
            Safety = that.Safety;
            Failed = that.Failed;
            Repaired = that.Repaired;
            Lost = that.Lost;
            InfoText = that.InfoText;
            StockYearArray = that.StockYearArray;
            RSYearArray = that.RSYearArray;
            SafetyYearArray = that.SafetyYearArray;
            LtbChart = that.LtbChart;

            YearLabel0 = that.YearLabel0;
            YearLabel1 = that.YearLabel1;
            YearLabel2 = that.YearLabel2;
            YearLabel3 = that.YearLabel3;
            YearLabel4 = that.YearLabel4;
            YearLabel5 = that.YearLabel5;
            YearLabel6 = that.YearLabel6;
            YearLabel7 = that.YearLabel7;
            YearLabel8 = that.YearLabel8;
            YearLabel9 = that.YearLabel9;
        }
    }
}
