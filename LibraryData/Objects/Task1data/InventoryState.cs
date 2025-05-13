namespace LibraryData.Objects.Task1data
{
    internal class InventoryState : IInventoryState
    {
        public IBook Book { get; set; }
        public int AvailableCopies { get; set; }

        public InventoryState(Book book,int av_copies)
        {
            Book = book;
            AvailableCopies = av_copies;
        }
    }
}
