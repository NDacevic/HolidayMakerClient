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
        public TempReservation(string numberOfGuests, DateTimeOffset startDate, DateTimeOffset endDate, decimal totalPrice, Home home)
        {
            NumberOfGuests = numberOfGuests;
            StartDate = startDate;
            EndDate = endDate;
            TotalPrice = totalPrice;
            AddonList = new ObservableCollection<Addon>();
            Home = home;
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
        #endregion

        #region Methods
        #endregion
    }
}
