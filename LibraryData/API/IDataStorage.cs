namespace LibraryData.API
{
    public abstract class IDataStorage
    {
        public abstract void AddBook(string title,string author,string genre);
        public abstract void RemoveBook(int id);
        public abstract IBook FindBook(int id);
        public abstract void AddCustomer(string name, string email);
        public abstract void RemoveCustomer(int id);
        public abstract ICustomer FindCustomer(int id);
        public abstract void AddRecord(int customerId, int bookId, string type, DateTime a);
        public abstract IBookRecord FindRecord(int id);
        public abstract void RemoveRecord(int id);
        public abstract void AddInventoryState(int bookId, int availableCopies);
        public abstract IInventoryState FindInventoryState(int bookId);
        public abstract void RemoveInventoryState(int bookId);
        public abstract void UpdateInventoryState(int bookId, int change);
        public abstract List<ICustomer> getAllCustomers();
        public abstract List<IBook> getAllBooks();
        public abstract List<IBookRecord> getAllBooksRecord();
        public abstract List<IInventoryState> getAllInventoryStates();

        public abstract void ClearAllBooks();

        public static IDataStorage GenerateStorage()
        {
            return new DatabaseDataStorage();
        }
    }
}
