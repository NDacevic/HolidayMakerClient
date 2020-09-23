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

        #endregion

        #region Methods
        /// <summary>
        /// Takes all the parameters needed for a search.
        /// Clears the list of homes and then calls the method for sorting in case the user
        /// picked some advanced search options.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="numberOfGuests"></param>
        /// <param name="advancedFilterParams"></param>
        /// <param name="grid_AdvancedSearch"></param>
        public async Task Search(string searchString, DateTimeOffset startDate, DateTimeOffset endDate, int numberOfGuests, Home advancedFilterParams, DependencyObject grid_AdvancedSearch)
        {
            //Create a search object and send it to the API. Store the result in the homelist.
            SearchParameterDto searchObj = new SearchParameterDto(searchString, startDate, endDate, numberOfGuests);
            var homeList = await ApiHelper.Instance.GetSearchResults(searchObj);
            
            //empty the homelist so the listview removes all the items and updates itself.
            HomeList.Clear();

            //Calculate the average rating and add the home items to the HomeList property.
            foreach (var x in homeList)
            {
                if (x.NumberOfRatings != 0)
                    x.AverageRating = ((double)x.SumOfRatings / (double)x.NumberOfRatings);
                else
                    x.AverageRating = 0;
                HomeList.Add(x);
            }
            
            //Call the Filter method and send the advanced search parameters.  
            Filter(advancedFilterParams, grid_AdvancedSearch);
        }

        /// <summary>
        /// Applies a filter depending on what parameters the user chose.
        /// If everything is false, no search is applied.
        /// Distance parameters are applied if they aren't 0
        /// </summary>
        /// <param name="advancedFilterParams"></param>
        /// <param name="grid"></param>
        public void Filter(Home advancedFilterParams, DependencyObject grid)
        {
            //Clear the sorted list and initilize a temporary list that holds the filtered homes.
            SortedHomeList.Clear();
            IEnumerable<Home> filteredHomeList;

            //If the sliders are set to something other than zero. Filter the homes according to the slider values.
            if (advancedFilterParams.CityDistance != 0 ||
                    advancedFilterParams.BeachDistance != 0)
            {
                if (advancedFilterParams.CityDistance != 0 &&
                    advancedFilterParams.BeachDistance == 0)
                    filteredHomeList = HomeList.Where(x => x.CityDistance <= advancedFilterParams.CityDistance);
                else if (
                    advancedFilterParams.BeachDistance != 0 &&
                    advancedFilterParams.CityDistance == 0)
                    filteredHomeList = HomeList.Where(x => x.BeachDistance <= advancedFilterParams.BeachDistance);
                else
                    filteredHomeList = HomeList.Where(x =>
                    x.CityDistance <= advancedFilterParams.CityDistance &&
                    x.BeachDistance <= advancedFilterParams.BeachDistance);
            }
            else
                filteredHomeList = HomeList.Select(x => x);

            //Go through the UI element that holds all the toggle switches and check if they're all false.
            //if not, Apply the filters that the toggle switches specify
            if (!AllFalseAdvancedSearch(grid))
            {
                filteredHomeList = filteredHomeList.Where(x =>
                x.AllowPets == advancedFilterParams.AllowPets &&
                x.AllowSmoking == advancedFilterParams.AllowSmoking &&
                x.HasBalcony == advancedFilterParams.HasBalcony &&
                x.HasPool == advancedFilterParams.HasPool &&
                x.HasWifi == advancedFilterParams.HasWifi);
            }

            //After filtering is done. Add the homes to the SortedHomeList
            foreach (var x in filteredHomeList)
            {
                SortedHomeList.Add(x);
            }
        }

        /// <summary>
        /// Get the name of the button and sort the list according to the button pressed
        /// </summary>
        /// <param name="parameter"></param>
        public void SortList(string parameter)
        {
            
            IOrderedEnumerable<Home> orderedList = null;

            switch (parameter)
            {
                case "bttn_SortLocation":
                    if (IsAscending)
                        orderedList = SortedHomeList.OrderBy(x => x.Location);
                    else
                        orderedList = SortedHomeList.OrderByDescending(x => x.Location);
                    break;

                case "bttn_SortPrice":
                    if(IsAscending)
                        orderedList = SortedHomeList.OrderBy(x => x.Price);
                    else
                        orderedList = SortedHomeList.OrderByDescending(x => x.Price);
                    break;

                case "bttn_SortRooms":
                    if (IsAscending)
                        orderedList = SortedHomeList.OrderBy(x => x.Rooms);
                    else
                        orderedList = SortedHomeList.OrderByDescending(x => x.Rooms);
                    break;

                case "bttn_SortBeds":
                    if (IsAscending)
                        orderedList = SortedHomeList.OrderBy(x => x.NumberOfBeds);
                    else
                        orderedList = SortedHomeList.OrderByDescending(x => x.NumberOfBeds);
                    break;

                case "bttn_SortCityDistance":
                    if (IsAscending)
                        orderedList = SortedHomeList.OrderBy(x => x.CityDistance);
                    else
                        orderedList = SortedHomeList.OrderByDescending(x => x.CityDistance);
                    break;

                case "bttn_SortBeachDistance":
                    if (IsAscending)
                        orderedList = SortedHomeList.OrderBy(x => x.BeachDistance);
                    else
                        orderedList = SortedHomeList.OrderByDescending(x => x.BeachDistance);
                    break;

                case "bttn_SortAverageRating":
                    if (IsAscending)
                        orderedList = SortedHomeList.OrderBy(x => x.AverageRating);
                    else
                        orderedList = SortedHomeList.OrderByDescending(x => x.AverageRating);
                    break;
            }

            //The sorted home objects are put in a seperate list as the orderedList gets cleared when the SortedHomeList gets cleared since they are the same object
            List<Home> tempList = new List<Home>();
            tempList.AddRange(orderedList);
            PopulateSortedHomeList(tempList);
        }

        /// <summary>
        /// Populates the SortedHomeList with the sorted home objects
        /// </summary>
        /// <param name="orderedList"></param>
        private void PopulateSortedHomeList(List<Home> orderedList)
        {
            if (orderedList == null)
                return;

            SortedHomeList.Clear();

            foreach (var x in orderedList)
                SortedHomeList.Add(x);
            
        }

        /// <summary>
        /// Finds the fonticon element of the button and sets the glyph property to an up or down pointing chevron
        /// </summary>
        /// <param name="sortButton"></param>
        public void SortColumns(Button sortButton, DependencyObject buttonContainer)
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
            ClearGlyphsExceptClickedSort(glyph, buttonContainer);
            //Sort the list
            SortList(((FrameworkElement)sortButton).Name);
        }

        /// <summary>
        /// Empties the glyph property on all fonticons except the one in the parameter list
        /// </summary>
        /// <param name="fontIcon"></param>
        private void ClearGlyphsExceptClickedSort(FontIcon fontIcon, DependencyObject buttonContainer)
        {
            //Go through the list of fonticons. if the fonticon is the one in the parameter list.
            //do nothing with it.
            List<FontIcon> fiList = new List<FontIcon>();
            GetAllFonticons(fiList, buttonContainer);

            foreach(var fi in fiList)
                if (fontIcon != fi)
                    fi.Glyph = "";
        }

        public void ClearSortGlyphs(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is FontIcon)
                    ((FontIcon)child).Glyph = "";
                else
                    //If the icon isn't found. Recurse through this method again
                    ClearSortGlyphs(child);
            }
        }
        private void GetAllFonticons(List<FontIcon> fontIconList, DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is FontIcon)
                    fontIconList.Add((FontIcon)child);
                else
                    //If the icon isn't found. Recurse through this method again
                    GetAllFonticons(fontIconList, child);
            }
        }
        /// <summary>
        /// Recursive method that finds one fonticon object inside the UI element that is supplied and returns it
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Recursive method that goes through a UI element and finds all the Toggle Switches
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parent"></param>
        public void FindAdvancedSearchElements(List<Control> list, DependencyObject parent)
        {
            //Loop through once for each child of the parent object
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                //store the current child object
                var child = VisualTreeHelper.GetChild(parent, i);

                //check if it's a ToggleSwitch and add it to a list if it is.
                //else run this method again on the current child.
                if (child.GetType() == typeof(ToggleSwitch))
                    list.Add((Control)child);
                else
                    FindAdvancedSearchElements(list, child);
            }
        }

        /// <summary>
        /// Use the FindAdvancedSearchElements method to extract a list of ToggleSwitches.
        /// Check if they are set to false and return a bool indicating if that is the case
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public bool AllFalseAdvancedSearch(DependencyObject uiElement)
        {
            //create a list and send it to the FindAdvanced method.
            List<Control> controls = new List<Control>();
            FindAdvancedSearchElements(controls, uiElement);

            bool allFalse = true;

            //Go through the list and check if the toggleswitches are on.
            foreach (var x in controls)
            {
                if (x.GetType() == typeof(ToggleSwitch))
                    if (((ToggleSwitch)x).IsOn)
                        allFalse = false;
            }

            //return true if they are all off otherwise return false
            return allFalse;
        }
        #endregion
    }
}
