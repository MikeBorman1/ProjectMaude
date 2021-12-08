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

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class ScannerPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private IGetInfoBarcode _barcodeService;

        public ScannerPageViewModel ViewModel { get; set; }
        public NavigationParameters par;

        public string TestBarcode = "12345678";
        public string TestUnknownBarcode = "0000";

        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            _barcodeService = new MockGetInfoBarcode();
            ViewModel = new ScannerPageViewModel(_navigationService.Object, _dialogService.Object, _barcodeService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            ViewModel = null;
        }


        //Barcode is found
        [TestMethod]
        public void TestLoadBarcodeInfo()
        {
            //Arrange
            ViewModel.LoadInfoPage(TestBarcode);
            _navigationService.Setup(e => e.NavigateAsync(Names.InfoPageName, It.IsAny<NavigationParameters>()));

            //Assert
            _navigationService.Verify(e => e.NavigateAsync(Names.InfoPageName, It.IsAny<NavigationParameters>()), Times.Once);
        }

        //Barcode isn't found
        [TestMethod]
        public void TestLoadUnknownBarcodeInfo()
        {
            //Arrange
            ViewModel.LoadInfoPage(TestUnknownBarcode);
            _dialogService.Setup(e => e.DisplayAlertAsync("No Barcode found", "Would you like to search for your product?", "Yes", "No"));
            _navigationService.Setup(e => e.GoBackAsync());

            //Assert
            _dialogService.Verify(e => e.DisplayAlertAsync("No Barcode found", "Would you like to search for your product?", "Yes", "No"), Times.Once);
            _navigationService.Verify(e => e.GoBackAsync());
        }

        //If it cant find a barcode, does it go back?
        //Currently the user can just hit the back button
        
    }
}
