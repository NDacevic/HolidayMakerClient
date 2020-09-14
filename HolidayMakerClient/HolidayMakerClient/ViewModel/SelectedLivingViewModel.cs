using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient
{
    public class SelectedLivingViewModel
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructor
        public SelectedLivingViewModel()
        {

        }
        public SelectedLivingViewModel(Home selectedHome, Reservation selectedReservation)
        {
            SelectedHome = selectedHome;
            SelectedReservation = selectedReservation;
            AddonList = new List<Addon>();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public Home SelectedHome { get; set; }
        public Reservation SelectedReservation { get; set; }
        public List<Addon> AddonList { get; set; }
        #endregion

        #region Methods
        public void CreateReservation()
        {

        }

        public void DeleteReservation()
        {

        }

        public void EditReservation()
        {

        }
        public async void GetAddonList()
        {
            AddonList = await ApiHelper.Instance.GetAllAddon();
        }

        #endregion
    }
}
