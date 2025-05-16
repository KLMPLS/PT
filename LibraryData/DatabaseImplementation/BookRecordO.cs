using LibraryData.API;
namespace LibraryData.Objects
{
    internal class BookRecordO : IBookRecord
    {
        public int Id { get; set; }
        public int customer_id { get; set; }
        public int book_id{ get; set; }
        public string type { get; set; }
        public DateTime Date { get; set; }
        public BookRecordO(int id, int customer, int book, string type)
        {
            Id = id;
            customer_id = customer;
            book_id = book;
            this.type = type;
            Date = DateTime.Now;
        }
    }
}
