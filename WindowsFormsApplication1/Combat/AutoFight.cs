using dmnet;

namespace CrossGateHelper.Combat
{
    public class AutoFight
    {
        private CrossGate CG;
        public Unit[] Unit = new Unit[20] { new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), new Unit(), };
        public AutoFight(CrossGate p)
        {
            CG = p;
        }

        /// <summary>
        /// 1=人物行动 4=宠物行动 5=结束战斗
        /// </summary>
        private int whosturn
        {
            get
            {
                return dm.d.ReadInt(CG.Hwnd, "0058ABC0", 2);
            }
        }
        public void Refresh()
        {
            for (int i = 0; i <= 19; i++)
            {

                Unit[i].Units = Unit;
                Unit[i].clear();

            }
            string info = dm.d.ReadString(CG.Hwnd, "005829AC", 0, 4000);
            string[] sArray = info.Split('|');
            for (int i = 2; i <= sArray.Length - 5; i += 12)
            {
                Unit[int.Parse(sArray[i], System.Globalization.NumberStyles.AllowHexSpecifier)].Position = int.Parse(sArray[i], System.Globalization.NumberStyles.AllowHexSpecifier);
                Unit[int.Parse(sArray[i], System.Globalization.NumberStyles.AllowHexSpecifier)].Name = sArray[i + 1];
                Unit[int.Parse(sArray[i], System.Globalization.NumberStyles.AllowHexSpecifier)].Level = int.Parse(sArray[i + 4], System.Globalization.NumberStyles.AllowHexSpecifier);
                Unit[int.Parse(sArray[i], System.Globalization.NumberStyles.AllowHexSpecifier)].Hp = int.Parse(sArray[i + 5], System.Globalization.NumberStyles.AllowHexSpecifier);
                Unit[int.Parse(sArray[i], System.Globalization.NumberStyles.AllowHexSpecifier)].Hpmax = int.Parse(sArray[i + 6], System.Globalization.NumberStyles.AllowHexSpecifier);
                Unit[int.Parse(sArray[i], System.Globalization.NumberStyles.AllowHexSpecifier)].Mp = int.Parse(sArray[i + 7], System.Globalization.NumberStyles.AllowHexSpecifier);
                Unit[int.Parse(sArray[i], System.Globalization.NumberStyles.AllowHexSpecifier)].Mpmax = int.Parse(sArray[i + 8], System.Globalization.NumberStyles.AllowHexSpecifier);
            }



        }
        public void Fight()
        {
            //不在战斗中,退出
            if (CG.Player.Isfighting == false)
            {
                return;
            }

            if (CG.Player.MapID == 1000 && CG.Player.Hp == 1)
            {
                dm.d.WriteInt(CG.Hwnd, "00e9bfb8", 0, 7);
                return;
            }

            //没到选择命令时刻，退出
            if ((whosturn != 1) && (whosturn != 4)) { return; }
            //刷新info
            
            Refresh();
            //人物行动
            if (whosturn == 1)
            {
               System.Threading.Thread.Sleep(2000);
                if ((whosturn != 1)) { return; }
                //扔药
                if (((((double)CG.Player.Hp) / CG.Player.HpMax) < 0.5) && (ActionCount == 0) && (CG.Bag.ItemExist("", 43) >= 0)) { CombatCall("I|" + (CG.Bag.ItemExist("", 43) + 8).ToString("X") + "|" + CG.Player.Position.ToString("X")); }
                else if (EnemiesCount >= 4) { CombatCall(UseSkill()); }
                else { CombatCall("H|" + RandomTarget(true)); }

            }
            //宠物行动
            else if (whosturn == 4)
            {
                Unit pet = Unit[CG.Player.Position].FrontOrBehind;
                if ((pet.Hpmax - pet.Hp) >= 280 && CG.CombatPet.SkillPosition("吸血攻击") > 0 && pet.Mp >= CG.CombatPet.SkillCost(CG.CombatPet.SkillPosition("吸血攻击")))
                {

                    CombatCall("W|" + CG.CombatPet.SkillPosition("吸血攻击").ToString("X") + "|" + RandomTarget());

                }
                else if (CG.CombatPet.SkillPosition("连击") > 0 && pet.Mp >= CG.CombatPet.SkillCost(CG.CombatPet.SkillPosition("连击")))
                {
                    CombatCall("W|" + CG.CombatPet.SkillPosition("连击").ToString("X") + "|" + RandomTarget());

                }
                else
                {
                    CombatCall("W|" + CG.CombatPet.SkillPosition("攻击").ToString("X") + "|" + RandomTarget());
                }
            }
           
        }

