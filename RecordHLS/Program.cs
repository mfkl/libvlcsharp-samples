﻿using LibVLCSharp.Shared;
using System;
using System.IO;
using System.Reflection;

namespace RecordHLS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Record in a file "record.ts" located in the bin folder next to the app
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var destination = Path.Combine(currentDirectory, "record.ts");

            // Load native libvlc library
            Core.Initialize();

            using (var libvlc = new LibVLC())
            using (var mediaPlayer = new MediaPlayer(libvlc))
            {
                // Redirect log output to the console
                libvlc.Log += (sender, e) => Console.WriteLine($"[{e.Level}] {e.Module}:{e.Message}");

                // Create new media with HLS link
                using (var media = new Media(libvlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"),
                    // Define stream output options.
                    // In this case stream to a file with the given path and play locally the stream while streaming it.
                    ":sout=#file{dst=" + destination + "}", 
                    ":sout-keep"))
                {
                    // Start recording
                    mediaPlayer.Play(media);
                }

                Console.WriteLine($"Recording in {destination}");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}