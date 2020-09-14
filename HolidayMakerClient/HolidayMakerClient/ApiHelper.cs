using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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

        public async Task<List<Addon>> GetAddonList()
        {
            jsonString = await httpClient.GetStringAsync("Addons/");
            var addonList = JsonConvert.DeserializeObject<List<Addon>>(jsonString);
            return addonList;
        }

        //public async Task<List<TestResult>> GetAllTestResults(int testId)
        //{
        //    try
        //    {
        //        jsonString = await httpClient.GetStringAsync("TestResults/" + testId);
        //        var testResults = JsonConvert.DeserializeObject<List<TestResult>>(jsonString);
        //        return testResults;
        //    }
        //    catch (Exception exc)
        //    {
        //        BasicNoConnectionMessage(exc);
        //        return new List<TestResult>();
        //    }

        //}
        #endregion
    }
}
