using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using static CPMA_Core_APP.Common.StaticVariables;

namespace CPMA_Core_APP.Utils
{
    public static class NavUtils
    {
        /// <summary>
        ///  this is a quick utility to navigate to a page from your current page
        /// </summary>
        /// <param name="pageName">string target page name</param>
        /// <param name="navigationService">Navigation service, this will be inherited from the viewmodel base, in the constructor.</param>
        /// <param name="navigationParameters">any other navigation parameters? if none just pass in new NavigationParameters()</param>
        public static async Task NavigateTo(string pageName, INavigationService navigationService, NavigationParameters navigationParameters)
        {    
            await navigationService.NavigateAsync(pageName, navigationParameters);
        }

        public static async Task NavigateToAbsolute(string pageName, INavigationService navigationService, NavigationParameters navigationParameters)
        {
            var uri = new Uri("myapp:///" + pageName + "/");
            await navigationService.NavigateAsync(uri, navigationParameters);
        }
    }
}
