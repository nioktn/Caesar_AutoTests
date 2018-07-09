using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Caesar
{
    public class MainPage
    {
        private IWebElement _profileButton, _rightMenu, _leftMenu, _profileExitButton;

        public IWebElement ProfileButton
        {
            get => _profileButton;
            private set => _profileButton = value;
        }

        public IWebElement RightMenu
        {
            get => _rightMenu;
            private set => _rightMenu = value;
        }

        public IWebElement LeftMenu
        {
            get => _leftMenu;
            set => _leftMenu = value;
        }

        public IWebElement ProfileExitButton
        {
            get => _profileExitButton;
            set => _profileExitButton = value;
        }

        public MainPage(IWebDriver driver)
        {
            if (IsMainPage(driver))
            {
                ProfileButton = driver.FindElement(By.XPath("//*[@id='icon']/div/img[@class='img-circle']"));
                RightMenu = driver.FindElement(By.Id("right-menu"));
                LeftMenu = driver.FindElement(By.Id("left-menu"));
                ProfileExitButton = driver.FindElement(By.XPath("//*[@id=\'right-menu']/div/a"));
            }
            else
            {
                throw new Exception("Main page is not opened");
            }
        }

        public void SignOutViaProfileExitButton()
        {
            ProfileExitButton.Click();
        }

        public void OpenProfileDataSection()
        {
            ProfileButton.Click();
        }

        public String GetRightMenuState()
        {
            return RightMenu.GetAttribute("class");
        }

        public static bool IsMainPage(IWebDriver driver)
        {
            return driver.FindElements(By.Id("main-section")).Count > 0 &&
                driver.FindElements(By.Id("left-side-bar")).Count > 0 &&
                driver.FindElements(By.Id("right-side-bar")).Count > 0 ?
                true : false;
        }
    }
}
