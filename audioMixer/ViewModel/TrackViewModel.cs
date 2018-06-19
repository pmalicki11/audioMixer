using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using audioMixer.Model;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using System.Timers;

namespace audioMixer.ViewModel
{
    public class TrackViewModel : INotifyPropertyChanged
    {
        private TrackModel model;
        private AudioPlayer player;
        private Timer timer;

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
                return player.Length.ToString();
            }
        }

        public int CurrentPosition
        {
            get
            {
                return player.Position;
            }
            set
            {
                OnPropertyChanged("CurrentPosition");
                player.Position = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TrackViewModel(TrackModel track)
        {
            model = track;
            player = new AudioPlayer(model.FileName, model.TrackName);
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimerTick);
            timer.Interval = 100;
            timer.Enabled = true;
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            CurrentPosition = player.Position;
            OnPropertyChanged("CurrentPosition");
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
                            timer.Start();
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
                            timer.Stop();
                        }
                    );
                }
                return stopTrack;
            }
        } 
    }
}
