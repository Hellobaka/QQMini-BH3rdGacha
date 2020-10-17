using SaveInfos;
using QQMini.PluginSDK.Core.Model;
using System;

namespace BH3rdGacha.OrderFunction
{
    public class Sign : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_sign;
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

            bool exist = SQLHelper.IDExist(e);
            if (!exist)
            {
                sendText.MsgToSend.Add(PublicArgs.noReg.Replace("<@>", $"[@{e.FromQQ.Id}]"));
                return result;
            }

            int diamond = SQLHelper.Sign(e);
            SQLHelper.AddCount_Sign(e,1);
            if (diamond >= 0)
            {
                sendText.MsgToSend.Add(PublicArgs.sign1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]")
                    .Replace("<#>", diamond.ToString()));
                sendText.MsgToSend.Add(PublicArgs.sign2.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]")
                    .Replace("<#>", diamond.ToString()));
            }
            else
            {
                sendText.MsgToSend.Add(PublicArgs.mutiSign.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]")
                    .Replace("<#>", diamond.ToString()));
            }
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
