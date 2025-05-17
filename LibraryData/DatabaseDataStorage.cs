using LibraryData.Objects;
using LibraryData.API;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryData
{
     public class DatabaseDataStorage : IDataStorage
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
                type = type,
                date = DateTime.Now
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
            return new BookRecordO(state.Id, state.customer_id, state.book_id, state.type, state.date);
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

        public override List<ICustomer> getAllCustomers()
        {
            var customers = (from customer in _context.Customers select FindCustomer(customer.Id));
            List<ICustomer> customerList = new List<ICustomer>();
            foreach (var customer in customers)
            {
                customerList.Add(customer);
            }
            return customerList;
        }

        public override List<IBook> getAllBooks()
        {
            var books = (from book in _context.Books select FindBook(book.Id));
            List<IBook> bookList = new List<IBook>();
            foreach (var book in books)
            {
                bookList.Add(book);
            }
            return bookList;
        }

        public override List<IBookRecord> getAllBooksRecord()
        {
            var records = (from record in _context.BookRecords select FindRecord(record.Id));
            List<IBookRecord> recordList = new List<IBookRecord>();
            foreach (var record in records)
            {
                recordList.Add(record);
            }
            return recordList;
        }

        public override List<IInventoryState> getAllInventoryStates()
        {
            var states = (from state in _context.InventoryStates select FindInventoryState(state.Book_id));
            List<IInventoryState> stateList = new List<IInventoryState>();
            foreach (var state in states)
            {
                stateList.Add(state);
            }
            return stateList;
        }
        public override void ClearAllBooks()
        {
            _context.ExecuteCommand("DELETE FROM Book");
        }
    }
}
