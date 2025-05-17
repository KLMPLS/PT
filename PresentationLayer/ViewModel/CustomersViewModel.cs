using LibraryService.API;
using PresentationLayer.Model;
using PresentationLayer.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace PresentationLayer.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly ILibraryService _service;

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

        public CustomersViewModel(ILibraryService service)
        {
            _service = service;
            LoadCustomers();

            AddCustomerCommand = new RelayCommand(AddCustomer, CanAddCustomer);
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer, CanDeleteCustomer);
        }

        private void LoadCustomers()
        {
            var serviceCustomers = _service.getAllCustomers();
            Customers = new ObservableCollection<CustomerModel>(
                serviceCustomers.Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email
                }));
        }

        private bool CanAddCustomer() =>
            !string.IsNullOrWhiteSpace(NewCustomerName) &&
            !string.IsNullOrWhiteSpace(NewCustomerEmail);

        private void AddCustomer()
        {
            _service.AddCustomer(NewCustomerName, NewCustomerEmail);
            LoadCustomers();
            NewCustomerName = "";
            NewCustomerEmail = "";
        }

        private bool CanDeleteCustomer() => int.TryParse(DeleteCustomerId, out int id) && id > 0;

        private void DeleteCustomer()
        {
            if (int.TryParse(DeleteCustomerId, out int id))
            {
                _service.RemoveCustomer(id);
                LoadCustomers();
                DeleteCustomerId = "";
            }
        }
    }
}