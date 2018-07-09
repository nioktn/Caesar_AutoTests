using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Caesar
{
    public class LoginPage
    {
        private IWebElement _loginField, _passwordField, _loginButton, _messageField;
        private IWebDriver _loginDriver;
        public IWebElement LoginField
        {
            get => _loginField;
            private set => _loginField = value;
        }

        public IWebElement PasswordField
        {
            get => _passwordField;
            private set => _passwordField = value;
        }

        public IWebElement LoginButton
        {
            get => _loginButton;
            private set => _loginButton = value;
        }

        public IWebElement MessageField
        {
            get => _messageField;
            set => _messageField = value;
        }

        public LoginPage(IWebDriver driver)
        {
            if (IsLoginPage(driver))
            {
                _loginDriver = driver;
                LoginField = _loginDriver.FindElement(By.Name("login"));
                PasswordField = _loginDriver.FindElement(By.Name("password"));
                MessageField = _loginDriver.FindElement(By.ClassName("message"));
                LoginButton = _loginDriver.FindElement(By.CssSelector("body > div.app > div > div > div > button"));
            }
            else
            {
                throw new Exception("Login page is not opened");
            }
        }

        public void SetLoginFieldValue(String value)
        {
            LoginField.SendKeys(value);
        }

        public void SetPasswordFieldValue(String value)
        {
            PasswordField.SendKeys(value);
        }

        public String GetLoginFieldValue()
        {
            return LoginField.GetAttribute("value");
        }

        public String GetPasswordFieldValue()
        {
            return PasswordField.GetAttribute("value");
        }

        public void ClickLoginButton()
        {
            LoginButton.Click();
        }

        public void Login(String login, String password)
        {
            LoginField.SendKeys(login);
            PasswordField.SendKeys(password);
            LoginButton.Click();
            _loginDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static bool IsLoginPage(IWebDriver driver)
        {
            return driver.FindElements(By.Name("login")).Count > 0 &&
                driver.FindElements(By.Name("password")).Count > 0 &&
                driver.FindElements(By.CssSelector("body > div.app > div > div > div > button")).Count > 0 ?
                true : false;
        }
    }
}
