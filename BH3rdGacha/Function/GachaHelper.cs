using QQMini.PluginSDK.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BH3rdGacha.GachaHelper
{
    public class Helper
    {
        /// <summary>
        /// 获取文字抽卡结果
        /// </summary>
        public static string TextGacha(List<GachaResult> ls)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in ls)
            {
                string type = item.type;
                switch (type)
                {
                    case "Character":
                        switch (item.class_)
                        {
                            case "S":
                                type = "[S角色卡]";
                                break;
                            case "A":
                                type = "[A角色卡]";
                                break;
                            case "B":
                                type = "[B角色卡]";
                                break;
                        }
                        break;
                    case "Weapon":
                        switch (item.evaluation)
                        {
                            case 4:
                                type = "[四星武器]";
                                break;
                            case 3:
                                type = "[三星武器]";
                                break;
                            case 2:
                                type = "[二星武器]";
                                break;
                        }
                        break;
                    case "Stigmata":
                        switch (item.evaluation)
                        {
                            case 4:
                                type = "[四星圣痕]";
                                break;
                            case 3:
                                type = "[三星圣痕]";
                                break;
                            case 2:
                                type = "[二星圣痕]";
                                break;
                        }
                        break;
                    case "Material":
                        type = "[材料]";
                        break;
                    case "debri":
                        type = "[碎片]";
                        break;
                }
                sb.AppendLine(type + item.name + $"x{item.count}");
            }
            return sb.ToString();
        }
        public static bool CheckAdmin(QMGroupMessageEventArgs e)
        {
            return CheckAdmin(e.FromGroup.Id, e.FromQQ.Id);
        }

        /// <summary>
        /// 使用群与QQ号作为限制，查询是否为管理员
        /// </summary>
        /// <param name="FromGroup"></param>
        /// <param name="FromQQ"></param>
        /// <returns></returns>
        public static bool CheckAdmin(long FromGroup, long FromQQ)
        {
            int count = Save.AppConfig.Object[$"{FromGroup}"]["Count"].GetValueOrDefault(0);
            bool flag = false;
            for (int i = 0; i < count; i++)
            {
                if (FromQQ == Save.AppConfig.Object[$"{FromGroup}"][$"Index{i}"].GetValueOrDefault((long)0))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        /// <summary>
        /// 查询群是否在配置中
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool GroupInConfig(QMGroupMessageEventArgs e)
        {
            int count = Convert.ToInt32(Save.AppConfig.Object["群控"]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                if (e.FromGroup.Id == Save.AppConfig.Object["群控"][$"Item{i}"].GetValueOrDefault((long)0))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 使用RNGCryptoServiceProvider生成种子
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            Random rd = new Random();
            byte[] bytes = new byte[rd.Next(0, 10000000)];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
