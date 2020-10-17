using BH3rdGacha.CustomPool;
using Newtonsoft.Json;
using QQMini.PluginSDK.Core.Model;
using System;
using System.IO;
using SaveInfos;


namespace BH3rdGacha.OrderFunction
{
    public class CustomGacha : IOrderModel
    {
        public string GetOrderStr()
        {
            return null;
        }

        public bool Judge(string destStr)
        {
            if (!File.Exists(Path.Combine(MainSave.AppDirectory, "CustomPool", "pool.json")))
                return false;
            CustomPool.CustomPool pool = JsonConvert.DeserializeObject<CustomPool.CustomPool>
                (File.ReadAllText(Path.Combine(MainSave.AppDirectory, "CustomPool", "pool.json")));
            foreach (var item in pool.Infos)
            {
                if (destStr == item.OneOrder || destStr == item.TenOrder)
                {
                    return true;
                }
            }
            return false;
        }

        public FunctionResult Progress(QMGroupMessageEventArgs e)
        {
            FunctionResult result = new FunctionResult
            {
                result = QMEventHandlerTypes.Intercept,
                SendFlag = true
            };
            SendText sendText = new SendText();
            sendText.SendID = e.FromGroup.Id;
            result.SendObject.Add(sendText); 
            if (!File.Exists(Path.Combine(MainSave.AppDirectory, "CustomPool", "pool.json")))
            {
                result.SendFlag = false;
                result.result = QMEventHandlerTypes.Continue;
                return result; 
            }
            CustomPool.CustomPool pool = JsonConvert.DeserializeObject<CustomPool.CustomPool>
                (File.ReadAllText(Path.Combine(MainSave.AppDirectory, "CustomPool", "pool.json")));
            foreach (var item in pool.Infos)
            {
                if (e.Message.Text == item.OneOrder)
                {
                    if (CustomGachaHelper.CanGacha(e, item, 1))
                    {
                        sendText.MsgToSend.Add(CustomGachaHelper.GetPicPath(1, item, e)); 
                    }
                }
                else if (e.Message.Text == item.TenOrder)
                {
                    if (CustomGachaHelper.CanGacha(e, item, 10))
                    {
                        sendText.MsgToSend.Add(CustomGachaHelper.GetPicPath(10, item, e)); 
                    }
                }
            }
            return result;
        }

        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
