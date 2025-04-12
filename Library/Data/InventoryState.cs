namespace Library.Data
{
    public class InventoryState
    {
        public Book Book { get; set; }
        public int AvailableCopies { get; set; }

        public InventoryState(Book book)
        {
            Book = book;
            AvailableCopies = book.TotalCopies;
        }
    }
}
