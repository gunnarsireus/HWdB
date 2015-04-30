using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HWdB.Properties;

namespace HWdB.Model
{
    public class BaseActivatable 
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