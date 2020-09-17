using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient.ViewModel
{
    public class RegisterAccountViewModel
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public RegisterAccountViewModel()
        {

        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// TBD
        /// </summary>
        public async Task PostUser(User tempUser)
        {
            await ApiHelper.Instance.PostUser(tempUser);
        }
        /// <summary>
        /// TBD
        /// </summary>
        public async Task CreateNewUser(string name, string surName,string email,string password,bool isCompany)
        {
            User tempUser = new User();
            tempUser.Name = name;
            tempUser.Surname = surName;
            tempUser.Email = email;
            tempUser.Password = password;
            tempUser.IsCompany = isCompany;
           await PostUser(tempUser);
        }
        /// <summary>
        /// TBD
        /// </summary>
        public void LoginUser()
        {

        }
        #endregion
    }
}
