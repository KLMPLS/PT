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
    public class BookTests
    {
        private IBook book = new(1, "SampleTitle", "SampleAuthor", "SampleGenre");

        [TestMethod()]
        public void BookTest()
        {
            Assert.AreEqual(book.Id, 1);
            Assert.AreEqual(book.Title, "SampleTitle");
            Assert.AreEqual(book.Author, "SampleAuthor");
            Assert.AreEqual(book.Genre, "SampleGenre");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(book.ToString(), "SampleTitle by SampleAuthor [SampleGenre]");
        }
    }
}