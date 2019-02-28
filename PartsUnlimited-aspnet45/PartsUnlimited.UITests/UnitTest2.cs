using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System.Linq;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;

namespace Partsunlimited.UITests
{
    [TestClass]
   // [Ignore]
    public class PartsUnlimitedTests
    {
        static IWebDriver driverC;
        //static IWebDriver driverFF;

        [AssemblyInitialize]
        public static void Setup(TestContext context)
        {
            //driverC = new ChromeDriver();

            //in hosted agents 2
            driverC = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"));

            //driverC = new PhantomJSDriver();
            


        }
        [AssemblyCleanup]
        public static void TearDown()
        {
            if (driverC != null)
                driverC.Quit();
        }

       


        [TestMethod]
        [TestCategory("Selenium")]
        public void TestDriver()
        {
            driverC.Navigate().GoToUrl("http://partsunlimited.azurewebsites.net/");

        }

        //[AssemblyCleanup]
        //public static void Cleanup()
        //{
        //    driver.Quit();
        //}

        [TestMethod]
        [TestCategory("Selenium")]
        public void TestShoppingCart()
        {
            var homeUrl = "http://partunlimitedunai-dev.azurewebsites.net";

            //go to cart
            driverC.Navigate().GoToUrl($"{homeUrl}/ShoppingCart");

            // check that the cart is empty
            var container = driverC.FindElement(By.Id("shopping-cart-page"));
            Assert.AreEqual("Review your Cart", container.FindElement(By.TagName("h2")).Text);
            var empty = container.FindElement(By.Id("empty-cart"));
            Assert.IsNotNull(empty);

            // go to the first category
            driverC.Navigate().GoToUrl($"{homeUrl}/Store/Browse?categoryId=1");
            // find the 1st element
            var item = driverC.FindElements(By.ClassName("list-item-part")).First();
            var itemName = item.FindElement(By.TagName("h4")).Text;
            var price = item.FindElement(By.TagName("h5")).Text;
            // naviate to the item
            item.FindElement(By.TagName("a")).Click();

            // add it to the cart
            driverC.FindElement(By.ClassName("btn")).Click();

            ////// check the contents of the cart
            ////var cartContainer = driverC.FindElement(By.Id("shopping-cart-page"));
            ////Assert.AreEqual("Review your Cart", cartContainer.FindElement(By.TagName("h2")).Text);
            ////var cartItems = driverC.FindElements(By.ClassName("cart-item"));
            ////Assert.AreEqual(1, cartItems.Count);
            ////var cartItem = cartItems.First();
            ////Assert.IsTrue(cartItem.FindElements(By.TagName("a")).Any(e => e.Text == itemName));
            ////Assert.AreEqual(price, cartItem.FindElement(By.ClassName("item-price")).Text);

            ////Assert.AreEqual(price, cartContainer.FindElement(By.Id("cart-sub-total")).Text);
        }
         

    }
}
