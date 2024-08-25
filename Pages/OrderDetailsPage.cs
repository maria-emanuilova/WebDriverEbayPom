using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverEbayPom.Pages
{
    public class OrderDetailsPage
    {
        private WebDriver driver;
        public OrderDetailsPage(WebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement ItemTitle => driver.FindElement(By.CssSelector("h1 > span"));
        public IWebElement ItemPrice => driver.FindElement(By.CssSelector("div.x-price-primary > span"));
        public IWebElement QtyTextBox => driver.FindElement(By.Id("qtyTextBox"));
        public IWebElement AddToCartButton => driver.FindElement(By.Id("atcBtn_btn_1"));
        public IWebElement OrderQuantityDropdown => driver.FindElement(By.XPath("(//select[@data-test-id='qty-dropdown'])[1]"));
        public IWebElement TotalAmountText => driver.FindElement(By.CssSelector("div.item-price.font-title-3 > span > span > span"));

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
