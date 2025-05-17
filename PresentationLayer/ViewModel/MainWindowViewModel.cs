using LibraryService.API;
using System.Windows.Input;
using PresentationLayer.Model.API;

namespace PresentationLayer.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand ShowBooksViewCommand { get; }
        public ICommand ShowBookRecordsViewCommand { get; }
        public ICommand ShowCustomersViewCommand { get; }
        public ICommand ShowInventoryStateViewCommand { get; }

        private readonly IModelService _service;

        public MainWindowViewModel(IModelService service)
        {
            _service = service;

            ShowBooksViewCommand = new RelayCommand(() => CurrentViewModel = new BooksViewModel(_service));
            ShowBookRecordsViewCommand = new RelayCommand(() => CurrentViewModel = new BookRecordsViewModel(_service));
            ShowCustomersViewCommand = new RelayCommand(() => CurrentViewModel = new CustomersViewModel(_service));
            ShowInventoryStateViewCommand = new RelayCommand(() => CurrentViewModel = new InventoryStatesViewModel(_service));
            // Default view
            CurrentViewModel = new BooksViewModel(_service);
        }
    }
}
