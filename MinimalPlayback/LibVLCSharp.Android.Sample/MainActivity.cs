using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using VideoView = LibVLCSharp.Platforms.Android.VideoView;
using LibVLCSharp.Shared;
using LibVLCSharp.Android.Sample;

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
            _mediaPlayer = new MediaPlayer(_libVLC) { EnableHardwareDecoding = true };

            _videoView = new VideoView(this) { MediaPlayer = _mediaPlayer };
            AddContentView(_videoView, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
            var media = new Media(_libVLC, "https://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4", FromType.FromLocation);
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