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
using System.Diagnostics;
using Windows.UI.Popups;
using System.Net.Http.Headers;
using Windows.UI.Xaml.Documents;
using HolidayMakerClient.Dto;
using System.Net;

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
        public async Task PostUser(User user)
        {
            try
            {
                jsonString = JsonConvert.SerializeObject(user);
                using (HttpContent httpContent = new StringContent(jsonString))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage respons = await httpClient.PostAsync("Users", httpContent);

                    //Check if succesfull
                    if (respons.IsSuccessStatusCode)
                    {
                        await new MessageDialog("Registrering lyckades!").ShowAsync();
                    }

                    else
                    {
                        await new MessageDialog("Denna epost är redan registrerad. Var vänlig försök igen.").ShowAsync();
                    }
                }

          
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
            }
        }

        public async Task<User> GetUser(string email)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"LoginUsers/{email}");

                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync().Result;

                    var user = JsonConvert.DeserializeObject<User>(jsonString);

                    return user;
                }
                else
                {
                    throw new ArgumentNullException("Angiven e-mail är ej registrerad i systemet. Vänligen registrera ett konto eller kontrollera angiven e-mail.");
                }

            }
            catch (ArgumentNullException exc)
            {
                Debug.WriteLine(exc.Message);
                await new MessageDialog(exc.ParamName).ShowAsync();
                return new User();
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return new User();
            }
        }

        public async Task DeleteUser(int userId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"users/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    await new MessageDialog("Din användarprofil är nu borttagen. Alla aktiva reservationer är avbokade.").ShowAsync();
                }
                else
                {
                    Debug.WriteLine($"Http Error: {response.StatusCode}. {response.ReasonPhrase}");
                    throw new HttpRequestException();
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
            }
        }

        public async Task DeleteHome(int homeId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"homes/{homeId}");

                if (response.IsSuccessStatusCode)
                {
                    await new MessageDialog("Boendet är nu borttaget. Alla aktiva reservationer är också borttagna.").ShowAsync();
                }
                else
                {
                    Debug.WriteLine($"Http Error: {response.StatusCode}. {response.ReasonPhrase}");
                    throw new HttpRequestException();
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
            }
        }

        public async Task<ObservableCollection<Home>> GetSearchResults(SearchParameterDto searchParameters)
        {
            try
            {
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
        public async Task PostReservation(Reservation reservation)
        {
            try
            {
                //Convert the object to a json string.
                jsonString = JsonConvert.SerializeObject(reservation);

                //Set this part of the code into a scope so we don't have to worry about it not getting disposed.
                using (HttpContent content = new StringContent(jsonString))
                {
                    //Set the type of content
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Call the api and send the Json string.
                    HttpResponseMessage response = await httpClient.PostAsync("reservations", content);

                    //Check if it is successfull. In that case display a message telling the user.
                    //Otherwise throw an error and tell the user that the reservation was not posted.
                    if (response.IsSuccessStatusCode)
                    {
                        await new MessageDialog("Din reservation är nu slutförd!\nHoppas att du kommer trivas i ditt boende!").ShowAsync();
                    }
                    else
                    {
                        Debug.WriteLine($"Http Error: {response.StatusCode}. {response.ReasonPhrase}");
                        throw new HttpRequestException("Reservation ej genomförd. Kontakta administratör");
                    }
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
            }
        }
        public async Task<ObservableCollection<Reservation>> GetUserReservations()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"UsersReservations/{LoginViewModel.Instance.ActiveUser.UserId}");

                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync().Result;

                    var reservations = JsonConvert.DeserializeObject<ObservableCollection<Reservation>>(jsonString);

                    return reservations;
                }
                else if (response.Content == null)
                {
                    throw new HttpRequestException("Här var det tomt! Gå in och gör en reservation för att se listan.");
                }
                else
                {
                    throw new HttpRequestException("Kunde inte hämta några reservationer, var vänlig försök igen.");
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return new ObservableCollection<Reservation>();
            }

        }
        public async Task <Home> GetHome(int id)
        {
           try
            {
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
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return new Home();
            }

        }

        public async Task<bool> PatchReservation(int id, JsonPatchDocument<Reservation>jsonPatchReservation)
        {
            //httpClient.PatchAsync doesn't exist as a predefined method so we have to use SendAsync() which requires a HttpRequestMessage as a parameter
            
            try
            {
                //Name the method Patch
                HttpMethod method = new HttpMethod("PATCH");
                //Serialize the JsonPatchDocument
                jsonString = JsonConvert.SerializeObject(jsonPatchReservation);

                //Set json as content
                HttpContent content = new StringContent(jsonString);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //request
                var request = new HttpRequestMessage(method, new Uri(httpClient.BaseAddress, $"Reservations/{id}"))
                {
                    Content = content
                };

                //Send the request
                using(HttpResponseMessage response = await httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Debug.Write("Reservationen är uppdaterat.");
                        return true;
                    }
                    else
                    {
                        throw new HttpRequestException("Reservationen kunde inte uppdateras, försök igen.");
                        
                    }
                }
            }
            
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return false;
            }
        }
     
        

        public async void DeleteReservation(int reservationId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"reservations/{reservationId}");

                if (response.IsSuccessStatusCode)
                {
                    await new MessageDialog("Reservationen är nu borttagen.").ShowAsync();
                }
                else
                {
                    Debug.WriteLine($"Http Error: {response.StatusCode}. {response.ReasonPhrase}");
                    throw new HttpRequestException();
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
            }
        }

        public async Task<ObservableCollection<Addon>> GetReservationAddon(int id)
        {
            try
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
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return new ObservableCollection<Addon>();
            }

        }
        public async Task<ObservableCollection<Addon>> GetAllAddon ()
        {
            ObservableCollection<Addon> addonList = new ObservableCollection<Addon>();
            try
            {               
                HttpResponseMessage response = await httpClient.GetAsync("Addons");
                if(response.IsSuccessStatusCode)
                {
                     jsonString = response.Content.ReadAsStringAsync().Result;
                     addonList = JsonConvert.DeserializeObject<ObservableCollection<Addon>>(jsonString);
                     //foreach(var a in addon)
                     //{
                     //   addonList.Add(a);
                     //}
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

        public async Task<List<Reservation>> GetAllReservation (int homeId)
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"HomeReservations/{homeId}");
                if(response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync().Result;
                    reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonString);
                    return reservations;
                }
                else
                {
                    throw new HttpRequestException("Gick ej att hämta, vänligen försök igen.");
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);

                return reservations;
            }
        }


        public async Task<Home> PostHome(Home home)
        {
            try
            {
                jsonString = JsonConvert.SerializeObject(home);
                using (HttpContent httpContent = new StringContent(jsonString))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await httpClient.PostAsync("Homes", httpContent);

                    //Check if succesfull
                    if (response.IsSuccessStatusCode)
                    {
                        await new MessageDialog("Boende uppladdat för uthyrning!").ShowAsync();
                    }

                    else
                    {
                        throw new HttpRequestException();
                    }

                    Home postedHome = JsonConvert.DeserializeObject<Home>(jsonString);
                    return postedHome;
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return new Home();
            }
        }

        public async Task<ObservableCollection<Home>> GetUserHomes(int userId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"userhomes/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    jsonString = response.Content.ReadAsStringAsync().Result;
                    var userHomesList = JsonConvert.DeserializeObject<ObservableCollection<Home>>(jsonString);
                    return userHomesList;
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch (Exception exc)
            {
                BasicNoConnectionMessage(exc);
                return new ObservableCollection<Home>();
            }
        }


        private async void BasicNoConnectionMessage(Exception exc)
        {
            Debug.WriteLine(exc.Message);
            await new MessageDialog("Ingen kontakt med servern. Kontakta admin").ShowAsync();
        }

        #endregion
    }
}
