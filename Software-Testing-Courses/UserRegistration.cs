using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    class UserRegistration
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
        public void UserRegistrationTest()
        {
            driver.Url = "http://litecart-lapshov.ru/";            
            string email = DateTime.Now.ToString().Replace(" ","").Replace(":","") + "@mail.ru";
            string pass = "12345";
            driver.FindElement(By.LinkText("New customers click here")).Click();
            driver.FindElement(By.Name("tax_id")).SendKeys("TestUser");
            driver.FindElement(By.Name("company")).SendKeys("TestCompany");
            driver.FindElement(By.Name("firstname")).SendKeys("TestFirstName");
            driver.FindElement(By.Name("lastname")).SendKeys("TestLastName");
            driver.FindElement(By.Name("address1")).SendKeys("TestArdress1");
            driver.FindElement(By.Name("address2")).SendKeys("TestArdress2");
            driver.FindElement(By.Name("postcode")).SendKeys("40015");
            driver.FindElement(By.Name("city")).SendKeys("TestCity");
            driver.FindElement(By.CssSelector("span.select2")).Click();            
            driver.FindElement(By.CssSelector("input.select2-search__field")).SendKeys("United States"+Keys.Enter);            
            IWebElement selectElement = driver.FindElement(By.CssSelector("select[name = 'zone_code']"));
            selectElement.Click();         
            new SelectElement(selectElement).SelectByIndex(rendomizeSelectElement(selectElement));            
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("+19999999999");
            driver.FindElement(By.Name("password")).SendKeys(pass);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(pass);
            driver.FindElement(By.Name("create_account")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys(pass);
            driver.FindElement(By.Name("login")).Click();                        
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

        public int rendomizeSelectElement(IWebElement selectElement)
        {
            ReadOnlyCollection<IWebElement> selectElementList = selectElement
                .FindElements(By.CssSelector("select[name = 'zone_code'] option"));                       
            return new Random().Next(0, selectElementList.Count - 1);
        }
    }
}
