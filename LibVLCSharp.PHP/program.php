<?php

use LibVLCSharp\Shared\Core as Core;
use LibVLCSharp\Shared\LibVLC as LibVLC;
use LibVLCSharp\Shared\MediaPlayer as MediaPlayer;
use LibVLCSharp\Shared\Media as Media;

# Hack to compute libvlc.dll/libvlccore.dll path for PHP projects
list($scriptPath) = get_included_files();
$p = str_replace("program.php", "", $scriptPath);
$libvlcPath = $p . "bin\\Debug\\netcoreapp2.0";

Core::Initialize($libvlcPath);

$libVLC = new LibVLC();
$mediaPlayer = new MediaPlayer($libVLC);
$media = new Media($libVLC, "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4", 1);

$mediaPlayer->Play($media);

echo "Loading...";
$handle = fopen ("php://stdin","r");
$line = fgets($handle);
if(trim($line) != 'yes'){
    echo "ABORTING!\n";
    exit;
}
fclose($handle);