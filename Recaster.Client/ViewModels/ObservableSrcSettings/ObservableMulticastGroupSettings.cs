using Recaster.Client.Utility;
using Recaster.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Recaster.Client.ViewModels.ObservableSrcSettings
{
    public class ObservableMulticastGroupSettings : ObservableElement
    {
        private MulticastGroupSettings _settings;
        private ObservableCollection<ObservableQualifierSettings> _qualifiers;

        public ObservableMulticastGroupSettings(MulticastGroupSettings settings)
        {
            _settings = settings;
            _qualifiers = new ObservableCollection<ObservableQualifierSettings>();
            foreach (var q in _settings.Qualifier)
            {
                var observableQ = new ObservableQualifierSettings(q);
                _qualifiers.Add(observableQ);
            }
            _qualifiers.CollectionChanged += _qualifiers_CollectionChanged;
        }

        private void _qualifiers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                var addedQualifiers = from q in e.NewItems.Cast<ObservableQualifierSettings>()
                                      select new QualifierSettings()
                                      {
                                          sourceIP = q.SourceIP,
                                          Port = q.Port,
                                          Discard = q.Discard
                                      };
                _settings.Qualifier.AddRange(addedQualifiers);
            }
            if (e.OldItems != null)
            {
                var deletedItems = e.OldItems.Cast<ObservableQualifierSettings>();
                _settings.Qualifier.RemoveAll(q => deletedItems.Any(dq => q.sourceIP == dq.SourceIP && 
                                                                          q.Port == dq.Port && 
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
            get { return _settings.Port; }
            set
            {
                if (_settings.Port != value)
                {
                    _settings.Port = Port;
                    OnPropertyChanged("Port");
                }
            }
        }

        public ObservableCollection<ObservableQualifierSettings> Qualifiers
        {
            get { return _qualifiers; }
        }
    }
}
