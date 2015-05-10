using System;
using System.ComponentModel.DataAnnotations;

namespace HWdB.ViewModels
{
    class LTBViewModel : ViewModelBase
    {
        public override string ButtonName { get; set; }
        public string ConfidenceLevel { get; set; }
        public string RepairLeadTime { get; set; }
        [RegularExpression(@"^([0]|[1-9][0-9]{0,4}|EoS)$", ErrorMessage = "Must be within 0 and 99999")]
        public string IB0
        {
            get { return GetValue(() => IB0); }
            set { SetValue(() => IB0, value); }
        }
        public string IB1 { get; set; }
        public string IB2 { get; set; }
        public string IB3 { get; set; }
        public string IB4 { get; set; }
        public string IB5 { get; set; }
        public string IB6 { get; set; }
        public string IB7 { get; set; }
        public string IB8 { get; set; }
        public string IB9 { get; set; }

        string ltbDate = DateTime.Now.ToString();
        public string LTBDate
        {
            get { return this.ltbDate; }
            set
            {
                if (this.ltbDate == value)
                    return;

                this.ltbDate = value;
                OnPropertyChanged("LTBDate");
            }
        }

        string eosDate = DateTime.Now.AddYears(10).ToString();
        public string EOSDate
        {
            get { return this.eosDate; }
            set
            {
                if (this.eosDate == value)
                    return;

                this.eosDate = value;
                OnPropertyChanged("EOSDate");
            }
        }
        public LTBViewModel()
        {
            this.ButtonName = "LTB";
            this.ConfidenceLevel = "60%";
            this.RepairLeadTime = "40";
            this.LTBDate = DateTime.Now.ToString();
            this.EOSDate = DateTime.Now.AddYears(10).ToString();
            IB0 = "9876";
        }

        bool isYes = true;

        public bool IsYes
        {
            get { return this.isYes; }
            set
            {
                if (this.isYes == value)
                    return;

                this.isYes = value;
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
    }
}
