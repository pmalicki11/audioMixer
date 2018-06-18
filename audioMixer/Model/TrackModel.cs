using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audioMixer.Model
{
    public class TrackModel
    {
        public string FileName { get; set; }
        public string TrackName {get; private set; }

        public TrackModel(string fileName)
        {
            this.FileName = fileName;
            getTrackNameFromPath();
        }

        private void getTrackNameFromPath()
        {
            string trackName = "no name";
            if (FileName.Length > 0)
            {
                trackName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
            }
            TrackName = trackName;
        }

    }
}
