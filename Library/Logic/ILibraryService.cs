namespace Library.Logic
{
    using Library.Data;
    public interface ILibraryService
    {
        void AddBook(Book book);
        bool BorrowBook(string id, int userId);
        bool ReturnBook(string id, int userId);
        int GetAvailableCopies(string id);
    }
}
