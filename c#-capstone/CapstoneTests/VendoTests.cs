using System;
using System.Collections.Generic;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class VendoTests
    {
        [TestMethod]
        public void AcceptCashTest()
        {
            //expected pass
            Transaction acceptCash = new Transaction();
            acceptCash.AcceptCash("5.00");
            decimal insertedCash = acceptCash.TotalMoneyInserted;

            Assert.AreEqual(5.00m, insertedCash, "Input: $5");

            //Cant insert $8.00 bill, Total inserted cash still 5.00
            acceptCash.AcceptCash("8.00");
            insertedCash = acceptCash.TotalMoneyInserted;
            Assert.AreEqual(5.00m, insertedCash, "Input: $8");

            //Cant insert coins
            acceptCash.AcceptCash(".50");
            insertedCash = acceptCash.TotalMoneyInserted;
            Assert.AreEqual(5.00m, insertedCash, "Input: $0.50");

            //$2.00 bills are acceptable
            acceptCash.AcceptCash("2");
            insertedCash = acceptCash.TotalMoneyInserted;
            Assert.AreEqual(7.00m, insertedCash, "Input: $2.00");
        }

        [TestMethod]
        public void GetChangeTest()
        {
            Transaction transaction = new Transaction();
            StockService productStock = new StockService();
            List<Product> products = productStock.Products;

            //Put $5.00 w/ no purchase should return 5$
            transaction.AcceptCash("5.00");
            decimal customerChange = transaction.ReturnChange();
            Assert.AreEqual(5.00m, customerChange, "Input: inserted $5 w/ no purchase");


            //Inserted $2 with a $1.45 purchase
            transaction.AcceptCash("2.00");
            transaction.SelectItem("A2", products);
            customerChange = transaction.ReturnChange();
            Assert.AreEqual(0.55m, customerChange, "Input: inserted $2 w/ $1.45 purchase");

            //Inserted 10.00 with a $1.50 & $3.65 purchase
            transaction.AcceptCash("10.00");
            transaction.SelectItem("B2", products);
            transaction.SelectItem("A4", products);
            customerChange = transaction.ReturnChange();
            Assert.AreEqual(4.85m, customerChange, "Input: inserted $10 w/ $1.50 & $3.65 purchase");

        }

        [TestMethod]
        public void SelectItemTest()
        {
            Transaction transaction = new Transaction();
            StockService stock = new StockService();
            List<Product> products = stock.Products;

            // Choose an item and check cart list w
            transaction.AcceptCash("10");
            transaction.SelectItem("A1", products);
            Dictionary<string, int> selectedItems = transaction.AllItemsPurchased;
            Assert.AreEqual(true, selectedItems.ContainsKey("Potato Crisps"));

            transaction.SelectItem("C2", products);
            selectedItems = transaction.AllItemsPurchased;
            Assert.AreEqual(true, selectedItems.ContainsKey("Dr. Salt"));

            transaction.SelectItem("B3", products);
            selectedItems = transaction.AllItemsPurchased;
            Assert.AreEqual(true, selectedItems.ContainsKey("Wonka Bar"));

        }

        [TestMethod]
        public void SoldOutTest()
        {
            Transaction transaction = new Transaction();
            StockService stock = new StockService();
            List<Product> products = stock.Products;
            var chiclet = products.Find(p => p.Slot == "D3");

            //Insert $10 & buy a $0.75 Chiclet 6x
            //Should return SOLD OUT after the fifth purchase
            //Checking "SoldOut" Prop

            transaction.AcceptCash("10");
            //1
            transaction.SelectItem("D3", products);
            Assert.AreEqual(false, chiclet.SoldOut);
            //2
            transaction.SelectItem("D3", products);
            Assert.AreEqual(false, chiclet.SoldOut);
            //3
            transaction.SelectItem("D3", products);
            Assert.AreEqual(false, chiclet.SoldOut);
            //4
            transaction.SelectItem("D3", products);
            Assert.AreEqual(false, chiclet.SoldOut);
            //5.... SOLD OUT
            transaction.SelectItem("D3", products);
            Assert.AreEqual(true, chiclet.SoldOut);
        }

    }
}
 