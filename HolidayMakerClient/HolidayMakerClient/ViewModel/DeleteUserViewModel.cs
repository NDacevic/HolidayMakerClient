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
        private async Task DeleteUser()
        {
            await ApiHelper.Instance.DeleteUser(LoginViewModel.Instance.ActiveUser.UserId);
        }

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
