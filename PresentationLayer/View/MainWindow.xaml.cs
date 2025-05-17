using System.Windows;
using LibraryService.API;
using PresentationLayer.ViewModel;
using LibraryService;
using LibraryData;
using PresentationLayer.Model;
namespace PresentationLayer.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new ModelService());
        }

    }
}
