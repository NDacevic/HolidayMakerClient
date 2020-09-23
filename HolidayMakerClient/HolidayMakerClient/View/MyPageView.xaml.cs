using HolidayMakerClient.Model;
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
        LoginViewModel loginViewModel;
        MyPageViewModel myPageViewModel = MyPageViewModel.Instance;
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
            loginViewModel = LoginViewModel.Instance;
        }

        private void Lv_MyReservations_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Reservation selectedReservation = (Reservation)Lv_MyReservations.SelectedItem;
            MyPageViewModel.Instance.SelectedUserReservation(selectedReservation);       
        }
        
        /// <summary>
        /// Shows a ContentDialog asking if you're sure you want to cancel the booking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void bttn_CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog deleteReservation = new ContentDialog()
            {
                Title = "Avboka", Content = "Är du säker på att du vill avboka vald reservation ? ", PrimaryButtonText = "Ok", CloseButtonText = "Avbryt"
            };
            ContentDialogResult result = await deleteReservation.ShowAsync();

            if (result == ContentDialogResult.Primary) //If they are ok we send the users id forward for deletion
            {
                MyPageViewModel.Instance.DeleteReservation((Reservation)Lv_MyReservations.SelectedItem);
            }
        }

        /// <summary>
        /// After determining that a reservation is selected. Navigates to the SelectedLivingView with the chosen reservation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void bttn_ChangeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (Lv_MyReservations.SelectedItem == null)
            {
                await new MessageDialog("Vänligen välj vilken reservation du vill ändra.").ShowAsync();
            }
            else
            {
                TempReservation currentReservation = new TempReservation
                {
                    TempHome = MyPageViewModel.Instance.SelectedHome[0],
                    OldReservation = MyPageViewModel.Instance.SelectedReservation[0]
                };
                PopulateList(currentReservation);

            }
        }  
        private void PopulateList(TempReservation currentReservation)
        {
            currentReservation.OldReservation.AddonList = new List<Addon>();
            foreach (Addon a in myPageViewModel.SelectedReservationAddons)
            {
                currentReservation.OldReservation.AddonList.Add(a);
            }
            Frame.Navigate(typeof(SelectedLivingView), currentReservation);

        }

        private void bttn_navigateBack_Click(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack==true)
            {
                Frame.GoBack();
            }
        }

        private async void bttn_RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            await new DeleteUserView().ShowAsync();
            if (LoginViewModel.Instance.ActiveUser == null)
            {
                Frame.Navigate(typeof(SearchView));
            }
        }

        private void bttn_UploadLiving_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UploadLivingView));
        }
       

        private async void bttn_RemoveLiving_Click(object sender, RoutedEventArgs e)
        {
            if ((Home)Lv_MyUploadedLiving.SelectedItem == null)
            {
                await new MessageDialog("Vänligen välj ett boende du vill ta bort").ShowAsync();
            }
            else
            {
                MyPageViewModel.Instance.DeleteHome((Home)Lv_MyUploadedLiving.SelectedItem);

            }  
        }


        #endregion


    }
}
