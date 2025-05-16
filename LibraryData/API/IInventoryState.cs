namespace LibraryData.API
{
    public interface IInventoryState
    {
        public int book_id { get; set; }
        public int AvailableCopies { get; set; }
    }
}
