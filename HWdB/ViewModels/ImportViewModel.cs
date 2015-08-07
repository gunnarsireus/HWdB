namespace HWdB.ViewModels
{
    class ImportViewModel : BaseViewModel
    {
        public override sealed string ButtonName { get; set; }

        public ImportViewModel()
        {
            ButtonName = "Import";
        }
    }
}
