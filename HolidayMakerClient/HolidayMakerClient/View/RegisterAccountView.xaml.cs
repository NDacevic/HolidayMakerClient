﻿using HolidayMakerClient.Model;
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

        /// <summary>
        /// Calls the encrypt password and CreateNewUser methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Bttn_Register_Click(object sender, RoutedEventArgs e)
        {
            string encryptedPassword;
            bool isCompany = true;
            bool isCorrectEmailFormat;
            bool isCorrectPasswordLength;

            if (!CheckTextBoxes())
            {
                await new MessageDialog("Du måste fylla i alla fält").ShowAsync();
            }
            else
            {              
                if (Rb_Private.IsChecked == true)
                    isCompany = false;

                isCorrectEmailFormat = regAccountVM.ValidateEmail(Tb_Email.Text);
                isCorrectPasswordLength = regAccountVM.ValidatePassword(Pwb_Password1.Password);

                if (!isCorrectEmailFormat)
                {
                    await new MessageDialog("Angiven e-mail verkar inkorrekt. Säkerställ att du har angivit exakt ett '@' och minst en '.'").ShowAsync();
                }
                else if (!isCorrectPasswordLength)
                {
                    await new MessageDialog("Angivet lösenord behöver vara mellan 8 och 128 tecken. Vänligen välj ett nytt lösenord").ShowAsync();
                }
                else if (CheckPassword(Pwb_Password1.Password, Pwb_Password2.Password))
                {
                    var load = new LoadDataView();
                    Hide();
                    _ = load.ShowAsync();

                    encryptedPassword = PasswordHelper.EncryptPassword(Pwb_Password1.Password);

                    //make sure to only send what's in the surname text field if the registered user isn't a company.
                    //otherwise send an empty string
                    string surnameText = "";
                    if (!isCompany)
                        surnameText = Tb_LastName.Text;
                    
                    await regAccountVM.CreateNewUser(Tb_FirstName.Text, surnameText, Tb_Email.Text, encryptedPassword, isCompany);
                    load.Hide();
                    //Hide();
                }
                else
                {
                    await new MessageDialog("Lösenorden stämmer inte, var god kontrollera").ShowAsync();
                }
            }
        }

        private bool CheckTextBoxes()
        {
            if ((bool)Rb_Private.IsChecked)
            {
                if (Tb_FirstName.Text == "" || Tb_LastName.Text == "" || Tb_Email.Text == "" || Pwb_Password1.Password == "" || Pwb_Password2.Password == "")
                {
                    return false;
                }
                
            }
            else if ((bool)Rb_Business.IsChecked)
            {
                if (Tb_FirstName.Text == "" || Tb_Email.Text == "" || Pwb_Password1.Password == "" || Pwb_Password2.Password == "")
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckPassword(string pass1,string pass2)
        {
            if(pass1==pass2)
            {
                return true;
            }
            return false;
        }

        private void Bttn_Abort_Click(object sender, RoutedEventArgs e)
        {
            Vw_RegisterAccountPage.Hide();
        }

        private void ShowHideSurname(object sender, RoutedEventArgs e)
        {
            if (sender == Rb_Business)
            {
                Tb_LastName.Visibility = Visibility.Collapsed;
                textBlock_LastName.Visibility = Visibility.Collapsed;
                textBlock_Firstname.Text = "Företagsnamn:";
            }
            else if (sender == Rb_Private)
            {
                Tb_LastName.Visibility = Visibility.Visible;
                textBlock_LastName.Visibility = Visibility.Visible;
                textBlock_Firstname.Text = "Förnamn:";
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if(Rb_Private != null)
                Rb_Private.IsChecked = true;
        }

        #endregion
    }
}
