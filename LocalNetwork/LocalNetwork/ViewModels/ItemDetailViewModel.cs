using LibVLCSharp.Shared;
using LocalNetwork.Models;

namespace LocalNetwork.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        readonly Item _item;

        public ItemDetailViewModel(Item item)
        {
            _item = item;

            _mediaPlayer = new MediaPlayer(item.Media);
        }

        private MediaPlayer _mediaPlayer;
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => SetProperty(ref _mediaPlayer, value);
        }

        public void Play()
        {
            MediaPlayer = _mediaPlayer;
            MediaPlayer.Play();
        }

        public void Stop()
        {
            MediaPlayer.Stop();
            MediaPlayer.Dispose();
            _item.Media.Dispose();
        }
    }
}