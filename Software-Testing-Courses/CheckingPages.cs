using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    [TestFixture]    
    class CheckingPages
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private ReadOnlyCollection<IWebElement> productElements;
        private string itemHomePage;
        private string itemProductPage;
        private bool flag;
        private string message;
        string driverType;
        string cssValue1;
        string cssValue2;
        string cssName;

        [SetUp]
        public void start()
        {
            //Для тестирования в нужном браузере, раскоментировать нужный driver и driverType
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            //Указать тип драйвера chrome, firefox или Edge 
            driverType = "Chrome";
            //driverType = "FireFox";
            //driverType = "Edge";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Url = "http://litecart-lapshov.ru/";
            flag = true;
            message = "";
        }
        
        //Тест названия продукции
        [Test]
        public void ProductNameTest()
        {
                                  
            Assert.IsTrue(CompareElements("div.name", "h1.title"),
                  "В некоторых элементах "+ message + "не совпадает название продукта");            

        }
        //Тест обычной цены
        [Test]
        public void ProductNormalPriceTest()
        {
            Assert.IsTrue(CompareElements("span.price", "span.price[itemprop='price']"),
                "Тест обычной цены. В некоторых элементах не совпадают цены");
        }
        //Тест акционной цены
        [Test]
        public void ProductPromotionalPriceTest()
        {
            Assert.IsTrue(CompareElements("strong.campaign-price", 
                "strong.campaign-price[itemprop='price']"),
                "Тест акционной цены. В некоторых элементах не совпадают цены");
        }
        //Тест шрифта обычной цены
        [Test]
        public void FontNormalPriceTest()
        {
            Assert.IsTrue(CheckingColorPrice("normal", "s.regular-price", "color"),
                "Шрифт обычной цены не серый");
            if (driverType == "Chrome")
            {
                cssValue1 = "line-through";
                cssValue2 = "line-through";
                cssName = "text-decoration-line";
            }
            if (driverType == "FireFox")
            {
                cssValue1 = "line-through";
                cssValue2 = "line-through";
                cssName = "text-decoration-line";
            }
            if (driverType == "Edge")
            {
                cssValue1 = "line-through";
                cssValue2 = "line-through";
                cssName = "text-decoration";
            }
            Assert.IsTrue(CheckingStyleElements("s.regular-price", cssName,
                cssValue1, cssValue2), "Шрифт обычной цены не перечеркнут");
        }
        //Тест шрифта акционной цены
        [Test]
        public void FontPromotionalPriceTest()
        {
            Assert.IsTrue(CheckingColorPrice("promo", "strong.campaign-price", "color"),
                "Шрифт промо цены не красный");
            if (driverType == "Chrome")
            {
                cssValue1 = "700";
                cssValue2 = "700";
            }
            if (driverType == "FireFox")
            {
                cssValue1 = "900";
                cssValue2 = "700";
            }
            if (driverType == "Edge")
            {
                cssValue1 = "700";
                cssValue2 = "700";
            }
            Assert.IsTrue(CheckingStyleElements("strong.campaign-price", "font-weight", cssValue1, cssValue2)
                ,"Шрифт промо цены не жирный");
        }
        //Тест проверяющий, что ширфт акционой цены больше чем ширфт обычной цены
        [Test]
        public void FontSizeTest()
        {
            Assert.IsTrue(ChekingFontSize("s.regular-price", "strong.campaign-price", "font-size"),
                "Шрифт промо цены не больше шрифта обычной цены");
        }

        //Метод сравнения элементов страницы
        /// <summary>
        /// Нужно указать локаторы элементов для сравнения
        /// </summary>
        /// <param name="itemHomePageLocator">Элемент домашней страницы</param>
        /// <param name="itemProductPageLocator">Элемент страницы продукта</param>
        /// <returns></returns>
        private bool CompareElements(string itemHomePageLocator, string itemProductPageLocator)
        {
            productElements = driver.FindElements(By.CssSelector("li.product"));
            for (int i = 0; i < productElements.Count; i++)
            {
                try
                {
                    productElements = driver.FindElements(By.CssSelector("li.product"));
                    itemHomePage = productElements[i]
                        .FindElement(By.CssSelector(itemHomePageLocator)).GetAttribute("textContent");
                    productElements[i].Click();
                    itemProductPage = driver.FindElement(By.CssSelector(itemProductPageLocator))
                        .GetAttribute("textContent");
                    if (itemHomePage != itemProductPage)
                    {                        
                        flag = false;
                    }
                    driver.Navigate().Back();
                }
                catch (NoSuchElementException ex)
                {
                    
                }
                
            }

            return flag;
        }
                
        //Медот проверяющий, что применен нужный цвет 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator">указать локатор</param>
        /// <param name="cssName">указать название css стиля</param>
        /// <param name="cssValue">указать значение взвращаемое css стилем</param>
        /// <returns></returns>
        private bool CheckingStyleElements(string locator, string cssName, string cssValue1, 
            string cssValue2)
        {
            productElements = driver.FindElements(By.CssSelector("li.product"));
            for (int i = 0; i < productElements.Count; i++)
            {
                try
                {
                    productElements = driver.FindElements(By.CssSelector("li.product"));
                    string itemHPTextDecor = productElements[i]
                        .FindElement(By.CssSelector(locator)).GetCssValue(cssName);
                    productElements[i].Click();
                    string itemPPTextDecor = driver.FindElement(By.CssSelector(locator))
                        .GetCssValue(cssName);                  
                    
                    //Выполняем проверку, что присутствует нужный  "text-decoration"
                    if (itemHPTextDecor != cssValue1)
                    {
                        flag = false;
                    }
                    if (itemPPTextDecor != cssValue2)
                    {
                        flag = false;
                    }                  
                    

                    driver.Navigate().Back();

                }
                catch (NoSuchElementException ex)
                {

                }                
            }
            return flag;
        }

        //Медот проверяющий, что применен нужный цвет
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typePrice">Указать promo или normal, в зависимости от
        /// того какая стоимиость проверяется</param>
        /// <param name="cssName"></param>
        /// <param name="cssValue"></param>
        /// <returns></returns>
        private bool CheckingColorPrice(string typePrice, string locator, string cssName)
        {
            productElements = driver.FindElements(By.CssSelector("li.product"));
            for (int i = 0; i < productElements.Count; i++)
            {
                try
                {
                    productElements = driver.FindElements(By.CssSelector("li.product"));                    
                    string itemHPTextColor = productElements[i]
                        .FindElement(By.CssSelector(locator)).GetCssValue(cssName);
                    productElements[i].Click();                   
                    string itemPPTextColor = driver.FindElement(By.CssSelector(locator))
                        .GetCssValue(cssName);                   
                    //Проверяем, что цвет серый сравниваем каналы RGB
                    string itemHPTextColorSplit = itemHPTextColor.Split("(".ToCharArray())[1]
                        .Split(")".ToCharArray())[0];
                    string rHP = itemHPTextColorSplit.Split(",".ToCharArray())[0].Trim();
                    string gHP = itemHPTextColorSplit.Split(",".ToCharArray())[1].Trim();
                    string bHP = itemHPTextColorSplit.Split(",".ToCharArray())[2].Trim();
                    string itemPPTextColorSplit = itemPPTextColor.Split("(".ToCharArray())[1]
                        .Split(")".ToCharArray())[0];
                    string rPP = itemPPTextColorSplit.Split(",".ToCharArray())[0].Trim();
                    string gPP = itemPPTextColorSplit.Split(",".ToCharArray())[1].Trim();
                    string bPP = itemPPTextColorSplit.Split(",".ToCharArray())[2].Trim();
                    if (typePrice == "normal") {
                        if (rHP != gHP | gHP != bHP)
                        {
                            flag = false;
                        }
                        if (rPP != gPP | gPP != bPP)
                        {
                            flag = false;
                        }
                    }
                    if (typePrice == "promo")
                    {
                        if (gHP != "0" & bHP != "0")
                        {
                            flag = false;
                        }
                        if (gPP != "0" & bPP != "0")
                        {
                            flag = false;
                        }
                    }
                    driver.Navigate().Back();

                }
                catch (NoSuchElementException ex)
                {

                }
            }
            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator1">Локатор первого элемента</param>
        /// <param name="locator2">Локатор второго элтемента</param>
        /// <param name="cssName">Название атрибута</param>
        /// <returns></returns>
        private bool ChekingFontSize(string locator1, string locator2, string cssName)
        {

            productElements = driver.FindElements(By.CssSelector("li.product"));
            for (int i = 0; i < productElements.Count; i++)
            {
                try
                {
                    productElements = driver.FindElements(By.CssSelector("li.product"));
                    string itemHPFontSize1 = productElements[i]
                        .FindElement(By.CssSelector(locator1)).GetCssValue(cssName);
                    string itemHPFontSize2 = productElements[i]
                        .FindElement(By.CssSelector(locator2)).GetCssValue(cssName);
                    productElements[i].Click();
                    string itemPPFontSize1 = driver.FindElement(By.CssSelector(locator1))
                        .GetCssValue(cssName);
                    string itemPPFontSize2 = driver.FindElement(By.CssSelector(locator2))
                        .GetCssValue(cssName);
                    double sizeHP1 = double.Parse(itemHPFontSize1.Replace("px", "").Replace(".",","));
                    double sizeHP2 = double.Parse(itemHPFontSize2.Replace("px", "").Replace(".", ","));
                    if (sizeHP1 > sizeHP2) 
                    {
                        flag = false;
                    }
                    double sizePP1 = double.Parse(itemPPFontSize1.Replace("px", "").Replace(".", ","));
                    double sizePP2 = double.Parse(itemPPFontSize2.Replace("px", "").Replace(".", ","));
                    if (sizePP1 > sizePP2)
                    {
                        flag = false;
                    }
                    driver.Navigate().Back();

                }
                catch (NoSuchElementException ex)
                {

                }
            }
            return flag;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
