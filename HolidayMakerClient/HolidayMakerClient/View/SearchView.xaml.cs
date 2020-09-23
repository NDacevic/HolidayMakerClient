using HolidayMakerClient.Model;
using HolidayMakerClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography.Core;
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

        private DateTimeOffset searchedStartDate;
        private DateTimeOffset searchedEndDate;
        #endregion

        #region Constructors
        public SearchView()
        {
            this.InitializeComponent();
            searchViewModel = new SearchViewModel();
            datePicker_StartDate.Date = DateTime.Now;
            datePicker_EndDate.Date = DateTime.Now.AddDays(1.0);

            for (int i = 1; i <= 20; i++)
            {
                comboBox_NumberOfGuests.Items.Add(i);
            }

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
            if (grid_AdvancedSearch.Visibility == Visibility.Collapsed)
                grid_AdvancedSearch.Visibility = Visibility.Visible;
            else
                grid_AdvancedSearch.Visibility = Visibility.Collapsed;
        }
        private void SearchButton_Clicked(object sender, RoutedEventArgs args)
        {
            Search();
            searchViewModel.ClearSortGlyphs(stackPanel_SortButtons);
        }

        private async void Search()
        {
            try
            {
                int.TryParse(comboBox_NumberOfGuests.SelectedValue.ToString(), out int numberOfGuests);

                searchedStartDate = (DateTimeOffset)datePicker_StartDate.Date;
                searchedEndDate = (DateTimeOffset)datePicker_EndDate.Date;

                if (string.IsNullOrEmpty(txtBox_Search.Text))
                    throw new FormatException("Skriv in ett sökord");

                var date = (((DateTimeOffset)datePicker_EndDate.Date).Subtract((DateTimeOffset)datePicker_StartDate.Date)).Days;

                if (date == 0)
                    throw new FormatException(@"'Från'- och 'Till-datum' får ej vara samma värde");
                else if (date < 0)
                    throw new FormatException(@"'Till-datum' får ej vara före 'Från-datum'");

                var load = new LoadDataView();

                load.ShowAsync();

                await searchViewModel.Search
                    (
                    txtBox_Search.Text,
                    searchedStartDate,
                    searchedEndDate,
                    numberOfGuests,
                    CreateAdvancedFilterParams(),
                    grid_AdvancedSearch
                    );

                load.Hide();
                DetermineSearchVisibility();
            }
            catch(Exception e)
            {
                await new MessageDialog(e.Message).ShowAsync();
            }
        }

        /// <summary>
        /// Collects the information on all the advanced search toggleswitches and sliders.
        /// </summary>
        /// <returns></returns>
        private Home CreateAdvancedFilterParams()
        {
            Home advancedFilterParams = new Home()
            {
                AllowPets = toggle_AllowPets.IsOn,
                AllowSmoking = toggle_AllowSmoking.IsOn,
                HasBalcony = toggle_HasBalcony.IsOn,
                HasPool = toggle_HasPool.IsOn,
                HasWifi = toggle_HasWifi.IsOn,
                CityDistance = (int)slider_CityDistance.Value,
                BeachDistance = (int)slider_BeachDistance.Value
            };
            return advancedFilterParams;
        }


        private void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            CreateTempRes();
            Frame.Navigate(typeof(SelectedLivingView), tempReservation);
        }

        /// <summary>
        /// Creates a temporary reservation object for sending to the SelectedLivingView
        /// </summary>
        public void CreateTempRes ()
        {
            tempReservation = new TempReservation();
            SetDates();
            tempReservation.NumberOfGuests = comboBox_NumberOfGuests.SelectedValue.ToString();
            tempReservation.TempHome = (Home)listView_SearchList.SelectedItem;
        }

        public void SetDates()
        {
            tempReservation.StartDate = searchedStartDate;
            tempReservation.EndDate = searchedEndDate;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            await new LoginView().ShowAsync();
            CheckActiveUser();
                
        }

        private void CheckActiveUser()
        {
            if (LoginViewModel.Instance.ActiveUser != null)
            {
                bttn_Login.Visibility = Visibility.Collapsed;
                bttn_UserOptions.Visibility = Visibility.Visible;
            }
            else
            {
                //This happens after a user has removed itself from the system or logs out
                bttn_Login.Visibility = Visibility.Visible;
                bttn_UserOptions.Visibility = Visibility.Collapsed;
            }
        }

        private async void NavigateToMyPage_Click(object sender, RoutedEventArgs e)
        {
            //Contact API and populate MyReservations and ActiveUserHomes properties before navigating to the page
            await MyPageViewModel.Instance.GetActiveUserHomes();
            await MyPageViewModel.Instance.GetReservations();
            Frame.Navigate(typeof(MyPageView));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginViewModel.Instance.ActiveUser = null;
            CheckActiveUser();
        }

        private void SortColumns_Click(object sender, RoutedEventArgs e)
        {
            searchViewModel.SortColumns((Button)sender, stackPanel_SortButtons);
        }
        
        private void SearchKeydown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                Search();
        }

        private void RefreshSearch(object sender, RoutedEventArgs e)
        {
            searchViewModel.Filter(CreateAdvancedFilterParams(), grid_AdvancedSearch);
            searchViewModel.ClearSortGlyphs(stackPanel_SortButtons);
            DetermineSearchVisibility();
        }

        private void DatePickers_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs e)
        {
            if (e.Item.Date < DateTime.Today)
            {
                e.Item.IsBlackout = true;

            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CheckActiveUser();
        }

        private void DetermineSearchVisibility()
        {
            if (searchViewModel.SortedHomeList.Count > 0)
            {
                scrollViewer_SearchResults.Visibility = Visibility.Visible;
                textBlock_NoResults.Visibility = Visibility.Collapsed;
            }
            else
            {
                scrollViewer_SearchResults.Visibility = Visibility.Collapsed;
                textBlock_NoResults.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
