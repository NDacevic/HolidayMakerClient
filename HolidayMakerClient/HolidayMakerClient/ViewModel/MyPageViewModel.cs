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
        private static MyPageViewModel instance = null;
        private static readonly object padlock = new object();
        #endregion

        #region Constructors

        public MyPageViewModel()
        {
            SelectedReservation = new ObservableCollection<Reservation>();
            MyReservations = new ObservableCollection<Reservation>();
            SelectedHome = new ObservableCollection<Home>();
            SelectedReservationAddons = new ObservableCollection<Addon>();
            ActiveUserHomes = new ObservableCollection<Home>();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties

        /// <summary>
        /// The instance for the LoginViewModel singleton.
        /// </summary>
        public static MyPageViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MyPageViewModel();
                    }
                    return instance;
                }
            }
        }
        public ObservableCollection<Reservation> MyReservations { get; set; }
        public ObservableCollection<Addon> SelectedReservationAddons { get; set; }
        public ObservableCollection<Reservation> SelectedReservation { get; set; }
        public ObservableCollection<Home> SelectedHome { get; set; }
        public ObservableCollection<Home> ActiveUserHomes { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// When navigated to the page, we get all reservations connected to the user and add them to our OC.
        /// </summary>
        public async Task GetReservations()
        {
            try
            {
                MyReservations = await ApiHelper.Instance.GetUserReservations();
            }
            catch
            {
                return;
            }

        }
        /// <summary>
        /// When user selects a reservation we get specific details about home,reservation and addons.
        /// </summary>
        public async void SelectedUserReservation(Reservation reservation)
        {
            try
            {
                ResetLists();
                SelectedReservation.Add(reservation);
                Home home = await ApiHelper.Instance.GetHome(reservation.HomeId);
                SelectedHome.Add(home);

                var addonlist = await ApiHelper.Instance.GetReservationAddon(reservation.ReservationId);
                foreach (Addon a in addonlist)
                {
                
                    SelectedReservationAddons.Add(a);
                }
            }
            catch
            {
                return;
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
        public async void DeleteReservation(Reservation reservation)
        {
            bool isDeleted = await ApiHelper.Instance.DeleteReservation(reservation.ReservationId);
            //If the Reservation was removed from the database, also remove it locally
            if (isDeleted)
            {
                MyReservations.Remove(reservation);
            }
        }
        

        public async Task GetActiveUserHomes()
        {
            ActiveUserHomes = await ApiHelper.Instance.GetUserHomes(LoginViewModel.Instance.ActiveUser.UserId);
        }
        public async Task DeleteHome(Home homeToBeDeleted)
        {
            await ApiHelper.Instance.DeleteHome(homeToBeDeleted.HomeId);
            Instance.ActiveUserHomes.Remove(homeToBeDeleted);
        }

        #endregion
    }
}
