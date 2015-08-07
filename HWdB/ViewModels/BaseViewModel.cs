using HWdB.MVVMFramework;
using System;

namespace HWdB.ViewModels
{
    public abstract class BaseViewModel : PropertyChangedNotification, IDisposable
    {
        public abstract string ButtonName { get; set; }

        public virtual void Dispose()
        {
            OnDispose();
        }
        protected virtual void OnDispose()
        {

        }
    }
}
