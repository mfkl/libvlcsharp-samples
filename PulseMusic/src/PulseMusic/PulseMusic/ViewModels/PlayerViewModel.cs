/// Fork from https://github.com/jsuarezruiz/PulseMusic

using PulseMusic.Models;
using PulseMusic.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseMusic.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {    
        private Song _song;
        private string _title;
        private TimeSpan _startTime;
        private TimeSpan _remainTime;
        private bool _isPlaying;
        private double _progress;
        private string _icon;
        private PlaybackService _playbackService;

        public PlayerViewModel()
        {
            _playbackService = DependencyService.Get<PlaybackService>();

            IsPlaying = true;
            Icon = "pause";
        }

        public ICommand PlayCommand => new Command(Play);
        public ICommand RewindCommand => new Command(Rewind);
        public ICommand PreviousCommand => new Command(Previous);
        public ICommand NextCommand => new Command(Next);
        public ICommand ForwardCommand => new Command(Forward);

        public Song Song
        {
            get => _song;
            set => SetProperty(ref _song, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public TimeSpan StartTime
        {
            get => _startTime;
            set => SetProperty(ref _startTime, value);
        }

        public TimeSpan RemainTime
        {
            get => _remainTime;
            set => SetProperty(ref _remainTime, value);
        }

        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private long _length;

        public override Task LoadAsync()
        {
            _playbackService.Init();

            MessagingCenter.Subscribe<string, float>(MessengerKeys.App, MessengerKeys.Position, (app, position) => Progress = position);
       
            MessagingCenter.Subscribe<string, long>(MessengerKeys.App, MessengerKeys.Time, (app, time) =>
            {
                RemainTime = TimeSpan.FromMilliseconds((double)new decimal(_length - time));
                StartTime = TimeSpan.FromMilliseconds((double)new decimal(time));
            });

            MessagingCenter.Subscribe<string, long>(MessengerKeys.App, MessengerKeys.Length, (app, length) => _length = length);
            
            MessagingCenter.Subscribe<string>(MessengerKeys.App, MessengerKeys.EndReached, app => EndReached());
            
            LoadSong();
            
            _playbackService.Play(true);

            return base.LoadAsync();
        }

        void LoadSong()
        {
           
            Song = new Song
            {
                Title = "Imagine Dragons",
                Artist = "Radioactive",
                Cover = "imagine_dragons",
            };

            Title = string.Format("{0} - {1}", Song.Artist, Song.Title);
        }
        
        void EndReached()
        {
            Progress = 0;
            IsPlaying = false;

            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Play, IsPlaying);
        }

        void Play()
        {
            IsPlaying = !IsPlaying;

            if (IsPlaying)
            {
                Icon = "pause";
            }
            else
            {
                Icon = "play";
            }

            _playbackService.Play(IsPlaying);
        }

        void Rewind()
        {
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Rewind);
        }

        void Previous()
        {
            Debug.WriteLine("Previous");
        }

        void Next()
        {
            Debug.WriteLine("Next");
        }

        void Forward()
        {
            MessagingCenter.Send(MessengerKeys.App, MessengerKeys.Forward);
        }
    }
}