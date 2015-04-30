
namespace HWdB.ViewModels
{
    class StrategyViewModel:ViewModelBase
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
            this.ButtonName = "Strategy";
        }

        protected override void OnDispose()
        {
        }
    }
}
