using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryData.API;

namespace LibraryData.Tests
{
    [TestClass()]
    public class InventoryStateTests
    {
        private IBook book = new(1, "SampleTitle", "SampleAuthor", "SampleGenre");

        [TestMethod()]
        public void InventoryStateTest()
        {
            InventoryState state = new(book,5);
            Assert.AreEqual(state.Book, book);
            Assert.AreEqual(state.AvailableCopies, 5);
        }
    }
}