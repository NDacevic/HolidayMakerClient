using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient.ViewModel
{
    /// <summary>
    /// Enum used to populate the HomeType listbox on UploadLivingView
    /// </summary>
    public enum HomeType
    {
        Lägenhet,
        Villa,
        Hotell,
        Extrarum,
        Stuga
    }


    public class UploadLivingViewModel
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public UploadLivingViewModel()
        {
        }

        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        public async Task CreateNewHome(string homeType, int rooms, string location, decimal price, bool hasBalcony, 
            bool allowPets, bool hasWifi, bool hasHalfPension, bool hasFullPension, bool hasAllInclusive, bool hasExtraBed, 
            int cityDistance, int beachDistance, int numberOfBeds, bool hasPool, bool allowSmoking, string description)
        {
            await PostHome(new Home(homeType,rooms, location, price, hasBalcony, 
                allowPets, hasWifi, hasHalfPension, hasFullPension, hasAllInclusive, hasExtraBed, 
                cityDistance, beachDistance, numberOfBeds, hasPool, allowSmoking, description,LoginViewModel.Instance.ActiveUser.UserId));
        }
        public async Task PostHome(Home newHome)
        {
            //Saves the posted home locally to get the correct HomeId, in case the user wants to Delete the home instantly. 
            Home postedHome = await ApiHelper.Instance.PostHome(newHome);
            MyPageViewModel.Instance.ActiveUserHomes.Add(postedHome);
        }
        #endregion

    }
}
