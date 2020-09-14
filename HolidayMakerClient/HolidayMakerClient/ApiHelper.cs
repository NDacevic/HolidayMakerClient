using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient
{
    public static class ApiHelper
    {
        #region Constant Fields
        private static HttpClient httpClient;
        private static string jsonString;
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static void PostUser()
        {

        }

        public static void GetUser()
        {

        }

        public static void DeleteUser()
        {

        }

        public static void GetHome()
        {

        }

        public static void PostReservation()
        {

        }
        public static async Task<ObservableCollection<Reservation>> GetUserReservations()
        {
            //TODO:Only GET the users reservations. Send in User id and get a list of reservations linked to that id

            HttpResponseMessage response = await httpClient.GetAsync("UserReservations");

            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;
                //Convert jsonString to list of courses objects
                var reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString);
                return reservations;
            }
            else if(response.Content==null)
            {
                throw new HttpRequestException("Här var det tomt! Gå in och gör en reservation för att se listan.");
            }
            else
            {
                throw new HttpRequestException("Kunde inte hämta några reservationer, var vänlig försök igen.");
            }
        }

        public static void GetReservation()
        {

        }

        public static void PatchReservation()
        {

        }

        public static void DeleteReservation()
        {

        }

        public static void PostReservationAddon()
        {

        }

        public static void GetAddon()
        {

        }
        #endregion
    }
}
