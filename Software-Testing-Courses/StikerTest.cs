using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    [TestFixture]
    class StikerTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private ReadOnlyCollection<IWebElement> productElements;
        private ReadOnlyCollection<IWebElement> stikerElements;               
        private bool flag = true;
        string message;

        [SetUp]
        public void start()
        {        
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void LiteCartStikerTest()
        {
            
            driver.Url = "http://litecart-lapshov.ru/";
            productElements = driver.FindElements(By.CssSelector("li.product.column.shadow.hover-light"));
            for(int i = 0; i < productElements.Count; i++)
            {
                stikerElements = productElements[i]
                    .FindElements(By.CssSelector("div[class *='sticker']"));      
                //Проверяем что на каждый товар приходится один стикер
                if (stikerElements.Count != 1)
                {
                    flag = false;
                    message += " " + productElements[i].Text + ", ";
                }               
                
            }
         Assert.IsTrue(flag, "Количество стикеров на товаре: " +
                    message + " не равно 1");

        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
