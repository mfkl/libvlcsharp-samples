Imports System.IO
Imports LibVLCSharp.Shared

Module Program
    Sub Main(args As String())
        Core.Initialize()
        Using libVLC = New LibVLC()
            Dim video = New Media(libVLC, "https://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4",
                                  FromType.FromLocation)

            Using mp = New MediaPlayer(video)
                video.Dispose()
                mp.Play()
                Console.ReadKey()
            End Using
        End Using
    End Sub
End Module