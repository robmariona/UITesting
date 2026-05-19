using System;
using OpenQA.Selenium;
using UITesting.Base;
using UITesting.Models;
using UITesting.Pages;
using Xunit; // Ensure you have Xunit for [Fact]

namespace UITesting.Tests
{
    [Collection("Sequential Tests")]
    public class ProductTest : TestBase
    {
        private readonly DashboardPage _dashboard;
        private readonly LoginPage _loginPage;
        private readonly ProductPage _productPage;

        public ProductTest()
        {
            _loginPage = new LoginPage(Driver);
            _dashboard = new DashboardPage(Driver);
            _productPage = new ProductPage(Driver);
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void ProductIsVisible_AfterCreation()
        {
            // 1. Arrange - Group your data into the Model
            string uniqueName = "Technics 1200 " + Guid.NewGuid().ToString().Substring(0, 5);
            var productData = new ProductModel
            {
                Name = uniqueName, // Keep names consistent!
                Description = "High-end Turntable",
                Price = 1200.00m,
                Category = "General"
            };

            // 2. Act
            _dashboard.ClickDashboard();
            _dashboard.ClickProductTab();

            // Pass the whole object to your new method
            _productPage.NewProduct(productData);

            // 3. Assert
            // Use productData.Name instead of hardcoding strings again
            bool isVisible = _productPage.IsProductVisibleInGrid(productData.Name);

            Assert.True(isVisible, $"Product '{productData.Name}' was not found in the grid.");

           // _loginPage.LogoutUser();
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ProductIsVisible_AfterUpdate()
        {


            // 1. Arrange - Group your data into the Model

            string originalName = "Technics 1200 MK7"; // What is on the screen now
            var productDataEdited = new ProductModel
            {
                Name = originalName + " Edit", // Keep names consistent!
                Description = "High-end Turntable edit",
                Price = 1400.00m,
                Category = "Property"
            };

            // 2. Act
            _dashboard.ClickDashboard();
            _dashboard.ClickProductTab();

            // Pass the whole object to your new method
            _productPage.UpdateProduct(productDataEdited,originalName);

            // 3. Assert
            // Use productData.Name instead of hardcoding strings again
            bool isVisible = _productPage.IsProductVisibleInGrid(productDataEdited.Name);

            Assert.True(isVisible, $"Product '{productDataEdited.Name}' was not found in the grid.");

            //_loginPage.LogoutUser();
        }
    }
}