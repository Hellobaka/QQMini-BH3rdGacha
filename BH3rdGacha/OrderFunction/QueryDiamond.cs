using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using System;

namespace BH3rdGacha.OrderFunction
{
    public class QueryDiamond : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_querydiamond;
        }
        public bool Judge(string destStr)
        {
            return destStr.Replace(" ", "") == GetOrderStr()
                && Save.AppConfig.Object["ExtraConfig"]["SwitchQueDiamond"].GetValueOrDefault("1") != "0";
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
                sendText.MsgToSend.Add(PublicArgs.noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
                return result;
            }
            int diamond = SQLHelper.GetDiamond(e);
            sendText.MsgToSend.Add(PublicArgs.queryDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]")
                .Replace("<#>", diamond.ToString()));
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
