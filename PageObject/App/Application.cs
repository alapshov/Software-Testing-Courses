using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PageObject.Pages;

namespace PageObject.App
{    
    class Application
    {
        private IWebDriver driver;
        private CartPage cartPage;
        private HomePage homePage;
        private ProductPage productPage;
        private WebDriverWait wait;

        public Application()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            cartPage = new CartPage(driver, wait);
            homePage = new HomePage(driver);            
            productPage = new ProductPage(driver, wait);
            driver.Url = "http://litecart-lapshov.ru";
            driver.Manage().Window.Maximize();
        }

        public void AddAndDeleteProductCart()
        {
            for (int i = 0; i < 3; i++)
            {
                homePage
                    .OpenPruduct();
                productPage
                    .AddProduct()
                    .VerifyAddProduct(i)
                    .Back();                
            }
            cartPage
                .OpenCart()
                .DeleteProduct();
        }

        public void Quit()
        {
            driver.Quit();
        }        


    }
}
