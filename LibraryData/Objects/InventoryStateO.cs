using LibraryData.API;

namespace LibraryData.Objects
{
    internal class InventoryStateO : IInventoryState
    {
        public int book_id { get; set; }
        public int AvailableCopies { get; set; }

        public InventoryStateO(int id,int av_copies)
        {
            book_id = id;
            AvailableCopies = av_copies;
        }
    }
}
