using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITesting.Base;
using UITesting.Models;
using UITesting.Pages;

namespace UITesting.Tests
{
    //[Collection("Sequential Tests")]
    public class ProductDeletionTest : TestBase
    {

        private readonly DashboardPage _dashboard;
        private readonly LoginPage _loginPage;
        private readonly ProductPage _productPage;

        public ProductDeletionTest() {
            _loginPage = new LoginPage(Driver);
            _dashboard = new DashboardPage(Driver);
            _productPage = new ProductPage(Driver);
        }

        [Fact]
        public void DeleteDuplicates()
        {

            string uniqueName = "Technics 1200 " + Guid.NewGuid().ToString().Substring(0, 5);

            // Arrange: Create a product that we know already exists
            var duplicateData = new ProductModel {
                Name = uniqueName, // Keep names consistent!
                Description = "High-end Turntable",
                Price = 1200.00m,
                Category = "General"
            };

            _dashboard.ClickDashboard();


            _dashboard.ClickProductTab();
            _productPage.NewProduct(duplicateData); // Now there are two!

            // Act: Find and delete the first duplicate
            _productPage.DeleteFirstDuplicate();

            // Assert: Verify the duplicate is gone (wait for UI to refresh)
            // You might want to check the count of rows or verify the success toast


        }


    }
}
