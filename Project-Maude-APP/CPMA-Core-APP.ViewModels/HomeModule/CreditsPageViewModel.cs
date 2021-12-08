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

namespace CPMA_Core_APP.ViewModels.HomeModule
{
    public class CreditsPageViewModel : ViewModelBase
    {

        public ObservableCollection<CreditsVM> Credits { get; set; }
        public CreditsPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        : base(navigationService, dialogService)
        {

        }
        
        public async override void Load() {
            Title = "Credits";
            Credits = new ObservableCollection<CreditsVM>
            {
                new CreditsVM("Katie Marquand", "Team Leader"),
                new CreditsVM("Jess Salisbury", "Organizer"),
                new CreditsVM("Mike Borman", "API Guy"),
                new CreditsVM("Will Tiffin", "Intern"),
                new CreditsVM("Charles Christian", "Actually made it")
            };

            ShuffleObservable(Credits);
            Credits.Insert(0, new CreditsVM("Rufus Barnes", "Was here"));
            Random R = new Random();
            int r = R.Next(0, 3);
            if (r != 2)
            {
                ShuffleObservable(Credits);
            }
        }



        [AddINotifyPropertyChangedInterface]
        public class CreditsVM
        {
            public string Position { get; set; }
            public string Name { get; set; }
            
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            public CreditsVM(string name, string position)
            {
                Position = textInfo.ToTitleCase(position);
                Name = textInfo.ToTitleCase(name);
            }
        }

        public void ShuffleObservable<T>(ObservableCollection<T> target)
        {
            ObservableCollection<T> intermediate = new ObservableCollection<T>();
            foreach (T t in target)
            {
                intermediate.Add(t);
            }
            target.Clear();

            Random R = new Random();
            while (intermediate.Count > 0)
            {
                int index = R.Next(0, intermediate.Count() - 1);
                target.Add(intermediate[index]);
                intermediate.RemoveAt(index);
            }
        }
    }
}
