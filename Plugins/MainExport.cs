using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BH3rdGacha.OrderFunction;
using Gacha.UI;
using Native.Tool.IniConfig;
using QQMini.PluginSDK.Core;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;

namespace BH3rdGacha
{
    public class MainExport : PluginBase
    {
        public override PluginInfo PluginInfo => new PluginInfo
        {
            Author = "Mr.喜",
            Description = "崩坏三图片抽卡",
            DeveloperKey = "ABC123",
            Name = "水银抽卡机",
            Version = new Version(1, 4, 0),
            SDKVersion = 3,
            PackageId = "me.luohuaming.BH3Gacha",
        };
        public override void OnInitialize()
        {
            MainSave.AppDirectory = Path.Combine(Environment.CurrentDirectory, "Data"
                , PluginInfo.PackageId.ToLower()) + "\\";
            MainSave.ImageDirectory = Path.Combine(Environment.CurrentDirectory, "Image") + "\\";
            if (!Directory.Exists(MainSave.ImageDirectory))
            {
                Directory.CreateDirectory(MainSave.ImageDirectory);
            }
            MainSave.Instances = GetInstances();
            MainSave.AppConfig = new IniConfig(MainSave.AppDirectory + "Config.ini");
            MainSave.AppConfig.Load();
            Save.AppConfig = MainSave.AppConfig;
            MainSave.AppInfo = PluginInfo;
            Thread s = new Thread(()=>
            {
                while (QMApiV2.GetFrameAllOnlineQQ().Count == 0)
                {
                    Thread.Sleep(500);
                }
                MainSave.RobotQQ = QMApiV2.GetFrameAllOnlineQQ()[0].Id;
                Event_StartUp.Init();
                Event_StartUp.ReadConfig();
            });s.Start();
        }
        public override QMEventHandlerTypes OnReceiveGroupMessage(QMGroupMessageEventArgs e)
        {
            if (MainSave.AppConfig.Object["接口"]["Group"].GetValueOrDefault(0) is 0)
            {
                bool flag = false;
                for (int i = 0; i < MainSave.AppConfig.Object["群控"]["Count"].GetValueOrDefault(0); i++)
                {
                    if (e.FromGroup.Id == MainSave.AppConfig.Object["群控"][$"Item{i}"].GetValueOrDefault(0))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag is false)
                {
                    return QMEventHandlerTypes.Continue;
                }
            }
            FunctionResult result = Event_GroupMessage.GroupMessage(e);
            if (result.SendFlag)
            {
                if (result.SendObject == null)
                {
                    return QMEventHandlerTypes.Continue;
                }
                foreach (var item in result.SendObject)
                {
                    Group DestGroup = new Group(item.SendID);
                    foreach (var sendMsg in item.MsgToSend)
                    {
                        QMApi.SendGroupMessage(e.RobotQQ, DestGroup, sendMsg);
                    }
                }
            }
            return result.result;
        }
        [STAThread]
        public override void OnOpenSettingMenu()
        {
            MainWindow fm = new MainWindow();
            fm.Show();
        }
        public override QMEventHandlerTypes OnReceiveFriendMessage(QMPrivateMessageEventArgs e)
        {
            if (MainSave.AppConfig.Object["接口"]["Private"].GetValueOrDefault(0) is 0)
            {
                return QMEventHandlerTypes.Continue;
            }
            FunctionResult result = new FunctionResult();
            if (result.SendFlag)
            {
                foreach (var item in result.SendObject)
                {
                    QQ DestFriend = new QQ(item.SendID);
                    foreach (var sendMsg in item.MsgToSend)
                    {
                        QMApi.SendFriendMessage(e.RobotQQ, DestFriend, sendMsg);
                    }
                }
            }
            return result.result;
        }
        private static List<IOrderModel> GetInstances()
        {
            List<IOrderModel> Instance = new List<IOrderModel>
            {
                new KC1(),
                new KC10(),
                new JZA1(),
                new JZA10(),
                new JZB1(),
                new JZB10(),
                new BP1(),
                new BP10(),
                new ChangePoolBegin(),
                new ChangePool(),
                new CloseGacha(),
                new ExecuteSQL(),
                new GachaUP(),
                new GetPool(),
                new GetHelp(),
                new KaKin(),
                new QueryDiamond(),
                new OrderFunction.Rank(),
                new Register(),
                new Sign(),
                new SignReset(),
                new WeekRank()
            };
            return Instance;
        }
    }
}
