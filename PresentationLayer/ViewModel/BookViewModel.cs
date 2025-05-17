using LibraryService.API;
using PresentationLayer.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

public class BooksViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private ObservableCollection<IServiceBook> _books;
    public ObservableCollection<IServiceBook> Books
    {
        get => _books;
        set
        {
            _books = value;
            OnPropertyChanged(nameof(Books));
        }
    }

    private IServiceBook _selectedBook;

    private ILibraryService _service;

    public ICommand UpdateBooksCommand { get; }
    public ICommand AddBookCommand { get; }
    public ICommand DeleteBookCommand { get; }

    public IServiceBook SelectedBook
    {
        get => _selectedBook;
        set
        {
            _selectedBook = value;
            OnPropertyChanged(nameof(SelectedBook));
            // Possibly load detailed view here
        }
    }



    private string _newBookTitle;
    public string NewBookTitle
    {
        get => _newBookTitle;
        set { _newBookTitle = value; 
            OnPropertyChanged(nameof(NewBookTitle)); 
            ((RelayCommand)AddBookCommand).RaiseCanExecuteChanged();
        }
    }

    private string _newBookAuthor;
    public string NewBookAuthor
    {
        get => _newBookAuthor;
        set { _newBookAuthor = value;
            OnPropertyChanged(nameof(NewBookAuthor));
            ((RelayCommand)AddBookCommand).RaiseCanExecuteChanged();
        }

    }

    private string _newBookGenre;
    public string NewBookGenre
    {
        get => _newBookGenre;
        set { _newBookGenre = value; 
            OnPropertyChanged(nameof(NewBookGenre)); 
            ((RelayCommand)AddBookCommand).RaiseCanExecuteChanged();
        }
    }

    // Property for DeleteBookId input
    private string _deleteBookId;
    public string DeleteBookId
    {
        get => _deleteBookId;
        set { _deleteBookId = value; 
            OnPropertyChanged(nameof(DeleteBookId));
            ((RelayCommand)DeleteBookCommand).RaiseCanExecuteChanged();
        }
    }



    public BooksViewModel(ILibraryService service)
    {
        _service = service;
        LoadBooks();

        AddBookCommand = new RelayCommand(AddBook, CanAddBook);
        DeleteBookCommand = new RelayCommand(DeleteBook, CanDeleteBook);
    }

    private void LoadBooks()
    {
        Books = new ObservableCollection<IServiceBook>(_service.getAllBooks());
    }

    private bool CanAddBook() =>
        !string.IsNullOrWhiteSpace(NewBookTitle) &&
        !string.IsNullOrWhiteSpace(NewBookAuthor) &&
        !string.IsNullOrWhiteSpace(NewBookGenre);

    private void AddBook()
    {
        _service.AddBook(NewBookTitle, NewBookAuthor, NewBookGenre);
        LoadBooks();

        // Clear inputs
        NewBookTitle = "";
        NewBookAuthor = "";
        NewBookGenre = "";
    }

    private bool CanDeleteBook() => int.TryParse(DeleteBookId, out int id) && id > 0;

    private void DeleteBook()
    {
        if (int.TryParse(DeleteBookId, out int id))
        {
            _service.RemoveBook(id);
            LoadBooks();

            DeleteBookId = "";
        }
    }
}
