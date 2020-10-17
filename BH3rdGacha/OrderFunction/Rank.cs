using SaveInfos;
using BH3rdGacha.Rank;
using QQMini.PluginSDK.Core.Model;
using System;

namespace BH3rdGacha.OrderFunction
{
    public class Rank : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_Rank;
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
            sendText.MsgToSend.Add(TotalRank.GetRank(e));
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
