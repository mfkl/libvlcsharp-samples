using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using LocalNetwork.Models;
using LibVLCSharp.Shared;
using System.Collections.Generic;
using System.Linq;

namespace LocalNetwork.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        LibVLC _libVLC;
        List<MediaDiscoverer> _mediaDiscoverers = new List<MediaDiscoverer>();
        Item _directory;

        /// <summary>
        /// Constructor for initial root screen
        /// </summary>
        public ItemsViewModel()
        {
            Title = "Local Network";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            InitializeLibVLC();

            InitializeMediaDiscoverers();
        }

        /// <summary>
        /// Directory constructor
        /// </summary>
        /// <param name="item">The current directory</param>
        public ItemsViewModel(Item item)
        {
            _directory = item;
            Title = item.Name;
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteParseItemsCommand());
        }

        async Task<MediaParsedStatus> ExecuteParseItemsCommand()
        {
            // adding previously parsed medias if directory parsed
            foreach (var media in _directory.Media.SubItems)
            {
                Items.Add(new Item(media));
            }

            _directory.Media.SubItems.ItemAdded += MediaList_ItemAdded;
            _directory.Media.SubItems.ItemDeleted += MediaList_ItemDeleted;

            return await _directory.Media.Parse(MediaParseOptions.ParseNetwork);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {                
                DiscoverNetworkShares();

                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void InitializeMediaDiscoverers()
        {
            foreach (var md in _libVLC.MediaDiscoverers(MediaDiscovererCategory.Lan))
            {
                var discoverer = new MediaDiscoverer(_libVLC, md.Name);
                discoverer.MediaList.ItemAdded += MediaList_ItemAdded;
                discoverer.MediaList.ItemDeleted += MediaList_ItemDeleted;
                _mediaDiscoverers.Add(discoverer);
            }
        }

        void MediaList_ItemDeleted(object sender, MediaListItemDeletedEventArgs e)
        {
            var itemToDelete = Items.FirstOrDefault(i => i.Media == e.Media);
            if (itemToDelete != null)
                Items.Remove(itemToDelete);
        }

        void MediaList_ItemAdded(object sender, MediaListItemAddedEventArgs e) => Items.Add(new Item(e.Media));
        
        void InitializeLibVLC()
        {
            Core.Initialize();

            _libVLC = new LibVLC("--verbose=2");
        }

        private void DiscoverNetworkShares() => _mediaDiscoverers.ForEach(md => md.Start());
    }
}