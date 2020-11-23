using Native.Tool.IniConfig;
using QQMini.PluginSDK.Core;
using QQMini.PluginSDK.Core.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SaveInfos
{
    public static class MainSave
    {
        public static string AppDirectory { get; set; }
        public static string ImageDirectory { get; set; }
        public static IniConfig AppConfig { get; set; }
        public static QMApi QMApi { get; set; }
        public static QQ RobotQQ { get; set; }
        public static List<IOrderModel> Instances { get; set; }
        public static PluginInfo AppInfo { get; set; }
        /// <summary>
        /// 使用RNGCryptoServiceProvider生成种子
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            Random rd = new Random();
            byte[] bytes = new byte[rd.Next(0, 10000000)];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        //获取时间戳
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        //MD5
        public static string GetMD5(string str)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(str);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
