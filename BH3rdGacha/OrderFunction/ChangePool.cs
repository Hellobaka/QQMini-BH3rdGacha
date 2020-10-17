using BH3rdGacha.GachaHelper;
using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;
using QQMini.PluginSDK.Core.Model;
using System;
using System.Windows.Forms;
using SaveInfos;


namespace BH3rdGacha.OrderFunction
{
    public class ChangePoolBegin : IOrderModel
    {
        public string GetOrderStr()
        {
            return "#更换池子";
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
            sendText.SendID = e.FromGroup.Id;result.SendObject.Add(sendText);

            if (Helper.CheckAdmin(e))
            {
                string option = e.Message.Text.Substring("#更换池子".Length).Trim();
                if (option == "扩充" && Save.AppConfig.Object["OCR"]["app_id"].GetValueOrDefault("") == "")
                {
                    sendText.MsgToSend.Add("参数缺失，请按照日志提示补全参数");
                    //TODO: Fix Implemented Methods
                    MessageBox.Show("参数缺失", $"请到插件数据 Config.ini 下OCR字段填写App_id与App_key。若没有可到插件论坛页面按照提示获取.");
                    return result;
                }
                sendText.MsgToSend.Add("获取中……请耐心等待");
                string str = new PaChonger().GetPoolOnline(option);
                if (string.IsNullOrEmpty(str))
                {
                    str = "查无此池";
                }
                else
                {
                    str += "立刻更改请回复#now";
                }
            }
            else
            {
                sendText.MsgToSend.Add("权限不足，拒绝操作");
            }
            return result;
        }
        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    public class ChangePool : IOrderModel
    {
        public string GetOrderStr()
        {
            return "#now";
        }
        public bool Judge(string destStr)
        {
            return destStr.ToLower() == GetOrderStr();
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

            if (Helper.CheckAdmin(e))
            {
                sendText.MsgToSend.Add(DoChange());
            }
            else
            {
                sendText.MsgToSend.Add("权限不足，拒绝操作");
            }
            return result;
        }
        /// <summary>
        /// 根据获取到的内容更换池子
        /// </summary>
        /// <returns></returns>
        string DoChange()
        {
            if (PaChonger.JZStigmata.Count == 0 && PaChonger.JZWeapon.Count == 0 &&
                PaChonger.KC.Count == 0 && PaChonger.KC.Count == 0)
            {
                return "未获取公告内容，请先执行#更换池子 关键字 功能";
            }

            string ret_Text = "已更换 ";

            string path = MainSave.AppDirectory + "\\概率\\精准概率.txt";
            IniConfig ini = new IniConfig(path);
            ini.Load();
            if (PaChonger.JZWeapon.Count != 0 && PaChonger.JZStigmata.Count != 0 &&
               PaChonger.UPAStigmata != "" && PaChonger.UPAWeapon != "")
            {
                ini.Object["详情"]["A_UpWeapon"] = new IValue(PaChonger.UPAWeapon);
                ini.Object["详情"]["A_UpStigmata"] = new IValue(PaChonger.UPAStigmata);

                int count = 0;
                for (int i = 0; i < PaChonger.JZWeapon.Count; i++)
                {
                    if (PaChonger.JZWeapon[i] == PaChonger.UPAWeapon) continue;
                    ini.Object["详情"][$"A_Weapon_Item{count}"] = new IValue(PaChonger.JZWeapon[i]);

                    count++;
                }
                count = 0;
                for (int i = 0; i < PaChonger.JZStigmata.Count; i++)
                {
                    if (PaChonger.JZStigmata[i] == PaChonger.UPAStigmata) continue;
                    ini.Object["详情"][$"A_Stigmata_Item{count}"] = new IValue(PaChonger.JZStigmata[i]);

                    count++;
                }
                ini.Save();
                ret_Text += "精准A ";
            }

            if (PaChonger.JZWeapon.Count != 0 && PaChonger.JZStigmata.Count != 0 &&
               PaChonger.UPBStigmata != "" && PaChonger.UPBWeapon != "")
            {
                ini.Object["详情"]["B_UpWeapon"] = new IValue(PaChonger.UPBWeapon);
                ini.Object["详情"]["B_UpStigmata"] = new IValue(PaChonger.UPBStigmata);

                int count = 0;
                for (int i = 0; i < PaChonger.JZWeapon.Count; i++)
                {
                    if (PaChonger.JZWeapon[i] == PaChonger.UPBWeapon) continue;
                    ini.Object["详情"][$"B_Weapon_Item{count}"] = new IValue(PaChonger.JZWeapon[i]);

                    count++;
                }
                count = 0;
                for (int i = 0; i < PaChonger.JZStigmata.Count; i++)
                {
                    if (PaChonger.JZStigmata[i] == PaChonger.UPBStigmata) continue;
                    ini.Object["详情"][$"B_Stigmata_Item{count}"] = new IValue(PaChonger.JZStigmata[i]);
                    count++;
                }
                ini.Save();
                ret_Text += "精准B ";
            }

            path = MainSave.AppDirectory + "\\概率\\扩充概率.txt";
            ini = new IniConfig(path);
            ini.Load();
            if (PaChonger.KC.Count != 0 && PaChonger.KC.Count != 0)
            {
                ini.Object["详情"]["UpS"] = new IValue(PaChonger.KC[0]);
                ini.Object["详情"]["UpA"] = new IValue(PaChonger.KC[1]);
                ini.Save();

                for (int i = 2; i < PaChonger.KC.Count; i++)
                {
                    ini.Object["详情"][$"Item{i - 2}"] = new IValue(PaChonger.KC[i]);
                    ini.Save();
                }
                ret_Text += "扩充 ";
            }
            new PaChonger().Initialize();
            return ret_Text == "已更换 " ? "获取出错" : ret_Text;
        }

        public FunctionResult Progress(QMPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
