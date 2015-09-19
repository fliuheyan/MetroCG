using System.Threading;
namespace CrossGateHelper.Script
{
     public class Scripts
    {
        public Scripts(CrossGate v)
        {
            CG = v;

        }
        private CrossGate CG;

        public void start()
        {


        }

        /// <summary>
        /// 上中下789
        /// </summary>
        /// <param name="p"></param>
        public void TP(int p=0)
        {
            while (true)
            {
                CG.Calls.TP();
                Thread.Sleep(1000);
                if (p == 0) { return; }
                if (CG.Player.X == 233 & CG.Player.Y == 78)
                {
                    if (p == 1||p==7) { return; }
                    
                    if (p == 3||p==8) { CG.Calls.SendMove(233, 78, "A");Thread.Sleep(1000); return; }
                    
                    if (p == 6||p==9) { CG.Calls.SendMove(233, 78, "A");  CG.Calls.SendMove(162, 130, "C"); Thread.Sleep(2000); return; }
                   
                };
                if (CG.Player.X == 162 & CG.Player.Y == 130)
                {
                    if (p == 1 || p == 7) { CG.Calls.SendMove(162, 130, "C"); CG.Calls.SendMove(72, 123, "C"); Thread.Sleep(2000); return; }
                    
                    if (p == 3 || p == 8) { return; }
                    
                    if (p == 6 || p == 9) { CG.Calls.SendMove(162, 130, "C"); Thread.Sleep(1000); return; }
                    
                };
                if (CG.Player.X == 72 & CG.Player.Y == 123)
                {
                    if (p == 1 || p == 7) { CG.Calls.SendMove(72, 123, "C"); Thread.Sleep(1000); return; }
                    
                    if (p == 3 || p == 8) { CG.Calls.SendMove(72, 123, "C"); CG.Calls.SendMove(233, 78, "A"); Thread.Sleep(2000); return; }
                    
                    if (p == 6 || p == 9) { return; }
                  
                    
                };
                if (CG.Player.X == 242 & CG.Player.Y == 100)
                {
                    
                    if (p == 2 || p == 7) { return; }
                    
                    if (p == 4 || p == 8) { CG.Calls.SendMove(242, 100, "C"); Thread.Sleep(1000); return; }
                    if (p == 5 || p == 9) { CG.Calls.SendMove(242, 100, "C"); CG.Calls.SendMove(141, 148, "A"); Thread.Sleep(2000); return; }
                    
                    
                };
                if (CG.Player.X == 141 & CG.Player.Y == 148)
                {
                    if (p == 2 || p == 7) { CG.Calls.SendMove(141, 148, "A"); CG.Calls.SendMove(63, 79, "A"); Thread.Sleep(2000); return; }

                    if (p == 4 || p == 8) { return; }
                    if (p == 5 || p == 9) { CG.Calls.SendMove(141, 148, "A"); Thread.Sleep(1000); return; }

                };
                if (CG.Player.X == 63 & CG.Player.Y == 79)
                {
                    if (p == 2 || p == 7) { CG.Calls.SendMove(63, 79, "A"); Thread.Sleep(1000); return; }

                    if (p == 4 || p == 8) { CG.Calls.SendMove(63, 79, "A"); CG.Calls.SendMove(242, 100, "C"); Thread.Sleep(2000); return; }
                    if (p == 5 || p == 9) { return; }

                    
                };
            }
        }

        private void Sell_Falan()
        {

        }

    }
}
