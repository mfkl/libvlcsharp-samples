
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LocalNetwork.Models;
using LocalNetwork.ViewModels;
using LibVLCSharp.Shared;

namespace LocalNetwork.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        public ItemsPage(Item item)
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel(item);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            if(item.IsDirectory)
                await Navigation.PushAsync(new ItemsPage(item));
            else await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}