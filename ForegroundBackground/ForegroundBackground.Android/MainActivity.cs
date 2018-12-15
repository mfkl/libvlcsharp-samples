using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using LibVLCSharp.Forms.Shared;

namespace ForegroundBackground.Droid
{
    [Activity(Label = "ForegroundBackground", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            LibVLCSharpFormsRenderer.Init();
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        protected override void OnPause()
        {
            base.OnPause();

            MessagingCenter.Send("app", "OnPause");

        }

        protected override void OnRestart()
        {
            base.OnRestart();

            MessagingCenter.Send("app", "OnRestart");
        }
    }
}