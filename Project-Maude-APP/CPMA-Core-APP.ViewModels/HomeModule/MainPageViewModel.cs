using CPMA_Core_APP.Utils;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;
using CPMA_Core_APP.Utils.Interfaces;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CPMA_Core_APP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        // delegate actions buttons and what they do i.e.: s
        //public DelegateCommand DoSomethingCommand { get; set; }
        public DelegateCommand<string> SearchBarCommand { get; set; }
        public DelegateCommand ScanBarcodeButtonCommand { get; set; }
        public DelegateCommand CreditsButtonCommand { get; set; }
        public string SearchBarText { get; set; } = "";
        public string Endpoint { get; set; }
        public bool IsEnabled { get; set; } = true;

        private ISecureStore SecureStore { get; set; }
        public DelegateCommand SettingsButtonCommand{get; set;}



        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISecureStore secureStore)
            : base(navigationService, dialogService)
        {
            SecureStore = secureStore;

        }

        public async override void Load()
        {
            Title = Titles.MainPageTitle;
            //Loading endpoint for API Calls
            Endpoint = await API.ApiEndpoints.EndpointBase();
            ////Load page data and delegate commands
            ///Sets a new delegate command which takes type string
            SearchBarCommand = new DelegateCommand<string>(SearchBar);
            ScanBarcodeButtonCommand = new DelegateCommand(ScanBarcodeButton);
            SettingsButtonCommand = new DelegateCommand(SettingsButton);
            //Runs checkpostcode function
            CreditsButtonCommand = new DelegateCommand(CreditsButton);
        }

        

        public async void SearchBar(string sC)
        {
            SearchBarText = "";
            //Sets up new nav parameters. This is what is parsed when navigating from page to page
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, sC }
            };
            //Navigates user to SearchResult page
            await NavUtils.NavigateTo(Pages.Names.SearchResultsPageName, NavigationService, par);
            
        }
        public  async void ScanBarcodeButton()
        {
            //Request Permissions
            var permissionsToRequest = new List<Permission> { };
            if (!await PermissionsUtils.CheckPermission(Permission.Camera))
            {
               permissionsToRequest.Add(Permission.Camera);
            }
            if (!await PermissionsUtils.CheckPermission(Permission.Storage))
            {
                permissionsToRequest.Add(Permission.Storage);
            }
            await CrossPermissions.Current.RequestPermissionsAsync(permissionsToRequest.ToArray());

            //Check Permissions
            if (!IsEnabled || !await PermissionsUtils.CheckPermission(Permission.Camera) || !await PermissionsUtils.CheckPermission(Permission.Storage))
            {
                return;
            }

            IsEnabled = false;

            try
            {
                //Required for permissions
                await Task.Delay(5);
                //New Navigation parameters for navigating user to scanner pages
                var par = new NavigationParameters
                {
                    { ParameterKeys.responseType, 0 }
                };
                await NavUtils.NavigateTo(Pages.Names.ScannerPage, NavigationService, par); 
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }



            finally
            {
                IsEnabled = true;
            }
            
          
        }

        public async void SettingsButton()
        {
            var par = new NavigationParameters {};
            //Navigates user to the settings page
            await NavUtils.NavigateTo(Pages.Names.SettingsPageName, NavigationService, par);
        }


        public async void CreditsButton()
        {
            await NavUtils.NavigateTo(Pages.Names.CreditsPageName, NavigationService, new NavigationParameters());
        }
    }
}

