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

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class RecycleInfoPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private IGetRecycleInfo _recycleInfo;

        public RecycleInfoPageViewModel ViewModel { get; set; }
        public NavigationParameters par;

       

        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            _recycleInfo = new MockRecycleInfo();
            ViewModel = new RecycleInfoPageViewModel(_navigationService.Object, _dialogService.Object, _recycleInfo);

        }

        [TestCleanup]
        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            _recycleInfo = null;
            ViewModel = null;
        }

        [TestMethod]
        public async Task TestRecycleInfo()
        {
            //Arrange
            ViewModel.Load();
            while (!ViewModel.isLoaded)
            {
                await Task.Delay(5);
            }


            //Assert
            Assert.IsNotNull(ViewModel.Model[0].AcceptedItems);
            Assert.IsNotNull(ViewModel.Model[0].RejectedItems);
            
        }

       


    }
}
