using dmnet;


namespace CrossGateHelper.Player
{
    public class Role
    {
        private CrossGate CG;
        public  Role(CrossGate p)
        {
            CG = p;
        }
        

        public bool Ismoving
        {
            get
            {
                if (dm.d.ReadInt(CG.Hwnd, "0054156C", 0) == 65535) { return false; }
                return true;
            }

        }

        public bool Isfighting
        {
            get
            {
                if ((dm.d.ReadInt(CG.Hwnd, "00C05F4C", 2) == 1)&&(dm.d.ReadInt(CG.Hwnd, "0058AAA4", 2) == 0)) { return true; }
                return false;
            }
        }

       public int X
        {
            get
            {
                return ((int)dm.d.ReadFloat(CG.Hwnd, "0094DA5C"))/64;
            }
        }

        public int Y
        {
            get
            {
                return ((int)dm.d.ReadFloat(CG.Hwnd, "0094DA60")) / 64;
            }
        }

        public int Hp
        {
            get
            {
                
                string[] temp= dm.d.ReadString(CG.Hwnd, "00C4720C", 0, 10).Split('/');
                return int.Parse(temp[0]);

            }

        }
        public int HpMax
        {
            get
            {

                string[] temp = dm.d.ReadString(CG.Hwnd, "00C4720C", 0, 10).Split('/');
                return int.Parse(temp[1]);

            }

        }
        public int Mp
        { 
            get
            {

                string[] temp = dm.d.ReadString(CG.Hwnd, "00C4C1F8", 0, 10).Split('/');
                return int.Parse(temp[0]);

            }

        }
        public int MpMax
        {
            get
            {

                string[] temp = dm.d.ReadString(CG.Hwnd, "00C4C1F8", 0, 10).Split('/');
                return int.Parse(temp[1]);

            }

        }

        public int Position
        {
            get
            {
                return dm.d.ReadInt(CG.Hwnd, "0058ac24", 0);
            }
        }

        public int MapID
        {
            get
            {
                return dm.d.ReadInt(CG.Hwnd, "0094DA68", 0);
            }

        }
    }
}
