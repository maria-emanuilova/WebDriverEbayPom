using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebDriverEbayPom.Pages;

namespace WebDriverEbayPom.Tests
{
    public class OrderDetailsPageTest
    {
        private WebDriver driver;
        private OrderDetailsPage detailsPage;
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
            this.detailsPage = new OrderDetailsPage(driver);
            homePage.Open();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void Test_CheckSelectedItemDetails()
        {
            homePage.SelectCategory("Toys & Hobbies");
            homePage.SearchWord("Monopoly");
            homePage.ClickSearchButton();
            string firstItemPrice = homePage.GetFirstResultPrice();
            homePage.ClickFirstSearchResult();

            var detailsTab = driver.WindowHandles[1];
            this.driver.SwitchTo().Window(detailsTab);
            StringAssert.Contains("Monopoly", detailsPage.GetItemTitleText());
            string itemPrice = detailsPage.GetItemPriceText();
            Assert.That(itemPrice, Is.EqualTo(firstItemPrice)); //test currently fails because item price is in AU dollars
        }

        [Test]
        public void Test_CheckThatItemCanBeOrdered()
        {
            homePage.SelectCategory("Toys & Hobbies");
            homePage.SearchWord("Monopoly");
            homePage.ClickSearchButton();
            homePage.ClickFirstSearchResult();

            var detailsTab = driver.WindowHandles[1];
            this.driver.SwitchTo().Window(detailsTab);
            string desiredQty = "2";

            string itemPriceText = detailsPage.GetItemPriceText();
            var itemPriceNum = Regex.Match(itemPriceText, @"\d+\.\d+").Value.Replace(".", ",");
            var itemPriceTotal = double.Parse(itemPriceNum) * int.Parse(desiredQty);
        
            
            detailsPage.FillItemQty(desiredQty);
            detailsPage.clickAddToCartButton();
            Assert.That(driver.Url, Is.EqualTo("https://cart.payments.ebay.com/"));
            Assert.That(detailsPage.GetSelectedQuantittyInDropdown(), Is.EqualTo(desiredQty));
            string subtotalValueText = detailsPage.GetTotalValueText();

            var subtotalValueNum = Regex.Match(subtotalValueText, @"\d+\.\d+").Value.Replace(".", ",");
            var subtotalAmount = double.Parse(subtotalValueNum);

            Assert.That(itemPriceTotal, Is.EqualTo(subtotalAmount));
        }
    }
}
