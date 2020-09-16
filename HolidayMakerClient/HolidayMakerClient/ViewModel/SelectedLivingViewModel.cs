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
            try
            {
                int days = (TempReservation.EndDate - TempReservation.StartDate).Days;
                TempReservation.TotalPrice = TempReservation.Home.Price * days;

                if (TempReservation.AddonList.Count > 0)
                {
                     foreach (var a in TempReservation.AddonList)
                     {
                        if(a.AddonType == "All-inclusive" || a.AddonType == "Helpension" || a.AddonType == "Halvpension")
                        {
                            TempReservation.TotalPrice += a.AddonPrice * int.Parse(TempReservation.NumberOfGuests);
                        }
                        if(a.AddonType == "Extrasäng")
                        {
                            TempReservation.TotalPrice += a.AddonPrice;
                        }
                     }
                }

            }
            catch (Exception)
            {
                TempReservation.TotalPrice = 0;
                
            }

        }

        #endregion
    }
}
