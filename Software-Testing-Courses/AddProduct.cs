using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
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
            string productName = DateTime.Now.ToString().Replace(" ", "").Replace(":", "");
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)+
                "\\Image\\3-red-duck-1.png";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.CssSelector("li#app-:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("a.button:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("input[type = 'radio'][value='1']")).Click();
            driver.FindElement(By.Name("name[en]")).SendKeys(productName);
            driver.FindElement(By.Name("code")).SendKeys("Test");
            driver.FindElement(By.CssSelector("input[type='checkbox'][value='1']")).Click();
            driver.FindElement(By.CssSelector("input[type='checkbox'][value='1-2']")).Click();
            driver.FindElement(By.CssSelector("input[type='number'][name='quantity']")).Clear();
            driver.FindElement(By.CssSelector("input[type='number'][name='quantity']"))
                .SendKeys("50");           
            driver.FindElement(By.Name("new_images[]")).SendKeys(path.Replace("file:\\",""));
            driver.FindElement(By.CssSelector("input[name = 'date_valid_from']"))
                .SendKeys("26022018");
            driver.FindElement(By.CssSelector("input[name = 'date_valid_to']"))
                .SendKeys("26082018");
            driver.FindElement(By.LinkText("Information")).Click();
            IWebElement manufacturerSelect = driver.FindElement(By.Name("manufacturer_id"));
            new SelectElement(manufacturerSelect).SelectByText("ACME Corp.");
            driver.FindElement(By.Name("keywords")).SendKeys("testkeywords");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("testshort_description");            
            driver.FindElement(By.CssSelector("div.trumbowyg-editor")).SendKeys("testDescription");
            driver.FindElement(By.Name("head_title[en]")).SendKeys("testhead_title");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("meta_description");
            driver.FindElement(By.LinkText("Prices")).Click();
            driver.FindElement(By.Name("purchase_price")).SendKeys("50");
            IWebElement valuteSelect = driver.FindElement(By.Name("purchase_price_currency_code"));
            new SelectElement(valuteSelect).SelectByText("US Dollars");
            driver.FindElement(By.Name("prices[USD]")).SendKeys("50");
            driver.FindElement(By.Name("prices[EUR]")).SendKeys("50");
            driver.FindElement(By.Name("save")).Click();
            //Проверяем что продукт добавлен
            Assert.IsTrue(productName == driver.FindElement(By.LinkText(productName)).Text);
        }


        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

        
    }
}
