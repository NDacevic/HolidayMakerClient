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
            Reservation r2 = new Reservation();
            Reservation r3 = new Reservation();
            Reservation r4 = new Reservation();
            Reservation r5 = new Reservation();
            r1.TotalPrice = 2000;
            r1.HomeId = 1;
            r2.TotalPrice = 4000;
            r2.HomeId = 2;
            r3.TotalPrice = 15000;
            r3.HomeId = 3;
            r4.TotalPrice = 6000;
            r4.HomeId = 4;
            r5.TotalPrice = 18000;
            r5.HomeId = 5;
            r1.AddonList = new List<Addon>();
            r1.AddonList.Add(a1);
            r1.AddonList.Add(a2);

            SelectedReservationAddon = new ObservableCollection<Addon>();
            SelectedReservation = new ObservableCollection<Reservation>();

            MyReservations = new ObservableCollection<Reservation>();
            MyReservations.Add(r1);
            MyReservations.Add(r2);
            MyReservations.Add(r3);
            MyReservations.Add(r4);
            MyReservations.Add(r5);

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
    }
}
