using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShop.Models;

namespace WebShopTest
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void GetPriceWithTaxTest()
        {
            //Arange
            Product product = new Product()
            {
                Price = 100
            };

            //Act
            decimal result = product.GetPriceWithTax(20);

            //Assert
            Assert.AreEqual(120, result);
        }
    }
}
