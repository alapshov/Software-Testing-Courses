using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    [TestFixture]
    class EmptyLogBrawser
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        ReadOnlyCollection<IWebElement> productElements;

       [SetUp]
        public void start()
        {            
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void EmptyLogBrawsersTest()
        {
            driver.Url = "http://litecart-lapshov.ru/admin/?app=catalog&doc=catalog&category_id=1";
            Autorization();
            productElements = driver
                .FindElements(By.
                CssSelector("tr.row td:nth-child(3) a[href*='product&category']"));
            for(int i = 0; i < productElements.Count; i++)
            {
                productElements = driver
                    .FindElements(By.
                    CssSelector("tr.row td:nth-child(3) a[href*='product&category']"));
                productElements[i].Click();              
                Assert.IsTrue(driver.Manage().Logs.GetLog("browser").Count == 0);
                driver.Navigate().Back();
            }
        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
        //Метод авторизации
        public void Autorization()
        {
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }
    }
}
