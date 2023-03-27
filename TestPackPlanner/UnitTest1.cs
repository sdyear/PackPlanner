using PackPlanner;
using static PackPlanner.Program;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestPackPlanner
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void VerifyNaturalSortOrder()
        {
            List<Item> items = new List<Item>
            {
                new Item(1001, 6200, 30, 9.653f),
                new Item(2001, 7200, 50, 11.21f)
            };
            List<List<Item>> result = PackPlanner.Program.CreatePacks(Program.SortOrder.NATURAL, 40, 500, items);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0].Count);
            Assert.AreEqual(1, result[1].Count);
            Assert.IsTrue(result[0][0].VerifyAttributes(1001, 6200, 30, 9.653f));
            Assert.IsTrue(result[0][1].VerifyAttributes(2001, 7200, 10, 11.21f));
            Assert.IsTrue(result[1][0].VerifyAttributes(2001, 7200, 40, 11.21f));
        }

        [TestMethod]
        public void VerifyShortLongSortOrder()
        {
            List<Item> items = new List<Item>
            {
                new Item(1001, 6200, 30, 9.653f),
                new Item(2001, 7200, 50, 11.21f)
            };
            List<List<Item>> result = PackPlanner.Program.CreatePacks(Program.SortOrder.SHORT_TO_LONG, 40, 500, items);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0].Count);
            Assert.AreEqual(1, result[1].Count);
            Assert.IsTrue(result[0][0].VerifyAttributes(1001, 6200, 30, 9.653f));
            Assert.IsTrue(result[0][1].VerifyAttributes(2001, 7200, 10, 11.21f));
            Assert.IsTrue(result[1][0].VerifyAttributes(2001, 7200, 40, 11.21f));
        }

        [TestMethod]
        public void VerifyLongShortSortOrder()
        {
            List<Item> items = new List<Item>
            {
                new Item(1001, 6200, 30, 9.653f),
                new Item(2001, 7200, 50, 11.21f)
            };
            List<List<Item>> result = PackPlanner.Program.CreatePacks(Program.SortOrder.LONG_TO_SHORT, 40, 500, items);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Count);
            Assert.AreEqual(2, result[1].Count);
            Assert.IsTrue(result[0][0].VerifyAttributes(2001, 7200, 40, 11.21f));
            Assert.IsTrue(result[1][0].VerifyAttributes(2001, 7200, 10, 11.21f));
            Assert.IsTrue(result[1][1].VerifyAttributes(1001, 6200, 30, 9.653f));
        }

        [TestMethod]
        public void VerifySingleEmptyItem()
        {
            List<Item> items = new List<Item>
            {
                new Item(1001, 6200, 0, 9.653f)
            };
            List<List<Item>> result = PackPlanner.Program.CreatePacks(Program.SortOrder.NATURAL, 40, 500, items);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void VerifyStartsWithEmptyItem()
        {
            List<Item> items = new List<Item>
            {
                new Item(1001, 6200, 0, 9.653f),
                new Item(2001, 7200, 50, 11.21f)
            };
            List<List<Item>> result = PackPlanner.Program.CreatePacks(Program.SortOrder.NATURAL, 40, 500, items);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Count);
            Assert.AreEqual(1, result[1].Count);
            Assert.IsTrue(result[0][0].VerifyAttributes(2001, 7200, 40, 11.21f));
            Assert.IsTrue(result[1][0].VerifyAttributes(2001, 7200, 10, 11.21f));
        }

        [TestMethod]
        public void VerifyEndWithEmptyItem()
        {
            List<Item> items = new List<Item>
            {
                new Item(1001, 6200, 30, 9.653f),
                new Item(2001, 7200, 0, 11.21f)
            };
            List<List<Item>> result = PackPlanner.Program.CreatePacks(Program.SortOrder.NATURAL, 40, 500, items);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Count);
            Assert.IsTrue(result[0][0].VerifyAttributes(1001, 6200, 30, 9.653f));
        }

        [TestMethod]
        public void VerifyMiddleEmptyItem()
        {
            List<Item> items = new List<Item>
            {
                new Item(1001, 6200, 30, 9.653f),
                new Item(2001, 6200, 0, 9.653f),
                new Item(3001, 7200, 50, 11.21f)
            };
            List<List<Item>> result = PackPlanner.Program.CreatePacks(Program.SortOrder.NATURAL, 40, 500, items);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0].Count);
            Assert.AreEqual(1, result[1].Count);
            Assert.IsTrue(result[0][0].VerifyAttributes(1001, 6200, 30, 9.653f));
            Assert.IsTrue(result[0][1].VerifyAttributes(3001, 7200, 10, 11.21f));
            Assert.IsTrue(result[1][0].VerifyAttributes(3001, 7200, 40, 11.21f));
        }
    }
}