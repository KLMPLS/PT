namespace LibraryLogicTests
{
    internal class FakeCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public FakeCustomer(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public override string ToString()
        {
            return $"{Name} ({Email})";
        }
    }
}