        private string UseSkill()
        {
            if (SkillPosition("气功弹") >= 0)
            {
                if (CG.Player.Mp < 35) { return "H|" + RandomTarget(true); }
                else { return "S|" + SkillPosition("气功弹") + "|" + SkillLV(SkillPosition("气功弹")) + "|" + RandomTarget(); }
            }
            else
            {
                return "H|" + RandomTarget(true);
            }
        }

        private int EnemiesCount
        {
            get
            {
                int count = 0;
                for (int i = 10; i <= 19; i++)
                {
                    if (Unit[i].Hp > 0) { count++; }
                }
                return count;

            }

        }

        /// <summary>
        /// 返回A|
        /// </summary>
        /// <param name="front">只返回前排目标</param>
        /// <returns></returns>
        public string RandomTarget(bool front = false)
        {
            System.Random r = new System.Random(dm.d.ReadInt(CG.Hwnd, "0056CC00", 0));
            int ran = r.Next(1, EnemiesCount + 1);
            int temp = 0;
            int result = 0;
            for (int i = 10; i <= 19; i++)
            {
                if (Unit[i].Exist)
                {
                    temp++;
                    if (temp == ran) { result = i; break; }
                }

            }
            if (front == true)
            {
                if (result < 15 && Unit[result].FrontOrBehind.Exist) { result = Unit[result].FrontOrBehind.Position; }

            }
            return result.ToString("X") + "|";

        }


        /// <summary>
        /// -1未找到
        /// </summary>
        /// <param name="SkillName">技能名</param>
        /// <returns></returns>
        public int SkillPosition(string SkillName)
        {
            int baseaddr = 0x00DEAD9C;
            for (int i = 0; i <= 12; i++)
            {
                if (dm.d.ReadString(CG.Hwnd, (baseaddr + i * 0x49FC).ToString("X"), 0, 20) == SkillName)
                {
                    return i;

                }
            }
            return -1;
        }
        /// <summary>
        /// 从0开始
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int SkillLV(int index)
        {
            int baseaddr = 0x00DEAD9C + 0x1C;
            return dm.d.ReadInt(CG.Hwnd, (baseaddr + index * 0x49FC).ToString("X"), 0) - 1;
        }

        public void CombatCall(string str)
        {
            dm.d.AsmClear();
            dm.d.AsmAdd("push 00610000");
            dm.d.AsmAdd("push dword ptr ds:[0xC92718]");
            dm.d.AsmAdd("call 004fa7e0");
            dm.d.AsmAdd("call 00432c50");
            dm.d.AsmAdd("push 0f0");
            dm.d.AsmAdd("push 0140");
            dm.d.AsmAdd("push 039");
            dm.d.AsmAdd("mov dword ptr ds:[0x58ABF8],0x1");
            dm.d.AsmAdd("call 0514470");
            dm.d.AsmAdd("add esp,014");
            dm.d.WriteString(CG.Hwnd, "00610000", 0, str);
            dm.d.AsmCall(CG.Hwnd, 1);

        }
        /// <summary>
        /// 人物第几次行动 1动=0 二动=1
        /// </summary>
        public int ActionCount
        {

            get
            {
                return dm.d.ReadInt(CG.Hwnd, "058AB78", 0);
            }
        }













    }
}
