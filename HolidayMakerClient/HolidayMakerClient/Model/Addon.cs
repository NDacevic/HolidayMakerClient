using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient.Model
{
    public class Addon
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public Addon(int addonId,string addonType,decimal addonPrice)
        {
            AddonId = addonId;
            AddonType = addonType;
            AddonPrice = addonPrice;
        }
        public Addon()
        {

        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public int AddonId { get; set; }
        public string AddonType { get; set; }
        [JsonProperty("Price")]
        public decimal AddonPrice { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
