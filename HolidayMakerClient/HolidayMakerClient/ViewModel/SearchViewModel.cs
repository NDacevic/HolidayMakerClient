using HolidayMakerClient.Dto;
using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HolidayMakerClient.ViewModel
{
    public class SearchViewModel
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public SearchViewModel()
        {
            HomeList = new ObservableCollection<Home>();
            SortedHomeList = new ObservableCollection<Home>();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public bool IsAscending { get; set; }
        public bool SortingAttributes { get; set; }
        public ObservableCollection<Home> HomeList { get; }
        public ObservableCollection<Home> SortedHomeList { get; }
        public List<FontIcon> FontIconList = new List<FontIcon>();
        #endregion

        #region Methods
        /// <summary>
        /// TBD
        /// </summary>
        public async void Search(string searchString, DateTimeOffset startDate, DateTimeOffset endDate, int numberOfGuests, bool AllFalseFilter, Home advancedFilterParams)
        {
            SearchParameterDto searchObj = new SearchParameterDto(searchString, startDate, endDate, numberOfGuests);
            var homeList = await ApiHelper.Instance.GetSearchResults(searchObj);
            
            HomeList.Clear();
            SortedHomeList.Clear();

            foreach (var x in homeList)
            {
                x.AverageRating = ((double)x.SumOfRatings / (double)x.NumberOfRatings);

                HomeList.Add(x);
                SortedHomeList.Add(x);
            }

            if(!AllFalseFilter)
                Filter(advancedFilterParams);
        }
        /// <summary>
        /// TBD
        /// </summary>
        public void GetHomes()
        {

        }
        /// <summary>
        /// TBD
        /// </summary>
        public void Filter(Home advancedFilterParams)
        {
            SortedHomeList.Clear();


            var filteredHomeList = HomeList.Where(x =>
            x.AllowPets == advancedFilterParams.AllowPets &&
            x.AllowSmoking == advancedFilterParams.AllowSmoking &&
            x.HasBalcony == advancedFilterParams.HasBalcony &&
            x.HasPool == advancedFilterParams.HasPool &&
            x.HasWifi == advancedFilterParams.HasWifi);

            // x.CityDistance <= advancedFilterParams.CityDistance
            //x.BeachDistance <= advancedFilterParams.BeachDistance

            foreach (var x in filteredHomeList)
                SortedHomeList.Add(x);
        }

        public void ClearFilter()
        {
            SortedHomeList.Clear();
            foreach (var home in HomeList)
                SortedHomeList.Add(home);
        }

        public void SortList(string parameter)
        {
            SortedHomeList.Clear();
            IOrderedEnumerable<Home> orderedList;

            switch (parameter)
            {
                case "bttn_SortLocation":
                    if (IsAscending)
                        orderedList = HomeList.OrderBy(x => x.Location);
                    else
                        orderedList = HomeList.OrderByDescending(x => x.Location);

                    foreach (var x in orderedList)
                        SortedHomeList.Add(x);
                    break;

                case "bttn_SortPrice":
                    if(IsAscending)
                        orderedList = HomeList.OrderBy(x => x.Price);
                    else
                        orderedList = HomeList.OrderByDescending(x => x.Price);

                    foreach (var x in orderedList)
                        SortedHomeList.Add(x);
                    break;

                case "bttn_SortRooms":
                    if (IsAscending)
                        orderedList = HomeList.OrderBy(x => x.Rooms);
                    else
                        orderedList = HomeList.OrderByDescending(x => x.Rooms);

                    foreach (var x in orderedList)
                        SortedHomeList.Add(x);
                    break;

                case "bttn_SortBeds":
                    if (IsAscending)
                        orderedList = HomeList.OrderBy(x => x.NumberOfBeds);
                    else
                        orderedList = HomeList.OrderByDescending(x => x.NumberOfBeds);

                    foreach (var x in orderedList)
                        SortedHomeList.Add(x);
                    break;

                case "bttn_SortCityDistance":
                    if (IsAscending)
                        orderedList = HomeList.OrderBy(x => x.CityDistance);
                    else
                        orderedList = HomeList.OrderByDescending(x => x.CityDistance);

                    foreach (var x in orderedList)
                        SortedHomeList.Add(x);
                    break;

                case "bttn_SortBeachDistance":
                    if (IsAscending)
                        orderedList = HomeList.OrderBy(x => x.BeachDistance);
                    else
                        orderedList = HomeList.OrderByDescending(x => x.BeachDistance);

                    foreach (var x in orderedList)
                        SortedHomeList.Add(x);
                    break;

                case "bttn_SortAverageRating":
                    if (IsAscending)
                        orderedList = HomeList.OrderBy(x => x.AverageRating);
                    else
                        orderedList = HomeList.OrderByDescending(x => x.AverageRating);

                    foreach (var x in orderedList)
                        SortedHomeList.Add(x);
                    break;
            }

        }
        
        public void SortColumns(Button sortButton)
        {
            //Find the child of type FontIcon to the button
            FontIcon glyph = FindFontIconChild((DependencyObject)sortButton);

            //Fill the glyph property of the fonticon. Either an up-arrow or down arrow
            //and set if the sorting is ascending or descending
            if ((glyph).Glyph == "\uE96E")
            {
                (glyph).Glyph = "\uE96D";
                IsAscending = false;
            }
            else
            {
                (glyph).Glyph = "\uE96E";
                IsAscending = true;
            }

            //Clear the arrow from any button except the clicked one
            ClearGlyphs(glyph);
            //Sort the list
            SortList(((FrameworkElement)sortButton).Name);
        }

        private void ClearGlyphs(FontIcon fontIcon)
        {
            //Go through the list of fonticons. if the fonticon is the one in the parameter list.
            //do nothing with it.
            foreach(var fi in FontIconList)
                if (fontIcon != fi)
                    fi.Glyph = "";
        }
        public FontIcon FindFontIconChild(DependencyObject parent)
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

        public void FindAdvancedSearchElements(List<Control> list, DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child.GetType() == typeof(ToggleSwitch) ||
                    child.GetType() == typeof(Slider))
                    list.Add((Control)child);
                else
                    FindAdvancedSearchElements(list, child);
            }
        }

        public bool AllFalseAdvancedSearch(DependencyObject uiElement)
        {
            List<Control> controls = new List<Control>();
            FindAdvancedSearchElements(controls, uiElement);

            bool allFalse = true;

            foreach (var x in controls)
            {
                if (x.GetType() == typeof(ToggleSwitch))
                    if (((ToggleSwitch)x).IsOn)
                        allFalse = false;
                if (x.GetType() == typeof(Slider))
                    if (((Slider)x).Value != 0)
                        allFalse = false;
            }

            return allFalse;
        }
        #endregion
    }
}
