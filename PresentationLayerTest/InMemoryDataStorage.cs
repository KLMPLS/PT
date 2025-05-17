using LibraryData.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTest
{
    internal class InMemoryDataStorage : IDataStorage
    {
        private List<IBook> books = new();
        private List<ICustomer> customers = new();
        private List<IBookRecord> records = new();
        private List<IInventoryState> inventoryStates = new();

        private int bookIdCounter = 1;
        private int customerIdCounter = 1;
        private int recordIdCounter = 1;

        public override void AddBook(string title, string author, string genre)
        {
            books.Add(new TestBook
            {
                Id = bookIdCounter++,
                Title = title,
                Author = author,
                Genre = genre
            });
        }

        public override void RemoveBook(int id) =>
            books.RemoveAll(b => b.Id == id);

        public override IBook FindBook(int id) =>
            books.FirstOrDefault(b => b.Id == id);

        public override void AddCustomer(string name, string email)
        {
            customers.Add(new TestCustomer
            {
                Id = customerIdCounter++,
                Name = name,
                Email = email
            });
        }

        public override void RemoveCustomer(int id) =>
            customers.RemoveAll(c => c.Id == id);

        public override ICustomer FindCustomer(int id) =>
            customers.FirstOrDefault(c => c.Id == id);

        public override void AddRecord(int customerId, int bookId, string type, DateTime a)
        {
            records.Add(new TestBookRecord
            {
                Id = recordIdCounter++,
                customer_id = customerId,
                book_id = bookId,
                type = type,
                Date = a
            });
        }

        public override IBookRecord FindRecord(int id) =>
            records.FirstOrDefault(r => r.Id == id);

        public override void RemoveRecord(int id) =>
            records.RemoveAll(r => r.Id == id);

        public override void AddInventoryState(int bookId, int availableCopies)
        {
            inventoryStates.Add(new TestInventoryState
            {
                book_id = bookId,
                AvailableCopies = availableCopies
            });
        }

        public override IInventoryState FindInventoryState(int bookId) =>
            inventoryStates.FirstOrDefault(s => s.book_id == bookId);

        public override void RemoveInventoryState(int bookId) =>
            inventoryStates.RemoveAll(s => s.book_id == bookId);

        public override void UpdateInventoryState(int bookId, int change)
        {
            var state = FindInventoryState(bookId);
            if (state != null)
            {
                state.AvailableCopies += change;
            }
        }

        public override List<ICustomer> getAllCustomers() => new(customers);
        public override List<IBook> getAllBooks() => new(books);
        public override List<IBookRecord> getAllBooksRecord() => new(records);
        public override List<IInventoryState> getAllInventoryStates() => new(inventoryStates);
    }

}
