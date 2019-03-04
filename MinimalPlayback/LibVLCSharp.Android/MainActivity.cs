using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using VideoView = LibVLCSharp.Platforms.Android.VideoView;
using LibVLCSharp.Shared;

namespace LibVLCSharp.Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        VideoView _videoView;
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
        }
        
        protected override void OnResume()
        {
            base.OnResume();

            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            _videoView = new VideoView(this) { MediaPlayer = _mediaPlayer };
            AddContentView(_videoView, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
            var media = new Media(_libVLC, "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4", FromType.FromLocation);
            var configuration = new MediaConfiguration();
            configuration.EnableHardwareDecoding();
            media.AddOption(configuration);
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