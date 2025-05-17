using LibraryData.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerTest
{
    internal class TestInventoryState : IInventoryState
    {
        public int book_id { get; set; }
        public int AvailableCopies { get; set; }

    }
}
