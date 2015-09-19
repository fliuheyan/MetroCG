using System.Drawing;
using CrossGateHelper.Funtions;
using CrossGateHelper.Player;
using CrossGateHelper.Items;
using CrossGateHelper.Combat;
using dmnet;
using CrossGateHelper.Pets;
using CrossGateHelper.Script;
namespace CrossGateHelper
{
    public class CrossGate
    {


        public Role Player;
        public SpeedFight SpeedFight;
        public SpeedUp SpeedUp;
        public Movement Movement;
        public Bag Bag;
        public Calls Calls;
        public AutoFight AutoFight;
        public FuntionThread FuntionThread;
        public Pet[] Pet;
        public Scripts Scripts;

        public CrossGate()
        {
            Player = new Role(this);
            SpeedFight = new SpeedFight(this);
            SpeedUp = new SpeedUp(this);
            Movement = new Movement(this);
            Bag = new Bag(this);
            Calls = new Calls(this);
            AutoFight = new AutoFight(this);
            FuntionThread = new FuntionThread(this);
            Pet = new Pet[5] { new Pet(this, 0), new Pet(this, 1), new Pet(this, 2), new Pet(this, 3), new Pet(this, 4) };
            Scripts = new Scripts(this);
        }
        ~CrossGate()
        {


        }

        private int hwnd;
        public int type;

        public int Hwnd
        {
            get
            {
                return hwnd;
            }

            set
            {
                hwnd = value;
            }
        }
        /// <summary>
        /// 1=原生态魔力  2=茉莉魔力  3=新月魔力
        /// </summary>
        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        private Point location = new Point();
        /// <summary>
        /// 窗口位置
        /// </summary>
        public Point Location
        {
            get
            {
                object x1, y1, x2, y2;
                dm.d.GetWindowRect(Hwnd, out x1, out y1, out x2, out y2);
                location.X = (int)x1;
                location.Y = (int)y1;
                return location;
            }

            set
            {
                location = value;
                dm.d.MoveWindow(Hwnd, location.X, location.Y);
            }
        }

        public bool Online
        {
            get
            {

                if (dm.d.ReadInt(Hwnd, "7435521C", 2) == 1) { return true; }
                return false;

            }

        }



        public void BindWindow()
        {


            if (Hwnd == 0)
            {
                Hwnd = dm.d.FindWindow("魔力", "魔力");
                dm.d.SetWindowText(Hwnd, Hwnd.ToString());
            }

            if (dm.d.GetWindowState(Hwnd, 0) == 1)
            {
                dm.d.SetWindowText(Hwnd, Hwnd.ToString());

                //分析魔力种类
                if (dm.d.ReadData(Hwnd, "0044D000", 1) == "00")
                { Type = 1; }
                if (dm.d.ReadData(Hwnd, "0044D000", 1) == "8B")
                { Type = 2; }
                if (dm.d.ReadData(Hwnd, "0044CFFF", 1) == "C3")
                { Type = 3; }
            }
            else
            {
                Hwnd = 0;
            }





        }

        public int MessageBoxType
        {
            get
            {
                return dm.d.ReadInt(Hwnd, "00562F48", 0);
            }

        }

        public void OnExit()
        {

            SpeedUp.StopThread();
            FuntionThread.StopThread();
            dm.d.SetWindowText(Hwnd, "魔力宝贝");

        }


        public Pet CombatPet
        {
            get
            {
                for (int i = 0; i <= 4; i++)
                {
                    if (Pet[i].OnBattle) { return Pet[i]; }

                }
                return Pet[0];
            }
        }

    }
}
