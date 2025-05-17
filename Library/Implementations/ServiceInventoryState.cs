using LibraryService.API;

namespace LibraryService.Implementations
{
    internal class ServiceInventoryState : IServiceInventoryState
    {
        public int book_id { get; set; }
        public int AvailableCopies { get; set; }

        public ServiceInventoryState(int id,int av_copies)
        {
            book_id = id;
            AvailableCopies = av_copies;
        }
    }
}
