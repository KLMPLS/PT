﻿using LibraryData.API;
using LibraryDataTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LibraryDataTest
{
    [TestClass]
    public class LibraryDataUnitTests
    {
        String connect = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\GniewkoPC\\Desktop\\PT\\PT\\LibraryData\\Database1.mdf;Integrated Security=True";
        [TestMethod]
        public void BookTests()
        {
            using (IDataStorage storage = IDataStorage.GenerateStorage(connect))
            {
                CreateData.generateBookData(storage);
                var book = storage.getAllBooks().FirstOrDefault(b => b.Title == "UnitTest_UniqueBook" && b.Author == "UnitTester");

                Assert.IsNotNull(book);
                Assert.AreEqual("TestGenre", book.Genre);

                storage.RemoveBook(book.Id);
            }
        }

        [TestMethod]
        public void CustomerTests()
        {
            using (IDataStorage storage = IDataStorage.GenerateStorage(connect))
            {
                CreateData.generateCustomerData(storage);
                var customer = storage.getAllCustomers().FirstOrDefault(c => c.Name == "TestUser_Unique" && c.Email == "unique_email@example.com");

                Assert.IsNotNull(customer);

                storage.RemoveCustomer(customer.Id);
            }
        }

        [TestMethod]
        public void BookRecordTests()
        {
            using (IDataStorage storage = IDataStorage.GenerateStorage(connect))
            {
                string bookTitle = "UnitTest_BookRecordBook";
                string customerName = "UnitTest_Customer";
                string customerEmail = "bookrecord@example.com";

                storage.AddCustomer(customerName, customerEmail);
                storage.AddBook(bookTitle, "AuthorTest", "GenreTest");

                var customer = storage.getAllCustomers().First(c => c.Name == customerName && c.Email == customerEmail);
                var book = storage.getAllBooks().First(b => b.Title == bookTitle);

                storage.AddRecord(customer.Id, book.Id, "borrow", DateTime.Today);

                var record = storage.getAllBooksRecord().FirstOrDefault(r => r.customer_id == customer.Id && r.book_id == book.Id && r.type == "borrow");

                Assert.IsNotNull(record);

                storage.RemoveRecord(record.Id);
                storage.RemoveBook(book.Id);
                storage.RemoveCustomer(customer.Id);
            }
        }

        [TestMethod]
        public void InventoryStateTests()
        {
            using (IDataStorage storage = IDataStorage.GenerateStorage(connect))
            {
                string bookTitle = "UnitTest_InventoryBook";

                storage.AddBook(bookTitle, "InventoryAuthor", "InventoryGenre");
                var book = storage.getAllBooks().First(b => b.Title == bookTitle);

                storage.AddInventoryState(book.Id, 15);
                var state = storage.getAllInventoryStates().FirstOrDefault(s => s.book_id == book.Id);

                Assert.IsNotNull(state);
                Assert.AreEqual(15, state.AvailableCopies);

                storage.UpdateInventoryState(book.Id, -5);
                Assert.AreEqual(10, storage.FindInventoryState(book.Id).AvailableCopies);

                storage.RemoveInventoryState(book.Id);
                storage.RemoveBook(book.Id);
            }
        }
    }
}
