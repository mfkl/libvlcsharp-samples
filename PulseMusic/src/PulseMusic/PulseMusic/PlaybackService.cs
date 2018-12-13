using LibVLCSharp.Shared;
using Xamarin.Forms;

namespace PulseMusic
{
    public class PlaybackService
    {
        LibVLC _libVLC;
        MediaPlayer _mp;

        public PlaybackService()
        {
            Core.Initialize();

            _libVLC = new LibVLC();

            _mp = new MediaPlayer(_libVLC);        
        }
        
        public void Init()
        {
            _mp.Media = new Media(_libVLC, "https://archive.org/download/ImagineDragons_201410/imagine%20dragons.mp4", Media.FromType.FromLocation);

            _mp.Media.AddOption(":no-video");

            _mp.PositionChanged += PositionChanged;
            _mp.LengthChanged += LengthChanged;
            _mp.EndReached += EndReached;

            MessagingCenter.Subscribe<string, bool>(MessengerKeys.App, MessengerKeys.Play, (vm, play) =>
            {
                if (play)
                    _mp.Play();
                else _mp.Pause();
            });
        }

        private void EndReached(object sender, System.EventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.EndReached);

        private void LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Length, e.Length);

        private void PositionChanged(object sender, MediaPlayerPositionChangedEventArgs e) =>
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Progress, e.Position);
    }
}
