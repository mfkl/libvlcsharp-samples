# EncryptedStream
## play encrypted video stream using cryptostream 
Same code as was in <a href="https://github.com/graysuit/vlc_text_encr">vlc_text_encr</a>. Just used <b>libvlcsharp</b> instead of <b>vlc.dotnet</b>.
After build you will have to choose any encrypted video which can be found at <a href="https://github.com/graysuit/vlc_text_encr/blob/master/Encrypted_Video/video.ded">here</a>.
Normally you can't play encrypted stream easily.Because they are unseekable. That's why we use unseekable wrapper made by <a href="https://github.com/ZeBobo5/Vlc.DotNet/issues/647#issuecomment-632657788">jeremyVignelles</a> 
According to him: <b>unseekable wrapper destroys and recreate a new stream on seek.</b>

<b>Important:</b> 4th constructor of <b>cryptostream</b> named <b>leaveopen</b> only available in modern framework versions. This should be true. Else stream will close automatically and video won't play.
