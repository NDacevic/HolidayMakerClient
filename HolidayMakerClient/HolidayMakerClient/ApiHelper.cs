using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Popups;
using System.Net.Http.Headers;
using Windows.UI.Xaml.Documents;
using HolidayMakerClient.Dto;

namespace HolidayMakerClient
{
    public class ApiHelper
    {
        #region Constant Fields


        #endregion

        #region Fields
        private string jsonString;
        private static ApiHelper instance = null;
        private static readonly object padlock = new object();
        private HttpClient httpClient = new HttpClient();
        #endregion

        #region Constructors
        public ApiHelper()
        {
            httpClient.BaseAddress = new Uri(@"https://localhost:5001/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        public static ApiHelper Instance
        {
            get 
            { 
                lock (padlock)
                {
                    if (instance == null)
                    { 
                        instance = new ApiHelper();
                    }
                    return instance;
                }
            }
        }
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

        public async Task<ObservableCollection<Home>> GetSearchResults(SearchParameterDto searchParameters)
        {
            try { 
                //Convert the object to a json string.
                jsonString = JsonConvert.SerializeObject(searchParameters);

                //Set this part of the code into a scope so we don't have to worry about it not getting disposed.
                using (HttpContent content = new StringContent(jsonString))
                {
                    //Set the type of content
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Call the api and send the Json string.
                    HttpResponseMessage response = await httpClient.PostAsync("searchresults", content);

                    //Check if it is successfull. If so, return all search results in form of a OC of Home objects
                    //Otherwise throw an error and tell the user that the question was not posted.
                    if (response.IsSuccessStatusCode)
                    {
                        jsonString = response.Content.ReadAsStringAsync().Result;
                        var searchResults = JsonConvert.DeserializeObject<ObservableCollection<Home>>(jsonString);
                        return searchResults;
                    }
                    else
                    {
                        Debug.WriteLine($"Http Error: {response.StatusCode}. {response.ReasonPhrase}");
                        throw new HttpRequestException("Uppkopplingen mot servern misslyckades, var god försök igen senare");
                    }
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return new ObservableCollection<Home>();
            }
        }       
        public static void PostReservation()
        {

        }
        public async Task<ObservableCollection<Reservation>> GetUserReservations()
        {
            //TODO:Only GET the users reservations. Send in User id and get a list of reservations linked to that id
            HttpResponseMessage response = await httpClient.GetAsync("UsersReservations/1");

            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;
               
                var reservations = JsonConvert.DeserializeObject<ObservableCollection<Reservation>>(jsonString);

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
        public async Task <Home> GetHome(int id)
        {
            //Used to get Home details from the selected reservation in MyPage
            HttpResponseMessage response = await httpClient.GetAsync($"Homes/{id}");
            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;

                var home = JsonConvert.DeserializeObject<Home>(jsonString);

                return home;
            }
            else
            {
                throw new HttpRequestException("Kunde inte hämta några boende, var vänlig försök igen.");
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
        public async Task<ObservableCollection<Addon>> GetReservationAddon(int id)
        {
            //TODO: Send in reservation id, get a list of addons linked to that reservation and return.
            HttpResponseMessage response = await httpClient.GetAsync($"ReservationAddons/{id}");
            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;

                var addons = JsonConvert.DeserializeObject<ObservableCollection<Addon>>(jsonString);

                return addons;
            }
            else
            {
                throw new HttpRequestException("Kunde inte hämta några boende, var vänlig försök igen.");
            }
        }
        public async Task<List<Addon>> GetAllAddon ()
        {
            List<Addon> addonList = new List<Addon>();
            try
            {               
                HttpResponseMessage response = await httpClient.GetAsync("Addons");
                if(response.IsSuccessStatusCode)
                {
                     jsonString = response.Content.ReadAsStringAsync().Result;
                     var addon = JsonConvert.DeserializeObject<List<Addon>>(jsonString);
                     foreach(var a in addon)
                     {
                        addonList.Add(a);
                     }
                        return addonList;
                }
                else
                {
                    throw new HttpRequestException("Gick ej att hämta, vänligen försök igen.");
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return addonList;
            }
        }
        private static async void BasicNoConnectionMessage(Exception exc)
        {
            Debug.WriteLine(exc.Message);
            await new MessageDialog("Ingen kontakt med servern. Kontakta admin").ShowAsync();
        }

        #endregion
    }
}
