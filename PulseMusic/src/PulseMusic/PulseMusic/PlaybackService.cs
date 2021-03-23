/// Fork from https://github.com/jsuarezruiz/PulseMusic

using LibVLCSharp.Shared;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace PulseMusic
{
    public class PlaybackService
    {
        readonly LibVLC _libVLC;
        readonly MediaPlayer _mp;
        const long OFFSET = 5000;

        public PlaybackService()
        {
            if (DesignMode.IsDesignModeEnabled) return;

            Core.Initialize();

            _libVLC = new LibVLC();

            _mp = new MediaPlayer(_libVLC);
        }

        public void Init()
        {
            // create a libvlc media
            // disable video output, we only need audio
            using (var media = new Media(_libVLC, new Uri("https://archive.org/download/ImagineDragons_201410/imagine%20dragons.mp4"), ":no-video"))
                _mp.Media = media;

            // subscribe to libvlc playback events
            _mp.TimeChanged += TimeChanged;
            _mp.PositionChanged += PositionChanged;
            _mp.LengthChanged += LengthChanged;
            _mp.EndReached += EndReached;
            _mp.Playing += Playing;
            _mp.Paused += Paused;

            // subscribe to UI app events for seeking.

            MessagingCenter.Subscribe<string>(MessengerKeys.App, MessengerKeys.Rewind, vm =>
            {
                Debug.WriteLine("Rewind");
                _mp.Time -= OFFSET;
            });

            MessagingCenter.Subscribe<string>(MessengerKeys.App, MessengerKeys.Forward, vm =>
            {
                Debug.WriteLine("Forward");
                _mp.Time += OFFSET;
            });
        }


        public void Play(bool play)
        {
            if (play)
                _mp.Play();
            else _mp.Pause();
        }

        // when the libvlc mediaplayer events fire, publish an event with the MessagingCenter

        private void PositionChanged(object sender, MediaPlayerPositionChangedEventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Position, e.Position);

        private void Paused(object sender, System.EventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Play, false);

        private void Playing(object sender, System.EventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Play, true);

        private void EndReached(object sender, System.EventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.EndReached);

        private void LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Length, e.Length);

        private void TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Time, e.Time);
    }
}