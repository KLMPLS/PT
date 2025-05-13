namespace LibraryData.Objects
{
    internal interface IInventoryState
    {
        public IBook Book { get; set; }
        public int AvailableCopies { get; set; }
    }
}
