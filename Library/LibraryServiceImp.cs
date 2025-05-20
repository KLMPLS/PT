using LibraryData.API;
using LibraryService.API;
using LibraryService.Implementations;
namespace LibraryService
{

    internal class LibraryServiceImp : ILibraryService, IDisposable
    {
        IDataStorage dataStorage;
        public LibraryServiceImp(IDataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }
        public LibraryServiceImp()
        {
            this.dataStorage = IDataStorage.GenerateStorage();
        }
        public override void AddBook(string title, string author, string genre)
        {
            dataStorage.AddBook(title, author, genre);
        }
        public override void RemoveBook(int id)
        {
            dataStorage.RemoveBook(id);
        }
        public override void AddCustomer(string name, string email)
        {
            dataStorage.AddCustomer(name, email);
        }
        public override void RemoveCustomer(int id)
        {
            dataStorage.RemoveCustomer(id);
        }
        public override void AddRecord(int customerId, int bookId, string type, DateTime a)
        {
            dataStorage.AddRecord(customerId, bookId, type, a);
        }
        public override void RemoveRecord(int id)
        {
            dataStorage.RemoveRecord(id);
        }
        public override void AddInventoryState(int bookId, int availableCopies)
        {
            dataStorage.AddInventoryState(bookId, availableCopies);
        }
        public override void RemoveInventoryState(int bookId)
        {
            dataStorage.RemoveInventoryState(bookId);
        }
        public override void BorrowBook(int bookId, int change)
        {
            dataStorage.UpdateInventoryState(bookId, -change);
        }
        public override void ReturnBook(int bookId, int change)
        {
            dataStorage.UpdateInventoryState(bookId, change);
        }
        public override List<IServiceCustomer> getAllCustomers()
        {
            var data = dataStorage.getAllCustomers();
            List<IServiceCustomer> customers = new List<IServiceCustomer>();
            foreach (var customer in data)
            {
                customers.Add(new ServiceCustomer(customer.Id, customer.Name, customer.Email));
            }
            return customers;
        }
        public override List<IServiceBook> getAllBooks()
        {
            var data = dataStorage.getAllBooks();
            List<IServiceBook> books = new List<IServiceBook>();
            foreach (var book in data)
            {
                books.Add(new ServiceBook(book.Id, book.Title, book.Author, book.Genre));
            }
            return books;
        }
        public override List<IServiceBookRecord> getAllBooksRecord()
        {
            var data = dataStorage.getAllBooksRecord();
            List<IServiceBookRecord> records = new List<IServiceBookRecord>();
            foreach (var record in data)
            {
                records.Add(new ServiceBookRecord(record.Id, record.customer_id, record.book_id, record.type, record.Date));
            }
            return records;
        }
        public override List<IServiceInventoryState> getAllInventoryStates()
        {
            var data = dataStorage.getAllInventoryStates();
            List<IServiceInventoryState> inventoryStates = new List<IServiceInventoryState>();
            foreach (var state in data)
            {
                inventoryStates.Add(new ServiceInventoryState(state.book_id, state.AvailableCopies));
            }
            return inventoryStates;
        }

        public void Dispose()
        {
            dataStorage?.Dispose();
        }
    }
}
