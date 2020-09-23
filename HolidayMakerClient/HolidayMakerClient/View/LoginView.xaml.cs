using HolidayMakerClient.Model;
using HolidayMakerClient.View;
using HolidayMakerClient.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HolidayMakerClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView //: ContentDialog
    {
        public LoginView()
        {
            this.InitializeComponent();
        }

        private async void Bttn_Register_Click(object sender, RoutedEventArgs e)
        {
            Vw_LoginPage.Hide();
            await new RegisterAccountView().ShowAsync();
        }

        private void Bttn_Abort_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// Hashes the iput password and gets the user from the database. Checking if the passwords match
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Bttn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            var load = new LoadDataView();
            Hide();
            _ = load.ShowAsync();

            string encryptedPassword = PasswordHelper.EncryptPassword(Pwb_EnterPassword.Password);
            await LoginViewModel.Instance.GetUser(Tb_EnterUsername.Text, encryptedPassword);

            load.Hide();
            
            if (LoginViewModel.Instance.ActiveUser!=null)
            {
                Hide();
            }
            else
                await ShowAsync();

        }        

    }
}
