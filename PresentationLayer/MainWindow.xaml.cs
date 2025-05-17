using System.Windows;
using LibraryService.API;
using PresentationLayer.ViewModel;
using LibraryService;
using LibraryData;
namespace PresentationLayer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ILibraryService service = new LibraryServiceImp(new DatabaseDataStorage("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\GniewkoPC\\Desktop\\PT\\PT\\LibraryData\\Database1.mdf; Integrated Security = True"));
            DataContext = new BooksViewModel(service);
            //service.AddBook("Test Book", "Test Author", "Test Genre");
            //service.AddBook("Test Book2", "Test Author2", "Test Genre2");
            //service.ClearAll();
        }

    }
}
