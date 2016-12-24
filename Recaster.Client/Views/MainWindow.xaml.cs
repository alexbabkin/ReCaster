using Recaster.Client.ViewModels;
using System.Windows;
using Recaster.Client.Dependency;

namespace Recaster.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainVM;
        public MainWindow()
        {
            InitializeComponent();

            // Create UI-friendly wrappers around the 
            // raw data objects (i.e. the view-model).
            mainVM = DependencyResolver.Get<MainViewModel>();

            // Let the UI bind to the view-model.
            DataContext = mainVM;
        }
    }
}
