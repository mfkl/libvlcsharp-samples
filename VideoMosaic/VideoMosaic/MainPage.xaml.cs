using Xamarin.Forms;
using LibVLCSharp.Shared;
using System;

namespace VideoMosaic
{
    public partial class MainPage : ContentPage
    {
        const string VIDEO_URL = "rtsp://184.72.239.149/vod/mp4:BigBuckBunny_175k.mov";
        readonly LibVLC _libvlc;

        public MainPage()
        {
            InitializeComponent();

            // this will load the native libvlc library (if needed, depending on the platform).
            Core.Initialize();

            // instanciate the main libvlc object
            _libvlc = new LibVLC();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // create mediaplayer objects,
            // attach them to their respective VideoViews
            // create media objects and start playback

            VideoView0.MediaPlayer = new MediaPlayer(_libvlc);
            using(var media = new Media(_libvlc, new Uri(VIDEO_URL)))
                VideoView0.MediaPlayer.Play(media);

            VideoView1.MediaPlayer = new MediaPlayer(_libvlc);
            using (var media = new Media(_libvlc, new Uri(VIDEO_URL)))
                VideoView1.MediaPlayer.Play(media);

            VideoView2.MediaPlayer = new MediaPlayer(_libvlc);
            using (var media = new Media(_libvlc, new Uri(VIDEO_URL)))
                VideoView2.MediaPlayer.Play(media);

            VideoView3.MediaPlayer = new MediaPlayer(_libvlc);
            using (var media = new Media(_libvlc, new Uri(VIDEO_URL)))
                VideoView3.MediaPlayer.Play(media);
        }
    }
}