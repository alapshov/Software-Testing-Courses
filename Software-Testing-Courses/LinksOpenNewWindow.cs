using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    class LinksOpenNewWindow
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Func<IWebDriver, string> newWindowNow;
        private ReadOnlyCollection<IWebElement> countriesElements;
        private ReadOnlyCollection<IWebElement> linkElements;

        [SetUp]
        public void start()
        {

            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void LinksOpenNewWindowTest()
        {
            driver.Url = "http://litecart-lapshov.ru/admin/?app=countries&doc=countries";
            Autorization();
            countriesElements = driver
                .FindElements(By.CssSelector("i.fa.fa-pencil"));            
            
            for (int i = 0; i < countriesElements.Count; i++)
            {
                countriesElements = driver
                    .FindElements(By.CssSelector("i.fa.fa-pencil"));
                countriesElements[i].Click();
                linkElements = driver
                    .FindElements(By.CssSelector("i.fa.fa-external-link"));
                for (int j = 0; j < linkElements.Count; j++)
                {
                    string mainWindow = driver.CurrentWindowHandle;
                    IList<string> oldWindows = driver.WindowHandles;
                    linkElements[j].Click();
                    string newWindow = wait.Until(ThereIsWindowOtherThan(oldWindows));
                    driver.SwitchTo().Window(newWindow);
                    driver.Close();
                    driver.SwitchTo().Window(mainWindow);
                }
                driver.Navigate().Back();
            }
            
        }


        [TearDown]
        public void stop()
        {
           driver.Quit();
           driver = null;
        }
        //Метод определения нового окна
        public Func<IWebDriver, string> ThereIsWindowOtherThan(IList<string> oldWindows)
        {
            IList<string> newWindows = driver.WindowHandles;            
            for(int i = 0; i < oldWindows.Count; i++)
            {
                foreach(string newWindow in newWindows)
                {
                    if(!oldWindows.Contains(newWindow))
                    {
                        driver.SwitchTo().Window(newWindow);                        
                    }
                }
                newWindowNow = returnWindow;
                newWindowNow(driver);
            }
            return newWindowNow;            
        }
        //Возвращаем новое окно
        public string returnWindow(IWebDriver diver)
        {
            return driver.CurrentWindowHandle;
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
