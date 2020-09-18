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
        public async Task GetUser(string email, string enteredPassword)
        {
            User user = await ApiHelper.Instance.GetUser(email);
            await ConfirmPassword(user, enteredPassword);
        }

        public async Task ConfirmPassword(User user, string enteredPassword)
        {
            if (user.Email == null)
            {

            }
            else if (user.Password == enteredPassword)
                ActiveUser = user;
            else
                await new MessageDialog("Inkorrekt lösenord, vänligen försök igen.").ShowAsync();
        }
        #endregion
    }
}
