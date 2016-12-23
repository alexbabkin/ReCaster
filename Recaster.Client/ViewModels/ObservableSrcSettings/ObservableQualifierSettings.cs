using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recaster.Common;
using System.Windows.Input;

namespace Recaster.Client.ViewModels.ObservableSrcSettings
{
    public class ObservableQualifierSettings : ObservableElement
    {
        private QualifierSettings _qualifier;
        public ObservableQualifierSettings(QualifierSettings qualifier)
        {
            _qualifier = qualifier;
        }

        public string SourceIP
        {
            get { return _qualifier.sourceIP; }
            set
            {
                if (_qualifier.sourceIP != value)
                {
                    _qualifier.sourceIP = value;
                    OnPropertyChanged("SourceIP");
                }
            }
        }

        public int Port
        {
            get { return _qualifier.Port; }
            set
            {
                if (_qualifier.Port != value)
                {
                    _qualifier.Port = value;
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
