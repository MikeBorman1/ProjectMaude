using System;
using System.Collections.Generic;
using System.Text;

namespace CPMA_Core_APP.Common
{

    public static partial class StaticVariables
    {
        public static class Pages
        {
            /// <summary>
            /// List of navigation page names, these are used in the NavUtils method, and also the Navigation Registration in App.xaml.cs
            /// </summary>
            public static class Names
            {
                public const string NavigationPageName = "NavigationPage";
                public const string LoadingPageName = "LoadingPage";
                public const string AppStartPage = NavigationPageName + "/" + LoadingPageName;
                public const string LoginPageName = "LoginPage";

                public const string TabbedHomePageName = "TabbedHomePage";
                public const string MainPageName = "MainPage";
                public const string CollectionInfoPageName = "CollectionInfoPage";
                public const string SettingsPageName = "SettingsPage";
                public const string RecycleInfoPageName = "RecycleInfo";
                public const string CreditsPageName = "CreditsPage";
                public const string ScannerPage = "ScannerPage";

                public const string SearchResultsPageName = "SearchResultsPage";
                public const string InfoPageName = "InfoPage";

                public const string ReportsPageName = "ReportsPage";
                public const string ReportsFormPageName = "ReportsFormPage";
                public const string SolveFormPageName = "SolveFormPage";
                public const string MaterialsPageName = "MaterialsPage";
                
            }

            //string titles for pages if they need them. this is used in the page view models.
            public static class Titles
            {
                public const string MainPageTitle = "Home";
                public const string LoginPageTitle = "Login";
                public const string InfoPageTitle = "Info";
                public const string SearchResultsPageTitle = "Search Results";
                public const string SettingsPageTitle = "Settings";
                public const string SolveFormPageTitle = "Solve Form";
                public const string ReportsFormPageTitle = "Reports Form";
            }
        }
    }
}
