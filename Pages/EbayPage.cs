using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverEbayPom.Pages
{
    public class EbayPage
    {
        private readonly WebDriver driver;
        private const string baseUrl = "https://www.ebay.com/";

        public EbayPage(WebDriver driver)
        {
            this.driver = driver;
        }

        //public IWebElement categoryDropdown { get { return driver.FindElement(By.Id("gh-cat")); } }
        public IWebElement CategoryDropdown => driver.FindElement(By.Id("gh-cat"));
        public IWebElement SearchField => driver.FindElement(By.Id("gh-ac"));
        public IWebElement SearchButton => driver.FindElement(By.Id("gh-btn"));
        public IWebElement FirstSearchResult => driver.FindElement(By.XPath("(//span[@role='heading'])[3]"));
        public IWebElement SecondSearchResult => driver.FindElement(By.XPath("(//span[@role='heading'])[4]"));
        public IWebElement ThirdSearchResult => driver.FindElement(By.XPath("(//span[@role='heading'])[5]"));
        public IWebElement FirstResultPrice => driver.FindElement(By.XPath("(//span[@class='s-item__price'])[3]"));
        public IWebElement SecondResultPrice => driver.FindElement(By.XPath("(//span[@class='s-item__price'])[4]"));
        public IWebElement ThirdResultPrice => driver.FindElement(By.XPath("(//span[@class='s-item__price'])[5]"));
        public IWebElement FirstResultShippingLabel => driver.FindElement(By.XPath("(//span[@class='s-item__shipping s-item__logisticsCost'])[1]"));
        public IWebElement SecondResultShippingLabel => driver.FindElement(By.XPath("(//span[@class='s-item__shipping s-item__logisticsCost'])[2]"));
        public IWebElement ThirdtResultShippingLabel => driver.FindElement(By.XPath("(//span[@class='s-item__shipping s-item__logisticsCost'])[3]"));
        public IWebElement ItemTitle => driver.FindElement(By.CssSelector("h1 > span"));
        public IWebElement ItemPrice => driver.FindElement(By.CssSelector("div.x-price-primary > span"));
        public IWebElement QtyTextBox => driver.FindElement(By.Id("qtyTextBox"));
        public IWebElement AddToCartButton => driver.FindElement(By.Id("atcBtn_btn_1"));
        public IWebElement OrderQuantityDropdown => driver.FindElement(By.XPath("(//select[@data-test-id='qty-dropdown'])[1]"));
        public IWebElement TotalAmountText => driver.FindElement(By.CssSelector("div.item-price.font-title-3 > span > span > span"));


        public void Open()
        {
            driver.Navigate().GoToUrl(baseUrl);
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public bool IsPageOpen()
        {
            return driver.Url == baseUrl;
        }

        public void SearchWord(string searchWord)
        {
            SearchField.SendKeys(searchWord);
        }

        public void SelectCategory(string optionToSelect)
        {
            var selectElement = new SelectElement(CategoryDropdown);
            selectElement.SelectByText(optionToSelect);
        }

        public void ClickSearchButton()
        {
            SearchButton.Click();
        }

        public string GetHeading1Text()
        {
            return FirstSearchResult.Text;
        }

        public string GetHeading2Text()
        {
            return SecondSearchResult.Text;
        }

        public string GetHeading3Text()
        {
            return ThirdSearchResult.Text;
        }

        public string GetFirstResultPrice()
        {
            return FirstResultPrice.Text;
        }

        public string GetSecondtResultPrice()
        {
            return SecondResultPrice.Text;
        }

        public string GetThirdResultPrice()
        {
            return ThirdResultPrice.Text;
        }

        public string GetFirstResultShippingLabel()
        {
            return FirstResultShippingLabel.Text;
        }

        public string GetSecondResultShippingLabel()
        {
            return SecondResultShippingLabel.Text;
        }

        public string GetThirdResultShippingLabel()
        {
            return ThirdtResultShippingLabel.Text;
        }

        public void ClickFirstSearchResult()
        {
            FirstSearchResult.Click();
        }

        public string GetItemTitleText()
        {
            return ItemTitle.Text;
        }

        public string GetItemPriceText()
        {
            return ItemPrice.Text;
        }

        public void FillItemQty(string quantity)
        {
            QtyTextBox.Clear();
            QtyTextBox.SendKeys(quantity);
        }

        public void clickAddToCartButton()
        {
            AddToCartButton.Click();
        }

        public string GetSelectedQuantittyInDropdown()
        {
            var selectElement = new SelectElement(OrderQuantityDropdown);
            var selectedQty = selectElement.SelectedOption;
            return selectedQty.Text;
        }

        public string GetTotalValueText()
        {
            return TotalAmountText.Text;
        }
    }
}
