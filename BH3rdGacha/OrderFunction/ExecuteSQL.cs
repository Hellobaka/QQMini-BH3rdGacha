using BH3rdGacha.GachaHelper;
using QQMini.PluginSDK.Core.Model;
using System;
using SaveInfos;


namespace BH3rdGacha.OrderFunction
{
    public class ExecuteSQL : IOrderModel
    {
        public string GetOrderStr()
        {
            return "#SQL";
        }
        public bool Judge(string destStr)
        {
            return destStr.ToUpper().StartsWith(GetOrderStr());
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

            if (Save.AppConfig.Object["ExtraConfig"]["ExecuteSql"].GetValueOrDefault("0") == "0")
            {
                sendText.MsgToSend.Add($"[@{e.FromQQ.Id}]此功能未在控制台开启，拒绝操作");
                return result;
            }
            int count = Convert.ToInt32(Save.AppConfig.Object[e.FromGroup.Id.ToString()]["Count"]
                .GetValueOrDefault("0"));
            
            if (!Helper.CheckAdmin(e))
            {
                sendText.MsgToSend.Add($"[@{e.FromQQ.Id}]权限不足，拒绝操作");
                return result;
            }
            try
            {
                int countSql = SQLHelper.ExecuteSQL(e.Message.Text.Substring(4));
                sendText.MsgToSend.Add($"操作成功，{countSql}行受影响");
            }
            catch (Exception err)
            {
                sendText.MsgToSend.Add($"[@{e.FromQQ.Id}]执行失败:\n{err.Message}");
            }
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
