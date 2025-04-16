namespace LibraryData
{
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

        public Book(int id, string title, string author, string genre)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
        }
        public override string ToString()
        {
            return $"{Title} by {Author} [{Genre}]";
        }
    }
}
