using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Tests
{
    [TestClass()]
    public class InventoryStateTests
    {
        private Book book = new("1", "SampleTitle", "SampleAuthor", "SampleGenre", 5);

        [TestMethod()]
        public void InventoryStateTest()
        {
            InventoryState state = new(book);
            Assert.AreEqual(state.Book, book);
            Assert.AreEqual(state.AvailableCopies, book.TotalCopies);
        }
    }
}