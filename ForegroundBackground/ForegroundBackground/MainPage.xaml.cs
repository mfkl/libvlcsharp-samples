using LibVLCSharp.Forms.Shared;
using LibVLCSharp.Shared;
using System;
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
                VideoView.MediaPlayerChanged -= MediaPlayerChanged;
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

                _videoView.MediaPlayerChanged += MediaPlayerChanged;

                _videoView.MediaPlayer = _mediaPlayer;
                _videoView.MediaPlayer.Position = _position;
                _position = 0;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            VideoView.MediaPlayerChanged += MediaPlayerChanged;

            Core.Initialize();

            _libVLC = new LibVLC();
            using var media = new Media(_libVLC, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"));

            _mediaPlayer = new MediaPlayer(_libVLC)
            {
                Media = media
            };

            VideoView.MediaPlayer = _mediaPlayer;
        }

        private void MediaPlayerChanged(object sender, EventArgs e)
        {
            _mediaPlayer.Play();
        }
    }
}