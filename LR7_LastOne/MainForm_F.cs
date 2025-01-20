using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR7_LastOne
{
    public partial class MainForm : Form
    {
        Device[] DevInventary;
        //Device maindevice;
        string filepath = "data.txt";
        private delegate void MessageHandler(string msg);
        private event MessageHandler ErrorHandler;
        private void FormLoad()
        {
            
        }

        public void ErrorMSGHandle(string msg)
        {
            tbMessage.Text += msg + "\r\n\r\n";
        }
        private int GetDevices()
        {
            try
            {
                if (cbDNS.Checked)
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        // Открытие выбранного файла
                        filepath = openFileDialog1.FileName;
                        DevInventary = FileReader.GetDevices(ErrorMSGHandle, filepath);
                    }
                }
                else
                {
                    DevInventary = FileReader.GetDevices(ErrorMSGHandle, filepath);
                }
                if (DevInventary.Length != 0)
                    return 0;
                else
                    return 1;
            }
            catch (ArgumentException ex)
            {
                tbMessage.Text = ex.Message;
                return 1;
            }
            catch (FileNotFoundException ex)
            {
                tbMessage.Text = ex.Message;
                return 1;
            }
            catch (Exception ex)
            {
                tbMessage.Text = ex.Message;
                return 1;
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
        private int LetsShopping()
        {
            try
            {

                foreach (var dev in DevInventary)
                {
                    dgvListOfDevices.Rows.Add(dev.ToString(), dev.Manufacturer, dev.Price, GetPlugStat(dev), GetAssembleStat(dev)); ;
                }
                return 0;
            }
            catch (Exception ex)
            {
                tbMessage.Text = ex.Message;
                return 1;
            }
        }
    }
}
