using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR7_LastOne
{


    public partial class MainForm : Form
    {
        string filepath = "data.txt";

        Device[] DevInventary;
        Device MainDev;
        HandleDeviceEventsPROP Notify;
        HandleDeviceEventsWF ErrNotify;
        HandleDeviceEventsWF.FormHandleArgs FormStatusArg;

        private void FormLoad()
        {
            MainDev = null;
            DevInventary = null;
            Notify = new HandleDeviceEventsPROP(this, MSGHandle);
            ErrNotify = new HandleDeviceEventsPROP(this, ErrorMSGHandle);
            FormStatusArg = new HandleDeviceEventsPROP.FormHandleArgs(FormStatus);
        }
        public void ErrorMSGHandle(string msg)
        {
            
            rtbMessage.SelectionColor = System.Drawing.Color.Red;
            //rtbMessage.Text = msg + "\r\n" + rtbMessage.Text;
            rtbMessage.AppendText(msg + Environment.NewLine);
            rtbMessage.SelectionStart = rtbMessage.Text.Length - 1;
            rtbMessage.ScrollToCaret();
        }
        public void MSGHandle(string msg)
        {
            rtbMessage.SelectionColor = System.Drawing.Color.Black;
            rtbMessage.Text = msg + "\r\n" + rtbMessage.Text;
            rtbMessage.SelectionStart = rtbMessage.Text.Length- 1 ;
            rtbMessage.ScrollToCaret();
        }
        void UpdateGrid()
        {
            dgvListOfDevices.Rows.Clear();

            if (FormStatusArg.UserStat == HandleDeviceEventsWF.FormHandleArgs.Status.Inventary)
                if (DevInventary != null)
                    for (int i = 0; i < DevInventary.Length; ++i)
                    {
                        dgvListOfDevices.Rows.Add(i + 1, DevInventary[i].ToString(), DevInventary[i].Manufacturer, DevInventary[i].Price, GetPlugStat(DevInventary[i]), GetAssembleStat(DevInventary[i])); ;
                    }
                else
                {
                    ErrNotify.ThrowMassage("Инвентарь пуст, надо заскочить в магазин");
                }
            dgvListOfDevices.ClearSelection();
        }
        void UpdateMain()
        {
            if (MainDev != null)
            {
                if (MainDev.IsPluggedIn)
                    panelPlugStat.BackColor = Color.Green;
                else
                    panelPlugStat.BackColor = Color.Red;
                if (MainDev.IsAssembled)
                    panelAssembleStat.BackColor = Color.Red;
                else
                    panelAssembleStat.BackColor = Color.Green;

                tbMainType.Text = MainDev.ToString();
                tbMainPrice.Text = MainDev.Price.ToString();
                tbMainManuf.Text = MainDev.Manufacturer;
                if (MainDev is Printer printer)
                    tbPaperCount.Text = printer.Papercount.ToString();
                else
                    tbPaperCount.Text = "--";
            }
            else
            {
                panelAssembleStat.BackColor = Color.LightGray;
                panelPlugStat.BackColor = Color.LightGray;
                tbPaperCount.Text = "--";
                tbMainType.Text = "";
                tbMainPrice.Text = "";
                tbMainManuf.Text = "";


            }
        }
        private void AddDevice(Device dev)
        {
            Device[] result = null;
            if (DevInventary == null)
            {

                result = new Device[1];
                result[0] = dev;
            }
            else
            {
                result = new Device[DevInventary.Length + 1];
                for (int i = 0; i < DevInventary.Length; ++i)
                {
                    result[i] = DevInventary[i];
                }
                result[result.Length - 1] = dev;
            }

            DevInventary = result;
        }
        private Device CreateDevice()
        {
            Device result = null;
            try
            {
                if (rbDevice.Checked)
                {
                    result = new Device((int)nudPrice_OtBaldy.Value, tbManufact_OtBaldy.Text, Notify);
                }
                else if (rbPrinter.Checked)
                {
                    result = new Printer((int)nudPrice_OtBaldy.Value, tbManufact_OtBaldy.Text, Notify);
                }
                else if (rbScanner.Checked)
                {
                    result = new Scanner((int)nudPrice_OtBaldy.Value, tbManufact_OtBaldy.Text, Notify);
                }
                else if (rbMFP.Checked)
                {
                    result = new MFP((int)nudPrice_OtBaldy.Value, tbManufact_OtBaldy.Text, Notify);
                }
                else
                    ErrNotify.ThrowMassage("Как так получилось, что ничего не выбрано???");
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
            dgvListOfDevices.Rows.Add(dgvListOfDevices.Rows.Count + 1, result.ToString(), result.Manufacturer, result.Price, GetPlugStat(result), GetAssembleStat(result));
            return result;
        }
        private Device[] LoadDevicesFromFile()
        {
            try
            {
                Device[] result = null;
                if (cbDNS.Checked)
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        // Открытие выбранного файла
                        filepath = openFileDialog1.FileName;
                        result = FileReader.GetDevices(Notify, filepath);
                    }
                }
                else
                {
                    result = FileReader.GetDevices(Notify, filepath);
                }
                if (result.Length != 0)
                    return result;
                else
                    return null;
            }
            catch (ArgumentException ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
                return null;
            }
            catch (FileNotFoundException ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
                return null;
            }
        }
        private string GetPlugStat(Device dev)
        {
            if (dev.IsPluggedIn)
                return "Вкл";
            else
                return "Выкл";
        }
        private string GetAssembleStat(Device dev)
        {
            if (dev.IsAssembled)
                return "разобран";
            else
                return "собран";

        }
        private int LetsShopping(Device[] device )
        {
            try
            {
                for (int i = 0; i < device.Length; ++i)
                {
                    dgvListOfDevices.Rows.Add(i+1, device[i].ToString(), device[i].Manufacturer, device[i].Price, GetPlugStat(device[i]), GetAssembleStat(device[i])); ;

                }
                return 0;
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
                return 1;
            }
        }
    }
}
