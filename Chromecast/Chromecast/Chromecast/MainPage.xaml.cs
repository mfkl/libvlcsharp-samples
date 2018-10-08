using LibVLCSharp.Shared;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Chromecast
{
    public partial class MainPage : ContentPage
	{
        HashSet<RendererItem> _rendererItems = new HashSet<RendererItem>();
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;
        RendererDiscoverer _rendererDiscoverer;

        public MainPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async () =>
            {
                // start chromecast discovery
                DiscoverChromecasts();

                // hold on a bit
                await Task.Delay(5000);

                // start casting if any renderer found
                StartCasting();
            });
        }

        /// <summary>
        /// This is the method that starts the playback on the chromecast
        /// </summary>
        private void StartCasting()
        {
            // abort casting if no renderer items were found
            if (!_rendererItems.Any()) return;

            // create new media
            var media = new Media(_libVLC, 
                "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4", 
                Media.FromType.FromLocation);

            // create the mediaplayer from the media
            _mediaPlayer = new MediaPlayer(media);

            // set the previously discovered renderer item (chromecast) on the mediaplayer
            _mediaPlayer.SetRenderer(_rendererItems.First());

            // start the playback
            _mediaPlayer.Play(media);
        }

        private void DiscoverChromecasts()
        {
            // load native libvlc libraries
            Core.Initialize();

            // create core libvlc object
            _libVLC = new LibVLC();

            // create a renderer discoverer
            _rendererDiscoverer = new RendererDiscoverer(_libVLC, _libVLC.RendererList[0].Name);

            // register callback when a new renderer is found
            _rendererDiscoverer.ItemAdded += RendererDiscoverer_ItemAdded;

            // start discovery on the local network
            _rendererDiscoverer.Start();
        }

        private void _libVLC_Log(object sender, LogEventArgs e)
        {
            Debug.WriteLine(e.Message);
        }

        /// <summary>
        /// Raised when a renderer has been discovered or has been removed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RendererDiscoverer_ItemAdded(object sender, RendererDiscovererItemAddedEventArgs e)
        {
            Debug.WriteLine($"New item discovered: {e.RendererItem.Name} of type {e.RendererItem.Type}");
            if (e.RendererItem.CanRenderVideo)
                Debug.WriteLine("Can render video");
            if (e.RendererItem.CanRenderAudio)
                Debug.WriteLine("Can render audio");

            // add newly found renderer item to local collection
            _rendererItems.Add(e.RendererItem);
        }    
    }
}