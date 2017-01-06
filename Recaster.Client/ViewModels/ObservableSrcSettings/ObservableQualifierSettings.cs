using Recaster.Common;

namespace Recaster.Client.ViewModels.ObservableSrcSettings
{
    public class ObservableQualifierSettings : ObservableElement
    {
        private readonly QualifierSettings _qualifier;
        public ObservableQualifierSettings(QualifierSettings qualifier)
        {
            _qualifier = qualifier;
        }

        public string SourceIp
        {
            get { return _qualifier.SourceIp; }
            set
            {
                if (_qualifier.SourceIp != value)
                {
                    _qualifier.SourceIp = value;
                    OnPropertyChanged("SourceIp");
                }
            }
        }

        public int Port
        {
            get { return _qualifier.SourcePort; }
            set
            {
                if (_qualifier.SourcePort != value)
                {
                    _qualifier.SourcePort = value;
                    OnPropertyChanged("Port");
                }
            }
        }

        public bool Discard
        {
            get { return _qualifier.Discard; }
            set
            {
                if (_qualifier.Discard != value)
                {
                    _qualifier.Discard = value;
                    OnPropertyChanged("Discard");
                }
            }
        }        
    }
}
