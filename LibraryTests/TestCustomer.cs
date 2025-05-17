using LibraryData.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServiceTest
{
    internal class TestCustomer : ICustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


    }

}
