using CPMA_Core_APP.API.Implementations;
using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using CPMA_Core_APP.Utils;
using CPMA_Core_APP.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;
using System.Globalization;
using CPMA_Core_APP.API.TestInterfaces;
using CPMA_Core_APP.API;
using CPMA_Core_APP.ViewModels.HomeModule;
using CPMA_Core_APP.Utils.Interfaces;

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class SettingsPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private ISecureStore getSecureStoreService;

        public SettingsPageViewModel ViewModel { get; set; }
        public string Postcode = "GUERNSEY";
        public string Postcode1 = "GY0000";

        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            getSecureStoreService = new MockSecureStore();
            ViewModel = new SettingsPageViewModel(_navigationService.Object, _dialogService.Object, getSecureStoreService);
        }


        [TestCleanup]

        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            ViewModel = null;
            getSecureStoreService = null;
        }

        [TestMethod]
        public async Task TestSubmitPostcodeOk()
        {
            _dialogService.Setup(e => e.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            ViewModel.Load();
            await Task.Delay(5);

            ViewModel.ConfirmPostcode(Postcode1.ToUpper());
            await Task.Delay(5);
            _dialogService.Verify(e => e.DisplayAlertAsync("Postcode Confirmed", "", "Ok"));
        }
        [TestMethod]
        public async Task TestSubmitPostcodeNotOk()
        {
            _dialogService.Setup(e => e.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            ViewModel.Load();
            await Task.Delay(5);

            ViewModel.ConfirmPostcode(Postcode.ToUpper());
            await Task.Delay(5);
            _dialogService.Verify(e => e.DisplayAlertAsync("Something went wrong confirming your postcode!", "", "Ok"));
        }
    } }