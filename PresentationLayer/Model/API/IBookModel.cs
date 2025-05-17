using System.ComponentModel;

namespace PresentationLayer.Model.API
{
    public interface IBookModel : INotifyPropertyChanged
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Genre { get; set; }
    }
}