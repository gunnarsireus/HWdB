
namespace HWdB.Model
{
    public class StatusMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string NextUrl { get; set; }

        public StatusMessage()
        {
            Success = true;
            Message = "";
            NextUrl = "";
        }
    }
}
