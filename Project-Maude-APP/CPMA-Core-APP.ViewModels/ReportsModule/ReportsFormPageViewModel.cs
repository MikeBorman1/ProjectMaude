using CPMA_Core_APP.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;
using System.Globalization;
using CPMA_Core_APP.API;
using PropertyChanged;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Xamarin.Forms;
using static CPMA_Core_APP.Common.StaticVariables.ThemeStrings;

namespace CPMA_Core_APP.ViewModels.HomeModule
{
    [AddINotifyPropertyChangedInterface]
    public class ReportsFormPageViewModel : ViewModelBase
    {
        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand CameraButtonCommand { get; set; }
        public DelegateCommand ScanBarcodeButtonCommand { get; set; }
        public string DefaultImage { get; set; } = Application.Current.Resources[WhiteCameraSource].ToString();
        public string ReportImage { get; set; } = Application.Current.Resources[WhiteCameraSource].ToString();

        private ISetReport SetReportAPI { get; set; }
        public IStorage BlobStore { get; set; }

        //Variables that must be set to send off API set request
        public string BarcodeLabel { get; set; } = "Scan Barcode";
        public string Barcode { get; set; }
        public string ReportTitle { get; set; }
        public string ReportImageID { get; set; }
        public bool IsEnabled { get; set; } = true;

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        public ReportsFormPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISetReport setReport, IStorage blobstore)
            : base(navigationService, dialogService)
        {
            SetReportAPI = setReport;
            BlobStore = blobstore;
        }
        public override void Load()
        {
            Title = Titles.ReportsFormPageTitle;
            SubmitCommand = new DelegateCommand(Submit);
            CameraButtonCommand = new DelegateCommand(CameraButton);
            ScanBarcodeButtonCommand = new DelegateCommand(BarcodeCommand);
        }

        public async void Submit()
        {
            if (!IsEnabled || IsLoading)
            {
                return;
            }

            IsEnabled = false;
            try
            {
                if (ReportImage != DefaultImage && !String.IsNullOrEmpty(ReportTitle))
                {
                    bool submit = await DialogService.DisplayAlertAsync("Submit Report?", "", "Ok", "Cancel");
                    if (submit)
                    {
                        await ConfirmSubmit();
                    }
                }
                else
                {
                    await DialogService.DisplayAlertAsync("Unfinished Report", "Please fill in all required data", "Ok");
                }
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
        public async Task ConfirmSubmit()
        {
            if (IsLoading) //don't double search
                return;

            IsLoading = true;
            try
            {
                ReportImageID = Guid.NewGuid().ToString();
                await BlobStore.UploadPhoto("reports/" + ReportImageID, ReportImage);
                await SetReportAPI.SetReport(ReportImageID, ReportTitle, Barcode);
                await NavigationService.GoBackToRootAsync(new NavigationParameters());
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


        public async void CameraButton()
        {
            //Request Permissions
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
                ReportImage = await CameraUtils.TakePicture(DialogService);
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

        public async void BarcodeCommand()
        {
            //Request Permissions
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

            await BarcodeScanner();
        }

        public async Task BarcodeScanner()
        {
            IsEnabled = false;
            try
            {
                //Required for permissions
                await Task.Delay(5);
                var par = new NavigationParameters
                {
                    { ParameterKeys.responseType, 1 },
                    { ParameterKeys.reportForm, this }
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
    }
}
