using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient.ViewModel
{
    public class MyPageViewModel
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
     
        public MyPageViewModel()
        {

            SelectedReservation = new ObservableCollection<Reservation>();
            MyReservations = new ObservableCollection<Reservation>();
            SelectedHome = new ObservableCollection<Home>();
            SelectedReservationAddons = new ObservableCollection<Addon>();
      

        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
       
        public ObservableCollection<Reservation> MyReservations { get; set; }
        public ObservableCollection<Addon> SelectedReservationAddons { get; set; }
        public ObservableCollection<Reservation> SelectedReservation { get; set; }
        public ObservableCollection<Home> SelectedHome { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// When navigated to the page, we get all reservations connected to the user and add them to our OC.
        /// </summary>
        public async void GetReservations()
        {
            //TODO:Send in active users id to the ApiHelper
          var res = await ApiHelper.Instance.GetUserReservations();
            foreach(Reservation r in res)
            {
                MyReservations.Add(r);
            }
        }
        /// <summary>
        /// When user selects a reservation we get specific details about home,reservation and addons.
        /// </summary>
        public async void SelectedUserReservation(Reservation reservation)
        {
            ResetLists();
            SelectedReservation.Add(reservation);
            Home home = await ApiHelper.Instance.GetHome(reservation.HomeId);
            SelectedHome.Add(home);
            var addonlist= await ApiHelper.Instance.GetReservationAddon(reservation.ReservationId);
            foreach(Addon a in addonlist)
            {
                SelectedReservationAddons.Add(a);
            }

        }

        /// <summary>
        /// Clears most of the ObservableCollections inside the MyPageViewModel class
        /// </summary>
        public void ResetLists()
        {
            SelectedReservationAddons.Clear();
            SelectedReservation.Clear();
            SelectedHome.Clear();
        }

        /// <summary>
        /// Calls the APIHelper method DeleteReservation and supplies an Id for deletion
        /// </summary>
        /// <param name="reservation"></param>
        public void DeleteReservation(Reservation reservation)
        {
             ApiHelper.Instance.DeleteReservation(reservation.ReservationId);
        }
        #endregion

    }
}
