using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverEbayPom.Pages;

namespace WebDriverEbayPom.Tests
{
   
    public class HomePageTest
    {
        private WebDriver driver;
        private HomePage homePage;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-search-engine-choice-screen");
            this.driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            this.homePage = new HomePage(driver);
            homePage.Open();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void CheckPageTitle()
        {
            Assert.That(homePage.GetPageTitle(), Is.EqualTo("Electronics, Cars, Fashion, Collectibles & More | eBay"));
        }

        [Test]
        public void CheckMonopolySearchResults()
        {
            homePage.SelectCategory("Toys & Hobbies");
            homePage.SearchWord("Monopoly");
            homePage.ClickSearchButton();

            StringAssert.Contains("Monopoly", homePage.GetHeading1Text());
            Assert.That(homePage.GetFirstResultPrice(), Is.Not.Empty);
            Assert.That(homePage.GetFirstResultShippingLabel(), Is.Not.Empty);

            StringAssert.Contains("Monopoly", homePage.GetHeading2Text());
            Assert.That(homePage.GetSecondtResultPrice(), Is.Not.Empty);
            Assert.That(homePage.GetSecondResultShippingLabel(), Is.Not.Empty);

            StringAssert.Contains("Monopoly", homePage.GetHeading3Text());
            Assert.That(homePage.GetThirdResultPrice(), Is.Not.Empty);
            Assert.That(homePage.GetThirdResultShippingLabel(), Is.Not.Empty);
        }
    }
}
