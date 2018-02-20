using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Software_Testing_Courses
{
    [TestFixture]
    class SortCountriesAndGeoZones
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private ReadOnlyCollection<IWebElement> countriesRows;
        private ReadOnlyCollection<IWebElement> zonesRows;
        private List<String> countriesList = new List<string>();
        private List<String> verifyingList;
        private List<String> zonesList = new List<string>();

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }

        [Test]
        /* на странице http://localhost/litecart/admin/?app=countries&doc=countries
         * проверить, что страны расположены в алфавитном порядке
         * для тех стран, у которых количество зон отлично от нуля 
         * открыть страницу этой страны и там проверить, что зоны расположены в алфавитном порядке*/
        public void SortCountriesTest()
        {
            driver.Url = "http://litecart-lapshov.ru/admin/?app=countries&doc=countries";
            Autorization();
            countriesRows = driver.FindElements(By.CssSelector("tr.row"));
            for (int i = 0; i < countriesRows.Count; i++)
            {
                countriesList.Add(countriesRows[i].FindElement(By.CssSelector("td:nth-child(5)"))
                    .GetAttribute("textContent"));
                if (countriesRows[i].FindElement(By.CssSelector("td:nth-child(6)"))
                    .GetAttribute("textContent") != "0")
                {
                    countriesRows[i].FindElement(By.CssSelector("td:nth-child(5) a:nth-child(1)")).Click();
                    //Ищем зоны
                    zonesRows = driver.FindElements(By.CssSelector("table#table-zones"));
                    foreach (var zones in zonesRows)
                    {
                        zonesList.Add(zones.FindElement(By.CssSelector("td:nth-child(3)")).GetAttribute("textContent"));
                    }
                    //Проверка сортировки списка Zones
                    Assert.IsTrue(sortValidation(zonesList));

                    zonesList.Clear();
                    driver.Navigate().Back();
                    countriesRows = driver.FindElements(By.CssSelector("tr.row"));
                }
            }
            //Проверка сортировки списка Countries
            Assert.IsTrue(sortValidation(countriesList));

        }

        [Test]
        /*на странице http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones
         * зайти в каждую из стран и проверить, что зоны расположены в алфавитном порядке*/
        public void SortGeoZonesTest()
        {
            driver.Url = "http://litecart-lapshov.ru/admin/?app=geo_zones&doc=geo_zones";
            Autorization();
            countriesRows = driver.FindElements(By.CssSelector("tr.row"));
            for (int i = 0; i < countriesRows.Count; i++)
            {
                countriesRows[i].FindElement(By.CssSelector("td:nth-child(3) a:nth-child(1)")).Click();
                //Ищем зоны
                zonesRows = driver.FindElements(By.CssSelector("select[name *='zone_code']"));
                foreach (var zones in zonesRows)
                {
                    zonesList.Add(zones.FindElement(By.CssSelector("option[selected='selected']")).GetAttribute("textContent"));
                }
                //Проверка сортировки списка Zones
                Assert.IsTrue(sortValidation(zonesList));

                zonesList.Clear();
                driver.Navigate().Back();
                countriesRows = driver.FindElements(By.CssSelector("tr.row"));
            }
        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
        //Метод авторизации
        public void Autorization()
        {
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }
        //Метод проверки сортировки
        public bool sortValidation(List<string> listCountries)
        {
            verifyingList = new List<string>(listCountries);
            verifyingList.Sort();
            for (int i = 0; i < listCountries.Count; i++)
            {
                if (listCountries[i] != verifyingList[i])
                    return false;
            }
            return true;
        }
    }
}
