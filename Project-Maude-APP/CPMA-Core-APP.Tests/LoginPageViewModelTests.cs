using CPMA_Core_APP.API.Interfaces;
using CPMA_Core_APP.Tests.TestInterfaces;
using CPMA_Core_APP.Utils.Interfaces;
using CPMA_Core_APP.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static CPMA_Core_APP.Common.StaticVariables;
using static CPMA_Core_APP.Common.StaticVariables.Pages;

namespace CPMA_Core_APP.Tests
{
    //[TestClass]
    //public class LoginPageViewModelTests
    //{
    //    private Mock<INavigationService> _navigationService;
    //    private ILogin _loginService;
    //    private ISecureStore _secureStoreService;
    //    public LoginPageViewModel ViewModel { get; set; }

    //    [TestInitialize]
    //    public void Init()
    //    {
    //        _navigationService = new Mock<INavigationService>();
    //        _loginService = new MockLogin();
    //        _secureStoreService = new MockSecureStore();
    //        ViewModel = new LoginPageViewModel(_navigationService.Object, _loginService, _secureStoreService);
    //    }

    //    [TestCleanup]
    //    public void Cleanup()
    //    {
    //        _navigationService = null;
    //        _loginService = null;
    //        ViewModel = null;
    //    }

    //    [TestMethod]
    //    public async Task LoginMethod_SuccessfulLogon()
    //    {
    //        //Arrange
    //        ViewModel.Load();
    //        ViewModel.Username = TestingStaticVariables.TestingUsername;
    //        ViewModel.Password = TestingStaticVariables.TestingPassword;
    //        _navigationService.Setup(e => e.NavigateAsync(Names.LoadingPageName, It.IsAny<NavigationParameters>()));

    //        //Act
    //        await ViewModel.Login();

    //        //Assert
    //        Assert.IsNull(ViewModel.ErrorMessage);
    //        _navigationService.Verify(e => e.NavigateAsync(Names.LoadingPageName, It.IsAny<NavigationParameters>()), Times.Once);
    //    }

    //    [TestMethod]
    //    public async Task LoginMethod_IncorrectUsername()
    //    {
    //        //Arrange
    //        ViewModel.Load();
    //        ViewModel.Username = TestingStaticVariables.TestingIncorrectUsername;
    //        ViewModel.Password = TestingStaticVariables.TestingPassword;
    //        _navigationService.Setup(e => e.NavigateAsync(Names.LoadingPageName, It.IsAny<NavigationParameters>()));

    //        //Act
    //        await ViewModel.Login();

    //        //Assert
    //        Assert.AreEqual(ViewModel.ErrorMessage, TestingStaticVariables.TestingLogonError);
    //        _navigationService.Verify(e => e.NavigateAsync(Names.LoadingPageName, It.IsAny<NavigationParameters>()), Times.Never);
    //    }

    //    [TestMethod]
    //    public async Task LoginMethod_IncorrectPassword()
    //    {
    //        //Arrange
    //        ViewModel.Load();
    //        ViewModel.Username = TestingStaticVariables.TestingUsername;
    //        ViewModel.Password = TestingStaticVariables.TestingIncorrectPassword;
    //        _navigationService.Setup(e => e.NavigateAsync(Names.LoadingPageName, It.IsAny<NavigationParameters>()));

    //        //Act
    //        await ViewModel.Login();

    //        //Assert
    //        Assert.AreEqual(ViewModel.ErrorMessage, TestingStaticVariables.TestingLogonError);
    //        _navigationService.Verify(e => e.NavigateAsync(Names.LoadingPageName, It.IsAny<NavigationParameters>()), Times.Never);
    //    }

    //    [TestMethod]
    //    public async Task LoginMethod_IncorrectBoth()
    //    {
    //        //Arrange
    //        ViewModel.Load();
    //        ViewModel.Username = TestingStaticVariables.TestingIncorrectUsername;
    //        ViewModel.Password = TestingStaticVariables.TestingIncorrectPassword;
    //        _navigationService.Setup(e => e.NavigateAsync(Names.LoadingPageName, It.IsAny<NavigationParameters>()));

    //        //Act
    //        await ViewModel.Login();

    //        //Assert
    //        Assert.AreEqual(ViewModel.ErrorMessage, TestingStaticVariables.TestingLogonError);
    //        _navigationService.Verify(e => e.NavigateAsync(Names.LoadingPageName, It.IsAny<NavigationParameters>()), Times.Never);
    //    }
    //}
}
