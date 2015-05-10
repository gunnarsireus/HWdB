using HWdB.Notification;
using LTBCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace HWdB.Model
{

    public class LtbDataSet : PropertyChangedNotification
    {
        [Required(ErrorMessage = "ID is required")]
        public int ID { get; set; }
        public int UserId { get; set; }
        public string Customer { get; set; }
        public string Version { get; set; }
        public String Saved { get; set; }

        public String LTBDate { get; set; }
        public String EOSDate { get; set; }
        [RegularExpression(@"^([2-9]|[1-9][0-9]|[1-2][0-9][0-9]|3[0-6][0-5])$", ErrorMessage = "Must be within 2 and 365")]
        public int RepairLeadTime { get; set; }
        public string ConfidenceLevel { get; set; }
        [DisplayName(" ")]
        public Boolean RepairPossible { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB0
        {
            get { return GetValue(() => IB0); }
            set { SetValue(() => IB0, value); }
        }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB1 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB2 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB3 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB4 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB5 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB6 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB7 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB8 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public String IB9 { get; set; }
        public String IB10 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR0 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR1 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR2 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR3 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR4 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR5 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR6 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR7 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR8 { get; set; }
        [RegularExpression(@"^([1-9]|[1-9][0-9]|100|[0-9][.,][0-9]{0,4}[1-9])$", ErrorMessage = "Must be within 0.00001 and 100")]
        public String FR9 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS0 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS1 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS2 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS3 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS4 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS5 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS6 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS7 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS8 { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,3}|EoS)$", ErrorMessage = "Must be within 0 and 9999")]
        public String RS9 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL0 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL1 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL2 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL3 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL4 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL5 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL6 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL7 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL8 { get; set; }
        [RegularExpression(@"^([0]|[1-9]|[1-9][0-9]|100)$", ErrorMessage = "Must be within 0 and 100")]
        public String RL9 { get; set; }
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
        public string InfoText
        {
            get { return GetValue(() => InfoText); }
            set { SetValue(() => InfoText, value); }
        }
        public long[] StockYearArray = new long[LTBCommon.MaxYear + 1];
        public long[] RSYearArray = new long[LTBCommon.MaxYear + 1];
        public long[] SafetyYearArray = new long[LTBCommon.MaxYear + 1];

    }
}
