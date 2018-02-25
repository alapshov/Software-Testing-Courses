using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    class AddProduct
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private ReadOnlyCollection<IWebElement> menuElements;
        private ReadOnlyCollection<IWebElement> subMenuElements;

        [SetUp]
        public void start()
        {

            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void AddProductTest()
        {

            driver.Url = "http://litecart-lapshov.ru/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.CssSelector("li#app-:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("a.button:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("input[type = 'radio'][value='1']")).Click();
            driver.FindElement(By.Name("name[en]")).SendKeys("DuckTest");
            driver.FindElement(By.Name("code")).SendKeys("Test");
            driver.FindElement(By.CssSelector("input[type='checkbox'][value='1']")).Click();
            driver.FindElement(By.CssSelector("input[type='checkbox'][value='1-2']")).Click();
            driver.FindElement(By.CssSelector("input[type='number'][name='quantity']")).Clear();
            driver.FindElement(By.CssSelector("input[type='number'][name='quantity']"))
                .SendKeys("50");

        }


        [TearDown]
        public void stop()
        {
            //driver.Quit();
            //driver = null;
        }
    }
}
