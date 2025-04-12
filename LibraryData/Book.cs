namespace LibraryData
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int TotalCopies { get; set; }

        public Book(string id, string title, string author, string genre, int totalCopies)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            TotalCopies = totalCopies;
        }
        public override string ToString()
        {
            return $"{Title} by {Author} [{Genre}] ({TotalCopies} copies)";
        }
    }
}
