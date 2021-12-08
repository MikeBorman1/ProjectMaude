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
using CPMA_Core_APP.Utils.Interfaces;
using CPMA_Core_APP.Tests.TestInterfaces;

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class ReportsPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private IGetReports getReportsService;
        private ISetUpvote setUpvoteService;
        private IUpvoteStore setUpvoteStoreService;

        public ReportsPageViewModel ViewModel { get; set; }

        public int Testrpid = 0;

        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            getReportsService = new MockGetReports();
            setUpvoteService = new MockSetUpvote();
            setUpvoteStoreService = new MockUpvoteStorage();
            ViewModel = new ReportsPageViewModel(_navigationService.Object, _dialogService.Object, getReportsService, setUpvoteService, setUpvoteStoreService);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            ViewModel = null;
            getReportsService = null;
            setUpvoteService = null;
            setUpvoteStoreService = null;
        }

        [TestMethod]
        public void TestLoadReportsQueue()
        {
            //Arrange
            ViewModel.LoadReportsQueue();
            //Assert
            _navigationService.Setup(e => e.NavigateAsync(Names.ReportsPageName, It.IsAny<NavigationParameters>()));
        }

        [TestMethod]
        public async Task TestExpandButton()
        {
            //Arrange

            ViewModel.Load();

            _navigationService.Setup(e => e.NavigateAsync(Names.SolveFormPageName, It.IsAny<NavigationParameters>()));

            //Pre Act Assert
            Assert.IsNotNull(ViewModel.ExpandReportButtonCommand);
            Assert.IsNotNull(ViewModel.ReportsQueue);

            //Act
            await Task.Run(() =>
            {
                ViewModel.ExpandButton(ViewModel.ReportsQueue.First().Report);
            });

            //Assert
            _navigationService.Verify(e => e.NavigateAsync(Names.SolveFormPageName, It.IsAny<NavigationParameters>()), Times.Once);
        }
    }
}
