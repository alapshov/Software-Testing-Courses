using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    [TestFixture]
    class MyFirstTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
       
        [SetUp]
        public void start()
        {
            driver = new ChromeDriver(@"C:\Driver"); //Указать путь к драйверу
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));           

        }
        [Test]
        public void FirstTest()
        {
            driver.Url = "https://www.google.ru";
            driver.FindElement(By.Name("q")).SendKeys("Тестовый поиск");
            driver.FindElement(By.XPath(@"//*[@id=""sbtc""]/div[2]/div[2]/div[1]/div/ul/li[2]/div/span[1]/span/input")).Click();
        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}
