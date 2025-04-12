namespace LibraryLogic
{
    using LibraryData;
    public interface ILibraryService
    {
        void AddBook(Book book);
        bool BorrowBook(string id, int userId);
        bool ReturnBook(string id, int userId);
        int GetAvailableCopies(string id);
    }
}
