using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;
using Caesar;

namespace Tests
{
    [TestFixture]
    class TC_1_01_04
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
        }

        [Test]
        public void ExecuteTest_EmptyFields_LoginButtonClick_NoChanges()
        {
            loginPageInstance.ClickLoginButton();
            Assert.IsTrue(LoginPage.IsLoginPage(driver));
        }

        [Test]
        public void ExecuteTest_EmptyFields_EnterPressed_NoChanges()
        {
            loginPageInstance.LoginField.SendKeys(Keys.Enter);
            Assert.IsTrue(LoginPage.IsLoginPage(driver));
        }

        [Test]
        public void ExecuteTest_EmptyLoginField_LoginButtonUnactive()
        {
            loginPageInstance.SetPasswordFieldValue("pass");
            Assert.IsFalse(loginPageInstance.LoginButton.Enabled);
        }

        [Test]
        public void ExecuteTest_EmptyPassField_LoginButtonUnactive()
        {
            loginPageInstance.SetLoginFieldValue("log1");
            Assert.IsFalse(loginPageInstance.LoginButton.Enabled);
        }

        [Test]
        public void ExecuteTest_InvalidValues_ErrorMessage()
        {
            List<String> logins = new List<String>() { "login1", "#$%^#${}", "4f4&4" };
            List<String> passwords = new List<String>() { "$%^&#*", "pass2", "#@#@#@" };
            int i = 0;
            while (i < logins.Count)
            {
                loginPageInstance.SetLoginFieldValue(logins[i]);
                loginPageInstance.SetPasswordFieldValue(passwords[i]);
                loginPageInstance.ClickLoginButton();
                Assert.AreEqual(logins[i], loginPageInstance.GetLoginFieldValue());
                Assert.AreEqual(String.Empty, loginPageInstance.GetPasswordFieldValue());
                String expectedMessage = "Incorrect login or password. Please, try again";
                Assert.AreEqual(expectedMessage, loginPageInstance.MessageField.Text);
                loginPageInstance.LoginField.SendKeys(Keys.Escape);
                i++;
            }
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
