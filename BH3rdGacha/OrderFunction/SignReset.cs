using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using System;

namespace BH3rdGacha.OrderFunction
{
    public class SignReset : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_signreset;
        }
        public bool Judge(string destStr)
        {
            return destStr.Replace(" ", "") == GetOrderStr()
                && Save.AppConfig.Object["ExtraConfig"]["SwitchResSign"].GetValueOrDefault("1") != "0";
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
            bool InGroup = false;
            for (int i = 0; i < count; i++)
            {
                if (Save.AppConfig.Object[e.FromGroup.Id.ToString()][$"Index{i}"].GetValueOrDefault("0")
                    == e.FromQQ.Id.ToString())
                {
                    InGroup = true;
                    break;
                }
            }
            if (InGroup)
            {
                SQLHelper.SignReset(e);
                Random rd = new Random();
                switch (rd.Next(0, 6))
                {
                    case 0:
                        sendText.MsgToSend.Add(PublicArgs. reset1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
                        break;
                    case 1:
                        sendText.MsgToSend.Add(PublicArgs. reset2.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
                        break;
                    case 2:
                        sendText.MsgToSend.Add(PublicArgs.reset3.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
                        break;
                    case 3:
                        sendText.MsgToSend.Add(PublicArgs. reset4.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
                        break;
                    case 4:
                        sendText.MsgToSend.Add(PublicArgs. reset5.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
                        break;
                    case 5:
                        sendText.MsgToSend.Add(PublicArgs.reset6.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
                        break;
                }
            }
            else
            {
                sendText.MsgToSend.Add("只有管♂理员才能这么做");
            }
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            return new FunctionResult { SendFlag = false };
        }
    }
}
