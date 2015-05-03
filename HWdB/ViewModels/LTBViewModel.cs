namespace HWdB.ViewModels
{
    class LTBViewModel:ViewModelBase
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

        public LTBViewModel()
        {
             this.ButtonName = "LTB";
        }

        protected override void OnDispose()
        {
        }
    }
}
