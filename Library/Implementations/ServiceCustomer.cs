using LibraryService.API;

namespace LibraryService.Implementations
{
    internal class ServiceCustomer : IServiceCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ServiceCustomer(int id, string name, string email)
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
