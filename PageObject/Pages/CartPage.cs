using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObject.Pages
{
    class CartPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public CartPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        public CartPage OpenCart()
        {
            driver.FindElement(By.LinkText("Checkout »")).Click();
            return this;
        }
        public void DeleteProduct()
        {
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

    }
}
