using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.API.TestInterfaces;
using CPMA_Core_APP.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class SearchResultsPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private IGetSearchResults _searchService;
        private IGetInfoBarcode _barcodeService;

        public SearchResultsPageViewModel ViewModel { get; set; }

        public string keyword { get; set; } = "SearchTest";

        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            _searchService = new MockGetSearchResults();
            _barcodeService = new MockGetInfoBarcode();
            ViewModel = new SearchResultsPageViewModel(_navigationService.Object, _dialogService.Object, _searchService, _barcodeService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            _searchService = null;
            ViewModel = null;
        }


        [TestMethod]
        public async Task TestSearchAPICall()
        {
            //Arrange
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, keyword }
            };
            ViewModel.Load(par);
            while (!ViewModel.LoadComplete)
            {
                await Task.Delay(5);
            }

            _navigationService.Setup(e => e.NavigateAsync(Names.InfoPageName, It.IsAny<NavigationParameters>()));

            //Assert
            Assert.IsNotNull(ViewModel.SearchResults);
        }


        [TestMethod]
        public async Task TestExpandButtonFunction()
        {
            //Arrange
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, keyword }
            };
            ViewModel.Load(par);

            _navigationService.Setup(e => e.NavigateAsync(Names.InfoPageName, It.IsAny<NavigationParameters>()));

            //Pre Act Assert
            Assert.IsNotNull(ViewModel.ExpandSearchResultButtonCommand);
            Assert.IsNotNull(ViewModel.SearchResults);

            //Act
            await Task.Run(() =>
            {
                ViewModel.ExpandButton(ViewModel.SearchResults.First());
            });

            //Assert
            _navigationService.Verify(e => e.NavigateAsync(Names.InfoPageName, It.IsAny<NavigationParameters>()), Times.Once);
        }


        [TestMethod]
        public async Task TestSearchResultTitleCaseName()
        {
            //Arrange
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, keyword }
            };
            ViewModel.Load(par);
            while (!ViewModel.LoadComplete)
            {
                await Task.Delay(5);
            }
            //Assert
            Assert.AreEqual(ViewModel.SearchResults[0].SearchResult.ProductName, "Plastic Bottle");
        }


        [TestMethod]
        public void TestInitialText()
        {
            //Arrange
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, keyword }
            };
            ViewModel.Load(par);

            //Assert
            Assert.AreEqual(ViewModel.InitialText, keyword);
        }


        [TestMethod]
        public async Task TestSearchResults()
        {
            //Arrange
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, keyword }
            };
            ViewModel.Load(par);
            while (!ViewModel.LoadComplete)
            {
                await Task.Delay(5);
            }
            //Assert
            Assert.IsNotNull(ViewModel.SearchResults);
            Assert.AreNotEqual(ViewModel.SearchResults.Count(), 0);
        }


        [TestMethod]
        public async Task TestSearchResultsKeyPoints()
        {
            //Arrange
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, keyword }
            };
            ViewModel.Load(par);
            while (!ViewModel.LoadComplete)
            {
                await Task.Delay(5);
            }

            //Assert
            Assert.IsTrue(ViewModel.SearchResults[0].KeyPoints.Count() != 0);
        }


        [TestMethod]
        public async Task TestSearchResultsKeyPointsOverflow()
        {
            //Arrange
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, keyword }
            };
            ViewModel.Load(par);
            while (!ViewModel.LoadComplete)
            {
                await Task.Delay(5);
            }
            Assert.AreNotEqual(ViewModel.SearchResults.Count(), 0);

            //Assert
            Assert.IsTrue(ViewModel.SearchResults[0].KeyPoints.Count() <= 3);
        }


        [TestMethod]

        public async Task TestImages()
        {
            //Arrange
            var par = new NavigationParameters
            {
                { ParameterKeys.keywordSearch, keyword }
            };
            ViewModel.Load(par);
            while (!ViewModel.LoadComplete)
            {
                await Task.Delay(5);
            }

            //Assert
            var i = ViewModel.SearchResults[0].Image;
            Assert.IsInstanceOfType(i , typeof(string));
        }
    }
}
