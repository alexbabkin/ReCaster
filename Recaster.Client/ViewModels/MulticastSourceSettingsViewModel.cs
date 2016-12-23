﻿using Recaster.Common;
using Recaster.Client.SettingsProvider;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Recaster.Client.ViewModels.ObservableSrcSettings;
using System.Collections.Specialized;
using System.Windows.Input;
using System;
using Recaster.Client.Utility;

namespace Recaster.Client.ViewModels
{
    public class MulticastSourcesSettingsViewModel : ObservableElement, ISettingsPageViewModel
    {
        private ObservableCollection<ObservableMulticastGroupSettings> _settings;
        private ObservableMulticastGroupSettings _selectedSource;

        private void LoadCommand()
        {
            AddSourceCommand = new CustomCommand(AddSource, CanAddSource);
            DeleteSourceCommand = new CustomCommand(DeleteSource, CanDeleteSource);
        }

        private bool CanDeleteSource(object obj)
        {
            return _selectedSource != null;
        }

        private void DeleteSource(object obj)
        {
            _settings.Remove(_selectedSource);
            SelectedSource = null;
        }

        private void AddSource(object obj)
        {
            var newSource = new MulticastGroupSettings()
            {
                Name = "Name",
                GroupAdreass = "::1",
                Port = 0,
                Qualifier = new List<QualifierSettings>()
            };
            _settings.Add(new ObservableMulticastGroupSettings(newSource));
        }

        private bool CanAddSource(object obj)
        {
            return true;
        }

        public MulticastSourcesSettingsViewModel(IProvider settingsProvider)
        {
            _settings = new ObservableCollection<ObservableMulticastGroupSettings>();
            var sourcesSettings = settingsProvider.GetMulticastSourceSettings();
            
            foreach (var sourceSetting in sourcesSettings)
            {
                var s = new ObservableMulticastGroupSettings(sourceSetting);
                _settings.Add(s);
            }
            LoadCommand();
        }

        public string Title
        {
            get { return "Multicast Sources"; }
        }

        public ObservableCollection<ObservableMulticastGroupSettings> Settings
        {
            get { return _settings; }
        }

        public ObservableMulticastGroupSettings SelectedSource
        {
            get { return _selectedSource; }
            set
            {
                if (_selectedSource != value)
                {
                    _selectedSource = value;
                    OnPropertyChanged("SelectedSource");
                }
            }
        }

        public ICommand AddSourceCommand { get; set; }
        public ICommand DeleteSourceCommand { get; set; }
    }    
}
