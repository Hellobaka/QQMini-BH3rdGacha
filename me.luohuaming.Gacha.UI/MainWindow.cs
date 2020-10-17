using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BH3rdGacha.GachaHelper;
using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;
using QQMini.PluginSDK.Core.Model;
using SaveInfos;
using static SaveInfos.PublicArgs;

namespace Gacha.UI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public bool UnsaveFlag = false;
        public int LastGroupChoice;
        static string path;
        private void 抽卡_Load(object sender, EventArgs e)
        {
            if (!File.Exists($@"{MainSave.AppDirectory}装备卡\框\标配十连.png"))
            {
                MessageBox.Show("数据包未更新!前往论坛更新吧", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int count = Convert.ToInt32(MainSave.AppConfig.Object["群控"]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                listBox_Group.Items.Add(MainSave.AppConfig.Object["群控"][$"Item{i}"].GetValueOrDefault("0"));
            }
            checkBox1.Checked = (MainSave.AppConfig.Object["接口"]["Private"]
                .GetValueOrDefault("0") == "1") ? true : false;
            checkBox2.Checked = (MainSave.AppConfig.Object["接口"]["Group"]
                .GetValueOrDefault("0") == "1") ? true : false;

            checkBox_JZABaodi.Checked = (MainSave.AppConfig.Object["详情"]["A_Baodi"]
                .GetValueOrDefault("1") == "1") ? true : false;
            checkBox_JZAAt.Checked = (MainSave.AppConfig.Object["详情"]["A_ResultAt"]
                .GetValueOrDefault("0") == "1") ? true : false;

            checkBox_JingzhunBaodi.Checked = (MainSave.AppConfig.Object["详情"]["B_Baodi"]
                .GetValueOrDefault("1") == "1") ? true : false;
            checkBox_JingzhunAt.Checked = (MainSave.AppConfig.Object["详情"]["B_ResultAt"]
                .GetValueOrDefault("0") == "1") ? true : false;

            listBox_Group.SelectedIndex = (listBox_Group.Items.Count != 0) ? 0 : -1;

            checkBox_KuochongBaodi.Checked = (MainSave.AppConfig.Object["详情"]["Baodi"]
                .GetValueOrDefault("1") == "1") ? true : false;
            checkBox_KuochongAt.Checked = (MainSave.AppConfig.Object["详情"]["ResultAt"]
                .GetValueOrDefault("0") == "1") ? true : false;
            Init();
            PluginInfo appInfo = MainSave.AppInfo;
            label_NowVersion.Text = $"当前版本:{appInfo.Version}";
        }
        void Init()
        {
            textBox_KuochongUpS.Text = Texts.Text_UpS;
            textBox_KuochongUpA.Text = Texts.Text_UpA;
            listBox_Kuochong.Items.Add(Texts.Text_A1);
            listBox_Kuochong.Items.Add(Texts.Text_A2);
            listBox_Kuochong.Items.Add(Texts.Text_A3);

            textBox_JZAUpWeapon.Text = Texts.Text_UpAWeapon;
            textBox_JZAStigmataUp.Text = Texts.Text_UpAStigmata;
            listBox_JZAWeapon.Items.Add(Texts.Text_AWeapon1);
            listBox_JZAWeapon.Items.Add(Texts.Text_AWeapon2);
            listBox_JZAWeapon.Items.Add(Texts.Text_AWeapon3);
            listBox_JZAWeapon.Items.Add(Texts.Text_AWeapon4);
            listBox_JZAWeapon.Items.Add(Texts.Text_AWeapon5);
            listBox_JZAWeapon.Items.Add(Texts.Text_AWeapon6);
            listBox_JZAStigmata.Items.Add(Texts.Text_AStigmata1);
            listBox_JZAStigmata.Items.Add(Texts.Text_AStigmata2);
            listBox_JZAStigmata.Items.Add(Texts.Text_AStigmata3);
            listBox_JZAStigmata.Items.Add(Texts.Text_AStigmata4);

            textBox_JZBUpWeapon.Text = Texts.Text_UpBWeapon;
            textBox_JZBUpStigmata.Text = Texts.Text_UpBStigmata;
            listBox_JZBWeapon.Items.Add(Texts.Text_BWeapon1);
            listBox_JZBWeapon.Items.Add(Texts.Text_BWeapon2);
            listBox_JZBWeapon.Items.Add(Texts.Text_BWeapon3);
            listBox_JZBWeapon.Items.Add(Texts.Text_BWeapon4);
            listBox_JZBWeapon.Items.Add(Texts.Text_BWeapon5);
            listBox_JZBWeapon.Items.Add(Texts.Text_BWeapon6);
            listBox_JZBStigmata.Items.Add(Texts.Text_BStigmata1);
            listBox_JZBStigmata.Items.Add(Texts.Text_BStigmata2);
            listBox_JZBStigmata.Items.Add(Texts.Text_BStigmata3);
            listBox_JZBStigmata.Items.Add(Texts.Text_BStigmata4);

            IniConfig BPConfig = new IniConfig(Path.Combine(MainSave.AppDirectory, "概率", "标配概率.txt"));
            BPConfig.Load();
            int count = Convert.ToInt32(BPConfig.Object["详情_S"]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                listBox_BPS.Items.Add(BPConfig.Object["详情_S"][$"Index{i + 1}"].GetValueOrDefault(""));
            }
            count = Convert.ToInt32(BPConfig.Object["详情_A"]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                listBox_BPA.Items.Add(BPConfig.Object["详情_A"][$"Index{i + 1}"].GetValueOrDefault(""));
            }
            count = Convert.ToInt32(BPConfig.Object["详情_B"]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                listBox_BPB.Items.Add(BPConfig.Object["详情_B"][$"Index{i + 1}"].GetValueOrDefault(""));
            }
            checkBox_BPAt.Checked = (BPConfig.Object["设置"]["ResultAt"].GetValueOrDefault("0") == "1") ? true : false;
            checkBox_BPBaodi.Checked = (BPConfig.Object["设置"]["Baodi"].GetValueOrDefault("1") == "1") ? true : false;
        }
        private void button_GetKuochong_Click(object sender, EventArgs e)
        {
            textBox_KuochongProbablity.Text = Read($@"{MainSave.AppDirectory}\概率\扩充概率.txt");
        }

        public string Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string text = sr.ReadToEnd();
            sr.Close();//2020.3.16解决文件锁的bug
            return text;
        }

        private void button_JingzhunGetProbablity_Click(object sender, EventArgs e)
        {
            textBox_JingzhunProbablity.Text = Read(path);
        }

        private void button_Gacha10_Click(object sender, EventArgs e)
        {
            List<GachaResult> ls = new List<GachaResult>();
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    for (int i = 0; i < 10; i++)
                    {
                        ls.Add(MainGacha.KC_Gacha());
                        ls.Add(MainGacha.KC_GachaSub());
                    }
                    break;
                case 1:
                case 2:
                    for (int i = 0; i < 10; i++)
                    {
                        ls.Add(MainGacha.JZ_GachaMain(tabControl1.SelectedIndex == 1
                            ? PoolName.精准A : PoolName.精准B));
                        ls.Add(MainGacha.JZ_GachaMaterial());
                    }
                    break;
                case 3:
                    for (int i = 0; i < 10; i++)
                    {
                        ls.Add(MainGacha.BP_GachaMain());
                        ls.Add(MainGacha.BP_GachaSub());
                    }
                    break;
            }
            ls = ls.OrderByDescending(x => x.value).ToList();
            for (int i = 0; i < ls.Count; i++)
            {
                for (int j = i + 1; j < ls.Count; j++)
                {
                    if (ls[i].name == ls[j].name && ls[i].type != TypeS.Stigmata.ToString()
                        && ls[i].type != TypeS.Weapon.ToString())
                    {
                        ls[i].count += ls[j].count;
                        ls.RemoveAt(j);
                        i--; j--;
                        if (i == -1) i = 0;
                    }
                }
            }
            foreach (var item in ls)
            {
                listBox_Result.Items.Add(item.name + " x" + item.count);
            }
            listBox_Result.Items.Add("");
            listBox_Result.SelectedIndex = listBox_Result.Items.Count - 2;
        }
        private void button_Gacha1_Click(object sender, EventArgs e)
        {
            List<GachaResult> ls = new List<GachaResult>();
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    ls.Add(MainGacha.KC_Gacha());
                    ls.Add(MainGacha.KC_GachaSub());
                    break;
                case 1:
                case 2:
                    ls.Add(MainGacha.JZ_GachaMain(tabControl1.SelectedIndex == 1
                            ? PoolName.精准A : PoolName.精准B));
                    ls.Add(MainGacha.JZ_GachaMaterial());
                    break;
                case 3:
                    ls.Add(MainGacha.BP_GachaMain());
                    ls.Add(MainGacha.BP_GachaSub());
                    break;
            }
            listBox_Result.Items.Add(ls[ls.Count - 2].name);
            listBox_Result.Items.Add(ls[ls.Count - 1].name);
            listBox_Result.SelectedIndex = listBox_Result.Items.Count - 1;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label12.Text = "当前池:" + tabControl1.TabPages[tabControl1.SelectedIndex].Text;
        }

        private void button_KuochongPlus_Click(object sender, EventArgs e)
        {
            if (listBox_Kuochong.Items.Count >= 3) return;
            listBox_Kuochong.Items.Add(textBox_Kuochong.Text);
            textBox_Kuochong.Text = "";
        }

        private void button_KuochongSub_Click(object sender, EventArgs e)
        {
            listBox_Kuochong.Items.RemoveAt(listBox_Kuochong.SelectedIndex);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {            
            UnsaveFlag = false;
            path = Path.Combine(MainSave.AppDirectory, "概率", "扩充概率.txt");
            MainSave.AppConfig = new IniConfig(path);
            MainSave.AppConfig.Load();
            MainSave.AppConfig.Object["详情"]["Count"] = new IValue(listBox_Kuochong.Items.Count.ToString());
            MainSave.AppConfig.Object["详情"]["UpS"] = new IValue(textBox_KuochongUpS.Text);
            MainSave.AppConfig.Object["详情"]["UpA"] = new IValue(textBox_KuochongUpA.Text);
            MainSave.AppConfig.Object["详情"]["Baodi"] = new IValue((checkBox_KuochongBaodi.Checked) ? "1" : "0");
            MainSave.AppConfig.Object["详情"]["ResultAt"] = new IValue((checkBox_KuochongAt.Checked) ? "1" : "0");
            for (int i = 0; i < listBox_Kuochong.Items.Count; i++)
            {
                MainSave.AppConfig.Object["详情"][$"Item{i}"] = new IValue(listBox_Kuochong.Items[i].ToString());
            }
            MainSave.AppConfig.Save();

            path = Path.Combine(MainSave.AppDirectory, "概率", "精准概率.txt");
            MainSave.AppConfig = new IniConfig(path);
            MainSave.AppConfig.Load();
            MainSave.AppConfig.Object["详情"]["A_UpWeapon"] = new IValue(textBox_JZAUpWeapon.Text);
            MainSave.AppConfig.Object["详情"]["A_UpStigmata"] = new IValue(textBox_JZAStigmataUp.Text);
            MainSave.AppConfig.Object["详情"]["A_Baodi"] = new IValue((checkBox_JZABaodi.Checked) ? "1" : "0");
            MainSave.AppConfig.Object["详情"]["A_ResultAt"] = new IValue((checkBox_JZAAt.Checked) ? "1" : "0");
            for (int i = 0; i < listBox_JZAWeapon.Items.Count; i++)
            {
                MainSave.AppConfig.Object["详情"][$"A_Weapon_Item{i}"] = new IValue(listBox_JZAWeapon.Items[i].ToString());
            }
            for (int i = 0; i < listBox_JZAStigmata.Items.Count; i++)
            {
                MainSave.AppConfig.Object["详情"][$"A_Stigmata_Item{i}"] = new IValue(listBox_JZAStigmata.Items[i].ToString());
            }
            //MainSave.AppConfig.Object["详情"]["Count_Weapon"]=new IValue(listBox_JingzhunWeapon.Items.Count.ToString());
            //MainSave.AppConfig.Object["详情"]["Count_Stigmata"]=new IValue(listBox_JingzhunStigmata.Items.Count.ToString());
            MainSave.AppConfig.Object["详情"]["B_UpWeapon"] = new IValue(textBox_JZBUpWeapon.Text);
            MainSave.AppConfig.Object["详情"]["B_UpStigmata"] = new IValue(textBox_JZBUpStigmata.Text);
            MainSave.AppConfig.Object["详情"]["B_UpWeaponBackup"] = new IValue(textBox_JZBUpWeapon.Text);
            MainSave.AppConfig.Object["详情"]["B_UpStigmataBackup"] = new IValue(textBox_JZBUpStigmata.Text);

            MainSave.AppConfig.Object["详情"]["B_Baodi"] = new IValue((checkBox_JingzhunBaodi.Checked) ? "1" : "0");
            MainSave.AppConfig.Object["详情"]["B_ResultAt"] = new IValue((checkBox_JingzhunAt.Checked) ? "1" : "0");

            for (int i = 0; i < listBox_JZBWeapon.Items.Count; i++)
            {
                MainSave.AppConfig.Object["详情"][$"B_Weapon_Item{i}"] = new IValue(listBox_JZBWeapon.Items[i].ToString());
            }
            for (int i = 0; i < listBox_JZBStigmata.Items.Count; i++)
            {
                MainSave.AppConfig.Object["详情"][$"B_Stigmata_Item{i}"] = new IValue(listBox_JZBStigmata.Items[i].ToString());
            }
            MainSave.AppConfig.Save();

            path = Path.Combine(MainSave.AppDirectory, "Config.MainSave.AppConfig");
            MainSave.AppConfig = new IniConfig(path);
            MainSave.AppConfig.Load();
            MainSave.AppConfig.Object["群控"]["Count"] = new IValue(listBox_Group.Items.Count.ToString());
            for (int i = 0; i < listBox_Group.Items.Count; i++)
            {
                MainSave.AppConfig.Object["群控"][$"Item{i}"] = new IValue(listBox_Group.Items[i].ToString());
            }
            MainSave.AppConfig.Object["接口"]["Private"] = new IValue((checkBox1.Checked) ? "1" : "0");
            MainSave.AppConfig.Object["接口"]["Group"] = new IValue((checkBox2.Checked) ? "1" : "0");
            if (listBox_Group.SelectedIndex >= 0)
            {
                string groupid = listBox_Group.Items[listBox_Group.SelectedIndex].ToString();
                path = $@"{MainSave.AppDirectory}\Config.MainSave.AppConfig";
                MainSave.AppConfig.Object[groupid]["Count"] = new IValue(listBox_Admin.Items.Count.ToString());
                for (int i = 0; i < listBox_Admin.Items.Count; i++)
                {
                    MainSave.AppConfig.Object[groupid][$"Index{i}"] = new IValue(listBox_Admin.Items[i].ToString());
                }
            }
            MainSave.AppConfig.Save();

            path = Path.Combine(MainSave.AppDirectory, "概率", "标配概率.txt");
            MainSave.AppConfig = new IniConfig(path);
            MainSave.AppConfig.Load();

            if (listBox_BPS.Items.Count > 0)
            {
                MainSave.AppConfig.Object["设置"]["Baodi"] = new IValue((checkBox_BPBaodi.Checked) ? "1" : "0");
                MainSave.AppConfig.Object["设置"]["ResultAt"] = new IValue((checkBox_BPAt.Checked) ? "1" : "0");

                MainSave.AppConfig.Object["详情_S"]["Count"] = new IValue(listBox_BPS.Items.Count.ToString());
                MainSave.AppConfig.Object["详情_A"]["Count"] = new IValue(listBox_BPA.Items.Count.ToString());
                MainSave.AppConfig.Object["详情_B"]["Count"] = new IValue(listBox_BPB.Items.Count.ToString());

                for (int i = 0; i < listBox_BPS.Items.Count; i++)
                {
                    MainSave.AppConfig.Object["详情_S"][$"Index{i + 1}"] = new IValue(listBox_BPS.Items[i].ToString());
                }
                for (int i = 0; i < listBox_BPA.Items.Count; i++)
                {
                    MainSave.AppConfig.Object["详情_A"][$"Index{i + 1}"] = new IValue(listBox_BPA.Items[i].ToString());
                }
                for (int i = 0; i < listBox_BPB.Items.Count; i++)
                {
                    MainSave.AppConfig.Object["详情_B"][$"Index{i + 1}"] = new IValue(listBox_BPB.Items[i].ToString());
                }
            }

            MainSave.AppConfig.Save();

            MessageBox.Show("更改已保存");
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            listBox_Result.Items.Clear();
        }

        private void button_KuochongUpSPic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_KuochongUpS.Text)) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_KuoChong.Image = Image.FromFile(path + $"\\{textBox_KuochongUpS.Text}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\角色卡 下找到 {textBox_KuochongUpS.Text}.png 文件");
            }
        }

        private void button_KuochongUpAPic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_KuochongUpA.Text)) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_KuoChong.Image = Image.FromFile(path + $"\\{textBox_KuochongUpA.Text}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\角色卡 下找到 {textBox_KuochongUpA.Text}.png 文件");
            }
        }

        private void listBox_Kuochong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Kuochong.SelectedIndex < 0) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_KuoChong.Image = Image.FromFile(path + $"\\{listBox_Kuochong.Items[listBox_Kuochong.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\角色卡 下找到 {listBox_Kuochong.Items[listBox_Kuochong.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void button_JingzhunWeaponPlus_Click(object sender, EventArgs e)
        {
            if (listBox_JZBWeapon.Items.Count >= 6) return;
            listBox_JZBWeapon.Items.Add(textBox_JingzhunWeapon.Text);
            textBox_JingzhunWeapon.Text = "";
        }

        private void button_JingzhunWeaponSub_Click(object sender, EventArgs e)
        {
            if (listBox_JZBWeapon.SelectedIndex < 0) return;
            listBox_JZBWeapon.Items.RemoveAt(listBox_JZBWeapon.SelectedIndex);
        }

        private void button_JingzhunStigmataPlus_Click(object sender, EventArgs e)
        {
            if (listBox_JZBStigmata.Items.Count >= 4) return;
            listBox_JZBStigmata.Items.Add(textBox_JingzhunStigmata.Text);
            textBox_JingzhunStigmata.Text = "";
        }

        private void button_JingzhunStigmataSub_Click(object sender, EventArgs e)
        {
            if (listBox_JZBStigmata.SelectedIndex < 0) return;
            listBox_JZBStigmata.Items.RemoveAt(listBox_JZBStigmata.SelectedIndex);
        }

        private void listBox_JingzhunWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_JZBWeapon.SelectedIndex < 0) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\武器";
            try
            {
                pictureBox_JingZhun.Image = Image.FromFile(path + $"\\{listBox_JZBWeapon.Items[listBox_JZBWeapon.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\武器 下找到 {listBox_JZBWeapon.Items[listBox_JZBWeapon.SelectedIndex].ToString()}.png 文件");
            }
        }

        private void listBox_JingzhunStigmata_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox_JZBStigmata.SelectedIndex < 0) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\圣痕卡";
            try
            {
                pictureBox_JingZhun.Image = Image.FromFile(path + $"\\{listBox_JZBStigmata.Items[listBox_JZBStigmata.SelectedIndex].ToString()}上.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\圣痕卡 下找到 {listBox_JZBStigmata.Items[listBox_JZBStigmata.SelectedIndex].ToString()}上.png 文件");
            }

        }

        private void button_JingzhunUpStigmataPic_Click(object sender, EventArgs e)
        {
            string path = $@"{MainSave.AppDirectory}\装备卡\圣痕卡\{textBox_JZBUpStigmata.Text}上.png";

            try
            {
                pictureBox_JingZhun.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\圣痕卡 下找到 {textBox_JZBUpStigmata.Text}上.png 文件");
            }
        }

        private void button_JingzhunUpWeaponPic_Click(object sender, EventArgs e)
        {
            string path = $@"{MainSave.AppDirectory}\装备卡\武器\{textBox_JZBUpWeapon.Text}.png";
            try
            {
                pictureBox_JingZhun.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\武器 下找到 {textBox_JZBUpWeapon.Text}.png 文件");
            }
        }

        private void button_GroupPlus_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt64(textBox_Group.Text);
            }
            catch
            {
                MessageBox.Show("群格式有误");
                return;
            }
            listBox_Group.Items.Add(textBox_Group.Text);
            textBox_Group.Text = "";
        }

        private void button_GroupSub_Click(object sender, EventArgs e)
        {
            if (listBox_Group.SelectedIndex < 0) return;
            listBox_Group.Items.RemoveAt(listBox_Group.SelectedIndex);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnswerDIY fm = new AnswerDIY();
            fm.Show();
        }

        private void button_AdminPlus_Click(object sender, EventArgs e)
        {
            UnsaveFlag = true;
            try
            {
                Convert.ToInt64(textBox_Admin.Text);
            }
            catch
            {
                MessageBox.Show("QQ格式有误");
                return;
            }
            listBox_Admin.Items.Add(textBox_Admin.Text);
            textBox_Admin.Text = "";
        }

        bool tempflag = true;

        private void listBox_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnsaveFlag && tempflag)
            {
                if (MessageBox.Show("当前群的更改未保存，是否继续?", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    tempflag = false;
                    listBox_Group.SelectedIndex = LastGroupChoice;
                    return;
                }
            }
            UnsaveFlag = false;
            tempflag = true;
            if (listBox_Group.SelectedIndex < 0) return;
            LastGroupChoice = listBox_Group.SelectedIndex;
            listBox_Admin.Items.Clear();
            path = Path.Combine(MainSave.AppDirectory, "Config.MainSave.AppConfig");
            MainSave.AppConfig = new IniConfig(path);
            MainSave.AppConfig.Load();
            string groupid = listBox_Group.Items[listBox_Group.SelectedIndex].ToString();
            label16.Text = $"编辑群:{groupid} 群管中";
            int count = Convert.ToInt32(MainSave.AppConfig.Object[groupid]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                listBox_Admin.Items.Add(MainSave.AppConfig.Object[groupid][$"Index{i}"].GetValueOrDefault("0"));
            }
        }

        private void button_AdminSub_Click(object sender, EventArgs e)
        {
            UnsaveFlag = true;
            if (listBox_Admin.SelectedIndex < 0) return;
            listBox_Admin.Items.RemoveAt(listBox_Admin.SelectedIndex);
        }

        private void 批量导入群列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ImportGroupList fm = new ImportGroupList();
            //fm.Show();
        }

        private void button_JZAWeaponUpPic_Click(object sender, EventArgs e)
        {
            string path = $@"{MainSave.AppDirectory}\装备卡\武器\{textBox_JZAUpWeapon.Text}.png";
            try
            {
                pictureBox_JZA.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\武器 下找到 {textBox_JZAUpWeapon.Text}.png 文件");
            }

        }

        private void button_JZAStigmataUpPic_Click(object sender, EventArgs e)
        {
            string path = $@"{MainSave.AppDirectory}\装备卡\圣痕卡\{textBox_JZAStigmataUp.Text}上.png";
            try
            {
                pictureBox_JZA.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\圣痕卡 下找到 {textBox_JZAStigmataUp.Text}上.png 文件");
            }

        }

        private void button_JZAWeaponPlus_Click(object sender, EventArgs e)
        {
            if (listBox_JZAWeapon.Items.Count >= 6) return;
            listBox_JZAWeapon.Items.Add(textBox_JZAWeapon.Text);
            textBox_JZAWeapon.Text = "";
        }

        private void button_JZAWeaponSub_Click(object sender, EventArgs e)
        {
            if (listBox_JZAWeapon.SelectedIndex < 0) return;
            listBox_JZAWeapon.Items.RemoveAt(listBox_JZAWeapon.SelectedIndex);
        }

        private void listBox_JZAWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_JZAWeapon.SelectedIndex < 0) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\武器";
            try
            {
                pictureBox_JZA.Image = Image.FromFile(path + $"\\{listBox_JZAWeapon.Items[listBox_JZAWeapon.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\武器 下找到 {listBox_JZAWeapon.Items[listBox_JZAWeapon.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void button_JZAStigmataPlus_Click(object sender, EventArgs e)
        {
            if (listBox_JZAStigmata.Items.Count >= 4) return;
            listBox_JZAStigmata.Items.Add(textBox_JZAStigmata.Text);
            textBox_JZAStigmata.Text = "";
        }

        private void button_JZAStigmataSub_Click(object sender, EventArgs e)
        {
            if (listBox_JZAStigmata.SelectedIndex < 0) return;
            listBox_JZAStigmata.Items.RemoveAt(listBox_JZAStigmata.SelectedIndex);
        }

        private void listBox_JZAStigmata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_JZAStigmata.SelectedIndex < 0) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\圣痕卡";
            try
            {
                pictureBox_JZA.Image = Image.FromFile(path + $"\\{listBox_JZAStigmata.Items[listBox_JZAStigmata.SelectedIndex].ToString()}上.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\圣痕卡 下找到 {listBox_JZAStigmata.Items[listBox_JZAStigmata.SelectedIndex].ToString()}上.png 文件");
            }
        }

        private void button_JZAGetProbablity_Click(object sender, EventArgs e)
        {
            textBox_JZAProbablity.Text = Read(path);
        }

        private void button_BPSPlus_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (var item in listBox_BPS.Items)
            {
                if (item.ToString() == textBox_BPS.Text)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                listBox_BPS.Items.Add(textBox_BPS.Text);
            }
            else
            {
                MessageBox.Show($"{textBox_BPS.Text} 项目已存在");
            }
            textBox_BPS.Text = "";
        }

        private void button_BPSSub_Click(object sender, EventArgs e)
        {
            listBox_BPS.Items.RemoveAt(listBox_BPS.SelectedIndex);
        }

        private void button_BPAPlus_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (var item in listBox_BPA.Items)
            {
                if (item.ToString() == textBox_BPA.Text)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                listBox_BPA.Items.Add(textBox_BPA.Text);
            }
            else
            {
                MessageBox.Show($"{textBox_BPA.Text} 项目已存在");
            }
            textBox_BPA.Text = "";
        }

        private void button_BPASub_Click(object sender, EventArgs e)
        {
            listBox_BPA.Items.RemoveAt(listBox_BPA.SelectedIndex);
        }

        private void button_BPBPlus_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (var item in listBox_BPB.Items)
            {
                if (item.ToString() == textBox_BPB.Text)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                listBox_BPB.Items.Add(textBox_BPB.Text);
            }
            else
            {
                MessageBox.Show($"{textBox_BPB.Text} 项目已存在");
            }
            textBox_BPB.Text = "";
        }

        private void button_BPBSub_Click(object sender, EventArgs e)
        {
            listBox_BPB.Items.RemoveAt(listBox_BPB.SelectedIndex);
        }

        private void listBox_BPS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_BPS.SelectedIndex >= 0)
            {
                int temp = listBox_BPS.SelectedIndex;
                button_BPSSub.PerformClick();
                if (temp == 0) return;
                listBox_BPS.SelectedIndex = temp - 1;
            }
        }

        private void listBox_BPA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_BPA.SelectedIndex >= 0)
            {
                int temp = listBox_BPA.SelectedIndex;
                button_BPASub.PerformClick();
                if (temp == 0) return;
                listBox_BPA.SelectedIndex = temp - 1;
            }
        }

        private void listBox_BPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_BPB.SelectedIndex >= 0)
            {
                int temp = listBox_BPB.SelectedIndex;
                button_BPBSub.PerformClick();
                if (temp == 0) return;
                listBox_BPB.SelectedIndex = temp - 1;
            }
        }

        private void listBox_JingzhunWeapon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_JZBWeapon.SelectedIndex >= 0)
            {
                int temp = listBox_JZBWeapon.SelectedIndex;
                button_JingzhunWeaponSub.PerformClick();
                if (temp == 0) return;
                listBox_JZBWeapon.SelectedIndex = temp - 1;
            }
        }

        private void listBox_JingzhunStigmata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_JZBStigmata.SelectedIndex >= 0)
            {
                int temp = listBox_JZBStigmata.SelectedIndex;
                button_JingzhunStigmataSub.PerformClick();
                if (temp == 0) return;
                listBox_JZBStigmata.SelectedIndex = temp - 1;
            }
        }

        private void listBox_JZAWeapon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_JZAWeapon.SelectedIndex >= 0)
            {
                int temp = listBox_JZAWeapon.SelectedIndex;
                button_JZAWeaponSub.PerformClick();
                if (temp == 0) return;
                listBox_JZAWeapon.SelectedIndex = temp - 1;
            }
        }

        private void listBox_JZAStigmata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_JZAStigmata.SelectedIndex >= 0)
            {
                int temp = listBox_JZAStigmata.SelectedIndex;
                button_JZAStigmataSub.PerformClick();
                if (temp == 0) return;
                listBox_JZAStigmata.SelectedIndex = temp - 1;
            }
        }

        private void listBox_Kuochong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_Kuochong.SelectedIndex >= 0)
            {
                int temp = listBox_Kuochong.SelectedIndex;
                button_KuochongSub.PerformClick();
                if (temp == 0) return;
                listBox_Kuochong.SelectedIndex = temp - 1;
            }
        }

        private void listBox_BPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_BPS.SelectedIndex < 0) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_BP.Image = Image.FromFile(path + $"\\{listBox_BPS.Items[listBox_BPS.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\角色卡 下找到 {listBox_BPS.Items[listBox_BPS.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void listBox_BPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_BPA.SelectedIndex < 0) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_BP.Image = Image.FromFile(path + $"\\{listBox_BPA.Items[listBox_BPA.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\角色卡 下找到 {listBox_BPA.Items[listBox_BPA.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void listBox_BPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_BPB.SelectedIndex < 0) return;
            string path = $@"{MainSave.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_BP.Image = Image.FromFile(path + $"\\{listBox_BPB.Items[listBox_BPB.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{MainSave.AppDirectory}\装备卡\角色卡 下找到 {listBox_BPB.Items[listBox_BPB.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void textBox_BPS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_BPS.Text))
            {

                button_BPSPlus.PerformClick();
                listBox_BPS.SelectedIndex = listBox_BPS.Items.Count - 1;
            }
        }

        private void textBox_BPA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_BPA.Text))
            {

                button_BPAPlus.PerformClick();
                listBox_BPA.SelectedIndex = listBox_BPA.Items.Count - 1;
            }
        }

        private void textBox_BPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_BPB.Text))
            {

                button_BPBPlus.PerformClick();
                listBox_BPB.SelectedIndex = listBox_BPB.Items.Count - 1;
            }
        }

        private void textBox_JingzhunWeapon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_JingzhunWeapon.Text))
            {

                button_JingzhunWeaponPlus.PerformClick();
                listBox_JZBWeapon.SelectedIndex = listBox_JZBWeapon.Items.Count - 1;
            }
        }

        private void textBox_JingzhunStigmata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_JingzhunStigmata.Text))
            {
                button_JingzhunStigmataPlus.PerformClick();
                listBox_JZBStigmata.SelectedIndex = listBox_JZBStigmata.Items.Count - 1;
            }
        }

        private void textBox_JZAWeapon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_JZAWeapon.Text))
            {
                button_JZAWeaponPlus.PerformClick();
                listBox_JZAWeapon.SelectedIndex = listBox_JZAWeapon.Items.Count - 1;
            }
        }

        private void textBox_JZAStigmata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_JZAStigmata.Text))
            {
                button_JZAStigmataPlus.PerformClick();
                listBox_JZAStigmata.SelectedIndex = listBox_JZAStigmata.Items.Count - 1;
            }
        }

        private void textBox_KuochongA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_Kuochong.Text))
            {
                button_KuochongPlus.PerformClick();
                listBox_Kuochong.SelectedIndex = listBox_Kuochong.Items.Count - 1;
            }
        }

        private void textBox_BPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_BPB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_BPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_JingzhunWeapon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_JingzhunStigmata_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_JZAWeapon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_JZAStigmata_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_Kuochong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_Group_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_Admin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_Group_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_Group.Text))
            {
                button_GroupPlus.PerformClick();
            }
        }

        private void textBox_Admin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_Admin.Text))
            {
                button_AdminPlus.PerformClick();
            }
        }

        private void listBox_Group_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_Group.SelectedIndex >= 0)
            {
                int temp = listBox_Group.SelectedIndex;
                button_GroupSub.PerformClick();
                if (temp == 0) return;
                listBox_Group.SelectedIndex = temp - 1;
            }
        }

        private void listBox_Admin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_Admin.SelectedIndex >= 0)
            {
                int temp = listBox_Admin.SelectedIndex;
                button_AdminSub.PerformClick();
                if (temp == 0) return;
                listBox_Admin.SelectedIndex = temp - 1;
            }
        }

        private void button_GetUpdate_Click(object sender, EventArgs e)
        {
            label_NewVersion.Visible = true;
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                GetUpdate_button();
            }).Start();
            //MessageBox.Show("开始拉取版本号，请耐心等待，不要关闭控制台");
        }

        void GetUpdate_button()
        {
            GetUpdate getUpdate = new GetUpdate();
            try
            {
                GetUpdate.Update update = getUpdate.GetVersion();
                label_NewVersion.Text = $"最新版本:{update.GachaVersion}";
                if (update.GachaVersion != MainSave.AppInfo.Version.ToString())
                {
                    MessageBox.Show($"有更新了！\n\n新版本:{update.GachaVersion}\n\n更新时间:{update.Date}\n\n更新内容:{update.Whatsnew}\n\n前往论坛或者GitHub下载吧", "有新版本", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("当前就是最新了哦");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"拉取版本号失败\n错误信息:{e.Message}\n请稍后再试");
            }
        }

        private void 关于界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aboutme fm = new Aboutme();
            fm.Show();
        }

        private void 扩展设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtraConfig fm = new ExtraConfig();
            fm.Show();
        }

        private void AbyssHelper_Click(object sender, EventArgs e)
        {
            AbyssHelper fm = new AbyssHelper();
            fm.Show();
        }
    }
}
