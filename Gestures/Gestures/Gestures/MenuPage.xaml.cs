using Xamarin.Forms;

namespace Gestures
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            PlayerControl.Clicked += PlayerControl_Clicked;
            ThreeSixty.Clicked += ThreeSixty_Clicked;
        }

        private void ThreeSixty_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ThreeSixty());
        }

        private void PlayerControl_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PlayerControl());
        }
    }
}
