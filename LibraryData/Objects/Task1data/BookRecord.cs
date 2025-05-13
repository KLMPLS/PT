namespace LibraryData.Objects.Task1data
{
    internal class BookRecord : IBookRecord
    {
        public int Id { get; set; }
        public ICustomer Customer { get; set; }
        public IBook Book { get; set; }
        public BookRecordType Type { get; set; }
        public DateTime Date { get; set; }
        public BookRecord(int id, Customer customer, Book book, BookRecordType type)
        {
            Id = id;
            Customer = customer;
            Book = book;
            Type = type;
            Date = DateTime.Now;
        }
    }
}
