using LibVLCSharp.Shared;
using System.Threading.Tasks;

namespace ScreenRecorder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Core.Initialize();

            using var libvlc = new LibVLC();
            using var mediaPlayer = new MediaPlayer(libvlc);
            using var media = new Media(libvlc, "screen://", FromType.FromLocation);

            media.AddOption(":screen-fps=24");
            media.AddOption(":sout=#transcode{vcodec=h264,vb=0,scale=0,acodec=mp4a,ab=128,channels=2,samplerate=44100}:file{dst=record.mp4}");
            media.AddOption(":sout-keep");

            mediaPlayer.Play(media); // start recording

            await Task.Delay(5000); // record for 5 seconds

            mediaPlayer.Stop(); // stop recording and saves the file
        }
    }
}


