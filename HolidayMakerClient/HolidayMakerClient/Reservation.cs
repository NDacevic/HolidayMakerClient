using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace HolidayMakerClient
{
    class Reservation
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public Reservation()
        {

        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public int ReserationId { get; set; }
        public int HomeId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Addon> AddonList { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
