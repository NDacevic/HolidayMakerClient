﻿using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Popups;

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
                jsonString = await httpClient.GetStringAsync("Addons");
                addonList = JsonConvert.DeserializeObject<List<Addon>>(jsonString);
                return addonList;
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
