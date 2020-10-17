using BH3rdGacha.GachaHelper;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using System;

namespace BH3rdGacha.OrderFunction
{
    public class WeekRank : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_WeekRank;
        }
        public bool Judge(string destStr)
        {
            return destStr.Replace(" ", "") == GetOrderStr();
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
            sendText.MsgToSend.Add(BH3rdGacha.Rank.WeekRank.GetWeekRank(e));
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
