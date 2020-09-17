﻿using System;
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

        private LoginViewModel loginViewModel;

        public LoginView()
        {
            this.InitializeComponent();
        }

        //private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        //{
        //    loginViewModel = new LoginViewModel();
        //}


        private void Bttn_Abort_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private async void Bttn_LogIn_Click(object sender, RoutedEventArgs e)
        {

            await LoginViewModel.Instance.GetUser(Tb_EnterUsername.Text, Pwb_EnterPassword.Password);
            if (LoginViewModel.Instance.ActiveUser!=null)
            {
                Hide();
            }
                       
        }        
    }
}
