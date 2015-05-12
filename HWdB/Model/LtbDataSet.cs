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
        public string Saved { get; set; }

        [LTBDateWithinRangeAttribute(ErrorMessage = "Not valid LTB date")]
        public string LTBDate
        {
            get { return GetValue(() => LTBDate); }
            set
            {
                if (LTBDate != value)
                {
                    SetValue(() => LTBDate, value);
                    if (this.HasErrors.Count == 0)
                    {
                        LtbCalculation.InitYearTabIndex(this);
                    }
                }
            }
        }

        [EOSDateWithinRangeAttribute(ErrorMessage = "Not valid EOS date")]
        public string EOSDate
        {
            get { return GetValue(() => EOSDate); }
            set
            {
                if (EOSDate != value)
                {
                    SetValue(() => EOSDate, value);
                    if (this.HasErrors.Count == 0)
                    {
                        LtbCalculation.InitYearTabIndex(this);
                    }
                }
            }
        }

        //[RegularExpression(@"^([2-9]|[1-9][0-9]|[1-2][0-9][0-9]|3[0-6][0-5])$", ErrorMessage = "Must be within 2 and 365")]
        [Range(2, 365, ErrorMessage = "Must be within 2 and 365")]
        public int RepairLeadTime
        {
            get { return GetValue(() => RepairLeadTime); }
            set { SetValue(() => RepairLeadTime, value); }
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
                    if (this.HasErrors.Count == 0)
                    {
                        LtbCalculation.InitYearTabIndex(this);
                    }
                }
            }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB0
        {
            get { return GetValue(() => IB0); }
            set { SetValue(() => IB0, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB1
        {
            get { return GetValue(() => IB1); }
            set { SetValue(() => IB1, value); }
        }
        [NotMappedAttribute]
        public bool IB1ReadOnly
        {
            get { return GetValue(() => IB1ReadOnly); }
            set { SetValue(() => IB1ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB2
        {
            get { return GetValue(() => IB2); }
            set { SetValue(() => IB2, value); }
        }
        [NotMappedAttribute]
        public bool IB2ReadOnly
        {
            get { return GetValue(() => IB2ReadOnly); }
            set { SetValue(() => IB2ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB3
        {
            get { return GetValue(() => IB3); }
            set { SetValue(() => IB3, value); }
        }
        [NotMappedAttribute]
        public bool IB3ReadOnly
        {
            get { return GetValue(() => IB3ReadOnly); }
            set { SetValue(() => IB3ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB4
        {
            get { return GetValue(() => IB4); }
            set { SetValue(() => IB4, value); }
        }
        [NotMappedAttribute]
        public bool IB4ReadOnly
        {
            get { return GetValue(() => IB4ReadOnly); }
            set { SetValue(() => IB4ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB5
        {
            get { return GetValue(() => IB5); }
            set { SetValue(() => IB5, value); }
        }
        [NotMappedAttribute]
        public bool IB5ReadOnly
        {
            get { return GetValue(() => IB5ReadOnly); }
            set { SetValue(() => IB5ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB6
        {
            get { return GetValue(() => IB6); }
            set { SetValue(() => IB6, value); }
        }
        [NotMappedAttribute]
        public bool IB6ReadOnly
        {
            get { return GetValue(() => IB6ReadOnly); }
            set { SetValue(() => IB6ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB7
        {
            get { return GetValue(() => IB7); }
            set { SetValue(() => IB7, value); }
        }
        [NotMappedAttribute]
        public bool IB7ReadOnly
        {
            get { return GetValue(() => IB7ReadOnly); }
            set { SetValue(() => IB7ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB8
        {
            get { return GetValue(() => IB8); }
            set { SetValue(() => IB8, value); }
        }
        [NotMappedAttribute]
        public bool IB8ReadOnly
        {
            get { return GetValue(() => IB8ReadOnly); }
            set { SetValue(() => IB8ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB9
        {
            get { return GetValue(() => IB9); }
            set { SetValue(() => IB9, value); }
        }
        [NotMappedAttribute]
        public bool IB9ReadOnly
        {
            get { return GetValue(() => IB9ReadOnly); }
            set { SetValue(() => IB9ReadOnly, value); }
        }
        public string IB10 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR0
        {
            get { return GetValue(() => FR0); }
            set { SetValue(() => FR0, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR1
        {
            get { return GetValue(() => FR1); }
            set { SetValue(() => FR1, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR2
        {
            get { return GetValue(() => FR2); }
            set { SetValue(() => FR2, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR3
        {
            get { return GetValue(() => FR3); }
            set { SetValue(() => FR3, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR4
        {
            get { return GetValue(() => FR4); }
            set { SetValue(() => FR4, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR5
        {
            get { return GetValue(() => FR5); }
            set { SetValue(() => FR5, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR6
        {
            get { return GetValue(() => FR6); }
            set { SetValue(() => FR6, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR7
        {
            get { return GetValue(() => FR7); }
            set { SetValue(() => FR7, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR8
        {
            get { return GetValue(() => FR8); }
            set { SetValue(() => FR8, value); }
        }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][,][0-9]{0,4}[1-9]|[1-9][0-9][,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public string FR9
        {
            get { return GetValue(() => FR9); }
            set { SetValue(() => FR9, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS0
        {
            get { return GetValue(() => RS0); }
            set { SetValue(() => RS0, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS1
        {
            get { return GetValue(() => RS1); }
            set { SetValue(() => RS1, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS2
        {
            get { return GetValue(() => RS2); }
            set { SetValue(() => RS2, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS3
        {
            get { return GetValue(() => RS3); }
            set { SetValue(() => RS3, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS4
        {
            get { return GetValue(() => RS4); }
            set { SetValue(() => RS4, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS5
        {
            get { return GetValue(() => RS5); }
            set { SetValue(() => RS5, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS6
        {
            get { return GetValue(() => RS6); }
            set { SetValue(() => RS6, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS7
        {
            get { return GetValue(() => RS7); }
            set { SetValue(() => RS7, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS8
        {
            get { return GetValue(() => RS8); }
            set { SetValue(() => RS8, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public string RS9
        {
            get { return GetValue(() => RS9); }
            set { SetValue(() => RS9, value); }
        }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL0
        {
            get { return GetValue(() => RL0); }
            set { SetValue(() => RL0, value); }
        }
        [NotMappedAttribute]
        public bool RL0ReadOnly
        {
            get { return GetValue(() => RL0ReadOnly); }
            set { SetValue(() => RL0ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL1
        {
            get { return GetValue(() => RL1); }
            set { SetValue(() => RL1, value); }
        }
        [NotMappedAttribute]
        public bool RL1ReadOnly
        {
            get { return GetValue(() => RL1ReadOnly); }
            set { SetValue(() => RL1ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL2
        {
            get { return GetValue(() => RL2); }
            set { SetValue(() => RL2, value); }
        }
        [NotMappedAttribute]
        public bool RL2ReadOnly
        {
            get { return GetValue(() => RL2ReadOnly); }
            set { SetValue(() => RL2ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL3
        {
            get { return GetValue(() => RL3); }
            set { SetValue(() => RL3, value); }
        }
        [NotMappedAttribute]
        public bool RL3ReadOnly
        {
            get { return GetValue(() => RL3ReadOnly); }
            set { SetValue(() => RL3ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL4
        {
            get { return GetValue(() => RL4); }
            set { SetValue(() => RL4, value); }
        }
        [NotMappedAttribute]
        public bool RL4ReadOnly
        {
            get { return GetValue(() => RL4ReadOnly); }
            set { SetValue(() => RL4ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL5
        {
            get { return GetValue(() => RL5); }
            set { SetValue(() => RL5, value); }
        }
        [NotMappedAttribute]
        public bool RL5ReadOnly
        {
            get { return GetValue(() => RL5ReadOnly); }
            set { SetValue(() => RL5ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL6
        {
            get { return GetValue(() => RL6); }
            set { SetValue(() => RL6, value); }
        }
        [NotMappedAttribute]
        public bool RL6ReadOnly
        {
            get { return GetValue(() => RL6ReadOnly); }
            set { SetValue(() => RL6ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL7
        {
            get { return GetValue(() => RL7); }
            set { SetValue(() => RL7, value); }
        }
        [NotMappedAttribute]
        public bool RL7ReadOnly
        {
            get { return GetValue(() => RL7ReadOnly); }
            set { SetValue(() => RL7ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL8
        {
            get { return GetValue(() => RL8); }
            set { SetValue(() => RL8, value); }
        }
        [NotMappedAttribute]
        public bool RL8ReadOnly
        {
            get { return GetValue(() => RL8ReadOnly); }
            set { SetValue(() => RL8ReadOnly, value); }
        }

        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public string RL9
        {
            get { return GetValue(() => RL9); }
            set { SetValue(() => RL9, value); }
        }
        [NotMappedAttribute]
        public bool RL9ReadOnly
        {
            get { return GetValue(() => RL9ReadOnly); }
            set { SetValue(() => RL9ReadOnly, value); }
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
    }
}
