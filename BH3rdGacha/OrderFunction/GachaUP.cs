using BH3rdGacha.GachaHelper;
using Native.Tool.IniConfig;
using QQMini.PluginSDK.Core;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH3rdGacha.OrderFunction
{
    public class GachaUP : IOrderModel
    {
        public string GetOrderStr()
        {
            return "#抽干家底";
        }

        public bool Judge(string destStr)
        {
            return destStr.StartsWith(GetOrderStr());
        }

        public FunctionResult Progress(QMGroupMessageEventArgs e)
        {
            FunctionResult result = new FunctionResult
            {
                result = QMEventHandlerTypes.Intercept,
                SendFlag = true
            };
            SendText sendText = new SendText();
            sendText.SendID = e.FromGroup.Id; result.SendObject.Add(sendText);

            if (e.Message.Text.Trim() == GetOrderStr())
            {
                sendText.MsgToSend.Add($"[@{e.FromQQ.Id}]请指定要抽取的池子，扩充或者精准A/B");
                return result;
            }
            string order = e.Message.Text.Substring(GetOrderStr().Length).Trim().ToUpper().Replace(" ", "");
            if (order == "扩充")
            {
                if (!SQLHelper.IDExist(e))
                {
                    sendText.MsgToSend.Add(PublicArgs.noReg.Replace("<@>", $"[@{e.FromQQ.Id}]"));
                    return result;
                }

                IniConfig ini = new IniConfig(MainSave.AppDirectory + "概率\\扩充概率.txt");
                ini.Load();
                QMApi.CurrentApi.SendGroupMessage(e.RobotQQ, e.FromGroup
                    , $"正在抽干家底……抽到<{ini.Object["详情"]["UpS"].GetValueOrDefault("UPS角色")}>就会收手");

                int diamond = SQLHelper.GetDiamond(e);
                List<GachaResult> ls = new List<GachaResult>();
                int count = 0;
                for (int i = 0; i < diamond / 280; i++)
                {
                    ls.Add(MainGacha.KC_Gacha());
                    ls.Add(MainGacha.KC_GachaSub());
                    count++;
                    if (ls.FindIndex(x => x.class_ == "S") != -1)
                    {
                        break;
                    }
                }
                ls = ls.OrderByDescending(x => x.value).ToList();
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = i + 1; j < ls.Count; j++)
                    {
                        if (ls[i].name == ls[j].name && ls[i].type != PublicArgs.TypeS.Character.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                SQLHelper.AddItem2Repositories(ls, e);
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = i + 1; j < ls.Count; j++)
                    {
                        if (ls[i].name == ls[j].name)
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                SQLHelper.SubDiamond(e, count * 280);
                SQLHelper.AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, count);
                if (ls.FindIndex(x => x.class_ == "S") == -1)
                {
                    sendText.MsgToSend.Add($"今天不适合你抽卡……\n" +
                        $"合计:抽取{count}次\n消耗了{count * 280}水晶 " +
                        $"折合大约是{count * 280 / 7640 + 1}单648[CQ:face,id=67]");
                }
                else
                {
                    sendText.MsgToSend.Add($"抽到啦！\n合计:抽取{count}次\n" +
                        $"消耗了{count * 280}水晶 折合大约是{count * 280 / 7640 + 1}单648\n٩( 'ω' )و");
                }
                string items = "获取到的物品如下:\n";
                int count_purple = 0;
                foreach (var item in ls)
                {
                    if (item.type == PublicArgs.TypeS.Character.ToString())
                    {
                        items += $"{item.name} ×{item.count}\n";
                        count_purple += item.count;
                    }
                }
                items += $"出货率为{(double)count_purple / count * 100:f2}%\n平均每10发抽到紫{(double)count_purple / count * 10:f2}个";
                sendText.MsgToSend.Add(items);
                GC.Collect();
                return result;
            }
            else if (order.Contains("精准"))
            {
                if (!SQLHelper.IDExist(e))
                {
                    sendText.MsgToSend.Add(PublicArgs.noReg.Replace("<@>", $"[@{e.FromQQ.Id}]"));
                    return result;
                }
                string pool = order.Substring("精准".Length);
                QMApi.CurrentApi.SendGroupMessage(e.RobotQQ, e.FromGroup, $"正在抽干家底……" +
                    $"抽到{pool}池毕业就会收手");

                int diamond = SQLHelper.GetDiamond(e);
                string UPWeapon, UPStigmata;
                UPWeapon = pool == "A" ? Texts.Text_UpAWeapon : Texts.Text_UpBWeapon;
                UPStigmata = pool == "A" ? Texts.Text_UpAStigmata : Texts.Text_UpBStigmata;

                List<GachaResult> ls = new List<GachaResult>();
                int count = 0;
                for (int i = 0; i < diamond / 280; i++)
                {
                    ls.Add(MainGacha.JZ_GachaMain(pool == "A"
                        ? PublicArgs.PoolName.精准A : PublicArgs.PoolName.精准B));
                    ls.Add(MainGacha.JZ_GachaMaterial());
                    count++;
                    if (ls.Exists(x => x.name == UPWeapon) && ls.Exists(x => x.name == UPStigmata + "上")
                        && ls.Exists(x => x.name == UPStigmata + "中") && ls.Exists(x => x.name == UPStigmata + "下"))
                    {
                        break;
                    }
                }
                ls = ls.OrderByDescending(x => x.value).ToList();
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = i + 1; j < ls.Count; j++)
                    {
                        if (ls[i].name == ls[j].name && ls[i].type != PublicArgs.TypeS.Stigmata.ToString()
                            && ls[i].type != PublicArgs.TypeS.Weapon.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                SQLHelper.AddItem2Repositories(ls, e);
                ls = ls.OrderByDescending(x => x.name).ToList();
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = i + 1; j < ls.Count; j++)
                    {
                        if (ls[i].name == ls[j].name)
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                SQLHelper.SubDiamond(e, count * 280);
                SQLHelper.AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, count);

                if (ls.Exists(x => x.name == UPWeapon) && ls.Exists(x => x.name == UPStigmata + "上")
                    && ls.Exists(x => x.name == UPStigmata + "中") && ls.Exists(x => x.name == UPStigmata + "下"))
                {
                    sendText.MsgToSend.Add($"抽到啦！\n合计:抽取{count}次\n" +
                        $"消耗了{count * 280}水晶 折合大约是{count * 280 / 7640 + 1}单648\n٩( 'ω' )و");
                }
                else
                {
                    sendText.MsgToSend.Add($"嘛，精准池不毕业挺正常……\n合计:抽取{count}次\n" +
                        $"消耗了{count * 280}水晶 折合大约是{count * 280 / 7640 + 1}单648[CQ:face,id=67]");
                }
                string items = "获取到的物品如下:\n";
                int count_purple = 0;
                foreach (var item in ls)
                {
                    if ((item.type == PublicArgs.TypeS.Weapon.ToString() && item.quality == 2)
                        || (item.type == PublicArgs.TypeS.Stigmata.ToString() && item.quality == 2))
                    {
                        items += $"{item.name} ×{item.count}\n";
                        count_purple += item.count;
                    }
                }
                items += $"出货率为{(double)count_purple / count * 100:f2}%\n平均每10发抽到紫{(double)count_purple / count * 10:f2}个";
                sendText.MsgToSend.Add(items);

                GC.Collect();
                return result;
            }
            return result;
        }

        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
