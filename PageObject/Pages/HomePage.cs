using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PageObject.Pages
{
    class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenPruduct()
        {
            driver.FindElement(By.CssSelector("div.image-wrapper")).Click();
        }
    }
}
