using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    [TestFixture]
    class LiteCartLoginTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void LiteCartTest()
        {
            //разворачивал на denwer поэтому такой  путь к сайту
            driver.Url = "http://litecart-lapshov.ru/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            /*делаем проверку на то что мы оказались в админке. 
             * "app-" - ' - это ID значка пунтка меню "Appearence"             * 
             */
            wait.Until(ExpectedConditions.ElementExists(By.Id("app-"))); 

        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
