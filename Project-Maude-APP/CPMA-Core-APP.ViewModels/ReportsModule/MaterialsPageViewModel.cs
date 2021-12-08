using CPMA_Core_APP.API.Implementations;
using CPMA_Core_APP.API.Interfaces;
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
using System.Threading;
using System.Threading.Tasks;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;
using System.Globalization;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms.MultiSelectListView;
using static CPMA_Core_APP.ViewModels.HomeModule.SolveFormPageViewModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using PropertyChanged;

namespace CPMA_Core_APP.ViewModels
{
    public class MaterialsPageViewModel : ViewModelBase
    {
        //API
        IGetMaterials MaterialsAPI { get; set; }

        //Buttons
        public DelegateCommand<string> SearchBarCommand { get; set; }
        public DelegateCommand<MaterialVM> AddSelectedCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }

        //Checks
        public bool IsSearching { get; set; } = false;
        public bool LoadComplete { get; set; } = false;

        //Other
        public MultiSelectObservableCollection<MaterialVM> Materials { get; set; }
        public ObservableCollection<MaterialVM> UnfilteredMaterials { get; set; }
        public ObservableCollection<MaterialVM> SelectedMaterials { get; set; }
        public SolveVM Model { get; set; }

        public MaterialsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IGetMaterials materialsAPI)
        : base(navigationService, dialogService)
        {
            MaterialsAPI = materialsAPI;
        }


        public override async void Load(INavigationParameters parameters)
        {
            try
            {
                Model = parameters.GetValue<SolveVM>(ParameterKeys.solveVM);
                
                if (Model.Materials != null)
                {
                    SelectedMaterials = new ObservableCollection<MaterialVM>(Model.Materials);
                }
                else
                {
                    SelectedMaterials = new ObservableCollection<MaterialVM>();
                }

                var mats = await MaterialsAPI.getMaterials();
                UnfilteredMaterials = new ObservableCollection<MaterialVM>(mats.Select(ms => new MaterialVM(ms)).OrderBy(m => m.Name));
                var selectableMats = mats.Select(ms => new SelectableItem<MaterialVM>(new MaterialVM(ms), SelectedMaterials.Any(m => m.Material.MaterialID == ms.MaterialID))).OrderBy(m => m.Data.Name);
                Materials = new MultiSelectObservableCollection<MaterialVM>(selectableMats); //Grab from API

                AddSelectedCommand = new DelegateCommand<MaterialVM>(AddSelected);
                SubmitCommand = new DelegateCommand(Submit);
                SearchBarCommand = new DelegateCommand<string>(SearchBar);
            }
            catch(Exception e)
            {
                Crashes.TrackError(e);
            }
            finally
            {
                LoadComplete = true;
            }
        }


        [AddINotifyPropertyChangedInterface]
        public class MaterialVM
        {
            public MaterialSearch Material { get; set; }
            public string Name { get; set; }

            public MaterialVM(MaterialSearch material)
            {
                Material = material;
                var textInfo = new CultureInfo("en-US", false).TextInfo;
                Name = textInfo.ToTitleCase(Material.Name);
            }
        }


        public void AddSelected(MaterialVM Material)
        {
            try
            {
                //Cannot recognise when same material is added, maybe treats them as different
                var selectedIds = new ObservableCollection<int>(SelectedMaterials.Select(m => m.Material.MaterialID));
                if (!selectedIds.Contains(Material.Material.MaterialID))
                {
                    SelectedMaterials.Add(Material);
                }
                else
                {
                    SelectedMaterials.Remove(SelectedMaterials.Single(m => m.Material.MaterialID == Material.Material.MaterialID));
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        public void SearchBar(string sR)
        {
            try
            {
                var mats = new List<MaterialVM>(UnfilteredMaterials.Where(m => SearchUtils.SearchBy(m.Name, sR)));
                var selected = new List<SelectableItem<MaterialVM>>(mats.Select(m => new SelectableItem<MaterialVM>(m, SelectedMaterials.Any(sm => sm.Material.MaterialID == m.Material.MaterialID))));
                Materials = new MultiSelectObservableCollection<MaterialVM>(selected);
            }
            catch(Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        public async void Submit()
        {
            try
            {
                //Fill model variable
                if (SelectedMaterials.Count() != 0)
                {
                    Model.Materials = SelectedMaterials;
                }
                
                if (SelectedMaterials.Count() > 0)
                {
                    //Go back
                    await NavigationService.GoBackAsync();
                }
                else
                {
                    //Display warning
                    await DialogService.DisplayAlertAsync("Warning", "You must select at least one material", "Ok");
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }
    }
}
