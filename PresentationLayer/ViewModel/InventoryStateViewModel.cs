using LibraryService.API;
using PresentationLayer.Model;
using PresentationLayer.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
namespace PresentationLayer.ViewModel
{
    public class InventoryStatesViewModel : ViewModelBase
    {
        private readonly ILibraryService _service;

        private ObservableCollection<InventoryStateModel> _inventoryStates;
        public ObservableCollection<InventoryStateModel> InventoryStates
        {
            get => _inventoryStates;
            set => SetProperty(ref _inventoryStates, value);
        }

        private InventoryStateModel _selectedInventoryState;
        public InventoryStateModel SelectedInventoryState
        {
            get => _selectedInventoryState;
            set             {
                if (SetProperty(ref _selectedInventoryState, value))
                {
                    ((RelayCommand)BorrowCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)ReturnCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private string _newInventoryStateBookId;
        public string NewInventoryStateBookId
        {
            get => _newInventoryStateBookId;
            set
            {
                if (SetProperty(ref _newInventoryStateBookId, value))
                    ((RelayCommand)AddInventoryStateCommand).RaiseCanExecuteChanged();
            }
        }

        private string _newInventoryStateAvailable;
        public string NewInventoryStateAvailable
        {
            get => _newInventoryStateAvailable;
            set
            {
                if (SetProperty(ref _newInventoryStateAvailable, value))
                    ((RelayCommand)AddInventoryStateCommand).RaiseCanExecuteChanged();
            }
        }

        private string _deleteInventoryStateId;
        public string DeleteInventoryStateId
        {
            get => _deleteInventoryStateId;
            set
            {
                if (SetProperty(ref _deleteInventoryStateId, value))
                    ((RelayCommand)DeleteInventoryStateCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddInventoryStateCommand { get; }
        public ICommand DeleteInventoryStateCommand { get; }
        public ICommand BorrowCommand { get; }
        public ICommand ReturnCommand { get; }

        public InventoryStatesViewModel(ILibraryService service)
        {
            _service = service;
            LoadInventoryStates();

            AddInventoryStateCommand = new RelayCommand(AddInventoryState, CanAddInventoryState);
            DeleteInventoryStateCommand = new RelayCommand(DeleteInventoryState, CanDeleteInventoryState);
            BorrowCommand = new RelayCommand(BorrowCommandDo, () => SelectedInventoryState != null);
            ReturnCommand = new RelayCommand(ReturnCommandDo, () => SelectedInventoryState != null);
        }
        private void BorrowCommandDo()
        {
            _service.BorrowBook(SelectedInventoryState.Id,1);
            LoadInventoryStates();
        }
        private void ReturnCommandDo()
        {
            _service.ReturnBook(SelectedInventoryState.Id,1);
            LoadInventoryStates();
        }
        private void LoadInventoryStates()
        {
            var serviceInventoryStates = _service.getAllInventoryStates();
            InventoryStates = new ObservableCollection<InventoryStateModel>(
                serviceInventoryStates.ConvertAll(b => new InventoryStateModel
                {
                    Id = b.book_id,
                    Available = b.AvailableCopies
                }));
        }

        private bool CanAddInventoryState() =>
            int.TryParse(NewInventoryStateBookId, out int id) && id > 0 &&
            int.TryParse(NewInventoryStateAvailable, out int available) && available >= 0;


        private void AddInventoryState()
        {
            int id = int.Parse(NewInventoryStateBookId);
            int available = int.Parse(NewInventoryStateAvailable);

            _service.AddInventoryState(id, available);
            LoadInventoryStates();

            NewInventoryStateAvailable = "";
        }

        private bool CanDeleteInventoryState() => int.TryParse(DeleteInventoryStateId, out int id) && id > 0;

        private void DeleteInventoryState()
        {
            if (int.TryParse(DeleteInventoryStateId, out int id))
            {
                _service.RemoveInventoryState(id);
                LoadInventoryStates();
                DeleteInventoryStateId = "";
            }
        }
    }
}