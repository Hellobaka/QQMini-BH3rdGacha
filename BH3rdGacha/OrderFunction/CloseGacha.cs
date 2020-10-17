using BH3rdGacha.GachaHelper;
using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using System;
using System.Collections.Generic;

namespace BH3rdGacha.OrderFunction
{
    public class CloseGacha : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_closegacha;
        }
        public bool Judge(string destStr)
        {
            return destStr.StartsWith(GetOrderStr()??Guid.NewGuid().ToString())
                && Save.AppConfig.Object["ExtraConfig"]["SwitchCloseGroup"].GetValueOrDefault("1") == "1";
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

            int count = Convert.ToInt32(Save.AppConfig.Object[e.FromGroup.Id.ToString()]["Count"]
                .GetValueOrDefault("0"));
            
            if (Helper.CheckAdmin(e))
            {
                count = Convert.ToInt32(Save.AppConfig.Object["群控"]["Count"].GetValueOrDefault("0"));
                
                if (!Helper.GroupInConfig(e))
                {
                    sendText.MsgToSend.Add($"[@{e.FromQQ.Id}] 群:{e.FromGroup.Id}已经关闭了，" +
                        $"不需要重复关闭");
                    return result;
                }
                else
                {
                    List<long> grouplist = new List<long>();
                    for (int i = 0; i < count; i++)
                    {
                        long groupid = Convert.ToInt64(Save.AppConfig.Object["群控"][$"Item{i}"]
                            .GetValueOrDefault("0"));
                        if (groupid == e.FromGroup.Id) continue;
                        grouplist.Add(groupid);
                    }
                    Save.AppConfig.Object["群控"][$"Count"] = new IValue((count - 1).ToString());
                    for (int i = 0; i < grouplist.Count; i++)
                    {
                        Save.AppConfig.Object["群控"][$"Item{i}"] = new IValue(grouplist[i].ToString());
                    }
                    Save.AppConfig.Save();
                    sendText.MsgToSend.Add($"[@{e.FromQQ.Id}] 群:{e.FromGroup.Id}已关闭");
                }
            }
            else
            {
                sendText.MsgToSend.Add($"[@{e.FromQQ.Id}]权限不足，拒绝操作");
            }
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
