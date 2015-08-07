namespace HWdB.ViewModels
{
    class ProductGroupsViewModel : BaseViewModel
    {
        public override sealed string ButtonName { get; set; }

        public ProductGroupsViewModel()
        {
            ButtonName = "Product Groups";
        }
    }
}
