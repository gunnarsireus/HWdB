
namespace HWdB.ViewModels
{
    class SupplyViewModel : BaseViewModel
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
