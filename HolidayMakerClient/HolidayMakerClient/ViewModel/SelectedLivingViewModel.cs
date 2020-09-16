using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public SelectedLivingViewModel(Home selectedHome, Reservation selectedReservation, TempReservation tempReservation)
        {
            SelectedHome = selectedHome;
            SelectedReservation = selectedReservation;
            AddonList = new ObservableCollection<Addon>();
            TempReservation = tempReservation;
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public Home SelectedHome { get; set; }
        public Reservation SelectedReservation { get; set; }
        public ObservableCollection<Addon> AddonList { get; set; }
        public TempReservation TempReservation { get; set; }
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
        public void TempTotalPrice()
        {
            
        }

        #endregion
    }
}
