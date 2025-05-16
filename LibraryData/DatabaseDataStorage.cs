using LibraryData.Objects;
using LibraryData.API;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryData
{
     internal class DatabaseDataStorage : IDataStorage
    {
        private LibraryDataContext _context;

        public DatabaseDataStorage (string connection) {
                _context = new LibraryDataContext (connection);
            }

        public override void AddBook(string title, string author, string genre) {
            Book book = new Book()
            {
                title = title,
                author = author,
                genre = genre
            };
            _context.Books.InsertOnSubmit(book);
            _context.SubmitChanges();
        }
        public override void AddCustomer(string name, string email)
        {
            Customer customer = new Customer()
            {
                name = name,
                email = email
            };
            _context.Customers.InsertOnSubmit(customer);
            _context.SubmitChanges();
        }
        public override void AddRecord(int customerId, int bookId, string type)
        {
            BookRecord record = new BookRecord()
            {
                book_id = bookId,
                customer_id = customerId,
                type = type
            };
            _context.BookRecords.InsertOnSubmit(record);
            _context.SubmitChanges();
        }
        public override void AddInventoryState(int bookId, int availableCopies)
        {
            InventoryState state = new InventoryState()
            {
                Book_id = bookId,
                Available = availableCopies
            };
            _context.InventoryStates.InsertOnSubmit(state);
            _context.SubmitChanges();
        }
        public override void UpdateInventoryState(int bookId, int change)
        {
            InventoryState state = _context.InventoryStates.Single(a=>a.Book_id == bookId);
            state.Available += change;
            _context.SubmitChanges();
        }
        public override IInventoryState FindInventoryState(int bookId)
        {
            InventoryState state = _context.InventoryStates.Single(id=>id.Book_id == bookId);
            return new InventoryStateO(state.Book_id, (int)state.Available);
        }
        public override IBook FindBook(int id)
        {
            Book state = _context.Books.Single(a => a.Id == id);
            return new BookO(state.Id, state.title, state.author, state.genre);
        }
        public override ICustomer FindCustomer(int id)
        {
            Customer state = _context.Customers.Single(a => a.Id == id);
            return new CustomerO(state.Id, state.name, state.email);
        }
        public override IBookRecord FindRecord(int id)
        {
            BookRecord state = _context.BookRecords.Single(a => a.Id==id);
            return new BookRecordO(state.Id, state.customer_id, state.book_id, state.type);
        }

        public override void RemoveBook(int id)
        {
            Book state = _context.Books.Single(a => a.Id == id);
            _context.Books.DeleteOnSubmit(state);
            _context.SubmitChanges();
        }

        public override void RemoveCustomer(int id)
        {
            Customer state = _context.Customers.Single(a => a.Id == id);
            _context.Customers.DeleteOnSubmit(state);
            _context.SubmitChanges();
        }

        public override void RemoveRecord(int id)
        {
            BookRecord state = _context.BookRecords.Single(a=> a.Id ==id);
            _context.BookRecords.DeleteOnSubmit(state);
            _context.SubmitChanges();
        }

        public override void RemoveInventoryState(int bookId)
        {
           InventoryState state = _context.InventoryStates.Single(a=> a.Book_id==bookId);
            _context.InventoryStates.DeleteOnSubmit(state);
            _context.SubmitChanges();
        }

        public override List<Customer> getAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public override List<Book> getAllBooks()
        {
            return _context.Books.ToList();
        }

        public override List<BookRecord> getAllBooksRecord()
        {
            return _context.BookRecords.ToList();
        }

        public override List<InventoryState> getAllInventoryStates()
        {
            return _context.InventoryStates.ToList();
        }
    }
}
