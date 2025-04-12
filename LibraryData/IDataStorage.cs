namespace LibraryData
{
    public interface IDataStorage
    {
        List<Book> Books { get; }
        List<Customer> Customers { get; }
        List<BookRecord> Records { get; }
        List<InventoryState> States { get; }
    }
}
