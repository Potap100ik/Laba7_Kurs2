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
            FormLoad();
            //настроим размеры экрана
            this.Width = 1150;
            this.Height = 452;
            //изменяем доступность полей
            UserStat.Invoke(UserEventArgs.Status.StartPosition);
            
        }
        
        //батоны все здесь
        private void bEnterDNS_Click(object sender, EventArgs e)
        {
            UserStat.Invoke(UserEventArgs.Status.ShoppingDNS);
            

        }

        private void bBuy_OtBaldy_Click(object sender, EventArgs e)
        {
            UserStat.Invoke(UserEventArgs.Status.Shopping_OtBaldy);
        }

        private void bShowInventory_Click(object sender, EventArgs e)
        {
            UserStat.Invoke(UserEventArgs.Status.Inventary);
        }

        private void bSwitchMainDevice_Click(object sender, EventArgs e)
        {
            if (dgvListOfDevices.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvListOfDevices.SelectedRows[0];
                string Type = selectedRow.Cells["Type"].Value.ToString();
                int price = Convert.ToInt32(selectedRow.Cells["Price"].Value);
                string manuf = selectedRow.Cells["Manufacturer"].Value.ToString();
                Device result = null;
                try
                {

                }
                catch (Exception ex)
                {
                    ErrNotify.ThrowMassage(ex.Message);
                }
                AddDevice(result);
            }
            else
            {
                ErrNotify.ThrowMassage("Сначала надо выбрать строку, чтобы ее добавить в инвентарь");
            }
        }

        private void bBuyForFree_Click(object sender, EventArgs e)
        {
            if (Status == UserEventArgs.Status.Shopping_OtBaldy)
            {
                Device result = CreateDevice();
                if (result != null)
                    AddDevice(result);
            }
            else if (Status == UserEventArgs.Status.ShoppingDNS)
            {
                if (dgvListOfDevices.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvListOfDevices.SelectedRows[0];
                    string Type = selectedRow.Cells["Type"].Value.ToString();
                    int price = Convert.ToInt32(selectedRow.Cells["Price"].Value);
                    string manuf = selectedRow.Cells["Manufacturer"].Value.ToString();
                    Device result = null;
                    try
                    {
                        if (Type == Device.GetClass())
                        {
                            result = new Device(price, manuf, Notify);
                        }
                        else if (Type == Printer.GetClass())
                        {
                            result = new Printer(price, manuf, Notify);
                        }
                        else if (Type == Scanner.GetClass())
                        {
                            result = new Scanner(price, manuf, Notify);
                        }
                        else if (Type == MFP.GetClass())
                        {
                            result = new MFP(price, manuf, Notify);
                        }
                        else
                            ErrNotify.ThrowMassage("Как так получилось, не понятно, но что поделать");
                    }
                    catch (Exception ex)
                    {
                        ErrNotify.ThrowMassage(ex.Message);
                    }
                    AddDevice(result);
                }
                else
                {
                    ErrNotify.ThrowMassage("Сначала надо выбрать строку, чтобы ее добавить в инвентарь");
                }
            }
            else
                ErrNotify.ThrowMassage("как так получилось: нажатие кнопки покупки запрещено");

        }

        private void bLeaveDNS_Click(object sender, EventArgs e)
        {
            UserStat.Invoke(UserEventArgs.Status.StartPosition);
        }

        private void bPlugSwitch_Click(object sender, EventArgs e)
        {
            UserM(UserEventArgs.Move.PlugSwitch);
        }

        private void bAssem_Click(object sender, EventArgs e)
        {
            UserM(UserEventArgs.Move.Assem);
        }

        private void bDisassSam_Click(object sender, EventArgs e)
        {
            UserM(UserEventArgs.Move.DisassSam);
        }

        private void bDisassShop_Click(object sender, EventArgs e)
        {
            UserM(UserEventArgs.Move.DisassShop);
        }

        private void bThrowDev_Click(object sender, EventArgs e)
        {
            UserM(UserEventArgs.Move.ThrowDev);
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            UserM(UserEventArgs.Move.Print);
        }

        private void bScan_Click(object sender, EventArgs e)
        {
            UserM(UserEventArgs.Move.Scan);
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            UserM(UserEventArgs.Move.Copy);
        }

        private void timerClearMSG_Tick(object sender, EventArgs e)
        {
            rtbMessage.Clear();
            timerClearMSG.Stop();
        }
    }
}
