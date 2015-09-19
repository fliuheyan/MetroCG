using dmnet;
using System.Threading;
namespace CrossGateHelper.Player
{
    public class Movement
    {
        private CrossGate CG;
        public Movement(CrossGate p)
        {
            CG = p;
        }



        public void MoveTo(int x,int y)
        {

            
            dm.d.WriteData(CG.Hwnd, "0045D56D", "BA 00 00 00 00 90");
            dm.d.WriteData(CG.Hwnd, "0045d5a8", "B8 00 00 00 00");

            dm.d.WriteInt(CG.Hwnd, "0045D56E", 0, x);
            dm.d.WriteInt(CG.Hwnd, "0045d5a9", 0, y);
            dm.d.WriteInt(CG.Hwnd, "00BFD4A4", 0, 1);
            Thread.Sleep(100);
            dm.d.WriteInt(CG.Hwnd, "00BFD4A4", 0, 0);
            dm.d.WriteData(CG.Hwnd, "0045D56D", "8B 15 90 D4 BF 00");
            dm.d.WriteData(CG.Hwnd, "0045d5a8", "A1 94 D4 BF 00");
        }


        public void MoveR(int direction, int steps)
        {


            
        }
    }
}
