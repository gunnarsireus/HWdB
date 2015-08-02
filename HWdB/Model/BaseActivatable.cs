using HWdB.MVVMFramework;
using HWdB.Properties;
using System.ComponentModel.DataAnnotations;

namespace HWdB.Model
{
    public class BaseActivatable : PropertyChangedNotification
    {
        [UIHint("Active")]
        [Display(Name = "Active", ResourceType = typeof(Strings))]
        public string Active { get; set; }

        public BaseActivatable()
        {
            Active = "";
        }

        public BaseActivatable(BaseActivatable that)
        {
            this.Active = that.Active;
        }

        public string ActiveAsString
        {
            get
            {
                return (Active == "1") ? Properties.Strings.Yes : Properties.Strings.No;
            }
        }

        public bool IsActive()
        {
            return (Active == "1");
        }

        public void Clone(BaseActivatable that)
        {
            this.Active = that.Active;
        }
    }
}