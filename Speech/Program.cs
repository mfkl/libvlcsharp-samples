using LibVLCSharp.Shared;
using System;
using System.Speech.Recognition;

namespace Speech
{
    class Program
    {
        // commands
        const string PLAY = "play";
        const string PAUSE = "pause";
        const string STOP = "stop";
        const string INCREASE_VOLUME = "increase volume";
        const string DECREASE_VOLUME = "decrease volume";
        const string MUTE = "mute";
        const string UNMUTE = "unmute";
        const string SEEK = "seek";
        const string REWIND = "rewind";
        const string SECONDS = "seconds";
        const string MINUTES = "minutes";

        static LibVLC _libvlc;
        static MediaPlayer _mp;

        static void Main(string[] args)
        {
            using var sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            sre.SpeechRecognized += OnSpeechRecognized;

            sre.SetInputToDefaultAudioDevice();

            var choices = new Choices();
            choices.Add(PLAY);
            choices.Add(PAUSE);
            choices.Add(STOP);
            choices.Add(INCREASE_VOLUME);
            choices.Add(DECREASE_VOLUME);
            choices.Add(MUTE);
            choices.Add(UNMUTE);

            for (var i = 1; i < 100; i++)
            {
                choices.Add($"{SEEK} {i} {SECONDS}");
                choices.Add($"{SEEK} {i} {MINUTES}");
                choices.Add($"{REWIND} {i} {SECONDS}");
                choices.Add($"{REWIND} {i} {MINUTES}");
            }

            var grammarBuilder = new GrammarBuilder(choices)
            {
                Culture = new System.Globalization.CultureInfo("en-GB")
            };

            var grammar = new Grammar(grammarBuilder);

            sre.LoadGrammar(grammar);
            sre.RecognizeAsync(RecognizeMode.Multiple);

            Core.Initialize();

            _libvlc = new LibVLC("--quiet");

            using var media = new Media(_libvlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"));

            _mp = new MediaPlayer(media);

            _mp.Play();

            while (true)
                Console.ReadLine();
        }

        static void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence < 0.5) return;

            Console.WriteLine($"[Recognized] {e.Result.Text} ({nameof(e.Result.Confidence)}: {e.Result.Confidence:P})");

            switch (e.Result.Text)
            {
                case PLAY:
                    _mp.Play();
                    break;
                case PAUSE:
                    _mp.Pause();
                    break;
                case STOP:
                    _mp.Stop();
                    break;
                case INCREASE_VOLUME:
                    _mp.Volume += 10;
                    Console.WriteLine($"Volume is now {_mp.Volume}");
                    break;
                case DECREASE_VOLUME:
                    _mp.Volume -= 10;
                    Console.WriteLine($"Volume is now {_mp.Volume}");
                    break;
                case MUTE:
                    _mp.Mute = true;
                    break;
                case UNMUTE:
                    _mp.Mute = false;
                    break;
                case var cmd when cmd.StartsWith(SEEK) || cmd.StartsWith(REWIND):
                    var cmdWords = cmd.Split(' ');
                    if(string.Equals(cmdWords[0], SEEK))
                        _mp.Time += string.Equals(cmdWords[2], SECONDS) ? int.Parse(cmdWords[1]) * 1000 : int.Parse(cmdWords[1]) * 60000;
                    else _mp.Time -= string.Equals(cmdWords[2], SECONDS) ? int.Parse(cmdWords[1]) * 1000 : int.Parse(cmdWords[1]) * 60000;
                    Console.WriteLine($"Time is now {_mp.Time}");
                    break;
                default:
                    break;
            }
        }
    }
}