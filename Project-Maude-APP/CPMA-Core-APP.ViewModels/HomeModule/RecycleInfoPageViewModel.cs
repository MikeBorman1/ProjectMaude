using CPMA_Core_APP.API.Interfaces;
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace CPMA_Core_APP.ViewModels
{
    public class RecycleInfoPageViewModel : ViewModelBase
    {
        public bool isLoaded;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private bool isLoading;
        public ObservableCollection<RecycleInfoVM> Model {get;set;}
        public IGetRecycleInfo GetRecycleInfo { get; set; }
        public RecycleInfoPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IGetRecycleInfo getRecycleInfo)
            : base(navigationService, dialogService)
        {
            //Sets the get recycle info api
            GetRecycleInfo = getRecycleInfo;
        }

        public async override void Load()
        {
            try
            {
                if (IsLoading) //don't double search
                    return;

                //Sets the result of getting recycle info
                var result = await GetRecycleInfo.getInfo();

                IsLoading = true;
                if (result != null)
                {
                    //Creates a new observable collection of RecycleInfoVMs using the Recycle rows in the databse
                    Model = new ObservableCollection<RecycleInfoVM>(result.Select(r => new RecycleInfoVM(r.Name, r.Accepted, r.Rejected)));
                    isLoaded = true;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        [AddINotifyPropertyChangedInterface]
        public class RecycleInfoVM
        {
            //Creates a string of BagText
            public string BagText { get; set; }
            //Creates accepted items and rejected item observable collections
            public ObservableCollection<string> AcceptedItems { get; set; }
            public ObservableCollection<string> RejectedItems { get; set; }
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            //Class constructor
            public RecycleInfoVM(string BagName, string a, string r)
            {
               
                //Sets up the title text for what bag name is in this VM
                BagText = textInfo.ToUpper(BagName);
                //Sets up the Accepted items string
                AcceptedItems = new ObservableCollection<string>(a.Split(';'));
                //Sets up rejected items string
                RejectedItems = new ObservableCollection<string>(r.Split(';'));
            }
        }
    }
}
