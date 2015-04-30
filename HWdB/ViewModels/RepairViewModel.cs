﻿namespace HWdB.ViewModels
{
    class RepairViewModel:ViewModelBase
    {
        string _buttonName;
        public override string ButtonName
        {
            get
            {
                return _buttonName;
            }
            set
            {
                _buttonName = value;
            }
        }

        public RepairViewModel()
        {
            this.ButtonName = "Repair";
        }

        protected override void OnDispose()
        {
        }
    }
}
