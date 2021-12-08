using CPMA_Core_APP.API;
using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using CPMA_Core_APP.Utils;
using CPMA_Core_APP.Utils.Interfaces;
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.ThemeStrings;

namespace CPMA_Core_APP.ViewModels
{
    public class ReportsPageViewModel : ViewModelBase
    {
        public DelegateCommand MyReportsLinkCommand { get; set; }
        public DelegateCommand<ReportVM> VoteButtonCommand { get; set; }
        public DelegateCommand<Report> ExpandReportButtonCommand { get; set; }
        public ObservableCollection<ReportVM> ReportsQueue { get; set; }
        private ISetUpvote UpvoteAPI { get; set; }
        private IGetReports ReportsAPI { get; set; }
        public IUpvoteStore UpvoteStore { get; set; }

        public bool IsEnabled { get; set; } = true;
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


        public ReportsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IGetReports reportsAPI, ISetUpvote upvoteAPI, IUpvoteStore upvoteStore)
        : base(navigationService, dialogService)
        {
            UpvoteAPI = upvoteAPI;
            ReportsAPI = reportsAPI;
            UpvoteStore = upvoteStore;
        }
        private int ReportCounter { get; set; } = 0;
        public async override void Load()
        {
            VoteButtonCommand = new DelegateCommand<ReportVM>(VoteButton);
            ExpandReportButtonCommand = new DelegateCommand<Report>(ExpandButton);

            LoadReportsQueue();
        }

        public async void LoadReportsQueue( )
        {
            if (IsLoading)
                return;

            IsLoading = true;
            try
            {
                ReportCounter = 0;
                ReportsQueue = new ObservableCollection<ReportVM>();
                var result = await ReportsAPI.getReport();
                var results = new ObservableCollection<ReportVM>(result.Select(r => new ReportVM(r, this, Counter(),UpvoteStore)));
                ReportsQueue = new ObservableCollection<ReportVM>(ReportsQueue.Concat(results));
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

        [AddINotifyPropertyChangedInterface]
        public class ReportVM
        {
            public Report Report { get; set; }
            public IUpvoteStore UpvoteStore { get; set; }
            public ReportsPageViewModel Parent { get; set; }
            public int Counter { get; set; }
            public string Rank { get; set; }
            public string Image { get; set; }
            public bool Upvoted { get; set; } = false;
            public string VoteColor { get; set; } = "Green";
            //public ImageSource VoteImage { get; set; }
            public string VoteImage { get; set; } = Application.Current.Resources[UpvoteSource].ToString();

            public List<int> Upvotes = new List<int>();

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            public ReportVM(Report r, ReportsPageViewModel p, int c, IUpvoteStore US)
            {
                Report = r;
                Parent = p;
                Counter = c;
                Rank = "#" + c;
                UpvoteStore = US;
                Report.ReportTitle = textInfo.ToTitleCase(Report.ReportTitle);
                Report.ReportTitle = Regex.Replace(Report.ReportTitle, @"[\w* \'S]\b", m => m.Value.ToLower());
                var guid = textInfo.ToLower(Report.ReportImageUrl);
                Image = "https://inthebag.blob.core.windows.net/app-images/reports/" + guid;

                GetUpvotes();

                //Checks if upvotes contains this Classes Report Id. 
                if (Upvotes.Contains(r.ReportId))
                {
                    //It changed upvoted to true and vote colour to green
                    Upvoted = true;
                    VoteColor = "Red";
                    VoteImage = Application.Current.Resources[DownvoteSource].ToString();
                }
            }

            public async void GetUpvotes()
            {
                //The list of upvotes is retreieved from the local files.
                Upvotes = await UpvoteStore.GetUpvoteData();

            }
        }

        public async void VoteButton(ReportVM r)
        {

            //Checks if the file is upvoted or not when the button is clicked
            if (r.Upvoted)
            {
                //It it is upvoted it changes vote colour to red and upvoted to false
                r.VoteColor = "Green";
                r.VoteImage = Application.Current.Resources[UpvoteSource].ToString();
                r.Upvoted = false;
                //Downvotes the RpId in the local storage
                await UpvoteStore.Downvote(r.Report.ReportId);
                //Removes one from the number of upvotes the report has in the Database

                await UpvoteAPI.Downvote(r.Report.ReportId);                
            }
            else
            {

                //If it isnt upvoted when clicked changes vote colr to Green and Upvoted to True
                r.VoteColor = "Red";
                r.VoteImage = Application.Current.Resources[DownvoteSource].ToString();
                r.Upvoted = true;
                //Upvotes the RpId in the local storage
                await UpvoteStore.Upvote(r.Report.ReportId);
                //Adds one to the number of upvotes the report has in the Database

                await UpvoteAPI.Upvote(r.Report.ReportId);
            }
        }

        public async void ExpandButton(Report rp)
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
                    { ParameterKeys.report, rp }
                };
                await NavUtils.NavigateTo(Pages.Names.SolveFormPageName, NavigationService, par);
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

        public int Counter()
        {
            ReportCounter++;
            return ReportCounter;
        }
    }
}
