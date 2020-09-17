using HolidayMakerClient.Model;
using HolidayMakerClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        }
        private void Search()
        {
            //TODO: Add error handling when search parameters are empty //MO
            int.TryParse(comboBox_NumberOfGuests.SelectedValue.ToString(), out int numberOfGuests);

            bool advancedSearchActive = grid_AdvancedSearch.Visibility == Visibility.Visible ? true : false;

            searchViewModel.Search
                (
                txtBox_Search.Text,
                (DateTimeOffset)datePicker_StartDate.Date,
                (DateTimeOffset)datePicker_EndDate.Date,
                numberOfGuests,
                advancedSearchActive,
                CreateAdvancedFilterParams()
                );
        }
        private void UpdateFilter(object obj, RoutedEventArgs e)
        {
           
            searchViewModel.Filter(CreateAdvancedFilterParams());
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
            tempReservation.NumberOfGuests = comboBox_NumberOfGuests.SelectedValue.ToString();
            tempReservation.Home = (Home)lv_SearchList.SelectedItem;
        }
        public void SetDates()
        { 
            tempReservation.StartDate = new DateTime(datePicker_StartDate.Date.Value.Year, datePicker_StartDate.Date.Value.Month, datePicker_StartDate.Date.Value.Day);
            tempReservation.EndDate = new DateTime(datePicker_EndDate.Date.Value.Year, datePicker_EndDate.Date.Value.Month, datePicker_EndDate.Date.Value.Day);
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

            FontIcon glyph = FindFontIconChild((DependencyObject)sender);
            if ((glyph).Glyph == "\uE96E")
            {
                (glyph).Glyph = "\uE96D";
                searchViewModel.IsAscending = false;
            }
            else
            {
                (glyph).Glyph = "\uE96E";
                searchViewModel.IsAscending = true;
            }

            ClearGlyphs(glyph);
            searchViewModel.SortList(((FrameworkElement)sender).Name);
        }
        private void ClearGlyphs(FontIcon fontIcon)
        {
            if(fontIcon != fontIcon_SortLocation)
                fontIcon_SortLocation.Glyph = "";

            if (fontIcon != fontIcon_SortPrice)
                fontIcon_SortPrice.Glyph = "";

            if (fontIcon != fontIcon_SortRooms)
                fontIcon_SortRooms.Glyph = "";
        }
        private FontIcon FindFontIconChild(DependencyObject parent)
        {
            //If the font icon is found then we just exit the method with the object in hand.
            if (parent is FontIcon)
                return (FontIcon)parent;
            
            //create a fonticon variable.
            FontIcon foundFontIcon = null;
            
            //go through the children of the visual element and see if one of them is of the type FontIcon
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is FontIcon)
                    foundFontIcon = (FontIcon)child;
                else
                    //If the icon isn't found. Recurse through this method again
                    foundFontIcon = FindFontIconChild(child);
            }
            //return with the fonticon
            return foundFontIcon;
        }
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
        #endregion

        private void SearchKeydown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                Search();
        }
    }
}
