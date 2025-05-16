namespace LibraryLogicTests
{
    internal class FakeInventoryState
    {
        public FakeBook Book { get; set; }
        public int AvailableCopies { get; set; }

        public FakeInventoryState(FakeBook book,int av_copies)
        {
            Book = book;
            AvailableCopies = av_copies;
        }
    }
}
