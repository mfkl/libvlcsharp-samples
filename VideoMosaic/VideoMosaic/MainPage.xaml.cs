using Xamarin.Forms;
using LibVLCSharp.Shared;

namespace VideoMosaic
{
    public partial class MainPage : ContentPage
    {
        const string VIDEO_URL = "rtsp://184.72.239.149/vod/mp4:BigBuckBunny_175k.mov";
        readonly LibVLC _libvlc;

        public MainPage()
        {
            InitializeComponent();

            Core.Initialize();

            _libvlc = new LibVLC();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
      
            VideoView0.MediaPlayer = new MediaPlayer(_libvlc);
            VideoView0.MediaPlayer.Play(new Media(_libvlc, VIDEO_URL, Media.FromType.FromLocation));

            VideoView1.MediaPlayer = new MediaPlayer(_libvlc);
            VideoView1.MediaPlayer.Play(new Media(_libvlc, VIDEO_URL, Media.FromType.FromLocation));

            VideoView2.MediaPlayer = new MediaPlayer(_libvlc);
            VideoView2.MediaPlayer.Play(new Media(_libvlc, VIDEO_URL, Media.FromType.FromLocation));

            VideoView3.MediaPlayer = new MediaPlayer(_libvlc);
            VideoView3.MediaPlayer.Play(new Media(_libvlc, VIDEO_URL, Media.FromType.FromLocation));
        }
    }
}