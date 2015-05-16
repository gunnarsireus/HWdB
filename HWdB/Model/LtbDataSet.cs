using CenterSpace.NMath.Core;
using CenterSpace.NMath.Stats;
using HWdB.CustomValidationAttributes;
using HWdB.Notification;
using HWdB.Utils;
using LTBCore;
using MHWdB.CustomValidationAttributes;
using System;
using System.ComponentModel;
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
            StockYearArray = new long[LTBCommon.MaxYear + 1];

            RSYearArray = new long[LTBCommon.MaxYear + 1];

            SafetyYearArray = new long[LTBCommon.MaxYear + 1];
            ClearChartData();
            GetChart();
        }
        [Key]
        public int ID { get; set; }
        public string CreatedBy { get; set; }
        [MaxLength(100, ErrorMessage = "Customer exceeded 100 letters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Name contains invalid letters")]
        public string Customer { get; set; }
        [MaxLength(40, ErrorMessage = "Version exceeded 40 letters")]
        public string Version { get; set; }
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
                SetValue(() => LTBDate, value);
                SetInputArray();
            }
        }

        [EOSDateWithinRangeAttribute(ErrorMessage = "Not valid EOS date")]
        public string EOSDate
        {
            get { return GetValue(() => EOSDate); }
            set
            {
                SetValue(() => EOSDate, value);
                SetInputArray();
            }
        }

        [RepairLeadTimeEOSLTBRangeAttribute(ErrorMessage = "Service period cannot be shorter than Repair Lead Time")]
        [Range(2, 365, ErrorMessage = "Reapir Lead Time must be within 2 and 365")]
        public int RepairLeadTime
        {
            get { return GetValue(() => RepairLeadTime); }
            set
            {
                SetValue(() => RepairLeadTime, value);
                SetInputArray();
            }
        }
        public string ConfidenceLevel { get; set; }
        [DisplayName(" ")]
        public Boolean RepairPossible
        {
            get { return GetValue(() => RepairPossible); }
            set
            {
                if (RepairPossible != value)
                {
                    SetValue(() => RepairPossible, value);
                    SetInputArray();
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
        public string l0
        {
            get { return GetValue(() => l0); }
            set { SetValue(() => l0, value); }
        }
        [NotMappedAttribute]
        public string l1
        {
            get { return GetValue(() => l1); }
            set { SetValue(() => l1, value); }
        }
        [NotMappedAttribute]
        public string l2
        {
            get { return GetValue(() => l2); }
            set { SetValue(() => l2, value); }
        }
        [NotMappedAttribute]
        public string l3
        {
            get { return GetValue(() => l3); }
            set { SetValue(() => l3, value); }
        }
        [NotMappedAttribute]
        public string l4
        {
            get { return GetValue(() => l4); }
            set { SetValue(() => l4, value); }
        }
        [NotMappedAttribute]
        public string l5
        {
            get { return GetValue(() => l5); }
            set { SetValue(() => l5, value); }
        }
        [NotMappedAttribute]
        public string l6
        {
            get { return GetValue(() => l6); }
            set { SetValue(() => l6, value); }
        }
        [NotMappedAttribute]
        public string l7
        {
            get { return GetValue(() => l7); }
            set { SetValue(() => l7, value); }
        }
        [NotMappedAttribute]
        public string l8
        {
            get { return GetValue(() => l8); }
            set { SetValue(() => l8, value); }
        }
        [NotMappedAttribute]
        public string l9
        {
            get { return GetValue(() => l9); }
            set { SetValue(() => l9, value); }
        }
        [NotMappedAttribute]
        int ServiceYears
        {
            get
            {

                System.DateTime NewYear = default(System.DateTime);

                if (Convert.ToDateTime(LTBDate).Year == Convert.ToDateTime(EOSDate).Year)
                {
                    return 0;
                }
                else
                {
                    NewYear = Convert.ToDateTime(Convert.ToDateTime(LTBDate).Year.ToString() + "-01-01");
                    if (IsLeapYear(Convert.ToDateTime(LTBDate).Year) & DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, NewYear, Convert.ToDateTime(LTBDate)) < 59)
                    {
                        return Convert.ToInt32((DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, Convert.ToDateTime(LTBDate), Convert.ToDateTime(EOSDate)) + CountLeaps(Convert.ToDateTime(LTBDate).Year) - CountLeaps(Convert.ToDateTime(EOSDate).Year) - 2) / 365);
                    }
                    else
                    {
                        return Convert.ToInt32((DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, Convert.ToDateTime(LTBDate), Convert.ToDateTime(EOSDate)) + CountLeaps(Convert.ToDateTime(LTBDate).Year) - CountLeaps(Convert.ToDateTime(EOSDate).Year) - 1) / 365);
                    }
                }
            }
        }
        public void Clone(LtbDataSet that)
        {
            Customer = that.Customer;
            ID = that.ID;
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

            l0 = that.l0;
            l1 = that.l1;
            l2 = that.l2;
            l3 = that.l3;
            l4 = that.l4;
            l5 = that.l5;
            l6 = that.l6;
            l7 = that.l7;
            l8 = that.l8;
            l9 = that.l9;
        }

        public void ClearResult()
        {
            TotalStock = string.Empty;
            Stock = string.Empty;
            Safety = string.Empty;
            InfoText = string.Empty;
            Failed = string.Empty;
            Repaired = string.Empty;
            Lost = string.Empty;
        }
        public void ClearChartData()
        {
            int YearCnt = 0;
            while (YearCnt <= 10)
            {
                RSYearArray[YearCnt] = 0;
                StockYearArray[YearCnt] = 0;
                SafetyYearArray[YearCnt] = 0;
                YearCnt = YearCnt + 1;
            }
        }
        public void GetChart()
        {
            System.Web.UI.DataVisualization.Charting.Chart chart = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Height = 300,
                Width = 900,
                ImageType = System.Web.UI.DataVisualization.Charting.ChartImageType.Png
            };
            System.Web.UI.DataVisualization.Charting.ChartArea chartArea = chart.ChartAreas.Add("Stock");
            chartArea.Area3DStyle.Enable3D = true;

            System.Web.UI.DataVisualization.Charting.Series RS = chart.Series.Add("0");
            System.Web.UI.DataVisualization.Charting.Series Stock = chart.Series.Add("1");
            System.Web.UI.DataVisualization.Charting.Series Safety = chart.Series.Add("2");
            RS.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;
            Stock.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;
            Safety.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;

            chart.Series["0"].Points.DataBindXY(xValues, RSYearArray);
            chart.Series["0"].Color = System.Drawing.Color.Green;
            chart.Series["1"].Points.DataBindXY(xValues, StockYearArray);
            chart.Series["1"].Color = System.Drawing.Color.Blue;
            chart.Series["2"].Points.DataBindXY(xValues, SafetyYearArray);
            chart.Series["2"].Color = System.Drawing.Color.Red;
            MemoryStream ms = new MemoryStream();

            chart.SaveImage(ms);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            LtbChart = image;

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
        static double Conf_in;
        static int N;
        static double Conf_Level;


        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private static double[] norminv_a = { 2.50662823884, -18.61500062529, 41.39119773534, -25.44106049637 };
        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private static double[] norminv_b = { -8.47351093090, 23.08336743743, -21.06224101826, 3.13082909833 };
        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private static double[] norminv_c = { 0.3374754822726147, 0.9761690190917186, 0.1607979714918209, 0.0276438810333863, 0.0038405729373609, 0.0003951896511919, 0.0000321767881768, 0.0000002888167364, 0.0000003960315187 };
        private static double norminv(double u)
        {
            /* returns the inverse of cumulative normal distribution function Reference> The Full Monte, by Boris Moro, Union Bank of Switzerland
                         RISK 1995(2)*/

            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //static double a[4]={ 2.50662823884, -18.61500062529, 41.39119773534, -25.44106049637};
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //static double b[4]={ -8.47351093090, 23.08336743743, -21.06224101826, 3.13082909833};
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //static double c[9]={0.3374754822726147, 0.9761690190917186, 0.1607979714918209, 0.0276438810333863, 0.0038405729373609, 0.0003951896511919, 0.0000321767881768, 0.0000002888167364, 0.0000003960315187};
            double x;
            double r;
            x = u - 0.5;
            if (Math.Abs(x) < 0.42)
            {
                r = x * x;
                r = x * (((norminv_a[3] * r + norminv_a[2]) * r + norminv_a[1]) * r + norminv_a[0]) / ((((norminv_b[3] * r + norminv_b[2]) * r + norminv_b[1]) * r + norminv_b[0]) * r + 1.0);
                return (r);
            }
            r = u;
            if (x > 0.0) r = 1.0 - u;
            r = Math.Log(-Math.Log(r));
            r = norminv_c[0] + r * (norminv_c[1] + r * (norminv_c[2] + r * (norminv_c[3] + r * (norminv_c[4] + r * (norminv_c[5] + r * (norminv_c[6] + r * (norminv_c[7] + r * norminv_c[8])))))));
            if (x < 0.0) r = -r;
            return (r);
        }

        //private static int calcreserve2(int M, double FR, double p)
        //{
        //    double pi = 3.1415926535897932384626433832795028841971693993751058209749;
        //    double L = M * FR / pi;
        //    //double lp;
        //    return (int)(RoundLong(2 * Math.Sqrt(L) + pi * L + NormSInv(p) * Math.Sqrt(L * (2 * pi - 4) + Math.Sqrt(L) + 3 / 16)));
        //}


        static int calcreserve2(int M, double FR, double p)
        {
            switch (RoundLong(p * 1000, 0))
            {
                case 600: return (int)RoundLong(M + (133 * Sqr((double)M) / 100), 0);
                case 700: return (int)RoundLong(M + (156 * Sqr((double)M) / 100), 0);
                case 800: return (int)RoundLong(M + (184 * Sqr((double)M) / 100), 0);
                case 900: return (int)RoundLong(M + (223 * Sqr((double)M) / 100), 0);
                case 950: return (int)RoundLong(M + (255 * Sqr((double)M) / 100), 0);
                case 995: return (int)RoundLong(M + (340 * Sqr((double)M) / 100), 0);
                default: return (int)RoundLong(M + (340 * Sqr((double)M) / 100), 0);
            }
        }

        static int calcreserve(int M, double FR, double p)
        {
            if (M * FR > 10000) return calcreserve2(M, FR, p);
            int k; // init counter
            int i = 0;
            double pp = 0; // init prob
            while (pp < p) // loop while cumaltive probability less than p
            {
                for (k = 0; k <= i; k++)
                    /* calculate probability */
                    pp = pp + Math.Exp(-StatsFunctions.GammaLn(i + 2) - StatsFunctions.GammaLn(k + 1) + 2.0 * Math.Log((double)i + 1 - k) - 2.0 * FR * M + (k + i) * Math.Log(FR * M));
                i = i + 1;
            }
            return (i - 1);
        }
        static bool IsLeapYear(long Y)
        {
            return (Y > 0) && (Y % 4) == 0 && !((Y % 100) == 0 && !((Y % 400) == 0));
        }
        static long CountLeaps(long Y)
        {
            return (Y - 1) / 4 - (Y - 1) / 100 + (Y - 1) / 400;
        }
        static long CountDays(long Y)
        {
            return (Y - 1) * 365 + CountLeaps(Y);
        }
        static long CountYears(long d)
        {
            return 1 + (d - CountLeaps(d / 365)) / 365;
        }
        static long DaysBetweenYears(long y1, long y2)
        {
            return CountDays(y2) - CountDays(y1);
        }
        static double Sqr(double x)
        {
            return Math.Pow(x, 0.5);
        }
        static double RoundUpDouble(double x, int Y)
        {
            return Math.Round(x + 0.49999999999, Y);
        }
        static long RoundUpLong(double x, int Y)
        {
            return Convert.ToInt64(Math.Round(x + 0.49999999999, Y));
        }
        static long RoundLong(double x, int Y)
        {
            return Convert.ToInt64(Math.Round(x, Y));
        }
        static long RoundLong(double x)
        {
            return Convert.ToInt64(Math.Round(x, 0));
        }
        static int RoundUpInt(double x, int Y)
        {
            return Convert.ToInt32(Math.Round(x + 0.49999999999, Y));
        }
        // This function is a replacement for the Microsoft Excel Worksheet function NORMSINV.
        // It uses the algorithm of Peter J. Acklam to compute the inverse normal cumulative
        // distribution. Refer to http://home.online.no/~pjacklam/notes/invnorm/index.html for
        // a description of the algorithm.
        // Adapted to VB by Christian d'Heureuse, http://www.source-code.biz.
        static double NormSInv(double p)
        {
            double functionReturnValue = 0;
            const double a1 = -39.6968302866538, a2 = 220.946098424521, a3 = -275.928510446969;
            const double a4 = 138.357751867269, a5 = -30.6647980661472, a6 = 2.50662827745924;
            const double b1 = -54.4760987982241, b2 = 161.585836858041, b3 = -155.698979859887;
            const double b4 = 66.8013118877197, b5 = -13.2806815528857, c1 = -0.00778489400243029;
            const double c2 = -0.322396458041136, c3 = -2.40075827716184, c4 = -2.54973253934373;
            const double c5 = 4.37466414146497, c6 = 2.93816398269878, d1 = 0.00778469570904146;
            const double d2 = 0.32246712907004, d3 = 2.445134137143, d4 = 3.75440866190742;
            const double p_low = 0.02425, p_high = 1 - p_low;
            double q = 0;
            double r = 0;
            functionReturnValue = 0;
            if (p < 0 | p > 1)
            {
                // Err.Raise(Constants.vbObjectError, "", "NormSInv: Argument out of range.");
            }
            else if (p < p_low)
            {
                q = Sqr(-2 * Math.Log(p));
                functionReturnValue = (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
            }
            else if (p <= p_high)
            {
                q = p - 0.5;
                r = q * q;
                functionReturnValue = (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q / (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1);
            }
            else
            {
                q = Sqr(-2 * Math.Log(1 - p));
                functionReturnValue = -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
            }
            return functionReturnValue;
        }
        static double ConfL(double Y)
        {
            return NormSInv(Y);
        }
        static double Pow(double x, double Y)
        {
            return Math.Pow(x, Y);
        }

        static long Factorial(long x)
        {
            long functionReturnValue = 0;
            if (x <= 1)
            {
                functionReturnValue = 1;
            }
            else
            {
                functionReturnValue = x * Factorial(x - 1);
            }
            return functionReturnValue;
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
                PoissonDistribution Poisson = new PoissonDistribution(Stock);
                double tmp = 0;
                NMathConfiguration.Init();
                tmp = Poisson.CDF(Math.Round(Stock + Safety, 0));
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
                PoissonDistribution Poisson = new PoissonDistribution(Average);
                long K = 0;
                K = Convert.ToInt64(Math.Round(Average, 0));

                while (Poisson.CDF(K) < CL)
                {
                    K = K + 1;
                }

                functionReturnValue = Convert.ToInt64(Math.Round(K - Average, 0));
                if (functionReturnValue < 0)
                    functionReturnValue = 0;
            }
            else
            {
                functionReturnValue = RoundLong(NormSInv(CL) * Sqr(Average), 0);
            }
            return functionReturnValue;
        }

        static long GetSafetyFromGamma(double CL, double FromAverage, double Returned)
        {
            long ReturnValue;

            ReturnValue = (long)(calcreserve((int)RoundLong(Returned, 0), 1, CL) - FromAverage);
            if (ReturnValue < 0) ReturnValue = 0;
            if (ReturnValue > FromAverage) ReturnValue = (long)FromAverage;

            return ReturnValue;
        }


        static double GetAverageFromReturned(double Average)
        {
            if (Average < 8) { return Average / 3.764705; } else { return (1.125 + Average / 8); }
        }



        private static double LogGamma(double x)
        {

            if (x <= 0.0)
            {
                //std.stringstream os = new std.stringstream();
                //os << "Invalid input argument " << x << ". Argument must be positive.";
                //throw std.invalid_argument(os.str());
            }

            if (x < 12.0)
            {
                return Math.Log(Math.Abs(Gamma(x)));
            }

            // Abramowitz and Stegun 6.1.41
            // Asymptotic series should be good to at least 11 or 12 figures
            // For error analysis, see Whittiker and Watson
            // A Course in Modern Analysis (1927), page 252

            double[] c = { 1.0 / 12.0, -1.0 / 360.0, 1.0 / 1260.0, -1.0 / 1680.0, 1.0 / 1188.0, -691.0 / 360360.0, 1.0 / 156.0, -3617.0 / 122400.0 };
            double z = 1.0 / (x * x);
            double sum = c[7];
            for (int i = 6; i >= 0; i--)
            {
                sum *= z;
                sum += c[i];
            }
            double series = sum / x;

            const double halfLogTwoPi = 0.91893853320467274178032973640562;
            double LogGamma = (x - 0.5) * Math.Log(x) - x + halfLogTwoPi + series;
            return LogGamma;
        }


        private static double Gamma(double x)
        {
            if (x <= 0.0)
            {
                //std.stringstream os = new std.stringstream();
                //os << "Invalid input argument " << x << ". Argument must be positive.";
                //throw std.invalid_argument(os.str());
            }

            // Split the function domain into three intervals:
            // (0, 0.001), [0.001, 12), and (12, infinity)

            ///////////////////////////////////////////////////////////////////////////
            // First interval: (0, 0.001)
            //
            // For small x, 1/Gamma(x) has power series x + gamma x^2  - ...
            // So in this range, 1/Gamma(x) = x + gamma x^2 with error on the order of x^3.
            // The relative error over this interval is less than 6e-7.

            const double Gamma = 0.577215664901532860606512090; // Euler's gamma constant

            if (x < 0.001)
                return 1.0 / (x * (1.0 + Gamma * x));

            ///////////////////////////////////////////////////////////////////////////
            // Second interval: [0.001, 12)

            if (x < 12.0)
            {
                // The algorithm directly approximates Gamma over (1,2) and uses
                // reduction identities to reduce other arguments to this interval.

                double y = x;
                int n = 0;
                bool arg_was_less_than_one = (y < 1.0);

                // Add or subtract integers as necessary to bring y into (1,2)
                // Will correct for this below
                if (arg_was_less_than_one)
                {
                    y += 1.0;
                }
                else
                {
                    n = (int)(Math.Floor(y)) - 1; // will use n later
                    y -= n;
                }

                // numerator coefficients for approximation over the interval (1,2)
                double[] p = { -1.71618513886549492533811E+0, 2.47656508055759199108314E+1, -3.79804256470945635097577E+2, 6.29331155312818442661052E+2, 8.66966202790413211295064E+2, -3.14512729688483675254357E+4, -3.61444134186911729807069E+4, 6.64561438202405440627855E+4 };

                // denominator coefficients for approximation over the interval (1,2)
                double[] q = { -3.08402300119738975254353E+1, 3.15350626979604161529144E+2, -1.01515636749021914166146E+3, -3.10777167157231109440444E+3, 2.25381184209801510330112E+4, 4.75584627752788110767815E+3, -1.34659959864969306392456E+5, -1.15132259675553483497211E+5 };

                double num = 0.0;
                double den = 1.0;
                int i;

                double z = y - 1;
                for (i = 0; i < 8; i++)
                {
                    num = (num + p[i]) * z;
                    den = den * z + q[i];
                }
                double result = num / den + 1.0;

                // Apply correction if argument was not initially in (1,2)
                if (arg_was_less_than_one)
                {
                    // Use identity Gamma(z) = gamma(z+1)/z
                    // The variable "result" now holds Gamma of the original y + 1
                    // Thus we use y-1 to get back the orginal y.
                    result /= (y - 1.0);
                }
                else
                {
                    // Use the identity Gamma(z+n) = z*(z+1)* ... *(z+n-1)*gamma(z)
                    for (i = 0; i < n; i++)
                        result *= y++;
                }

                return result;
            }

            ///////////////////////////////////////////////////////////////////////////
            // Third interval: [12, infinity)

            if (x > 171.624)
            {
                // Correct answer too large to display. Force +infinity.
                double temp = double.MaxValue;
                return temp * 2.0;
            }

            return Math.Exp(LogGamma(x));
        }

        void GetInputFromArray(double SD, int LastYear, double LD, ref double CL, int N)
        {
            int Cnt = 0;
            //int Conf = Convert.ToInt32(Ltb.ConfidenceLevel);
            switch (ConfidenceLevel)
            {
                //Confidence Level

                case "60%":
                    CL = ConfL(0.6);
                    Conf_in = 0.6;

                    break;
                case "70%":
                    CL = ConfL(0.7);
                    Conf_in = 0.7;

                    break;
                case "80%":
                    CL = ConfL(0.8);
                    Conf_in = 0.8;

                    break;
                case "90%":
                    CL = ConfL(0.9);
                    Conf_in = 0.9;

                    break;
                case "95%":
                    CL = ConfL(0.95);
                    Conf_in = 0.95;

                    break;
                case "99,5%":
                    CL = ConfL(0.995);
                    Conf_in = 0.995;

                    break;

            }

            Cnt = 0;

            while (Cnt <= LastYear)
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
        public void Calculate()
        {
            InfoText = "";
            ClearResult();
            ClearChartData();
            NMathConfiguration.LogLocation = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            NMathConfiguration.Init();
            long StockPresent = 0;
            long SafetyPresent = 0;

            if (RepairLeadTime < 1 | RepairLeadTime > 365)
            {
                InfoText = "Error: 2 <= Repair Lead Time <=365TextInfo;";
                return;
            }

            if (RepairLeadTime > ServiceDays)
            {
                ClearResult();
                InfoText = "Error: Repair Lead Time cannot be longer than Service Period. Please change EoS or Repair Lead Time";
                return;
            }

            if (ServiceDays > MaxServiceDays)
            {
                ClearResult();
                InfoText = "Error: The Service Period cannot be longer than 10 years. Please change EoS or LTB.";
                return;
            }

            N = RoundUpInt(ServiceDays / RepairLeadTime, 0);

            GetInputFromArray(ServiceDays, ServiceYears, RepairLeadTime, ref Conf_Level, N);

            LTBCommon LTB = new LTBCommon();
            LTB.LTBWorker(N, ServiceDays, RepairLeadTime, ServiceYears, Conf_Level, ref IBArray, ref RSArray, ref FRArray, ref RLArray,
            ref Stock_Array, ref Returned_Array, ref Demand_Array, ref SumDemand_Array, ref RepairLoss_Array, ref  SumRepairLoss_Array, ref Repair_Array, ref SumRepair_Array, ref SafetyMargin_Array, ref SafetyMarginDayArray, ref FSRepairLeadTimeDemand_Array,
            ref IBin, ref  FRin, ref  RLin, ref  RSin, ref  RSDayArray, ref  RLDayArray, ref  StockDayArray, ref  ReturnedDayArray, ref  SumDemandDayArray);
            SetChartData();
            GetChart();
            StockPresent = RoundLong(Stock_Array[1], 0);
            SafetyPresent = SafetyYearArray[0];

            Stock = StockPresent.ToString() + GetCLFromAverage(Conf_in, SafetyMargin_Array[1]).ToString();

            if (SafetyPresent > 0)
            {
                FromAverage = GetSafetyFromAverage(Conf_in, SafetyMargin_Array[1]);
                Safety = SafetyPresent.ToString() + GetCLFromStock(SafetyMargin_Array[1], FromAverage).ToString();
            }
            else
            {
                Safety = string.Empty;
            }

            TotalStock = Convert.ToString(StockPresent + SafetyPresent);

            Failed = RoundLong(SumDemand_Array[1], 0).ToString();

            Repaired = RoundLong(SumRepair_Array[1] - SumRepairLoss_Array[1], 0).ToString();

            if (RepairPossible) { Lost = RoundUpLong(SumRepairLoss_Array[1], 0).ToString(); } else { Lost = "Nothing"; }
        }

        void SetChartData()
        {
            //For Chart
            int YearCnt = 0;
            while (YearCnt <= ServiceYears)
            {
                RSYearArray[YearCnt] = RSDayArray[YearCnt * 365 + 1];
                StockYearArray[YearCnt] = RoundLong(StockDayArray[YearCnt * 365 + 1] - RSDayArray[YearCnt * 365 + 1], 0);
                FromAverage = RoundLong(GetSafetyFromAverage(Conf_in, SafetyMarginDayArray[YearCnt * 365 + 1]), 0);
                FromGamma = RoundLong(GetSafetyFromGamma(Conf_in, SafetyMarginDayArray[YearCnt * 365 + 1] + ReturnedDayArray[YearCnt * 365 + 1] + FromAverage, ReturnedDayArray[YearCnt * 365 + RepairLeadTime + 1]), 0);
                SafetyYearArray[YearCnt] = FromGamma + FromAverage;
                YearCnt = YearCnt + 1;
            }
        }


        public void InitLabels()
        {
            l0 = "LTB";
            l1 = Convert.ToDateTime(LTBDate).AddYears(1).Year.ToString();
            l2 = Convert.ToDateTime(LTBDate).AddYears(2).Year.ToString();
            l3 = Convert.ToDateTime(LTBDate).AddYears(3).Year.ToString();
            l4 = Convert.ToDateTime(LTBDate).AddYears(4).Year.ToString();
            l5 = Convert.ToDateTime(LTBDate).AddYears(5).Year.ToString();
            l6 = Convert.ToDateTime(LTBDate).AddYears(6).Year.ToString();
            l7 = Convert.ToDateTime(LTBDate).AddYears(7).Year.ToString();
            l8 = Convert.ToDateTime(LTBDate).AddYears(8).Year.ToString();
            l9 = Convert.ToDateTime(LTBDate).AddYears(9).Year.ToString();
        }
        public void SetInputArray()
        {
            if (EOSDate == null || LTBDate == null) return;
            InitLabels();
            int Cnt = 0;
            ServiceDays = ServiceDays;  //Trigger update of View
            Cnt = 0;
            bool EOSFound = false;
            while (Cnt <= ServiceYears)
            {
                switch (Cnt)
                {
                    case 0:
                        if (IB1 == "EoS")
                        {
                            EOSFound = true;
                            IB1 = " ";
                            FR1 = " ";
                            RL1 = " ";
                            RS1 = " ";
                        }
                        IB1IsEnabled = true;
                        break;
                    case 1:
                        if (IB2 == "EoS" || EOSFound)
                        {
                            EOSFound = true;
                            IB2 = " ";
                            FR2 = " ";
                            RL2 = " ";
                            RS2 = " ";
                        }
                        IB2IsEnabled = true;
                        break;
                    case 2:
                        if (IB3 == "EoS" || EOSFound)
                        {
                            EOSFound = true;
                            IB3 = " ";
                            FR3 = " ";
                            RL3 = " ";
                            RS3 = " ";
                        }
                        IB3IsEnabled = true;
                        break; ;
                    case 3:
                        if (IB4 == "EoS" || EOSFound)
                        {
                            EOSFound = true;
                            IB4 = " ";
                            FR4 = " ";
                            RL4 = " ";
                            RS4 = " ";
                        }
                        IB4IsEnabled = true;
                        break; ;
                    case 4:
                        if (IB5 == "EoS" || EOSFound)
                        {
                            EOSFound = true;
                            IB5 = " ";
                            FR5 = " ";
                            RL5 = " ";
                            RS5 = " ";
                        }
                        IB5IsEnabled = true;
                        break;
                    case 5:
                        if (IB6 == "EoS" || EOSFound)
                        {
                            EOSFound = true;
                            IB6 = " ";
                            FR6 = " ";
                            RL6 = " ";
                            RS6 = " ";
                        }
                        IB6IsEnabled = true;
                        break;
                    case 6:
                        if (IB7 == "EoS" || EOSFound)
                        {
                            EOSFound = true;
                            IB7 = " ";
                            FR7 = " ";
                            RL7 = " ";
                            RS7 = " ";
                        }
                        IB7IsEnabled = true;
                        break;
                    case 7:
                        if (IB8 == "EoS" || EOSFound)
                        {
                            EOSFound = true;
                            IB8 = " ";
                            FR8 = " ";
                            RL8 = " ";
                            RS8 = " ";
                        }
                        IB8IsEnabled = true;
                        break;
                    case 8:
                        if (IB9 == "EoS" || EOSFound)
                        {
                            EOSFound = true;
                            IB9 = " ";
                            FR9 = " ";
                            RL9 = " ";
                            RS9 = " ";
                        }
                        IB9IsEnabled = true;
                        break;
                    case 9:
                        if (IB10 == "EoS" || EOSFound)
                        {
                            IB10 = " ";
                        }
                        break;
                }
                Cnt += 1;
            }

            switch (ServiceYears)
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
            }
            while (Cnt <= LTBCommon.MaxYear)
            {
                switch (Cnt)
                {
                    case 1:
                        if (ServiceYears != 0)
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
                        if (ServiceYears != 1)
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
                        if (ServiceYears != 2)
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
                        if (ServiceYears != 3)
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
                        if (ServiceYears != 4)
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
                        if (ServiceYears != 5)
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
                        if (ServiceYears != 6)
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
                        if (ServiceYears != 7)
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
                        if (ServiceYears != 8)
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
                        if (ServiceYears != 9)
                        {
                            IB10 = string.Empty;
                        }
                        break;
                }
                Cnt += 1;
            }
            AdjustRepair();
        }
        void AdjustRepair()
        {
            int Cnt = 0;
            while (Cnt <= ServiceYears)
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
