using System;
using PropertyChanged;
using Prism.Navigation;
using static CPMA_Core_APP.Common.StaticVariables;
using Microsoft.AppCenter.Crashes;
using CPMA_Core_APP.Utils;
using CPMA_Core_APP.Utils.Interfaces;
using Prism.Services;

namespace CPMA_Core_APP.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoadingPageViewModel : ViewModelBase
    {
        private ISecureStore SecureStore { get; set; }

        public LoadingPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
        }

        public override async void Load()
        {
            while (true)
            {
                try
                {
                    NavigateToHome();
                    break;
                    
                }
                catch(Exception e)
                {
                    await DialogService.DisplayAlertAsync("Error", e.Message, "Ok");
                    break;
                }
            }
        }

        private async void NavigateToHome()
        {
            // this currently navigates to the home tabbed page, update the "Page Name" variable to change the direction.
            var pageName = Pages.Names.NavigationPageName + "/" + Pages.Names.TabbedHomePageName;
            await NavUtils.NavigateTo(pageName, NavigationService, new NavigationParameters());
        }
    }
}