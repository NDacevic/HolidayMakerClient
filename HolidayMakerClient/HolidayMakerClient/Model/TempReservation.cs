using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient.Model
{
    public class TempReservation
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
<<<<<<< HEAD
        public TempReservation(string numberOfGuests, DateTime startDate, DateTime endDate, Home home, Reservation oldReservation)
=======
        public TempReservation(string numberOfGuests, DateTimeOffset startDate, DateTimeOffset endDate, decimal totalPrice, Home home)
>>>>>>> SelectedCodeBehind
        {
            NumberOfGuests = numberOfGuests;
            StartDate = startDate;
            EndDate = endDate;
            TotalPrice = totalPrice;
            AddonList = new ObservableCollection<Addon>();
            Home = home;
            OldReservation = oldReservation;
        }
        public TempReservation() { }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public string NumberOfGuests { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ObservableCollection<Addon> AddonList { get; set; }
        public Home Home { get; set; }
        public Reservation OldReservation { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
