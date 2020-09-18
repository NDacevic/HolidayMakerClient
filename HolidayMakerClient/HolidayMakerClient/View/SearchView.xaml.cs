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
    public sealed partial class SearchView : Page
    {
        #region Constant Fields
        #endregion

        #region Fields
        SearchViewModel searchViewModel;
        TempReservation tempReservation;
        #endregion

        #region Constructors
        public SearchView()
        {
            this.InitializeComponent();
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
            //TODO: Add error handling when search parameters are empty //MO
            int.TryParse(txtBox_NumberOfGuests.Text, out int numberOfGuests);
            searchViewModel.Search
                (
                txtBox_Search.Text,
                (DateTimeOffset)datePicker_StartDate.Date,
                (DateTimeOffset)datePicker_EndDate.Date,
                numberOfGuests
                );
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
            tempReservation.EndDate = (DateTimeOffset)datePicker_EndDate.Date;
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

        private void SortColumns_Click(object sender, RoutedEventArgs e)
        {
            if (sender == bttn_SortLocation)
            {
                if (fontIcon_SortLocation.Glyph == "\uE96E")
                    fontIcon_SortLocation.Glyph = "\uE96D";
                else
                    fontIcon_SortLocation.Glyph = "\uE96E";

                searchViewModel.SortList("location");

                fontIcon_SortPrice.Glyph = "";
                fontIcon_SortRooms.Glyph = "";
            }
            else if (sender == bttn_SortPrice)
            {
                if (fontIcon_SortPrice.Glyph == "\uE96E")
                    fontIcon_SortPrice.Glyph = "\uE96D";
                else
                    fontIcon_SortPrice.Glyph = "\uE96E";

                fontIcon_SortLocation.Glyph = "";
                fontIcon_SortRooms.Glyph = "";
            }
            else if (sender == bttn_SortRooms)
            {
                if (fontIcon_SortRooms.Glyph == "\uE96E")
                    fontIcon_SortRooms.Glyph = "\uE96D";
                else
                    fontIcon_SortRooms.Glyph = "\uE96E";

                fontIcon_SortLocation.Glyph = "";
                fontIcon_SortPrice.Glyph = "";
            }
        }
        #endregion
    }
}
