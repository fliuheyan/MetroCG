using dmnet;

namespace CrossGateHelper.Funtions

{
    public class SpeedFight
    {
        private CrossGate CG;
        public SpeedFight(CrossGate p)
        {
            CG = p;

        }

        
        private bool available;

        public bool Available
        {
            get
            {
                return available;
            }

            set
            {
                if (value == true)
                {
                    dm.d.WriteData(CG.Hwnd, "004322DA", "90 90");
                };
                if (value == false)
                {
                    dm.d.WriteData(CG.Hwnd, "004322DA", "75 4D");
                };
                available = value;
            }
        }
    }
}
