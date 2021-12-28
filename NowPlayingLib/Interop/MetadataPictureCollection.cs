using Microsoft.MediaPlayer.Interop;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NowPlayingLib.Interop
{
    /// <summary>
    /// <see cref="Microsoft.MediaPlayer.Interop.IWMPMetadataPicture"/> に対してインデックスによってアクセスできる要素の読み取り専用コレクションを表します。
    /// </summary>
    public class MetadataPictureCollection : IReadOnlyList<IWMPMetadataPicture>
    {
        /// <summary>
        /// コレクション内の有効な要素のインデックスを取得します。
        /// </summary>
        protected IReadOnlyList<int> Indices { get; }

        /// <summary>
        /// コレクション内の要素の数を取得します。
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// 対象の曲情報を取得します。
        /// </summary>
        public IWMPMedia3 Media { get; }

        /// <summary>
        /// <see cref="NowPlayingLib.Interop.MetadataPictureCollection"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="media">曲情報。</param>
        public MetadataPictureCollection(IWMPMedia media)
            : this((IWMPMedia3)media) { }

        /// <summary>
        /// <see cref="NowPlayingLib.Interop.MetadataPictureCollection"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="media">曲情報。</param>
        public MetadataPictureCollection(IWMPMedia3 media)
        {
            if (media == null)
            {
                throw new ArgumentNullException(nameof(media));
            }
            this.Media = media;
            this.Indices = GetIndices();
            this.Count = this.Indices.Count;
        }

        /// <summary>
        /// コレクション内の有効な要素のインデックスを取得します。
        /// </summary>
        /// <returns>有効な要素のインデックス。</returns>
        protected IReadOnlyList<int> GetIndices()
        {
            var indices = new List<int>();

            for (int i = 0; i < this.Media.getAttributeCountByType(AudioAttributes.Picture, ""); i++)
            {
                try
                {
                    this.Media.getItemInfoByType(AudioAttributes.Picture, "", i);
                }
                catch (ArgumentException)
                {
                    // IWMPMedia3.getItemInfoByType(string,string,int) throws because not all WM/Picture entries can be retrieved for some reason.
                    continue;
                }
                indices.Add(i);
            }

            return indices;
        }

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>コレクションを反復処理するために使用できる <see cref="System.Collections.Generic.IEnumerator&lt;T&gt;"/>。</returns>
        public IEnumerator<IWMPMetadataPicture> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this[i];
            }
        }

        /// <summary>
        /// 読み取り専用のリスト内の指定したインデックスにある要素を取得します。
        /// </summary>
        /// <param name="index">取得する要素の 0 から始まるインデックス。</param>
        /// <returns>読み取り専用のリスト内の指定したインデックスにある要素。</returns>
        public IWMPMetadataPicture this[int index]
        {
            get
            {
                try
                {
                    return (IWMPMetadataPicture)this.Media.getItemInfoByType(AudioAttributes.Picture, "", this.Indices[index]);
                }
                catch (ArgumentException ex) when (this.Count <= index)
                {
                    throw new IndexOutOfRangeException(ex.Message, ex);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
