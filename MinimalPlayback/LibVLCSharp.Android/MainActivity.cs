using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using LibVLCSharp.Shared;
using System;
using VideoView = LibVLCSharp.Platforms.Android.VideoView;

namespace LibVLCSharp.Android
{
    [Activity(Label = "LibVLCSharp.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        VideoView _videoView;
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
        }

        protected override void OnResume()
        {
            base.OnResume();

            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC)
            {
                EnableHardwareDecoding = true
            };

            _videoView = new VideoView(this) { MediaPlayer = _mediaPlayer };
            AddContentView(_videoView, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
            var media = new Media(_libVLC, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
            _videoView.MediaPlayer.Play(media);
        }

        protected override void OnPause()
        {
            base.OnPause();

            _videoView.MediaPlayer.Stop();
            _videoView.Dispose();
        }
    }
}