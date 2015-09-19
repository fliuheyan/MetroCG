using System.Threading;
using dmnet;
namespace CrossGateHelper.Funtions
{
    public class FuntionThread
    {

        private CrossGate CG;
        public FuntionThread(CrossGate p)
        {
            CG = p;
            t = new Thread(new ThreadStart(ThreadFuntion));
            t.Start();
        }

        Thread t;
        bool stop;

        public void StopThread() { stop = true; }


        private bool autofight;
        public bool AutoFight
        {
            set
            {
                autofight = value;
            }
            get
            {
                return autofight;
            }
        }

        public bool AutoSell
        {
            get
            {
                return autosell;
            }

            set
            {
                autosell = value;
            }
        }

        public bool AutoNurse
        {
            get
            {
                return autonurse;
            }

            set
            {
                autonurse = value;
            }
        }

        private bool autosell;
        private bool autonurse;


        private void ThreadFuntion()
        {
            while (stop == false)
            {
                if (AutoFight) { CG.AutoFight.Fight(); }
                if (AutoSell)
                {
                    if ((CG.Bag.FindItem("魔石") >= 0 || CG.Bag.FindItem("卡片") >= 0) && CG.MessageBoxType == 5)
                    {
                        string result;
                        result = "";
                        for (int i = 0; i <= 19; i++)
                        {
                            if (CG.Bag.item[i].Name.Contains("魔石") || CG.Bag.item[i].Name.Contains("卡片")) { result += CG.Bag.item[i].SellFormat; }

                        }
                        CG.Calls.Sell(result);
                    }
                }
                if (AutoNurse)
                {
                    if (CG.Bag.Money > 0 && CG.MessageBoxType == 2)
                    {
                        if ((CG.Player.MpMax - CG.Player.Mp) > 0)
                        {
                            CG.Calls.NurseCall("149");
                            CG.Calls.NurseCall("16D");
                            CG.Calls.NurseCall("14C");
                            CG.Calls.NurseCall("170");
                        }
                        if ((CG.Player.HpMax - CG.Player.Hp) > 0)
                        {
                            CG.Calls.NurseCall("14B");
                            CG.Calls.NurseCall("16F");
                            CG.Calls.NurseCall("14C");
                            CG.Calls.NurseCall("170");
                        }
                    }


                }



                Thread.Sleep(500);

            }
            dm.d.SetWindowText(CG.Hwnd, "魔力宝贝");

        }
    }
}
