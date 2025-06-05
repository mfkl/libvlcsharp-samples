using Avalonia.Controls;
using LibVLCSharp.Shared;
using System;

namespace LibVLCFadeAnimation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private readonly LibVLC _libVlc = new();

        public MediaPlayer MediaPlayer { get; }

        public MainWindowViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
        }

        public void Play()
        {
            if (Design.IsDesignMode)
            {
                return;
            }

            using var media = new Media(_libVlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
            MediaPlayer.Play(media);
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        public void Dispose()
        {
            MediaPlayer.Stop();
            MediaPlayer.Dispose();
            _libVlc.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
