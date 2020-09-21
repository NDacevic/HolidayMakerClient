using HolidayMakerClient.Model;
using HolidayMakerClient.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public sealed partial class SelectedLivingView : Page, INotifyPropertyChanged 
    {
        SelectedLivingViewModel selectedLivingViewModel;
        private decimal totalPrice;
        public decimal price;
        public ObservableCollection<Addon> ChosenAddons;

        public event PropertyChangedEventHandler PropertyChanged;
        public decimal TotalPrice
        {
            get => totalPrice;
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPrice)));
                }
            }
        }

        public SelectedLivingView()
        {
            this.InitializeComponent();
            
            selectedLivingViewModel = new SelectedLivingViewModel();
            ChosenAddons = new ObservableCollection<Addon>();
            for (int i = 1; i <= 20; i++)
            {
                combobox_ChangeGuests.Items.Add(i);
            }

        }
        /// <summary>
        /// Recieves a TempReservation object when navigated to
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
          
            selectedLivingViewModel.TempRes = (TempReservation)e.Parameter;
            if(selectedLivingViewModel.TempRes.OldReservation == null)
            {
                selectedLivingViewModel.TempRes.AddonList = new ObservableCollection<Addon>();
                SetUpPage();
            }
            else
            {
                SetUpPageOldReservation();
            }

        }
<<<<<<< HEAD
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.SourcePageType == typeof(MyPageView))
            {
                bttn_bookChange.Content = "Ändra";
                bttn_deleteReservation.Visibility = Visibility.Visible;
            }
            if (e.SourcePageType == typeof(SearchView))
            {
                bttn_bookChange.Content = "Boka";
                bttn_deleteReservation.Visibility = Visibility.Collapsed;
            }

        }
=======

            
        //}
