using LibraryData.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServiceTest
{
    internal class TestBookRecord : IBookRecord
    {
        public int Id { get; set; }
        public int customer_id { get; set; }
        public int book_id { get; set; }
        public string type { get; set; }
        public DateTime Date { get; set; }

    }
}
