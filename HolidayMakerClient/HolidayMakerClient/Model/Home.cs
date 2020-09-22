using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMakerClient.Model
{
    public class Home : INotifyPropertyChanged
    {
        #region Constant Fields
        #endregion

        #region Fields
        private double averageRating;
        #endregion

        #region Constructors
        public Home(string homeType, int rooms,string location, decimal price,bool hasBalcony,bool allowPets,bool hasWifi,bool hasHalfPension,bool hasFullPension, bool hasAllInclusive, bool hasExtraBed,int cityDistance,int beachDistance,int numberOfBeds,bool hasPool,bool allowSmoking,string description)
        {
            HomeType = homeType;
            Rooms = rooms;
            Location = location;
            Price = price;
            HasBalcony = hasBalcony;
            AllowPets = allowPets;
            HasWifi = hasWifi;
            Image = "ms-appx:///Assets/hotelroom.jpg";
            HasHalfPension = hasHalfPension;
            HasFullPension = hasFullPension;
            HasAllInclusive = hasAllInclusive;
            HasExtraBed = hasExtraBed;
            CityDistance = cityDistance;
            BeachDistance = beachDistance;
            NumberOfBeds = numberOfBeds;
            HasPool = hasPool;
            AllowSmoking = allowSmoking;
            Description = description;
            NumberOfRatings = 0;
            SumOfRatings = 0;
        }
        public Home()
        {
        }
        #endregion

        #region Delegates
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public int HomeId { get; set; }
        public string HomeType { get; set; }
        public int Rooms { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public bool HasBalcony { get; set; }
        public bool AllowPets { get; set; }
        public bool HasWifi { get; set; }
        public string Image { get; set; }
        public bool HasHalfPension { get; set; }
        public bool HasFullPension { get; set; }
        public bool HasAllInclusive { get; set; }
        public bool HasExtraBed { get; set; }
        public int CityDistance { get; set; }
        public int BeachDistance { get; set; }
        public int NumberOfBeds { get; set; }
        public bool HasPool { get; set; }
        public bool AllowSmoking { get; set; }
        public string Description { get; set; }
        public int NumberOfRatings { get; set; }
        public int SumOfRatings { get; set; }
        public double AverageRating { get => averageRating; 
            set
            { 
                averageRating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AverageRating"));
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
