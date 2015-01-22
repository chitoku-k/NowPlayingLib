using SonyMediaPlayerXLib;
using SonyVzProperty;
using System;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace NowPlayingLib.SonyDatabase
{
    /// <summary>
    /// x-アプリのデータベースから曲情報を取得します。
    /// </summary>
    public class MediaManager
    {
        /// <summary>
        /// データベースのパス。
        /// </summary>
        public static readonly string DatabasePath = @"Sony Corporation\Sony MediaPlayerX\Packages\MtData.mdb";

        /// <summary>
        /// メディア オブジェクトを指定して曲情報を取得します。
        /// </summary>
        /// <param name="media">メディア オブジェクト。</param>
        /// <returns>データベースから得られた曲情報。</returns>
        public static ObjectTable GetMediaEntry(ISmpxMediaDescriptor2 media)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), DatabasePath);
            using (var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path))
            using (var context = new MtDataDataContext(connection))
            {
                // IQueryable.FirstOrDefault では不正な SQL 文が発行される
                return context.ObjectTable.Where(x => x.SpecId == (int)VzObjectCategoryEnum.vzObjectCategory_Music + 1 &&
                                                      x.Album == media.packageTitle &&
                                                      x.Artist == media.artist &&
                                                      x.Genre == media.genre &&
                                                      x.Name == media.title).AsEnumerable().FirstOrDefault();
            }
        }
    }
}
