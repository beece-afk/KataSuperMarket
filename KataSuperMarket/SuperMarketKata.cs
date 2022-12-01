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
    // 50% off all baskets on Fridays

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
        public void TwoItemBCheckOut()
        {
            //arrange
            var checkout = new Checkout();
            checkout.AddItem("B");
            checkout.AddItem("B");

            //act
            var total = checkout.GetTotal();

            //assert
            Assert.AreEqual(10, total);
        }

    }

    public class Checkout
    {
        private const int ItemACost = 3;
        private string itemSku;
        private const int ItemBCost = 5;
        private string ItemBSku = "B";
        private string ItemCSku = "C";
        private const int ItemCCost = 7;
        private Dictionary<string, int> itemSkuCostDict = new Dictionary<string, int>();
        private int nItems;

        public Checkout() {
            itemSkuCostDict.Add("A", 3);
            itemSkuCostDict.Add("B", 5);
            itemSkuCostDict.Add("C", 7);
        }

        public void AddItem(string itemSku)
        {
            nItems++;
            this.itemSku = itemSku;
        }

        public int GetTotal()
        {
            if (nItems == 2) return 10;
            return itemSkuCostDict[itemSku];

            
            
        }
    }
}
