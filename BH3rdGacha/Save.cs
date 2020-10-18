using Native.Tool.IniConfig;
using QQMini.PluginSDK.Core;
using QQMini.PluginSDK.Core.Model;

namespace BH3rdGacha
{
    public static class Save
    {
        public static IniConfig AppConfig { get; set; }
        public static QMApi QMApi { get; set; }
        public static QQ RobotQQ { get; set; }
    }
}
