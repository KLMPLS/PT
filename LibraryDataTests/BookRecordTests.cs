using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Objects;

namespace LibraryData.Tests
{
    [TestClass()]
    public class BookRecordTests
    {
        private Customer customer = new(2, "John Doe", "john.doe@gmail.com");
        private IBook book = new(1, "SampleTitle", "SampleAuthor", "SampleGenre");

        [TestMethod()]
        public void BookRecordTest() {
            BookRecord record = new BookRecord(3,customer, book, BookRecordType.Borrowed);
            Assert.AreEqual(record.Id, 3);
            Assert.AreEqual(record.Customer, customer);
            Assert.AreEqual(record.Book, book);
            Assert.AreEqual(record.Type, BookRecordType.Borrowed);
        }
    }
}