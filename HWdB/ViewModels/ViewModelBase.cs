using HWdB.MVVMFramework;
using System;
using System.ComponentModel;

namespace HWdB.ViewModels
{
    public abstract class BaseViewModel : PropertyChangedNotification, INotifyPropertyChanged, IDisposable
    {
        public abstract string ButtonName { get; set; }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
        public virtual void Dispose()
        {
            OnDispose();
        }
        protected virtual void OnDispose()
        {

        }
    }
}
