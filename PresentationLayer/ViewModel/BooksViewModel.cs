using LibraryService.API;
using PresentationLayer.Model;
using PresentationLayer.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace PresentationLayer.ViewModel
{
    public class BooksViewModel : ViewModelBase
    {
        private readonly ILibraryService _service;

        private ObservableCollection<BookModel> _books;
        public ObservableCollection<BookModel> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        private BookModel _selectedBook;
        public BookModel SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (SetProperty(ref _selectedBook, value))
                {
                    if(_selectedBook != null)
                        {
                            NewBookTitle = _selectedBook.Title;
                            NewBookAuthor = _selectedBook.Author;
                            NewBookGenre = _selectedBook.Genre;
                        }
                    else
                    {
                        NewBookTitle = "";
                        NewBookAuthor = "";
                        NewBookGenre = "";
                    }
                }
            }
        }

        private string _newBookTitle;
        public string NewBookTitle
        {
            get => _newBookTitle;
            set
            {
                if (SetProperty(ref _newBookTitle, value))
                    ((RelayCommand)AddBookCommand).RaiseCanExecuteChanged();
            }
        }

        private string _newBookAuthor;
        public string NewBookAuthor
        {
            get => _newBookAuthor;
            set
            {
                if (SetProperty(ref _newBookAuthor, value))
                    ((RelayCommand)AddBookCommand).RaiseCanExecuteChanged();
            }
        }

        private string _newBookGenre;
        public string NewBookGenre
        {
            get => _newBookGenre;
            set
            {
                if (SetProperty(ref _newBookGenre, value))
                    ((RelayCommand)AddBookCommand).RaiseCanExecuteChanged();
            }
        }

        private string _deleteBookId;
        public string DeleteBookId
        {
            get => _deleteBookId;
            set
            {
                if (SetProperty(ref _deleteBookId, value))
                    ((RelayCommand)DeleteBookCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand DeleteBookCommand { get; }

        public BooksViewModel(ILibraryService service)
        {
            _service = service;
            LoadBooks();

            AddBookCommand = new RelayCommand(AddBook, CanAddBook);
            DeleteBookCommand = new RelayCommand(DeleteBook, CanDeleteBook);
        }

        private void LoadBooks()
        {
            var serviceBooks = _service.getAllBooks();
            Books = new ObservableCollection<BookModel>(
                serviceBooks.ConvertAll(b => new BookModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre
                }));
        }

        private bool CanAddBook() =>
            !string.IsNullOrWhiteSpace(NewBookTitle) &&
            !string.IsNullOrWhiteSpace(NewBookAuthor) &&
            !string.IsNullOrWhiteSpace(NewBookGenre);

        private void AddBook()
        {
            _service.AddBook(NewBookTitle, NewBookAuthor, NewBookGenre);
            LoadBooks();

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
}