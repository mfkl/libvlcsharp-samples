using Xamarin.Forms;

namespace Gestures
{
    public partial class PlayerControl : ContentPage
    {
        MainViewModel _vm;

        public PlayerControl()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            videoView.MediaPlayerChanged += MediaPlayerChanged;

            _vm = BindingContext as MainViewModel;
            _vm.Initialize();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            videoView.MediaPlayerChanged -= MediaPlayerChanged;

            _vm = BindingContext as MainViewModel;
            _vm.Stop();
        }

        private void MediaPlayerChanged(object sender, System.EventArgs e)
        {
            _vm.MediaPlayer.Play();
        }
        
        void PanUpdated(object sender, PanUpdatedEventArgs e) => _vm.OnGesture(e);
    }
}