using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using ButtonCircle.FormsPlugin.Droid;
using LibVLCSharp.Forms.Shared;

namespace PulseMusic.Droid
{
    [Activity(Label = "PulseMusic", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            LibVLCSharpFormsRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(false);
            ButtonCircleRenderer.Init();
            LoadApplication(new App());
        }
    }
}

