using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossGateHelper.Combat
{
    public class Unit
    {
        public Unit()
        {
            Name = "";
            Level = -1;
            Hp = -1;
            Hpmax = -1;
            Mp = -1;
            Mpmax = -1;
            Position = -1;
        }
        public void clear()
        {
            Name = "";
            Level = -1;
            Hp = -1;
            Hpmax = -1;
            Mp = -1;
            Mpmax = -1;
            Position = -1;

        }

        public Unit[] Units=new Unit[20];
        private int position;
        private string name;
        private int level;
        private int hp;
        private int hpmax;
        private int mp;
        private int mpmax;
        public int friend
        {
            get
            {
                if (Position == -1) { return -1; }
                if (Position >= 10)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }


        }

        public Unit FrontOrBehind
        {
            get
            {
                if (Position == -1) { return null; }
                if (0 <= Position&&Position<=4) { return Units[Position + 5]; }
                if (10 <= Position && Position <= 14) { return Units[Position + 5]; }
                else { return Units[Position - 5]; }
            }

        }
        public bool Exist
        {
            get
            {
                if (Hp > 0) { return true; }
                return false;

            }

        }

        public int Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        public int Hp
        {
            get
            {
                return hp;
            }

            set
            {
                hp = value;
            }
        }

        public int Hpmax
        {
            get
            {
                return hpmax;
            }

            set
            {
                hpmax = value;
            }
        }

        public int Mp
        {
            get
            {
                return mp;
            }

            set
            {
                mp = value;
            }
        }

        public int Mpmax
        {
            get
            {
                return mpmax;
            }

            set
            {
                mpmax = value;
            }
        }
    }
}
