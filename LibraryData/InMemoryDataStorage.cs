namespace LibraryData
{
     public class InMemoryDataStorage : IDataStorage
    {
        public List<Book> Books { get; } = new();
        public List<Customer> Customers { get; } = new();
        public List<BookRecord> Records { get; } = new();
        public List<InventoryState> States { get; } = new();

    }
}
