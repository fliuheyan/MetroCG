using System.Threading;
using dmnet;
namespace CrossGateHelper.Funtions
{
    public class SpeedUp
    {
        private CrossGate CG;
        public SpeedUp(CrossGate p)
        {
            CG = p;
            t = new Thread(new ThreadStart(ThreadChangeSpeed));
            t.Start();
        }

       

        Thread t;
        bool stop;
        private int speed;

        public void StopThread() { stop = true; }
        
        public void ChangeSpeed(int vspeed)
        {
            speed = vspeed;

        }
        

        
        private void ThreadChangeSpeed()
        {
            while (stop == false)
            {
                if (speed != 0)
                {
                    if (CG.Type == 1)
                    {

                        dm.d.WriteDouble(CG.Hwnd, "0071CFD0", dm.d.ReadDouble(CG.Hwnd, "0071CFD0") - speed);

                    }
                    if (CG.Type == 2)
                    {


                        dm.d.WriteDouble(CG.Hwnd, "007D12B8", dm.d.ReadDouble(CG.Hwnd, "007D12B8") - speed);

                    }
                    if (CG.Type == 3)
                    {




                    }
                }
                Thread.Sleep(10);

            }
            dm.d.SetWindowText(CG.Hwnd, "魔力宝贝");

        }





    }
}
