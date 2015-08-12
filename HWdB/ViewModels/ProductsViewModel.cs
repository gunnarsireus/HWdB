namespace HWdB.ViewModels
{
    class ProductsViewModel : BaseViewModel
    {
        public override sealed string Title { get; set; }

        public ProductsViewModel()
        {
            Title = "Products";
        }
    }
}
