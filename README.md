# Samples for [LibVLCSharp](https://code.videolan.org/videolan/LibVLCSharp)

[![Join the chat at https://discord.gg/3h3K3JF](https://img.shields.io/discord/716939396464508958?label=discord)](https://discord.gg/3h3K3JF)
[![Build Status](https://videolan.visualstudio.com/libvlcsharp-samples/_apis/build/status/mfkl.libvlcsharp-samples?branchName=master)](https://videolan.visualstudio.com/libvlcsharp-samples/_build/latest?definitionId=45&branchName=master)

Various samples on different platforms showcasing [LibVLCSharp](https://code.videolan.org/videolan/LibVLCSharp) features. 

:wave: If you need custom LibVLCSharp integration or advanced samples, feel free to [contact us](mailto:dotnet@videolabs.io). Commercial support is also [available](https://videolabs.io/#contact).

## [Chromecast sample](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/Chromecast)

A sample demonstrating how to find and use a Chromecast in a crossplatform Xamarin.Forms app (iOS/Android). [Blog post](https://mfkl.github.io/chromecast/2018/10/21/High-performance-cross-platform-streaming-with-libvlc-and-Chromecast-on-.NET.html)

## [HLS sample](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/RecordHLS)

A sample demonstrating how to record an HLS stream using LibVLCSharp in a .NET Core CLI Windows app. [Blog post](https://mfkl.github.io/hls/2018/10/10/How-to-record-HLS-stream-with-LibVLCSharp-and-.NET-Core.html)

## [RTSP Mosaic sample](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/VideoMosaic)

<img src="https://mfkl.github.io/assets/mosaic-ios.png" Width="220" /> <img src="https://mfkl.github.io/assets/mosaic-android.png" Width="220" />

Short Xamarin.Forms sample demonstrating how to build a mosaic-style RTSP player (iOS/Android). [Blog post](https://mfkl.github.io/libvlc/rtsp/xamarin/forms/2018/12/05/crossplatform-RTSP-mosaic-views-with-libvlcsharp.html)

## [ForegroundBackground](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/ForegroundBackground)

This sample demonstrates how to deal with video playback when the app goes in the background/foreground.
It shows one way to deal with this on Android with LibVLCSharp.Forms on Xamarin.Forms specifically, which is a bit trickier than other platforms.

## [PulseMusic](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/PulseMusic)

<img src="https://mfkl.github.io/assets/pulse-music-concept.gif"/> <img src="https://mfkl.github.io/assets/pulse-music-playback.gif" Width="220" />

PulseMusic is an audio player sample showcasing use of Skia and LibVLCSharp to implement great UX player view. [Blog post](https://mfkl.github.io/libvlc/skia/xamarin/forms/ux/2018/12/31/PulseMusic-music-player-design.html)

## [LibVLCSharp.PHP](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/LibVLCSharp.PHP)

A small experiment that shows how to use LibVLCSharp from PHP using the [PeachPie](https://www.peachpie.io/) compiler.

## [LibVLCSharp.VB](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/LibVLCSharp.VB)

A VB.NET (Visual Basic) sample that uses LibVLCSharp.

## [Gestures](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/Gestures/Gestures)

[![VLC 3.0 playing 8K 48fps 360 video on Android Galaxy S8](https://i.imgur.com/0B34Hjj.png)](https://player.vimeo.com/video/254723180 "VLC 3.0 playing 8K 48fps 360 video on Android Galaxy S8")

[VLC 3.0 playing 8K 48fps 360 video on Android Galaxy S8](https://player.vimeo.com/video/254723180)

2 cross-platform gestures samples showcasing how to use the Xamarin.Forms crossplatform gestures to control the video position, volume level and 360 viewpoint. [Blog post](https://mfkl.github.io/libvlc/360/xamarin/forms/ux/2019/02/12/Fun-with-crossplatform-gestures-and-360-videos.html)

## [LocalNetwork](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/LocalNetwork)

![](localnetwork-record.mp4)

Xamarin.Forms sample showcasing local network browsing and playback of network shares (SMB, UPnP) on Android, iOS and WPF from 100% shared code. [Blog post](https://mfkl.github.io/libvlc/crossplatform/xamarin/forms/2019/07/02/Crossplatform-local-network-browsing-and-media-playback.html)

## [MediaPlayerElement](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/MediaElement)

<img src="https://mfkl.github.io/assets/media-element-iphone.png"/>

Minimal Xamarin.Forms sample to get up and running quickly with the MediaPlayerElement from LibVLCSharp.Forms. [Blog post](https://mfkl.github.io/libvlc/crossplatform/xamarin/forms/2019/08/13/MediaPlayerElement-Plug-and-play-LibVLCSharp-UI-video-control.html)

## [Preview Thumbnailer](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/PreviewThumbnailExtractor)

<img src="https://pbs.twimg.com/media/EMtC5TVUcAA2VMD?format=jpg&name=medium"/>

.NET Core crossplatform sample showing how to use the LibVLC 3 video callbacks API to extract frames and save them to disk with ImageSharp or Skia.

## [ScreenRecorder](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/ScreenRecorder)

.NET Core crossplatform (Windows, macOS, Linux) console app that records your screen for 5 seconds and saves the video as an mp4 on disk.

## [LVST](https://github.com/mfkl/lvst)

.NET Core crossplatform (Windows, macOS, Linux) CLI app that allows you to stream any media torrent for local or remote (chromecast) playback. [Blog post](https://mfkl.github.io/libvlc/2020/03/23/Torrents-and-multimedia-streaming.html)

<img src="https://raw.githubusercontent.com/mfkl/lvst/master/lvst.gif"/>

## [AudioCallbacks](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/AudioCallbacks)

This sample shows you how you can use `SetAudioFormatCallback` and `SetAudioCallbacks`. It does two things:

1. Play the sound from the specified video using [NAudio](https://github.com/naudio/NAudio)
2. Extract the sound into a file using [NAudio](https://github.com/naudio/NAudio)

## [Speech](https://code.videolan.org/mfkl/libvlcsharp-samples/tree/master/Speech)

Speak to your video player by integrating [System.Speech](https://docs.microsoft.com/en-us/dotnet/api/system.speech.recognition) with LibVLC in ~ 100 lines of code.

![](libvlc-speech.mp4)
---

For more samples, check out the [Community Projects](https://code.videolan.org/videolan/LibVLCSharp/-/blob/3.x/docs/made_with_libvlcsharp.md) made with LibVLCSharp.
