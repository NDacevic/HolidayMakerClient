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
            isUserDeleted = await deleteUserViewModel.ConfirmPassword(pwd_ConfirmPassword.Password);
            if (isUserDeleted)
                Hide();
      
        }

        private void bttn_Abort_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
