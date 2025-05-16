namespace LibraryService.API
{
    using LibraryData;
    public abstract class ILibraryService
    {
        /*
                 public abstract void AddBook(string title,string author,string genre);
        public abstract void RemoveBook(int id);
        public abstract IBook FindBook(int id);
        public abstract void AddCustomer(string name, string email);
        public abstract void RemoveCustomer(int id);
        public abstract ICustomer FindCustomer(int id);
        public abstract void AddRecord(int customerId, int bookId, string type);
        public abstract IBookRecord FindRecord(int id);
        public abstract void RemoveRecord(int id);
        public abstract void AddInventoryState(int bookId, int availableCopies);
        public abstract IInventoryState FindInventoryState(int bookId);
        public abstract void RemoveInventoryState(int bookId);
        public abstract void UpdateInventoryState(int bookId, int change);
         
         */
        public abstract void AddBook(string title, string author, string genre);
        public abstract void RemoveBook(int id);

        public abstract void AddCustomer(string name, string email);
        public abstract void RemoveCustomer(int id);

        public abstract void AddRecord(int customerId, int bookId, string type);
        public abstract void RemoveRecord(int id);
        public abstract void AddInventoryState(int bookId, int availableCopies);
        public abstract void RemoveInventoryState(int bookId);
        public abstract void BorrowBook(int bookId, int customerId, int change);
        public abstract void ReturnBook(int bookId, int customerId, int change);

    }
}
