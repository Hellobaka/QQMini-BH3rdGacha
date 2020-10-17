using QQMini.PluginSDK.Core.Model;
using System.Collections.Generic;

namespace SaveInfos
{
    /// <summary>
    /// 函数处理结果
    /// </summary>
    public class FunctionResult
    {
        /// <summary>
        /// 用于发送的文本
        /// </summary>
        public List<SendText> SendObject { get; set; } = new List<SendText>();
        /// <summary>
        /// 标志本次函数处理阻塞或者是继续
        /// </summary>
        public QMEventHandlerTypes result { get; set; }
        /// <summary>
        /// 发送消息标识
        /// </summary>
        public bool SendFlag { get; set; }
    }
    public class SendText
    {
        /// <summary>
        /// 每消息执行一次发送
        /// </summary>
        public List<string> MsgToSend { get; set; } = new List<string>();
        public long SendID { get; set; }
    }
}
