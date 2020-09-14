using HolidayMakerClient.Model;
using System;
using System.Collections.Generic;
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
    public sealed partial class SearchView : Page
    {
        #region Constant Fields
        #endregion

        #region Fields
        private List<Home> testSearchList;
        #endregion

        #region Constructors
        public SearchView()
        {
            this.InitializeComponent();
            testSearchList = new List<Home>();
            PopulateListView();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void ShowHideAdvancedSearch(object sender, RoutedEventArgs args)
        {
            if (stckPnl_AdvancedSearch.Visibility == Visibility.Collapsed)
                stckPnl_AdvancedSearch.Visibility = Visibility.Visible;
            else
                stckPnl_AdvancedSearch.Visibility = Visibility.Collapsed;
        }
        private void PopulateListView()
        {
            for (int i = 0; i < 10; i++)
            {
                testSearchList.Add(new Home(1, "Hotel", i, "Sweden", 3299, true, false, true, "ms-appx:///Assets/hotelroom.jpg", true, false, false, true, 10, 5, i, true, false, "An awesome hotelroom", 15, 85));
            }
        }
        #endregion

    }
}
