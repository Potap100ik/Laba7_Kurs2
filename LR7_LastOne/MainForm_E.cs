using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR7_LastOne
{

    public partial class MainForm : Form
    {
        class HandleDeviceErrorsInWF : Device_interface.HandleDeviceEvents
        {
            public HandleDeviceErrorsInWF() : base()
            {
                UnRegisterHandler(HandleProperties);
                RegisterHandler(HandleUserErrors);
            }
            public HandleDeviceErrorsInWF(HandleWriter msg) : this()
            {
                //UnRegisterWriter();
                RegisterWriter(msg);
            }
            protected void HandleUserErrors(Device_interface device, DeviceEventArgs e)
            {
                if (e is UserEventArgs)
                    switch (((UserEventArgs)e).userErr)
                    {
                        case null:
                            break;
                        case UserEventArgs.UserErrors.ErrMainDevEmpty:
                            {
                                ThrowMassage(new DeviceEventArgs( "Главное устройство не выбрано, незачем так тыкать"));
                            }
                            break;
                        case UserEventArgs.UserErrors.ErrOptionalLost:
                            {
                                ThrowMassage(new DeviceEventArgs( "Устройство не так многофункционально как кажется. Эту операцию я не могу вам позволить"));
                            }
                            break;
                        default:
                            ThrowMassage(new DeviceEventArgs( "Что-то наследование не полиморфируется, надо разбаговывать"));
                            break;
                    }

            }
        }
        public class UserEventArgs : DeviceEventArgs
        {
            public UserErrors? userErr;
            public UserEventArgs(string message = null, bool? status = null) : base(message, status) { userErr = null; }
            public UserEventArgs(UserErrors usererr, string message = null, bool? status = null) : this(message, status) { userErr = usererr; }
            public enum UserErrors
            {
                ErrMainDevEmpty,
                ErrOptionalLost,
            }

        }
        public class FormEventArgs : UserEventArgs
        {
            public delegate void FormStatusHandler(DeviceEventArgs msg);
            public event FormStatusHandler StatusHandler;
            private Status? userStat;
            private UserProperties? userProp;
            public UserProperties? UserProp { get => userProp; set { userProp = value; StatusHandler(this); } }
            public Status? UserStat { get => userStat; set { Clear(); userStat = value; StatusHandler(this); } }
            public FormEventArgs(FormStatusHandler notify) : base()
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
                userProp = null;
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
        class HandleDeviceEventsProperties : HandleDeviceErrorsInWF//не могу регистрировать обработчик событий вне класса
        {
            protected MainForm form;
            protected Random rnd;
            public HandleDeviceEventsProperties(MainForm form) : base()
            {
                rnd = new Random();
                this.form = form;
                UnRegisterHandler();
                RegisterHandler(HandleProperties);
            }
            public HandleDeviceEventsProperties(MainForm form, HandleWriter msg) : this(form)
            {
                UnRegisterWriter();
                RegisterWriter(msg);
            }
            new protected async void HandleProperties(Device_interface device, DeviceEventArgs e)
            {
                try
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
                        case UserEventArgs.PropertyType.Plug_changed:
                            {
                                if (device.IsPluggedIn)
                                {
                                    if (!device.IsAssembled)
                                    {
                                        if (rnd.Next(0, 100) < 20)
                                        {
                                            form.Notify.ThrowMassage(new DeviceEventArgs("Устройство включено в сеть (электрическую)"));
                                            form.Notify.ThrowMassage(new DeviceEventArgs("Начинается прогрев картриджей. БЖ"));
                                            form.cts = new System.Threading.CancellationTokenSource();
                                            for (int i = 0; i < 3; ++i)
                                            {
                                                await Task.Delay(400, form.cts.Token);
                                                form.Notify.ThrowMassage(new DeviceEventArgs("-Ж", false));
                                            }
                                           form.Notify.ThrowMassage(new DeviceEventArgs("ШЬМЯК! Вы прихлопнули муху и больше ничего не жужжит", false));
                                        }
                                        else
                                            form.Notify.ThrowMassage(new DeviceEventArgs( "Устройство подключено к сети и готово к работе"));
                                    }
                                    else
                                    {
                                        form.Notify.ThrowMassage(new DeviceEventArgs("Прежде чем тыкать вилку в розетку, надо бы собраться с силами и собрать устройство"));
                                        device.IsPluggedIn = false;
                                    }
                                }
                                else
                                {
                                    form.Notify.ThrowMassage(new DeviceEventArgs("Устройство выключено из сети и очень гурстит, включите его обратно!!!"));
                                    form.cts?.Cancel();
                                }

                            }
                            break;
                        case UserEventArgs.PropertyType.Assembled:
                            {

                                if (device.IsPluggedIn)
                                {
                                    form.Notify.ThrowMassage(new DeviceEventArgs("Допустим, вы такой смелый, что разобрали устройство, пока оно было под напряжением. Может пора вытащить вилку из розетки, чего она одна болтается?"));
                                }
                                else
                                    if (e.status == true)
                                        switch (rnd.Next(1, 3))
                                        {
                                            case 1: form.Notify.ThrowMassage(new DeviceEventArgs("Если дальше разбирать, то только на атомы")); break;

                                            case 2: form.Notify.ThrowMassage(new DeviceEventArgs("Внимание! дальнейшая разборка может привести к открытию новых физических законов")); break;
                                            case 3: form.Notify.ThrowMassage(new DeviceEventArgs("Устройство больше не разбирается. Никак и ни в чем, даже в php")); break;
                                            default: form.Notify.ThrowMassage(new DeviceEventArgs("Дальше разбираться нет смысла, это ведь не уроки программирования")); break;
                                        }
                                    else
                                        form.Notify.ThrowMassage(new DeviceEventArgs("Устройство теперь разобрано. Все винтики в коробочке или как обычно?"));
                                form.panelAssembleStat.BackColor = device.IsAssembled ? Color.Red : Color.Green;
                                form.cts?.Cancel();
                            }
                            break;
                        case UserEventArgs.PropertyType.DisassSam:
                            {

                                if (device.IsPluggedIn)
                                    form.ErrNotify.ThrowMassage(new DeviceEventArgs("Под напряжением собирать устройство явно веселее, но минздрав реккомендует таким не заниматься"));
                                else
                                {
                                    if (e.status == true)
                                    {
                                        if (rnd.Next(0, 100) < 70)//почти 50 на 50
                                        {
                                            device.IsAssembled = true;
                                            form.Notify.ThrowMassage(new DeviceEventArgs("Ну тут было 50 на 50. Увы, мы проиграли, теперь одна дорога - в мастерскую"));
                                        }
                                        else
                                            form.Notify.ThrowMassage(new DeviceEventArgs("Неважно с какой попытки, но удача обернулась нужным местом: мы самостоятельно собрали устройство. Не забыть бы лишние винтики в шкаф отнести к остальным"));
                                    }
                                    else
                                        form.Notify.ThrowMassage(new DeviceEventArgs("Собирай - не собирай, все одно - оно и так целое, непонятно что-ли??"));

                                }
                            }
                            break;
                        case UserEventArgs.PropertyType.DisassShop:
                            {
                                try { form.cts?.Cancel(); }
                                finally
                                {
                                    if (device.IsPluggedIn && e.status != false)
                                        form.ErrNotify.ThrowMassage(new DeviceEventArgs( "Вы его разобрали, а оно все еще включено сеть? Магия какая-то"));
                                    else
                                        form.Notify.ThrowMassage(new DeviceEventArgs("В мастеркой не знают, как чинить это. Ладно, шутка. С вас 4К за ремонт, можно без сдачи"));
                                    if (e.status == false)
                                        form.Notify.ThrowMassage(new DeviceEventArgs("Кстати, устройство и так было собрано. Лучше бы не носить его ни куда"));
                                }
                            }
                            break;
                        case UserEventArgs.PropertyType.ThrowDev:
                            {
                                try { form.cts?.Cancel(); }
                                finally
                                {
                                    if (form.DevInventary.Length == 1)
                                        form.DevInventary = null;
                                    else
                                    {
                                        Device[] result = form.DevInventary.Where(dev => dev != form.MainDev).ToArray();
                                        form.DevInventary = result;
                                    }

                                    form.MainDev = null;
                                    form.MainImageIndex = 0;
                                    form.UpdateMainInfo();
                                    form.MainFormEnabled();
                                    switch (rnd.Next(0, 5))
                                    {
                                        case 0: form.Notify.ThrowMassage(new DeviceEventArgs( $"Нельзя же такое просто так выбрасывать. Лучше подарть {device.Manufacturer} Потапову на НГ")); break;
                                        case 1: form.Notify.ThrowMassage(new DeviceEventArgs("Вы выкинули устройство в окно. Полиция уже едет за вами")); break;
                                        case 2: form.Notify.ThrowMassage(new DeviceEventArgs( $"Вы положили {device.Manufacturer} на балкон. Оно потеряно навсегда")); break;
                                        case 3: form.Notify.ThrowMassage(new DeviceEventArgs( $"Вы увезли {device.Manufacturer} на дачу. Туда ему и дорога, пусть паучки в нем живут")); break;
                                        case 4: form.Notify.ThrowMassage(new DeviceEventArgs("Ну чтож, может авито кому-нибудь понравится. Попробуем продать за двойную цену, товар ведь импортный")); break;
                                        default: form.Notify.ThrowMassage(new DeviceEventArgs("Вы просто выкинули устройство. Без шуток")); break;
                                    }
                                }
                            }
                            break;
                        case UserEventArgs.PropertyType.StartCopying:
                            {
                                try { form.cts?.Cancel(); }
                                finally
                                {
                                    form.Notify.ThrowMassage(new DeviceEventArgs("Копирование начинается"));
                                }
                            }
                            break;
                        case UserEventArgs.PropertyType.Printing:
                            {
                                try { form.cts?.Cancel(); }
                                finally
                                {
                                    form.cts = new System.Threading.CancellationTokenSource();
                                    form.Notify.ThrowMassage(new DeviceEventArgs("Печать в процессе в процессе"));
                                    for (int i = 0; i < 5; ++i)
                                    {
                                        await Task.Delay(500, form.cts.Token);
                                        form.Notify.ThrowMassage(new DeviceEventArgs(" . ", false));
                                    }
                                    form.Notify.ThrowMassage(new DeviceEventArgs("Печать завершена", false));
                                }
                            }
                            break;
                        case UserEventArgs.PropertyType.Scanning:
                            {
                                try { form.cts?.Cancel(); }
                                finally
                                {
                                    form.cts = new System.Threading.CancellationTokenSource();
                                    form.Notify.ThrowMassage(new DeviceEventArgs("Сканирование в процессе"));
                                    form.Notify.ThrowMassage(new DeviceEventArgs("Сканирование завершено", false));
                                }
                               
                            }
                            break;
                        case UserEventArgs.PropertyType.PaperCash:
                            {
                                form.Notify.ThrowMassage(new DeviceEventArgs("Вы нафармили 1 листок бумаги"));
                            }
                            break;
                    }
                    form.UpdateGrid();
                    form.UpdateMainInfo();
                }
                catch (OperationCanceledException)
                {
                    form.ErrNotify.ThrowMassage(new DeviceEventArgs( "Ну зачем же так резко выключать устройства из сети? Они могут заболеть"));
                }
            }

        }
        void FormStatus(DeviceEventArgs Msg)
        {
            if (!(Msg is FormEventArgs msg))
            {
                ErrNotify.ThrowMassage(new DeviceEventArgs( "Проблемы с полиморфимическим наследованием, звоните в поддержку"));
                return;
            }
            switch (msg.UserProp)
            {
                case FormEventArgs.UserProperties.BuyDevice:
                    {
                        switch (msg.UserStat)
                        {
                            case FormEventArgs.Status.ShoppingDNS:
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
                                                result = new Device(price, manuf, Notify, ErrNotify);
                                            }
                                            else if (Type == Printer.GetClass())
                                            {
                                                result = new Printer(price, manuf, Notify, ErrNotify);
                                            }
                                            else if (Type == Scanner.GetClass())
                                            {
                                                result = new Scanner(price, manuf, Notify, ErrNotify);
                                            }
                                            else if (Type == MFP.GetClass())
                                            {
                                                result = new MFP(price, manuf, Notify, ErrNotify);
                                            }
                                            else
                                                ErrNotify.ThrowMassage(new DeviceEventArgs( "Не нашли подходящий тип данных, что происходит?"));
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrNotify.ThrowMassage(new DeviceEventArgs( string.Format(ex.Message)));
                                        }
                                        AddDevice(result);
                                    }
                                    else
                                    {
                                        ErrNotify.ThrowMassage(new DeviceEventArgs( "Чтобы что-нибудь купить, надо что-нибудь выбрать"));
                                    }
                                }
                                break;
                            case FormEventArgs.Status.Shopping_OtBaldy:
                                {
                                    Device result = CreateDevice();
                                    if (result != null)
                                        AddDevice(result);
                                    dgvListOfDevices.ClearSelection();
                                }
                                break;
                            case FormEventArgs.Status.Inventary:
                                {
                                    FormStatusArg.UserProp = FormEventArgs.UserProperties.MainSwitch;
                                }
                                break;
                        }

                    }
                    break;
                case FormEventArgs.UserProperties.MainSwitch:
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
                                ErrNotify.ThrowMassage(new DeviceEventArgs( string.Format(ex.Message)));
                            }
                            MainImageIndex = rnd.Next(1, image.Length);
                        }
                        else
                        {
                            ErrNotify.ThrowMassage(new DeviceEventArgs( "Чтобы выбрать мейна, надо выбрать мейна (в таблице)"));
                        }
                        UpdateMainInfo();
                        MainFormEnabled();
                    }
                    break;
                case null:
                    {
                        switch (msg.UserStat)
                        {
                            case null:
                                break;
                            case FormEventArgs.Status.ShoppingDNS:
                                {
                                    dgvListOfDevices.Rows.Clear();
                                    gbListOfDevices.Text = "М.Видео";
                                    gbShop_OtBaldy.Enabled = false; gbShop_OtBaldy.BackColor = Color.LightGray;
                                    DisableButtons(bMainDeviceSwitch);
                                    DisableButtons(gbListOfDevices.Controls.OfType<Button>().ToArray());

                                    EnableButtons(gbBuyLeave.Controls.OfType<Button>().ToArray());
                                    try
                                    {
                                        Device[] dev = LoadDevicesFromFile();
                                        if (dev != null)
                                        {
                                            LetsShopping(dev);
                                        }
                                        else
                                        {
                                            ErrNotify.ThrowMassage(new DeviceEventArgs( "Что-то пошло не так при походе в файловый ДНС"));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrNotify.ThrowMassage(new DeviceEventArgs( string.Format(ex.Message)));
                                    }
                                }
                                break;
                            case FormEventArgs.Status.Shopping_OtBaldy:
                                {
                                    dgvListOfDevices.Rows.Clear();
                                    gbListOfDevices.Text = "Шоппинг на заказ";
                                    cbDNS.Checked = false;

                                    DisableButtons(bMainDeviceSwitch);
                                    gbShop_OtBaldy.Enabled = true; gbShop_OtBaldy.BackColor = SystemColors.Control;
                                    EnableButtons(gbListOfDevices.Controls.OfType<Button>().ToArray());
                                    EnableButtons(gbBuyLeave.Controls.OfType<Button>().ToArray());
                                }
                                break;
                            case FormEventArgs.Status.Inventary:
                                {
                                    dgvListOfDevices.Rows.Clear();
                                    gbListOfDevices.Text = "Инвентарь";
                                    cbDNS.Checked = false;

                                    gbShop_OtBaldy.Enabled = false; gbShop_OtBaldy.BackColor = Color.LightGray;
                                    DisableButtons(gbBuyLeave.Controls.OfType<Button>().ToArray());

                                    EnableButtons(gbListOfDevices.Controls.OfType<Button>().ToArray());
                                    UpdateGrid();
                                    MainFormEnabled();
                                }
                                break;
                            default:
                                ErrNotify.ThrowMassage(new DeviceEventArgs( "Забыли обработать событие в отделе formstatus, но помощь уже в пути"));
                                break;
                        }
                        dgvListOfDevices.ClearSelection();
                    }
                    break;
                default:
                    ErrNotify.ThrowMassage(new DeviceEventArgs( "Мы не знаем, что произошло в отделе userprop, но помощь уже в пути"));
                    break;
            }
        }
    }
}