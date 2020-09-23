using HolidayMakerClient.Model;
using HolidayMakerClient.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;

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
        private HashSet<DateTimeOffset> InvalidDates = new HashSet<DateTimeOffset>();

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


        }
        public void SetMaxNumberOfGuests()
        {
            if(selectedLivingViewModel.TempRes.TempHome.HasExtraBed == true)
            {
                for (int i = 1; i <= selectedLivingViewModel.TempRes.TempHome.NumberOfBeds+1; i++)
                {
                    combobox_ChangeGuests.Items.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= selectedLivingViewModel.TempRes.TempHome.NumberOfBeds; i++)
                {
                    combobox_ChangeGuests.Items.Add(i);
                }
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
            try
            {
                selectedLivingViewModel.GetAddonExtraBed();
            }
            catch
            {
                return;
            }
            

        }
        /// <summary>
        /// Prepare page to display correct information, not all homes have the same addons nor price
        /// </summary>
        public void SetUpPage()
        {
            HomePrice();
            CheckHome();
            GetDates();
            SetMaxNumberOfGuests();
            Bttn_bookChange.Content = "Boka";
            Bttn_deleteReservation.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Prepare page when navigated from MyPage
        /// </summary>
        public void SetUpPageOldReservation()
        {
            selectedLivingViewModel.TempRes.TempId = selectedLivingViewModel.TempRes.OldReservation.ReservationId;
            selectedLivingViewModel.TempRes.NumberOfGuests = selectedLivingViewModel.TempRes.OldReservation.NumberOfGuests.ToString();
            selectedLivingViewModel.TempRes.StartDate = selectedLivingViewModel.TempRes.OldReservation.StartDate;
            selectedLivingViewModel.TempRes.EndDate = selectedLivingViewModel.TempRes.OldReservation.EndDate;
            foreach (var ad in selectedLivingViewModel.TempRes.OldReservation.AddonList)
            {
                ChosenAddons.Add(ad);
            }
            
            HomePrice();
            CheckHome();
            UpdatePrice();

            OldReservationCheckbox();

            GetDates();

            SetMaxNumberOfGuests();

            Bttn_bookChange.Content = "Ändra";
            combobox_ChangeGuests.Visibility = Visibility.Visible;
            Bttn_deleteReservation.Visibility = Visibility.Visible;

        }

        public void OldReservationCheckbox()
        {
            
            foreach (var addon in ChosenAddons.ToList())
            {
                if(addon.AddonType=="Extrasäng")
                {
                    Cb_ExtraBed.Checked -= Cb_ExtraBed_Checked;
                    Cb_ExtraBed.IsChecked = true;
                    Cb_ExtraBed.Checked += Cb_ExtraBed_Checked;
                }
                if (addon.AddonType == "Halvpension")
                {
                    Rb_addon2.Checked -= RadioButton_Checked;
                    Rb_addon2.IsChecked = true;
                    Rb_addon2.Checked += RadioButton_Checked;
                }
                if (addon.AddonType == "Helpension")
                {
                    Rb_addon1.Checked -= RadioButton_Checked;
                    Rb_addon1.IsChecked = true;
                    Rb_addon1.Checked += RadioButton_Checked;
                }
                if (addon.AddonType == "All-inclusive")
                {
                    Rb_addon0.Checked -= RadioButton_Checked;
                    Rb_addon0.IsChecked = true;
                    Rb_addon0.Checked += RadioButton_Checked;
                }
            }
            
        }

        /// <summary>
        /// Price for selected home multiplied with how many days
        /// </summary>

        public void HomePrice()
        {
            price = selectedLivingViewModel.TempRes.TempHome.Price * (selectedLivingViewModel.TempRes.EndDate - selectedLivingViewModel.TempRes.StartDate).Days;
            TotalPrice = price;
            UpdatePrice();
        }
        /// <summary>
        /// Method checks wich addons are available for the selected home
        /// </summary>
        public void CheckHome()
        {
            if (selectedLivingViewModel.TempRes.TempHome.HasExtraBed == false)
            {
                Cb_ExtraBed.Visibility = Visibility.Collapsed;
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

        private async void Bttn_RemoveAddon_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                Addon ad = ((Addon)lv_DisplayAddons.SelectedItem);

                if (ad.AddonType == "Extrasäng") Cb_ExtraBed.IsChecked = false;

                if (ad.AddonType == "Extrasäng") 
                    Cb_ExtraBed.IsChecked = false;

                else if (ad.AddonType == "All-inclusive" || ad.AddonType == "Helpension" || ad.AddonType == "Halvpension") Rb_noPension.IsChecked = true;

                ChosenAddons.Remove((Addon)lv_DisplayAddons.SelectedItem);

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

        private async void Bttn_bookChange_Click(object sender, RoutedEventArgs e)
        {
            string numberOfGuests;
            if (Bttn_bookChange.Content.ToString() == "Ändra")
            {
                if (combobox_ChangeGuests.SelectedValue == null)
                {
                    numberOfGuests = selectedLivingViewModel.TempRes.NumberOfGuests.ToString();
                }
                else
                {
                    numberOfGuests = combobox_ChangeGuests.SelectedValue.ToString();
                }
                selectedLivingViewModel.EditReservation(selectedLivingViewModel.TempRes.OldReservation,Cdp_StartDate.Date.Value,Cdp_EndDate.Date.Value, totalPrice,ChosenAddons ,numberOfGuests);
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
 

        /// <summary>
        /// Delete reservation when navigating from MyPageView with chosen reservation. When navigated from SearchView the button is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Bttn_deleteReservation_Click(object sender, RoutedEventArgs e)

        {
            selectedLivingViewModel.DeleteReservation(selectedLivingViewModel.TempRes);
            Frame.Navigate(typeof(MyPageView));
        }
        /// <summary>
        /// Gets the list of addons plus the addon ExtraBed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
        private void Rb_noPension_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePrice();
        }
        /// <summary>
        /// Adding ExtraBed to the ObservableCollection ChosenAddons and updates TotalPrice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Cb_ExtraBed_Checked(object sender, RoutedEventArgs e)
        {
            ChosenAddons.Add((Addon)selectedLivingViewModel.ExtraBed);
            UpdatePrice();
        }
        /// <summary>
        /// Removes ExtraBed from ObservableCollection ChosenAdoons 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Cb_ExtraBed_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach(var a in ChosenAddons.ToList())
            {
                if(a.AddonType=="Extrasäng")
                {
                    ChosenAddons.Remove(a);
                }
            }
            //ChosenAddons.Remove((Addon)selectedLivingViewModel.ExtraBed);
            UpdatePrice();
            

        }
        /// <summary>
        /// If RadioButtens is unchecked or switching between them, correct Totalprice is shown 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Rb_addon_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach(var a in ChosenAddons.ToList())
            {
                if(a.AddonType!="Extrasäng")
                {
                    if (e.OriginalSource == Rb_addon0 || e.OriginalSource == Rb_addon1 || e.OriginalSource == Rb_addon2)
                    {
                        ChosenAddons.Remove(a);
                        UpdatePrice();
                    }
                }
            
            }


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
                decimal addonPrice = 0;
                foreach(var ad in ChosenAddons)
                {
                    if(ad.AddonType != "Extrasäng")
                    {
                        addonPrice += ad.AddonPrice * int.Parse(selectedLivingViewModel.TempRes.NumberOfGuests);
                    }
                    else
                    {
                        addonPrice += ad.AddonPrice;
                    }
                }
                TotalPrice = price + addonPrice;
            }

        }

         /// <summary>
         /// Get the invalid dates for placing in the hash to blackout the already reserved dates
         /// </summary>
        public async void GetDates()
        {
            await selectedLivingViewModel.GetHomeReservation(selectedLivingViewModel.TempRes.TempHome.HomeId);
            selectedLivingViewModel.SetInvalidDates();
            SetInvalidList();

        }
        /// <summary>
        /// Place dates in the hash with invalid dates
        /// </summary>
        public void SetInvalidList()
        {
            if(Bttn_bookChange.Content.ToString() == "Boka")
            {
                foreach (var date in selectedLivingViewModel.InvalidDates)
                {
                
                    InvalidDates.Add(date.Date.Date);
                }
            }
            else
            {
                foreach (var date in selectedLivingViewModel.InvalidDates)
                {

                    InvalidDates.Add(date.Date.Date);
                    for (int i = 0; i <= (selectedLivingViewModel.TempRes.EndDate.Date.Subtract(selectedLivingViewModel.TempRes.StartDate.Date)).Days; i++)
                    {
                        InvalidDates.Remove(selectedLivingViewModel.TempRes.StartDate.AddDays(i).Date);
                    }
                }
            }

        }
        /// <summary>
        /// When trying to change date the invalid dates is blacked out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Cdp_StartDate_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs e)
        {
          
            e.Item.IsBlackout = InvalidDates.Contains(e.Item.Date.Date);

            if (e.Item.Date < DateTime.Today)
            {
                e.Item.IsBlackout = true;

            }

        }
        /// <summary>
        /// Navigate back to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Bttn_GoBack_Click_(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private void Cdp_EndDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate.Value!=selectedLivingViewModel.TempRes.EndDate)
            {
                selectedLivingViewModel.TempRes.EndDate = args.NewDate.Value;
                HomePrice();
            }

        }

        private void Cdp_StartDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate.Value != selectedLivingViewModel.TempRes.StartDate)
            {
                selectedLivingViewModel.TempRes.StartDate = args.NewDate.Value;
                HomePrice();
            }
        }


    }
}
