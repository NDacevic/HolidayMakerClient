using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace HolidayMakerClient
{
    public class User
    {
        #region Constant Fields
        #endregion

        #region Fields
        
        #endregion

        #region Constructors
        public User()
        {
        }

        public User(int userId, string name, string surname, string email, string password, bool isCompany)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            IsCompany = isCompany;
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsCompany { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
