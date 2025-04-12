namespace Library.Logic
{
    using Library.Data;
    public class LibraryService : ILibraryService
    {
        private IDataStorage storage;

        public LibraryService(IDataStorage storage)
        {
            this.storage = storage;
        }

        public void AddBook(Book book)
        {
            if (storage.Books.Any(b => b.Id == book.Id && b.Title == book.Title))
            {
                var state = storage.States.Find(b => b.Book.Id == book.Id);
                if (state != null)
                {
                    state.AvailableCopies += book.TotalCopies;   
                }
                return;
            }
            storage.Books.Add(book);
            storage.States.Add(new InventoryState(book));
        }

        public bool BorrowBook(string id, int userId) {
            var book = storage.Books.FirstOrDefault(b => b.Id == id);
            var cust = storage.Customers.FirstOrDefault(u => u.Id == userId);
            var bookState = storage.States.FirstOrDefault(s => s.Book.Id == id);
            if (book == null || cust == null || bookState == null || bookState.AvailableCopies == 0)
            {
                return false;
            }
            int recordId = storage.Records.Count > 0 ? storage.Records.Max(r => r.Id) + 1 : 1;
            storage.Records.Add(new BookRecord(recordId, cust, book, BookRecordType.Borrowed));
            bookState.AvailableCopies--;
            return true;
        }
        public bool ReturnBook(string id, int userId){
            var book = storage.Books.FirstOrDefault(b => b.Id == id);
            var cust = storage.Customers.FirstOrDefault(u => u.Id == userId);
            var bookState = storage.States.FirstOrDefault(s => s.Book.Id == id);
            if (book == null || cust == null || bookState == null || bookState.AvailableCopies == 0)
            {
                return false;
            }
            int recordId = storage.Records.Count > 0 ? storage.Records.Max(r => r.Id) + 1 : 1;
            storage.Records.Add(new BookRecord(recordId, cust, book, BookRecordType.Returned));
            bookState.AvailableCopies++;
            return true;
        }
        public int GetAvailableCopies(string id)
        {
            var bookState = storage.States.FirstOrDefault(s => s.Book.Id == id);
            if (bookState != null)
            {
                return bookState.AvailableCopies;
            }
            return 0;
        }
    }
}
