using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationLayer.ViewModel;
using PresentationLayer.Model;
using PresentationLayer.Model.API;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PresentationLayerTest
{
    [TestClass]
    public class ViewModelTests
    {
        private ModelService model = null!;
        private IModelService imodel = null!;

        [TestInitialize]
        public void Setup()
        {
            var mockStorage = new InMemoryDataStorage();
            var fakeService = new FakeLibraryServiceImp(mockStorage);
            model = new ModelService(fakeService);
            imodel = model;
        }

        private async Task WaitForConditionAsync(Func<bool> condition, int timeoutMs = 2000, int pollMs = 50)
        {
            var start = DateTime.UtcNow;
            while (!condition())
            {
                if ((DateTime.UtcNow - start).TotalMilliseconds > timeoutMs)
                    break;
                await Task.Delay(pollMs);
            }
        }

        [TestMethod]
        public async Task BooksViewModel_AddAndDeleteBook()
        {
            var vm = new BooksViewModel(imodel);
            vm.NewBookTitle = "BookVMTest";
            vm.NewBookAuthor = "AuthorVM";
            vm.NewBookGenre = "GenreVM";
            Assert.IsTrue(vm.AddBookCommand.CanExecute(null));
            await Task.Run(() => vm.AddBookCommand.Execute(null));
            await WaitForConditionAsync(() => vm.Books.Any(b => b.Title == "BookVMTest"));
            Assert.IsTrue(vm.Books.Any(b => b.Title == "BookVMTest"));
            var book = vm.Books.First(b => b.Title == "BookVMTest");
            vm.DeleteBookId = book.Id.ToString();
            Assert.IsTrue(vm.DeleteBookCommand.CanExecute(null));
            await Task.Run(() => vm.DeleteBookCommand.Execute(null));
            await WaitForConditionAsync(() => !vm.Books.Any(b => b.Title == "BookVMTest"));
            Assert.IsFalse(vm.Books.Any(b => b.Title == "BookVMTest"));
        }

        [TestMethod]
        public async Task CustomersViewModel_AddAndDeleteCustomer()
        {
            var vm = new CustomersViewModel(imodel);
            vm.NewCustomerName = "CustomerVMTest";
            vm.NewCustomerEmail = "vm@email.com";
            Assert.IsTrue(vm.AddCustomerCommand.CanExecute(null));
            await Task.Run(() => vm.AddCustomerCommand.Execute(null));
            await WaitForConditionAsync(() => vm.Customers.Any(c => c.Name == "CustomerVMTest"));
            Assert.IsTrue(vm.Customers.Any(c => c.Name == "CustomerVMTest"));
            var customer = vm.Customers.First(c => c.Name == "CustomerVMTest");
            vm.DeleteCustomerId = customer.Id.ToString();
            Assert.IsTrue(vm.DeleteCustomerCommand.CanExecute(null));
            await Task.Run(() => vm.DeleteCustomerCommand.Execute(null));
            await WaitForConditionAsync(() => !vm.Customers.Any(c => c.Name == "CustomerVMTest"));
            Assert.IsFalse(vm.Customers.Any(c => c.Name == "CustomerVMTest"));
        }

        [TestMethod]
        public async Task BookRecordsViewModel_AddAndDeleteRecord()
        {
            var bookVm = new BooksViewModel(imodel);
            var custVm = new CustomersViewModel(imodel);
            bookVm.NewBookTitle = "BookRecVM";
            bookVm.NewBookAuthor = "RecAuthor";
            bookVm.NewBookGenre = "RecGenre";
            await Task.Run(() => bookVm.AddBookCommand.Execute(null));
            await WaitForConditionAsync(() => bookVm.Books.Any(b => b.Title == "BookRecVM"));
            custVm.NewCustomerName = "RecCustomer";
            custVm.NewCustomerEmail = "rec@vm.com";
            await Task.Run(() => custVm.AddCustomerCommand.Execute(null));
            await WaitForConditionAsync(() => custVm.Customers.Any(c => c.Name == "RecCustomer"));
            var book = bookVm.Books.First(b => b.Title == "BookRecVM");
            var customer = custVm.Customers.First(c => c.Name == "RecCustomer");
            var vm = new BookRecordsViewModel(imodel);
            vm.NewCustomerId = customer.Id.ToString();
            vm.NewBookId = book.Id.ToString();
            vm.NewType = "borrow";
            vm.NewDate = DateTime.Today;
            Assert.IsTrue(vm.AddCommand.CanExecute(null));
            await Task.Run(() => vm.AddCommand.Execute(null));
            await WaitForConditionAsync(() => vm.BookRecords.Any(r => r.CustomerId == customer.Id && r.BookId == book.Id));
            Assert.IsTrue(vm.BookRecords.Any(r => r.CustomerId == customer.Id && r.BookId == book.Id));
            var record = vm.BookRecords.First(r => r.CustomerId == customer.Id && r.BookId == book.Id);
            vm.DeleteRecordId = record.Id.ToString();
            Assert.IsTrue(vm.DeleteCommand.CanExecute(null));
            await Task.Run(() => vm.DeleteCommand.Execute(null));
            await WaitForConditionAsync(() => !vm.BookRecords.Any(r => r.Id == record.Id));
            Assert.IsFalse(vm.BookRecords.Any(r => r.Id == record.Id));
        }

        [TestMethod]
        public async Task InventoryStatesViewModel_AddBorrowReturnDelete()
        {
            var bookVm = new BooksViewModel(imodel);
            bookVm.NewBookTitle = "InvBookVM";
            bookVm.NewBookAuthor = "InvAuthor";
            bookVm.NewBookGenre = "InvGenre";
            await Task.Run(() => bookVm.AddBookCommand.Execute(null));
            await WaitForConditionAsync(() => bookVm.Books.Any(b => b.Title == "InvBookVM"));
            var book = bookVm.Books.First(b => b.Title == "InvBookVM");
            var vm = new InventoryStatesViewModel(imodel);
            vm.NewInventoryStateBookId = book.Id.ToString();
            vm.NewInventoryStateAvailable = "5";
            Assert.IsTrue(vm.AddInventoryStateCommand.CanExecute(null));
            await Task.Run(() => vm.AddInventoryStateCommand.Execute(null));
            await WaitForConditionAsync(() => vm.InventoryStates.Any(s => s.Id == book.Id));
            Assert.IsTrue(vm.InventoryStates.Any(s => s.Id == book.Id));
            var state = vm.InventoryStates.First(s => s.Id == book.Id);
            vm.SelectedInventoryState = state;
            await Task.Run(() => vm.BorrowCommand.Execute(null));
            await WaitForConditionAsync(() => vm.InventoryStates.First(s => s.Id == book.Id).Available == 4);
            Assert.AreEqual(4, vm.InventoryStates.First(s => s.Id == book.Id).Available);
            await Task.Run(() => vm.ReturnCommand.Execute(null));
            await WaitForConditionAsync(() => vm.InventoryStates.First(s => s.Id == book.Id).Available == 5);
            Assert.AreEqual(5, vm.InventoryStates.First(s => s.Id == book.Id).Available);
            vm.DeleteInventoryStateId = book.Id.ToString();
            Assert.IsTrue(vm.DeleteInventoryStateCommand.CanExecute(null));
            await Task.Run(() => vm.DeleteInventoryStateCommand.Execute(null));
            await WaitForConditionAsync(() => !vm.InventoryStates.Any(s => s.Id == book.Id));
            Assert.IsFalse(vm.InventoryStates.Any(s => s.Id == book.Id));
        }
    }
}
