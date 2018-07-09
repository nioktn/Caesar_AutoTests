using Caesar;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Tests
{
    [TestFixture]
    class TC_1_02_01
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
            loginPageInstance.Login("sasha", "1234");
            mainPageInstance = new MainPage(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void ExecuteTest_ProfileButtonClick_RightMenuOpened()
        {
            //mainPageInstance.OpenProfileDataSection();
            mainPageInstance.ProfileButton.Click();
            Assert.AreEqual("right-menu open", mainPageInstance.GetRightMenuState());
        }

        [Test]
        public void Executetest_DropMouseFocus_RightMenuClosed()
        {
            ////mouseout(mainPageInstance.RightMenu);
            //Actions act = new Actions(driver);
            IJavaScriptExecutor jse = driver as IJavaScriptExecutor;
            //act.MoveToElement(mainPageInstance.ProfileButton)
            //    .Click()
            //    .Perform();
            Thread.Sleep(1000);
            mainPageInstance.OpenProfileDataSection();
           .ExecuteScript("document.getElementByClassName('right-menu open').mouseout();");
            //Thread.Sleep(3000);
            //Actions acts1 = new Actions(driver);
            //acts1.MoveToElement(mainPageInstance.ProfileButton).Click().Build().Perform();
            //acts1.MoveByOffset(40, 40).Perform();
            Assert.IsTrue(wait.Until<bool>(d => Equals("right-menu", mainPageInstance.GetRightMenuState())));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}