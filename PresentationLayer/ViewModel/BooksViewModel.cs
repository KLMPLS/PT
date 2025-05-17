using LibraryService.API;
using PresentationLayer.Model;
using PresentationLayer.Model.API;
using PresentationLayer.ViewModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    internal class BooksViewModel : ViewModelBase
    {
        private readonly IModelService _service;

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
                    if (_selectedBook != null)
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

        public BooksViewModel(IModelService service)
        {
            _service = service;
            AddBookCommand = new RelayCommand(async () => await AddBook(), CanAddBook);
            DeleteBookCommand = new RelayCommand(async () => await DeleteBook(), CanDeleteBook);
            _ = LoadBooks();
        }

        private async Task LoadBooks()
        {
            var serviceBooks = await _service.GetAllBooksAsync();
            Books = new ObservableCollection<BookModel>();
            foreach (IBookModel b in serviceBooks)
            {
                // If BookModel implements IBookModel, you can cast or use a constructor
                if (b is BookModel bm)
                    Books.Add(bm);
                else
                    Books.Add(new BookModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        Genre = b.Genre
                    });
            }
        }

        private bool CanAddBook() =>
            !string.IsNullOrWhiteSpace(NewBookTitle) &&
            !string.IsNullOrWhiteSpace(NewBookAuthor) &&
            !string.IsNullOrWhiteSpace(NewBookGenre);

        private async Task AddBook()
        {
            await _service.AddBookAsync(NewBookTitle, NewBookAuthor, NewBookGenre);
            await LoadBooks();

            NewBookTitle = "";
            NewBookAuthor = "";
            NewBookGenre = "";
        }

        private bool CanDeleteBook() => int.TryParse(DeleteBookId, out int id) && id > 0;

        private async Task DeleteBook()
        {
            if (int.TryParse(DeleteBookId, out int id))
            {
                await _service.RemoveBookAsync(id);
                await LoadBooks();
                DeleteBookId = "";
            }
        }
    }
}   