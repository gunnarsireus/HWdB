
namespace HWdB.ViewModels
{
    class SupplyViewModel:ViewModelBase
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

        public SupplyViewModel()
        {
            this.ButtonName = "Supply";
        }

        protected override void OnDispose()
        {
        }
    }
}
