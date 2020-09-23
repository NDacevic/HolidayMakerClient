using HolidayMakerClient.Model;
using HolidayMakerClient.View;
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
using Windows.UI.Xaml.Controls;

namespace HolidayMakerClient
{
    public class SelectedLivingViewModel
    {
        #region Constant Fields
        #endregion

        #region Fields
        private ContentDialog loadData;
        #endregion

        #region Constructor
        public SelectedLivingViewModel()
        {
            AddonList = new ObservableCollection<Addon>();
            ExtraBed = new Addon();
            loadData = new LoadDataView();
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
        public List<Reservation> AllReservations { get; set; }
        public List<DateTimeOffset> InvalidDates { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Creates a reservation, adds the chosen addons and sends it to the API for posting.
        /// </summary>
        /// <param name="reservation"></param>
        /// <param name="addonList"></param>
        /// <param name="price"></param>
        public async void CreateReservation(TempReservation reservation, ObservableCollection<Addon> addonList, decimal price)
        {
            SelectedReservation = new Reservation();
            SelectedReservation.HomeId = reservation.TempHome.HomeId;
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

        /// <summary>
        /// Calls the API and sends an Id for the reservation to be deleted from the database
        /// </summary>
        /// <param name="tempReservation"></param>
        public void DeleteReservation(TempReservation tempReservation)
        {
            try
            {
                ApiHelper.Instance.DeleteReservation(tempReservation.TempId);
            }
            catch
            {
                return;
            }
            
        }
        /// <summary>
        /// Recieves parameter for the reservation change and sends it to a method for converting to a JsonPatchDocument
        /// </summary>
        /// <param name="tempReservation"></param>

        public async void EditReservation(Reservation updatedReservation,DateTimeOffset startDate,DateTimeOffset endDate,decimal totalPrice,ObservableCollection<Addon> chosenAddon,string numberOfGuests)
        {
            try
            {
                JsonPatchDocument<Reservation> patchDoc = new JsonPatchDocument<Reservation>();
                CreateReservationPatchDoc(updatedReservation, startDate, endDate ,numberOfGuests, totalPrice,chosenAddon ,patchDoc);

                bool success = await ApiHelper.Instance.PatchReservation(updatedReservation.ReservationId, patchDoc);

                if (success)
                {
                        await new MessageDialog("Ändring genomförd").ShowAsync();
                }

            }
            catch (FormatException exc)
            {
                await new MessageDialog(exc.Message).ShowAsync();
            }
        }
        /// <summary>
        /// We check what parameter that we want to change and populate the JsonPatchDocument
        /// </summary>
        /// <param name="tempReservation"></param>
        public void CreateReservationPatchDoc(Reservation updatedReservation, DateTimeOffset startDate, DateTimeOffset endDate, string numberOfGuests,decimal totalPrice,ObservableCollection<Addon> chosenAddon,JsonPatchDocument<Reservation> patchDoc)
        {
            List<Addon> tempList = new List<Addon>();
            foreach(var tl in chosenAddon)
            {
                tempList.Add(tl);
            }
            //We check for what is different and add that to the patch doc.
            if (updatedReservation.AddonList != tempList)
                patchDoc.Replace(x => x.AddonList, tempList);
            if (updatedReservation.StartDate != startDate)
                patchDoc.Replace(x => x.StartDate, startDate);

            if (updatedReservation.EndDate != endDate)
                patchDoc.Replace(x => x.EndDate, endDate);

            if (updatedReservation.NumberOfGuests != int.Parse(numberOfGuests))
                patchDoc.Replace(x => x.NumberOfGuests, int.Parse(numberOfGuests));

            if (updatedReservation.TotalPrice != totalPrice)
                patchDoc.Replace(x => x.TotalPrice, totalPrice);

            string reservation = JsonConvert.SerializeObject(patchDoc);

            if (reservation == "[]")
                throw new FormatException("Inga ändringar gjorda");
        }
        /// <summary>
        /// Gets all reservations based on homeId
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public async Task GetHomeReservation (int homeId)
        {
            try
            {
                AllReservations = new List<Reservation>();
                var reservations = await ApiHelper.Instance.GetAllReservation(homeId);
                foreach(var r in reservations)
                {
                    AllReservations.Add(r);
                }
            }
            catch
            {
                return;
            }

        }
        /// <summary>
        /// Place invalid dates in list
        /// </summary>
        /// <returns></returns>
        public void SetInvalidDates()
        {
            try
            {
                InvalidDates = new List<DateTimeOffset>();
                foreach(var r in AllReservations)
                {
                    for(int i = 0; i<=(r.EndDate.Date.Subtract(r.StartDate.Date)).Days; i++)
                    {
                        InvalidDates.Add(r.StartDate.AddDays(i).Date);
                    }
                }
            }
            catch
            {
                return;
            }

        }


        /// <summary>
        /// Gets a list of addons from the API and adds them to a list
        /// </summary>

        public async void GetAddonList()
        {
            try
            {
                loadData.ShowAsync();
                var addons = await ApiHelper.Instance.GetAllAddon();
                foreach (var a in addons)
                {
                    //adds all the addons except the extra bed addon.
                    if(a.AddonType != "Extrasäng")
                    {
                        AddonList.Add(a);
                    }

                }
                loadData.Hide();
            }
            catch (Exception)
            {
                return;
            }
            
        }

        /// <summary>
        /// Gets the extrabed addon and sets the Extrabed property with the gotten object
        /// </summary>
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
                return;
            }

        }

        #endregion
    }
}
