using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LibVLCFadeAnimation.Environments;
using LibVLCFadeAnimation.ViewModels;
using LibVLCFadeAnimation.Views;
using System.Runtime.InteropServices;

namespace LibVLCFadeAnimation
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                EnvironmentInitialization();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
                desktop.Exit += (_, __) => EnvironmentCleanup();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static void EnvironmentInitialization()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxApi.Initialize();
            }
        }

        private static void EnvironmentCleanup()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxApi.Shutdown();
            }
        }
    }
}