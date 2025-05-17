using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibraryService.API;

namespace PresentationLayer.Model
{
    public class CustomerModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _email;

        public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
