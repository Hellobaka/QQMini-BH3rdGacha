using System;
using System.IO;
using BH3rdGacha.GachaHelper;
using Native.Tool.IniConfig;

namespace SaveInfos
{
    //这代码……实话说，我真不想改了
    public class MainGacha
    {
        static IniConfig ini;
        static string path;
        static int Count_JZ;
        static int Count;
        static bool GetTargetItem;
        public static GachaResult KC_Gacha()
        {
            Random rd = new Random(MainSave.GetRandomSeed());
            double pro_1 = rd.Next(0, 1000000) / (double)10000;

            path = Path.Combine(MainSave.AppDirectory, "概率", "扩充概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            Count++;
        jumpin:
            GachaResult gr = new GachaResult();
            //
            if (Count < 10)
            {
                if (pro_1 < Prop.Probablity_KC角色卡)
                {
                    GetTargetItem = true;
                    Count = 0;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Prop.Probablity_UpS + Prop.Probablity_UpA + Prop.Probablity_A) * 1000)) / (double)1000;
                    if (pro_0 < Prop.Probablity_UpS)
                    {
                        gr.name = Texts.Text_UpS;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Character.ToString();
                        gr.class_ = "S";
                        gr.quality = 2;
                        return gr;
                    }
                    else if (pro_0 < Prop.Probablity_UpA + Prop.Probablity_UpS)
                    {
                        gr.name = Texts.Text_UpA;
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Character.ToString();
                        gr.class_ = "A";
                        gr.quality = 2;

                        return gr;
                    }
                    else
                    {
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Character.ToString();
                        gr.class_ = "A";
                        gr.quality = 2;
                        int temp = rd.Next(0, 4);
                        if (temp == 0)
                        {
                            gr.name = Texts.Text_A1;
                            return gr;
                        }
                        if (temp == 1)
                        {
                            gr.name = Texts.Text_A2;
                            return gr;
                        }
                        gr.name = Texts.Text_A3;
                        return gr;
                    }
                }
                else if (pro_1 < Prop.Probablity_KC角色卡 + Prop.Probablity_KC角色碎片)
                {
                    if (rd.Next(0, Convert.ToInt32((Prop.Probablity_UpS + Prop.Probablity_UpA + Prop.Probablity_A) * 1000)) / (double)1000 < Prop.Probablity_Sdebris)
                    {
                        gr.name = Texts.Text_UpS + "碎片";
                        gr.value = 2700;
                        gr.count = rd.Next(7, 10);
                        gr.type = PublicArgs.TypeS.debri.ToString();
                        gr.quality = 2;

                        return gr;
                    }
                    else
                    {
                        gr.value = 2600;
                        gr.count = rd.Next(4, 9);
                        gr.type = PublicArgs.TypeS.debri.ToString();
                        int temp = rd.Next(4);
                        if (temp == 0) { gr.name = Texts.Text_UpA + "碎片"; return gr; }
                        if (temp == 1) { gr.name = Texts.Text_A1 + "碎片"; return gr; }
                        if (temp == 2) { gr.name = Texts.Text_A2 + "碎片"; return gr; }
                        gr.name = Texts.Text_A3 + "碎片";
                        gr.quality = 2;

                        return gr;
                    }
                }
                else
                {
                    double pro_2 = rd.Next(0, Convert.ToInt32((Prop.Probablity_KC材料) * 1000)) / (double)1000;
                    if (pro_2 < Prop.Probablity_Material技能材料)
                    {
                        gr.name = "高级技能材料";
                        gr.value = 1000;
                        gr.count = rd.Next(4, 7);
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;
                        return gr;
                    }
                    else if (pro_2 < Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
                    {
                        gr.name = "超小型反应炉";
                        gr.value = 800;
                        gr.count = rd.Next(4, 7);
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 1;
                        gr.evaluation = 3;

                        return gr;
                    }
                    else if (pro_2 < Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
                    {
                        double pro_4 = rd.NextDouble();
                        if (pro_4 <= 0.33)
                        {
                            gr.name = "特级学习材料";
                            gr.value = 900;
                            gr.count = rd.Next(5, 9);
                            gr.type = PublicArgs.TypeS.Material.ToString();
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                        else
                        {
                            gr.name = "高级学习材料";
                            gr.value = 800;
                            gr.count = rd.Next(5, 9);
                            gr.type = PublicArgs.TypeS.Material.ToString();
                            gr.quality = 1;
                            gr.evaluation = 3;

                            return gr;
                        }
                    }
                    else if (pro_2 < Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
                    {
                        gr.name = "吼咪宝藏";
                        gr.value = 700;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else if (pro_2 < Prop.Probablity_Material吼美宝藏 + Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
                    {
                        gr.name = "吼美宝藏";
                        gr.value = 600;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else
                    {
                        gr.name = "吼里宝藏";
                        gr.value = 500;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 1;
                        gr.evaluation = 3;

                        return gr;
                    }
                }
            }
            else
            {
                if (ini.Object["详情"]["Baodi"].GetValueOrDefault("1") == "1")
                {
                    Count = 0;
                    GetTargetItem = false;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Prop.Probablity_KC角色卡) * 1000)) / (double)1000;
                    if (pro_0 < Prop.Probablity_UpS)
                    {
                        gr.name = Texts.Text_UpS;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.class_ = "S";
                        gr.quality = 2;

                        gr.type = PublicArgs.TypeS.Character.ToString();
                        return gr;
                    }
                    else if (pro_0 < Prop.Probablity_UpA + Prop.Probablity_UpS)
                    {
                        gr.name = Texts.Text_UpA;
                        gr.value = 2800;
                        gr.count = 1;
                        gr.class_ = "A";
                        gr.quality = 2;

                        gr.type = PublicArgs.TypeS.Character.ToString();
                        return gr;
                    }
                    else
                    {
                        gr.value = 2800;
                        gr.count = 1;
                        gr.class_ = "A";
                        gr.quality = 2;

                        gr.type = PublicArgs.TypeS.Character.ToString();
                        int temp = rd.Next(0, 4);
                        if (temp == 0)
                        {
                            gr.name = Texts.Text_A1;
                            return gr;
                        }
                        if (temp == 1)
                        {
                            gr.name = Texts.Text_A2;
                            return gr;
                        }
                        gr.name = Texts.Text_A3;
                        return gr;
                    }
                }
                else
                {
                    Count--;
                    goto jumpin;
                }
            }
        }
        public static GachaResult KC_GachaSub()
        {
            GachaResult gr = new GachaResult();
            Random rd = new Random(MainSave.GetRandomSeed());
            double pro_2 = rd.Next(0, 100000) / (double)1000;

            path = Path.Combine(MainSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            if (pro_2 < Prop.Probablity_Material技能材料)
            {
                gr.name = "高级技能材料";
                gr.value = 1000;
                gr.count = rd.Next(2, 4);
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;
                return gr;
            }
            else if (pro_2 < Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                gr.name = "超小型反应炉";
                gr.value = 800;
                gr.count = rd.Next(2, 4);
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 1;
                gr.evaluation = 3;

                return gr;
            }
            else if (pro_2 < Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                double pro_4 = rd.NextDouble();
                if (pro_4 <= 0.33)
                {
                    gr.name = "特级学习材料";
                    gr.value = 900;
                    gr.count = rd.Next(3, 6);
                    gr.type = PublicArgs.TypeS.Material.ToString();
                    gr.quality = 2;
                    gr.evaluation = 4;

                    return gr;
                }
                else
                {
                    gr.name = "高级学习材料";
                    gr.value = 800;
                    gr.count = rd.Next(3, 6);
                    gr.type = PublicArgs.TypeS.Material.ToString();
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
            }
            else if (pro_2 < Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                gr.name = "吼咪宝藏";
                gr.value = 700;
                gr.count = 1;
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_2 < Prop.Probablity_Material吼美宝藏 + Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                gr.name = "吼美宝藏";
                gr.value = 600;
                gr.count = 1;
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_2 < Prop.Probablity_Material吼姆宝藏 + Prop.Probablity_Material吼美宝藏 + Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                gr.name = "吼里宝藏";
                gr.value = 500;
                gr.count = 1;
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 1;
                gr.evaluation = 3;
                return gr;
            }
            else
            {
                double pro_0 = rd.Next(0, 10000) / (double)100;
                if (pro_0 <= 50)
                {
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Weapon2"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Weapon2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星武器");
                    gr.value = 2500;
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Weapon.ToString();
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 2;

                    return gr;
                }
                else
                {
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Stigmata2"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Stigmata2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星圣痕");
                    switch (rd.Next(0, 3))
                    {
                        case 0:
                            gr.name += "上";
                            break;
                        case 1:
                            gr.name += "中";
                            break;
                        case 2:
                            gr.name += "下";
                            break;
                    }
                    gr.value = 2400;
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Stigmata.ToString();
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 2;

                    return gr;
                }
            }

        }
        public static GachaResult JZ_GachaMain(PublicArgs.PoolName name)
        {
            Random rd = new Random(MainSave.GetRandomSeed());
            double pro_1 = rd.Next(0, 10000000) / (double)100000;
            Count_JZ++;

            path = Path.Combine(MainSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();

        jumpin:
            GachaResult gr = new GachaResult();

            if (Count_JZ < 10)
            {
                if (pro_1 < Prop.Probablity_Weapon4Total)
                {
                    GetTargetItem = true;
                    Count_JZ = 0;
                    double pro_2 = rd.Next(0, Convert.ToInt32((Prop.Probablity_Weapon4Total) * 100000)) / (double)100000;
                    if (pro_2 < Prop.Probablity_JZUpWeapon4)
                    {
                        gr.name = name== PublicArgs.PoolName.精准A ? Texts.Text_UpAWeapon : Texts.Text_UpBWeapon;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.level = 10;
                        gr.type = PublicArgs.TypeS.Weapon.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;
                        return gr;
                    }
                    else
                    {
                        double pro_3 = rd.Next(0, 6);
                        gr.value = 27000;
                        gr.count = 1;
                        gr.level = 1;
                        gr.type = PublicArgs.TypeS.Weapon.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        switch (pro_3)
                        {
                            case 0:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon1 : Texts.Text_BWeapon1;
                                break;
                            case 1:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon2 : Texts.Text_BWeapon2;
                                break;
                            case 2:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon3 : Texts.Text_BWeapon3;
                                break;
                            case 3:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon4 : Texts.Text_BWeapon4;
                                break;
                            case 4:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon5 : Texts.Text_BWeapon5;
                                break;
                            case 5:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon6 : Texts.Text_BWeapon6;
                                break;
                        }
                        return gr;
                    }
                }
                else if (pro_1 < Prop.Probablity_Stigmata4Total + Prop.Probablity_Weapon4Total)
                {
                    GetTargetItem = true;
                    Count_JZ = 0;
                    rd = new Random(MainSave.GetRandomSeed());
                    double pro_2 = rd.Next(0, Convert.ToInt32((Prop.Probablity_JZUpStigmata4 + Prop.Probablity_JZStigmata4) * 100000)) / (double)100000;
                    if (pro_2 < Prop.Probablity_JZUpStigmata4)
                    {
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_UpAStigmata : Texts.Text_UpBStigmata
                                    + "上";
                                break;
                            case 1:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_UpAStigmata : Texts.Text_UpBStigmata 
                                    + "中";
                                break;
                            case 2:
                                gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_UpAStigmata : Texts.Text_UpBStigmata 
                                    + "下";
                                break;
                        }
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Stigmata.ToString();
                        gr.level = 10;
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else
                    {
                        string temp = "";
                        switch (rd.Next(0, 4))
                        {
                            case 0:
                                temp = name == PublicArgs.PoolName.精准A ? Texts.Text_AStigmata1 : Texts.Text_BStigmata1;
                                break;
                            case 1:
                                temp = name == PublicArgs.PoolName.精准A ? Texts.Text_AStigmata2 : Texts.Text_BStigmata2;
                                break;
                            case 2:
                                temp = name == PublicArgs.PoolName.精准A ? Texts.Text_AStigmata3 : Texts.Text_BStigmata3;
                                break;
                            case 3:
                                temp = name == PublicArgs.PoolName.精准A ? Texts.Text_AStigmata4 : Texts.Text_BStigmata4;
                                break;
                        }
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name = temp + "上";
                                break;
                            case 1:
                                gr.name = temp + "中";
                                break;
                            case 2:
                                gr.name = temp + "下";
                                break;
                        }
                        gr.value = 2700;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Stigmata.ToString();
                        gr.level = 1;
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                }
                else if (pro_1 < Prop.Probablity_Weapon3Total + Prop.Probablity_Weapon4Total + Prop.Probablity_Stigmata4Total)
                {
                    int count = 0;
                    try
                    {
                        count = Convert.ToInt32(ini.Object["详情"]["Count_Weapon3"].GetValueOrDefault("0"));
                    }
                    catch
                    {
                        count = 0;
                    }
                    gr.name = ini.Object["详情"][$"Weapon3_Item{rd.Next(0, count)}"].GetValueOrDefault("3星武器");
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Weapon.ToString();
                    gr.value = 26000;
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
                else if (pro_1 < Prop.Probablity_Stigmata3Total + Prop.Probablity_Weapon3Total + Prop.Probablity_Weapon4Total + Prop.Probablity_Stigmata4Total)
                {
                    int count = 0;
                    try
                    {
                        count=Convert.ToInt32(ini.Object["详情"]["Count_Stigmata3"].GetValueOrDefault("0"));
                    }
                    catch
                    {
                        count = 0;
                    }
                    gr.name = ini.Object["详情"][$"Stigmata3_Item{rd.Next(0, count)}"].GetValueOrDefault("3星圣痕");
                    switch (rd.Next(0, 3))
                    {
                        case 0:
                            gr.name += "上";
                            break;
                        case 1:
                            gr.name += "中";
                            break;
                        case 2:
                            gr.name += "下";
                            break;
                    }
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Stigmata.ToString();
                    gr.value = 2600;
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
                else
                {
                    double pro_0 = rd.Next(0, 10000) / (double)100;
                    if (pro_0 <= 50)
                    {
                        int count = Convert.ToInt32(ini.Object["详情"]["Count_Weapon2"].GetValueOrDefault("0"));
                        gr.name = ini.Object["详情"][$"Weapon2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星武器");
                        gr.value = 25000;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Weapon.ToString();
                        gr.level = 1;
                        gr.quality = 1;
                        gr.evaluation = 2;

                        return gr;
                    }
                    else
                    {
                        int count = Convert.ToInt32(ini.Object["详情"]["Count_Stigmata2"].GetValueOrDefault("0"));
                        gr.name = ini.Object["详情"][$"Stigmata2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星圣痕");
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name += "上";
                                break;
                            case 1:
                                gr.name += "中";
                                break;
                            case 2:
                                gr.name += "下";
                                break;
                        }
                        gr.value = 2500;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Stigmata.ToString();
                        gr.level = 1;
                        gr.quality = 1;
                        gr.evaluation = 2;

                        return gr;
                    }
                }
            }
            else
            {
                if (ini.Object["详情"]["Baodi"].GetValueOrDefault("1") == "1")
                {
                    Count_JZ = 0;
                    GetTargetItem = false;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Prop.Probablity_Weapon4Total + Prop.Probablity_Stigmata4Total) * 1000000)) / (double)1000000;
                    if (pro_0 < Prop.Probablity_Weapon4Total)
                    {
                        double pro_2 = rd.Next(0, Convert.ToInt32((Prop.Probablity_Weapon4Total) * 1000000)) / (double)1000000;
                        if (pro_2 < Prop.Probablity_JZUpWeapon4)
                        {
                            gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_UpAWeapon : Texts.Text_UpBWeapon;
                            gr.value = 28000;
                            gr.count = 1;
                            gr.type = PublicArgs.TypeS.Weapon.ToString();
                            gr.level = 10;
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                        else
                        {
                            double pro_3 = rd.Next(0, 6);
                            gr.value = 27000;
                            gr.count = 1;
                            gr.type = PublicArgs.TypeS.Weapon.ToString();
                            gr.level = 1;
                            gr.quality = 2;
                            gr.evaluation = 4;

                            switch (pro_3)
                            {
                                case 0:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon1 : Texts.Text_BWeapon1;
                                    break;
                                case 1:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon2 : Texts.Text_BWeapon2;
                                    break;
                                case 2:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon3 : Texts.Text_BWeapon3;
                                    break;
                                case 3:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon4 : Texts.Text_BWeapon4;
                                    break;
                                case 4:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon5 : Texts.Text_BWeapon5;
                                    break;
                                case 5:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_AWeapon6 : Texts.Text_BWeapon6;
                                    break;
                            }
                            return gr;
                        }
                    }
                    else
                    {
                        if (rd.Next(0, Convert.ToInt32((Prop.Probablity_Stigmata4Total) * 1000000)) / (double)1000000 < Prop.Probablity_JZUpWeapon4)
                        {
                            switch (rd.Next(0, 3))
                            {
                                case 0:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_UpAStigmata : Texts.Text_UpBStigmata
                                        + "上";
                                    break;
                                case 1:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_UpAStigmata : Texts.Text_UpBStigmata
                                        + "中";
                                    break;
                                case 2:
                                    gr.name = name == PublicArgs.PoolName.精准A ? Texts.Text_UpAStigmata : Texts.Text_UpBStigmata
                                        + "下";
                                    break;
                            }
                            gr.value = 20000;
                            gr.count = 1;
                            gr.type = PublicArgs.TypeS.Stigmata.ToString();
                            gr.level = 10;
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                        else
                        {
                            string temp = "";
                            switch (rd.Next(0, 4))
                            {
                                case 0:
                                    temp = name == PublicArgs.PoolName.精准A ? Texts.Text_AStigmata1 : Texts.Text_BStigmata1;
                                    break;
                                case 1:
                                    temp = name == PublicArgs.PoolName.精准A ? Texts.Text_AStigmata2 : Texts.Text_BStigmata2;
                                    break;
                                case 2:
                                    temp = name == PublicArgs.PoolName.精准A ? Texts.Text_AStigmata3 : Texts.Text_BStigmata3;
                                    break;
                                case 3:
                                    temp = name == PublicArgs.PoolName.精准A ? Texts.Text_AStigmata4 : Texts.Text_BStigmata4;
                                    break;
                            }
                            switch (rd.Next(0, 3))
                            {
                                case 0:
                                    gr.name = temp + "上";
                                    break;
                                case 1:
                                    gr.name = temp + "中";
                                    break;
                                case 2:
                                    gr.name = temp + "下";
                                    break;
                            }
                            gr.value = 19000;
                            gr.count = 1;
                            gr.type = PublicArgs.TypeS.Stigmata.ToString();
                            gr.level = 1;
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                    }
                }
                else
                {
                    Count_JZ--;
                    goto jumpin;
                }
            }
        }
        public static GachaResult JZ_GachaMaterial()
        {
            Random rd = new Random(MainSave.GetRandomSeed());
            double pro_0 = Prop.Probablity_JZ装备经验 + Prop.Probablity_JZ通用进化材料 + Prop.Probablity_JZGold;
            double pro_1 = rd.Next(0, Convert.ToInt32(pro_0 * 100)) / (double)100;
            GachaResult gr = new GachaResult();

            path = Path.Combine(MainSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            if (pro_1 < Prop.Probablity_JZ通用进化材料)
            {
                gr.name = "相转移镜面";
                gr.count = 1;
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.value = 1200;
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_1 < Prop.Probablity_JZ装备经验 + Prop.Probablity_JZ通用进化材料)
            {
                double db = rd.NextDouble();
                gr.name = (db <= 0.5) ? "双子灵魂结晶" : "灵魂结晶";
                gr.count = 1;
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.value = (db <= 0.5) ? 1100 : 1000;
                gr.quality = (db <= 0.5) ? 2 : 1;
                gr.evaluation = (db <= 0.5) ? 4 : 3;
                return gr;
            }
            else
            {
                double pro_2 = rd.Next(0, Convert.ToInt32((Prop.Probablity_JZGold) * 1000)) / (double)1000;
                if (pro_2 < Prop.Probablity_Material吼咪宝藏)
                {
                    gr.name = "吼咪宝藏";
                    gr.value = 700;
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Material.ToString();
                    gr.quality = 2;
                    gr.evaluation = 4;

                    return gr;
                }
                else if (pro_2 < Prop.Probablity_Material吼美宝藏 + Prop.Probablity_Material吼咪宝藏)
                {
                    gr.name = "吼美宝藏";
                    gr.value = 600;
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Material.ToString();
                    gr.quality = 2;
                    gr.evaluation = 4;

                    return gr;
                }
                else
                {
                    gr.name = "吼里宝藏";
                    gr.value = 500;
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Material.ToString();
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
            }
        }
        public static GachaResult BP_GachaMain()
        {
            Random rd = new Random(MainSave.GetRandomSeed());
            double pro_1 = rd.Next(0, 1000000) / (double)10000;
            path = Path.Combine(MainSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            Count++;
        jumpin:
            GachaResult gr = new GachaResult();
            if (Count < 10)
            {
                if (pro_1 < Prop.Probablity_BP角色卡)
                {
                    GetTargetItem = true;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Prop.Probablity_BP角色卡) * 1000)) / (double)1000;
                    if (pro_0 < Prop.Probablity_BPS)
                    {
                        Count = 0;
                        gr.name = GetBPCharacter("S");
                        gr.value = 28000;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Character.ToString();
                        gr.class_ = "S";
                        gr.quality = 2;
                        return gr;
                    }
                    else if (pro_0 < Prop.Probablity_BPA + Prop.Probablity_BPS)
                    {
                        Count = 0;
                        gr.name = GetBPCharacter("A");
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Character.ToString();
                        gr.class_ = "A";
                        gr.quality = 2;
                        return gr;
                    }
                    else if (pro_1 < Prop.Probablity_BP角色卡 + Prop.Probablity_BP角色碎片 + Prop.Probablity_BP装备)
                    {
                        double pro_2 = rd.Next(0, (int)(10000 * (Prop.Probablity_BPWeapon4 + Prop.Probablity_BPStigmata4))) / (double)10000;
                        string path = $@"{MainSave.AppDirectory}概率\标配概率.txt";
                        if (pro_2 < Prop.Probablity_BPWeapon4)
                        {
                            gr.name = GetBPWeapon();
                            gr.value = 2400;
                            gr.count = 1;
                            gr.type = PublicArgs.TypeS.Weapon.ToString();
                            gr.quality = 2;
                            gr.evaluation = 4;
                            return gr;
                        }
                        else
                        {
                            gr.name = GetBPStigmata();
                            gr.value = 2400;
                            gr.count = 1;
                            gr.type = PublicArgs.TypeS.Stigmata.ToString();
                            gr.quality = 2;
                            gr.evaluation = 4;
                            return gr;
                        }
                    }
                    else
                    {
                        gr.name = GetBPCharacter("B");
                        gr.value = 2750;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Character.ToString();
                        gr.class_ = "B";
                        gr.quality = 2;
                        return gr;
                    }
                }
                else if (pro_1 < Prop.Probablity_BP角色卡 + Prop.Probablity_BP角色碎片)
                {
                    double pro_0 = rd.Next(0, Convert.ToInt32((Prop.Probablity_BP角色碎片) * 1000)) / (double)1000;
                    if (pro_0 < Prop.Probablity_BPSdebris)
                    {
                        gr.name = GetBPCharacter("S") + "碎片";
                        gr.value = 2700;
                        gr.count = rd.Next(4, 9);
                        gr.type = PublicArgs.TypeS.debri.ToString();
                        gr.quality = 2;
                        return gr;
                    }
                    else if (pro_0 < Prop.Probablity_BPSdebris + Prop.Probablity_BPAdebris)
                    {
                        gr.value = 2600;
                        gr.count = rd.Next(4, 9);
                        gr.type = PublicArgs.TypeS.debri.ToString();
                        gr.name = GetBPCharacter("A") + "碎片";
                        gr.quality = 2;
                        return gr;
                    }
                    else
                    {
                        gr.value = 2500;
                        gr.count = rd.Next(4, 9);
                        gr.type = PublicArgs.TypeS.debri.ToString();
                        gr.name = GetBPCharacter("B") + "碎片";
                        gr.quality = 2;
                        return gr;
                    }
                }
                else
                {
                    double pro_2 = rd.Next(0, Convert.ToInt32((Prop.Probablity_BP材料) * 1000)) / (double)1000;
                    if (pro_2 < Prop.Probablity_Material技能材料)
                    {
                        gr.name = "高级技能材料";
                        gr.value = 1000;
                        gr.count = rd.Next(4, 7);
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;
                        return gr;
                    }
                    else if (pro_2 < Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
                    {
                        gr.name = "超小型反应炉";
                        gr.value = 800;
                        gr.count = rd.Next(4, 7);
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 1;
                        gr.evaluation = 3;

                        return gr;
                    }
                    else if (pro_2 < Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
                    {
                        double pro_4 = rd.NextDouble();
                        if (pro_4 <= 0.33)
                        {
                            gr.name = "特级学习材料";
                            gr.value = 900;
                            gr.count = rd.Next(5, 9);
                            gr.type = PublicArgs.TypeS.Material.ToString();
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                        else
                        {
                            gr.name = "高级学习材料";
                            gr.value = 800;
                            gr.count = rd.Next(5, 9);
                            gr.type = PublicArgs.TypeS.Material.ToString();
                            gr.quality = 1;
                            gr.evaluation = 3;

                            return gr;
                        }
                    }
                    else if (pro_2 < Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
                    {
                        gr.name = "吼咪宝藏";
                        gr.value = 700;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else if (pro_2 < Prop.Probablity_Material吼美宝藏 + Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
                    {
                        gr.name = "吼美宝藏";
                        gr.value = 600;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else
                    {
                        gr.name = "吼里宝藏";
                        gr.value = 500;
                        gr.count = 1;
                        gr.type = PublicArgs.TypeS.Material.ToString();
                        gr.quality = 1;
                        gr.evaluation = 3;

                        return gr;
                    }
                }
            }
            else
            {
                if (ini.Object["设置"]["Baodi"].GetValueOrDefault("1") == "1")
                {
                    Count = 0;
                    GetTargetItem = false;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Prop.Probablity_BP角色卡 - Prop.Probablity_BPB) * 1000)) / (double)1000;
                    if (pro_0 < Prop.Probablity_BPS)
                    {
                        gr.name = GetBPCharacter("S");
                        gr.value = 28000;
                        gr.count = 1;
                        gr.class_ = "S";
                        gr.quality = 2;
                        gr.type = PublicArgs.TypeS.Character.ToString();
                        return gr;
                    }
                    else
                    {
                        gr.name = GetBPCharacter("A");
                        gr.value = 2800;
                        gr.count = 1;
                        gr.class_ = "A";
                        gr.quality = 2;
                        gr.type = PublicArgs.TypeS.Character.ToString();
                        return gr;
                    }
                }
                else
                {
                    Count--;
                    goto jumpin;
                }
            }
        }
        public static GachaResult BP_GachaSub()
        {
            GachaResult gr = new GachaResult();
            Random rd = new Random(MainSave.GetRandomSeed());
            double pro_2 = rd.Next(0, 100000) / (double)1000;
            path = Path.Combine(MainSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            if (pro_2 < Prop.Probablity_Material技能材料)
            {
                gr.name = "高级技能材料";
                gr.value = 1000;
                gr.count = rd.Next(2, 4);
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;
                return gr;
            }
            else if (pro_2 < Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                gr.name = "超小型反应炉";
                gr.value = 800;
                gr.count = rd.Next(2, 4);
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 1;
                gr.evaluation = 3;

                return gr;
            }
            else if (pro_2 < Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                double pro_4 = rd.NextDouble();
                if (pro_4 <= 0.33)
                {
                    gr.name = "特级学习材料";
                    gr.value = 900;
                    gr.count = rd.Next(3, 6);
                    gr.type = PublicArgs.TypeS.Material.ToString();
                    gr.quality = 2;
                    gr.evaluation = 4;

                    return gr;
                }
                else
                {
                    gr.name = "高级学习材料";
                    gr.value = 800;
                    gr.count = rd.Next(3, 6);
                    gr.type = PublicArgs.TypeS.Material.ToString();
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
            }
            else if (pro_2 < Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                gr.name = "吼咪宝藏";
                gr.value = 700;
                gr.count = 1;
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_2 < Prop.Probablity_Material吼美宝藏 + Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                gr.name = "吼美宝藏";
                gr.value = 600;
                gr.count = 1;
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_2 < Prop.Probablity_Material吼姆宝藏 + Prop.Probablity_Material吼美宝藏 + Prop.Probablity_Material吼咪宝藏 + Prop.Probablity_Material紫色角色经验 + Prop.Probablity_Material反应炉 + Prop.Probablity_Material技能材料)
            {
                gr.name = "吼里宝藏";
                gr.value = 500;
                gr.count = 1;
                gr.type = PublicArgs.TypeS.Material.ToString();
                gr.quality = 1;
                gr.evaluation = 3;
                return gr;
            }
            else
            {
                double pro_0 = rd.Next(0, 10000) / (double)100;
                if (pro_0 <= 50)
                {
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Weapon2"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Weapon2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星武器");
                    gr.value = 2500;
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Weapon.ToString();
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 2;

                    return gr;
                }
                else
                {
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Stigmata2"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Stigmata2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星圣痕");
                    switch (rd.Next(0, 3))
                    {
                        case 0:
                            gr.name += "上";
                            break;
                        case 1:
                            gr.name += "中";
                            break;
                        case 2:
                            gr.name += "下";
                            break;
                    }
                    gr.value = 2400;
                    gr.count = 1;
                    gr.type = PublicArgs.TypeS.Stigmata.ToString();
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 2;
                    return gr;
                }
            }
        }
        private static string GetBPCharacter(string pos)
        {
            int count;
            Random rd = new Random(MainSave.GetRandomSeed());
            string result = "";
            path = Path.Combine(MainSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            switch (pos)
            {
                case "S":
                    count = Convert.ToInt32(ini.Object["详情_S"]["Count"].GetValueOrDefault("0"));
                    result = ini.Object["详情_S"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"S角色{count}");
                    break;
                case "A":
                    count = Convert.ToInt32(ini.Object["详情_A"]["Count"].GetValueOrDefault("0"));
                    result = ini.Object["详情_A"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"A角色{count}");
                    break;
                case "B":
                    count = Convert.ToInt32(ini.Object["详情_B"]["Count"].GetValueOrDefault("0"));
                    result = ini.Object["详情_B"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"B角色{count}");
                    break;
            }
            return result;
        }
        private static string GetBPWeapon()
        {
            int count;
            path = Path.Combine(MainSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            Random rd = new Random(MainSave.GetRandomSeed());
            string result;
            count = Convert.ToInt32(ini.Object["详情_Weapon"]["Count"].GetValueOrDefault("0"));
            result = ini.Object["详情_Weapon"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"四星武器{count}");
            return result;
        }
        private static string GetBPStigmata()
        {
            int count;
            path = Path.Combine(MainSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            Random rd = new Random(MainSave.GetRandomSeed());
            string result, pos = "";
            switch (rd.Next(0, 3))
            {
                case 0:
                    pos = "上";
                    break;
                case 1:
                    pos = "中";
                    break;
                case 2:
                    pos = "下";
                    break;
            }
            count = Convert.ToInt32(ini.Object["详情_Stigmata"]["Count"].GetValueOrDefault("0"));
            result = ini.Object["详情_Stigmata"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"四星圣痕{count}{pos}");
            return result;
        }
        public static void Init()
        {
            Read_Kuochong();
            Read_Jingzhun();
            Read_BP();
        }
        private static void Read_Kuochong()
        {
            path = Path.Combine(MainSave.AppDirectory, "概率", "扩充概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            Prop.Probablity_KC角色卡 = Convert.ToDouble(ini.Object["综合概率"]["角色卡"].GetValueOrDefault("15.00").Replace("%", ""));
            Prop.Probablity_KC角色碎片 = Convert.ToDouble(ini.Object["综合概率"]["角色碎片"].GetValueOrDefault("26.25").Replace("%", ""));
            Prop.Probablity_KC材料 = Convert.ToDouble(ini.Object["综合概率"]["材料"].GetValueOrDefault("58.75").Replace("%", ""));
            Prop.Probablity_UpS = Convert.ToDouble(ini.Object["详细概率"]["UpS角色"].GetValueOrDefault("1.50").Replace("%", ""));
            Prop.Probablity_UpA = Convert.ToDouble(ini.Object["详细概率"]["UpA角色"].GetValueOrDefault("4.50").Replace("%", ""));
            Prop.Probablity_A = Convert.ToDouble(ini.Object["详细概率"]["A角色"].GetValueOrDefault("3.00").Replace("%", ""));
            Prop.Probablity_Adebris = Convert.ToDouble(ini.Object["详细概率"]["UpA角色碎片"].GetValueOrDefault("15.00").Replace("%", ""));
            Prop.Probablity_Sdebris = Convert.ToDouble(ini.Object["详细概率"]["UpS角色碎片"].GetValueOrDefault("2.25").Replace("%", ""));
            Prop.Probablity_Material技能材料 = Convert.ToDouble(ini.Object["详细概率"]["技能材料"].GetValueOrDefault("10.00").Replace("%", ""));
            Prop.Probablity_Material反应炉 = Convert.ToDouble(ini.Object["详细概率"]["低星装备材料"].GetValueOrDefault("26.41").Replace("%", ""));
            Prop.Probablity_Material紫色角色经验 = Convert.ToDouble(ini.Object["详细概率"]["角色经验"].GetValueOrDefault("11.17").Replace("%", ""));
            Prop.Probablity_Material吼咪宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼咪宝藏"].GetValueOrDefault("2.232").Replace("%", ""));
            Prop.Probablity_Material吼美宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼美宝藏"].GetValueOrDefault("3.334").Replace("%", ""));
            Prop.Probablity_Material吼姆宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼姆宝藏"].GetValueOrDefault("5.556").Replace("%", ""));

            Texts.Text_UpS = ini.Object["详情"]["UpS"].GetValueOrDefault("S角色卡");
            Texts.Text_UpA = ini.Object["详情"]["UpA"].GetValueOrDefault("UpA角色卡");
            Texts.Text_A1 = ini.Object["详情"]["Item0"].GetValueOrDefault("A角色卡1");
            Texts.Text_A2 = ini.Object["详情"]["Item1"].GetValueOrDefault("A角色卡2");
            Texts.Text_A3 = ini.Object["详情"]["Item2"].GetValueOrDefault("A角色卡3");
        }
        private static void Read_Jingzhun()
        {
            path = Path.Combine(MainSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();
            Prop.Probablity_Weapon4Total = Convert.ToDouble(ini.Object["综合概率"]["4星武器"].GetValueOrDefault("4.958").Replace("%", ""));
            Prop.Probablity_Stigmata4Total = Convert.ToDouble(ini.Object["综合概率"]["4星圣痕"].GetValueOrDefault("7.437").Replace("%", ""));
            Prop.Probablity_Weapon3Total = Convert.ToDouble(ini.Object["综合概率"]["3星武器"].GetValueOrDefault("11.231").Replace("%", ""));
            Prop.Probablity_Stigmata3Total = Convert.ToDouble(ini.Object["综合概率"]["3星圣痕"].GetValueOrDefault("33.694").Replace("%", ""));
            Prop.Probablity_JZ通用进化材料 = Convert.ToDouble(ini.Object["综合概率"]["通用进化材料"].GetValueOrDefault("17.072").Replace("%", ""));
            Prop.Probablity_JZ装备经验 = Convert.ToDouble(ini.Object["综合概率"]["装备经验"].GetValueOrDefault("17.072").Replace("%", ""));
            Prop.Probablity_JZGold = Convert.ToDouble(ini.Object["综合概率"]["金币"].GetValueOrDefault("8.536").Replace("%", ""));

            Prop.Probablity_JZUpWeapon4 = Convert.ToDouble(ini.Object["详细概率"]["Up武器"].GetValueOrDefault("2.479").Replace("%", ""));
            Prop.Probablity_JZUpStigmata4 = Convert.ToDouble(ini.Object["详细概率"]["Up圣痕单件"].GetValueOrDefault("1.240").Replace("%", ""));
            Prop.Probablity_JZWeapon4 = Convert.ToDouble(ini.Object["详细概率"]["四星武器"].GetValueOrDefault("0.413").Replace("%", ""));
            Prop.Probablity_JZStigmata4 = Convert.ToDouble(ini.Object["详细概率"]["四星圣痕单件"].GetValueOrDefault("0.310").Replace("%", ""));
            Prop.Probablity_Weapon3 = Convert.ToDouble(ini.Object["详细概率"]["三星武器"].GetValueOrDefault("0.511").Replace("%", ""));
            Prop.Probablity_Stigmata3 = Convert.ToDouble(ini.Object["详细概率"]["三星圣痕单件"].GetValueOrDefault("0.936").Replace("%", ""));
            Prop.Probablity_Material镜面 = Convert.ToDouble(ini.Object["详细概率"]["镜面"].GetValueOrDefault("6.828").Replace("%", ""));
            Prop.Probablity_Material反应炉 = Convert.ToDouble(ini.Object["详细概率"]["反应炉"].GetValueOrDefault("10.242").Replace("%", ""));
            Prop.Probablity_Material紫色经验材料 = Convert.ToDouble(ini.Object["详细概率"]["紫色经验材料"].GetValueOrDefault("8.536").Replace("%", ""));
            Prop.Probablity_Material蓝色经验材料 = Convert.ToDouble(ini.Object["详细概率"]["蓝色经验材料"].GetValueOrDefault("8.536").Replace("%", ""));
            Prop.Probablity_Material吼咪宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼咪宝藏"].GetValueOrDefault("1.707").Replace("%", ""));
            Prop.Probablity_Material吼美宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼美宝藏"].GetValueOrDefault("2.561").Replace("%", ""));
            Prop.Probablity_Material吼姆宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼姆宝藏"].GetValueOrDefault("4.267").Replace("%", ""));
            Texts.Text_UpAWeapon = ini.Object["详情"]["A_UpWeapon"].GetValueOrDefault("Up四星武器");
            Texts.Text_UpAStigmata = ini.Object["详情"]["A_UpStigmata"].GetValueOrDefault("Up四星圣痕");
            Texts.Text_AWeapon1 = ini.Object["详情"]["A_Weapon_Item0"].GetValueOrDefault("四星武器1");
            Texts.Text_AWeapon2 = ini.Object["详情"]["A_Weapon_Item1"].GetValueOrDefault("四星武器2");
            Texts.Text_AWeapon3 = ini.Object["详情"]["A_Weapon_Item2"].GetValueOrDefault("四星武器3");
            Texts.Text_AWeapon4 = ini.Object["详情"]["A_Weapon_Item3"].GetValueOrDefault("四星武器4");
            Texts.Text_AWeapon5 = ini.Object["详情"]["A_Weapon_Item4"].GetValueOrDefault("四星武器5");
            Texts.Text_AWeapon6 = ini.Object["详情"]["A_Weapon_Item5"].GetValueOrDefault("四星武器6");
            Texts.Text_AStigmata1 = ini.Object["详情"]["A_Stigmata_Item0"].GetValueOrDefault("四星圣痕1");
            Texts.Text_AStigmata2 = ini.Object["详情"]["A_Stigmata_Item1"].GetValueOrDefault("四星圣痕2");
            Texts.Text_AStigmata3 = ini.Object["详情"]["A_Stigmata_Item2"].GetValueOrDefault("四星圣痕3");
            Texts.Text_AStigmata4 = ini.Object["详情"]["A_Stigmata_Item3"].GetValueOrDefault("四星圣痕4");
            Texts.Text_UpBWeapon = ini.Object["详情"]["B_UpWeapon"].GetValueOrDefault("Up四星武器");
            Texts.Text_UpBStigmata = ini.Object["详情"]["B_UpStigmata"].GetValueOrDefault("Up四星圣痕");
            Texts.Text_BWeapon1 = ini.Object["详情"]["B_Weapon_Item0"].GetValueOrDefault("四星武器1");
            Texts.Text_BWeapon2 = ini.Object["详情"]["B_Weapon_Item1"].GetValueOrDefault("四星武器2");
            Texts.Text_BWeapon3 = ini.Object["详情"]["B_Weapon_Item2"].GetValueOrDefault("四星武器3");
            Texts.Text_BWeapon4 = ini.Object["详情"]["B_Weapon_Item3"].GetValueOrDefault("四星武器4");
            Texts.Text_BWeapon5 = ini.Object["详情"]["B_Weapon_Item4"].GetValueOrDefault("四星武器5");
            Texts.Text_BWeapon6 = ini.Object["详情"]["B_Weapon_Item5"].GetValueOrDefault("四星武器6");
            Texts.Text_BStigmata1 = ini.Object["详情"]["B_Stigmata_Item0"].GetValueOrDefault("四星圣痕1");
            Texts.Text_BStigmata2 = ini.Object["详情"]["B_Stigmata_Item1"].GetValueOrDefault("四星圣痕2");
            Texts.Text_BStigmata3 = ini.Object["详情"]["B_Stigmata_Item2"].GetValueOrDefault("四星圣痕3");
            Texts.Text_BStigmata4 = ini.Object["详情"]["B_Stigmata_Item3"].GetValueOrDefault("四星圣痕4");
        }
        private static void Read_BP()
        {
            path = Path.Combine(MainSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            Prop.Probablity_BP角色卡 = Convert.ToDouble(ini.Object["综合概率"]["角色卡"].GetValueOrDefault("6.00").Replace("%", ""));
            Prop.Probablity_BP角色碎片 = Convert.ToDouble(ini.Object["综合概率"]["角色碎片"].GetValueOrDefault("17.25").Replace("%", ""));
            Prop.Probablity_BP装备 = Convert.ToDouble(ini.Object["综合概率"]["四星装备"].GetValueOrDefault("1.19").Replace("%", ""));
            Prop.Probablity_BP材料 = Convert.ToDouble(ini.Object["综合概率"]["材料"].GetValueOrDefault("75.56").Replace("%", ""));
            Prop.Probablity_BPS = Convert.ToDouble(ini.Object["详细概率"]["S角色"].GetValueOrDefault("1.50").Replace("%", ""));
            Prop.Probablity_BPA = Convert.ToDouble(ini.Object["详细概率"]["A角色"].GetValueOrDefault("4.50").Replace("%", ""));
            Prop.Probablity_BPB = Convert.ToDouble(ini.Object["详细概率"]["B角色"].GetValueOrDefault("1.00").Replace("%", ""));
            Prop.Probablity_BPSdebris = Convert.ToDouble(ini.Object["详细概率"]["S角色碎片"].GetValueOrDefault("2.25").Replace("%", ""));
            Prop.Probablity_BPAdebris = Convert.ToDouble(ini.Object["详细概率"]["A角色碎片"].GetValueOrDefault("15.00").Replace("%", ""));
            Prop.Probablity_BPWeapon4 = Convert.ToDouble(ini.Object["详细概率"]["4星武器"].GetValueOrDefault("0.46").Replace("%", ""));
            Prop.Probablity_BPStigmata4 = Convert.ToDouble(ini.Object["详细概率"]["4星圣痕"].GetValueOrDefault("0.73").Replace("%", ""));

            Prop.Probablity_Material技能材料 = Convert.ToDouble(ini.Object["详细概率"]["技能材料"].GetValueOrDefault("10.00").Replace("%", ""));
            Prop.Probablity_Material反应炉 = Convert.ToDouble(ini.Object["详细概率"]["低星装备材料"].GetValueOrDefault("26.41").Replace("%", ""));
            Prop.Probablity_Material紫色角色经验 = Convert.ToDouble(ini.Object["详细概率"]["角色经验"].GetValueOrDefault("11.17").Replace("%", ""));
            Prop.Probablity_Material吼咪宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼咪宝藏"].GetValueOrDefault("2.232").Replace("%", ""));
            Prop.Probablity_Material吼美宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼美宝藏"].GetValueOrDefault("3.334").Replace("%", ""));
            Prop.Probablity_Material吼姆宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼姆宝藏"].GetValueOrDefault("5.556").Replace("%", ""));
        }
    }
}
