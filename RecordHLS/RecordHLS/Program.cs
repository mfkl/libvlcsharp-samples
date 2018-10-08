using LibVLCSharp.Shared;
using System;
using System.IO;
using System.Reflection;

namespace RecordHLS
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var destination = Path.Combine(currentDirectory, "record.ts");

            Core.Initialize();

            using (var libvlc = new LibVLC())
            using (var mediaPlayer = new MediaPlayer(libvlc))
            {
                var media = new Media(libvlc, "http://hls1.addictradio.net/addictrock_aac_hls/playlist.m3u8", Media.FromType.FromLocation);
                media.AddOption(":sout=#file{dst=" + destination + "}");
                media.AddOption(":sout-keep");

                mediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
                mediaPlayer.Opening += MediaPlayer_Opening;
                mediaPlayer.PositionChanged += MediaPlayer_PositionChanged;
                mediaPlayer.EndReached += MediaPlayer_EndReached;

                mediaPlayer.Play(media);

                Console.WriteLine($"Recording in {destination}");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        private static void MediaPlayer_EndReached(object sender, EventArgs e)
        {
            Console.WriteLine("End reached");
        }

        private static void MediaPlayer_PositionChanged(object sender, MediaPlayerPositionChangedEventArgs e)
        {
            Console.WriteLine($"position: {e.Position}");
        }

        private static void MediaPlayer_Opening(object sender, EventArgs e)
        {
            Console.WriteLine("Opening... ");
        }

        private static void MediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            Console.WriteLine($"time: {e.Time}");
        }
    }
}