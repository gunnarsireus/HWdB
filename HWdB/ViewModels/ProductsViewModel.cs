namespace HWdB.ViewModels
{
    class ProductsViewModel : BaseViewModel
    {
        public override sealed string ButtonName { get; set; }

        public ProductsViewModel()
        {
            ButtonName = "Products";
        }

        protected override void OnDispose()
        {
        }
    }
}
