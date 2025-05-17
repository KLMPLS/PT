using LibraryService.API;
using PresentationLayer.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    public class BookRecordsViewModel : ViewModelBase
    {
        private readonly ILibraryService _service;

        private ObservableCollection<BookRecordModel> _bookRecords;
        public ObservableCollection<BookRecordModel> BookRecords
        {
            get => _bookRecords;
            set => SetProperty(ref _bookRecords, value);
        }

        private BookRecordModel _selectedRecord;
        public BookRecordModel SelectedRecord
        {
            get => _selectedRecord;
            set => SetProperty(ref _selectedRecord, value);
        }

        // Properties for adding a new record
        private string _newCustomerId;
        public string NewCustomerId
        {
            get => _newCustomerId;
            set
            { 
                if (SetProperty(ref _newCustomerId, value))
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged(); ; 
            }
        }

        private string _newBookId;
        public string NewBookId
        {
            get => _newBookId;
            set
            {
                if (SetProperty(ref _newBookId, value))
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged(); ;
            }
        }

        private string _newType;
        public string NewType
        {
            get => _newType;
            set
            {
                if (SetProperty(ref _newType, value))
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged(); ;
            }
        }

        private DateTime _newDate = DateTime.Now;
        public DateTime NewDate
        {
            get => _newDate;
            set => SetProperty(ref _newDate, value);
        }

        // Property for delete record id
        private string _deleteRecordId;
        public string DeleteRecordId
        {
            get => _deleteRecordId;
            set
            {
                if (SetProperty(ref _deleteRecordId, value))
                    ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged(); ;
            }
        }

        // Commands
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public BookRecordsViewModel(ILibraryService service)
        {
            _service = service;

            AddCommand = new RelayCommand(AddRecord, CanAddRecord);
            DeleteCommand = new RelayCommand(DeleteRecord, CanDeleteRecord);

            LoadBookRecords();
        }

        private void LoadBookRecords()
        {
            var records = _service.getAllBooksRecord(); 
            BookRecords = new ObservableCollection<BookRecordModel>();

            foreach (var rec in records)
            {
                BookRecords.Add(new BookRecordModel
                {
                    Id = rec.Id,
                    CustomerId = rec.customer_id,
                    BookId = rec.book_id,
                    Type = rec.type,
                    Date = rec.Date
                });
            }
        }

        private bool CanAddRecord()
        {
            return
                int.TryParse(NewCustomerId, out _) &&
                int.TryParse(NewBookId, out _) &&
                !string.IsNullOrWhiteSpace(NewType);
        }

        private void AddRecord()
        {
            // Convert inputs
            int custId = int.Parse(NewCustomerId);
            int bookId = int.Parse(NewBookId);

            _service.AddRecord(custId, bookId, NewType, NewDate);

            LoadBookRecords();

            // Clear inputs
            NewCustomerId = "";
            NewBookId = "";
            NewType = "";
            NewDate = DateTime.Now;
        }

        private bool CanDeleteRecord()
        {
            return int.TryParse(DeleteRecordId, out int id) && id > 0;
        }

        private void DeleteRecord()
        {
            if (int.TryParse(DeleteRecordId, out int id))
            {
                _service.RemoveRecord(id);
                LoadBookRecords();
                DeleteRecordId = "";
            }
        }
    }
}
