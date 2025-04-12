using LibraryData;
using LibraryLogic;
namespace LibraryLogicTests
{
    [TestClass]
    public class LibraryServiceTests
    {
        private Book CreateSampleBook() =>
            new Book("111", "Test Book", "Author", "Fiction", 3);

        private Customer CreateSampleUser() =>
            new Customer(1, "Alice", "alice@example.com");

        [TestMethod]
        public void AddBook_ShouldAddToCatalog_AndCreateState()
        {
            var storage = new InMemoryDataStorage();
            var service = new LibraryService(storage);
            var book = CreateSampleBook();

            service.AddBook(book);

            Assert.AreEqual(1, storage.Books.Count);
            Assert.AreEqual(1, storage.States.Count);
            Assert.AreEqual(book.TotalCopies, storage.States[0].AvailableCopies);
        }

        [TestMethod]
        public void BorrowBook_ShouldSucceed_AndReduceAvailability()
        {
            var context = new InMemoryDataStorage();
            var service = new LibraryService(context);
            var book = CreateSampleBook();
            var user = CreateSampleUser();
            context.Customers.Add(user);
            service.AddBook(book);

            var result = service.BorrowBook(book.Id, user.Id);

            Assert.IsTrue(result);
            Assert.AreEqual(2, context.States[0].AvailableCopies);
            Assert.AreEqual(1, context.Records.Count);
            Assert.AreEqual(BookRecordType.Borrowed, context.Records[0].Type);
        }

        [TestMethod]
        public void ReturnBook_ShouldSucceed_AndIncreaseAvailability()
        {
            var context = new InMemoryDataStorage();
            var service = new LibraryService(context);
            var book = CreateSampleBook();
            var user = CreateSampleUser();
            context.Customers.Add(user);
            service.AddBook(book);
            service.BorrowBook(book.Id, user.Id); // simulate borrowing

            var result = service.ReturnBook(book.Id, user.Id);

            Assert.IsTrue(result);
            Assert.AreEqual(3, context.States[0].AvailableCopies);
            Assert.AreEqual(2, context.Records.Count);
            Assert.AreEqual(BookRecordType.Returned, context.Records.Last().Type);
        }

        [TestMethod]
        public void BorrowBook_ShouldFail_WhenNoCopiesLeft()
        {
            var context = new InMemoryDataStorage();
            var service = new LibraryService(context);
            var book = CreateSampleBook();
            var user = CreateSampleUser();
            context.Customers.Add(user);
            service.AddBook(book);

            for (int i = 0; i < book.TotalCopies; i++)
                service.BorrowBook(book.Id, user.Id);

            var result = service.BorrowBook(book.Id, user.Id);

            Assert.IsFalse(result);
        }
    }
}
