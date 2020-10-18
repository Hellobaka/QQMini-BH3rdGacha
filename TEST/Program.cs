using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH3rdGacha;
using BH3rdGacha.GachaHelper;
using Native.Tool.IniConfig;
using SaveInfos;

namespace TEST
{
    public class Program
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            MainSave.AppDirectory = @"E:\QQMini机器人开发\QQMiniPro\Data\me.luohuaming.bh3gacha\";
            Save.AppConfig = new IniConfig(Path.Combine(MainSave.AppDirectory, "Config.ini"));
            Save.AppConfig.Load();
            Event_StartUp.Init();
            Event_StartUp.ReadConfig();
            sw.Stop();
            Console.WriteLine("初始化耗时："+sw.ElapsedMilliseconds+"ms");
            sw.Restart();
            BH3rdGacha.SQLHelper.IDExist(891787846, 863450594);
            sw.Stop();
            Console.WriteLine("查询存在耗时：" + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            Console.WriteLine(BH3rdGacha.SQLHelper.GetDiamond(891787846, 863450594));
            sw.Stop();
            Console.WriteLine("查询钻石耗时：" + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            List<GachaResult> ls = new List<GachaResult>();
            for(int i = 0; i < 10; i++)
            {
                ls.Add(MainGacha.KC_Gacha());
                ls.Add(MainGacha.KC_GachaSub());
            }
            Console.WriteLine("抽卡耗时：" + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            BH3rdGacha.SQLHelper.AddItem2Repositories(ls, 891787846, 863450594);
            sw.Stop();
            Console.WriteLine("加入仓库耗时：" + sw.ElapsedMilliseconds + "ms");
            
            Console.ReadKey();

        }
    }
}
