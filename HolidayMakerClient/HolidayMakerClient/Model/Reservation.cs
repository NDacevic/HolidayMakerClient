using HolidayMakerClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace HolidayMakerClient
{
    public class Reservation
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public Reservation()
        {

        }
        public Reservation(int reservationId, int homeId, int userId,int numberOfGuests, DateTimeOffset startDate, DateTimeOffset endDate, decimal totalPrice, List<Addon> addonList)
        {
            ReservationId = reservationId;
            HomeId = homeId;
            UserId = userId;
            NumberOfGuests = numberOfGuests;
            StartDate = startDate;
            EndDate = endDate;
            TotalPrice = totalPrice;
            AddonList = addonList;
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public int ReservationId { get; set; }
        public int HomeId { get; set; }
        public int UserId { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        [JsonProperty("Addons")]
        public List<Addon> AddonList { get; set; }
    
      
        #endregion

        #region Methods
        #endregion
    }
}
