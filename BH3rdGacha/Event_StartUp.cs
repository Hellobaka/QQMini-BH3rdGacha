using System;
using System.IO;
using Gacha.UI;
using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;
using QQMini.PluginSDK.Core;
using SaveInfos;

namespace BH3rdGacha
{
    public class Event_StartUp
    {
        public static void Init()
        {
            try
            {
                IniConfig ini;
                ini = new IniConfig(Path.Combine(MainSave.AppDirectory,"Config.ini"));
                ini.Load();
                string temp = ini.Object["OCR"]["app_id"].GetValueOrDefault("");
                if (temp == "")
                {
                    ini.Object["OCR"]["app_id"] = new IValue("");
                    ini.Object["OCR"]["app_key"] = new IValue("");
                }
                ini.Save();

                if (!File.Exists($@"{MainSave.AppDirectory}装备卡\框\抽卡背景.png"))
                {
                    QMLog.CurrentApi.Info("数据包未安装，插件无法运行，请仔细阅读论坛插件说明安装数据包，之后重载插件");
                }
                else
                {
                    if (!File.Exists(Path.Combine(MainSave.AppDirectory, "data.db")))
                    {
                        SQLHelper.CreateDB();
                    }
                    else
                    {
                        SQLHelper.Init(Path.Combine(MainSave.AppDirectory, "data.db"));
                    }
                }
                AbyssTimerHelper.Start();
                MainGacha.Init();
            }
            catch(Exception exc)
            {
                QMLog.CurrentApi.Info(exc.Message+exc.StackTrace);
            }
        }
        /// <summary>
        /// 抽卡指令与回答初始化
        /// </summary>
        /// <param name="e"></param>
        public static void ReadConfig()
        {
            PublicArgs.order_KC1 = Save.AppConfig.Object["Order"]["KC1"].GetValueOrDefault("#扩充单抽");
            PublicArgs.order_KC10 = Save.AppConfig.Object["Order"]["KC10"].GetValueOrDefault("#扩充十连");
            PublicArgs.order_JZA1 = Save.AppConfig.Object["Order"]["JZA1"].GetValueOrDefault("#精准单抽A");
            PublicArgs.order_JZA10 = Save.AppConfig.Object["Order"]["JZA10"].GetValueOrDefault("#精准十连A");
            PublicArgs.order_JZB1 = Save.AppConfig.Object["Order"]["JZB1"].GetValueOrDefault("#精准单抽B");
            PublicArgs.order_JZB10 = Save.AppConfig.Object["Order"]["JZB10"].GetValueOrDefault("#精准十连B");
            PublicArgs.order_BP1 = Save.AppConfig.Object["Order"]["BP1"].GetValueOrDefault("#标配单抽");
            PublicArgs.order_BP10 = Save.AppConfig.Object["Order"]["BP10"].GetValueOrDefault("#标配十连");

            PublicArgs.order_register = Save.AppConfig.Object["Order"]["Register"].GetValueOrDefault("#抽卡注册");
            PublicArgs.order_sign = Save.AppConfig.Object["Order"]["Sign"].GetValueOrDefault("#打扫甲板");
            PublicArgs.order_signreset = Save.AppConfig.Object["Order"]["SignReset"]
                .GetValueOrDefault("#甲板积灰");
            PublicArgs.order_querydiamond = Save.AppConfig.Object["Order"]["QueryDiamond"]
                .GetValueOrDefault("#我的水晶");
            PublicArgs.order_help = Save.AppConfig.Object["Order"]["Help"].GetValueOrDefault("#抽卡帮助");
            PublicArgs.order_getpool = Save.AppConfig.Object["Order"]["GetPool"].GetValueOrDefault("#获取池子");
            PublicArgs.order_closegacha = Save.AppConfig.Object["Order"]["CloseGacha"]
                .GetValueOrDefault("#抽卡关闭");
            PublicArgs.order_opengacha = Save.AppConfig.Object["Order"]["OpenGacha"]
                .GetValueOrDefault("#抽卡开启");
            PublicArgs.order_Rank = Save.AppConfig.Object["Order"]["Rank"].GetValueOrDefault("#排行榜");
            PublicArgs.order_WeekRank = Save.AppConfig.Object["Order"]["WeekRank"]
                .GetValueOrDefault("#周榜");

            PublicArgs.KC1 = Save.AppConfig.Object["Answer"]["KC1"].GetValueOrDefault("少女祈祷中……")
                .Replace("\\n", "\n");
            PublicArgs.KC10 = Save.AppConfig.Object["Answer"]["KC10"].GetValueOrDefault("少女祈祷中……")
                .Replace("\\n", "\n");
            PublicArgs.JZA1 = Save.AppConfig.Object["Answer"]["JZA1"].GetValueOrDefault("少女祈祷中……")
                .Replace("\\n", "\n");
            PublicArgs.JZA10 = Save.AppConfig.Object["Answer"]["JZA10"].GetValueOrDefault("少女祈祷中……")
                .Replace("\\n", "\n");
            PublicArgs.JZB1 = Save.AppConfig.Object["Answer"]["JZB1"].GetValueOrDefault("少女祈祷中……")
                .Replace("\\n", "\n");
            PublicArgs.JZB10 = Save.AppConfig.Object["Answer"]["JZB10"].GetValueOrDefault("少女祈祷中……")
                .Replace("\\n", "\n");
            PublicArgs.BP1 = Save.AppConfig.Object["Answer"]["BP1"].GetValueOrDefault("少女祈祷中……")
                .Replace("\\n", "\n");
            PublicArgs.BP10 = Save.AppConfig.Object["Answer"]["BP10"].GetValueOrDefault("少女祈祷中……")
                .Replace("\\n", "\n");

            PublicArgs.register = Save.AppConfig.Object["Answer"]["Register"]
                .GetValueOrDefault("<@>欢迎上舰，这是你的初始资源(<#>)水晶").Replace("\\n", "\n");
            PublicArgs.mutiRegister = Save.AppConfig.Object["Answer"]["MutiRegister"]
                .GetValueOrDefault("重复注册是不行的哦").Replace("\\n", "\n");
            PublicArgs.sign1 = Save.AppConfig.Object["Answer"]["Sign1"]
                .GetValueOrDefault("大姐你回来了，天气这么好一起多逛逛吧").Replace("\\n", "\n");
            PublicArgs.sign2 = Save.AppConfig.Object["Answer"]["Sign2"]
                .GetValueOrDefault("<@>这是你今天清扫甲板的报酬，拿好(<#>水晶)").Replace("\\n", "\n");
            PublicArgs.mutiSign = Save.AppConfig.Object["Answer"]["MutiSign"]
                .GetValueOrDefault("今天的甲板挺亮的，擦一遍就行了").Replace("\\n", "\n");
            PublicArgs.noReg = Save.AppConfig.Object["Answer"]["NoReg"]
                .GetValueOrDefault("<@>不是清洁工吧？来输入#抽卡注册 来上舰").Replace("\\n", "\n");
            PublicArgs.lowDiamond = Save.AppConfig.Object["Answer"]["LowDiamond"]
                .GetValueOrDefault("<@>水晶不足，无法进行抽卡，你还剩余<#>水晶").Replace("\\n", "\n");
            PublicArgs.queryDiamond = Save.AppConfig.Object["Answer"]["QueryDiamond"]
                .GetValueOrDefault("<@>你手头还有<#>水晶").Replace("\\n", "\n");
            PublicArgs.help = Save.AppConfig.Object["Answer"]["Help"]
                .GetValueOrDefault(@"水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n#氪金 目标账号或者at 数量(管理员限定 暂不支持自定义修改)\n#获取池子\n\n#精准单抽(A/B)大小写随意\n#扩充单抽\n#精准十连(A/B)大小写随意\n#扩充十连\n#标配单抽\n#标配十连\n#抽卡开启(在后台群后面可接群号)\n#抽卡关闭(在后台群后面可接群号)\n#置抽卡管理(示例:#置抽卡管理,群号,QQ或者at)\n#更换池子 查询公告的关键字\n#抽干家底 扩充或者精准A/B")
                    .Replace("\\", @"\");

            PublicArgs.reset1 = Save.AppConfig.Object["Answer"]["Reset1"]
                .GetValueOrDefault("贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n");
            PublicArgs.reset2 = Save.AppConfig.Object["Answer"]["Reset2"]
                .GetValueOrDefault("草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n");
            PublicArgs.reset3 = Save.AppConfig.Object["Answer"]["Reset3"]
                .GetValueOrDefault("你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n");
            PublicArgs.reset4 = Save.AppConfig.Object["Answer"]["Reset4"]
                .GetValueOrDefault("由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n");
            PublicArgs.reset5 = Save.AppConfig.Object["Answer"]["Reset5"]
                .GetValueOrDefault("理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n");
            PublicArgs.reset6 = Save.AppConfig.Object["Answer"]["Reset6"]
                .GetValueOrDefault("希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n");

            PublicArgs.registermin = Convert.ToInt32(Save.AppConfig.Object["GetDiamond"]["RegisterMin"]
                .GetValueOrDefault("0"));
            PublicArgs.registermax = Convert.ToInt32(Save.AppConfig.Object["GetDiamond"]["RegisterMax"]
                .GetValueOrDefault("14000"));
            PublicArgs.signmin = Convert.ToInt32(Save.AppConfig.Object["GetDiamond"]["SignMin"]
                .GetValueOrDefault("0"));
            PublicArgs.signmax = Convert.ToInt32(Save.AppConfig.Object["GetDiamond"]["SignMax"]
                .GetValueOrDefault("14000"));
        }
    }
}
