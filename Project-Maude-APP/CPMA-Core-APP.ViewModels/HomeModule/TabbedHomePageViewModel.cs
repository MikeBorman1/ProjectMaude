using System;
using System.Threading.Tasks;
using CPMA_Core_APP.API;
using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Utils;
using CPMA_Core_APP.Utils.Interfaces;
using CPMA_Core_APP.ViewModels.HomeModule;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using static CPMA_Core_APP.Common.StaticVariables;

namespace CPMA_Core_APP.ViewModels
{
    public class TabbedHomePageViewModel : ViewModelBase
    {   private IGetRecycleInfo Recycleinfo { get; set; }
        private ISecureStore SecureStore { get; set; }
        private IGetReports ReportsAPI { get; set; }
        private ISetUpvote UpvoteAPI { get; set; }
        private IUpvoteStore UpvoteStore { get; set; }
        private IGetLocations LocationsAPI { get; set; }
        public MainPageViewModel MainPageViewModel { get; set; }
        public ReportsPageViewModel ReportsPageViewModel { get; set; }
        public CollectionInfoPageViewModel CollectionInfoPageViewModel { get; set; }
        public RecycleInfoPageViewModel RecycleInfoPageViewModel { get; set; }
        public TabbedHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, ISecureStore secureStore, IGetReports reportsAPI, ISetUpvote upvoteAPI, IUpvoteStore upvoteStore, IGetLocations locationsAPI,IGetRecycleInfo recycleInfo)
            : base(navigationService, dialogService)
        {
            SecureStore = secureStore;
            ReportsAPI = reportsAPI;
            UpvoteAPI = upvoteAPI;
            UpvoteStore = upvoteStore;
            LocationsAPI = locationsAPI;
            Recycleinfo = recycleInfo;
            UpvoteStore = upvoteStore;
        }


        //For Tabbed pages, we need to load the viewmodels when the parent loads this is the purpose of the #FakeLoad method
        public override void Load()
        {
            MainPageViewModel = new MainPageViewModel(NavigationService, DialogService, SecureStore);
            //MainPageViewModel.FakeLoad();

            RecycleInfoPageViewModel = new RecycleInfoPageViewModel(NavigationService, DialogService, Recycleinfo);
            RecycleInfoPageViewModel.FakeLoad();

            ReportsPageViewModel = new ReportsPageViewModel(NavigationService, DialogService, ReportsAPI, UpvoteAPI, UpvoteStore);
            ReportsPageViewModel.FakeLoad();

            CollectionInfoPageViewModel = new CollectionInfoPageViewModel(NavigationService, DialogService, LocationsAPI, SecureStore);
            CollectionInfoPageViewModel.FakeLoad();
        }
    }
}
