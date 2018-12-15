using LibVLCSharp.Forms.Shared;
using LibVLCSharp.Shared;
using System.Diagnostics;
using Xamarin.Forms;

namespace ForegroundBackground
{
    public partial class MainPage : ContentPage
    {
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;
        VideoView _videoView;
        float _position;

        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<string>(this, "OnPause", app =>
            {
                VideoView.Loaded -= VideoView_Loaded;
                _mediaPlayer.Pause();
                _position = _mediaPlayer.Position;
                _mediaPlayer.Stop();
                MainGrid.Children.Clear();
                Debug.WriteLine($"saving mediaplayer position {_position}");
            });

            MessagingCenter.Subscribe<string>(this, "OnRestart", app =>
            {
                _videoView = new VideoView { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
                MainGrid.Children.Add(_videoView);

                _videoView.Loaded += VideoView_Loaded;

                _videoView.MediaPlayer = _mediaPlayer;
                _videoView.MediaPlayer.Position = _position;
                _position = 0;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            VideoView.Loaded += VideoView_Loaded;

            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC)
            {
                Media = new Media(_libVLC,
                "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4",
                Media.FromType.FromLocation)
            };

            VideoView.MediaPlayer = _mediaPlayer;
        }

        private void VideoView_Loaded(object sender, System.EventArgs e)
        {
            _mediaPlayer.Play();
        }
    }
}