using LibVLCSharp.Shared;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;

using Xamarin.Forms;
using System;

namespace Chromecast
{
    public partial class MainPage : ContentPage
	{
        readonly HashSet<RendererItem> _rendererItems = new HashSet<RendererItem>();
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;
        RendererDiscoverer _rendererDiscoverer;

        public MainPage()
		{
			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // start chromecast discovery
            DiscoverChromecasts();

            // hold on a bit at first to give libvlc time to find the chromecast
            await Task.Delay(2000);

            // start casting if any renderer found
            StartCasting();
        }

        /// <summary>
        /// This is the method that starts the playback on the chromecast
        /// </summary>
        private void StartCasting()
        {
            // abort casting if no renderer items were found
            if (!_rendererItems.Any())
            {
                WriteLine("No renderer items found. Abort casting...");
                return;
            }

            // create new media
            var media = new Media(_libVLC, 
                "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4", 
                Media.FromType.FromLocation);

            // create the mediaplayer
            _mediaPlayer = new MediaPlayer(_libVLC);

            // set the previously discovered renderer item (chromecast) on the mediaplayer
            // if you set it to null, it will start to render normally (i.e. locally) again
            _mediaPlayer.SetRenderer(_rendererItems.First());

            // start the playback
            _mediaPlayer.Play(media);
        }

        void DiscoverChromecasts()
        {
            // load native libvlc libraries
            Core.Initialize();

            // create core libvlc object
            _libVLC = new LibVLC();

            string rendererName;

            // choose the correct service discovery protocol depending on the host platform
            // Apple platforms use the Bonjour protocol
            if (Device.RuntimePlatform == Device.iOS)
                rendererName = _libVLC.RendererList.Select(renderer => renderer.Name).FirstOrDefault(name => name.Equals("Bonjour_renderer"));
            else if (Device.RuntimePlatform == Device.Android)
                rendererName = _libVLC.RendererList.Select(renderer => renderer.Name).FirstOrDefault(name => name.Equals("microdns_renderer"));
            else throw new PlatformNotSupportedException("Only Android and iOS are currently supported in this sample");

            // create a renderer discoverer
            _rendererDiscoverer = new RendererDiscoverer(_libVLC, rendererName);

            // register callback when a new renderer is found
            _rendererDiscoverer.ItemAdded += RendererDiscoverer_ItemAdded;

            // start discovery on the local network
            var r = _rendererDiscoverer.Start();
        }

        /// <summary>
        /// Raised when a renderer has been discovered or has been removed
        /// </summary>
        void RendererDiscoverer_ItemAdded(object sender, RendererDiscovererItemAddedEventArgs e)
        {
            WriteLine($"New item discovered: {e.RendererItem.Name} of type {e.RendererItem.Type}");
            if (e.RendererItem.CanRenderVideo)
                WriteLine("Can render video");
            if (e.RendererItem.CanRenderAudio)
                WriteLine("Can render audio");

            // add newly found renderer item to local collection
            _rendererItems.Add(e.RendererItem);
        }    
    }
}