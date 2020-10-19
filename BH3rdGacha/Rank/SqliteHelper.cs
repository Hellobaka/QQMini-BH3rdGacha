using QQMini.PluginSDK.Core;
using SaveInfos;
using System;
using System.Data.SQLite;

namespace BH3rdGacha.Rank
{
    public static class SqliteHelper
    {
        public static SQLiteConnection GetConnection()
        {
            string path = $@"{MainSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            return cn;
        }
        public static bool CloseConnection(SQLiteConnection cn)
        {
            try
            {
                cn.Close();
                return true;
            }
            catch (Exception e)
            {
                QMLog.CurrentApi.Info($"数据库关闭，关闭失败，错误信息:{e.Message}");
            }
            return false;
        }
    }
}
