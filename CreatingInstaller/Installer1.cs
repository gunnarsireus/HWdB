using Microsoft.Win32;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;


namespace CreatingInstaller
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
            //MessageBox.Show("Starting Check for SQL Server Express");
            RegistryKey Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions\11.0");
            if (Key == null)
            {
                //MessageBox.Show("Launching SQL Server Express installation");
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("SQL Server 2012 Express is not installed on this computer. Do you want to install it now?", "SQL Server Installation Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No) return;
                using (Process exeProcess = Process.Start("C:/Program Files (x86)/Siréus Consulting AB/LTBCalculation/SqlLocalDb.msi"))
                {
                    //MessageBox.Show("WaitForExit");
                    exeProcess.WaitForExit();
                }
                //MessageBox.Show("Exit");
            }
        }


        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            //Add custom code here
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            //Add custom code here
        }


        public override void Uninstall(IDictionary savedState)
        {
            Process application = null;
            foreach (var process in Process.GetProcesses())
            {
                if (!process.ProcessName.ToLower().Contains("creatinginstaller")) continue;
                application = process;
                break;
            }

            if (application != null && application.Responding)
            {
                application.Kill();
                base.Uninstall(savedState);
            }
        }

    }
}
