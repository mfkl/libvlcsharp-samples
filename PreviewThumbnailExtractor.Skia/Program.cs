using LibVLCSharp.Shared;
using SkiaSharp;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace PreviewThumbnailExtractor.Skia
{
    class Program
    {
        private const uint Width = 320;
        private const uint Height = 180;

        /// <summary>
        /// RGBA is used, so 4 byte per pixel, or 32 bits.
        /// </summary>
        private const uint BytePerPixel = 4;

        /// <summary>
        /// the number of bytes per "line"
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 32.
        /// </summary>
        private static readonly uint Pitch;

        /// <summary>
        /// The number of lines in the buffer.
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 32.
        /// </summary>
        private static readonly uint Lines;

        static Program()
        {
            Pitch = Align(Width * BytePerPixel);
            Lines = Align(Height);

            uint Align(uint size)
            {
                if (size % 32 == 0)
                {
                    return size;
                }

                return ((size / 32) + 1) * 32;// Align on the next multiple of 32
            }
        }

        private static SKBitmap CurrentBitmap;
        private static readonly ConcurrentQueue<SKBitmap> FilesToProcess = new ConcurrentQueue<SKBitmap>();
        private static long FrameCounter = 0;
        static async Task Main(string[] args)
        {
            // Extract thumbnails in the "preview" folder next to the app
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var destination = Path.Combine(currentDirectory, "preview");
            Directory.CreateDirectory(destination);

            // Load native libvlc library
            Core.Initialize();

            using (var libvlc = new LibVLC())
            using (var mediaPlayer = new MediaPlayer(libvlc))
            {
                // Listen to events
                var processingCancellationTokenSource = new CancellationTokenSource();
                mediaPlayer.Stopped += (s, e) => processingCancellationTokenSource.CancelAfter(1);

                // Create new media
                var media = new Media(libvlc, new Uri("http://www.caminandes.com/download/03_caminandes_llamigos_1080p.mp4"));

                media.AddOption(":no-audio");
                // Set the size and format of the video here.
                mediaPlayer.SetVideoFormat("RV32", Width, Height, Pitch);
                mediaPlayer.SetVideoCallbacks(Lock, null, Display);

                // Start recording
                mediaPlayer.Play(media);

                // Waits for the processing to stop
                try
                {
                    await ProcessThumbnailsAsync(destination, processingCancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                { }

                Console.WriteLine("Done. Press any key to exit.");
                Console.ReadKey();
            }
        }

        private static async Task ProcessThumbnailsAsync(string destination, CancellationToken token)
        {
            var frameNumber = 0;
            var surface = SKSurface.Create(new SKImageInfo((int) Width, (int) Height));
            var canvas = surface.Canvas;
            while (!token.IsCancellationRequested)
            {
                if (FilesToProcess.TryDequeue(out var bitmap))
                {
                    canvas.DrawBitmap(bitmap, 0, 0); // Effectively crops the original bitmap to get only the visible area

                    Console.WriteLine($"Writing {frameNumber:0000}.jpg");
                    var fileName = Path.Combine(destination, $"{frameNumber:0000}.jpg");
                    using (var outputImage = surface.Snapshot())
                    using (var data = outputImage.Encode(SKEncodedImageFormat.Jpeg, 50))
                    using (var outputFile = File.Open(fileName, FileMode.Create))
                    {
                        data.SaveTo(outputFile);
                        bitmap.Dispose();
                    }

                    frameNumber++;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), token);
                }
            }
        }

        private static IntPtr Lock(IntPtr opaque, IntPtr planes)
        {
            CurrentBitmap = new SKBitmap(new SKImageInfo((int)(Pitch / BytePerPixel), (int)Lines, SKColorType.Bgra8888));
            Marshal.WriteIntPtr(planes, CurrentBitmap.GetPixels());
            return IntPtr.Zero;
        }

        private static void Display(IntPtr opaque, IntPtr picture)
        {
            if (FrameCounter % 100 == 0)
            {
                FilesToProcess.Enqueue(CurrentBitmap);
                CurrentBitmap = null;
            }
            else
            {
                CurrentBitmap.Dispose();
                CurrentBitmap = null;
            }
            FrameCounter++;
        }
    }
}
