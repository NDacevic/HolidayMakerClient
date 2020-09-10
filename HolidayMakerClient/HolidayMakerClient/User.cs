using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient
{
    class User
    {
        #region Constant Fields
        #endregion

        #region Fields
        
        #endregion

        #region Constructors
        public User()
        {
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
