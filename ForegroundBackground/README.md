# ForegroundBackground sample

## Context: How it works and why

On Android, the native `libvlc` side will get released when the app gets paused/stopped (backgrounded). For LibVLCSharp users, that means you need to create a new `VideoView` and attach it to an existing, or new, `MediaPlayer` when the app goes back to foreground.

This is also true when using `LibVLCSharp.Forms`, which uses an additional abstraction layer: `Xamarin.Forms`. Since to the best of my knowledge, `Xamarin.Forms` does not provide advanced  APIs to handle app lifecycle across platforms, you need to do that small plumbing yourself by using the `MessagingCenter` for example like [here](https://code.videolan.org/mfkl/libvlcsharp-samples/commit/0af12636ac19710dc4e11b6f617a4fb36c7b86c3#cfde8b78cf808618fa580efa9007445c112443b3_0_28).

When the app goes to background, save the `MediaPlayer` time or position and remove the `VideoView` from its parent. 
When the app goes to foreground again, you will be able to reset the `MediaPlayer` position to where it stopped. Set the `MediaPlayer` on a new `VideoView` and add the view to the main layout (be it a `Grid`, `StackLayout`, etc.).