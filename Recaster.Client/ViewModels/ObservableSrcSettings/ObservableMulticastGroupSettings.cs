using Recaster.Common;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Recaster.Client.ViewModels.ObservableSrcSettings
{
    public class ObservableMulticastGroupSettings : ObservableElement
    {
        private readonly MulticastGroupSettings _settings;

        public ObservableMulticastGroupSettings(MulticastGroupSettings settings)
        {
            _settings = settings;
            Qualifiers = new ObservableCollection<ObservableQualifierSettings>();
            foreach (var q in _settings.Qualifier)
            {
                var observableQ = new ObservableQualifierSettings(q);
                Qualifiers.Add(observableQ);
            }
            Qualifiers.CollectionChanged += _qualifiers_CollectionChanged;
        }

        private void _qualifiers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                var addedQualifiers = from q in e.NewItems.Cast<ObservableQualifierSettings>()
                                      select new QualifierSettings()
                                      {
                                          SourceIp = q.SourceIp,
                                          SourcePort = q.Port,
                                          Discard = q.Discard
                                      };
                _settings.Qualifier.AddRange(addedQualifiers);
            }
            if (e.OldItems != null)
            {
                var deletedItems = e.OldItems.Cast<ObservableQualifierSettings>();
                _settings.Qualifier.RemoveAll(q => deletedItems.Any(dq => q.SourceIp == dq.SourceIp && 
                                                                          q.SourcePort == dq.Port && 
                                                                          q.Discard == dq.Discard));
            }
        }

        public string Name
        {
            get { return _settings.Name; }
            set
            {
                if (_settings.Name != value)
                {
                    _settings.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string GroupAdreass
        {
            get { return _settings.GroupAdreass; }
            set
            {
                if (_settings.GroupAdreass != value)
                {
                    _settings.GroupAdreass = value;
                    OnPropertyChanged("GroupAdreass");
                }
            }
        }

        public int Port
        {
            get { return _settings.GroupPort; }
            set
            {
                if (_settings.GroupPort != value)
                {
                    _settings.GroupPort = value;
                    OnPropertyChanged("Port");
                }
            }
        }

        public ObservableCollection<ObservableQualifierSettings> Qualifiers { get; }
    }
}
