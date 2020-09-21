using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace HolidayMakerClient.ViewModel
{
    public class DeleteUserViewModel
    {

        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public DeleteUserViewModel()
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
        /// Removes the user from the database through a call to the APIHelper class
        /// </summary>
        /// <returns></returns>
        private async Task DeleteUser()
        {
            await ApiHelper.Instance.DeleteUser(LoginViewModel.Instance.ActiveUser.UserId);
        }

        /// <summary>
        /// Confirms the password on user-deletion before calling the DeleteUser() method.
        /// Otherwise a messagedialog is displayed informing the user that the password is wrong.
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <returns></returns>
        public async Task<bool> ConfirmPassword(string enteredPassword)
        {
            if (enteredPassword == LoginViewModel.Instance.ActiveUser.Password)
            {
                await DeleteUser();
                LoginViewModel.Instance.ActiveUser = null;
                return true;
            }
            else
            {
                await new MessageDialog("Felaktigt lösenord. Vänligen försök igen.").ShowAsync();
                return false;
            }
        }
        #endregion
    }
}
