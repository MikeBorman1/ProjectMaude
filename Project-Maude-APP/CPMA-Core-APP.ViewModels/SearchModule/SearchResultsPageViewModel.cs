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
using PropertyChanged;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CPMA_Core_APP.ViewModels
{
    public class SearchResultsPageViewModel : ViewModelBase
    {
        //Buttons
        public DelegateCommand<SearchResultVM> ExpandSearchResultButtonCommand { get; set; }
        public DelegateCommand<string> SearchBarCommand { get; set; }
        public DelegateCommand ReportItemCommand { get; set; } 
        public string InitialText { get; set; }
        //Page Data
        public ObservableCollection<SearchResultVM> SearchResults { get; set; }
        public bool IsLoading {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        public bool LoadComplete { get; set; } = false;
        public bool IsEnabled { get; set; } = true;

        //API Reference
        private IGetSearchResults SearchAPI { get; set; }
        private IGetInfoBarcode BarcodeAPI { get; set; }

        private bool isLoading;
       

        public SearchResultsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IGetSearchResults searchAPI, IGetInfoBarcode barcodeAPI)
        : base(navigationService, dialogService)
        {
            SearchAPI = searchAPI;
            BarcodeAPI = barcodeAPI;
        }


        public override async void Load(INavigationParameters parameters)
        {
            
            try
            {
                if (parameters.ContainsKey(ParameterKeys.keywordSearch))
                {
                    InitialText = parameters.GetValue<string>("keywordSearch");
                }
                else
                {
                    //Throw warning
                    InitialText = "";
                }

                Title = Titles.SearchResultsPageTitle;
                ExpandSearchResultButtonCommand = new DelegateCommand<SearchResultVM>(ExpandButton);
                ReportItemCommand = new DelegateCommand(ReportItem);

                //Get list of SearchResults from API
                await Task.Run(() => SearchBar(InitialText));

                SearchBarCommand = new DelegateCommand<string>(SearchBar);
                
            }
            finally
            {
                LoadComplete = true;
                
            }
            
        }


        public async void ExpandButton(SearchResultVM sR)
        {
            if (!IsEnabled)
            {
                return;
            }

            IsEnabled = false;
            try
            {
                var par = new NavigationParameters
                {
                    { ParameterKeys.searchResult, sR.SearchResult}
                };
                await NavUtils.NavigateTo(Pages.Names.InfoPageName, NavigationService, par);
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


        public async void ReportItem()
        {
            if (!IsEnabled)
            {
                return;
            }

            IsEnabled = false;
            try
            {
                await NavUtils.NavigateTo(Pages.Names.ReportsFormPageName, NavigationService, new NavigationParameters());
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

        [AddINotifyPropertyChangedInterface]
        public class SearchResultVM
        {
            public SearchResult SearchResult { get; set; }
            public SearchResultsPageViewModel Parent { get; set; }
            public ObservableCollection<string> KeyPoints { get; set; }
            public string Image { get; set; } = null;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            public SearchResultVM(SearchResult s, SearchResultsPageViewModel p)
            {
                SearchResult = s;
                var kp = SearchResult.Materials.Select(m => KeyPointsUtils.BuildKeyPoints(m));
                var displayKp =  kp.Count() > 3 ? kp.Take(3) : kp.ToList();
                KeyPoints = new ObservableCollection<string>(displayKp);
                if (SearchResult.ProductPhotoID != null)
                {
                    Image = "https://inthebag.blob.core.windows.net/app-images/products/" + SearchResult.ProductPhotoID;
                }
                SearchResult.ProductName = textInfo.ToTitleCase(SearchResult.ProductName);
                SearchResult.ProductName = Regex.Replace(SearchResult.ProductName, @"[\w* \'S]\b", m => m.Value.ToLower());
                Parent = p;

            }
        }


        public async void SearchBar(string sR)
        {
            if (IsLoading) //don't double search
                return;
           
            IsLoading = true;
            string trimmed = sR.Trim();
            
            try
            {
                SearchResults = new ObservableCollection<SearchResultVM>(); // empty search results 

                //start search calls (barcode and keyword)
                var tasks = new List<Task<SearchResult[]>>
                {
                    SearchAPI.Search(trimmed),
                    BarcodeAPI.getProductInfo(trimmed)
                };

                // ***Add a loop to process the tasks one at a time until none remain.
                while (tasks.Count > 0)
                {
                    // Identify the first task that completes.
                    Task<SearchResult[]> firstFinishedTask = await Task.WhenAny(tasks);

                    // ***Remove the selected task from the list so that you don't
                    // process it more than once.
                    tasks.Remove(firstFinishedTask);

                    // Await the completed task.
                    var awaitedTask = await firstFinishedTask;

                    var finishedTaskResults = new ObservableCollection<SearchResultVM>(awaitedTask.Select(r => new SearchResultVM(r, this)));

                    var distinct = finishedTaskResults.Except(SearchResults);
                    SearchResults = new ObservableCollection<SearchResultVM>(SearchResults.Concat(distinct));
                    SearchResults = new ObservableCollection<SearchResultVM>(SearchResults.OrderBy(s=> !Regex.IsMatch(s.SearchResult.ProductName, @"\b"+trimmed+@"\b", RegexOptions.IgnoreCase)).ThenBy(s => !s.SearchResult.ProductName.StartsWith(trimmed, StringComparison.OrdinalIgnoreCase)));

                }
            }
            catch(Exception e)
            {
                Crashes.TrackError(e);
            }
            finally
            {
                IsLoading = false;
               
            }
        }
    }
}
