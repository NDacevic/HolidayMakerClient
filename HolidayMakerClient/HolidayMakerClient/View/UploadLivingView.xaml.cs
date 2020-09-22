using HolidayMakerClient.ViewModel;
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
    public sealed partial class UploadLivingView : Page
    {

        #region Constant Fields
        #endregion

        #region Fields
        private IEnumerable<HomeType> _enumval;
        #endregion

        #region Constructors
        public UploadLivingView()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _enumval = Enum.GetValues(typeof(HomeType)).Cast<HomeType>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cb_HomeType.ItemsSource = _enumval.ToList();
        }

        private void bttn_UploadLiving_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bttn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }        
        #endregion
    }
}
