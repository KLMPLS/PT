using PresentationLayer.Model;
using PresentationLayer.Model.API;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    internal class BookRecordsViewModel : ViewModelBase
    {
        private readonly IModelService _model;

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
            set
            {
                if (SetProperty(ref _selectedRecord, value))
                {
                    if (_selectedRecord != null)
                    {
                        NewCustomerId = _selectedRecord.CustomerId.ToString();
                        NewBookId = _selectedRecord.BookId.ToString();
                        NewType = _selectedRecord.Type;
                        NewDate = _selectedRecord.Date;
                    }
                    else
                    {
                        NewCustomerId = "";
                        NewBookId = "";
                        NewType = "";
                    }
                }
            }
        }

        // Properties for adding a new record
        private string _newCustomerId;
        public string NewCustomerId
        {
            get => _newCustomerId;
            set
            {
                if (SetProperty(ref _newCustomerId, value))
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }

        private string _newBookId;
        public string NewBookId
        {
            get => _newBookId;
            set
            {
                if (SetProperty(ref _newBookId, value))
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }

        private string _newType;
        public string NewType
        {
            get => _newType;
            set
            {
                if (SetProperty(ref _newType, value))
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
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
                    ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }

        // Commands
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public BookRecordsViewModel(IModelService model)
        {
            _model = model;

            AddCommand = new RelayCommand(async () => await AddRecord(), CanAddRecord);
            DeleteCommand = new RelayCommand(async () => await DeleteRecord(), CanDeleteRecord);

            _ = LoadBookRecords();
        }

        private async Task LoadBookRecords()
        {
            var records = await _model.GetAllBooksRecordAsync();
            BookRecords = new ObservableCollection<BookRecordModel>();
            foreach (BookRecordModel rec in records)
            {
                BookRecords.Add(rec);
            }
        }

        private bool CanAddRecord()
        {
            return
                int.TryParse(NewCustomerId, out _) &&
                int.TryParse(NewBookId, out _) &&
                !string.IsNullOrWhiteSpace(NewType);
        }

        private async Task AddRecord()
        {
            int custId = int.Parse(NewCustomerId);
            int bookId = int.Parse(NewBookId);

            await _model.AddRecordAsync(custId, bookId, NewType, NewDate);

            await LoadBookRecords();

            NewCustomerId = "";
            NewBookId = "";
            NewType = "";
            NewDate = DateTime.Now;
        }

        private bool CanDeleteRecord()
        {
            return int.TryParse(DeleteRecordId, out int id) && id > 0;
        }

        private async Task DeleteRecord()
        {
            if (int.TryParse(DeleteRecordId, out int id))
            {
                await _model.RemoveRecordAsync(id);
                await LoadBookRecords();
                DeleteRecordId = "";
            }
        }
    }
}
