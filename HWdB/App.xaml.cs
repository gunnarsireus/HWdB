using System;
using System.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HWdB.ViewModels;
using HWdB.DataAccess;

namespace HWdB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
 
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            using (var db = new DataContext()) {
                 db.Database.CreateIfNotExists();
            }
            base.OnStartup(e);
            ApplicationView app = new ApplicationView();
            ApplicationViewModel context = new ApplicationViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}
