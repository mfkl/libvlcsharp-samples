using LibVLCSharp.Forms.Shared;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MediaElement
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }


        /// <summary>
        /// Called when the application starts.
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
            MessagingCenter.Send(new LifecycleMessage(), nameof(OnStart));
        }

        /// <summary>
        /// Called when the application enters the sleeping state.
        /// </summary>
        protected override void OnSleep()
        {
            base.OnSleep();
            MessagingCenter.Send(new LifecycleMessage(), nameof(OnSleep));
        }

        /// <summary>
        /// Called when the application resumes from the sleeping state.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            MessagingCenter.Send(new LifecycleMessage(), nameof(OnResume));
        }
    }
}
