using BH3rdGacha.GachaHelper;
using Native.Tool.IniConfig;
using QQMini.PluginSDK.Core;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using System;
using System.Collections.Generic;

namespace BH3rdGacha.OrderFunction
{
    public class JZB1 : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_JZB1;
        }
        public bool Judge(string destStr)
        {
            return destStr.Replace(" ", "").ToUpper() == GetOrderStr()
                && Save.AppConfig.Object["ExtraConfig"]["SwitchJZB1"].GetValueOrDefault("1") != "0";
        }
        public FunctionResult Progress(QMGroupMessageEventArgs e)
        {
            FunctionResult result = new FunctionResult
            {
                result = QMEventHandlerTypes.Intercept,
                SendFlag = true
            };
            SendText sendText = new SendText();
            sendText.SendID = e.FromGroup.Id;result.SendObject.Add(sendText);

            bool exist = SQLHelper.IDExist(e);
            if (!exist)
            {
                sendText.MsgToSend.Add(PublicArgs.noReg.Replace("<@>", $"[@{e.FromQQ.Id}]"));
                return result;
            }
            int diamond = SQLHelper.GetDiamond(e);
            if (diamond < 280)
            {
                sendText.MsgToSend.Add(PublicArgs.lowDiamond.Replace("<@>", $"[@{e.FromQQ.Id}]")
                    .Replace("<#>", diamond.ToString()));
                return result;
            }
            QMApi.CurrentApi.SendGroupMessage(e.RobotQQ, e.FromGroup, PublicArgs.JZB1
                .Replace("<@>", $"[@{e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
            List<GachaResult> ls = new List<GachaResult>
            {
                MainGacha.JZ_GachaMain(PublicArgs.PoolName.精准B),
                MainGacha.JZ_GachaMaterial()
            };
            SQLHelper.AddItem2Repositories(ls, e);
            CombinePng cp = new CombinePng();
            SQLHelper.SubDiamond(e, 280);
            SQLHelper.AddCount_Gacha(e, 1);

            IniConfig ini = new IniConfig($@"{MainSave.AppDirectory}概率\精准概率.txt");
            ini.Load();
            sendText.MsgToSend.Add(ini.Object["详情"]["B_ResultAt"].GetValueOrDefault("0") == "1"
                ? $"[@{e.FromQQ.Id}]" : ""
                + Save.AppConfig.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1"
                ? Helper.TextGacha(ls) : $"[pic=" +
                    $"{cp.MakePic(ls, PublicArgs.PoolName.精准B, diamond - 280, e.FromQQ.Id, e.FromGroup.Id)}]");
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
