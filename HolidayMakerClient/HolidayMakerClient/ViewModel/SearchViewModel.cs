using HolidayMakerClient.Dto;
using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// TBD
        /// </summary>
        public async void Search(string searchString, DateTimeOffset startDate, DateTimeOffset endDate, int numberOfGuests, bool advancedFilterActive, Home advancedFilterParams)
        {
            SearchParameterDto searchObj = new SearchParameterDto(searchString, startDate, endDate, numberOfGuests);
            var homeList = await ApiHelper.Instance.GetSearchResults(searchObj);
            
            HomeList.Clear();
            SortedHomeList.Clear();

            foreach (var x in homeList)
            {
                HomeList.Add(x);
                SortedHomeList.Add(x);
            }

            if (advancedFilterActive)
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
            x.HasWifi == advancedFilterParams.HasWifi &&
            x.CityDistance <= advancedFilterParams.CityDistance &&
            x.BeachDistance <= advancedFilterParams.BeachDistance);

            foreach (var x in filteredHomeList)
                SortedHomeList.Add(x);
        }
        /// <summary>
        /// TBD
        /// </summary>
        public void ResetSearch()
        {

        }

        public void SortList(string parameter)
        {
            
            switch (parameter)
            {
                case "location":
                    SortedHomeList.Clear();

                    foreach (var x in HomeList)
                        SortedHomeList.Add(x);
                    break;
            }
        }
        #endregion
    }
}
