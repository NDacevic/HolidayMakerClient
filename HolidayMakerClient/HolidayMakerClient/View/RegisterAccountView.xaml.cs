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
    public sealed partial class RegisterAccountView : ContentDialog
    {
        #region Constant Fields
        #endregion

        #region Fields
        RegisterAccountViewModel regAccountVM;
        #endregion

        #region Constructors
        public RegisterAccountView()
        {
            this.InitializeComponent();
            regAccountVM = new RegisterAccountViewModel();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion

        private async void Bttn_Register_Click(object sender, RoutedEventArgs e)
        {
           
            bool userType = true;
            //TODO: Encrypt password
            try
            {
                if(Rb_Private.IsChecked==true)
                {
                    userType = false;
                }
               
                regAccountVM.CreateNewUser(Tb_FirstName.Text, Tb_LastName.Text, Tb_Email.Text, Pwb_Password1.Password, userType);

            }
            catch (Exception)
            {
                await new MessageDialog("Felaktig inmatning, var god fyll i alla fält.").ShowAsync();

            }
        }

        private void Bttn_Abort_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
