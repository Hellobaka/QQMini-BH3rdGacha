using SaveInfos;
using QQMini.PluginSDK.Core.Model;
using System;

namespace BH3rdGacha.OrderFunction
{
    public class Register : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_register;
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
                SQLHelper.Register(e);
                Random rd = new Random();
                int diamond = SQLHelper.GetDiamond(e);
                sendText.MsgToSend.Add(PublicArgs.register.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]")
                    .Replace("<#>", diamond.ToString()));
            }
            else
            {
                sendText.MsgToSend.Add(PublicArgs.mutiRegister.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
            }
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
