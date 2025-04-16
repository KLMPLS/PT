namespace LibraryData
{
    public abstract class IDataStorage
    {
        public abstract void AddBook(string title,string author,string genre);
        public abstract void AddCustomer(string name, string email);
        public abstract void AddRecord(int customerId, int bookId, int typeid);
        public abstract void AddInventoryState(int bookId, int availableCopies);
        public abstract void UpdateInventoryState(int bookId, int change);


    }
}
