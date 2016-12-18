using Recaster.Client.ViewModels;
using System.Windows;

namespace Recaster.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _settingsTree;
        public MainWindow()
        {
            InitializeComponent();

            // Create UI-friendly wrappers around the 
            // raw data objects (i.e. the view-model).
            _settingsTree = new MainViewModel();

            // Let the UI bind to the view-model.
            DataContext = _settingsTree;
        }
    }
}
