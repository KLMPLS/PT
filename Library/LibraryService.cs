namespace LibraryLogic
{
    using LibraryData.API;

    public class LibraryService : ILibraryService
    {
        DataRep dataRep;
        public LibraryService(IDataStorage dataStorage) {
        dataRep = new DataRep(dataStorage);
        }
        public override void BorrowBook(int BookId, int UserId) { 
            dataRep.AddRecord(UserId, BookId, 1);
        }
        public override void ReturnBook(int BookId, int UserId) { 
            dataRep.AddRecord(UserId, BookId, 2);
        }
        public override void AddtoInventory(int BookId, int NewCopies) {
            dataRep.UpdateInventoryState(BookId, NewCopies);
        }
        public override void RemoveFromInventory(int BookId, int MinusCopies) {
            dataRep.UpdateInventoryState(BookId, -MinusCopies);
        }
    }
}
