using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audioMixer.Model
{
    public class TrackModel
    {
        public string TrackName {get; set; }

        public TrackModel(string trackName)
        {
            this.TrackName = trackName;
        }
    }
}
