namespace LibraryData.Objects
{
    internal class InventoryStateO
    {
        public BookO Book { get; set; }
        public int AvailableCopies { get; set; }

        public InventoryStateO(BookO book,int av_copies)
        {
            Book = book;
            AvailableCopies = av_copies;
        }
    }
}
