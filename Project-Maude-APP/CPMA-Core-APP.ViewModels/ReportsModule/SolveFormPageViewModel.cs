using CPMA_Core_APP.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using CPMA_Core_APP.API;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;
using PropertyChanged;
using Microsoft.AppCenter.Crashes;
using CPMA_Core_APP.Model;
using System.Globalization;
using System.Collections.ObjectModel;
using Plugin.Permissions.Abstractions;
using static CPMA_Core_APP.ViewModels.MaterialsPageViewModel;
using Plugin.Permissions;

namespace CPMA_Core_APP.ViewModels.HomeModule
{
    [AddINotifyPropertyChangedInterface]
    public class SolveFormPageViewModel : ViewModelBase
    {
        //Buttons
        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand CameraButtonCommand { get; set; }
        public DelegateCommand MaterialsButtonCommand { get; set; }

        private ISetProductMaterialLink LinkAPI { get; set; }
        public IStorage BlobStore { get; set; }

        public bool IsEnabled { get; set; } = true; 
        public bool HasSelected { get; set; } = false;
        public bool localImage { get; set; } = false;
        
        public bool isLoaded = false;
        public string Name { get; set; }
        public string Image { get; set; } = "resource://CPMA_Core_APP.Resources.Icons.Camera.png";

        public SolveVM Model { get; set; }

        public SolveFormPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISetProductMaterialLink linkAPI, IStorage store)
        : base(navigationService, dialogService)
        {
            LinkAPI = linkAPI;
            BlobStore = store;
        }

        public override async void Load(INavigationParameters parameters)
        {
            try
            {
                if (parameters.ContainsKey(ParameterKeys.report))
                {
                    LoadReport(parameters);
                }
            }
            catch(Exception e)
            {
                await NavigationService.GoBackAsync();
            }
            isLoaded = true;
        }

        public void LoadReport(INavigationParameters parameters)
        {
            CameraButtonCommand = new DelegateCommand(CameraButton);
            MaterialsButtonCommand = new DelegateCommand(MaterialsButton);
            SubmitCommand = new DelegateCommand(Submit);

            Model = new SolveVM(parameters.GetValue<Report>(ParameterKeys.report));
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            Title = Titles.SolveFormPageTitle;
            Name = textInfo.ToUpper(Model.Report.ReportTitle);
            var guid = textInfo.ToLower(Model.Report.ReportImageUrl);
            Image = "https://inthebag.blob.core.windows.net/app-images/reports/" + guid;
        }

        [AddINotifyPropertyChangedInterface]
        public class SolveVM
        {
            public Report Report { get; set; }
            public ObservableCollection<MaterialVM> Materials { get; set; }

            public SolveVM(Report r)
            {
                Report = r;
                Materials = new ObservableCollection<MaterialVM>();
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
                Image = await CameraUtils.TakePicture(DialogService);
                localImage = true;
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

        public async void MaterialsButton()
        {
            var par = new NavigationParameters
            {
                //Why ParameterKeys.report?
                { ParameterKeys.solveVM, Model}
            };
            await NavUtils.NavigateTo(Pages.Names.MaterialsPageName, NavigationService, par);
            HasSelected = true;
        }

        public async void Submit()
        {
            try
            {
                var r = Model.Report;
                var materials = "";
                try
                {
                    materials = String.Join(",", Model.Materials.Select(m => m.Name));
                }
                catch(Exception e)
                {
                    Crashes.TrackError(e);
                }
                if (Model.Materials.Count() > 0)
                {
                    //Create blob stored image
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    var guid = textInfo.ToLower(r.ReportImageUrl);
                    if (localImage == true)
                    {
                        await BlobStore.UploadPhoto("products/" + guid, Image);
                    }
                    else
                    {
                        await BlobStore.CopyPhoto("reports/"+Model.Report.ReportImageUrl, "products/"+guid);
                    }
                    await LinkAPI.SetLink(r.ReportTitle, guid, r.Barcode, materials);
                    await NavigationService.GoBackAsync();
                    
                }
                else
                {
                    await DialogService.DisplayAlertAsync("Failure", "You must select at least 1 material", "Ok");
                }
            }
            catch(Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        public bool MaterialsEmpty()
        {
            if (HasSelected && Model.Materials.Count() == 0)
            {
                return true;
            }
            return false;
        }
    }
}
