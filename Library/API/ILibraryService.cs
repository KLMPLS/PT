namespace LibraryService.API
{
    using LibraryData.API;
    public abstract class ILibraryService
    {

        public abstract void AddBook(string title, string author, string genre);
        public abstract void RemoveBook(int id);

        public abstract void AddCustomer(string name, string email);
        public abstract void RemoveCustomer(int id);

        public abstract void AddRecord(int customerId, int bookId, string type, DateTime a);
        public abstract void RemoveRecord(int id);
        public abstract void AddInventoryState(int bookId, int availableCopies);
        public abstract void RemoveInventoryState(int bookId);
        public abstract void BorrowBook(int bookId, int customerId, int change, DateTime a);
        public abstract void ReturnBook(int bookId, int customerId, int change, DateTime a);

        public abstract List<IServiceCustomer> getAllCustomers();
        public abstract List<IServiceBook> getAllBooks();
        public abstract List<IServiceBookRecord> getAllBooksRecord();
        public abstract List<IServiceInventoryState> getAllInventoryStates();
        public abstract void ClearAll();
    }
}
