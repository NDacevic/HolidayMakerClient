using System;
using System.Collections.Generic;
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
        public TempReservation(int numberOfGuests, DateTime startDate, DateTime endDate, Home home)
        {
            NumberOfGuests = numberOfGuests;
            StartDate = startDate;
            EndDate = endDate;
            Home = home;
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public int NumberOfGuests { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Home Home { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
