namespace HWdB.ViewModels
{
    class ExportViewModel : BaseViewModel
    {
        public override sealed string ButtonName { get; set; }

        public ExportViewModel()
        {
            ButtonName = "Export";
        }
    }
}
