using LibVLCSharp.Shared;
using PulseMusic.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PulseMusic
{
    public class PlaybackService
    {
        LibVLC _libVLC;
        MediaPlayer _mp;
        const string SONG_NAME = "Imagine Dragons - Radioactive.mp3";

        public PlaybackService()
        {
            Core.Initialize();

            _libVLC = new LibVLC();

            _mp = new MediaPlayer(_libVLC);        
        }

        async Task<Stream> SongStream() => await FileSystem.OpenAppPackageFileAsync(SONG_NAME);

        public async void Init()
        {
            _mp.Media = new Media(_libVLC, await SongStream(), ":no-video");

            MessagingCenter.Subscribe<PlayerViewModel, bool>(this, MessengerKeys.Play, (vm, play) =>
            {
                if (play)
                    _mp.Play();
                else _mp.Pause();
            });
        }
    }
}
