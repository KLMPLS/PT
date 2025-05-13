namespace LibraryData.Objects.DatabaseData
{
    internal class InventoryState
    {
        public Book Book { get; set; }
        public int AvailableCopies { get; set; }

        public InventoryState(Book book,int av_copies)
        {
            Book = book;
            AvailableCopies = av_copies;
        }
    }
}
