using dmnet;

namespace CrossGateHelper.Items
{
    public class item
    {
        private CrossGate CG;
        public item(CrossGate p, int i)
        {
            CG = p;
            position = i;

        }
        private int position;
        public int Position
        {

            get
            {
                return position;
            }
        }
        public string Name
        {
            get
            {
                int addr = 0x00C209EC + 0x644 * position;
                return dm.d.ReadString(CG.Hwnd, addr.ToString("X"), 0,20);
            }
        }
        /// <summary>
        /// 药=43  料理=23 未知=26
        /// </summary>
        public int ItemClass
        {
            get
            {
                int addr = 0xE93DF4    + 0x65C * position;
                return dm.d.ReadInt(CG.Hwnd, addr.ToString("X"), 0);
            }
        }

        public int Count
        {
            get
            {
                int addr = 0xE93DF0 + 0x65C * position;
                return dm.d.ReadInt(CG.Hwnd, addr.ToString("X"), 0);
            }


        }

        public string SellFormat
        {
            get
            {
                return Position.ToString() + "|" + Count.ToString() + "|";
            }

        }


    }
}
