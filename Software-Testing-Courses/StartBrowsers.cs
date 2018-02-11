using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

namespace Software_Testing_Courses
{
    class StartBrowsers
    {
        private IWebDriver chromedriver;
        private IWebDriver geckodriver;
        private IWebDriver firefoxoldshema;
        private IWebDriver edgedriver;

        [SetUp]
        public void start()
        {           
            
            
        }
        [Test]
        //Запуск GoogleChrome
        public void StartChrome()
        {
            chromedriver = new ChromeDriver();
            chromedriver.Url = "https://www.google.ru";
            chromedriver.Quit();
            chromedriver = null;

        }
        [Test]
        //Запуск FireFox
        public void StartFireFox()
        {
            firefoxoldshema = new FirefoxDriver();
            firefoxoldshema.Url = "https://www.google.ru";
            firefoxoldshema.Quit();
            firefoxoldshema = null;
        }
        [Test]
        //Запуск FireFox по старой схеме
        public void StartFireFoxOldShema()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = true;
            geckodriver = new FirefoxDriver(options);
            geckodriver.Url = "https://www.google.ru";
            geckodriver.Quit();
            geckodriver = null;

        }
        [Test]
        //Запуск FireFox из указанной папки
        public void StartFireFoxFolder()
        {
            FirefoxBinary binary = new FirefoxBinary(@"C:\Program Files\Firefox Nightly\firefox.exe");
            IWebDriver driver = new FirefoxDriver(binary, new FirefoxProfile());            
            driver.Url = "https://www.google.ru";
            driver.Quit();
            driver = null;
        }
        [Test]
        //Запуск MicrosoftEdge
        public void StartMicrosoftEdge()
        {
            edgedriver = new EdgeDriver();
            edgedriver.Url = "https://www.google.ru";
            edgedriver.Quit();
            edgedriver = null;
        }

        [TearDown]
        public void stop()
        {
            
           
        }


    }
}
