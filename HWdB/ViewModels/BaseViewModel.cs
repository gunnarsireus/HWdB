using HWdB.MVVMFramework;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace HWdB.ViewModels
{
    public abstract class BaseViewModel : PropertyChangedNotification, IDisposable
    {
        public abstract string Title { get; set; }

        // Flag: Has Dispose already been called? 
        private bool _disposed = false;
        // Instantiate a SafeHandle instance.
        private readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
                // Free any other managed objects here. 
                //
            }

            // Free any unmanaged objects here. 
            //
            _disposed = true;
        }
    }
}
