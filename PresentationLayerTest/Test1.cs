using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationLayer.Model;
using PresentationLayer.Model.API;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayerTest
{
    [TestClass]
    public class ModelServiceUnitTests
    {
        private ModelService model;

        [TestInitialize]
        public void Setup()
        {
            var mockStorage = new InMemoryDataStorage(); 
            var fakeService = new FakeLibraryServiceImp(mockStorage);
            model = new ModelService(fakeService);
        }

        [TestMethod]
        public async Task BookTests()
        {
            await model.AddBookAsync("TestBook_Unique123", "Test Author", "Mystery");

            var books = await model.GetAllBooksAsync();
            var book = books.FirstOrDefault(b => b.Title == "TestBook_Unique123" && b.Author == "Test Author");
            Assert.IsNotNull(book);
            Assert.AreEqual("Mystery", book.Genre);

            await model.RemoveBookAsync(book.Id);
            books = await model.GetAllBooksAsync();
            Assert.IsFalse(books.Any(b => b.Id == book.Id));
        }

        [TestMethod]
        public async Task CustomerTests()
        {
            await model.AddCustomerAsync("TestCustomer_Unique123", "test@domain.com");

            var customers = await model.GetAllCustomersAsync();
            var customer = customers.FirstOrDefault(c => c.Name == "TestCustomer_Unique123" && c.Email == "test@domain.com");
            Assert.IsNotNull(customer);

            await model.RemoveCustomerAsync(customer.Id);
            customers = await model.GetAllCustomersAsync();
            Assert.IsFalse(customers.Any(c => c.Id == customer.Id));
        }

        [TestMethod]
        public async Task RecordTests()
        {
            await model.AddCustomerAsync("RecordUser", "record@domain.com");
            await model.AddBookAsync("RecordBook", "Record Author", "Sci-Fi");

            var customers = await model.GetAllCustomersAsync();
            var books = await model.GetAllBooksAsync();
            var customer = customers.First(c => c.Name == "RecordUser");
            var book = books.First(b => b.Title == "RecordBook");

            await model.AddRecordAsync(customer.Id, book.Id, "borrow", DateTime.Today);

            var records = await model.GetAllBooksRecordAsync();
            var record = records.FirstOrDefault(r => r.CustomerId == customer.Id && r.BookId == book.Id && r.Type == "borrow");
            Assert.IsNotNull(record);

            await model.RemoveRecordAsync(record.Id);
            records = await model.GetAllBooksRecordAsync();
            Assert.IsFalse(records.Any(r => r.Id == record.Id));
        }

        [TestMethod]
        public async Task InventoryStateTests()
        {
            await model.AddBookAsync("InventoryBook_Unique", "Inv Author", "Adventure");
            var books = await model.GetAllBooksAsync();
            var book = books.First(b => b.Title == "InventoryBook_Unique");

            await model.AddInventoryStateAsync(book.Id, 6);
            var states = await model.GetAllInventoryStatesAsync();
            var state = states.FirstOrDefault(s => s.Id == book.Id);
            Assert.IsNotNull(state);
            Assert.AreEqual(6, state.Available);

            await model.BorrowBookAsync(book.Id, 2);
            states = await model.GetAllInventoryStatesAsync();
            var afterBorrow = states.First(s => s.Id == book.Id);
            Assert.AreEqual(4, afterBorrow.Available);

            await model.ReturnBookAsync(book.Id, 3);
            states = await model.GetAllInventoryStatesAsync();
            var afterReturn = states.First(s => s.Id == book.Id);
            Assert.AreEqual(7, afterReturn.Available);

            await model.RemoveInventoryStateAsync(book.Id);
            states = await model.GetAllInventoryStatesAsync();
            Assert.IsFalse(states.Any(s => s.Id == book.Id));
        }
    }
}
