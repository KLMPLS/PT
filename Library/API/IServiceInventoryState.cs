namespace LibraryService.API
{
    public interface IServiceInventoryState
    {
        public int book_id { get; set; }
        public int AvailableCopies { get; set; }
    }
}
