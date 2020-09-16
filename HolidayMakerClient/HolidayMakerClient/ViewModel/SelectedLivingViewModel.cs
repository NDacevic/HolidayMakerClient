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
        public SelectedLivingViewModel(Home selectedHome, Reservation selectedReservation, TempReservation tempRes)
        {
            SelectedHome = selectedHome;
            SelectedReservation = selectedReservation;
            AddonList = new ObservableCollection<Addon>();
            TempRes = tempRes;
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
        public TempReservation TempRes { get; set; }
        public LoginViewModel loginViewModel { get; }
        #endregion

        #region Methods
        public async void CreateReservation(TempReservation reservation)
        {
            SelectedReservation = new Reservation();
            SelectedReservation.HomeId = reservation.Home.HomeId;
            SelectedReservation.UserId = loginViewModel.ActiveUser.UserId;
            SelectedReservation.StartDate = reservation.StartDate;
            SelectedReservation.EndDate = reservation.EndDate;
            SelectedReservation.TotalPrice = reservation.TotalPrice;
            foreach(var ad in reservation.AddonList)
            {
                SelectedReservation.AddonList.Add(ad);
            }

            try
            {
                await ApiHelper.Instance.PostReservation(SelectedReservation);
            }
            catch (Exception)
            {
                return;
            }
            
        }

        public void DeleteReservation()
        {

        }

        public void EditReservation()
        {

        }
        public async void GetAddonList()
        {
            //ObservableCollection<Addon> HomeAddons = new ObservableCollection<Addon>();
            try
            {             
                var addons = await ApiHelper.Instance.GetAllAddon();
                foreach (var a in addons)
                {
                    AddonList.Add(a);
                }
                    
                //foreach(var a in AddonList)
                //{
                //    if (tempReservation.Home.HasAllInclusive == true)
                //    {
                //        HomeAddons.Add();

                //    }
                //}

            }
            catch (Exception)
            {
                
            }
            
        }
        public decimal TempTotalPrice(TempReservation tempReservation)
        {
            try
            {
                int days = (tempReservation.EndDate - tempReservation.StartDate).Days;
                tempReservation.TotalPrice = tempReservation.Home.Price * days;

                if (tempReservation.AddonList.Count > 0)
                {
                    foreach (var a in tempReservation.AddonList)
                    {
                        if (a.AddonType == "All-inclusive" || a.AddonType == "Helpension" || a.AddonType == "Halvpension")
                        {
                            tempReservation.TotalPrice += a.AddonPrice * int.Parse(tempReservation.NumberOfGuests);
                        }
                        if (a.AddonType == "Extrasäng")
                        {
                            tempReservation.TotalPrice += a.AddonPrice;
                        }
                    }
                }

            }
            catch (Exception)
            {
                tempReservation.TotalPrice = 0;

            }
            return tempReservation.TotalPrice;

        }

        #endregion
    }
}
