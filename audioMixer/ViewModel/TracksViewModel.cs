using audioMixer.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;
using System.Windows.Input;

namespace audioMixer.ViewModel
{
    public class TracksViewModel
    {
        private TracksModel model;
        public TracksViewModel()
        {
            TrackList = new ObservableCollection<TrackViewModel>();
            model = new TracksModel();
        }

        public ObservableCollection<TrackViewModel> TrackList { get; private set; }

        private void copyTracks()
        {
            TrackList.CollectionChanged -= modelSync;
            TrackList.Clear();
            foreach (TrackModel track in model)
            {
                TrackList.Add(new TrackViewModel(track));
            }
            TrackList.CollectionChanged += modelSync;
        }

        private void modelSync(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    TrackViewModel newTrack = (TrackViewModel)e.NewItems[0];
                    if (newTrack != null)
                    {
                        model.AddTrack(newTrack.getModel());
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    TrackViewModel deletedTrack = (TrackViewModel)e.OldItems[0];
                    if (deletedTrack != null)
                    {
                        model.RemoveTrack(deletedTrack.getModel());
                    }
                    break;
            }
        }

        private ICommand addTrack;
        public ICommand AddTrack
        {
            get
            {
                if (addTrack == null)
                {
                    addTrack = new RelayCommand(
                        o =>
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = "WAV files (*.wav)|*.wav";
                            openFileDialog.RestoreDirectory = true;

                            if (openFileDialog.ShowDialog() == true)
                            {
                                TrackModel track = new TrackModel(openFileDialog.FileName);
                                model.AddTrack(track);
                                copyTracks();
                            }
                        }
                    );
                }
                return addTrack;
            }
        }

        private ICommand removeTrack;
        public ICommand RemoveTrack
        {
            get
            {
                if (removeTrack == null)
                {
                    removeTrack = new RelayCommand(
                        o =>
                        {
                            (o as TrackViewModel).StopTrack.Execute(o);
                            TrackList.Remove((TrackViewModel)o);
                            copyTracks();
                        }
                    );
                }
                return removeTrack;
            }
        }

        private ICommand playAll;
        public ICommand PlayAll
        {
            get
            {
                if (playAll == null)
                {
                    playAll = new RelayCommand(
                        o =>
                        {
                            foreach (TrackViewModel track in TrackList)
                            {
                                track.PlayTrack.Execute(o);
                            }
                        }
                    );
                }
                return playAll;
            }
        }

        private ICommand stopAll;
        public ICommand StopAll
        {
            get
            {
                if (stopAll == null)
                {
                    stopAll = new RelayCommand(
                        o =>
                        {
                            foreach (TrackViewModel track in TrackList)
                            {
                                track.StopTrack.Execute(o);
                            }
                        }
                    );
                }
                return stopAll;
            }
        }
    }
}
