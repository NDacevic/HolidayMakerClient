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
    public sealed partial class SearchView : Page
    {
        #region Constant Fields
        #endregion

        #region Fields
        private ObservableCollection<Home> testSearchList;
        SearchViewModel searchViewModel;
        TempReservation tempReservation;
        #endregion

        #region Constructors
        public SearchView()
        {
            this.InitializeComponent();
            testSearchList = new ObservableCollection<Home>();
            PopulateListView();
            searchViewModel = new SearchViewModel();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void ShowHideAdvancedSearch(object sender, RoutedEventArgs args)
        {
            if (stckPnl_AdvancedSearch.Visibility == Visibility.Collapsed)
                stckPnl_AdvancedSearch.Visibility = Visibility.Visible;
            else
                stckPnl_AdvancedSearch.Visibility = Visibility.Collapsed;
        }
        private void Search(object sender, RoutedEventArgs args)
        {
            int.TryParse(txtBox_NumberOfGuests.Text, out int numberOfGuests);
            searchViewModel.Search
                (
                txtBox_Search.Text,
                (DateTimeOffset)datePicker_StartDate.Date,
                (DateTimeOffset)datePicker_EndDate.Date,
                numberOfGuests
                );
        }
        private void PopulateListView()
        {
            for (int i = 0; i < 10; i++)
            {
                testSearchList.Add(new Home(1, "Hotel", i, "Sweden", 3299, true, false, true, "ms-appx:///Assets/hotelroom.jpg", true, false, false, true, 10, 5, i, true, false, "An awesome hotelroom", 15, 85));
            }
        }

        private void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            CreateTempRes();
            Frame.Navigate(typeof(SelectedLivingView), tempReservation);
        }
        public void CreateTempRes ()
        {
            tempReservation = new TempReservation();
            SetDates();
            tempReservation.NumberOfGuests = txtBox_NumberOfGuests.Text;
            tempReservation.Home = (Home)lv_SearchList.SelectedItem;
        }
        public void SetDates()
        {
            tempReservation.StartDate = (DateTimeOffset)datePicker_StartDate.Date;
            tempReservation.EndDate = (DatetimeOffset)datePicker_EndDate.Date;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            await new LoginView().ShowAsync();
            bttn_Login.Visibility = Visibility.Collapsed;
            bttn_UserOptions.Visibility = Visibility.Visible;
        }
        private void NavigateToMyPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyPageView));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion
    }
}
