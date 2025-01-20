using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LR7_LastOne
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        
        private void MainForm_Load(object sender, EventArgs e)
        {
            //настроим размеры экрана
            this.Width = 1150;
            this.Height = 452;
            //изменяем доступность полей
            gbBuyLeave.Enabled = false;
            gbListOfDevices.Enabled = false;
            gbMainDevice.Enabled = false;
            gbManagerBut.Enabled = false;
            gbShop_OtBaldy.Enabled = false;
            FormLoad();


        }
        
        //батоны все здесь
        private void bEnterDNS_Click(object sender, EventArgs e)
        {
            if (GetDevices() == 0)
            {
                gbBuyLeave.Enabled = true;
                gbListOfDevices.Enabled = true;
                LetsShopping();

            }
            else
            {

            }

        }

        private void bBuy_OtBaldy_Click(object sender, EventArgs e)
        {

        }

        private void bShowInventory_Click(object sender, EventArgs e)
        {

        }

        private void bSwitchMainDevice_Click(object sender, EventArgs e)
        {

        }

        private void bBuyForFree_Click(object sender, EventArgs e)
        {

        }

        private void bLeaveDNS_Click(object sender, EventArgs e)
        {

        }

        private void bPlugSwitch_Click(object sender, EventArgs e)
        {

        }

        private void bAssem_Click(object sender, EventArgs e)
        {

        }

        private void bDisassSam_Click(object sender, EventArgs e)
        {

        }

        private void bDisassShop_Click(object sender, EventArgs e)
        {

        }

        private void bThrowDev_Click(object sender, EventArgs e)
        {

        }

        private void bPrint_Click(object sender, EventArgs e)
        {

        }

        private void bScan_Click(object sender, EventArgs e)
        {

        }

        private void bCopy_Click(object sender, EventArgs e)
        {

        }

        private void timerClearMSG_Tick(object sender, EventArgs e)
        {
            tbMessage.Clear();
            timerClearMSG.Stop();
        }
    }
}
