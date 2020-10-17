using BH3rdGacha.GachaHelper;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using System;

namespace BH3rdGacha.OrderFunction
{
    public class GetPool : IOrderModel
    {
        public string GetOrderStr()
        {
            return PublicArgs.order_getpool;
        }
        public bool Judge(string destStr)
        {
            return destStr.Replace(" ", "") == GetOrderStr()
                && Save.AppConfig.Object["ExtraConfig"]["SwitchGetPool"].GetValueOrDefault("1") != "0";
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

            sendText.MsgToSend.Add($"当前扩充池为 {Texts.Text_UpS} {Texts.Text_UpA}\n" +
                $"当前精准A池为 {Texts.Text_UpAWeapon} {Texts.Text_UpAStigmata}\n" +
                $"当前精准B池为 {Texts.Text_UpBWeapon} {Texts.Text_UpBStigmata}");
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
