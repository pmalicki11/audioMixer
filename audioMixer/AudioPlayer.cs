using System;
using System.Runtime.InteropServices;
using System.Text;

namespace audioMixer
{
    public class AudioPlayer
    {

        private string fileName;
        private string mediaName;
        private bool isOpen = false;
        private bool isPlaying = false;
        private bool isPaused = false;
        StringBuilder returnData = new StringBuilder(128);
        long error;

        
        public AudioPlayer(string fileName)
        {
            this.fileName = fileName;
            this.mediaName = fileName;
        }


        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr winHandle);

        private void ClosePlayer()
        {
            if (isOpen)
            {
                string command = "Close " + mediaName;
                mciSendString(command, null, 0, IntPtr.Zero);
                isOpen = false;
                isPlaying = false;
                isPaused = false;
            }
        }

        private void OpenMediaFile()
        {
            ClosePlayer();
            string command = "Open " + fileName + " type waveaudio alias " + mediaName;
            mciSendString(command, null, 0, IntPtr.Zero);
            isOpen = true;
        }

        private void PlayMediaFile()
        {
            if (isOpen)
            {
                string command;

                if (isPlaying)
                {
                    command = "Pause " + mediaName + " notify";
                    mciSendString(command, null, 0, IntPtr.Zero);
                    isPlaying = false;
                    isPaused = true;
                }
                else if (isPaused)
                {
                    command = "Resume " + mediaName + " notify";
                    mciSendString(command, null, 0, IntPtr.Zero);
                    isPlaying = true;
                    isPaused = false;
                }
                else
                {
                    command = "Play " + mediaName + " notify";
                    mciSendString(command, null, 0, IntPtr.Zero);
                    isPlaying = true;
                    isPaused = false;
                }
            }
        }

        public void Play()
        {
            if (!isOpen)
            {
                OpenMediaFile();
            }

            PlayMediaFile();
        }

        public void Stop()
        {
            ClosePlayer();
        }

        public int TrackLength()
        {
            if (!isOpen)
            {
                OpenMediaFile();
            }
            
            string command = "Status " + mediaName + " length";
            error = mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
            if (error != 0)
            {
                int r = int.Parse(returnData.ToString());
                return r;
            }
            else
            {
                return 0;
            }
        }

        public int CurrentPosition()
        {
            string command = "status " + mediaName + " position";
            error = mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
            return int.Parse(returnData.ToString());
        }

        public void SetPosition(int miliseconds)
        {
            if (isPlaying)
            {
                string command = "play " + mediaName + " from " + miliseconds.ToString();
                error = mciSendString(command, null, 0, IntPtr.Zero);
            }
            else
            {
                string command = "seek " + mediaName + " to " + miliseconds.ToString();
                error = mciSendString(command, null, 0, IntPtr.Zero);
            }
        }
    }
}
