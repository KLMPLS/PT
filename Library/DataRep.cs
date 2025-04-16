using LibraryData;

namespace LibraryLogic
{
    internal class DataRep
    {
        IDataStorage dataStorage;
        public DataRep(IDataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }
        public void AddBook(string title, string author, string genre)
        {
            dataStorage.AddBook(title, author, genre);
        }
        public void AddCustomer(string name, string email)
        {
            dataStorage.AddCustomer(name, email);
        }
        public void AddRecord(int customerId, int bookId, int typeid)
        {
            dataStorage.AddRecord(customerId, bookId, typeid);
            dataStorage.UpdateInventoryState(bookId, typeid == 1 ? -1 : 1);
        }
        public void AddInventoryState(int bookId, int availableCopies)
        {
            dataStorage.AddInventoryState(bookId, availableCopies);
        }
        public void UpdateInventoryState(int bookId, int change)
        {
            dataStorage.UpdateInventoryState(bookId, change);
        }

    }
}
