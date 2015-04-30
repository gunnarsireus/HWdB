using System.Windows.Controls;
using HWdB.ViewModels;

namespace HWdB.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, IHavePassword
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public System.Security.SecureString Password
        {
            get
            {
                return UserPassword.SecurePassword;
            }
        }
    }
}
