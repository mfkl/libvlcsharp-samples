using LibVLCSharp.Shared;
using System.Diagnostics;
using Xamarin.Forms;

namespace PulseMusic
{
    public class PlaybackService
    {
        LibVLC _libVLC;
        MediaPlayer _mp;
        const long OFFSET = 5000;

        public PlaybackService()
        {
            Core.Initialize();

            _libVLC = new LibVLC();

            _mp = new MediaPlayer(_libVLC);
        }

        public void Init()
        {
            _mp.Media = new Media(_libVLC, "https://streams.videolan.org/streams/mp3/05-Mr.%20Zebra.mp3", Media.FromType.FromLocation);

            _mp.Media.AddOption(":no-video");

            _mp.Media.Parse();

            var artist = _mp.Media.Meta(Media.MetadataType.Artist);

            _mp.TimeChanged += TimeChanged;
            _mp.PositionChanged += PositionChanged;
            _mp.LengthChanged += LengthChanged;
            _mp.EndReached += EndReached;
            _mp.Playing += Playing;
            _mp.Paused += Paused;

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