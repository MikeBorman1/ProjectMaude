using Prism;
using Prism.Ioc;
using CPMA_Core_APP.ViewModels;
using CPMA_Core_APP.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static CPMA_Core_APP.Common.StaticVariables;
using CPMA_Core_APP.Themes;
using CPMA_Core_APP.Controls;
using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.API.Implementations;
using CPMA_Core_APP.Utils.Interfaces;
using CPMA_Core_APP.Utils;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
using CPMA_Core_APP.ViewModels.HomeModule;
using CPMA_Core_APP.API;
using CPMA_Core_APP.Views.Home;
using CPMA_Core_APP.API.TestInterfaces;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CPMA_Core_APP
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //This pushes the app load to the page specified in Pages.Names.AppStartPage currently the loading page.
            await NavigationService.NavigateAsync(Pages.Names.AppStartPage);
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
                Resources.Add(new Style(typeof(Frame))
                {
                    Setters =
                    {
                        new Setter { Property = Frame.HasShadowProperty, Value = false },
                        new Setter { Property = Frame.CornerRadiusProperty, Value = 0 }
                    }
                });

            //App Center Magic!
            AppCenter.Start("android=b1843926-edce-4eaf-b277-c5e007ee3f2e;" +
                  "ios=4d62140a-9a38-437b-b34b-5301edcc96aa;",
                  typeof(Analytics), typeof(Crashes));


            //Theme manager
            ThemeManager.LoadTheme();
            //MessagingCenter.Subscribe<SettingsPageViewModel, int>(this, ThemeStrings.UpdateTheme, (sender, arg) => {
            //    ThemeManager.ChangeThemeAsync((ThemeManager.Themes)arg);
            //});
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            /*
             * App Registration - this is going to be all of the API methods and Navigation page targets.
             * a page target is a combination of a view (see the views folder) and a view model, see the viewmodels project.
             * the pages.names values are the key to identify the combination used when we are navigating using the NavUtils method in the utils project.
             */
            containerRegistry.Register<IGetMaterials, APIGetMaterials>();
            containerRegistry.Register<IGetRecycleInfo, APIGetRecycleInfo>();
            containerRegistry.Register<IGetSearchResults, APIGetSearchResults>();
            containerRegistry.Register<ISetProductMaterialLink, APISetProductMaterialLink>();
            containerRegistry.Register<IUpvoteStore, UpvoteStore>();
            containerRegistry.Register<ISetUpvote, APISetUpvote>();
            containerRegistry.Register<IGetReports, APIGetReports>();
            containerRegistry.Register<IGetInfoBarcode, APIGetInfoBarcode>();
            containerRegistry.Register<ISetReport, APISetReport>();
            containerRegistry.Register<ISetFlag, APISetFlag>();
            containerRegistry.Register<ISecureStore, SecureStore>();
            containerRegistry.Register<IStorage, APIStorage>();
            containerRegistry.Register<IGetLocations, APIGetLocations>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ScannerPage, ScannerPageViewModel>(Pages.Names.ScannerPage);
            containerRegistry.RegisterForNavigation<LoadingPage, LoadingPageViewModel>(Pages.Names.LoadingPageName);
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>(Pages.Names.LoginPageName); 
            containerRegistry.RegisterForNavigation<TabbedHomePage, TabbedHomePageViewModel>(Pages.Names.TabbedHomePageName);
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(Pages.Names.MainPageName);
            containerRegistry.RegisterForNavigation<ReportsPage, ReportsPageViewModel>(Pages.Names.ReportsPageName);
            containerRegistry.RegisterForNavigation<InfoPage, InfoPageViewModel>(Pages.Names.InfoPageName);
            containerRegistry.RegisterForNavigation<SearchResultsPage, SearchResultsPageViewModel>(Pages.Names.SearchResultsPageName);
            containerRegistry.RegisterForNavigation<ReportsFormPage, ReportsFormPageViewModel>(Pages.Names.ReportsFormPageName);
            containerRegistry.RegisterForNavigation<SolveFormPage, SolveFormPageViewModel>(Pages.Names.SolveFormPageName);
            containerRegistry.RegisterForNavigation<MaterialsPage, MaterialsPageViewModel>(Pages.Names.MaterialsPageName);
            containerRegistry.RegisterForNavigation<CollectionInfoPage, CollectionInfoPageViewModel>(Pages.Names.CollectionInfoPageName);
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>(Pages.Names.SettingsPageName);
            containerRegistry.RegisterForNavigation<RecycleInfoPage, RecycleInfoPageViewModel>(Pages.Names.RecycleInfoPageName);
            containerRegistry.RegisterForNavigation<CreditsPage, CreditsPageViewModel>(Pages.Names.CreditsPageName);
        }
    }
}
