using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Threading;
using Caesar;
using OpenQA.Selenium.Support.UI;

namespace Tests
{
    [TestFixture]
    class TC_1_02_02
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.Login("artur", "1234");
            mainPageInstance = new MainPage(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
        }

        [Test]
        public void ExecuteTest_ExitButtonClicked_LoginPageOpened()
        {
            mainPageInstance.OpenProfileDataSection();
            wait.Until(d => Equals("right-menu open", mainPageInstance.GetRightMenuState()));
            mainPageInstance.SignOutViaProfileExitButton();
            Assert.IsTrue(wait.Until(d => LoginPage.IsLoginPage(driver)));
        }

        [Test]
        public void ExecuteTest_SettingsProfileButton_ModalWindowOpened()
        {
            Assert.Fail();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}