using Recaster.Client.ViewModels;
using Recaster.Client.ViewModels.ObservableSrcSettings;
using Recaster.Common;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Recaster.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для MulticastSourcesSettingsView.xaml
    /// </summary>
    public partial class MulticastSourcesSettingsView : UserControl
    {
        public MulticastSourcesSettingsView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var q = new List<QualifierSettings>();
            q.Add(new QualifierSettings() { sourceIP = "::1", Port = 0, Discard = true });
            var s = new MulticastGroupSettings() { Name = "Added", GroupAdreass = "ff3e::ffff:ff01", Port = 57125, Qualifier = q };
            var os = new ObservableMulticastGroupSettings(s);
            (DataContext as MulticastSourcesSettingsViewModel).Settings.Add(os);

            (DataContext as MulticastSourcesSettingsViewModel).SelectedItem.Qualifiers.Remove(
               (DataContext as MulticastSourcesSettingsViewModel).SelectedItem.Qualifiers[0]);
        }
    }
}
