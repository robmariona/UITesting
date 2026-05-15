using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using UITesting.Extensions;
using UITesting.Models;

namespace UITesting.Pages
{
    public class ProductPage : BasePage


    {
        public ProductPage(IWebDriver driver) : base(driver) { }



        private readonly By _productName = By.CssSelector("input[placeholder*='Name']");
        private readonly By _productDescription = By.CssSelector("input[placeholder*='Description']");
        private readonly By _productPrice = By.CssSelector("input[placeholder*='Price']");
        private readonly By _selectCategory = By.CssSelector("select[class*='w-full p-2 border rounded']");
        private readonly By _btnSaveProduct = By.XPath($"//button[contains(text(), 'Save Product')]");
        private readonly By _btnupdateProduct = By.XPath($"//button[contains(text(), 'Update Product')]");
        public void ClickEditForProduct(string productName)
        {
            // This string interpolation creates the custom XPath for the specific product
            string xpath = $"//td[contains(text(), '{productName}')]/..//button[contains(., 'Edit')]";

            Driver.WaitAndClick(By.XPath(xpath));
        }
        public void ClickDeleteForProduct(string productName)
        {
            // This string interpolation creates the custom XPath for the specific product
            string xpath = $"//td[contains(text(), '{productName}')]/..//button[contains(., 'Delete')]";

            Driver.WaitAndClick(By.XPath(xpath));
        }

        public void DeleteFirstDuplicate()
        {
            // 1. Get all product names from the first column (Product Name)
            // Adjust the XPath to match your table's structure
            var nameElements = Driver.FindElements(By.XPath("//table//tr/td[1]"));

            var seenNames = new HashSet<string>();
            string duplicateName = null;

            foreach (var element in nameElements)
            {
                string name = element.Text.Trim();

                // If Add returns false, it means the name was already in the set
                if (!seenNames.Add(name))
                {
                    duplicateName = name;
                    break; // Stop at the very first duplicate found
                }
            }

            if (duplicateName != null)
            {
                // 2. Locate the Delete button for this specific duplicate
                // This XPath targets the button in the same row as the duplicate name
                By deleteBtn = By.XPath($"//td[text()='{duplicateName}']/..//button[contains(., 'Delete')]");

                Driver.WaitAndClick(deleteBtn);

                // 3. Handle your Custom React Modal (from our previous conversation)
                // You'll need a locator for that "Yes, Delete Everything" button
                By confirmBtn = By.XPath("//button[contains(text(), 'Yes, Delete')]");
                Driver.WaitAndClick(confirmBtn);
            }
        }

        public void filloutproductForm(ProductModel product)
        {
            string priceText = product.Price.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            Driver.WaitAndType(_productName, product.Name);
            Driver.WaitAndType(_productDescription, product.Description);
            Driver.WaitAndType(_productPrice, priceText);
            Driver.WaitAndSelectByText(_selectCategory, product.Category);

        }


        public void NewProduct(ProductModel product)
        {


            filloutproductForm(product);
            Driver.WaitAndClick(_btnSaveProduct);
            
        }

        public void UpdateProduct(ProductModel productDataEdited, string originalName)


        {
            ClickEditForProduct(originalName);
            filloutproductForm(productDataEdited);
            Driver.WaitAndClick(_btnupdateProduct);

        }

        public bool IsProductVisibleInGrid(string productName)
        {
            // Dynamically locate the TD that contains your product name
            // Use XPath for text-based searching in tables—it's much more reliable
            By productRow = By.XPath($"//td[contains(text(), '{productName}')]");

            try
            {
                return Driver.WaitAndFind(productRow, 5).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

    }
}
