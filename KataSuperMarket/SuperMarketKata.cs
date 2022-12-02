using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KataSuperMarket
{
    // The customer has requested a supermarket checkout system
    // Used to calculate basket totals
    // The following specs apply:
    //
    // Item A cost 3
    // Item B cost 5
    // Item C cost 7
    //
    // Special discounts:
    // 2 x A for 5
    // 3 x C for 18
    //
    // 50% off all baskets on Fridays in addition to existing discounts!!! WOHOO!

    [TestClass]
    public class SuperMarketKata
    {
        [TestMethod]
        [DataRow ("A", 3, DisplayName = "Item A Costs 3")]
        [DataRow("B", 5, DisplayName = "Item B Costs 5")]
        [DataRow("C", 7, DisplayName = "Item C Costs 7")]
        public void SingleItemCheckOut(string itemSku, int cost)
        {
            //arrange
            var checkout = new Checkout();
            checkout.AddItem(itemSku);

            //act
            var total = checkout.GetTotal();

            //assert
            Assert.AreEqual(cost, total);
        }

        [TestMethod]
        [DataRow("B", 10, DisplayName ="Two item B's cost 10")]
        [DataRow("C", 14, DisplayName = "Two item C's cost 14")]
        public void TwoItemCheckOut(string itemSku, int cost)
        {
            //arrange
            var checkout = new Checkout();
            checkout.AddItem(itemSku);
            checkout.AddItem(itemSku);

            //act
            var total = checkout.GetTotal();

            //assert
            Assert.AreEqual(cost, total);
        }

        [TestMethod]
        [DataRow("B",2,5, DisplayName = "Two Item Bs at half off")]
        [DataRow("C", 1, 3.5, DisplayName = "One Item C at half off")]
        public void FridayHalfOffDiscountCheck(string itemSku, int nitems,double expectedtotal)
        {
            //arrange
            var checkout = new Checkout();
            for(int i = 0; i < nitems; i++)
            {
                checkout.AddItem(itemSku);
            }

            //act
            checkout.SetCheckoutDay("Friday");
            var total = checkout.GetTotal();

            //assert
            Assert.AreEqual(expectedtotal,total);
        }

        [TestMethod]
        public void FridayHalfOffDiscountCheckEqualFraction()
        {
            //arrange
            var checkout = new Checkout();
            checkout.AddItem("C");

            //act
            checkout.SetCheckoutDay("Friday");
            var total = checkout.GetTotal();

            //assert
            Assert.AreEqual(3.5, total);
        }

    }

    public class Checkout
    {
        private Dictionary<string, int> itemSkuCostDict = new Dictionary<string, int>();
        private double total;
        private string day;
        private const string discountDay = "Friday";

        public Checkout() {
            itemSkuCostDict.Add("A", 3);
            itemSkuCostDict.Add("B", 5);
            itemSkuCostDict.Add("C", 7);
        }

        public void SetCheckoutDay(string day) 
        {
            this.day = day;
        }

        public void AddItem(string itemSku)
        {
            total += itemSkuCostDict[itemSku];
        }

        public double GetTotal()
        {
            if (this.day == discountDay) 
            {
                return total/2;
            }
            return total;
        }
    }
}
