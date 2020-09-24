using HolidayMakerClient.Model;
using HolidayMakerClient.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace HolidayMakerClient.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeleteUserView : ContentDialog
    {
        private DeleteUserViewModel deleteUserViewModel = new DeleteUserViewModel();

        public DeleteUserView()
        {
            this.InitializeComponent();

        }

        private async void bttn_ConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            bool isUserDeleted;

            //Hash the input password and check if it matches the stored user password
            string encryptedPassword = PasswordHelper.EncryptPassword(pwd_ConfirmPassword.Password);
            //call the API and delete the user. Update the bool depending on success.
            isUserDeleted = await deleteUserViewModel.ConfirmPassword(encryptedPassword);
            //Hide this contentdialog if the user was removed.
            if (isUserDeleted)
                Hide();
      
        }

        private void bttn_Abort_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
