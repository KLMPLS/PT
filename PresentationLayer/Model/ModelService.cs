using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryService.API;
using PresentationLayer.Model.API;

namespace PresentationLayer.Model
{
    internal class ModelService : IModelService
    {
        private readonly ILibraryService _libraryService;

        public ModelService(ILibraryService? libraryService = null)
        {
            _libraryService = libraryService ?? ILibraryService.GenerateLibraryService();
        }

        public Task AddBookAsync(string title, string author, string genre)
        {
            return Task.Run(() => _libraryService.AddBook(title, author, genre));
        }

        public Task RemoveBookAsync(int id)
        {
            return Task.Run(() => _libraryService.RemoveBook(id));
        }

        public Task AddCustomerAsync(string name, string email)
        {
            return Task.Run(() => _libraryService.AddCustomer(name, email));
        }

        public Task RemoveCustomerAsync(int id)
        {
            return Task.Run(() => _libraryService.RemoveCustomer(id));
        }

        public Task AddRecordAsync(int customerId, int bookId, string type, DateTime date)
        {
            return Task.Run(() => _libraryService.AddRecord(customerId, bookId, type, date));
        }

        public Task RemoveRecordAsync(int id)
        {
            return Task.Run(() => _libraryService.RemoveRecord(id));
        }

        public Task AddInventoryStateAsync(int bookId, int availableCopies)
        {
            return Task.Run(() => _libraryService.AddInventoryState(bookId, availableCopies));
        }

        public Task RemoveInventoryStateAsync(int bookId)
        {
            return Task.Run(() => _libraryService.RemoveInventoryState(bookId));
        }

        public Task BorrowBookAsync(int bookId, int change)
        {
            return Task.Run(() => _libraryService.BorrowBook(bookId, change));
        }

        public Task ReturnBookAsync(int bookId, int change)
        {
            return Task.Run(() => _libraryService.ReturnBook(bookId, change));
        }

        public Task<List<ICustomerModel>> GetAllCustomersAsync()
        {
            return Task.Run(() =>
            {
                var serviceCustomers = _libraryService.getAllCustomers();
                var models = new List<ICustomerModel>();
                foreach (var c in serviceCustomers)
                {
                    models.Add(new CustomerModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email
                    });
                }
                return models;
            });
        }

        public Task<List<IBookModel>> GetAllBooksAsync()
        {
            return Task.Run(() =>
            {
                var serviceBooks = _libraryService.getAllBooks();
                var models = new List<IBookModel>();
                foreach (var b in serviceBooks)
                {
                    models.Add(new BookModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        Genre = b.Genre
                    });
                }
                return models;
            });
        }

        public Task<List<IBookRecordModel>> GetAllBooksRecordAsync()
        {
            return Task.Run(() =>
            {
                var serviceRecords = _libraryService.getAllBooksRecord();
                var models = new List<IBookRecordModel>();
                foreach (var r in serviceRecords)
                {
                    models.Add(new BookRecordModel
                    {
                        Id = r.Id,
                        CustomerId = r.customer_id,
                        BookId = r.book_id,
                        Type = r.type,
                        Date = r.Date
                    });
                }
                return models;
            });
        }

        public Task<List<IInventoryStateModel>> GetAllInventoryStatesAsync()
        {
            return Task.Run(() =>
            {
                var serviceStates = _libraryService.getAllInventoryStates();
                var models = new List<IInventoryStateModel>();
                foreach (var s in serviceStates)
                {
                    models.Add(new InventoryStateModel
                    {
                        Id = s.book_id,
                        Available = s.AvailableCopies
                    });
                }
                return models;
            });
        }
    }
}