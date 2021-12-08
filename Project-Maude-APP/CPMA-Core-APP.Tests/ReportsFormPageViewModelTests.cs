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
using CPMA_Core_APP.Tests.TestInterfaces;
using Plugin.Permissions.Abstractions;

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class ReportsFormPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private IGetReports getReportsService;
        private ISetReport setReportService;
        private IStorage apiStorageService;

        public ReportsFormPageViewModel ViewModel { get; set; }

        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            getReportsService = new MockGetReports();
            setReportService = new MockSetReport();
            apiStorageService = new MockStorage();
            ViewModel = new ReportsFormPageViewModel(_navigationService.Object, _dialogService.Object, setReportService, apiStorageService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            ViewModel = null;
            getReportsService = null;
            setReportService = null;
            apiStorageService = null;
        }
        
        [TestMethod]
        public async Task TestConfirmSubmit()
        {
            //arrange
            _navigationService.Setup(e => e.GoBackToRootAsync(It.IsAny<NavigationParameters>()));
            //act
            await ViewModel.ConfirmSubmit();
            //assert
            _navigationService.Verify(e => e.GoBackToRootAsync(It.IsAny<NavigationParameters>()), Times.Once);
        }
        //test for not all the data
        [TestMethod]
        public async Task TestSubmit()
        {
            _dialogService.Setup(e => e.DisplayAlertAsync("Submit Report?", "", "Ok", "Cancel")).Returns(Task.Run(() => true));
            ViewModel.Submit();
            _dialogService.Verify(e => e.DisplayAlertAsync("Unfinished Report", "Please fill in all required data", "Ok"));

        }
        //test for all the data
        [TestMethod]
        public async Task TestDataSubmit()
        {
            _dialogService.Setup(e => e.DisplayAlertAsync("Submit Report?", "", "Ok", "Cancel")).Returns(Task.Run(() => true));

            ViewModel.ReportImage="hehe";
            ViewModel.ReportImageID = "1";
            ViewModel.ReportTitle= "ihateunittests";
            ViewModel.Submit();
            await Task.Delay(5);
            _dialogService.Verify(e => e.DisplayAlertAsync("Submit Report?", "", "Ok", "Cancel"));

        }


        [TestMethod]

        public async Task TestBarcodeCommand()
        {
            //Arrange

            ViewModel.Load();

            _navigationService.Setup(e => e.NavigateAsync(Names.ScannerPage, It.IsAny<NavigationParameters>()));

            //Pre Act Assert
            Assert.IsNotNull(ViewModel.ScanBarcodeButtonCommand);

            //Act
            await ViewModel.BarcodeScanner();

            //Assert
            _navigationService.Verify(e => e.NavigateAsync(Names.ScannerPage, It.IsAny<NavigationParameters>()), Times.Once);

        }
    }
}
