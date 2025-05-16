namespace LibraryLogicTests
{
    internal class FakeBookRecord
    {
        public int Id { get; set; }
        public FakeCustomer Customer { get; set; }
        public FakeBook Book { get; set; }
        public FakeBookRecordType Type { get; set; }
        public DateTime Date { get; set; }
        public FakeBookRecord(int id, FakeCustomer customer, FakeBook book, FakeBookRecordType type)
        {
            Id = id;
            Customer = customer;
            Book = book;
            Type = type;
            Date = DateTime.Now;
        }
    }
}
