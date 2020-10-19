using System;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using QQMini.PluginSDK.Core;

namespace BH3rdGacha
{
    public class Event_GroupMessage
    {
        public static FunctionResult GroupMessage(QMGroupMessageEventArgs e)
        {
            FunctionResult result = new FunctionResult()
            {
                SendFlag = false
            };
            try
            {                
                foreach (var item in MainSave.Instances)
                {
                    if (!item.Judge(e.Message.Text))
                    {
                        continue;
                    }
                    return item.Progress(e);
                }
                return result;
            }
            catch (Exception exc)
            {
                QMLog.CurrentApi.Info(exc.Message + exc.StackTrace);
                return result;
            }
        }
    }
}
