NowPlayingLib
=============

A wrapper library that controlls the media players on Windows

README は [日本語版](https://github.com/chitoku-k/NowPlayingLib/blob/master/README.ja.md) もあります。

## Supported Players

- Windows Media Player
- iTunes
- x-APPLICATION
- LISMO Port (x-APPLICATION for LISMO)
- foobar2000
    - requires [COM Automation server](http://foosion.foobar2000.org/0.9/#comserver)
    - no artwork support

## Sample

```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NowPlayingLib;

class Sample
{
    public async Task Run()
    {
        try
        {
            // Instantiate a player without blocking
            using (var player = await Task.Run(() => new WindowsMediaPlayer()))
            {
                // Get the current media asynchronously
                var media = await player.GetCurrentMedia();

                // Output the title if playing
                Debug.WriteLine(media?.Title ?? "Not playing");

                // Add event handler
                player.CurrentMediaChanged += OnMediaChanged;

                // Skip to the next track
                player.NextTrack();
            }
        }
        catch (TypeInitializationException ex)
        {
            // InnerException is a COMException
            Debug.WriteLine($"Could not create an instance: {ex.InnerException.Message}");
        }
        catch (COMException ex)
        {
            // The exceptions from the player
            Debug.WriteLine($"The operation has failed: {ex.Message}");
        }

        // The player and its COM object have been disposed by using statement
    }

    private void OnMediaChanged(object sender, CurrentMediaChangedEventArgs e)
    {
        if (e.CurrentMedia != null)
        {
            Debug.WriteLine($"Name: {e.CurrentMedia.Name}");
        }
    }
}
```

## Special Thanks

The implementation of artwork support for `SonyMediaPlayerX` is based on [NowPlayingForX-APP](https://github.com/tuyapin/NowPlayingForX-APP) by [tuyapin](https://github.com/tuyapin).
