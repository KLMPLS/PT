using LibraryLogic;
using LibraryData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryLogicTests
{
    [TestClass]
    public class LibraryServiceTests
    {
        public InMemoryDataStorage init()
        {
            InMemoryDataStorage dataStorage = new InMemoryDataStorage();
            dataStorage.AddBook("Test Book", "Test Author", "Test Genre");
            dataStorage.AddInventoryState(1, 5);
            dataStorage.AddCustomer("Kutin", "nullKutin@email.com");
            return dataStorage;
        }

        [TestMethod()]
        public void BorrowBookTest()
        {
            InMemoryDataStorage dataStorage = init();
            LibraryService libraryService = new(dataStorage);

            libraryService.BorrowBook(1, 1);

            Assert.AreEqual(4, dataStorage.States[0].AvailableCopies);
            Assert.AreEqual(1, dataStorage.Records.Count);
            Assert.AreEqual(BookRecordType.Borrowed, dataStorage.Records[0].Type);
        }

        [TestMethod()]
        public void ReturnBookTest()
        {
            InMemoryDataStorage dataStorage = init();
            LibraryService libraryService = new(dataStorage);

            libraryService.ReturnBook(1, 1);

            Assert.AreEqual(6, dataStorage.States[0].AvailableCopies);
            Assert.AreEqual(1, dataStorage.Records.Count);
            Assert.AreEqual(BookRecordType.Returned, dataStorage.Records[0].Type);
        }

        [TestMethod()]
        public void AddtoInventoryTest()
        {
            InMemoryDataStorage dataStorage = init();
            LibraryService libraryService = new(dataStorage);

            libraryService.AddtoInventory(1, 3);

            Assert.AreEqual(8, dataStorage.States[0].AvailableCopies);
        }

        [TestMethod()]
        public void RemoveFromInventoryTest()
        {
            InMemoryDataStorage dataStorage = init();
            LibraryService libraryService = new(dataStorage);

            libraryService.RemoveFromInventory(1, 2);

            Assert.AreEqual(3, dataStorage.States[0].AvailableCopies);
        }
    }
}
