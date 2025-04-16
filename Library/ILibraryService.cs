namespace LibraryLogic
{
    using LibraryData;
    public abstract class ILibraryService
    {
        public abstract void BorrowBook(int BookId, int UserId);
        public abstract void ReturnBook(int BookId, int UserId);
        public abstract void AddtoInventory(int BookId, int NewCopies);
        public abstract void RemoveFromInventory(int BookId, int MinusCopi);


    }
}
