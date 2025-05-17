using LibraryService.API;
using PresentationLayer.Model;
using PresentationLayer.Model.API;
using PresentationLayer.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    internal class CustomersViewModel : ViewModelBase
    {
        private readonly IModelService _service;

        private ObservableCollection<CustomerModel> _customers;
        public ObservableCollection<CustomerModel> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        private CustomerModel _selectedCustomer;
        public CustomerModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (SetProperty(ref _selectedCustomer, value))
                {
                    if (_selectedCustomer != null)
                    {
                        NewCustomerName = _selectedCustomer.Name;
                        NewCustomerEmail = _selectedCustomer.Email;
                    }
                    else
                    {
                        NewCustomerName = "";
                        NewCustomerEmail = "";
                    }
                }
            }
        }

        private string _newCustomerName;
        public string NewCustomerName
        {
            get => _newCustomerName;
            set
            {
                SetProperty(ref _newCustomerName, value);
                ((RelayCommand)AddCustomerCommand).RaiseCanExecuteChanged();
            }
        }

        private string _newCustomerEmail;
        public string NewCustomerEmail
        {
            get => _newCustomerEmail;
            set
            {
                SetProperty(ref _newCustomerEmail, value);
                ((RelayCommand)AddCustomerCommand).RaiseCanExecuteChanged();
            }
        }

        private string _deleteCustomerId;
        public string DeleteCustomerId
        {
            get => _deleteCustomerId;
            set
            {
                SetProperty(ref _deleteCustomerId, value);
                ((RelayCommand)DeleteCustomerCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }

        public CustomersViewModel(IModelService service)
        {
            _service = service;
            AddCustomerCommand = new RelayCommand(async () => await AddCustomer(), CanAddCustomer);
            DeleteCustomerCommand = new RelayCommand(async () => await DeleteCustomer(), CanDeleteCustomer);
            _ = LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            var serviceCustomers = await _service.GetAllCustomersAsync();
            Customers = new ObservableCollection<CustomerModel>();
            foreach (ICustomerModel c in serviceCustomers)
            {
                if (c is CustomerModel cm)
                    Customers.Add(cm);
                else
                    Customers.Add(new CustomerModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email
                    });
            }
        }

        private bool CanAddCustomer() =>
            !string.IsNullOrWhiteSpace(NewCustomerName) &&
            !string.IsNullOrWhiteSpace(NewCustomerEmail);

        private async Task AddCustomer()
        {
            await _service.AddCustomerAsync(NewCustomerName, NewCustomerEmail);
            await LoadCustomers();
            NewCustomerName = "";
            NewCustomerEmail = "";
        }

        private bool CanDeleteCustomer() => int.TryParse(DeleteCustomerId, out int id) && id > 0;

        private async Task DeleteCustomer()
        {
            if (int.TryParse(DeleteCustomerId, out int id))
            {
                await _service.RemoveCustomerAsync(id);
                await LoadCustomers();
                DeleteCustomerId = "";
            }
        }
    }
}