namespace LibraryData.API
{
    public interface IInventoryState
    {
        public IBook Book { get; set; }
        public int AvailableCopies { get; set; }
    }
}
