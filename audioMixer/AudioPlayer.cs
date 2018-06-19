using System;
using System.Runtime.InteropServices;
using System.Text;

namespace audioMixer
{

    public class AudioPlayer
    {
        private StringBuilder message;
        private StringBuilder returnData;
        private int error;
        private string command;

        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        public static extern int mciGetErrorString(int errCode, StringBuilder errMsg, int buflen);

        public AudioPlayer(string fileName, string mediaName)
        {
            FileName = fileName;
            MediaName = mediaName;
            IsPlaying = false;
            IsPaused = false;
            IsStopped = false;
            message = new StringBuilder(128);
            returnData = new StringBuilder(128);
            OpenFile();
        }

        //po zakończeniu sprawdzić które właściwości są używane na zewnątrz, jeżeli nie są to zamienić je na pola;
       
        public string FileName { get; private set; }

        public string MediaName { get; private set; }

        public int Length {
            get
            {
                if (IsOpen)
                {
                    command = "Status " + MediaName + " length";
                    error = mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
                    return int.Parse(returnData.ToString());
                }
                return 0;
            }
        
        }

        public bool IsPlaying { get; private set; }

        public bool IsPaused { get; private set; }

        public bool IsStopped { get; private set; }

        public bool IsOpen { get; set;}
        
        public int Position {
            get
            {
                command = "Status " + MediaName + " position";
                error = mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
                if (error == 0 )
                {
                    return int.Parse(returnData.ToString());
                }
                return 0;
            }
            set
            {
                if (IsPlaying)
                {
                    command = "Play " + MediaName + " from " + value.ToString();
                }
                else
                {
                    command = "Seek " + MediaName + " to " + value.ToString();
                }
                error = mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
            }
        }

        public void Play()
        {
            OpenFile();
            command = "Play " + MediaName;
            error = mciSendString(command, null, 0, IntPtr.Zero);
            IsPlaying = true;
            IsPaused = false;
            IsStopped = false;
        }

        public void Pause()
        {
            if (IsOpen)
            {
                command = "Pause " + MediaName;
                error = mciSendString(command, null, 0, IntPtr.Zero);
                IsPlaying = false;
                IsPaused = true;
                IsStopped = false;
            }
        }

        public void Resume()
        {
            if (IsOpen)
            {
                command = "Resume " + MediaName;
                error = mciSendString(command, null, 0, IntPtr.Zero);
                IsPlaying = true;
                IsPaused = false;
                IsStopped = false;
            }
        }

        public void Stop()
        {
            command = "Stop " + MediaName;
            error = mciSendString(command, null, 0, IntPtr.Zero);
            IsPlaying = false;
            IsPaused = false;
            IsStopped = true;
            CloseFile();
        }

        public void OpenFile()
        {
            if (IsOpen)
            {
                CloseFile();
            }
            command = "Open " + FileName + " type waveaudio alias " + MediaName;
            error = mciSendString(command, null, 0, IntPtr.Zero);
            IsOpen = true;
        }

        public void CloseFile()
        {
            command = "Close " + MediaName;
            error = mciSendString(command, null, 0, IntPtr.Zero);
            IsOpen = false;
        }
    }

}
