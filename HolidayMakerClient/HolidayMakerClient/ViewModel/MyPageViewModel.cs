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
        public static MyPageViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyPageViewModel();
                }
                return instance;
            }
        }
        public MyPageViewModel()
        {
         
            //TODO:Remove Mock data
            Addon a1 = new Addon();
            a1.AddonType = "Extra säng";
            a1.AddonPrice = 200;
            SelectedReservation = new ObservableCollection<Reservation>();
            MyReservations = new ObservableCollection<Reservation>();
            SelectedHome = new ObservableCollection<Home>();
            SelectedReservationAddon = new ObservableCollection<Addon>();
            Reservation r1 = new Reservation();
            Home h1 = new Home();
            h1.HomeType = "Lägenhet";
            h1.Rooms = 2;
            h1.AllowPets = true;
            h1.AllowSmoking = false;
            h1.Description = "Mysig 2:a, perfekt för den som vill vara sig själv för en stund";
            h1.HasBalcony = true;
            h1.HasPool = true;

            r1.TotalPrice = 2000;
            r1.HomeId = 1;

            r1.AddonList = new List<Addon>();
            r1.AddonList.Add(a1);
            SelectedReservation.Add(r1);
            SelectedHome.Add(h1);
            MyReservations.Add(r1);

        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        private static MyPageViewModel instance = null;
        public ObservableCollection<Reservation> MyReservations { get; set; }
        public ObservableCollection<Addon> SelectedReservationAddon { get; set; }
        public ObservableCollection<Reservation> SelectedReservation { get; set; }
        public ObservableCollection<Home> SelectedHome { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// TBD
        /// </summary>
        public void GetReservation()
        {

        }
        /// <summary>
        /// TBD
        /// </summary>
        public void SelectReservation()
        {

        }
        #endregion

    }
}
