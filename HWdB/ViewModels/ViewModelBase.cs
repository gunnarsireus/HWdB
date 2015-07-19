using HWdB.Notification;
using System;
using System.ComponentModel;

namespace HWdB.ViewModels
{
    public abstract class ViewModelBase : PropertyChangedNotification, INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase()
        {
            ButtonName = "";
        }

        public virtual string ButtonName { get; set; }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
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
