using LibraryData.API;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataTests
{
    internal static class CreateData
    {
        public static void generateBookData(IDataStorage storage)
        {
            storage.AddBook("UnitTest_UniqueBook", "UnitTester", "TestGenre");
        }
        public static void generateCustomerData(IDataStorage storage)
        {
            storage.AddCustomer("TestUser_Unique", "unique_email@example.com");
        }
    }
}
