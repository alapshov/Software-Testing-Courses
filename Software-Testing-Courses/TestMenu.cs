using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    [TestFixture]
    class TestMenu
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
        public void LiteCartMenuTest()
        {

            driver.Url = "http://litecart-lapshov.ru/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            //Получаем все элементы меню для того что бы понять размерность списка
            menuElements = driver.FindElements(By.Id("app-"));
            //В цыклах прокликиваем все пункты меню, а также вложеные пункты
            for (int i = 0; i < menuElements.Count; i++)
            {
                /**Перед тем как кликнуть, а так же после клика по выбранному пункту меню получаем 
                 * элементы, иначе при следующем цылке элемент перестанет существовать, так как 
                 * по меню был произведен клик открылась новая страница и все элементы обновились            
                **/
                menuElements = driver.FindElements(By.Id("app-"));
                menuElements[i].Click();
                menuElements = driver.FindElements(By.Id("app-"));
                subMenuElements = menuElements[i].FindElements(By.CssSelector("ul li"));
                for (int j = 0; j < subMenuElements.Count; j++)
                {
                    menuElements = driver.FindElements(By.Id("app-"));
                    subMenuElements = menuElements[i].FindElement(By.TagName("ul"))
                        .FindElements(By.TagName("li"));
                    subMenuElements[j].Click();
                    //Проверяем существование элемента с тегом h1 после каждого клика
                    driver.FindElement(By.TagName("h1"));
                }
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
