namespace HWdB.ViewModels
{
    class ProductGroupsViewModel:ViewModelBase
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

        public ProductGroupsViewModel()
        {
            this.ButtonName = "Product Groups";
        }

        protected override void OnDispose()
        {
        }
    }
}
