using CPMA_Core_APP.API.Implementations;
using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
using CPMA_Core_APP.Tests.TestInterfaces;
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

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class InfoPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private IGetLocations _locationService;
        private ISetFlag _flagAPI;

        public InfoPageViewModel ViewModel { get; set; }
        public NavigationParameters par;

        public string TestProductName = "TestProductName";

        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            _locationService = new MockLocationService();
            ViewModel = new InfoPageViewModel(_navigationService.Object, _dialogService.Object, _locationService, _flagAPI);

            var mats = new List<Material>
                {
                    new Material()
                    {
                        Name = "TestMaterial",
                        ImageUrl = "1",
                        Recyclable = true,
                        RecycleBin = "TestBin",
                        Warnings = new ObservableCollection<Warning>()
                        {
                            new Warning()
                            {
                                WarningMessage = "TestWarning1"
                            },
                            new Warning()
                            {
                                WarningMessage = "TestWarning2"
                            }
                        }
                    }
                };

            SearchResult TestProduct = new SearchResult()
            {
                ProductID = 1,
                ProductName = TestProductName,
                ProductPhotoID = Guid.NewGuid(),
                Materials = new ObservableCollection<Material>(mats)
            };

            par = new NavigationParameters
            {
                { ParameterKeys.searchResult, TestProduct }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            _locationService = null;
            _flagAPI = null;
            ViewModel = null;
        }

        [TestMethod]
        public void TestTitle()
        {
            //Arrange
            ViewModel.Load(par);
            var textInfo = new CultureInfo("en-US", false).TextInfo;

            //Assert
            Assert.AreEqual(ViewModel.Title, textInfo.ToTitleCase(TestProductName));
        }

        [TestMethod]
        public void TestWarnings()
        {
            //Arrange
            ViewModel.Load(par);

            //Assert
            Assert.IsNotNull(ViewModel.Materials[0].Warnings);
            Assert.AreEqual(ViewModel.Materials[0].Warnings.Count(), 2);
        }

        [TestMethod]
        public void TestLocations()
        {
            //Arrange
            ViewModel.Load(par);

            //Assert
            Assert.IsNotNull(ViewModel.Materials[0].Latitude);
            Assert.IsNotNull(ViewModel.Materials[0].Longitude);
            
        }


        [TestMethod]
        public void TestImages()
        {
            //Arrange
            ViewModel.Load(par);

            //Assert
            Assert.IsNotNull(ViewModel.Materials[0].MaterialImage);
            Assert.IsInstanceOfType(ViewModel.Materials[0].MaterialImage, typeof(string));
        }




    }
}
