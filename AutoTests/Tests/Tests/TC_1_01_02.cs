using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using Caesar;

namespace Tests
{
    [TestFixture]
    class TC_1_01_02
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
        }

        [Test]
        public void ExecuteTest_LoginWithInvalidLoginCredentials()
        {
            loginPageInstance.SetLoginFieldValue("aezakmi");
            loginPageInstance.SetPasswordFieldValue("password1");
            loginPageInstance.ClickLoginButton();
            Assert.IsTrue(LoginPage.IsLoginPage(driver));
        }

        [Test]
        public void ExecuteTest_LoginWithValidLoginCredentials()
        {
            loginPageInstance.SetLoginFieldValue("Dmytro");
            loginPageInstance.SetPasswordFieldValue("1234");
            loginPageInstance.ClickLoginButton();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            Assert.IsTrue(wait.Until(d => MainPage.IsMainPage(d)));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
