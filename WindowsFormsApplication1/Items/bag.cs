using dmnet;

namespace CrossGateHelper.Items
{
    public class Bag
    {

        private CrossGate CG;
        public Bag(CrossGate p)
        {
            CG = p;
            for (int i = 0; i <= 19; i++)
            {
                item[i] = new item(CG, i);
            }

        }

        public item[] item = new item[20];

        public int Money
        {
            get
            {
                return dm.d.ReadInt(CG.Hwnd, "00E90424", 0);
            }


        }

        /// <summary>
        /// class 药=43  料理=23 未知=26 未找到返回-1
        /// </summary>
        public int ItemExist(string name,int ClassID)
        {
            if (name != "")
            {
                for (int i = 0; i <= 19; i++)
                {
                    if (item[i].Name == name) { return i; }
                }

            }
            else
            {
                for (int i = 0; i <= 19; i++)
                {
                    if (item[i].ItemClass == ClassID) { return i; }
                }
            }
            return -1;
        }

        /// <summary>
        /// 返回从0开始  没找到返回-1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindItem(string name)
        {
            for(int i = 0; i <= 19; i++)
            {
                if (item[i].Name.Contains(name)) { return i; }

            }
            return -1;

        }
    }
}
