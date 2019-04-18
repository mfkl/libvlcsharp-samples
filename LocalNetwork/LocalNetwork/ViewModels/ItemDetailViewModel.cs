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
            MediaPlayer = new MediaPlayer(item.Media);
            MediaPlayer.Play();
        }

        private MediaPlayer _mediaPlayer;
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => SetProperty(ref _mediaPlayer, value);
        }
    }
}