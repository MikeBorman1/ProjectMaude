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
using CPMA_Core_APP.ViewModels.HomeModule;
using CPMA_Core_APP.API;
using static CPMA_Core_APP.ViewModels.MaterialsPageViewModel;
using CPMA_Core_APP.Utils.Interfaces;

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class SolveFromPageViewModelTest
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private Mock<ISetProductMaterialLink> _InfoLinkService;
        private IStorage _apiStorageService;
        public string TestProductName = "Solve Form";
        public SolveFormPageViewModel ViewModel { get; set; }
        public NavigationParameters par;
        public ObservableCollection<MaterialVM> materialVM;



        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            _InfoLinkService = new Mock<ISetProductMaterialLink>();
            _apiStorageService = new MockStorage();
            ViewModel = new SolveFormPageViewModel(_navigationService.Object, _dialogService.Object, _InfoLinkService.Object, _apiStorageService);


            Report TestReport = new Report()
            {
                ReportImageUrl = Guid.NewGuid().ToString(),
                ReportScore = 50,
                ReportTitle = "Glass Bottle",
                Barcode = "123456789",
                ReportId = 1
            };
            MaterialSearch materialSearch = new MaterialSearch()
            {
                Name = "Glass",
                Recyclable = true,
                ImageUrl = Guid.NewGuid().ToString(),
                RecycleBin = "Glass",
                IsBin = true,
                MaterialID = 1

            };

            materialVM = new ObservableCollection<MaterialVM>(){
                new MaterialVM(materialSearch)
            };

            par = new NavigationParameters
            {
                { ParameterKeys.report, TestReport }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            _InfoLinkService = null;
            _apiStorageService = null;
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
        public async Task TestLoadReport()
        {
            //Arrange
            ViewModel.Load(par);

            while (!ViewModel.isLoaded)
            {
                await Task.Delay(5);
            }


            //Assert
            Assert.IsNotNull(ViewModel.Model);

        }

        [TestMethod]
        public async Task TestSubmit()
        {
            _InfoLinkService.Setup(e => e.SetLink("Glass Bottle", It.IsAny<string>(), "123456789", It.IsAny<string>()));
            //Arrange
            ViewModel.Load(par);
            while (!ViewModel.isLoaded)
            {
                await Task.Delay(5);
            }
            

            ViewModel.Model.Materials = new ObservableCollection<MaterialVM>(materialVM);
            ViewModel.Submit();
            
            await Task.Delay(5);
            

            _InfoLinkService.Verify(e => e.SetLink("Glass Bottle", It.IsAny<string>(), "123456789", It.IsAny<string>()));

            //Assert
            Assert.IsNotNull(ViewModel.Model);
        }

        [TestMethod]
        public async Task TestMaterialButton()
        {
            ViewModel.Load();

            _navigationService.Setup(e => e.NavigateAsync(Names.MaterialsPageName, It.IsAny<NavigationParameters>()));


            await Task.Run(() =>
            {
                ViewModel.MaterialsButton();
            });

            _navigationService.Verify(e => e.NavigateAsync(Names.MaterialsPageName, It.IsAny<NavigationParameters>()), Times.Once);
        }

        [TestMethod]
        public async Task TestSubmitNav()
        {
            ViewModel.Load();

            _navigationService.Setup(e => e.GoBackAsync());

            ViewModel.Load(par);
            while (!ViewModel.isLoaded)
            {
                await Task.Delay(5);
            }


            ViewModel.Model.Materials = new ObservableCollection<MaterialVM>(materialVM);
            ViewModel.Submit();

            await Task.Delay(5);
            _navigationService.Verify(e => e.GoBackAsync(), Times.Once);
        }
    }
}
