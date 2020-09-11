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
            Reservation r1 = new Reservation();
            Reservation r2 = new Reservation();
            Reservation r3 = new Reservation();
            r1.TotalPrice = 2000;
            r1.HomeId = 1;
            r2.TotalPrice = 4000;
            r2.HomeId = 2;
            r3.TotalPrice = 15000;
            r3.HomeId = 4;
            MyReservations = new ObservableCollection<Reservation>();
            MyReservations.Add(r1);
            MyReservations.Add(r2);
            MyReservations.Add(r3);
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        ObservableCollection<Reservation>MyReservations { get; set; }
        #endregion

        #region Methods
        #endregion

    }
}
