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
        /// Calls the APIHelper method PostUser and sends a user object for creation
        /// </summary>
        public async Task PostUser(User tempUser)
        {
           await ApiHelper.Instance.PostUser(tempUser);
        }

        /// <summary>
        /// Sets all the parameters for a new  user and calls the PostUser method inside the RegisterAccountViewModel class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isCompany"></param>
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
        /// Validates if there is exactly 1 '@' in the email, and at least 1 '.'
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateEmail(string email)
        {
            int numAt=0;
            int numDot=0;

            char[] emailCharacters = email.ToCharArray();

            foreach (char character in emailCharacters)
            {
                if (character=='@')
                    numAt++;
                else if (character=='.')
                    numDot++;
            }

            if (numAt!=1)
                //Invalid number of @
                return false;


            if (numDot < 1)
                //Too few .
                return false;

            return true;

        }

        /// <summary>
        /// Validates that the password is between 8 and 128 characters
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {
            int passwordLength = (password.ToCharArray()).Length;
            if (passwordLength < 8 || passwordLength > 128)
                return false;
            else
                return true;
        }
        #endregion
    }
}
