using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PresentationLayer.Model.API
{
    public interface IModelService
    {
        public Task AddBookAsync(string title, string author, string genre);
        public Task RemoveBookAsync(int id);

        public Task AddCustomerAsync(string name, string email);
        public Task RemoveCustomerAsync(int id);

        public Task AddRecordAsync(int customerId, int bookId, string type, DateTime date);
        public Task RemoveRecordAsync(int id);

        public Task AddInventoryStateAsync(int bookId, int availableCopies);
        public Task RemoveInventoryStateAsync(int bookId);

        public Task BorrowBookAsync(int bookId, int change);
        public Task ReturnBookAsync(int bookId, int change);

        public Task<List<ICustomerModel>> GetAllCustomersAsync();
        public Task<List<IBookModel>> GetAllBooksAsync();
        public Task<List<IBookRecordModel>> GetAllBooksRecordAsync();
        public Task<List<IInventoryStateModel>> GetAllInventoryStatesAsync();
    }
}