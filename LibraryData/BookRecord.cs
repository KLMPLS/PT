namespace LibraryData
{
    public class BookRecord
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Book Book { get; set; }
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
