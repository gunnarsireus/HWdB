namespace HWdB.ViewModels
{
    class ExportViewModel:ViewModelBase
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

        public ExportViewModel()
        {
            this.ButtonName = "Export";
        }

        protected override void OnDispose()
        {
        }
    }
}
