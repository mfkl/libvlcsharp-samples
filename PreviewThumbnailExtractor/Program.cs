using LibVLCSharp.Shared;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace PreviewThumbnailExtractor
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

        private static MemoryMappedFile CurrentMappedFile;
        private static MemoryMappedViewAccessor CurrentMappedViewAccessor;
        private static readonly ConcurrentQueue<(MemoryMappedFile file, MemoryMappedViewAccessor accessor)> FilesToProcess = new ConcurrentQueue<(MemoryMappedFile file, MemoryMappedViewAccessor accessor)>();
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
                mediaPlayer.Stopped += (s, e) => processingCancellationTokenSource.Cancel();

                // Create new media
                var media = new Media(libvlc, "http://www.caminandes.com/download/03_caminandes_llamigos_1080p.mp4", FromType.FromLocation);
                
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
            while (!token.IsCancellationRequested)
            {
                if (FilesToProcess.TryDequeue(out var file))
                {
                    using (var image = new Image<SixLabors.ImageSharp.PixelFormats.Bgra32>((int)(Pitch / BytePerPixel), (int)Lines))
                    using (var sourceStream = file.file.CreateViewStream())
                    {
                        sourceStream.Read(MemoryMarshal.AsBytes(image.GetPixelSpan()));

                        var fileName = Path.Combine(destination, $"{frameNumber:0000}.jpg");
                        using (var outputFile = File.Open(fileName, FileMode.Create))
                        {
                            image.Mutate(ctx => ctx.Crop((int)Width, (int)Height));
                            image.SaveAsJpeg(outputFile);
                        }
                    }
                    file.accessor.Dispose();
                    file.file.Dispose();
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
            CurrentMappedFile = MemoryMappedFile.CreateNew(null, Pitch * Lines);
            CurrentMappedViewAccessor = CurrentMappedFile.CreateViewAccessor();
            Marshal.WriteIntPtr(planes, CurrentMappedViewAccessor.SafeMemoryMappedViewHandle.DangerousGetHandle());
            return IntPtr.Zero;
        }

        private static void Display(IntPtr opaque, IntPtr picture)
        {
            if(FrameCounter % 100 == 0)
            {
                FilesToProcess.Enqueue((CurrentMappedFile, CurrentMappedViewAccessor));
                CurrentMappedFile = null;
                CurrentMappedViewAccessor = null;
            }
            else
            {
                CurrentMappedViewAccessor.Dispose();
                CurrentMappedFile.Dispose();
                CurrentMappedFile = null;
                CurrentMappedViewAccessor = null;
            }
            FrameCounter++;
        }
    }
}
