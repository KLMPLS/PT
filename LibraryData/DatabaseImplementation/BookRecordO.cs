using LibraryData.API;
namespace LibraryData.Objects
{
    internal class BookRecordO : IBookRecord
    {
        public int Id { get; set; }
        public CustomerO Customer { get; set; }
        public BookO Book { get; set; }
        public BookRecordType Type { get; set; }
        public DateTime Date { get; set; }
        public BookRecordO(int id, CustomerO customer, BookO book, BookRecordType type)
        {
            Id = id;
            Customer = customer;
            Book = book;
            Type = type;
            Date = DateTime.Now;
        }
    }
}
