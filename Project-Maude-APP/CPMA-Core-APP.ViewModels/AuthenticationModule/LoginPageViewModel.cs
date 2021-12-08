using CPMA_Core_APP.API;
using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.API.Implementations;
using Prism.Commands;
using Prism.Navigation;
using System;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;
using CPMA_Core_APP.Utils;
using System.Threading.Tasks;
using CPMA_Core_APP.Utils.Interfaces;

namespace CPMA_Core_APP.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        //Local Services
        ISecureStore SecureStore { get; set; }
        ILogin LoginService { get; set; }

        //Button commands
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand RegisterCommand { get; set; }
        //form inputs
        public bool Enabled { get; set; } = true;
        public string ErrorMessage { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        public LoginPageViewModel(INavigationService navigationService, ILogin loginService, ISecureStore secureStore)
            : base(navigationService)
        {
            SecureStore = secureStore;
            LoginService = loginService;
        }

        public override void Load()
        {
            
        }

       
    }
}
