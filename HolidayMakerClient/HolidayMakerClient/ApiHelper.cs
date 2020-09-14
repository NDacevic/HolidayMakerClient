﻿using HolidayMakerClient.Model;
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

namespace HolidayMakerClient
{
    public class ApiHelper
    {
        #region Constant Fields


        #endregion

        #region Fields
        private static string jsonString;
        private static ApiHelper instance = null;
        private static readonly object padlock = new object();
        private static HttpClient httpClient;
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

        public static void GetHome()
        {

        }

        public static void PostReservation()
        {

        }
        public static async Task<ObservableCollection<Reservation>> GetUserReservations()
        {
            //TODO:Only GET the users reservations. Send in User id and get a list of reservations linked to that id
            ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();
            HttpResponseMessage response = await httpClient.GetAsync("UserReservations");

            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;
                //Convert jsonString to list of courses objects
                var res = JsonConvert.DeserializeObject<List<Reservation>>(jsonString);
                foreach(var r in res)
                {
                    reservations.Add(r);
                }
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

        public static async Task<List<Addon>> GetAllAddon ()
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
