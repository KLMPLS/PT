using PresentationLayer.Model;
using PresentationLayer.Model.API;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    internal class InventoryStatesViewModel : ViewModelBase
    {
        private readonly IModelService _service;

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
            set
            {
                if (SetProperty(ref _selectedInventoryState, value))
                {
                    if (_selectedInventoryState != null)
                    {
                        NewInventoryStateBookId = _selectedInventoryState.Id.ToString();
                        NewInventoryStateAvailable = _selectedInventoryState.Available.ToString();
                    }
                    else
                    {
                        NewInventoryStateBookId = "";
                        NewInventoryStateAvailable = "";
                    }
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

        public InventoryStatesViewModel(IModelService service)
        {
            _service = service;
            AddInventoryStateCommand = new RelayCommand(async () => await AddInventoryState(), CanAddInventoryState);
            DeleteInventoryStateCommand = new RelayCommand(async () => await DeleteInventoryState(), CanDeleteInventoryState);
            BorrowCommand = new RelayCommand(async () => await BorrowCommandDo(), () => SelectedInventoryState != null);
            ReturnCommand = new RelayCommand(async () => await ReturnCommandDo(), () => SelectedInventoryState != null);
            _ = LoadInventoryStates();
        }

        private async Task BorrowCommandDo()
        {
            if (SelectedInventoryState != null)
            {
                await _service.BorrowBookAsync(SelectedInventoryState.Id, 1);
                await LoadInventoryStates();
            }
        }

        private async Task ReturnCommandDo()
        {
            if (SelectedInventoryState != null)
            {
                await _service.ReturnBookAsync(SelectedInventoryState.Id, 1);
                await LoadInventoryStates();
            }
        }

        private async Task LoadInventoryStates()
        {
            var serviceInventoryStates = await _service.GetAllInventoryStatesAsync();
            InventoryStates = new ObservableCollection<InventoryStateModel>();
            foreach (IInventoryStateModel b in serviceInventoryStates)
            {
                if (b is InventoryStateModel im)
                    InventoryStates.Add(im);
                else
                    InventoryStates.Add(new InventoryStateModel
                    {
                        Id = b.Id,
                        Available = b.Available
                    });
            }
        }

        private bool CanAddInventoryState() =>
            int.TryParse(NewInventoryStateBookId, out int id) && id > 0 &&
            int.TryParse(NewInventoryStateAvailable, out int available) && available >= 0;

        private async Task AddInventoryState()
        {
            int id = int.Parse(NewInventoryStateBookId);
            int available = int.Parse(NewInventoryStateAvailable);

            await _service.AddInventoryStateAsync(id, available);
            await LoadInventoryStates();

            NewInventoryStateAvailable = "";
        }

        private bool CanDeleteInventoryState() => int.TryParse(DeleteInventoryStateId, out int id) && id > 0;

        private async Task DeleteInventoryState()
        {
            if (int.TryParse(DeleteInventoryStateId, out int id))
            {
                await _service.RemoveInventoryStateAsync(id);
                await LoadInventoryStates();
                DeleteInventoryStateId = "";
            }
        }
    }
}