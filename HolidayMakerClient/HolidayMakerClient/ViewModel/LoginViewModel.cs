using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient
{
    public class LoginViewModel
    {
        #region Constant Fields
        #endregion

        #region Fields
        private static LoginViewModel instance = null;
        private static readonly object padlock = new object();
        #endregion

        #region Constructors
        private LoginViewModel()
        {

        }

        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public User ActiveUser { get; set; }
        public static LoginViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LoginViewModel();
                    }
                    return instance;
                }
            }
        }

        #endregion

        #region Methods
        public void GetUser()
        {

        }

        public void ConfirmPassword()
        {

        }
        #endregion
    }
}
