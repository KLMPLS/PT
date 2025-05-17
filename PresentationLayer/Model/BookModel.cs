using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibraryService.API;
using PresentationLayer.Model.API;
namespace PresentationLayer.Model
{
    internal class BookModel : INotifyPropertyChanged, IBookModel
    {
        private int _id;
        private string _title;
        private string _author;
        private string _genre;

        public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(); } }
        public string Author { get => _author; set { _author = value; OnPropertyChanged(); } }
        public string Genre { get => _genre; set { _genre = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
