using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HolidayMakerClient.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyPageView : Page
    {
        #region Constant Fields
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public MyPageView()
        {
            this.InitializeComponent();
            Addon a1 = new Addon();
            a1.AddonType = "Extra säng";
            a1.AddonPrice = 200;
            Addon a2 = new Addon();
            a2.AddonType = "Champagne";
            a2.AddonPrice = 100;
            Reservation r1 = new Reservation();
            Home h1 = new Home();
            h1.HomeType = "Lägenhet";
            h1.Rooms = 2;
            h1.AllowPets = true;
            h1.AllowSmoking = false;
            h1.Description = "Mysig 2:a, perfekt för den som vill vara sig själv för en stund";
            h1.HasBalcony = true;
            h1.HasPool = true;

            r1.TotalPrice = 2000;
            r1.HomeId = 1;
     
            r1.AddonList = new List<Addon>();
            r1.AddonList.Add(a1);
            r1.AddonList.Add(a2);
            
            SelectedReservationAddon = new ObservableCollection<Addon>();
            SelectedReservation = new ObservableCollection<Reservation>();
            SelectedHome = new ObservableCollection<Home>();
            SelectedHome.Add(h1);
            MyReservations = new ObservableCollection<Reservation>();
            MyReservations.Add(r1);
            
        

        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
       public ObservableCollection<Reservation>MyReservations { get; set; }
       public ObservableCollection<Addon> SelectedReservationAddon { get; set; }
       public ObservableCollection<Reservation> SelectedReservation { get; set; }
        public ObservableCollection<Home> SelectedHome { get; set; }

        #endregion

        #region Methods
        #endregion

        private void Lv_MyReservations_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var selectedItem = Lv_MyReservations.SelectedItems;

                 foreach(Reservation r in selectedItem)
                 {
                SelectedReservation.Add(r);
                    foreach(Addon a in r.AddonList)
                     {
                         SelectedReservationAddon.Add(a);
                     }
               
                  }
        }

        private void bttn_CancelReservation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bttn_ChangeReservation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
