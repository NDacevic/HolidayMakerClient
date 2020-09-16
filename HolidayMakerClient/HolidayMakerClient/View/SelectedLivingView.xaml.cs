using HolidayMakerClient.Model;
using HolidayMakerClient.View;
using System;
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
        public SelectedLivingView()
        {
            this.InitializeComponent();
            selectedLivingViewModel = new SelectedLivingViewModel();
            AddonList();
            GetTotalPrice();
           
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedLivingViewModel.TempRes = (TempReservation)e.Parameter;

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

        private async void cb_Addon_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedLivingViewModel.TempRes.AddonList.Add((Addon)lv_ChooseAddons.SelectedItem);
                GetTotalPrice();
            }
            catch (Exception)
            {
                await new MessageDialog("Kunde inte läggas till tillägg.").ShowAsync();
            }

        }
        public async void GetTotalPrice()
        {
            try
            {
               selectedLivingViewModel.TempRes.TotalPrice = selectedLivingViewModel.TempTotalPrice(selectedLivingViewModel.TempRes);
            }
            catch (Exception)
            {
                await new MessageDialog("Något gick fel, kan inte visa totalpriset.").ShowAsync();
            }

        }

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
    }
}
