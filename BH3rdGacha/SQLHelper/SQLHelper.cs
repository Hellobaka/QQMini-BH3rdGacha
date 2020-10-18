using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BH3rdGacha.GachaHelper;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using SQLHelper.Models;
using SqlSugar;

namespace BH3rdGacha
{
    public static class SQLHelper
    {
        static string DBPath { get; set; }
        public static void Init(string DBPath)
        {
            SQLHelper.DBPath = DBPath;
        }
        private static SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = $"data source={DBPath}",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
            });
            return db;
        }
        public static int ExecuteSQL(string SQL)
        {
            using (var db = GetInstance())
            {
                var result = db.Ado.UseTran(() =>
                {
                    return db.Ado.ExecuteCommand(SQL);
                });
                if (result.IsSuccess)
                {
                    return result.Data;
                }
                else
                {
                    throw new Exception("执行错误，发生位置 ExecuteSQL " + result.ErrorMessage);
                }
            }
        }
        public static void SignReset(QMGroupMessageEventArgs e)
        {
            SignReset(e.FromGroup.Id);
        }

        public static void SignReset(Group Group)
        {
            SignReset(Group.Id);
        }

        public static void SignReset(long GroupID)
        {
            using (var db = GetInstance())
            {
                db.Ado.UseTran(() =>
                {
                    var query = db.Queryable<UserData>().Where(x => x.fromgroup == GroupID).ToList();
                    query.ForEach(x => x.timestamp = 0);
                    db.Updateable(query).ExecuteCommand();
                });
            }
        }
        public static void Register(QMGroupMessageEventArgs e)
        {
            Register(e.FromGroup, e.FromQQ);
        }
        public static void Register(Group Group, QQ QQ)
        {
            Register(Group.Id, QQ.Id);
        }

        public static void Register(long GroupID, long QQ)
        {
            using (var db = GetInstance())
            {
                db.Ado.UseTran(() =>
                {
                    UserData data = new UserData()
                    {
                        fromgroup = GroupID,
                        qq = QQ,
                        timestamp = 0,
                        sign = 0,
                        diamond = 0,
                        total_diamond = 0,
                        gacha_count = 0,
                        purple_count = 0,
                        sign_count = 0
                    };
                    db.Insertable(data).ExecuteCommand();
                });
            }
        }
        public static bool IDExist(long GroupID, long QQ)
        {
            using (var db = GetInstance())
            {
                var result = db.Ado.UseTran(() =>
                {
                    return db.Queryable<UserData>().Where(x => x.fromgroup == GroupID && x.qq == QQ).Any();
                });
                if (result.IsSuccess)
                {
                    return result.Data;
                }
                else
                {
                    throw new Exception("执行错误，发生位置 IDExist " + result.ErrorMessage);
                }
            }
        }
        public static bool IDExist(Group Group, QQ QQ)
        {
            return IDExist(Group.Id, QQ.Id);
        }
        public static bool IDExist(QMGroupMessageEventArgs e)
        {
            return IDExist(e.FromGroup, e.FromQQ);
        }
        public static int GetDiamond(QMGroupMessageEventArgs e)
        {
            return GetDiamond(e.FromGroup, e.FromQQ);
        }
        public static int GetDiamond(Group Group, QQ QQ)
        {
            return GetDiamond(Group.Id, QQ.Id);
        }

        public static int GetDiamond(long GroupID, long QQ)
        {
            using (var db = GetInstance())
            {
                var result = db.Ado.UseTran(() =>
                {
                    return db.Queryable<UserData>().First(x => x.fromgroup == GroupID && x.qq == QQ)
                    .diamond.GetValueOrDefault(0);
                });
                if (result.IsSuccess)
                {
                    return result.Data;
                }
                else
                {
                    throw new Exception("执行错误，发生位置 GetDiamond " + result.ErrorMessage);
                }
            }
        }
        private static UserData GetUser(long GroupID, long QQ, SqlSugarClient db)
        {
            return db.Queryable<UserData>().First(x => x.fromgroup == GroupID && x.qq == QQ);
        }
        public static int SubDiamond(QMGroupMessageEventArgs e, int num)
        {
            return SubDiamond(e.FromGroup, e.FromQQ, num);
        }

        public static int SubDiamond(Group Group, QQ QQ, int num)
        {
            return SubDiamond(Group.Id, QQ.Id, num);
        }
        public static int SubDiamond(long GroupID, long QQ, int num)
        {
            using (var db = GetInstance())
            {
                var result = db.Ado.UseTran(() =>
                {
                    var query = GetUser(GroupID, QQ, db);
                    query.diamond -= num;
                    db.Updateable(query).ExecuteCommand();
                    return query.diamond.GetValueOrDefault(0);
                });
                if (result.IsSuccess)
                {
                    return result.Data;
                }
                else
                {
                    throw new Exception("执行错误，发生位置 SubDiamond " + result.ErrorMessage);
                }
            }
        }
        public static int AddDiamond(QMGroupMessageEventArgs e, int num)
        {
            return AddDiamond(e.FromGroup, e.FromQQ, num);
        }

        public static int AddDiamond(Group Group, QQ QQ, int num)
        {
            return AddDiamond(Group.Id, QQ.Id, num);
        }
        public static int AddDiamond(long GroupID, long QQ, int num)
        {
            using (var db = GetInstance())
            {
                var result = db.Ado.UseTran(() =>
                {
                    var query = GetUser(GroupID, QQ, db);
                    query.diamond += num;
                    db.Updateable(query).ExecuteCommand();
                    return query.diamond.GetValueOrDefault(0);
                });
                if (result.IsSuccess)
                {
                    return result.Data;
                }
                else
                {
                    throw new Exception("执行错误，发生位置 AddDiamond " + result.ErrorMessage);
                }
            }
        }
        public static void AddCount_Gacha(QMGroupMessageEventArgs e, int num)
        {
            AddCount_Gacha(e.FromGroup, e.FromQQ, num);
        }
        public static void AddCount_Gacha(Group Group, QQ QQ, int num)
        {
            AddCount_Gacha(Group.Id, QQ.Id, num);
        }
        public static void AddCount_Gacha(long GroupID, long QQ, int num)
        {
            using (var db = GetInstance())
            {
                db.Ado.UseTran(() =>
                {
                    var query = GetUser(GroupID, QQ, db);
                    query.gacha_count += num;
                    db.Updateable(query).ExecuteCommand();
                });
            }
        }
        public static void AddCount_Sign(QMGroupMessageEventArgs e, int num)
        {
            AddCount_Sign(e.FromGroup, e.FromQQ, num);
        }

        public static void AddCount_Sign(Group group, QQ qq, int num)
        {
            AddCount_Sign(group.Id, qq.Id, num);
        }

        public static void AddCount_Sign(long GroupID, long QQ, int num)
        {
            using (var db = GetInstance())
            {
                db.Ado.UseTran(() =>
                {
                    var query = GetUser(GroupID, QQ, db);
                    query.sign_count += num;
                    db.Updateable(query).ExecuteCommand();
                });
            }
        }

        public static int Sign(QMGroupMessageEventArgs e)
        {
            return Sign(e.FromGroup, e.FromQQ);
        }
        public static int Sign(long GroupID, long QQ)
        {
            using (var db = GetInstance())
            {
                var result = db.Ado.UseTran(() =>
                {
                    var query = GetUser(GroupID, QQ, db);
                    long timestamp = query.timestamp.GetValueOrDefault(0);
                    DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                    TimeSpan toNow = new TimeSpan(timestamp * 10000000);
                    DateTime targetDt = dtStart.Add(toNow);
                    if (targetDt.Day != DateTime.Now.Day)
                    {
                        Random rd = new Random();
                        query.timestamp = 0;
                        query.diamond += rd.Next(PublicArgs.signmin, PublicArgs.signmax);
                        query.sign_count++;
                        db.Updateable(query).ExecuteCommand();
                        return query.diamond.GetValueOrDefault(0);
                    }
                    return -1;
                });
                if (result.IsSuccess)
                {
                    return result.Data;
                }
                else
                {
                    throw new Exception("执行错误，发生位置 Sign " + result.ErrorMessage);
                }
            }
        }
        public static void AddItem2Repositories(List<GachaResult> ls, QMGroupMessageEventArgs e)
        {
            AddItem2Repositories(ls, e.FromGroup, e.FromQQ);
        }
        public static void AddItem2Repositories(List<GachaResult> ls, Group Group, QQ QQ)
        {
            AddItem2Repositories(ls, Group.Id, QQ.Id);
        }
        public static void AddItem2Repositories(List<GachaResult> ls, long GroupID, long QQ)
        {
            using (var db = GetInstance())
            {
                db.Ado.UseTran(() =>
                {
                    foreach (var item in ls)
                    {
                        Repositories newItem = new Repositories()
                        {
                            fromgroup = GroupID,
                            qq = QQ,
                            type = item.type,
                            name = item.name,
                            class_ = item.class_,
                            level = item.level,
                            value = item.value,
                            quality = item.quality,
                            count = item.count,
                            date = DateTime.Now.ToString()
                        };
                        if (item.CanbeFold)
                        {
                            var query = db.Queryable<Repositories>().First(x => x.fromgroup == GroupID
                                    && x.qq == QQ && x.name == item.name);
                            if (query != null)
                            {
                                query.count += item.count;
                                query.date = DateTime.Now.ToString();
                                db.Updateable(query).ExecuteCommandAsync();
                            }
                            else
                            {
                                db.Updateable(newItem).ExecuteCommandAsync();
                            }
                        }
                        else
                        {
                            db.Insertable(newItem).ExecuteCommandAsync();
                        }
                    }
                });
            }
        }
        public static void CreateDB()
        {
            using (var db = GetInstance())
            {
                db.DbMaintenance.CreateDatabase();
                db.CodeFirst.InitTables(typeof(UserData));
                db.CodeFirst.InitTables(typeof(Repositories));
            }
        }
    }
}
