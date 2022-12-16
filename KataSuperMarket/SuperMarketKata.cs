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
        [DataRow("A", 2, 5, DisplayName = "Two Item As for 5")]
        [DataRow("A", 3, 8, DisplayName = "Three Item A for 8")]
        [DataRow("C", 3, 18,DisplayName ="Three Item C for 18")]
        [DataRow("C", 4, 25, DisplayName = "Four Item C for 25")]
        public void DiscountsOnMultipleItemAAndCApply(string itemSku, int nitems, double expectedtotal)
        {
            //arrange
            var checkout = new Checkout();
            for (int i = 0; i < nitems; i++)
            {
                checkout.AddItem(itemSku);
            }

            //act
            var total = checkout.GetTotal();

            //assert
            Assert.AreEqual(expectedtotal, total);
        }

        [TestMethod]
        public void MultipleItemsOfDifferentSkus()
        {
            //arrange
            var checkout = new Checkout();
            checkout.AddItem("A");
            checkout.AddItem("B");
            checkout.AddItem("C");
            checkout.AddItem("A");


            //act
            var total = checkout.GetTotal();

            //assert
            Assert.AreEqual(17, total);
        }

    }

    public class Checkout
    {
        private Dictionary<string, int> itemSkuCostDict = new Dictionary<string, int>();
        private double total;
        private string day;
        private const string discountDay = "Friday";
        private int itemAcount = 0;
        private string itemASku = "A";
        private string itemCSku  = "C";
        private int itemCcount=0;

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
            if(itemSku == itemASku)
            {
                itemAcount++;
            }
            else if (itemSku == itemCSku)
            {
                itemCcount++;
            }

            total += itemSkuCostDict[itemSku];
        }

        public double GetTotal()
        {
            this.CheckForDiscounts();
            return total;
        }

        public void CheckForDiscounts()
        {
            if (itemAcount > 1)
            {
                total -= (itemAcount / 2);
            }

            if (itemCcount > 1)
            {
                total -= ((itemCcount / 3) * 3);
            }

            if (this.day == discountDay)
            {
                total /= 2;
            }
        }
    }
}
