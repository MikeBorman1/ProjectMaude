using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Model;
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
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.ViewModels.HomeModule.SolveFormPageViewModel;
using static CPMA_Core_APP.ViewModels.MaterialsPageViewModel;
using CPMA_Core_APP.API.TestInterfaces;
using CPMA_Core_APP.API;
using System.Threading.Tasks;
using static CPMA_Core_APP.Common.StaticVariables.Pages;

namespace CPMA_Core_APP.Tests
{
    [TestClass]
    public class MaterialsPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IPageDialogService> _dialogService;
        private IGetMaterials _materialsAPI;

        public MaterialsPageViewModel ViewModel { get; set; }
        public NavigationParameters par;
        public MaterialVM materialVM;


        [TestInitialize]
        public void Init()
        {
            _navigationService = new Mock<INavigationService>();
            _dialogService = new Mock<IPageDialogService>();
            _materialsAPI = new MockGetMaterials();
            ViewModel = new MaterialsPageViewModel(_navigationService.Object, _dialogService.Object, _materialsAPI);

            var report = new Report()
            {
                ReportTitle = "TestReport",
                ReportImageUrl = null,
                Barcode = "12345678",
                ReportScore = 0,
                ReportId = 1
            };

            var material = new MaterialSearch()
            {
                 Name = "TestMaterial",
                 RecycleBin ="Green",
                 Recyclable = true,
                 ImageUrl = null,
                 MaterialID = 1,
                 IsBin = true
            };

            materialVM = new MaterialVM(material)
            {
                Name = "TestMaterialVM",
                Material = material
            };

            SolveVM solveVM = new SolveVM(report)
            {
                Report = report,
                Materials = new ObservableCollection<MaterialVM>()
                {}
            };

            par = new NavigationParameters
            {
                { ParameterKeys.solveVM, solveVM }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _navigationService = null;
            _dialogService = null;
            ViewModel = null;
        }

        [TestMethod]
        public async Task TestAddSelected()
        {
            //Arrange
            ViewModel.Load(par);
            await Task.Delay(5);
            ViewModel.AddSelected(materialVM);
            Assert.IsNotNull(ViewModel.SelectedMaterials);
            //Assert
            Assert.IsTrue(ViewModel.SelectedMaterials.Count() > 0);
        }

        [TestMethod]
        public async Task TestSearchBar()
        {
            //Arrange
            ViewModel.Load(par);
            ViewModel.SearchBar("");
            await Task.Delay(5);
            Assert.IsNotNull(ViewModel.Materials);
            var c = ViewModel.Materials.Count();
            ViewModel.SearchBar("p");
            //Assert
            Assert.IsTrue(c > ViewModel.Materials.Count());
        }

        [TestMethod]
        public async Task TestSelectedSubmit()
        {
            //Arrange
            ViewModel.Load(par);
            await Task.Delay(5);
            ViewModel.AddSelected(materialVM);
            ViewModel.Submit();
            //Assert
            _navigationService.Verify(e => e.GoBackAsync(), Times.Once);
        }

        [TestMethod]
        public async Task TestUnselectedSubmit()
        {
            //Arrange
            ViewModel.Load(par);
            await Task.Delay(5);
            ViewModel.Submit();
            //Assert
            _dialogService.Verify(e => e.DisplayAlertAsync("Warning", "You must select at least one material", "Ok"), Times.Once);
        }
    }
}
