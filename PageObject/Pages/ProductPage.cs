using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObject.Pages
{
    class ProductPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ProductPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        //Добавляем продукт в карзину
        public ProductPage AddProduct()
        {
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

            return this;
        }
        //Проверяем что изменилось количество товаров в карзине
        public ProductPage VerifyAddProduct(int i)
        {

            wait.Until(ExpectedConditions
                .TextToBePresentInElement(driver.FindElement(By
                .CssSelector("span.quantity")), i.ToString()));
            return this;
        }
        //Вызвращаемся на домашнюю страницу
        public void Back()
        {
            driver.Navigate().Back();
        }       
                
                
    }
}
