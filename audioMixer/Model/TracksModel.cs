using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audioMixer.Model
{
    public class TracksModel : IEnumerable<TrackModel>
    {
        private List<TrackModel> trackList = new List<TrackModel>();

        public void AddTrack(TrackModel track)
        {
            trackList.Add(track);
        }

        public bool RemoveTrack(TrackModel track)
        {
            return trackList.Remove(track);
        }

        public int TracksCount()
        {
            return trackList.Count;
        }

        public TrackModel this[int index]
        {
            get 
            { 
                return trackList[index]; 
            }
        }

        public IEnumerator<TrackModel> GetEnumerator()
        {
            return trackList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }
    }
}
