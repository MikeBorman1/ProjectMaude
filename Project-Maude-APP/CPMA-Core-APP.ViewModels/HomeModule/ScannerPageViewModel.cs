using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Mobile;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using CPMA_Core_APP.Utils;
using static CPMA_Core_APP.Common.StaticVariables;
using CPMA_Core_APP.API.Interfaces;
using Xamarin.Forms;
using CPMA_Core_APP.Model;
using Microsoft.AppCenter.Crashes;
using CPMA_Core_APP.ViewModels.HomeModule;

namespace CPMA_Core_APP.ViewModels
{
    public class ScannerPageViewModel : ViewModelBase
    {
        //Interfaces
        private IGetInfoBarcode BarcodeAPI { get; set; }

        //Button Commands
        public DelegateCommand OnBarcodeScannedCommand { get; set; }
        
        //Page Properties
        public bool IsProcessing { get; set; } = false;
        public bool IsAnalyzing { get; set; }
        public bool IsScanning { get; set; }
        public ZXing.Result Result { get; set; }
        public int ResponseType { get; set; }
   
        public ReportsFormPageViewModel Report { get; set; }
         public ScannerPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IGetInfoBarcode barcodeAPI) 
            : base(navigationService, dialogService)
        {
            //Sets the barcode APIx
            BarcodeAPI = barcodeAPI;
            

        }
        public override void Load(INavigationParameters parameters)
        {

            OnBarcodeScannedCommand = new DelegateCommand(OnBarcodeScanned);
            ResponseType = parameters.GetValue<int>(ParameterKeys.responseType);
            if (parameters.ContainsKey(ParameterKeys.reportForm))
            {
                Report = parameters.GetValue<ReportsFormPageViewModel>(ParameterKeys.reportForm);
            }
            Title = "Scan";
            IsScanning = true;


        }

        public void OnBarcodeScanned()
        {
            try
            {
                //Checks if it is Processing
                if (!IsProcessing)
                {
                    IsProcessing = true;
                    IsScanning = false;
                    IsAnalyzing = false;
                    if (Result != null)
                    {
                        //If the result is found in the db it tries to take the user to that items info page
                        string message = String.Format("Barcode Scanned: {0}!", Result.Text);
                        Debug.WriteLine(message);

                        Device.BeginInvokeOnMainThread(() => LoadInfoPage(Result.Text));
                    }
                    else
                    {
                        IsScanning = true;
                        IsAnalyzing = true;
                    }
                }
            }
            finally
            {
                IsProcessing = false;
            }
        }

        public async void LoadInfoPage(string barcode)
        {
            try
            {
                //Checks if the Barcode is in the DB
                var result = await LoadBarcodeInfo(barcode);

                /*
                 * case 1 should be report is not null and result is null
                 * case 2 should be no report (implicit) and result is not null; if there is a report show info anyway. don't report.
                 * case 3 is remainder, this should only be when result is not null
                 */
                if (ResponseType == 1 && Report != null) {
                    Report.Barcode = barcode;
                    Report.BarcodeLabel = barcode;
                    await NavigationService.GoBackAsync();

                }
                else if (ResponseType == 0 && result != null )
                {
                    var par = new NavigationParameters
                    {
                        { ParameterKeys.searchResult, result }
                    };

                    //If there is a barcode it takes it to that barcodes info page.
                    var pg = "../" + Pages.Names.InfoPageName;
                    await NavUtils.NavigateTo(pg, NavigationService, par);
                }
                else
                {
                    bool answer = await DialogService.DisplayAlertAsync("No Barcode found", "Would you like to search for your product?", "Yes", "No");

                    if (answer)
                    {
                        var par = new NavigationParameters {};

                     
                        await NavUtils.NavigateTo(Pages.Names.SearchResultsPageName, NavigationService, par);

                    }
                    else
                    {
                        await NavigationService.GoBackAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
                await DialogService.DisplayAlertAsync("Something went wrong", "Something went wrong when searching for barcode: "+ barcode, "OK");
                await NavigationService.GoBackAsync();
            }
        }

        public async Task<SearchResult> LoadBarcodeInfo(string Barcode)
        {
            var result = await BarcodeAPI.getProductInfo(Barcode);
            return result.FirstOrDefault();
        }

    }
}