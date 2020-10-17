using BH3rdGacha.GachaHelper;
using Native.Tool.IniConfig;
using QQMini.PluginSDK.Core;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using System.Collections.Generic;

namespace BH3rdGacha
{
    public static class Save
    {
        public static IniConfig AppConfig { get; set; }
        public static QMApi QMApi { get; set; }
        public static QQ RobotQQ { get; set; }
    }
}
