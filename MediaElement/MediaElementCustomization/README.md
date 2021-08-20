## How to use the MediaElement control ?

```xaml
<vlc:MediaPlayerElement
    EnableRendererDiscovery="True"
    LibVLC="{Binding LibVLC}"
    MediaPlayer="{Binding MediaPlayer}" />
```

Without customization, the view looks like VLC Android or iOS UI.

| <img src="https://raw.githubusercontent.com/egbakou/libvlcsharp-assets/master/customization/Screen001.png" width="280" height="480" /> | <img src="https://raw.githubusercontent.com/egbakou/libvlcsharp-assets/master/customization/Screen002.png" width="280" height="480" /> |
| ------------------------------------------------------------ | ------------------------------------------------------------ |

## PlayBack Control Customization

|                Property                |  Type   |
| :------------------------------------: | :-----: |
|              ButtonColor               |  Color  |
|               Foreground               |  Color  |
|               MainColor                |  Color  |
|           TracksButtonStyle            |  Style  |
|       BufferingProgressBarStyle        |  Style  |
|            CastButtonStyle             |  Style  |
|           ControlsPanelStyle           |  Style  |
|              MessageStyle              |  Style  |
|          PlayPauseButtonStyle          |  Style  |
|        RemainingTimeLabelStyle         |  Style  |
|         ElapsedTimeLabelStyle          |  Style  |
|              SeekBarStyle              |  Style  |
|            StopButtonStyle             |  Style  |
|         AspectRatioButtonStyle         |  Style  |
|           RewindButtonStyle            |  Style  |
|            SeekButtonStyle             |  Style  |
|           ButtonBarStartArea           |  Style  |
|            ButtonBarEndArea            |  Style  |
|              ErrorMessage              |  Style  |
|              KeepScreenOn              | boolean |
|        ShowAndHideAutomatically        | boolean |
|          IsCastButtonVisible           | boolean |
|        IsPlayPauseButtonVisible        | boolean |
|             IsSeekEnabled              | boolean |
|            IsSeekBarVisible            | boolean |
|          IsStopButtonVisible           | boolean |
|       IsAspectRatioButtonVisible       | boolean |
|         IsRewindButtonVisible          | boolean |
|          IsSeekButtonVisible           | boolean |
|  IsAudioTracksSelectionButtonVisible   | boolean |
| IsClosedCaptionsSelectionButtonVisible | boolean |



## Customization Examples

- [x] Hide the Playback Controls (the Seek bar and the buttons Bar)

```xaml
<vlc:MediaPlayerElement
    EnableRendererDiscovery="True"
    LibVLC="{Binding LibVLC}"
    MediaPlayer="{Binding MediaPlayer}">
    <vlc:MediaPlayerElement.PlaybackControls>
        <vlc:PlaybackControls IsVisible="False"  />
    </vlc:MediaPlayerElement.PlaybackControls>
</vlc:MediaPlayerElement>
```

<img src="https://raw.githubusercontent.com/egbakou/libvlcsharp-assets/master/customization/Screen003.png" width="280" height="480" />



- [x] Hide Audio Track, Closed Captions and Aspect Ratio button

```xaml
<vlc:MediaPlayerElement
	EnableRendererDiscovery="True"
    LibVLC="{Binding LibVLC}"
    MediaPlayer="{Binding MediaPlayer}">
   	<vlc:MediaPlayerElement.PlaybackControls>
    	<vlc:PlaybackControls
        	IsAspectRatioButtonVisible="False"
            IsAudioTracksSelectionButtonVisible="False"
            IsClosedCaptionsSelectionButtonVisible="False" />
    </vlc:MediaPlayerElement.PlaybackControls>
</vlc:MediaPlayerElement>
```

<img src="https://raw.githubusercontent.com/egbakou/libvlcsharp-assets/master/customization/Screen006.png" width="280" height="480" />

- [x] Change the Main Color

```xaml
<vlc:MediaPlayerElement
    EnableRendererDiscovery="True"
    LibVLC="{Binding LibVLC}"
    MediaPlayer="{Binding MediaPlayer}">
    <vlc:MediaPlayerElement.PlaybackControls>
        <vlc:PlaybackControls MainColor="Red"  />
    </vlc:MediaPlayerElement.PlaybackControls>
</vlc:MediaPlayerElement>
```

| <img src="https://raw.githubusercontent.com/egbakou/libvlcsharp-assets/master/customization/Screen004.png" width="280" height="480" /> | <img src="https://raw.githubusercontent.com/egbakou/libvlcsharp-assets/master/customization/Screen005.png" width="280" height="480" /> |
| ------------------------------------------------------------ | ------------------------------------------------------------ |

