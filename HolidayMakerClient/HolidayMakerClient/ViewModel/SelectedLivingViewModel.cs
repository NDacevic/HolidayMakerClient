﻿using HolidayMakerClient.Model;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
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
        public decimal TotalPrice { get; set; }
        public Addon ExtraBed { get; set; }
        #endregion

        #region Methods
        public async void CreateReservation(TempReservation reservation, ObservableCollection<Addon> addonList, decimal price)
        {
            SelectedReservation = new Reservation();
            SelectedReservation.HomeId = reservation.Home.HomeId;
            SelectedReservation.UserId = LoginViewModel.Instance.ActiveUser.UserId;
            SelectedReservation.NumberOfGuests = int.Parse(reservation.NumberOfGuests);
            SelectedReservation.StartDate = reservation.StartDate;
            SelectedReservation.EndDate = reservation.EndDate;
            SelectedReservation.TotalPrice = price;
            SelectedReservation.AddonList = new List<Addon>();
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

        public async void EditReservation(Reservation updatedReservation,DateTimeOffset startDate,DateTimeOffset endDate,string numberOfGuests)
        {
            try
            {
                JsonPatchDocument<Reservation> patchDoc = new JsonPatchDocument<Reservation>();
                CreateReservationPatchDoc(updatedReservation, startDate, endDate, numberOfGuests, patchDoc);

                bool success = await ApiHelper.Instance.PatchReservation(updatedReservation.ReservationId, patchDoc);

                if (success)
                {
 
                    await new MessageDialog("Användarinformation ändrad").ShowAsync();
                }

            }
            catch (FormatException exc)
            {
                await new MessageDialog(exc.Message).ShowAsync();
            }
        }
        public void CreateReservationPatchDoc(Reservation updatedReservation, DateTimeOffset startDate, DateTimeOffset endDate, string numberOfGuests,JsonPatchDocument<Reservation> patchDoc)
        {
            //We check for what is different and add that to the patch doc.
            if (updatedReservation.StartDate != startDate)
                patchDoc.Replace(x => x.StartDate, startDate);

            if (updatedReservation.EndDate != endDate)
                patchDoc.Replace(x => x.EndDate, endDate);

            if (updatedReservation.NumberOfGuests != int.Parse(numberOfGuests))
                patchDoc.Replace(x => x.NumberOfGuests, int.Parse(numberOfGuests));

            string reservation = JsonConvert.SerializeObject(patchDoc);

            if (reservation == "[]")
                throw new FormatException("Inga ändringar gjorda");
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
