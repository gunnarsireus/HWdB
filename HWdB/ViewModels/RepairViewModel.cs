namespace HWdB.ViewModels
{
    class RepairViewModel : BaseViewModel
    {
        public override sealed string ButtonName { get; set; }

        public RepairViewModel()
        {
            this.ButtonName = "Repair";
        }
    }
}
