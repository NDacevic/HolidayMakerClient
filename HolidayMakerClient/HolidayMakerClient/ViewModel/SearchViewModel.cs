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
        public async void Search(string searchString, DateTimeOffset startDate, DateTimeOffset endDate, int numberOfGuests)
        {
            SearchParameterDto searchObj = new SearchParameterDto(searchString, startDate, endDate, numberOfGuests);
            var homeList = await ApiHelper.Instance.GetSearchResults(searchObj);
            
            HomeList.Clear();
            
            foreach(var x in homeList)
                HomeList.Add(x);
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
        public void Filter()
        {

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
