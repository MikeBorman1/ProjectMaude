using CPMA_Core_APP.Model;
using CPMA_Core_APP.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;
using System.Globalization;
using System.Diagnostics;
using Xamarin.Essentials;
using Microsoft.AppCenter.Crashes;
using CPMA_Core_APP.API.Interfaces;
using Xamarin.Forms;
using CPMA_Core_APP.Utils.Interfaces;
using System.Threading.Tasks;

namespace CPMA_Core_APP.ViewModels
{
    public class CollectionInfoPageViewModel : ViewModelBase {

        //Creating an ObservableCollection of Location VM's called Locations
        public ObservableCollection<LocationsVM> Locations { get; set; }

        //Set a delegate command which accepts type LocationVM
        public DelegateCommand<LocationsVM> MapButtonCommand { get; set; }
       
        public DelegateCommand PriceButtonCommand { get; set; }

        public DelegateCommand CollectionTimesButtonCommand { get; set; }
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private bool isLoading;


        private IGetLocations LocationsAPI { get; set; }
        private ISecureStore SecureStore { get; set; }


        public CollectionInfoPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IGetLocations locationsAPI, ISecureStore secureStore)
        : base(navigationService, dialogService)
        {
            LocationsAPI = locationsAPI;
            SecureStore = secureStore;

        }

        public async override void Load()
        {
            //Creates the map button command
            MapButtonCommand = new DelegateCommand<LocationsVM>(MapButton);
            //Creates the price button command
            PriceButtonCommand = new DelegateCommand(PriceButton);
            CollectionTimesButtonCommand = new DelegateCommand(CollectionTimesButton);
            //Runs load locations
            LoadLocations();
        }

        public async void LoadLocations()
        {
            if (IsLoading) 
                return;

            IsLoading = true;
            try
            {
                //Creates an Obvservable Collection of LocationsVMs
                Locations = new ObservableCollection<LocationsVM>();
                var singlelocation = await LocationsAPI.getLocations();
                var singlelocations = new ObservableCollection<LocationsVM>(singlelocation.Select(l => new LocationsVM(l, this)));
                Locations = new ObservableCollection<LocationsVM>(Locations.Concat(singlelocations));
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
            finally
            {
                IsLoading = false;

            }
        }

        public class LocationsVM
        {
            //Sets the attributes of the class
            public CollectionInfoPageViewModel Parent { get; set; }
            public string LocationName{ get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public bool PriceButtonVisible { get; set; }

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            //Constructor of the LocationsVM. Locations Model is parsed in. Locations Model contains all locations data from DB
            public LocationsVM(Locations l, CollectionInfoPageViewModel p)
            {
                //Turns recycle bin into uppercase. F
                LocationName = textInfo.ToTitleCase(l.RecycleBin);
                //Gets Lat Longs from model
                Latitude = l.Latitude;
                Longitude = l.Longitude;
                Parent = p;
                //Checks what type of Location it is. If the location has a price page the price button is visible
                if (LocationName == "Longue Hougue" || LocationName == "Mont Cuet" || LocationName == "Waste Transfer Station")
                {
                    PriceButtonVisible = true;
                }
                else
                {
                    PriceButtonVisible = false;
                }

            }
        }

        public async void MapButton(LocationsVM l)
        {
            //Sets pin latitudes and longitudes
            var location = new Location(l.Latitude, l.Longitude);
            //Gives options. In this case it sets the name
            var options = new MapLaunchOptions { Name = l.LocationName };


            try
            {
                //Trys to open the map
                await Map.OpenAsync(location, options);
            }
            catch
            {
                //If there is an issue a dialog box appears.
                await DialogService.DisplayAlertAsync("Warning", "There was an issue opening the map, do you have a map app installed?", "Ok");
            }
        }

        public void PriceButton()
        {
            //Sets the URL that the price button opens
            Device.OpenUri(new Uri("https://gov.gg/CHttpHandler.ashx?id=116962&p=0"));
        }

        public void CollectionTimesButton()
        {
            Device.OpenUri(new Uri("https://gov.gg/mybinnight"));

        }
    }
}
