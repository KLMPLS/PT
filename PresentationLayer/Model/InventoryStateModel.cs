using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibraryService.API;

namespace PresentationLayer.Model
{
    public class InventoryStateModel : INotifyPropertyChanged
    {
        private int _id;
        private int _available;

        public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public int Available { get => _available; set { _available = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
