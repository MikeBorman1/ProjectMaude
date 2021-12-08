using CPMA_Core_APP.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static CPMA_Core_APP.Common.StaticVariables;

namespace CPMA_Core_APP.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        //part of login stuff
        //public string Session { get; set; }
        //public bool CheckingSession { get; set; }
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService DialogService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        /// <summary>
        /// This takes the Navigation parameters passed by prism if any, and then automatically loads the viewmodel for the page specified.
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.Count > 0)
            {
                Load(parameters);
            }
            else
            {
                Load();
            }
        }

        public virtual void Destroy()
        {

        }

        /*
         * Overridden in LoadingPageViewModel I think this could be used for more between screen interaction. 
         * perhaps call the loading page with a target destination? 
         * especially if there is lots of data to load in the interim 
         */
        public virtual void Load()
        {
            return;
        }

        /*
         * Overridden in LoadingPageViewModel same as above, but with the options in parameters.
         */
        public virtual void Load(INavigationParameters parameters)
        {
            return;
        }

        /// <summary>
        /// Used for Tabbed page loading
        /// </summary>
        public virtual void FakeLoad()
        {
            Load();
        }
    }
}
