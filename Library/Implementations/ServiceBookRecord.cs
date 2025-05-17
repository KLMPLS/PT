using LibraryService.API;
namespace LibraryService.Implementations
{
    internal class ServiceBookRecord : IServiceBookRecord
    {
        public int Id { get; set; }
        public int customer_id { get; set; }
        public int book_id{ get; set; }
        public string type { get; set; }
        public DateTime Date { get; set; }
        public ServiceBookRecord(int id, int customer, int book, string type, DateTime a)
        {
            Id = id;
            customer_id = customer;
            book_id = book;
            this.type = type;
            Date = a;
        }
    }
}
