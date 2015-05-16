using HWdB.Notification;
using HWdB.Properties;
using System.ComponentModel.DataAnnotations;

namespace HWdB.Model
{
    public class BaseActivatable : PropertyChangedNotification
    {
        [UIHint("Active")]
        [Display(Name = "Active", ResourceType = typeof(Strings))]
        public string active { get; set; }

        public BaseActivatable()
        {
            active = "";
        }

        public BaseActivatable(BaseActivatable that)
        {
            this.active = that.active;
        }

        public string ActiveAsString
        {
            get
            {
                return (active == "1") ? Properties.Strings.Yes : Properties.Strings.No;
            }
        }

        public bool IsActive()
        {
            return (active == "1");
        }

        public void Clone(BaseActivatable that)
        {
            this.active = that.active;
        }
    }
}