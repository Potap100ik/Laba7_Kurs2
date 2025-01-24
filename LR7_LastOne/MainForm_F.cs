using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace LR7_LastOne
{
    public partial class MainForm : Form
    {
        string filepath = "file.txt";//переменная для открывания файла с данными
        int messagescount = 0;//счетчик сообщений
        Device[] DevInventary;//список девайсов в инвентаре
        Device MainDev;//текущий девайс
        int MainImageIndex;
        HandleDeviceEventsProperties Notify;//сообщения в черном цвете
        HandleDeviceErrorsInWF ErrNotify;//смс об ошибках 
        FormEventArgs FormStatusArg;
        Image[] image;
        Random rnd;
        CancellationTokenSource cts;
        StringBuilder MsgB;

        private void FormLoad()
        {
            MainDev = null;
            DevInventary = null;
            Notify = new HandleDeviceEventsProperties(this, MSGHandle);
            ErrNotify = new HandleDeviceErrorsInWF(ErrorMSGHandle);
            FormStatusArg = new FormEventArgs(FormStatus);
            MainImageIndex = 0;
            image = new Image[]
            {
            Image.FromFile("empty.jpg"),
            Image.FromFile("printer1.jpg"),
            Image.FromFile("printer2.jpg"),
            Image.FromFile("printer3.jpg"),
            Image.FromFile("printer4.jpg"),
            Image.FromFile("printer5.jpg"),
            Image.FromFile("printer6.jpg"),
            Image.FromFile("printer7.jpg"),
            Image.FromFile("printer8.jpg"),
            Image.FromFile("printer9.jpg"),
            Image.FromFile("printer10.jpg")
            };
            rnd = new Random();
            MsgB = new StringBuilder(1000);
        }
        private void ShowImage(int index)
        {
            if (index > 0 && index < image.Length)
                pictureBox1.Image = image[index];
            else
                pictureBox1.Image = image[0];
        }
        //обработчики сообщений
        public void ErrorMSGHandle(DeviceEventArgs msg)
        {
            ++messagescount;
            MsgB.Clear();MsgB.Append(messagescount.ToString() + ")" + msg.ToString() + "\r\n");
            rtbMessage.SelectionStart = rtbMessage.Text.Length;
            rtbMessage.SelectionLength = 0;
            rtbMessage.SelectionColor = Color.Red;
            rtbMessage.AppendText(MsgB.ToString());
            rtbMessage.SelectionStart = rtbMessage.Text.Length;
            rtbMessage.ScrollToCaret();
            bInventoryShow.Focus();

        }
        public void MSGHandle(DeviceEventArgs msg)
        {
            MsgB.Clear();
            if (msg.status == null)
            {
                ++messagescount;
                MsgB.Append(messagescount.ToString() + ")" + msg.ToString() + "\r\n");
            }
            else
            {
                MsgB.Append(msg.ToString() + "\r\n");
            }
            rtbMessage.AppendText(MsgB.ToString());
            rtbMessage.SelectionStart = rtbMessage.Text.Length;
            rtbMessage.ScrollToCaret();
            bInventoryShow.Focus();
        }
        //обновляторы формы
        void UpdateGrid()
        {
            dgvListOfDevices.Rows.Clear();

            if (FormStatusArg.UserStat == FormEventArgs.Status.Inventary)
                if (DevInventary != null)
                    for (int i = 0; i < DevInventary.Length; ++i)
                    {
                        AddGridRow(DevInventary[i], i);
                    }
                else
                {
                    ErrNotify.ThrowMassage(new DeviceEventArgs( "Инвентарь пуст, надо заскочить в магазин"));
                }
            dgvListOfDevices.ClearSelection();
        }
        void UpdateMainInfo()
        {
            if (MainDev != null)
            {
                panelPlugStat.BackColor = MainDev.IsPluggedIn ? Color.Green : Color.Red;
                panelAssembleStat.BackColor = !MainDev.IsAssembled ? Color.Green : Color.Red;
                tbMainType.Text = MainDev.ToString();
                tbMainPrice.Text = MainDev.Price.ToString();
                tbMainManuf.Text = MainDev.Manufacturer;
                tbPaperCount.Text = MainDev is IPrinter printer ? printer.Papercount.ToString() : "--";
                
            }
            else
            {
                panelAssembleStat.BackColor = Color.LightGray;
                panelPlugStat.BackColor = Color.LightGray;
                tbPaperCount.Text = "--";
                tbMainType.Text = "пусто";
                tbMainPrice.Text = "";
                tbMainManuf.Text = "";
            }

        }
        void MainFormEnabled()
        {
            if (dgvListOfDevices.Rows.Count == 0)
            {
                bMainDeviceSwitch.BackColor = Color.LightGray;
                bMainDeviceSwitch.Enabled = false;
            }
            else
            {
                bMainDeviceSwitch.Enabled = true;
                bMainDeviceSwitch.BackColor = Color.LightSalmon;
            }

            if (MainDev == null)
            {
                gbMainDevice.BackColor = Color.LightGray;
                DisableButtons(gbManagerBut.Controls.OfType<Button>().ToArray());
                DisableButtons(gbPrintScanCopy.Controls.OfType<Button>().ToArray());

            }
            else if (MainDev is MFP)
            {
                gbMainDevice.BackColor = SystemColors.Control;
                EnableButtons(gbManagerBut.Controls.OfType<Button>().ToArray());
                EnableButtons(gbPrintScanCopy.Controls.OfType<Button>().ToArray());
            }
            else if (MainDev is Scanner)
            {
                gbMainDevice.BackColor = SystemColors.Control;
                EnableButtons(gbManagerBut.Controls.OfType<Button>().ToArray());
                DisableButtons(gbPrintScanCopy.Controls.OfType<Button>().ToArray());
                EnableButtons(bScan);
            }
            else if (MainDev is Printer)
            {
                gbMainDevice.BackColor = SystemColors.Control;
                EnableButtons(gbManagerBut.Controls.OfType<Button>().ToArray());
                DisableButtons(gbPrintScanCopy.Controls.OfType<Button>().ToArray());
                EnableButtons(bPrint, bPaperCash);
            }
            else if (MainDev is Device)
            {
                gbMainDevice.BackColor = SystemColors.Control;
                EnableButtons(gbManagerBut.Controls.OfType<Button>().ToArray());
                DisableButtons(gbPrintScanCopy.Controls.OfType<Button>().ToArray());
            }
            ShowImage(MainImageIndex);

        }
        //оформление батонов
        void DisableButtons(params Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.Enabled = false;
                button.BackColor = Color.LightGray;
            }

        }
        void EnableButtons(params Button[] buttons)
        {
            foreach (var button in buttons)
            {
                button.Enabled = true;
                button.BackColor = Color.LightCyan;
            }

        }
        //управление данными формы - составление новых объектов из файлов или таблиц
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
                    result = new Device((int)nudPrice_OtBaldy.Value, tbManufact_OtBaldy.Text, Notify, ErrNotify);
                }
                else if (rbPrinter.Checked)
                {
                    result = new Printer((int)nudPrice_OtBaldy.Value, tbManufact_OtBaldy.Text, Notify, ErrNotify);
                }
                else if (rbScanner.Checked)
                {
                    result = new Scanner((int)nudPrice_OtBaldy.Value, tbManufact_OtBaldy.Text, Notify, ErrNotify);
                }
                else if (rbMFP.Checked)
                {
                    result = new MFP((int)nudPrice_OtBaldy.Value, tbManufact_OtBaldy.Text, Notify, ErrNotify);
                }
                else
                    ErrNotify.ThrowMassage(new DeviceEventArgs( "Как так получилось, что ничего не выбрано???"));
                AddGridRow(result, dgvListOfDevices.Rows.Count);
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(new DeviceEventArgs( string.Format(ex.Message)));
            }
           
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
                        result = FileReader.GetDevices(Notify, ErrNotify, filepath);
                    }
                }
                else
                {
                    result = FileReader.GetDevices(Notify, ErrNotify, filepath);
                }
                return result.Length != 0 ? result : null;
            }
            catch (ArgumentException ex)
            {
                ErrNotify.ThrowMassage(new DeviceEventArgs("FileError: " + ex.Message));
                return null;
            }
            catch (FileNotFoundException ex)
            {
                ErrNotify.ThrowMassage(new DeviceEventArgs("FileError: " + ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(new DeviceEventArgs("FileError: " + ex.Message));
                return null;
            }
        }
        private int LetsShopping(Device[] device)
        {
            try
            {
                for (int i = 0; i < device.Length; ++i)
                    AddGridRow(device[i], i);
                return 0;
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(new DeviceEventArgs( string.Format(ex.Message)));
                return 1;
            }
        }
        //оформление таблиц
        private void AddGridRow(Device device, int i)
        {
            Color plugcolor, assemblecolor;
            dgvListOfDevices.Rows.Add(i + 1, device.ToString(), device.Manufacturer, device.Price, GetPlugStat(device), GetAssembleStat(device)); ;

            plugcolor = device.IsPluggedIn ? Color.LightGreen : Color.LightPink;
            assemblecolor = !device.IsAssembled ? Color.LightGreen : Color.LightPink;

            dgvListOfDevices.Rows[i].Cells["PlugStatus"].Style.BackColor = plugcolor;
            dgvListOfDevices.Rows[i].Cells["AssembleStatus"].Style.BackColor = assemblecolor;
        }
        private string GetPlugStat(Device dev) => dev.IsPluggedIn ? "Вкл" : "Выкл";
        private string GetAssembleStat(Device dev) => dev.IsAssembled ? "разобран" : "собран";
    }
}
