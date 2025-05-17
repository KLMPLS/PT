using System;
using System.ComponentModel;
using LibraryService.API;

namespace PresentationLayer.Model
{
    public class BookRecordModel : INotifyPropertyChanged
    {
        private int _id;
        private int _customerId;
        private int _bookId;
        private string _type;
        private DateTime _date;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public int CustomerId
        {
            get => _customerId;
            set { _customerId = value; OnPropertyChanged(nameof(CustomerId)); }
        }

        public int BookId
        {
            get => _bookId;
            set { _bookId = value; OnPropertyChanged(nameof(BookId)); }
        }

        public string Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(nameof(Type)); }
        }

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
