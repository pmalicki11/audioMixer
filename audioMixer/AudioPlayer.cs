﻿using System;
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
        public static extern uint waveOutSetVolume(IntPtr mediaName, uint newVolume);

        public AudioPlayer(string fileName, string mediaName)
        {
            FileName = fileName;
            MediaName = mediaName;
            Paused = false;
            message = new StringBuilder(128);
            returnData = new StringBuilder(128);
            OpenFile();
        }

        public string FileName { get; private set; }

        public string MediaName { get; private set; }

        public bool Paused { get; private set; }

        public bool OpenFile()
        {
            
            CloseFile();
            command = "open \"" + FileName + "\" type waveaudio alias " + MediaName;
            error = mciSendString(command, null, 0, IntPtr.Zero);
            if (error == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CloseFile()
        {
            command = "close " + MediaName;
            mciSendString(command, null, 0, IntPtr.Zero);
        }
        
        public bool Play()
        {
            if (OpenFile())
            {
                command = "play " + MediaName;
                error = mciSendString(command, null, 0, IntPtr.Zero);
                if (error == 0)
                {
                    return true;
                }
                else
                {
                    CloseFile();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Pause()
        {
            if (Paused)
            {
                Resume();
                Paused = false;
            }
            else if (isPlaying())
            {
                command = "pause " + MediaName;
                mciSendString(command, null, 0, IntPtr.Zero);
                Paused = true;
            }
        }

        public void Stop()
        {
            command = "stop " + MediaName;
            mciSendString(command, null, 0, IntPtr.Zero);
            Paused = false;
            CloseFile();
        }

        public void Resume()
        {
            command = "resume " + MediaName;
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public bool isOpen()
        {
            command = "status " + MediaName + " mode";
            mciSendString(command, returnData, 128, IntPtr.Zero);
            //if (returnData.Length == 4 && returnData.ToString().Substring(0, 4) == "open")
            if (returnData.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isPlaying()
        {
            command = "status " + MediaName + " mode";
            mciSendString(command, returnData, 128, IntPtr.Zero);
            if (returnData.Length == 7 && returnData.ToString().Substring(0, 7) == "playing")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isPaused()
        {
            command = "status " + MediaName + " mode";
            mciSendString(command, returnData, 128, IntPtr.Zero);
            if (returnData.Length == 6 && returnData.ToString().Substring(0, 6) == "paused")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isStopped()
        {
            command = "status " + MediaName + " mode";
            mciSendString(command, returnData, 128, IntPtr.Zero);
            if (returnData.Length == 7 && returnData.ToString().Substring(0, 7) == "stopped")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getLength()
        {
            if (isOpen())
            {
                command = "status " + MediaName + " length";
                mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
                return int.Parse(returnData.ToString());
            }
            else
            {
                return 0;
            }
        }

        public int getPosition()
        {
            if (isOpen())
            {
                command = "status " + MediaName + " position";
                mciSendString(command, returnData, returnData.Capacity, IntPtr.Zero);
                return int.Parse(returnData.ToString());
            }
            else
            {
                return 0;
            }
        }

        public void setPosition(int newPosition)
        {
            if (isPlaying())
            {
                command = "play " + MediaName + " from " + newPosition.ToString();
                mciSendString(command, null, 0, IntPtr.Zero);
            }
            else
            {
                command = "seek " + MediaName + " to " + newPosition.ToString();
                mciSendString(command, null, 0, IntPtr.Zero);
            }
        }

        public bool setVolume(int volume)
        {
            if (volume >= 0 && volume <= 1000)
            {
                command = "setaudio " + MediaName + " volume to " + volume.ToString();
                mciSendString(command, null, 0, IntPtr.Zero);
                
                double newVolume = ushort.MaxValue * volume / 100;
                uint v = ((uint)newVolume) & 0xffff;
                uint vAll = v | (v << 16);
                waveOutSetVolume(IntPtr.Zero, vAll);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool setBalance(int balance)
        {
            if (balance >= 0 && balance <= 1000)
            {
                command = "setaudio " + MediaName + " left volume to " + (1000 - balance).ToString();
                mciSendString(command, null, 0, IntPtr.Zero);
                command = "setaudio " + MediaName + " right volume to " + balance.ToString();
                mciSendString(command, null, 0, IntPtr.Zero);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}