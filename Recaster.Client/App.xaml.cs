using System.Windows;

namespace Recaster.Client
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            this.StartupUri = new System.Uri("Views/MainWindow.xaml", System.UriKind.Relative);
        }
    }
}
