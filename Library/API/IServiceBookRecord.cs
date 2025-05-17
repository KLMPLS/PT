namespace LibraryService.API
{
    public interface IServiceBookRecord
    {
        public int Id { get; set; }
        public int customer_id{ get; set; }
        public int book_id { get; set; }
        public string type { get; set; }
        public DateTime Date { get; set; }
    }
}
