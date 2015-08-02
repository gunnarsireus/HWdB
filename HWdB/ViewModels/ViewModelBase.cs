using HWdB.MVVMFramework;
using System;
using System.ComponentModel;

namespace HWdB.ViewModels
{
    public abstract class BaseViewModel : PropertyChangedNotification, INotifyPropertyChanged, IDisposable
    {
        protected BaseViewModel()
        {
            ButtonName = "";
        }

        public virtual string ButtonName { get; set; }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
        public virtual void Dispose()
        {
            this.OnDispose();
        }
        protected virtual void OnDispose()
        {

        }
    }
}
