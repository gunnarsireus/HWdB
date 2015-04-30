using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HWdB.Model
{
    public class StatusMessage
    {
        public bool   success { get; set; }
        public string message { get; set; }
        public string nextURL { get; set; }

        public StatusMessage()
        {
            success = true;
            message = "";
            nextURL = "";
        }
    }
}
