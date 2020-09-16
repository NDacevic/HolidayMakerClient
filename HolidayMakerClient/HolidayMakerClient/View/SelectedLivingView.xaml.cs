using HolidayMakerClient.Model;
using HolidayMakerClient.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HolidayMakerClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectedLivingView : Page
    {
        SelectedLivingViewModel SelectedLivingViewModel; 
        public SelectedLivingView()
        {
            this.InitializeComponent();
            GetAddonList();
            SelectedLivingViewModel.TempTotalPrice();
           
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SelectedLivingViewModel.TempReservation = (TempReservation)e.Parameter;

        }
        public void GetAddonList()
        {
            try
            {
                SelectedLivingViewModel.GetAddonList();
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
                SelectedLivingViewModel.TempReservation.AddonList.Add((Addon)lv_ChooseAddons.SelectedItem);
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
                SelectedLivingViewModel.TempTotalPrice();
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
                SelectedLivingViewModel.TempReservation.AddonList.Remove((Addon)lv_DisplayAddons.SelectedItem);
            }
            catch (Exception)
            {
                await new MessageDialog("Kunde inte ta bort tillval, vänligen försök ien.").ShowAsync();
            }
            
        }

        private async void bttn_bookChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedLivingViewModel.CreateReservation();
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
