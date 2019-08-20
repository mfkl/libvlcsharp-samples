# LibVLCSharp Preview thumbnail extractor sample

## Purpose

This project aims at demonstrating how to extract frames from a video with LibVLCSharp, in an way that is as efficient as possible.
This can be used to extract preview thumbnails from a video like this sample does, or to grab frames to perform video analysis.

## A few words of warning
Each time a frame is sent by VLC, our callback is called synchronously.
Attempting to do long operations there or setting the FPS to a high value would result in high CPU usage and VLC skipping frames.

If you really need each frame, this is probably not the right way to do so.

This sample is made with the current features of libvlc 3.
The feature we are using here is likely to change in libvlc 4, and this sample will need to be updated or rewritten.