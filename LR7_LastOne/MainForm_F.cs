using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR7_LastOne
{
    class HandleDeviceEventsWF : Device_interface.HandleDeviceEvents
    {
        public HandleDeviceEventsWF() : base()
        {
            UnRegisterHandler();
            DeviceHandler de = this.HandleProperties;
            de += this.HandleCritErrors;
            de += HandleErrors;
            de += HandleMassages;
            RegisterHandler(de);
        }
        public HandleDeviceEventsWF(HandleWriter msg) : this()
        {
            UnRegisterWriter();
            RegisterWriter(msg);
        }
        new protected void HandleProperties(Device_interface device, DeviceEventArgs e)
        {

            switch (e.property)
            {
                case null:
                    break;
                case DeviceEventArgs.PropertyType.Assemble_changed:
                    {
                        RaiseLogEvent(device, new DeviceEventArgs($"WFAssemble changed of {device.ToString()}"));
                    }
                    break;
                case DeviceEventArgs.PropertyType.Plug_changed:
                    {
                        RaiseLogEvent(device, new DeviceEventArgs("WFPlug changed of {device.ToString()}"));
                    }
                    break;
                case DeviceEventArgs.PropertyType.Copying:
                    {
                        RaiseLogEvent(device, new DeviceEventArgs("WF{device.ToString()} is copying"));
                    }
                    break;
                case DeviceEventArgs.PropertyType.Printing:
                    {
                        RaiseLogEvent(device, new DeviceEventArgs("WF{device.ToString()} is printing"));
                    }
                    break;
                case DeviceEventArgs.PropertyType.Scanning:
                    {
                        RaiseLogEvent(device, new DeviceEventArgs("WF{device.ToString()} is scanning"));
                    }
                    break;
            }
        }
        new protected void HandleCritErrors(Device_interface device, DeviceEventArgs e)
        {
            switch (e.criterror)
            {
                case null:
                    break;
                case DeviceEventArgs.CritErrorType.ErrPrice_Less_Then_Min:
                    {
                        throw new ArgumentException(string.Format("WF {0} {1} can't have so little price", device.ToString(), device.Manufacturer));
                    }
                case DeviceEventArgs.CritErrorType.ErrPrice_More_Then_Max:
                    {
                        throw new ArgumentException(string.Format("WF {0} {1} can't have so high price", device.ToString(), device.Manufacturer));
                    }
                case DeviceEventArgs.CritErrorType.ErrManufName_TooLong:
                    {
                        throw new ArgumentException(string.Format("WF {0} can't have so long manufacturer's name: {1}", device.ToString(), device.Manufacturer));
                    }
            }
        }
    }

    public partial class MainForm : Form
    {
        delegate void UserStatusHandle(UserEventArgs.Status stat);
        delegate void UserMoveHandle(UserEventArgs.Move move);
        delegate void UserErrorHandle(UserEventArgs.Errors err);

        string filepath = "data.txt";

        Device[] DevInventary;
        Device MainDev;
        HandleDeviceEventsWF Notify;
        HandleDeviceEventsWF ErrNotify;
        event UserStatusHandle UserStat;
        event UserMoveHandle UserM;
        event UserErrorHandle UserErr;
        UserEventArgs.Status Status;
        private void FormLoad()
        {
            MainDev = null;
            DevInventary = null;
            Notify = new HandleDeviceEventsWF(MSGHandle);
            ErrNotify = new HandleDeviceEventsWF(ErrorMSGHandle);
            UserStat = UsStatusSwitch;
            UserM = UsMoveCompile;
            UserErr = UsErrorHandle;
            Status = UserEventArgs.Status.StartPosition;
        }
        public void ErrorMSGHandle(string msg)
        {
            rtbMessage.SelectionColor = System.Drawing.Color.Red;
            rtbMessage.Text = msg + "\r\n\r\n" + rtbMessage.Text;
            //rtbMessage.AppendText(msg + Environment.NewLine);
            //rtbMessage.SelectionColor = System.Drawing.Color.Black;
            //rtbMessage.SelectionStart = rtbMessage.Text.Length;
            rtbMessage.ScrollToCaret();
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
        public void MSGHandle(string msg)
        {
            rtbMessage.Text = msg + "\r\n\r\n" +rtbMessage.Text;
            //rtbMessage.SelectionStart = rtbMessage.Text.Length;
            rtbMessage.ScrollToCaret();
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
                    ErrNotify.ThrowMassage("Как так получилось, не понятно, но что поделать");
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
            dgvListOfDevices.Rows.Add(result.ToString(), result.Manufacturer, result.Price, GetPlugStat(result), GetAssembleStat(result)); 
            return result;
        }
        private Device[] GetDevices()
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
                foreach (var dev in device)
                {
                    dgvListOfDevices.Rows.Add(dev.ToString(), dev.Manufacturer, dev.Price, GetPlugStat(dev), GetAssembleStat(dev)); ;
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
