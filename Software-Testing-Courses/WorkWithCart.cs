using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    [TestFixture]
    class WorkWithCart
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private ReadOnlyCollection<IWebElement> menuElements;
        private ReadOnlyCollection<IWebElement> subMenuElements;

        [SetUp]
        public void start()
        {

            driver = new ChromeDriver();
                
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void WorkWithCartTest()
        {

            driver.Url = "http://litecart-lapshov.ru"; 
            //Добавляем товар в корзину
            for (int i = 1; i <= 3; i++)
            {
                driver.FindElement(By.CssSelector("div.image-wrapper")).Click();
                try
                {
                    new SelectElement(driver
                        .FindElement(By.CssSelector("select[name='options[Size]']")))
                        .SelectByText("Small");
                }
                catch
                {

                }
                driver.FindElement(By.Name("add_cart_product")).Click();
                string s = driver.FindElement(By.CssSelector("span.quantity")).Text;
                wait.Until(ExpectedConditions
                    .TextToBePresentInElement(driver.FindElement(By.CssSelector("span.quantity")), i.ToString()));
                driver.Navigate().Back();
            }
            driver.FindElement(By.LinkText("Checkout »")).Click();
            //Удаляем товар из корзины
            ReadOnlyCollection<IWebElement> removeEmelnets = driver
                .FindElements(By.CssSelector("button[name='remove_cart_item']"));
            ReadOnlyCollection<IWebElement> summaryEmelnets = driver
                .FindElements(By.CssSelector("td[class='item']"));            
            for (int j = 0; j < removeEmelnets.Count; j++)
            {                
                driver.FindElement(By.CssSelector("button[name='remove_cart_item']")).Click();
                /*ждем пока внизу обновится таблица, проверяем что удаленный товар
                 * отсутсвует в таблице
                 */
                wait.Until(ExpectedConditions.StalenessOf(summaryEmelnets[j]));
                driver.Navigate().Refresh();

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
