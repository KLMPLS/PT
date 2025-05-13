using LibraryData.Objects.Task1data;
using LibraryData.Objects;

namespace LibraryData
{
     internal class InMemoryDataStorage : IDataStorage
    {
        internal List<Book> Books { get; } = new();
        internal List<Customer> Customers { get; } = new();
        internal List<BookRecord> Records { get; } = new();
        internal List<InventoryState> States { get; } = new();
        public override void AddBook(string title, string author, string genre) { 
            Book book = new Book(Books.Count + 1, title, author, genre);
            Books.Add(book);
        }
        public override void AddCustomer(string name, string email)
        {
            Customers.Add(new Customer(Customers.Count + 1, name, email));
        }
        public override void AddRecord(int customerId, int bookId, int typeid)
        {
            var customer = FindCustomer(customerId);
            var book = FindBook(bookId);
            if (customer != null && book != null && ((AmmoutLeft(bookId) != 0) || (typeid == 2)))
            {
                Records.Add(new BookRecord(Records.Count + 1, customer, book, GetBookRecordType(typeid)));
            }
        }
        public override void AddInventoryState(int bookId, int availableCopies)
        {
            var book = FindBook(bookId);
            if (book != null)
            {
                States.Add(new InventoryState(book, availableCopies));
            }
           
        }
        public override void UpdateInventoryState(int bookId, int change)
        {
            var state = FindInventoryState(bookId);
            if (state != null)
            {
                state.AvailableCopies += change;
            }
        }
        BookRecordType GetBookRecordType(int typeid)
        {
            return typeid switch
            {
                1 => BookRecordType.Borrowed,
                2 => BookRecordType.Returned,
                _ => throw new ArgumentOutOfRangeException(nameof(typeid), "Invalid book record type")
            };
        }
        InventoryState FindInventoryState(int bookId)
        {
            return States.FirstOrDefault(s => s.Book.Id == bookId);
        }
        IBook FindBook(int id)
        {
            return Books.FirstOrDefault(b => b.Id == id);
        }
        Customer FindCustomer(int id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }
        int AmmoutLeft(int bookId)
        {
            var state = FindInventoryState(bookId);
            return state != null ? state.AvailableCopies : 0;
        }
    }

}
