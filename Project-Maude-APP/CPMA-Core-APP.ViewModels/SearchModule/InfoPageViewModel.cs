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
using CPMA_Core_APP.API;

namespace CPMA_Core_APP.ViewModels
{
    public class InfoPageViewModel : ViewModelBase
    {
        public ObservableCollection<MaterialsVM> Materials { get; set; }
        public ObservableCollection<string> Warnings { get; set; }
        public SearchResult Model { get; set; }
        public string ProductImage { get; set; }

        public string IsVerified { get; set; }
        public bool Flagged { get; set; }
        public bool NotFlagged { get; set; }
        public string FlagText { get; set; } = "Flag";
        public string BinColour { get; set; } = "White";
        public Material Material { get; set; }


        public DelegateCommand<MaterialsVM> MapButtonCommand { get; set; }
        public DelegateCommand FlagCommand { get; set; }

        private IGetLocations LocationsAPI { get; set; }
        private ISetFlag FlagAPI { get; set; }

        public InfoPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IGetLocations locationsAPI, ISetFlag flagAPI)
        : base(navigationService, dialogService)
        {
            LocationsAPI = locationsAPI;
            FlagAPI = flagAPI;
        }

        public override async void Load(INavigationParameters parameters)
        {
            try
            {
                if (parameters.ContainsKey(ParameterKeys.searchResult))
                {
                    var textInfo = new CultureInfo("en-US", false).TextInfo;

                    //Load search result model from parameter
                    Model = parameters.GetValue<SearchResult>(ParameterKeys.searchResult); //Get from API
                    Title = textInfo.ToTitleCase(Model.ProductName); //pg name

                    Materials = new ObservableCollection<MaterialsVM>(Model.Materials.Select(m => new MaterialsVM(m, this)).ToList());
                    var Names = Model.Materials.Select(m => textInfo.ToTitleCase(m.Name));
                    ProductImage = "https://inthebag.blob.core.windows.net/app-images/products/" + Model.ProductPhotoID.ToString();

                    MapButtonCommand = new DelegateCommand<MaterialsVM>(MapButton);
                    FlagCommand = new DelegateCommand(Flag);
                    if (Model.IsVerified)
                    {
                        IsVerified = "This item has been verified to be correct.";
                    }
                    else
                    {
                        IsVerified = "This item has not yet been verified";
                    }

                    if (Model.Flagged)
                    {
                        Flagged = true;
                        NotFlagged = false;
                        FlagText = "";
                    }
                    else
                    {
                        Flagged = false;
                        NotFlagged = true;
                    }

                }
                else
                {
                    //Throw warning 
                    await NavigationService.GoBackAsync();
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }


        public string FormatRemaining(string item, char delimiter)
        {
            var split = item.Split(delimiter).ToList();
            split.RemoveAt(0);
            split[0] = split[0].TrimStart(' ');
            return String.Join(".", split);
        }

        public class MaterialsVM
        {
            public Material Material { get; set; }
            public InfoPageViewModel Parent { get; set; }
            public string MaterialName { get; set; }
            public string MaterialImage { get; set; }
            public string Location { get; set; }
            public bool MapButtonVisible { get; set; }
            public ObservableCollection<string> Warnings { get; set; }
            public string RecycleBin { get; set; }
            public int RecycleCodeID { get; set; }
       
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            

            public MaterialsVM(Material m, InfoPageViewModel p)
            {
                var textInfo = new CultureInfo("en-US", false).TextInfo;
                Material = m;
                MaterialName = textInfo.ToTitleCase(m.Name);
                MaterialImage = "https://inthebag.blob.core.windows.net/app-images/materials/" + textInfo.ToLower(m.ImageUrl.ToString());
                Warnings = new ObservableCollection<string>(m.Warnings.Select(w => textInfo.ToTitleCase(w.WarningMessage)).ToList().Distinct());
                if (m.IsBin && m.Recyclable)
                {
                    Location = String.Format("This can be recycled! It should be put in the {0} bin", textInfo.ToLower(m.RecycleBin));
                    MapButtonVisible = !m.IsBin;
                }
                else if (m.IsBin && !m.Recyclable)
                {
                    Location = String.Format("This cannot be recycled! It should be put in the {0} bin", textInfo.ToLower(m.RecycleBin));
                    MapButtonVisible = !m.IsBin;
                }
                else if (textInfo.ToTitleCase(m.RecycleBin) == "Contact States Works")
                {
                    Location = String.Format("To recycle this please contact States Works on : 01481 246263");
                }
                else if (textInfo.ToTitleCase(m.RecycleBin) == "Vehicle Disposal Scheme")
                {
                    Location = String.Format("To take part in the End of Life Vehicle scheme, please access the States of Guernsey website. https://www.gov.gg/bulkrefuse");
                }
                else
                {
                    Location = String.Format("This needs to be taken to {0}", textInfo.ToTitleCase(m.RecycleBin));
                    MapButtonVisible = !m.IsBin;
                }
                var a = MapButtonVisible;

                Parent = p;
                RecycleBin = m.RecycleBin;
                RecycleCodeID = m.RecycleCode;
                Location = m.RecycleBin;
                

                LoadLocations(RecycleCodeID, this);
            }

            protected async void LoadLocations(int RecycleCode, MaterialsVM m)
            {
                var location = await this.Parent.LocationsAPI.getRecycleCodeLocation(RecycleCode);

                if(location.Count() > 0)
                {
                    var LatLong = new ObservableCollection<Locations>(location);
                    m.Latitude = LatLong.First().Latitude;
                    m.Longitude = LatLong.First().Longitude;
                }
            }
        }


        public async void MapButton(MaterialsVM m)
        { 
            var location = new Location(m.Latitude, m.Longitude);
            var options = new MapLaunchOptions { Name = m.RecycleBin };
            

            try
            {
                await Map.OpenAsync(location, options);
            }
            catch
            {
                await DialogService.DisplayAlertAsync("Warning", "There was an issue opening the map, do you have a map app installed?", "Ok");
            }
        }

        public async void Flag()
        {
            await FlagAPI.SetFlag(Model.ProductID.ToString());
            await DialogService.DisplayAlertAsync("Flagged", "You have flagged this report for inspection", "Ok");
        }
    }
}
