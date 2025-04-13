using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryData.Tests
{
    [TestClass()]

    public class CustomerTests
    {
        private Customer customer = new(2, "John Doe", "john.doe@gmail.com");

        [TestMethod()]
        public void CustomerTest()
        {
            Assert.AreEqual(customer.Id, 2);
            Assert.AreEqual(customer.Name, "John Doe");
            Assert.AreEqual(customer.Email, "john.doe@gmail.com");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(customer.ToString(), "John Doe (john.doe@gmail.com)");
        }
    }
}