using LibVLCSharp.Shared;
using System.Windows;

namespace LibVLCSharp.WPF.Sample
{
    public partial class MainWindow : Window
    {
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        public MainWindow()
        {
            InitializeComponent();

            videoView.Loaded += VideoView_Loaded;
        }

        void VideoView_Loaded(object sender, RoutedEventArgs e)
        {
            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            videoView.MediaPlayer = _mediaPlayer;

            _mediaPlayer.Play(new Media(_libVLC, "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4", FromType.FromLocation));
        }
    }
}