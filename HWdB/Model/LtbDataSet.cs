using CenterSpace.NMath.Core;
using CenterSpace.NMath.Stats;
using HWdB.CustomValidationAttributes;
using HWdB.MVVMFramework;
using HWdB.Utils;
using LTBCore;
using MHWdB.CustomValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media.Imaging;
namespace HWdB.Model
{

    public class LtbDataSet : PropertyChangedNotification
    {
        bool _loopOk;
        public LtbDataSet()
        {
            Customer = "";
            Version = "";
            StockYearArray = new long[LTBCommon.MaxYear + 1];

            RSYearArray = new long[LTBCommon.MaxYear + 1];

            SafetyYearArray = new long[LTBCommon.MaxYear + 1];
            Presenter.ClearChartData(this);
            Presenter.GetChart(this);
        }
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
                if (_loopOk) Version = Version;
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
                if (_loopOk) Customer = Customer;
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

                UpdateInputViewModel();
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
                UpdateInputViewModel();
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
                    EOSDate = EOSDate;
                    LTBDate = LTBDate;
                }
                UpdateInputViewModel();
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
                    UpdateInputViewModel();
                }
            }
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
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR0
        {
            get { return GetValue(() => FR0); }
            set { SetValue(() => FR0, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR1
        {
            get { return GetValue(() => FR1); }
            set { SetValue(() => FR1, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR2
        {
            get { return GetValue(() => FR2); }
            set { SetValue(() => FR2, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR3
        {
            get { return GetValue(() => FR3); }
            set { SetValue(() => FR3, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR4
        {
            get { return GetValue(() => FR4); }
            set { SetValue(() => FR4, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR5
        {
            get { return GetValue(() => FR5); }
            set { SetValue(() => FR5, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR6
        {
            get { return GetValue(() => FR6); }
            set { SetValue(() => FR6, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR7
        {
            get { return GetValue(() => FR7); }
            set { SetValue(() => FR7, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
        public string FR8
        {
            get { return GetValue(() => FR8); }
            set { SetValue(() => FR8, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Failure Rate must be within 0.00001 and 100")]
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
        public long[] StockYearArray = new long[LTBCommon.MaxYear + 1];
        [NotMappedAttribute]
        public long[] RSYearArray = new long[LTBCommon.MaxYear + 1];
        [NotMappedAttribute]
        public long[] SafetyYearArray = new long[LTBCommon.MaxYear + 1];

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

        static string GetCLFromAverage(double CL, double Average)
        {
            string functionReturnValue = null;
            if (Average <= 0)
            {
                functionReturnValue = " (100%)";
                return functionReturnValue;
            }
            if (Average < 2492000)
            {
                PoissonDistribution Poisson = new PoissonDistribution(Average);
                double tmp = 0;
                NMathConfiguration.Init();
                tmp = Poisson.CDF(Math.Round(Average, 0));
                //tmp = 0.75
                if (tmp > CL)
                {
                    functionReturnValue = " (" + (100 * Math.Round(tmp, 2)).ToString() + "%)";
                }
                else
                {
                    functionReturnValue = string.Empty;
                }
            }
            else
            {
                functionReturnValue = string.Empty;
            }
            return functionReturnValue;
        }

        static string GetCLFromStock(double Stock, double Safety)
        {
            string functionReturnValue = null;
            if (Stock == 0)
            {
                functionReturnValue = " (100%)";
                return functionReturnValue;
            }
            if (Stock < 2492000)
            {
                var poisson = new PoissonDistribution(Stock);
                double tmp = 0;
                NMathConfiguration.Init();
                tmp = poisson.CDF(Math.Round(Stock + Safety, 0));
                functionReturnValue = " (" + (100 * Math.Round(tmp, 4)).ToString() + "%)";
            }
            else
            {
                functionReturnValue = string.Empty;
            }
            return functionReturnValue;
        }

        static long GetSafetyFromAverage(double CL, double Average)
        {
            long functionReturnValue = 0;
            if (Average <= 0)
            {
                functionReturnValue = 0;
                return functionReturnValue;
            }

            if (Average < 2492000)
            {
                var poisson = new PoissonDistribution(Average);
                long K = 0;
                K = Convert.ToInt64(Math.Round(Average, 0));

                while (poisson.CDF(K) < CL)
                {
                    K = K + 1;
                }

                functionReturnValue = Convert.ToInt64(Math.Round(K - Average, 0));
                if (functionReturnValue < 0)
                    functionReturnValue = 0;
            }
            else
            {
                functionReturnValue = Mathematics.RoundLong(Mathematics.NormSInv(CL) * Mathematics.Sqr(Average), 0);
            }
            return functionReturnValue;
        }

        static long GetSafetyFromGamma(double CL, double FromAverage, double Returned)
        {
            long ReturnValue;

            ReturnValue = (long)(Mathematics.calcreserve((int)Mathematics.RoundLong(Returned, 0), 1, CL) - FromAverage);
            if (ReturnValue < 0) ReturnValue = 0;
            if (ReturnValue > FromAverage) ReturnValue = (long)FromAverage;

            return ReturnValue;
        }


        void ConvertFromViewModel(double ServiceDays, int FinalYear, double LeadDays, out double ConfidenceLevelFromNormsInv)
        {
            int Cnt = 0;
            ConfidenceLevelFromNormsInv = 0.0;

            switch (ConfidenceLevel)
            {
                //Confidence Level

                case "60%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfL(0.6);
                    ConfidenceLevelDbl = 0.6;

                    break;
                case "70%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfL(0.7);
                    ConfidenceLevelDbl = 0.7;

                    break;
                case "80%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfL(0.8);
                    ConfidenceLevelDbl = 0.8;

                    break;
                case "90%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfL(0.9);
                    ConfidenceLevelDbl = 0.9;

                    break;
                case "95%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfL(0.95);
                    ConfidenceLevelDbl = 0.95;

                    break;
                case "99,5%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfL(0.995);
                    ConfidenceLevelDbl = 0.995;

                    break;

            }

            Cnt = 0;

            while (Cnt <= FinalYear)
            {
                switch (Cnt)
                {
                    case 0:
                        IBin[0] = Convert.ToInt64(IB0);
                        RSin[0] = Convert.ToInt64(RS0);
                        FRin[0] = Convert.ToDouble(FR0);
                        RLin[0] = Convert.ToDouble(Convert.ToDouble(RL0) / 100);

                        break;
                    case 1:
                        IBin[1] = Convert.ToInt64(IB1);
                        RSin[1] = Convert.ToInt64(RS1);
                        FRin[1] = Convert.ToDouble(FR1);
                        RLin[1] = Convert.ToDouble(Convert.ToDouble(RL1) / 100);

                        break;
                    case 2:
                        IBin[2] = Convert.ToInt64(IB2);
                        RSin[2] = Convert.ToInt64(RS2);
                        FRin[2] = Convert.ToDouble(FR2);
                        RLin[2] = Convert.ToDouble(Convert.ToDouble(RL2) / 100);

                        break;
                    case 3:
                        IBin[3] = Convert.ToInt64(IB3);
                        RSin[3] = Convert.ToInt64(RS3);
                        FRin[3] = Convert.ToDouble(FR3);
                        RLin[3] = Convert.ToDouble(Convert.ToDouble(RL3) / 100);

                        break;
                    case 4:
                        IBin[4] = Convert.ToInt64(IB4.ToString());
                        RSin[4] = Convert.ToInt64(RS4);
                        FRin[4] = Convert.ToDouble(FR4);
                        RLin[4] = Convert.ToDouble(Convert.ToDouble(RL4) / 100);

                        break;
                    case 5:
                        IBin[5] = Convert.ToInt64(IB5);
                        RSin[5] = Convert.ToInt64(RS5);
                        FRin[5] = Convert.ToDouble(FR5);
                        RLin[5] = Convert.ToDouble(Convert.ToDouble(RL5) / 100);

                        break;
                    case 6:
                        IBin[6] = Convert.ToInt64(IB6.ToString());
                        RSin[6] = Convert.ToInt64(RS6.ToString());
                        FRin[6] = Convert.ToDouble(FR6);
                        RLin[6] = Convert.ToDouble(Convert.ToDouble(RL6) / 100);

                        break;
                    case 7:
                        IBin[7] = Convert.ToInt64(IB7);
                        RSin[7] = Convert.ToInt64(RS7);
                        FRin[7] = Convert.ToDouble(FR7);
                        RLin[7] = Convert.ToDouble(Convert.ToDouble(RL7) / 100);

                        break;
                    case 8:
                        IBin[8] = Convert.ToInt64(IB8);
                        RSin[8] = Convert.ToInt64(RS8);
                        FRin[8] = Convert.ToDouble(FR8);
                        RLin[8] = Convert.ToDouble(Convert.ToDouble(RL8) / 100);

                        break;
                    case 9:
                        IBin[9] = Convert.ToInt64(IB9);
                        RSin[9] = Convert.ToInt64(RS9);
                        FRin[9] = Convert.ToDouble(FR9);
                        RLin[9] = Convert.ToDouble(Convert.ToDouble(RL9) / 100);

                        break;
                }
                Cnt += 1;
            }
            ClearRemains(Cnt);

        }

        void ClearRemains(int First)
        {
            int Cnt = First;
            while (Cnt <= LTBCommon.MaxYear)
            {
                switch (Cnt)
                {
                    case 0:
                        IB0 = string.Empty;
                        RS0 = string.Empty;
                        FR0 = string.Empty;
                        RL0 = string.Empty;
                        IB1 = string.Empty;
                        RS1 = string.Empty;
                        FR1 = string.Empty;
                        RL1 = string.Empty;
                        break;
                    case 1:
                        IB2 = string.Empty;
                        RS2 = string.Empty;
                        FR2 = string.Empty;
                        RL2 = string.Empty;
                        break;
                    case 2:
                        IB3 = string.Empty;
                        RS3 = string.Empty;
                        FR3 = string.Empty;
                        RL3 = string.Empty;
                        break;
                    case 3:
                        IB4 = string.Empty;
                        RS4 = string.Empty;
                        FR4 = string.Empty;
                        RL4 = string.Empty;
                        break;
                    case 4:
                        IB5 = string.Empty;
                        RS5 = string.Empty;
                        FR5 = string.Empty;
                        RL5 = string.Empty;
                        break;
                    case 5:
                        IB6 = string.Empty;
                        RS6 = string.Empty;
                        FR6 = string.Empty;
                        RL6 = string.Empty;
                        break;
                    case 6:
                        IB7 = string.Empty;
                        RS7 = string.Empty;
                        FR7 = string.Empty;
                        RL7 = string.Empty;
                        break;
                    case 7:
                        IB8 = string.Empty;
                        RS8 = string.Empty;
                        FR8 = string.Empty;
                        RL8 = string.Empty;
                        break;
                    case 8:
                        IB9 = string.Empty;
                        RS9 = string.Empty;
                        FR9 = string.Empty;
                        RL9 = string.Empty;
                        break;
                    case 9:
                        IB10 = string.Empty;
                        break;
                    case 10:
                        break;
                }
                Cnt += 1;
            }
        }

        static long FromGamma;
        static long FromAverage;
        //const long MaxYear = 10;
        const long MinRepairLeadTime = 2;
        const long MaxRepairLeadTime = 365;
        const long MaxServiceDays = LTBCommon.MaxYear * 365 + 2;
        const long MaxDayArr = MaxServiceDays + 365;
        const long MaxLTArr = MaxServiceDays / MinRepairLeadTime + 2;
        static long[] IBin = new long[LTBCommon.MaxYear + 1];
        static double[] FRin = new double[LTBCommon.MaxYear + 1];
        static double[] RLin = new double[LTBCommon.MaxYear + 1];
        static long[] RSin = new long[LTBCommon.MaxYear + 1];
        static long[] RSArray = new long[MaxLTArr + 1];
        static long[] RSDayArray = new long[MaxDayArr + 365];
        static double[] StockDayArray = new double[MaxDayArr + 365];
        static double[] ReturnedDayArray = new double[MaxDayArr + 365];
        static double[] SumDemandDayArray = new double[MaxDayArr + 365];
        static double[] IBArray = new double[MaxLTArr + 1];
        static double[] FRArray = new double[MaxLTArr + 1];
        static double[] RLArray = new double[MaxLTArr + 1];
        static double[] RLDayArray = new double[MaxDayArr + 365];
        static double[] Stock_Array = new double[MaxLTArr + 1];
        static double[] Returned_Array = new double[MaxLTArr + 2];
        static double[] Demand_Array = new double[MaxLTArr + 1];
        static double[] SumDemand_Array = new double[MaxLTArr + 1];
        static double[] FSRepairLeadTimeDemand_Array = new double[MaxLTArr + 1];
        static double[] RepairLoss_Array = new double[MaxLTArr + 1];
        static double[] SumRepairLoss_Array = new double[MaxLTArr + 1];
        static double[] Repair_Array = new double[MaxLTArr + 1];
        static double[] SumRepair_Array = new double[MaxLTArr + 1];
        static double[] SafetyMargin_Array = new double[MaxLTArr + 1];
        static double[] SafetyMarginDayArray = new double[MaxDayArr + 365];
        static double ConfidenceLevelDbl;
        public void Calculate(LtbDataSet ltbDataSet)
        {

            int NbrOfSamples;
            double ConfidenceLevelFromNormsInv;
            Presenter.ClearResult(this);
            Presenter.ClearChartData(this);
            NMathConfiguration.LogLocation = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            NMathConfiguration.Init();
            long stockPresent = 0;
            long safetyPresent = 0;

            if (ltbDataSet.RepairLeadTime < 1 || ltbDataSet.RepairLeadTime > 365)
            {
                ltbDataSet.InfoText = "Error: 2 <= Repair Lead Time <=365;";
                return;
            }

            if (ltbDataSet.RepairLeadTime > ltbDataSet.ServiceDays)
            {
                Presenter.ClearResult(this);
                ltbDataSet.InfoText = "Error: Repair Lead Time cannot be longer than Service Period. Please change EoS or Repair Lead Time";
                return;
            }

            if (ltbDataSet.ServiceDays > MaxServiceDays)
            {
                Presenter.ClearResult(this);
                ltbDataSet.InfoText = "Error: The Service Period cannot be longer than 10 years. Please change EoS or LTB.";
                return;
            }

            NbrOfSamples = Mathematics.RoundUpInt(ServiceDays / RepairLeadTime, 0);

            ConvertFromViewModel(ServiceDays, Mathematics.ServiceYears(this), RepairLeadTime, out ConfidenceLevelFromNormsInv);

            var ltb = new LTBCommon();
            ltb.LTBWorker(NbrOfSamples, ltbDataSet.ServiceDays, ltbDataSet.RepairLeadTime, Mathematics.ServiceYears(this), ConfidenceLevelFromNormsInv, ref IBArray, ref RSArray, ref FRArray, ref RLArray,
            ref Stock_Array, ref Returned_Array, ref Demand_Array, ref SumDemand_Array, ref RepairLoss_Array, ref  SumRepairLoss_Array, ref Repair_Array, ref SumRepair_Array, ref SafetyMargin_Array, ref SafetyMarginDayArray, ref FSRepairLeadTimeDemand_Array,
            ref IBin, ref  FRin, ref  RLin, ref  RSin, ref RSDayArray, ref  RLDayArray, ref  StockDayArray, ref  ReturnedDayArray, ref  SumDemandDayArray);
            SetChartData();
            Presenter.GetChart(this);
            stockPresent = Mathematics.RoundLong(Stock_Array[1], 0);
            safetyPresent = SafetyYearArray[0];

            Stock = stockPresent.ToString() + GetCLFromAverage(ConfidenceLevelDbl, SafetyMargin_Array[1]).ToString();

            if (safetyPresent > 0)
            {
                FromAverage = GetSafetyFromAverage(ConfidenceLevelDbl, SafetyMargin_Array[1]);
                Safety = safetyPresent.ToString() + GetCLFromStock(SafetyMargin_Array[1], FromAverage).ToString();
            }
            else
            {
                Safety = string.Empty;
            }

            TotalStock = Convert.ToString(stockPresent + safetyPresent);

            Failed = Mathematics.RoundLong(SumDemand_Array[1], 0).ToString();

            Repaired = Mathematics.RoundLong(SumRepair_Array[1] - SumRepairLoss_Array[1], 0).ToString();

            Lost = RepairPossible ? Mathematics.RoundUpLong(SumRepairLoss_Array[1], 0).ToString() : "Nothing";
        }

        void SetChartData()
        {
            //For Chart
            var yearCnt = 0;
            while (yearCnt <= Mathematics.ServiceYears(this))
            {
                RSYearArray[yearCnt] = RSDayArray[yearCnt * 365 + 1];
                StockYearArray[yearCnt] = Mathematics.RoundLong(StockDayArray[yearCnt * 365 + 1] - RSDayArray[yearCnt * 365 + 1], 0);
                FromAverage = Mathematics.RoundLong(GetSafetyFromAverage(ConfidenceLevelDbl, SafetyMarginDayArray[yearCnt * 365 + 1]), 0);
                FromGamma = Mathematics.RoundLong(GetSafetyFromGamma(ConfidenceLevelDbl, SafetyMarginDayArray[yearCnt * 365 + 1] + ReturnedDayArray[yearCnt * 365 + 1] + FromAverage, ReturnedDayArray[yearCnt * 365 + RepairLeadTime + 1]), 0);
                SafetyYearArray[yearCnt] = FromGamma + FromAverage;
                yearCnt = yearCnt + 1;
            }
        }


        public void InitLabels(LtbDataSet ltbDataSet)
        {
            ltbDataSet.YearLabel0 = "LTB";
            ltbDataSet.YearLabel1 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(1).Year.ToString();
            ltbDataSet.YearLabel2 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(2).Year.ToString();
            ltbDataSet.YearLabel3 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(3).Year.ToString();
            ltbDataSet.YearLabel4 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(4).Year.ToString();
            ltbDataSet.YearLabel5 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(5).Year.ToString();
            ltbDataSet.YearLabel6 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(6).Year.ToString();
            ltbDataSet.YearLabel7 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(7).Year.ToString();
            ltbDataSet.YearLabel8 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(8).Year.ToString();
            ltbDataSet.YearLabel9 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(9).Year.ToString();
        }
        void UpdateInputViewModel()
        {
            if (EOSDate == null || LTBDate == null) return;
            InitLabels(this);
            var cnt = 0;
            cnt = 0;
            var eosFound = false;
            while (cnt <= Mathematics.ServiceYears(this))
            {
                switch (cnt)
                {
                    case 0:
                        if (IB1 == "EoS")
                        {
                            eosFound = true;
                            IB1 = " ";
                            FR1 = " ";
                            RL1 = " ";
                            RS1 = " ";
                        }
                        IB1IsEnabled = true;
                        break;
                    case 1:
                        if (IB2 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            IB2 = " ";
                            FR2 = " ";
                            RL2 = " ";
                            RS2 = " ";
                        }
                        IB2IsEnabled = true;
                        break;
                    case 2:
                        if (IB3 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            IB3 = " ";
                            FR3 = " ";
                            RL3 = " ";
                            RS3 = " ";
                        }
                        IB3IsEnabled = true;
                        break; ;
                    case 3:
                        if (IB4 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            IB4 = " ";
                            FR4 = " ";
                            RL4 = " ";
                            RS4 = " ";
                        }
                        IB4IsEnabled = true;
                        break; ;
                    case 4:
                        if (IB5 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            IB5 = " ";
                            FR5 = " ";
                            RL5 = " ";
                            RS5 = " ";
                        }
                        IB5IsEnabled = true;
                        break;
                    case 5:
                        if (IB6 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            IB6 = " ";
                            FR6 = " ";
                            RL6 = " ";
                            RS6 = " ";
                        }
                        IB6IsEnabled = true;
                        break;
                    case 6:
                        if (IB7 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            IB7 = " ";
                            FR7 = " ";
                            RL7 = " ";
                            RS7 = " ";
                        }
                        IB7IsEnabled = true;
                        break;
                    case 7:
                        if (IB8 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            IB8 = " ";
                            FR8 = " ";
                            RL8 = " ";
                            RS8 = " ";
                        }
                        IB8IsEnabled = true;
                        break;
                    case 8:
                        if (IB9 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            IB9 = " ";
                            FR9 = " ";
                            RL9 = " ";
                            RS9 = " ";
                        }
                        IB9IsEnabled = true;
                        break;
                    case 9:
                        if (IB10 == "EoS" || eosFound)
                        {
                            IB10 = " ";
                        }
                        break;
                }
                cnt += 1;
            }

            switch (Mathematics.ServiceYears(this))
            {
                case 0:
                    IB1 = "EoS";
                    RS1 = string.Empty;
                    RL1 = string.Empty;
                    FR1 = string.Empty;
                    IB1IsEnabled = false;
                    RL1IsEnabled = false;
                    break;
                case 1:
                    IB2 = "EoS";
                    RS2 = string.Empty;
                    RL2 = string.Empty;
                    FR2 = string.Empty;
                    IB2IsEnabled = false;
                    RL2IsEnabled = false;
                    break;
                case 2:
                    IB3 = "EoS";
                    RS3 = string.Empty;
                    RL3 = string.Empty;
                    FR3 = string.Empty;
                    IB3IsEnabled = false;
                    RL3IsEnabled = false;
                    break;
                case 3:
                    IB4 = "EoS";
                    RS4 = string.Empty;
                    RL4 = string.Empty;
                    FR4 = string.Empty;
                    RL4IsEnabled = false;
                    IB4IsEnabled = false;
                    break;
                case 4:
                    IB5 = "EoS";
                    RS5 = string.Empty;
                    RL5 = string.Empty;
                    FR5 = string.Empty;
                    IB5IsEnabled = false;
                    RL5IsEnabled = false;
                    break;
                case 5:
                    IB6 = "EoS";
                    RS6 = string.Empty;
                    RL6 = string.Empty;
                    FR6 = string.Empty;
                    IB6IsEnabled = false;
                    RL6IsEnabled = false;
                    break;
                case 6:
                    IB7 = "EoS";
                    RS7 = string.Empty;
                    RL7 = string.Empty;
                    FR7 = string.Empty;
                    IB7IsEnabled = false;
                    RL7IsEnabled = false;
                    break;
                case 7:
                    IB8 = "EoS";
                    RS8 = string.Empty;
                    RL8 = string.Empty;
                    FR8 = string.Empty;
                    IB8IsEnabled = false;
                    RL8IsEnabled = false;
                    break;
                case 8:
                    IB9 = "EoS";
                    RS9 = string.Empty;
                    RL9 = string.Empty;
                    FR9 = string.Empty;
                    IB9IsEnabled = false;
                    RL9IsEnabled = false;
                    break;
                case 9:
                    IB10 = "EoS";
                    break;
                case 10:
                    break;
                default: break;
            }
            while (cnt <= LTBCommon.MaxYear)
            {
                switch (cnt)
                {
                    case 1:
                        if (Mathematics.ServiceYears(this) != 0)
                        {
                            IB1 = string.Empty;
                            FR1 = string.Empty;
                            RS1 = string.Empty;
                            RL1 = string.Empty;
                            IB1IsEnabled = false;
                            RL1IsEnabled = false;
                        }
                        break;
                    case 2:
                        if (Mathematics.ServiceYears(this) != 1)
                        {
                            IB2 = string.Empty;
                            FR2 = string.Empty;
                            RS2 = string.Empty;
                            RL2 = string.Empty;
                            IB2IsEnabled = false;
                            RL2IsEnabled = false;
                        }
                        break;
                    case 3:
                        if (Mathematics.ServiceYears(this) != 2)
                        {
                            IB3 = string.Empty;
                            FR3 = string.Empty;
                            RS3 = string.Empty;
                            RL3 = string.Empty;
                            IB3IsEnabled = false;
                            RL3IsEnabled = false;
                        }
                        break;
                    case 4:
                        if (Mathematics.ServiceYears(this) != 3)
                        {
                            IB4 = string.Empty;
                            FR4 = string.Empty;
                            RS4 = string.Empty;
                            RL4 = string.Empty;
                            IB4IsEnabled = false;
                            RL4IsEnabled = false;
                        }
                        break;
                    case 5:
                        if (Mathematics.ServiceYears(this) != 4)
                        {
                            IB5 = string.Empty;
                            FR5 = string.Empty;
                            RS5 = string.Empty;
                            RL5 = string.Empty;
                            IB5IsEnabled = false;
                            RL5IsEnabled = false;
                        }
                        break;
                    case 6:
                        if (Mathematics.ServiceYears(this) != 5)
                        {
                            IB6 = string.Empty;
                            FR6 = string.Empty;
                            RS6 = string.Empty;
                            RL6 = string.Empty;
                            IB6IsEnabled = false;
                            RL6IsEnabled = false;
                        }
                        break;
                    case 7:
                        if (Mathematics.ServiceYears(this) != 6)
                        {
                            FR7 = string.Empty;
                            RS7 = string.Empty;
                            RL7 = string.Empty;
                            IB7 = string.Empty;
                            IB7IsEnabled = false;
                            RL7IsEnabled = false;
                        }
                        break;
                    case 8:
                        if (Mathematics.ServiceYears(this) != 7)
                        {
                            IB8 = string.Empty;
                            FR8 = string.Empty;
                            RS8 = string.Empty;
                            RL8 = string.Empty;
                            IB8IsEnabled = false;
                            RL8IsEnabled = false;
                        }
                        break;
                    case 9:
                        if (Mathematics.ServiceYears(this) != 8)
                        {
                            IB9 = string.Empty;
                            FR9 = string.Empty;
                            RS9 = string.Empty;
                            RL9 = string.Empty;
                            IB9IsEnabled = false;
                            RL9IsEnabled = false;
                        }
                        break;
                    case 10:
                        if (Mathematics.ServiceYears(this) != 9)
                        {
                            IB10 = string.Empty;
                        }
                        break;
                }
                cnt += 1;
            }
            AdjustRepair();
        }
        void AdjustRepair()
        {
            int Cnt = 0;
            while (Cnt <= Mathematics.ServiceYears(this))
            {
                switch (Cnt)
                {
                    case 0:
                        if (!RepairPossible)
                        {
                            RL0IsEnabled = false;
                            RL0 = "100";
                        }
                        else
                        {
                            RL0IsEnabled = true;
                        }
                        break;
                    case 1:
                        if (!RepairPossible)
                        {
                            RL1IsEnabled = false;
                            RL1 = "100";
                        }
                        else
                        {
                            RL1IsEnabled = true;
                        }
                        break;
                    case 2:
                        if (!RepairPossible)
                        {
                            RL2IsEnabled = false;
                            RL2 = "100";
                        }
                        else
                        {
                            RL2IsEnabled = true;
                        }
                        break;
                    case 3:
                        if (!RepairPossible)
                        {
                            RL3IsEnabled = false;
                            RL3 = "100";
                        }
                        else
                        {
                            RL3IsEnabled = true;
                        }
                        break;
                    case 4:
                        if (!RepairPossible)
                        {
                            RL4IsEnabled = false;
                            RL4 = "100";
                        }
                        else
                        {
                            RL4IsEnabled = true;
                        }
                        break;
                    case 5:
                        if (!RepairPossible)
                        {
                            RL5IsEnabled = false;
                            RL5 = "100";
                        }
                        else
                        {
                            RL5IsEnabled = true;
                        }
                        break;
                    case 6:
                        if (!RepairPossible)
                        {
                            RL6IsEnabled = false;
                            RL6 = "100";
                        }
                        else
                        {
                            RL6IsEnabled = true;
                        }
                        break;
                    case 7:
                        if (!RepairPossible)
                        {
                            RL7IsEnabled = false;
                            RL7 = "100";
                        }
                        else
                        {
                            RL7IsEnabled = true;
                        }
                        break;
                    case 8:
                        if (!RepairPossible)
                        {
                            RL8IsEnabled = false;
                            RL8 = "100";
                        }
                        else
                        {
                            RL8IsEnabled = true;
                        }
                        break;
                    case 9:
                        if (!RepairPossible)
                        {
                            RL9IsEnabled = false;
                            RL9 = "100";
                        }
                        else
                        {
                            RL9IsEnabled = true;
                        }

                        break;
                }
                Cnt += 1;
            }
        }
    }
}
