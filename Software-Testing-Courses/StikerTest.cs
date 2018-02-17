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
        private ReadOnlyCollection<IWebElement> stikerNewElements;
        private ReadOnlyCollection<IWebElement> stikerSaleElements;

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
            productElements = driver.FindElements(By.CssSelector("li[class='product column shadow hover-light']"));
            for(int i = 0; i < productElements.Count; i++)
            {
                stikerNewElements = productElements[i]
                    .FindElements(By.CssSelector("div[class='sticker new']"));
                stikerSaleElements = productElements[i]
                    .FindElements(By.CssSelector("div[class='sticker sale']"));
                //Считаем количество стикеров new + sale, 
                int count = stikerNewElements.Count + stikerSaleElements.Count;
                //Проверяем что на каждый товар приходится один стикер
                Assert.IsTrue(count == 1, "Количество стикеров на товаре не равно 1");
            }

        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
