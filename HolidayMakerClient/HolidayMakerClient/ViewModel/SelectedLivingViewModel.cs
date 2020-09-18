using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

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
            AddonList = new ObservableCollection<Addon>();
            ExtraBed = new Addon();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public Reservation SelectedReservation { get; set; }
        public ObservableCollection<Addon> AddonList { get; set; }
        public TempReservation TempRes { get; set; }
        public LoginViewModel loginViewModel { get; }
        public decimal TotalPrice { get; set; }
        public Addon ExtraBed { get; set; }
        #endregion

        #region Methods
        public async void CreateReservation(TempReservation reservation, ObservableCollection<Addon> addonList, decimal price)
        {
            SelectedReservation = new Reservation();
            SelectedReservation.HomeId = reservation.Home.HomeId;
            SelectedReservation.UserId = loginViewModel.ActiveUser.UserId;
            SelectedReservation.StartDate = reservation.StartDate;
            SelectedReservation.EndDate = reservation.EndDate;
            SelectedReservation.TotalPrice = price;
            foreach(var ad in addonList)
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
            try
            {             
                var addons = await ApiHelper.Instance.GetAllAddon();
                foreach (var a in addons)
                {
                    if(a.AddonType != "Extrasäng")
                    {
                        AddonList.Add(a);
                    }

                }
            }
            catch (Exception)
            {
                await new MessageDialog("Ingen kontakt med servern. Kontakta admin").ShowAsync();
                return;
            }
            
        }
        public async void GetAddonExtraBed()
        {
            try
            {
                var addons = await ApiHelper.Instance.GetAllAddon();
                foreach (var a in addons)
                {
                    if (a.AddonType == "Extrasäng")
                    {
                        ExtraBed.AddonId = a.AddonId;
                        ExtraBed.AddonType = a.AddonType;
                        ExtraBed.AddonPrice = a.AddonPrice;
                    }

                }
            }
            catch (Exception)
            {
                await new MessageDialog("Ingen kontakt med servern. Kontakta admin").ShowAsync();
                return;
            }

        }

        #endregion
    }
}
