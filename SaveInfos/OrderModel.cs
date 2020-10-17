using QQMini.PluginSDK.Core.Model;

namespace SaveInfos
{
    public interface IOrderModel
    {
        string GetOrderStr();
        bool Judge(string destStr);
        FunctionResult Progress(QMGroupMessageEventArgs e);
        FunctionResult Progress(QMPrivateMessageEventArgs e);
    }
}
