namespace LibraryLogicTests
{
    using LibraryData;
    internal class FakeInMemoryDataStorage : IDataStorage
    {
        internal List<FakeBook> Books { get; } = new();
        internal List<FakeCustomer> Customers { get; } = new();
        internal List<FakeBookRecord> Records { get; } = new();
        internal List<FakeInventoryState> States { get; } = new();
        public override void AddBook(string title, string author, string genre) {
            FakeBook book = new FakeBook(Books.Count + 1, title, author, genre);
            Books.Add(book);
        }
        public override void AddCustomer(string name, string email)
        {
            Customers.Add(new FakeCustomer(Customers.Count + 1, name, email));
        }
        public override void AddRecord(int customerId, int bookId, int typeid)
        {
            var customer = FindCustomer(customerId);
            var book = FindBook(bookId);
            if (customer != null && book != null && ((AmmoutLeft(bookId) != 0) || (typeid == 2)))
            {
                Records.Add(new FakeBookRecord(Records.Count + 1, customer, book, GetBookRecordType(typeid)));
            }
        }
        public override void AddInventoryState(int bookId, int availableCopies)
        {
            var book = FindBook(bookId);
            if (book != null)
            {
                States.Add(new FakeInventoryState(book, availableCopies));
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
        FakeBookRecordType GetBookRecordType(int typeid)
        {
            return typeid switch
            {
                1 => FakeBookRecordType.Borrowed,
                2 => FakeBookRecordType.Returned,
                _ => throw new ArgumentOutOfRangeException(nameof(typeid), "Invalid book record type")
            };
        }
        FakeInventoryState FindInventoryState(int bookId)
        {
            return States.FirstOrDefault(s => s.Book.Id == bookId);
        }
        FakeBook FindBook(int id)
        {
            return Books.FirstOrDefault(b => b.Id == id);
        }
        FakeCustomer FindCustomer(int id)
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
