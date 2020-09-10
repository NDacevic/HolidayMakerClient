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
            MyReservations = new ObservableCollection<Reservation>();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        private static MyPageViewModel instance = null;
        public ObservableCollection<Reservation> MyReservations { get; set; }
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
