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
        private UploadLivingViewModel uploadLivingViewModel;
        #endregion

        #region Constructors
        public UploadLivingView()
        {
            this.InitializeComponent();
            uploadLivingViewModel = new UploadLivingViewModel();
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
            if (LoginViewModel.Instance.ActiveUser.IsCompany)
            {
                grid_BusinessExtras.Visibility = Visibility.Visible;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbb_HomeType.ItemsSource = _enumval.ToList();
        }

        private async void bttn_UploadLiving_Click(object sender, RoutedEventArgs e)
        {
            await uploadLivingViewModel.CreateNewHome(
                cbb_HomeType.SelectedItem.ToString(), 
                int.Parse(tb_NumberOfRooms.Text), 
                tb_Location.Text, 
                int.Parse(tb_Price.Text), 
                cb_HasBalcony.IsChecked.Value, 
                cb_AllowsPets.IsChecked.Value, 
                cb_HasWifi.IsChecked.Value, 
                cb_HasHalfPension.IsChecked.Value, 
                cb_HasFullPension.IsChecked.Value, 
                cb_HasAllInclusive.IsChecked.Value, 
                cb_HasExtrabed.IsChecked.Value, 
                int.Parse(tb_CityDistance.Text),
                int.Parse(tb_BeachDistance.Text), 
                int.Parse(tb_NumberOfBeds.Text), 
                cb_HasPool.IsChecked.Value, 
                cb_AllowsSmoking.IsChecked.Value, 
                tb_Description.Text);

            NavigateBack();
        }

        private void bttn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }        

        private void NavigateBack()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void NumericTextboxesBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            //if any written text is not a number, don't allow the entry to be displayed
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));

            //always move cursor to the end of all entered text
            if (!args.Cancel && args.NewText.Length == 1 && sender.Text == "")
                sender.SelectionStart = args.NewText.Length + 1;
        }
        #endregion



    }
}
