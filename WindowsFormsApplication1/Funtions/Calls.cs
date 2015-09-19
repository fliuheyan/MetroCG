using dmnet;
using System;
using System.Threading;
namespace CrossGateHelper.Funtions
{
    public class Calls
    {

        private CrossGate CG;
        public Calls(CrossGate p)
        {
            CG = p;

        }


        /// <summary>
        /// 发送走路包
        /// </summary>
        /// <param name="x">当前x</param>
        /// <param name="y">当前y</param>
        /// <param name="direction">方向a-h，大写右键小写走路</param>
        public void SendMove(int x, int y, string direction)
        {
            dm.d.WriteString(CG.Hwnd, "00600000", 0, direction);
            dm.d.AsmClear();
            dm.d.AsmAdd("push 00");
            dm.d.AsmAdd("push 00600000");
            dm.d.AsmAdd("push " + "0" + y.ToString("X"));
            dm.d.AsmAdd("push " + "0" + x.ToString("X"));
            dm.d.AsmAdd("push [00c92718]");
            dm.d.AsmAdd("call 004FA3A0");
            dm.d.AsmAdd("add esp,014");
            dm.d.AsmCall(CG.Hwnd, 1);
        }
        public void SendEJ()
        {

            dm.d.AsmClear();
            dm.d.AsmAdd("push [00c92718]");
            dm.d.AsmAdd("mov dword ptr ds:[0x71D01C],eax");
            dm.d.AsmAdd("call 004FCA30");
            dm.d.AsmAdd("add esp,04");
            dm.d.AsmCall(CG.Hwnd, 1);

        }

        private bool stopFightNow;

        public bool StopFightNow
        {
            get
            {
                return stopFightNow;
            }

            set
            {
                stopFightNow = value;
            }
        }

        public void FightNow()
        {
            StopFightNow = false;
            while (dm.d.ReadInt(CG.Hwnd, "00C05F4C", 2) != 1 && StopFightNow == false)
            {

                SendMove(CG.Player.X, CG.Player.Y, "c");
                Thread.Sleep(270);

                if (dm.d.ReadInt(CG.Hwnd, "00C05F4C", 2) == 1)
                {
                    break;
                };
                System.Windows.Forms.Application.DoEvents();
                SendMove(CG.Player.X + 1, CG.Player.Y, "g");
                Thread.Sleep(270);
                if (dm.d.ReadInt(CG.Hwnd, "00C05F4C", 2) == 1)
                {
                    break;
                };
                System.Windows.Forms.Application.DoEvents();
                SendMove(CG.Player.X, CG.Player.Y, "g");
                Thread.Sleep(270);
                if (dm.d.ReadInt(CG.Hwnd, "00C05F4C", 2) == 1)
                {
                    break;
                };
                System.Windows.Forms.Application.DoEvents();
                SendMove(CG.Player.X - 1, CG.Player.Y, "c");
                Thread.Sleep(270);
                

            }
        }

        /// <summary>
        /// 普通149  14B    14C    资深16D 16F  170
        /// </summary>
        /// <param name="type">普通149  14B    14C    资深16D 16F  170</param>
        public void NurseCall(string type)
        {
            Version ver = System.Environment.OSVersion.Version;
            string strClient = "";
            string esp = "";
            if (ver.Major == 5 && ver.Minor == 1)
            {
                strClient = "Win XP";
            }
            else if (ver.Major == 6 && ver.Minor == 0)
            {
                strClient = "Win Vista";
            }
            else if (ver.Major == 6 && ver.Minor == 1)
            {
                strClient = "Win 7";
                esp = "018E4B4";
            }
            else if (ver.Major == 6 && ver.Minor == 2)
            {
                strClient = "Win 10";
                esp = "019E4AC";
            }
            else
            {
                strClient = "未知";
            }


            dm.d.AsmClear();
            dm.d.AsmAdd("push " + esp);
            dm.d.AsmAdd("push 04");
            dm.d.AsmAdd("push dword ptr ds:[0xC14AD8]");  //npc编号
            dm.d.AsmAdd("push 0x" + type);
            dm.d.AsmAdd("mov ecx,009615D0");
            dm.d.AsmAdd("call 00401EA0");
            dm.d.AsmAdd("push eax");
            dm.d.AsmAdd("mov ecx,009615B8");
            dm.d.AsmAdd("call 00401EA0");
            dm.d.AsmAdd("mov ecx,dword ptr ds:[0xC92718]");
            dm.d.AsmAdd("push eax");
            dm.d.AsmAdd("push ecx");
            dm.d.AsmAdd("call 004FC3F0");
            dm.d.AsmAdd("add esp,01C");
            dm.d.AsmCall(CG.Hwnd, 1);


        }

        public void TP()
        {
            dm.d.AsmClear();
            dm.d.AsmAdd("push dword ptr ds:[0xC92718]");
            dm.d.AsmAdd("call 004FCAC0");
            dm.d.AsmAdd("add esp,04");
            dm.d.AsmCall(CG.Hwnd, 1);
        }
        
        /// <summary>
        /// 8|1|10|1|
        /// </summary>
        /// <param name="info"></param>
        public void Sell(string info)
        {
            dm.d.WriteString(CG.Hwnd, "00600100", 0, info);
            dm.d.AsmClear();
            dm.d.AsmAdd("push 0ff");
            dm.d.AsmAdd("push 00600500");
            dm.d.AsmAdd("push 00600100");
            dm.d.AsmAdd("call 005152F0");
            dm.d.AsmAdd("add esp,0C");
            dm.d.AsmAdd("push 00600500");
            dm.d.AsmAdd("push 0");
            dm.d.AsmAdd("push dword ptr ds:[0C4453C]");
            dm.d.AsmAdd("push 014E");
            dm.d.AsmAdd("mov ecx,0x9615D0");
            dm.d.AsmAdd("call 00401EA0");

            dm.d.AsmAdd("push eax");
            dm.d.AsmAdd("mov ecx,0x9615B8");
            dm.d.AsmAdd("call 00401EA0");

            dm.d.AsmAdd("push eax");
            dm.d.AsmAdd("push dword ptr ds:[0C92718]");
            dm.d.AsmAdd("call 004FC3F0");
            dm.d.AsmAdd("mov eax,dword ptr ds:[0xC31314]");
            dm.d.AsmAdd("add esp,1C");
            
            dm.d.AsmCall(CG.Hwnd, 1);
        }

        public void ThrowItem(int position)
        {
            
            dm.d.AsmClear();


            dm.d.AsmAdd("push " + position.ToString("X"));
            dm.d.AsmAdd("mov ecx,0x9615D0");
            dm.d.AsmAdd("call 00401EA0");

            dm.d.AsmAdd("push eax");
            dm.d.AsmAdd("mov ecx,0x9615B8");
            dm.d.AsmAdd("call 00401EA0");

            dm.d.AsmAdd("push eax");
            dm.d.AsmAdd("push dword ptr ds:[0C92718]");
            dm.d.AsmAdd("call 004FAB10");
            dm.d.AsmAdd("add esp,10");

            dm.d.AsmCall(CG.Hwnd, 1);
        }

    }
}
