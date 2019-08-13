using Xamarin.Forms;

namespace MediaElement
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnAppearing(object sender, System.EventArgs e)
        {
            base.OnAppearing();
            ((MainViewModel)BindingContext).OnAppearing();
        }
    }
}
