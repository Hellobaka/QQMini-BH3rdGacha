using BH3rdGacha.GachaHelper;
using QQMini.PluginSDK.Core.Model;
using System;
using SaveInfos;


namespace BH3rdGacha.OrderFunction
{
    public class KaKin : IOrderModel
    {
        public string GetOrderStr()
        {
            return "#氪金";
        }
        public bool Judge(string destStr)
        {
            return destStr.StartsWith(GetOrderStr())
                && Save.AppConfig.Object["ExtraConfig"]["SwitchKaKin"].GetValueOrDefault("1") != "0";
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
            
            if (!Helper.CheckAdmin(e))
            {
                sendText.MsgToSend.Add("氪金的权利掌握在管理员手里[Face178.gif]");
                return result;
            }
            string[] temp = e.Message.Text.Split(' ');
            if (temp.Length != 3)
            {
                sendText.MsgToSend.Add($"[@{e.FromQQ.Id}] 输入的格式不正确！" +
                    $"请按照 #氪金 目标QQ号或者at目标 数量 的格式填写");
                return result;
            }
            else
            {
                try
                {
                    long targetId = Convert.ToInt64(temp[1].Replace("[@", "").Replace("]", ""));
                    int countdia = Convert.ToInt32(temp[2]);
                    try
                    {
                        if (!SQLHelper.IDExist(e.FromGroup.Id, targetId))
                        {
                            sendText.MsgToSend.Add("操作对象不存在");
                            return result;
                        }
                        SQLHelper.AddDiamond(e.FromGroup.Id, targetId, countdia);
                        sendText.MsgToSend.Add($"操作成功,为[@{targetId}]充值{countdia}水晶" +
                            $",剩余{SQLHelper.GetDiamond(e.FromGroup.Id, targetId)}水晶");
                        return result;
                    }
                    catch
                    {
                        sendText.MsgToSend.Add("操作失败了……");
                        return result ;
                    }
                }
                catch
                {
                    sendText.MsgToSend.Add($"[@{e.FromQQ.Id}] 输入的格式不正确！" +
                        $"请按照格式输入纯数字");
                    return result;
                }
            }
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
