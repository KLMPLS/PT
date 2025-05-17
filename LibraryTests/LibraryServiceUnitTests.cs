using LibraryData.API;
using LibraryService.API;
using LibraryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServiceTest
{
    [TestClass]
    public class LibraryServiceUnitTests
    {
        private ILibraryService service;

        [TestInitialize]
        public void Setup()
        {
            IDataStorage mockStorage = new InMemoryDataStorage(); 
            service = new LibraryServiceImp(mockStorage);
        }

        [TestMethod]
        public void BookTests()
        {
            service.AddBook("TestBook_Unique123", "Test Author", "Mystery");

            var book = service.getAllBooks()
                .FirstOrDefault(b => b.Title == "TestBook_Unique123" && b.Author == "Test Author");
            Assert.IsNotNull(book);
            Assert.AreEqual("Mystery", book.Genre);

            service.RemoveBook(book.Id);
            Assert.IsFalse(service.getAllBooks().Any(b => b.Id == book.Id));
        }

        [TestMethod]
        public void CustomerTests()
        {
            service.AddCustomer("TestCustomer_Unique123", "test@domain.com");

            var customer = service.getAllCustomers()
                .FirstOrDefault(c => c.Name == "TestCustomer_Unique123" && c.Email == "test@domain.com");
            Assert.IsNotNull(customer);

            service.RemoveCustomer(customer.Id);
            Assert.IsFalse(service.getAllCustomers().Any(c => c.Id == customer.Id));
        }

        [TestMethod]
        public void RecordTests()
        {
            service.AddCustomer("RecordUser", "record@domain.com");
            service.AddBook("RecordBook", "Record Author", "Sci-Fi");

            var customer = service.getAllCustomers().First(c => c.Name == "RecordUser");
            var book = service.getAllBooks().First(b => b.Title == "RecordBook");

            service.AddRecord(customer.Id, book.Id, "borrow", DateTime.Today);

            var record = service.getAllBooksRecord()
                .FirstOrDefault(r => r.customer_id == customer.Id && r.book_id == book.Id && r.type == "borrow");
            Assert.IsNotNull(record);

            service.RemoveRecord(record.Id);
            Assert.IsFalse(service.getAllBooksRecord().Any(r => r.Id == record.Id));
        }

        [TestMethod]
        public void InventoryStateTests()
        {
            service.AddBook("InventoryBook_Unique", "Inv Author", "Adventure");
            var book = service.getAllBooks().First(b => b.Title == "InventoryBook_Unique");

            service.AddInventoryState(book.Id, 6);
            var state = service.getAllInventoryStates().FirstOrDefault(s => s.book_id == book.Id);
            Assert.IsNotNull(state);
            Assert.AreEqual(6, state.AvailableCopies);

            service.BorrowBook(book.Id, 2);
            var afterBorrow = service.getAllInventoryStates().First(s => s.book_id == book.Id);
            Assert.AreEqual(4, afterBorrow.AvailableCopies);

            service.ReturnBook(book.Id, 3);
            var afterReturn = service.getAllInventoryStates().First(s => s.book_id == book.Id);
            Assert.AreEqual(7, afterReturn.AvailableCopies);

            service.RemoveInventoryState(book.Id);
            Assert.IsFalse(service.getAllInventoryStates().Any(s => s.book_id == book.Id));
        }
    }
}
