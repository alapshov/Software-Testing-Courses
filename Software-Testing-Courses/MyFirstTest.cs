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
            /*Указать путь к драйверу можно прямо в конструктор объекта ChromeDriver, 
             * таким образом исполняемый файл драйвера 
             * не нужно будет добавлять в переменные среды */
            driver = new ChromeDriver(@"C:\Driver"); 
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void FirstTest()
        {
            driver.Url = "https://www.google.ru";            
            driver.FindElement(By.Name("q")).SendKeys("webdriver");            
            driver.FindElement(By.Name("btnK")).Click();
            wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }
        [TearDown]
        public void stop()
        {
           driver.Quit();
           driver = null;
        }

    }
}
