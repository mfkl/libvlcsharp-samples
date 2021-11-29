using LibVLCSharp.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubePlayback
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Core.Initialize();

            var libvlc = new LibVLC(enableDebugLogs: true);
            var mediaplayer = new MediaPlayer(libvlc);

            var media = new Media(libvlc, new Uri("https://www.youtube.com/watch?v=aqz-KE-bpKQ"));
            await media.Parse(MediaParseOptions.ParseNetwork);
            mediaplayer.Play(media.SubItems.First());

            Console.ReadKey();
        }
    }
}
