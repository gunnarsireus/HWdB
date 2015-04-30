namespace HWdB.ViewModels
{
    class ProductsViewModel:ViewModelBase
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

        public ProductsViewModel()
        {
             this.ButtonName = "Products";
        }

        protected override void OnDispose()
        {
        }
    }
}
