using CPMA_Core_APP.Utils;
using CPMA_Core_APP.Utils.Interfaces;
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;

namespace CPMA_Core_APP.ViewModels.HomeModule
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public DelegateCommand<string> ConfirmPostcodeCommand { get; set; }
        public DelegateCommand CreditsButtonCommand { get; set; }
        public ISecureStore SecureStore { get; set; }
        public string FrameColor { get; set; }

        public SettingsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISecureStore secureStore)
            : base(navigationService, dialogService)
        {
            //Sets up the Secure Store API
            SecureStore = secureStore;
        }

        public override async void Load()
        {
            //Sets a delegate command which takes a string parameter
            ConfirmPostcodeCommand = new DelegateCommand<string>(ConfirmPostcode);
            CreditsButtonCommand = new DelegateCommand(CreditsButton);
            FrameColor = !await SecureStore.CheckPostcode() ? "Red" : "Green";
        }

        public async void ConfirmPostcode(string Postcode)
        {
            //Saves the postcode into secure store. Makes sure its uppercase
            await SecureStore.SavePostcode(Postcode.ToUpper());
            //Checks if the Postcode is in the securestore. Then changes frame colour to red or green
            FrameColor = !await SecureStore.CheckPostcode() ? "Red" : "Green";
            //Retrieves the set postcode from the database
            var ReturnedPostcode = await SecureStore.RetrievePostcode();
            //Sets a dialog box telling the user if their postcode has been set
            var text = ReturnedPostcode == Postcode ? "Postcode Confirmed" : "Something went wrong confirming your postcode!";
            await DialogService.DisplayAlertAsync(text, "", "Ok");
        }

        public async void CreditsButton()
        {
            try
            {
                await NavUtils.NavigateTo(Pages.Names.CreditsPageName, NavigationService, new NavigationParameters());
            }
            catch(Exception e)
            {
                Crashes.TrackError(e);
            }
        }
    }
}
