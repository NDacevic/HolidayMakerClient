using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

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
        public LoginViewModel()
        {

        }

        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public User ActiveUser { get; set; }

        /// <summary>
        /// The instance for the LoginViewModel singleton.
        /// </summary>
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
        /// <summary>
        /// Gets a user from the database by email and calls the ConfirmPassword() method.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="enteredPassword"></param>
        /// <returns></returns>
        public async Task GetUser(string email, string enteredPassword)
        {
            User user = await ApiHelper.Instance.GetUser(email);
            await ConfirmPassword(user, enteredPassword);
        }

        /// <summary>
        /// Checks that the entered password matches the one associated to that user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        public async Task ConfirmPassword(User user, string encryptedPassword)
        {
            if (user.Email == null)
                return;
            else if (user.Password == encryptedPassword)
                ActiveUser = user;
            else
                await new MessageDialog("Inkorrekt lösenord, vänligen försök igen.").ShowAsync();
        }
        #endregion
    }
}
