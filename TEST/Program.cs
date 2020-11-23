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
using QQMini.PluginSDK.Core.Model;
using SaveInfos;

namespace TEST
{
    public class Program
    {
        public static QQ RobotQQ { get; set; }
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            MainSave.AppDirectory = @"E:\QQMini机器人开发\QQMiniPro\Data\me.luohuaming.bh3gacha\";
            Save.AppConfig = new IniConfig(Path.Combine(MainSave.AppDirectory, "Config.ini"));
            Save.AppConfig.Load();
            Event_StartUp.Init();
            //Event_StartUp.ReadConfig();
            //BH3rdGacha.SQLHelper.Register(891787846, 863450594);
            Console.WriteLine(BH3rdGacha.SQLHelper.GetDiamond(891787846, 863450594));
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
            BH3rdGacha.SQLHelper.Sign(891787846, 863450594);
            Console.WriteLine(BH3rdGacha.SQLHelper.GetDiamond(891787846, 863450594));
            sw.Stop();
            Console.WriteLine("签到耗时：" + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            List<GachaResult> ls = new List<GachaResult>();
            for(int i = 0; i < 10; i++)
            {
                var c = MainGacha.KC_Gacha();
                Console.WriteLine(c.ToString());
                ls.Add(c);
                c = MainGacha.BP_GachaSub();
                Console.WriteLine(c.ToString());
                ls.Add(c);
            }
            CombinePng pic = new CombinePng();
            Console.WriteLine(pic.MakePic(ls, PublicArgs.PoolName.扩充, 1288, 863450594, 891787846));
            Console.WriteLine("抽卡耗时：" + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            BH3rdGacha.SQLHelper.AddItem2Repositories(ls, 891787846, 863450594);
            sw.Stop();
            Console.WriteLine("加入仓库耗时：" + sw.ElapsedMilliseconds + "ms");
            
            Console.ReadKey();
        }
    }
}
