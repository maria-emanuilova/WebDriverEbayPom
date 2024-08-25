using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WebDriverEbayPom.Pages;

namespace WebDriverEbayPom.Tests
{
    [Binding]
    public class EbayPageTest
    {
        private WebDriver driver;
        private EbayPage page;
        private string firstItemPrice = "";
        private readonly string desiredQty = "2";
        private string itemPrice = "";
        private double itemPriceTotal;


        [Then(@"close browser")]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Given(@"user tries to navigate to Ebay url")]
        public void GivenUserTriesToNavigateToPage()
        {
        }

        [When(@"user loads the URL in the browser")]
        public void WhenUserLoadsThePage()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-search-engine-choice-screen");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            this.page = new EbayPage(driver);
            page.Open();
        }

        [Then(@"Ebay page is opened")]
        public void ThenEbayPageIsOpened()
        {
            Assert.That(page.IsPageOpen(), Is.True, "Page URL is not correct");
            Assert.That(page.GetPageTitle(), Is.EqualTo("Electronics, Cars, Fashion, Collectibles & More | eBay"));
        }

        [Given(@"user wants to search for an item in a category")]
        public void GivenUserSearchesForCategory()
        {
        }

        [When(@"user loads selects category and enters a word to search")]
        public void LoadSelectCategoryAndEnterSearchWord()
        {
            page.SelectCategory("Toys & Hobbies");
            page.SearchWord("Monopoly");
            page.ClickSearchButton();
        }

        [Then(@"relevant results are displayed with price and shipping information")]
        public void CheckSearchResults()
        {
            //Check that heading contains the search word "Monopoly"
            Assert.That(page.GetHeading1Text(), Does.Contain("Monopoly"), "First result heading doesn't contain the search word");
            Assert.That(page.GetFirstResultPrice(), Is.Not.Empty, "First item price is not displayed");
            Assert.That(page.GetFirstResultShippingLabel(), Is.Not.Empty, "Shipping info is not displayed for the first item");

            Assert.That(page.GetHeading2Text(), Does.Contain("Monopoly"), "Second result heading doesn't contain the search word");
            Assert.That(page.GetSecondtResultPrice(), Is.Not.Empty, "Second item price is not displayed");
            Assert.That(page.GetSecondResultShippingLabel(), Is.Not.Empty, "Shipping info is not displayed for the second item");

            Assert.That(page.GetHeading3Text(), Does.Contain("Monopoly"), "Third result heading doesn't contain the search word");
            Assert.That(page.GetThirdResultPrice(), Is.Not.Empty, "Third item price is not displayed");
            Assert.That(page.GetThirdResultShippingLabel(), Is.Not.Empty, "Shipping info is not displayed for the third item");
        }

        [Given(@"user wants to see the details on a searched item")]
        public void GivenUserWantsToSeeDetailsOnSearchedItem()
        {
        }


        [When(@"user clicks on the item search result")]
        public void WhenUserClicksOnItem()
        {
            page.ClickFirstSearchResult();
            firstItemPrice = page.GetFirstResultPrice();

            //Switch context to new tab opened:
            var detailsTab = driver.WindowHandles[1];
            this.driver.SwitchTo().Window(detailsTab);
        }

        [Then(@"the title of the item and the price are correctly displayed")]
        public void CheckTitleAndPriceOfItem()
        {
            Assert.That(page.GetItemTitleText(), Does.Contain("Monopoly"));
            itemPrice = page.GetItemPriceText();
            //test currently fails because item price is in AU dollars
            Assert.That(itemPrice, Is.EqualTo(firstItemPrice), "Item price doesn't match the one displayed on search results page");
        }

        [Given(@"user wants to add item to cart")]
        public void GivenUserAddsItemToCart()
        {
        }

        [When(@"user selects quantity and adds the item to cart")]
        public void WhenUserSelectsQtyAndAddsItemToCard()
        {
            itemPrice = page.GetItemPriceText();
            //Expression below takes only the numbers from the string and converts the . to , - depends on PC setting for decimal separator
            var itemPriceNum = Regex.Match(itemPrice, @"\d+\.\d+").Value.Replace(".", ",");
            //Price string is converted to number and multiplied by the quantity:
            itemPriceTotal = double.Parse(itemPriceNum) * int.Parse(desiredQty);

            page.FillItemQty(desiredQty);
            page.clickAddToCartButton();
        }

        [Then(@"user is redirected to the correct page")]
        public void ThenUserIsRedirectedToTheCorrectUrl()
        {
            Assert.That(driver.Url, Is.EqualTo("https://cart.payments.ebay.com/"));
        }

        [Then(@"the order quantity is correct")]
        public void ThenOrderQtyIsCorrect()
        {
            Assert.That(page.GetSelectedQuantittyInDropdown(), Is.EqualTo(desiredQty), "Quantity in order doesn't match the desired qty");
        }

        [Then(@"the price is correct")]
        public void ThenOrderPriceIsCorrect()
        {
            string subtotalValueText = page.GetTotalValueText();

            //Expression below takes only the numbers from the subtotal amount string
            //the . is converted to , - depends on PC setting for decimal separator
            var subtotalValueNum = Regex.Match(subtotalValueText, @"\d+\.\d+").Value.Replace(".", ",");
            //Subtotal string is converted to number
            var subtotalAmount = double.Parse(subtotalValueNum);

            Assert.That(itemPriceTotal, Is.EqualTo(subtotalAmount), "End price is not calculated correctly - check order qty and initial price");
        }
    }
}
