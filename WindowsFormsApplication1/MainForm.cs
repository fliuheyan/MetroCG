using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using dmnet;
using MetroFramework;

namespace CrossGateHelper
{
    public partial class MainForm : MetroForm
    {
        
        static public CrossGate CG=new CrossGate();

        public MainForm()
        {
            InitializeComponent();
            Random randomstyle = new Random();
            metroStyleManager.Theme = (MetroThemeStyle)randomstyle.Next(1, 3);
            metroStyleManager.Style = (MetroColorStyle)randomstyle.Next(3, 15);
            itemBindingSource.DataSource = CG.Bag.item;
            unitBindingSource.DataSource = CG.AutoFight.Unit;
            metroTrackBar1.Value = 3;
            CG.SpeedUp.ChangeSpeed(metroTrackBar1.Value);
            metroToggle2.Checked = true;
            metroToggle5.Checked = true;

        }
       
        
        private void metroTileSwitch_Click(object sender, EventArgs e)
        {
            
            if (metroStyleManager.Style > (MetroColorStyle)14) { metroStyleManager.Style = 0; }
            metroStyleManager.Style = metroStyleManager.Style + 1;
            
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            metroStyleManager.Theme = metroStyleManager.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
        }

       

        private void metroButton5_Click(object sender, EventArgs e)
        {
            metroContextMenu1.Show(metroButton5, new Point(0, metroButton5.Height));
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "This is a sample MetroMessagebox `OK` only button", "MetroMessagebox", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "This is a sample MetroMessagebox `OK` and `Cancel` button", "MetroMessagebox", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "This is a sample MetroMessagebox `Yes` and `No` button", "MetroMessagebox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "This is a sample MetroMessagebox `Yes`, `No` and `Cancel` button", "MetroMessagebox", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        private void metroButton11_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "This is a sample MetroMessagebox `Retry` and `Cancel` button.  With warning style.", "MetroMessagebox", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "This is a sample MetroMessagebox `Abort`, `Retry` and `Ignore` button.  With Error style.", "MetroMessagebox", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        }

        private void metroButton12_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "This is a sample `default` MetroMessagebox ", "MetroMessagebox");
            
        }


       
        private void metroTrackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            this.metroToolTip.SetToolTip(this.metroTrackBar1, this.metroTrackBar1.Value.ToString());
        }

        private void metroTile1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i <= 9; i++)
            {
                CG.Scripts.TP(i);
                MessageBox.Show("");
            }
            
            

            



        }
        
        private void metroTrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            CG.SpeedUp.ChangeSpeed(metroTrackBar1.Value);
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            CG.SpeedFight.Available = metroToggle1.Checked;
        
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            if (metroTile2.Text == "Is\nMoving...") { CG.Calls.StopFightNow = true; }
            else
            {
                metroTile2.Text = "Is\nMoving...";
                CG.Calls.FightNow();
                metroTile2.Text = "Fight\nHere";
            }
         



        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            
            
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
           
            CG.BindWindow();
            if (CG.Hwnd > 0)
            {
                Point temp = CG.Location;
                temp.X += 2;
                temp.Y += 507;
                Location = temp;
            }

        }

        private void metroToggle2_CheckedChanged(object sender, EventArgs e)
        {
            
            CG.FuntionThread.AutoFight= metroToggle2.Checked;
        }

        private void itemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void metroToggle3_CheckedChanged(object sender, EventArgs e)
        {
            CG.FuntionThread.AutoSell = metroToggle3.Checked;
        }

        private void metroToggle5_CheckedChanged(object sender, EventArgs e)
        {
            CG.FuntionThread.AutoNurse= metroToggle5.Checked;
        }

        private void metroProgressSpinner4_MouseDown(object sender, MouseEventArgs e)
        {
            metroProgressSpinner4.Visible = false;
        }

        private void metroProgressSpinner4_MouseUp(object sender, MouseEventArgs e)
        {
            int h = dm.d.GetMousePointWindow();
            if (dm.d.GetWindowClass(h) == "原生态魔力")
            {
                CG.Hwnd = h;
                
            }
            metroProgressSpinner4.Visible = true;   
        }
    }
}

