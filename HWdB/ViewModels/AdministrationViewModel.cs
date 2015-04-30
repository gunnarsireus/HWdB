using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWdB.ViewModels
{
    class AdministrationViewModel:ViewModelBase
    {
        string _buttonName;
        public override string ButtonName
        {
            get
            {
                return _buttonName;
            }
            set
            {
                _buttonName = value;
            }
        }

        public AdministrationViewModel()
        {
            this.ButtonName = "Administration";
        }

        protected override void OnDispose()
        {
        }
    }
}
