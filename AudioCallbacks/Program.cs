using System;
using NAudio.Wave;
using LibVLCSharp.Shared;
using System.Runtime.InteropServices;

namespace AudioCallbacksSample
{
    class Program
    {
        // This sample shows you how you can use SetAudioFormatCallback and SetAudioCallbacks. It does two things:
        // 1) Play the sound from the specified video using NAudio
        // 2) Extract the sound into a file using NAudio

        static void Main(string[] args)
        {
            Core.Initialize();

            using var libVLC = new LibVLC(enableDebugLogs: true);
            using var media = new Media(libVLC,
                new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"),
                ":no-video");
            using var mediaPlayer = new MediaPlayer(media);

            using var outputDevice = new WaveOutEvent();
            var waveFormat = new WaveFormat(8000, 16, 1);
            var writer = new WaveFileWriter("sound.wav", waveFormat);
            var waveProvider = new BufferedWaveProvider(waveFormat);
            outputDevice.Init(waveProvider);

            mediaPlayer.SetAudioFormatCallback(AudioSetup, AudioCleanup);
            mediaPlayer.SetAudioCallbacks(PlayAudio, PauseAudio, ResumeAudio, FlushAudio, DrainAudio);

            mediaPlayer.Play();
            mediaPlayer.Time = 20_000; // Seek the video 20 seconds
            outputDevice.Play();

            Console.WriteLine("Press 'q' to quit. Press any other key to pause/play.");
            while (true)
            {
                if (Console.ReadKey().KeyChar == 'q')
                    break;

                if (mediaPlayer.IsPlaying)
                    mediaPlayer.Pause();
                else
                    mediaPlayer.Play();
            }

            void PlayAudio(IntPtr data, IntPtr samples, uint count, long pts)
            {
                int bytes = (int)count * 2; // (16 bit, 1 channel)
                var buffer = new byte[bytes];
                Marshal.Copy(samples, buffer, 0, bytes);

                waveProvider.AddSamples(buffer, 0, bytes);
                writer.Write(buffer, 0, bytes);
            }

            int AudioSetup(ref IntPtr opaque, ref IntPtr format, ref uint rate, ref uint channels)
            {
                channels = (uint)waveFormat.Channels;
                rate = (uint)waveFormat.SampleRate;
                return 0;
            }

            void DrainAudio(IntPtr data)
            {
                writer.Flush();
            }

            void FlushAudio(IntPtr data, long pts)
            {
                writer.Flush();
                waveProvider.ClearBuffer();
            }

            void ResumeAudio(IntPtr data, long pts)
            {
                outputDevice.Play();
            }

            void PauseAudio(IntPtr data, long pts)
            {
                outputDevice.Pause();
            }

            void AudioCleanup(IntPtr opaque) { }
        }
    }
}
