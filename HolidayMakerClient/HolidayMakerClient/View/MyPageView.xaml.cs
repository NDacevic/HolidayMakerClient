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
            myPageViewModel.GetReservations();
           //TODO: GET user reservations and populate listview
        }
        private void Lv_MyReservations_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Reservation selectedReservation = (Reservation)Lv_MyReservations.SelectedItem;
            myPageViewModel.SelectedUserReservation(selectedReservation);
          //TODO: GET Home, populate Listviews Home/Reservation/Addon
        }

        private void bttn_CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Content dialog with "are you sure" Then proceed to delete.
        }

        private void bttn_ChangeReservation_Click(object sender, RoutedEventArgs e)
        {
            //TODO: goto SelectedLiving with the selected booking
        }
        #endregion


    }
}
