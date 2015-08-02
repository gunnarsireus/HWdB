
namespace HWdB.ViewModels
{
    class StrategyViewModel : BaseViewModel
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

        public StrategyViewModel()
        {
            this.ButtonName = "Strategies";
        }

        protected override void OnDispose()
        {
        }
    }
}
