NowPlayingLib
=============

Windows 向けメディア プレーヤー操作用ラッパー ライブラリ

## 対応プレーヤー

- Windows Media Player
- iTunes
- x-APPLICATION
- LISMO Port (x-APPLICATION for LISMO)
- foobar2000
    - 要 [COM Automation server](http://foosion.foobar2000.org/0.9/#comserver)
    - アルバム アートワーク未サポート

## サンプル

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
            // ブロッキングなしでプレーヤーを初期化
            using (var player = await Task.Run(() => new WindowsMediaPlayer()))
            {
                // 現在の曲を非同期で取得
                var media = await player.GetCurrentMedia();

                // 再生中の場合は曲名を出力
                Debug.WriteLine(media?.Title ?? "Not playing");

                // イベント ハンドラーを設定
                player.CurrentMediaChanged += OnMediaChanged;

                // 次の曲にスキップ
                player.NextTrack();
            }
        }
        catch (TypeInitializationException ex)
        {
            // InnerException: COMException
            Debug.WriteLine($"インスタンスの作成に失敗しました: {ex.InnerException.Message}");
        }
        catch (COMException ex)
        {
            // プレーヤーからの例外
            Debug.WriteLine($"操作は失敗しました: {ex.Message}");
        }

        // プレーヤーと COM オブジェクトは using ステートメントで解放済
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

`SonyMediaPlayerX` のアートワーク取得の実装は [tuyapin](https://github.com/tuyapin) による [NowPlayingForX-APP](https://github.com/tuyapin/NowPlayingForX-APP) をベースにしています。
