using LibraryData.Objects;

namespace LibraryData.API
{
    public interface IBookRecord
    {
        public int Id { get; set; }
        public ICustomer Customer { get; set; }
        public IBook Book { get; set; }
        public IRecordType Type { get; set; }
        public DateTime Date { get; set; }
    }
}
