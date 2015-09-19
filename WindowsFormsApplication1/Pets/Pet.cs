using dmnet;

namespace CrossGateHelper.Pets
{
    public class Pet
    {
        private CrossGate CG;
        public Pet(CrossGate p,int position)
        {
            CG = p;N = position;
        }
        private int n;

        public int N
        {
            get
            {
                return n;
            }

            set
            {
                n = value;
            }
        }
        /// <summary>
        /// 没找到返回-1
        /// </summary>
        /// <param name="skillname"></param>
        /// <returns></returns>
        public int SkillPosition(string skillname)
        {
            //第几只宠物的首地址
            int addr = 0x00E304C6+N*0x6d8;
            for (int i = 0; i <= 9; i++)
            {
                if (dm.d.ReadString(CG.Hwnd, addr.ToString("X"), 0, skillname.Length*2) == skillname)
                {
                    
                    return i;
                }
                addr=addr+0x8C;
            }
            return -1;

        }
        public int SkillCost(int skillp)
        {
            if (skillp < 0) { return 65535; }
            int addr = 0x00E30544 + N * 0x6d8+skillp*0x8C;
            return dm.d.ReadInt(CG.Hwnd, addr.ToString("X"), 0);
            
            

        }
        public bool OnBattle
        {
            get
            {
                
                int addr = 0x00C15ACC + N * 0x4;
                if (dm.d.ReadInt(CG.Hwnd, addr.ToString("X"), 2) == 2) { return true; }
                else { return false; }
            }
        }

    }
}
