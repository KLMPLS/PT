namespace LibraryData.Objects
{
    internal interface IBookRecord
    {
        public int Id { get; set; }
        public ICustomer Customer { get; set; }
        public IBook Book { get; set; }
        public BookRecordType Type { get; set; }
        public DateTime Date { get; set; }
    }
}
