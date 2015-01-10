using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace NowPlayingLib
{
    /// <summary>
    /// 音楽プレーヤーが再生しているトラックの情報を格納します。
    /// </summary>
    public class MediaItem
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Collection<Stream> _artworks;

        /// <summary>
        /// トラックのアルバム名を取得または設定します。
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// トラックのアルバムのアーティスト名を取得または設定します。
        /// </summary>
        public string AlbumArtist { get; set; }

        /// <summary>
        /// トラックのアーティスト名を取得または設定します。
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// トラックのアルバムアートワークのコレクションを取得または設定します。
        /// </summary>
        public Collection<Stream> Artworks
        {
            get { return _artworks ?? new Collection<Stream>(); }
            set { _artworks = value; }
        }

        /// <summary>
        /// トラックのビットレートを取得または設定します。
        /// </summary>
        public int BitRate { get; set;}

        /// <summary>
        /// トラックのカテゴリーを取得または設定します。
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// トラックの作曲者を取得または設定します。
        /// </summary>
        public string Composer { get; set; }

        /// <summary>
        /// トラックがライブラリに追加された日時を取得または設定します。
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// トラックの総再生時間を取得または設定します。
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// トラックのジャンルを取得または設定します。
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// トラックの種類を取得または設定します。
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// トラックのファイル情報を取得または設定します。
        /// </summary>
        public FileInfo FileInfo { get; set; }

        /// <summary>
        /// トラックの歌詞を取得または設定します。
        /// </summary>
        public string Lyrics { get; set; }

        /// <summary>
        /// トラックの曲名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// トラックの再生回数を取得または設定します。
        /// </summary>
        public int PlayedCount { get; set; }

        /// <summary>
        /// トラックの最終再生日時を取得または設定します。
        /// </summary>
        public DateTime PlayedDate { get; set; }

        /// <summary>
        /// トラックのリリース日時を取得または設定します。
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// トラックの番号を取得または設定します。
        /// </summary>
        public int TrackNumber { get; set; }

        /// <summary>
        /// トラックの発表年を取得または設定します。
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// トラックの情報を表す文字列を返します。
        /// </summary>
        /// <returns>トラックの基本情報。</returns>
        public override string ToString()
        {
            try
            {
                if (this.Name == null || this.Artist == null || this.Album == null)
                {
                    return "null";
                }
                else
                {
                    return string.Format("{0} - {1} / {2}", this.Artist, this.Name, this.Album);
                }
            }
            catch
            {
                return "null";
            }
        }
    }
}
