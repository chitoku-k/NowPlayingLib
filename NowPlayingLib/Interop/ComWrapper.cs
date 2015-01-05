using System;
using System.Runtime.InteropServices;

namespace NowPlayingLib.Interop
{
    /// <summary>
    /// <see cref="System.IDisposable"/> を実装し COM オブジェクトを自動的に解放するラッパーを提供します。
    /// </summary>
    /// <typeparam name="T">COM オブジェクトの型。</typeparam>
    public class ComWrapper<T> : ComWrapper, IDisposable
    {
        /// <summary>
        /// COM オブジェクトを取得します。
        /// </summary>
        public T Object { get; private set; }

        internal ComWrapper(T o)
        {
            this.Object = o;
        }

        /// <summary>
        /// ガベージ コレクターがオブジェクトを破棄する前に、最後のクリーンアップを実行します。
        /// </summary>
        ~ComWrapper()
        {
            Dispose(false);
        }

        /// <summary>
        /// COM オブジェクトを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// COM オブジェクトを解放します。
        /// </summary>
        /// <param name="disposing">明示的な破棄要求元から呼び出された場合は true に設定します。それ以外の場合は false。</param>
        protected void Dispose(bool disposing)
        {
            if (disposing && this.Object != null && Marshal.IsComObject(this.Object))
            {
                Marshal.FinalReleaseComObject(this.Object);
            }
        }
    }

    /// <summary>
    /// COM オブジェクトを自動的に解放するラッパーを表します。
    /// </summary>
    public class ComWrapper
    {
        /// <summary>
        /// <see cref="NowPlayingLib.Interop.ComWrapper"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        protected ComWrapper() { }

        /// <summary>
        /// 指定した COM オブジェクトのラッパーとして機能する <see cref="NowPlayingLib.Interop.ComWrapper"/> を作成します。
        /// </summary>
        /// <typeparam name="T">COM オブジェクトの型。</typeparam>
        /// <param name="o">COM オブジェクト。</param>
        /// <returns>COM オブジェクトのラッパー。</returns>
        public static ComWrapper<T> Create<T>(T o)
        {
            return new ComWrapper<T>(o);
        }
    }
}
