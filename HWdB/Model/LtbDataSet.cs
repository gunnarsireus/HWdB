using HWdB.CustomValidationAttributes;
using HWdB.Notification;
using HWdB.Utils;
using LTBCore;
using MHWdB.CustomValidationAttributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media.Imaging;
namespace HWdB.Model
{

    public class LtbDataSet : PropertyChangedNotification
    {
        [Key]
        [Required(ErrorMessage = "ID is required")]
        public int ID { get; set; }
        public int UserId { get; set; }
        [MaxLength(30, ErrorMessage = "Name exceeded 30 letters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Name contains invalid letters")]
        public string Customer { get; set; }
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
                LtbCalculation.InitYearTabIndex(this);
            }
        }

        [EOSDateWithinRangeAttribute(ErrorMessage = "Not valid EOS date")]
        public string EOSDate
        {
            get { return GetValue(() => EOSDate); }
            set
            {
                SetValue(() => EOSDate, value);
                LtbCalculation.InitYearTabIndex(this);
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
                LtbCalculation.InitYearTabIndex(this);
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
                    LtbCalculation.InitYearTabIndex(this);
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

        public string ServiceDays
        {
            get { return GetValue(() => ServiceDays); }
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
    }
}
