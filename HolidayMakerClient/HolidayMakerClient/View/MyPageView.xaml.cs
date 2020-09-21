﻿using HolidayMakerClient.Model;
using HolidayMakerClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class MyPageView : Page
    {
        #region Constant Fields
        #endregion

        #region Fields
        MyPageViewModel myPageViewModel = new MyPageViewModel();
        LoginViewModel loginViewModel = LoginViewModel.Instance;
        #endregion

        #region Constructors
        public MyPageView()
        {
            this.InitializeComponent();
            this.DataContext = myPageViewModel;

        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties


        #endregion

        #region Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Todo: Jag tror denna passar bättre i en PageLoaded-metod, annars är det risk att din metoden försöker populera din lista innan din Listview laddat färdigt. //MO
            myPageViewModel.GetReservations();
         
        }
        private void Lv_MyReservations_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Reservation selectedReservation = (Reservation)Lv_MyReservations.SelectedItem;
            myPageViewModel.SelectedUserReservation(selectedReservation);       
        }

        private async void bttn_CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog deleteReservation = new ContentDialog()
            {
                Title = "Avboka", Content = "Är du säker på att du vill avboka vald reservation ? ", PrimaryButtonText = "Ok", CloseButtonText = "Avbryt"
            };
            ContentDialogResult result = await deleteReservation.ShowAsync();

            if (result == ContentDialogResult.Primary) //If they are ok we send the users id forward for deletion
            {
                myPageViewModel.DeleteReservation((Reservation)Lv_MyReservations.SelectedItem);
            }

            
        }

        private async void bttn_ChangeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (Lv_MyReservations.SelectedItem == null)
            {
                await new MessageDialog("Vänligen välj vilken reservation du vill ändra.").ShowAsync();
            }
            else
            {
                Reservation reservation = (Reservation)Lv_MyReservations.SelectedItem;
                TempReservation currentReservation = new TempReservation
                {
                    TempId = reservation.ReservationId,
                    NumberOfGuests = reservation.NumberOfGuests.ToString(),
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    TotalPrice = reservation.TotalPrice,
                    TempHome = await myPageViewModel.GetHome(reservation.HomeId),
                    AddonList = new ObservableCollection<Addon>()
                };
                if(reservation.AddonList.Count != 0)
                {
                    foreach (var ad in reservation.AddonList) currentReservation.AddonList.Add(ad);
                }
                Frame.Navigate(typeof(SelectedLivingView), currentReservation);
            }
        }        

        private void bttn_navigateBack_Click(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack==true)
            {
                Frame.GoBack();
            }
        }

        #endregion

    }
}
