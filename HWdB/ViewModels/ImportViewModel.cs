namespace HWdB.ViewModels
{
    class ImportViewModel:ViewModelBase
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

        public ImportViewModel()
        {
            this.ButtonName = "Import";
        }

        protected override void OnDispose()
        {
        }
    }
}
