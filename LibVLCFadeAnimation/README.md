#### Additional Requirements
##### Linux:
This command has to be executed for LibVLC to work.

    sudo apt install vlc libvlc-dev libx11-dev

#### Building the Project
After executing the following commands, the output is saved in the bin folder.

##### Windows:
    dotnet build -c Debug -r win-x64 .\LibVLCFadeAnimation.csproj

##### Linux:
    dotnet build -c Debug -r linux-x64 .\LibVLCFadeAnimation.csproj