>>>>>>> dev
        /// <summary>
        /// Get list of all addons except for ExtraBed from DB, this because ExtraBed is treated differently from the other addons
        /// </summary>
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
        /// <summary>
        /// Gets Addon object ExtraBed from DB
        /// </summary>
        public void ExtraBed()
        {
            selectedLivingViewModel.GetAddonExtraBed();

        }
        /// <summary>
        /// Prepare page to display correct information, not all homes have the same addons nor price
        /// </summary>
        public void SetUpPage()
        {
<<<<<<< HEAD
           
            price = selectedLivingViewModel.TempRes.Home.Price * (selectedLivingViewModel.TempRes.EndDate - selectedLivingViewModel.TempRes.StartDate).Days;
            TotalPrice = price;
=======
            HomePrice();
            CheckHome();
            bttn_bookChange.Content = "Boka";
            bttn_deleteReservation.Visibility = Visibility.Collapsed;
        }
        public void SetUpPageOldReservation()
        {
            selectedLivingViewModel.TempRes.TempId = selectedLivingViewModel.TempRes.OldReservation.ReservationId;
            selectedLivingViewModel.TempRes.NumberOfGuests = selectedLivingViewModel.TempRes.OldReservation.NumberOfGuests.ToString();
            selectedLivingViewModel.TempRes.StartDate = selectedLivingViewModel.TempRes.OldReservation.StartDate;
            selectedLivingViewModel.TempRes.EndDate = selectedLivingViewModel.TempRes.OldReservation.EndDate;
            //foreach (var ad in selectedLivingViewModel.TempRes.OldReservation.AddonList)
            //{
            //    ChosenAddons.Add(ad);
            //}

            HomePrice();
>>>>>>> dev
            CheckHome();
            UpdatePrice();
            bttn_bookChange.Content = "Ändra";
            bttn_deleteReservation.Visibility = Visibility.Visible;
        }
        public void HomePrice()
        {
            price = selectedLivingViewModel.TempRes.TempHome.Price * (selectedLivingViewModel.TempRes.EndDate - selectedLivingViewModel.TempRes.StartDate).Days;
            TotalPrice = price;
        }
        /// <summary>
        /// Method checks wich addons are available
        /// </summary>
        public void CheckHome()
        {
            if (selectedLivingViewModel.TempRes.TempHome.HasExtraBed == false)
            {
                cb_ExtraBed.Visibility = Visibility.Collapsed;
            }
            if (selectedLivingViewModel.TempRes.TempHome.HasAllInclusive == false)
            {
                Rb_addon0.Visibility = Visibility.Collapsed;
            }
            if (selectedLivingViewModel.TempRes.TempHome.HasFullPension == false)
            {
                Rb_addon1.Visibility = Visibility.Collapsed;
            }
            if (selectedLivingViewModel.TempRes.TempHome.HasHalfPension == false)
            {
                Rb_addon2.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// Removes addons from the ObservableCollection and updates TotalPrice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void bttn_RemoveAddon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChosenAddons.Remove((Addon)lv_DisplayAddons.SelectedItem);
                UpdatePrice();
            }
            catch (Exception)
            {
                await new MessageDialog("Kunde inte ta bort tillval, vänligen försök igen.").ShowAsync();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void bttn_bookChange_Click(object sender, RoutedEventArgs e)
        {
            if (bttn_bookChange.Content.ToString() == "Ändra")
            {
                combobox_ChangeGuests.Visibility = Visibility.Visible;
                selectedLivingViewModel.EditReservation(selectedLivingViewModel.TempRes.OldReservation,Cdp_StartDate.Date.Value,Cdp_EndDate.Date.Value,combobox_ChangeGuests.SelectedValue.ToString());
            }
            else
            {
                try
                {
                    if (LoginViewModel.Instance.ActiveUser == null)
                    {
                        await new LoginView().ShowAsync();
                    }
                    selectedLivingViewModel.CreateReservation(selectedLivingViewModel.TempRes, ChosenAddons, TotalPrice);
                    Frame.Navigate(typeof(MyPageView));
                }
                catch (Exception)
                {
                    await new MessageDialog("Din bokning kunde inte sparas, vänligen testa igen eller kontakta admin.").ShowAsync();
                    return;
                }
            }
   
        }

       

        private void bttn_deleteReservation_Click(object sender, RoutedEventArgs e)
        {
            selectedLivingViewModel.DeleteReservation(selectedLivingViewModel.TempRes);
            Frame.Navigate(typeof(MyPageView));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AddonList();
            ExtraBed();
        }
        /// <summary>
        /// Adding the chosen addon to the ObservableCollection ChosenAddons and updates TotalPrice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(Rb_addon0.IsChecked == true)
            {
                foreach (var a in selectedLivingViewModel.AddonList)
                {
                    if (a.AddonType == "All-inclusive") ChosenAddons.Add(a);
                }               
            }
            if (Rb_addon1.IsChecked == true)
            {
                foreach (var a in selectedLivingViewModel.AddonList)
                {
                    if (a.AddonType == "Helpension") ChosenAddons.Add(a);
                }
            }
            if (Rb_addon2.IsChecked == true)
            {
                foreach (var a in selectedLivingViewModel.AddonList)
                {
                    if (a.AddonType == "Halvpension") ChosenAddons.Add(a);
                }
            }
            UpdatePrice();
        }
        /// <summary>
        /// Adding ExtraBed to the ObservableCollection ChosenAddons and updates TotalPrice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cb_ExtraBed_Checked(object sender, RoutedEventArgs e)
        {
            ChosenAddons.Add((Addon)selectedLivingViewModel.ExtraBed);
            UpdatePrice();
        }
        /// <summary>
        /// Removes ExtraBed from ObservableCollection ChosenAdoons 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cb_ExtraBed_Unchecked(object sender, RoutedEventArgs e)
        {
            ChosenAddons.Remove((Addon)selectedLivingViewModel.ExtraBed);
            UpdatePrice();

        }

        private void Rb_addon_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Rb_addon0.IsChecked == false)
            {
                foreach (var a in selectedLivingViewModel.AddonList)
                {
                    if (a.AddonType == "All-inclusive") ChosenAddons.Remove(a);
                }
            }
            if (Rb_addon1.IsChecked == false)
            {
                foreach (var a in selectedLivingViewModel.AddonList)
                {
                    if (a.AddonType == "Helpension") ChosenAddons.Remove(a);
                }
            }
            if (Rb_addon2.IsChecked == false)
            {
                foreach (var a in selectedLivingViewModel.AddonList)
                {
                    if (a.AddonType == "Halvpension") ChosenAddons.Remove(a);
                }
            }
            UpdatePrice();

        }
        /// <summary>
        /// Update TotalPrice for home plus addons
        /// </summary>
        public void UpdatePrice ()
        {
            if(ChosenAddons.Count == 0)
            {
                TotalPrice = price;
            }
            else
            {
                foreach(var ad in ChosenAddons)
                {
                    if(ad.AddonType != "Extrasäng")
                    {
                        TotalPrice += ad.AddonPrice * int.Parse(selectedLivingViewModel.TempRes.NumberOfGuests);
                    }
                    else
                    {
                        TotalPrice += ad.AddonPrice;
                    }
                } 
            }

        }

        private void Bttn_ToSearch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchView));
        }
    }
}
