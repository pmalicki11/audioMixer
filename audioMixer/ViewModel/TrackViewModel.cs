using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using audioMixer.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace audioMixer.ViewModel
{
    public class TrackViewModel : INotifyPropertyChanged
    {
        private TrackModel model;
        private AudioPlayer player;

        public string FileName
        {
            get
            {
                return model.FileName;
            }
            set
            {
                model.FileName = value;
            }
        }
        
        public string TrackName
        {
            get
            {
                return model.TrackName;
            }
        }

        public string TrackLength
        {
            get
            {
                return player.TrackLength().ToString();
            }
        }

        public int CurrentPosition
        {
            get
            {
                //OnPropertyChanged("CurrentPosition");
                return player.CurrentPosition();
            }
            //set
            //{
            //    player.SetPosition(value);
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TrackViewModel(TrackModel track)
        {
            this.model = track;
            this.player = new AudioPlayer(model.FileName);
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

        private ICommand playTrack;
        public ICommand PlayTrack
        {
            get
            {
                if (playTrack == null)
                {
                    playTrack = new RelayCommand(
                        o =>
                        {
                            player.Play();
                        }
                    );
                }
                return playTrack;
            }
        }

        private ICommand stopTrack;
        public ICommand StopTrack
        {
            get
            {
                if (stopTrack == null)
                {
                    stopTrack = new RelayCommand(
                        o =>
                        {
                            player.Stop();
                        }
                    );
                }
                return stopTrack;
            }
        } 
    }
}
