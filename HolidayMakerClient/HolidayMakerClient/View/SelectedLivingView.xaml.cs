using HolidayMakerClient.Model;
using HolidayMakerClient.View;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HolidayMakerClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectedLivingView : Page
    {
        SelectedLivingViewModel selectedLivingViewModel;
        public decimal price;
        public ObservableCollection<Addon> ChosenAddons;
        public SelectedLivingView()
        {
            this.InitializeComponent();
            selectedLivingViewModel = new SelectedLivingViewModel();
            ChosenAddons = new ObservableCollection<Addon>();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedLivingViewModel.TempRes = (TempReservation)e.Parameter;
            selectedLivingViewModel.TempRes.AddonList = new ObservableCollection<Addon>();
            GetTotalPrice();

        }
        public void AddonList()
        {
            try
            {
                 selectedLivingViewModel.GetAddonList();

            }
            catch (Exception)
            {
                return;
            }          
            
        }
        public void ExtraBed()
        {
            selectedLivingViewModel.GetAddonExtraBed();

        }
        public void GetTotalPrice()
        {
           price = selectedLivingViewModel.TempRes.Home.Price * (selectedLivingViewModel.TempRes.EndDate - selectedLivingViewModel.TempRes.StartDate).Days;
            if (selectedLivingViewModel.TempRes.Home.HasExtraBed == false)
            {
                cb_ExtraBed.Visibility = Visibility.Collapsed;
            }
        }

        //private async void cb_Addon_Checked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        selectedLivingViewModel.TempRes.AddonList.Add((Addon)lv_ChooseAddons.SelectedItem);
        //        GetTotalPrice();
        //    }
        //    catch (Exception)
        //    {
        //        await new MessageDialog("Kunde inte läggas till tillägg.").ShowAsync();
        //    }

        //}
        //public async void GetTotalPrice()
        //{
        //    try
        //    {
        //       selectedLivingViewModel.TempRes.TotalPrice = selectedLivingViewModel.TempTotalPrice(selectedLivingViewModel.TempRes);
        //    }
        //    catch (Exception)
        //    {
        //        await new MessageDialog("Något gick fel, kan inte visa totalpriset.").ShowAsync();
        //    }

        //}

        private async void bttn_RemoveAddon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedLivingViewModel.TempRes.AddonList.Remove((Addon)lv_DisplayAddons.SelectedItem);
            }
            catch (Exception)
            {
                await new MessageDialog("Kunde inte ta bort tillval, vänligen försök igen.").ShowAsync();
            }

        }

        private async void bttn_bookChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedLivingViewModel.CreateReservation(selectedLivingViewModel.TempRes);
                await new MessageDialog("Din bokning är skapad.").ShowAsync();
                Frame.Navigate(typeof(MyPageView));
            }
            catch (Exception)
            {
                await new MessageDialog("Din bokning kunde inte sparas, vänligen testa igen eller kontakta admin.").ShowAsync();
                return;
            }
        }


        private void bttn_deleteReservation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AddonList();
            ExtraBed();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void cb_ExtraBed_Checked(object sender, RoutedEventArgs e)
        {
            ChosenAddons.Add((Addon)selectedLivingViewModel.ExtraBed);
        }

        private void cb_ExtraBed_Unchecked(object sender, RoutedEventArgs e)
        {
            ChosenAddons.Remove((Addon)selectedLivingViewModel.ExtraBed);
        }

    }
}
