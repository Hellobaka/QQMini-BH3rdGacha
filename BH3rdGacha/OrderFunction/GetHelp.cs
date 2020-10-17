using SaveInfos;
using QQMini.PluginSDK.Core.Model;
using System;

namespace BH3rdGacha.OrderFunction
{
    public class GetHelp : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_help;
        }
        public bool Judge(string destStr)
        {
            return destStr.Replace(" ", "") == GetOrderStr()
                && Save.AppConfig.Object["ExtraConfig"]["SwitchGetHelp"].GetValueOrDefault("1") != "0";
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
            sendText.MsgToSend.Add(PublicArgs.help.Replace("\\n","\n"));
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
