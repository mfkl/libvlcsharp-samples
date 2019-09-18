<?php

use LibVLCSharp\Shared\Core;
use LibVLCSharp\Shared\LibVLC;
use LibVLCSharp\Shared\MediaPlayer;
use LibVLCSharp\Shared\Media;

Core::Initialize();

$libVLC = new LibVLC();
$mediaPlayer = new MediaPlayer($libVLC);
$media = new Media($libVLC, "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4", 1);

$mediaPlayer->Play($media);

echo "Loading...";
$handle = fopen ("php://stdin","r");
$line = fgets($handle);
if(trim($line) != 'yes'){
    echo "ABORTING!\n";
    exit;
}
fclose($handle);