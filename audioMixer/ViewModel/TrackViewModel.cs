using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using audioMixer.Model;
using System.ComponentModel;

namespace audioMixer.ViewModel
{
    public class TrackViewModel : INotifyPropertyChanged
    {
        private TrackModel model;

        public string TrackName
        {
            get
            {
                return model.TrackName;
            }
            set 
            { 
                model.TrackName = value; 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TrackViewModel(TrackModel track)
        {
            this.model = track;
        }

        private void OnPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        internal TrackModel getModel()
        {
            return model;
        }
    }
}
