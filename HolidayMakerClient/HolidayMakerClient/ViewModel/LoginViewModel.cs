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
        #endregion

        #region Constructors
        public LoginViewModel()
        {

        }

        public LoginViewModel(User activeUser)
        {
            ActiveUser = activeUser;
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public User ActiveUser { get; set; }

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
