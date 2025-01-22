using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR7_LastOne
{

    public partial class MainForm : Form
    {
        class HandleDeviceEventsWF : Device_interface.HandleDeviceEvents
        {
            public HandleDeviceEventsWF() : base()
            {
                UnRegisterHandler(HandleUserErrors);
                UnRegisterHandler(HandleProperties);
                DeviceHandler de = HandleUserErrors;
                de += this.HandleProperties;
                RegisterHandler(de);
            }
            public HandleDeviceEventsWF(HandleWriter msg) : this()
            {
                UnRegisterWriter();
                RegisterWriter(msg);
            }
            protected void HandleUserErrors(Device_interface device, DeviceEventArgs e)
            {
                if (e is UserEventArgs)
                    switch (((UserEventArgs)e).userErr)
                    {

                        case UserEventArgs.UserErrors.ErrMainDevEmpty:
                            {
                                ThrowMassage("Главное устройство не выбрано, незачем так тыкать");
                            }
                            break;
                        case UserEventArgs.UserErrors.ErrOptionalLost:
                            {
                                ThrowMassage("Устройство не так многофункционально как кажется. Эту операцию я не могу вам позволить");
                            }
                            break;
                    }
                else
                    ThrowMassage("Что-то наследование не полиморфируется, надо разбаговывать");
            }
            public class UserEventArgs : DeviceEventArgs
            {
                public UserErrors? userErr;
                public UserEventArgs(string message = null) : base(message) { userErr = null; }
                public UserEventArgs(UserErrors usererr, string message = null) : this(message) { userErr = usererr; }
                public enum UserErrors
                {
                    ErrMainDevEmpty,
                    ErrOptionalLost,
                }

            }
            public class FormHandleArgs : UserEventArgs
            {
                public delegate void FormStatusHandler(DeviceEventArgs msg);
                public event FormStatusHandler StatusHandler;
                private Status? userStat;
                private UserProperties? userProp;
                public UserProperties? UserProp { get => userProp; set { userProp = value; StatusHandler(this); } }
                public Status? UserStat { get => userStat; set { Clear(); userStat = value; StatusHandler(this); } }
                public FormHandleArgs(FormStatusHandler notify) : base()
                {
                    StatusHandler = notify;
                    UserStat = null;
                    UserProp = null;
                }

                public void RegisterEvent(FormStatusHandler notify)
                {
                    StatusHandler = notify;
                }
                public void Clear()
                {
                    UserProp = null;
                }
                public enum UserProperties
                {
                    BuyDevice,
                    MainSwitch
                }
                public enum Status
                {
                    ShoppingDNS,
                    Shopping_OtBaldy,
                    Inventary

                }
            }
        }
       
        class HandleDeviceEventsPROP : HandleDeviceEventsWF//не могу регистрировать обработчик событий вне класса
        {
            protected MainForm form;
            public HandleDeviceEventsPROP(MainForm form) : base()
            {
                this.form = form;
                UnRegisterHandler();
                RegisterHandler(HandleProperties);
            }
            public HandleDeviceEventsPROP(MainForm form, HandleWriter msg) : this(form)
            {
                UnRegisterWriter();
                RegisterWriter(msg);
            }
            protected void HandleProperties(Device_interface device, DeviceEventArgs e)
            {
                if (device == null)
                {
                    HandleUserErrors(device, new UserEventArgs(UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
                switch (e.property)
                {
                    case null:
                        break;
                    case UserEventArgs.PropertyType.Assembled:
                        {

                                if (device.IsAssembled)
                                    form.panelAssembleStat.BackColor = Color.Red;
                                else
                                    form.panelAssembleStat.BackColor = Color.Green;
                            
                        }
                        break;
                    case UserEventArgs.PropertyType.Plug_changed:
                        {

                                if (device.IsPluggedIn)
                                    form.panelPlugStat.BackColor = Color.Green;
                                else
                                    form.panelPlugStat.BackColor = Color.Red;
                            
                        }
                        break;
                    case UserEventArgs.PropertyType.DisassSam:
                        {
                            form.Notify.ThrowMassage("DisassambleSam");
                        }
                        break;
                    case UserEventArgs.PropertyType.DisassShop:
                        {
                            form.Notify.ThrowMassage("DisassambleShop");
                        }
                        break;
                    case UserEventArgs.PropertyType.ThrowDev:
                        {
                            device = null;
                            Device[] result = new Device[form.DevInventary.Length - 1];
                            for (int i = 0, j = 0; i < form.DevInventary.Length && j < form.DevInventary.Length - 1; ++i, ++j)
                            {
                                if (form.DevInventary[i] != null)
                                    result[j] = form.DevInventary[i];
                                else
                                    --j;
                            }
                            form.DevInventary = result;
                        }
                        break;
                    case UserEventArgs.PropertyType.Copying:
                        {
                            form.Notify.ThrowMassage("Copy");
                        }
                        break;
                    case UserEventArgs.PropertyType.Printing:
                        {
                            form.Notify.ThrowMassage("Print");
                        }
                        break;
                    case UserEventArgs.PropertyType.Scanning:
                        {
                            form.Notify.ThrowMassage("Scan");
                        }
                        break;
                    case UserEventArgs.PropertyType.PaperCash:
                        {
                            form.Notify.ThrowMassage("Papercash");
                        }
                        break;
                }
                form.UpdateGrid();
                form.UpdateMain();
            }

        }
        
        void FormStatus(DeviceEventArgs Msg)
        {
            if (!(Msg is HandleDeviceEventsWF.FormHandleArgs msg))
            {
                ErrNotify.ThrowMassage("Проблемы с полиморфимическим наследованием, звоните в поддержку");
                return;
            }
            switch (msg.UserProp)
            {
                case HandleDeviceEventsWF.FormHandleArgs.UserProperties.BuyDevice:
                    {
                        switch (msg.UserStat)
                        {
                            case HandleDeviceEventsWF.FormHandleArgs.Status.ShoppingDNS:
                                {
                                    if (dgvListOfDevices.CurrentCell != null)
                                    {
                                        int selectedRowIndex = dgvListOfDevices.CurrentCell.RowIndex;
                                        string Type = dgvListOfDevices.Rows[selectedRowIndex].Cells["Type"].Value.ToString();
                                        int price = Convert.ToInt32(dgvListOfDevices.Rows[selectedRowIndex].Cells["Price"].Value);
                                        string manuf = dgvListOfDevices.Rows[selectedRowIndex].Cells["Manufacturer"].Value.ToString();
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
                                                ErrNotify.ThrowMassage("Не нашли подходящий тип данных, что происходит?");
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrNotify.ThrowMassage(ex.Message);
                                        }
                                        AddDevice(result);
                                    }
                                    else
                                    {
                                        ErrNotify.ThrowMassage("Чтобы что-нибудь купить, надо что-нибудь выбрать");
                                    }
                                }
                                break;
                            case HandleDeviceEventsWF.FormHandleArgs.Status.Shopping_OtBaldy:
                                {
                                    Device result = CreateDevice();
                                    if (result != null)
                                        AddDevice(result);
                                }
                                break;
                        }
                        dgvListOfDevices.ClearSelection();
                    }
                    break;
                case HandleDeviceEventsWF.FormHandleArgs.UserProperties.MainSwitch:
                    {
                        if (dgvListOfDevices.CurrentCell != null)
                        {
                            int index = dgvListOfDevices.CurrentCell.RowIndex;
                            try
                            {
                                MainDev = DevInventary[index];

                            }
                            catch (Exception ex)
                            {
                                ErrNotify.ThrowMassage(ex.Message);
                            }
                        }
                        else
                        {
                            ErrNotify.ThrowMassage("Чтобы выбрать мейна, надо выбрать мейна (в таблице)");
                        }
                        UpdateMain();
                    }
                    break;
                case null:
                    {
                        switch (msg.UserStat)
                        {
                            case HandleDeviceEventsWF.FormHandleArgs.Status.ShoppingDNS:
                                {
                                    dgvListOfDevices.Rows.Clear();
                                    dgvListOfDevices.ClearSelection();
                                    dgvListOfDevices.Text = "М.Видео";
                                    gbShop_OtBaldy.Enabled = false;
                                    bSwitchMainDevice.Enabled = false;
                                    gbByeLeave.Enabled = true;
                                    gbListOfDevices.Enabled = true;
                                    try
                                    {
                                        Device[] dev = LoadDevicesFromFile();
                                        if (dev != null)
                                        {
                                            LetsShopping(dev);
                                        }
                                        else
                                        {
                                            ErrNotify.ThrowMassage("Что-то пошло не так при походе в файловый ДНС");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrNotify.ThrowMassage(ex.Message);
                                    }
                                }
                                break;
                            case HandleDeviceEventsWF.FormHandleArgs.Status.Shopping_OtBaldy:
                                {
                                    dgvListOfDevices.Rows.Clear();
                                    dgvListOfDevices.ClearSelection();
                                    dgvListOfDevices.Text = "Шоппинг на заказ";
                                    bSwitchMainDevice.Enabled = false;
                                    gbListOfDevices.Enabled = true;
                                    gbShop_OtBaldy.Enabled = true;
                                    gbByeLeave.Enabled = true;
                                    cbDNS.Checked = false;
                                }
                                break;
                            case HandleDeviceEventsWF.FormHandleArgs.Status.Inventary:
                                {
                                    dgvListOfDevices.Rows.Clear();
                                    dgvListOfDevices.ClearSelection();
                                    dgvListOfDevices.Text = "Инвентарь";
                                    gbListOfDevices.Enabled = true;
                                    bSwitchMainDevice.Enabled = true;
                                    gbShop_OtBaldy.Enabled = false;
                                    gbByeLeave.Enabled = false;
                                    cbDNS.Checked = false;
                                    UpdateGrid();
                                }
                                break;
                        }
                    }
                    break;
            }
        }
    }
}