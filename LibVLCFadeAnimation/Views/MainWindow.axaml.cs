using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Styling;
using Avalonia.Threading;
using Avalonia.VisualTree;
using LibVLCFadeAnimation.Environments;
using LibVLCFadeAnimation.ViewModels;
using System;
using System.Runtime.InteropServices;

namespace LibVLCFadeAnimation.Views
{
    public partial class MainWindow : Window, IDisposable
    {
        private DispatcherTimer FadeTimer { get; set; }
        private IDisposable OpacitySubscription { get; set; }
        private Animation FadeOutAndIn { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // create animation
            FadeOutAndIn = new()
            {
                Duration = TimeSpan.FromSeconds(4),
                Children =
                {
                    new KeyFrame
                    {
                        Cue = new Cue(0),
                        Setters = { new Setter(OpacityProperty, 1.0) }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(0.3),
                        Setters = { new Setter(OpacityProperty, 0.0) }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(0.7),
                        Setters = { new Setter(OpacityProperty, 0.0) }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(1),
                        Setters = { new Setter(OpacityProperty, 1.0) }
                    }
                }
            };

            // subscribe to opacity changed
            OpacitySubscription = this.GetObservable(OpacityProperty).Subscribe(newOpacity =>
            {
                OnVideoOpacityChanged(newOpacity);
            });

            // trigger animation
            FadeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(8)
            };
            FadeTimer.Tick += StartFadeAsync;
            FadeTimer.Start();

            // trigger cleanup
            Closed += (_, _) => Dispose();
        }



        private async void StartFadeAsync(object? sender, EventArgs e)
        {
            await FadeOutAndIn.RunAsync(this);
        }

        private void OnVideoOpacityChanged(double opacity)
        {
            // get handle
            IntPtr hWnd = (this.GetVisualRoot() as TopLevel)?.TryGetPlatformHandle()?.Handle ?? IntPtr.Zero;

            // set opacity
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WinApi.SetWindowOpacity(hWnd, opacity);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxApi.SetWindowOpacity(hWnd, opacity);
            }
            else
            {
                throw new NotSupportedException();
            }
        }



        public void Dispose()
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Dispose();
            }

            FadeTimer.Tick -= StartFadeAsync;
            if (FadeTimer.IsEnabled)
            {
                FadeTimer.Stop();
            }
            OpacitySubscription?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}