using System;
using System.Drawing;
using System.IO;

namespace LR7_LastOne
{
    delegate void HandleWriter(DeviceEventArgs e);
    delegate void DeviceHandler(Device_interface sender, DeviceEventArgs e);//??несогласованность по доступности
    //назначение класса абстрактным чтобы не было соблазна создавать его экземпляров
    abstract class Device_interface : IDevice
    {
        //переменные
        protected HandleDeviceEvents Notify;
        protected HandleDeviceEvents ErrMsg;
        protected int price;
        protected string manufacturer;
        protected bool isPluggedIn;
        protected bool isAssembled;
        static public readonly int MINPRICE = 1000;
        static public readonly int MAXPRICE = 10000;
        static public readonly byte MAXMANUFACTURERNAMELENGTH = 15;
        //свойства
        virtual public int Price
        {
            get => price;
            private set
            {
                price = value;//criterror не позволит создаться объекту. по идее. нужно передать в обработчик событий значение не принятой цены
                if (value < MINPRICE)
                    ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrPrice_Less_Then_Min));
                else if (value > MAXPRICE)
                    ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrPrice_More_Then_Max));

            }
        }
        virtual public string Manufacturer
        {
            get => manufacturer;
            private set
            {
                if(value.Length == 0)
                    ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrManufName_Empty));
                manufacturer = value;
                if (value.Length > MAXMANUFACTURERNAMELENGTH)
                    ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrManufName_TooLong));
            }
        }
        virtual public bool IsPluggedIn { get => isPluggedIn;  set => isPluggedIn = value; }
        virtual public bool IsAssembled { get => isAssembled;  set => isAssembled = value; }
        //конструкторы
        protected Device_interface(int price, string manufacturer)
        {
            ErrMsg = Notify = new HandleDeviceEvents();
            Manufacturer = manufacturer;
            isPluggedIn = isAssembled = false;
            Price = price;
        }
        protected Device_interface(int price, string manufacturer, HandleDeviceEvents notify, HandleDeviceEvents errMsg = null)
        {
            if (errMsg == null)
                ErrMsg = notify;
            else
                ErrMsg = errMsg;
            Notify = notify;
            Manufacturer = manufacturer;
            isPluggedIn = isAssembled = false;
            Price = price;
        }

        //методы работают исключительно через события или нет?
        virtual public void SwitchPlug()
        {
            IsPluggedIn = !IsPluggedIn;
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Plug_changed));
        }
        virtual public void Assemble()
        {
            if (IsAssembled)
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Assembled,null, IsAssembled));
            else
            {
                IsAssembled = true;
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Assembled));
            }

        }
        virtual public void DisassambleSam()
        {
            bool prev = IsAssembled;
            IsAssembled = false;
            if (!prev)
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.DisassSam, null, prev));
            else
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.DisassSam, null, prev));

        }
        virtual public void Disassamble_shop()
        {
            bool prev = IsAssembled;
            IsAssembled = false;
            if (!prev)
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.DisassShop, null, prev));
            else
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.DisassShop, null, prev));

        }
        virtual public void ThrowDevice()
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.ThrowDev));
        }
        protected bool UsageCheck()
        {
            if (IsAssembled)
                ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrUsingAsembled));
            else if (!IsPluggedIn)
                ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrUsingUnPlug));
            return !IsAssembled && IsPluggedIn;
        }
        //класс обработчик событий
        public class HandleDeviceEvents
        {
            private event DeviceHandler Notify;
            private event HandleWriter Msg;
            protected void RegisterWriter(HandleWriter msg) { Msg += msg; }
            protected void UnRegisterWriter(HandleWriter msg) { Msg -= msg; }
            protected void UnRegisterWriter() => Msg = null;
            protected void RegisterHandler(DeviceHandler notify) { Notify += notify; }
            protected void UnRegisterHandler(DeviceHandler notify) { Notify -= notify; }
            protected void UnRegisterHandler() => Notify = null;
            public void RaiseLogEvent(Device_interface sender, DeviceEventArgs e)
            {
                if (Notify == null)
                    throw new ArgumentException("Error: Oбработчик событий отсутствует");
                Notify?.Invoke(sender, e);
            }
            public void ThrowMassage(DeviceEventArgs msg)
            {
                if (Msg == null)
                    throw new ArgumentException("Error: Оператор вывода ошибок отсутствует");
                Msg?.Invoke(msg);
            }
            public HandleDeviceEvents()
            {
                RegisterHandler(HandleMassages);
                RegisterHandler(HandleProperties);
                RegisterHandler(HandleCritErrors);
                RegisterHandler(HandleErrors);
                RegisterWriter(Console);
            }
            public static void Console(DeviceEventArgs msg)
            {
                System.Console.WriteLine(msg);
            }
            protected void HandleMassages(Device_interface device, DeviceEventArgs e)
            {
                if (e.message != null)
                    ThrowMassage(e);
            }
            protected void HandleProperties(Device_interface device, DeviceEventArgs e)
            {
                switch (e.property)
                {
                    case null:
                        break;
                    case DeviceEventArgs.PropertyType.Assembled:
                        {
                            ThrowMassage(new DeviceEventArgs( $"Assemble changed of {device.ToString()}"));
                        }
                        break;
                    case DeviceEventArgs.PropertyType.Plug_changed:
                        {
                            ThrowMassage(new DeviceEventArgs( "Plug changed of {device.ToString()}"));
                        }
                        break;
                    case DeviceEventArgs.PropertyType.StartCopying:
                        {
                            ThrowMassage(new DeviceEventArgs( "{device.ToString()} is copying"));
                        }
                        break;
                    case DeviceEventArgs.PropertyType.Printing:
                        {
                            ThrowMassage(new DeviceEventArgs( "{device.ToString()} is printing"));
                        }
                        break;
                    case DeviceEventArgs.PropertyType.Scanning:
                        {
                            ThrowMassage(new DeviceEventArgs( "{device.ToString()} is scanning"));
                        }
                        break;
                    default:
                        ThrowMassage(new DeviceEventArgs( "Мы не знаем, что произошло в отделе исполнений, но помощь уже в пути"));
                        break;
                }
            }
            protected void HandleCritErrors(Device_interface device, DeviceEventArgs e)
            {
                switch (e.criterror)
                {
                    case null:
                        break;
                    case DeviceEventArgs.CritErrorType.ErrPrice_Less_Then_Min:
                        {
                            throw new ArgumentException(string.Format("{0} {1} can't have so little price ({2}); Minimum: {3}", device.ToString(), device.Manufacturer, device.Price, Device_interface.MINPRICE));
                        }
                    case DeviceEventArgs.CritErrorType.ErrPrice_More_Then_Max:
                        {
                            throw new ArgumentException(string.Format("{0} {1} can't have so high price ({2}); Maximum: {3}", device.ToString(), device.Manufacturer, device.Price, Device_interface.MAXPRICE));
                        }
                    case DeviceEventArgs.CritErrorType.ErrManufName_TooLong:
                        {
                            throw new ArgumentException(string.Format("{0} can't have so long  manufacturer's name: {1}; Max length: {2}", device.ToString(), device.Manufacturer, Device_interface.MAXMANUFACTURERNAMELENGTH));
                        }
                    case DeviceEventArgs.CritErrorType.ErrManufName_Empty:
                        {
                            throw new ArgumentException(string.Format("Can't accept empty manufacturers name for {0}", device.ToString()));
                        }
                    default:
                        ThrowMassage(new DeviceEventArgs( "Мы не знаем, что произошло в отделе критических ошибок, но помощь уже в пути"));
                        break;
                }
            }
            protected void HandleErrors(Device_interface device, DeviceEventArgs e)
            {
                switch (e.error)
                {
                    case null:
                        break;
                    case DeviceEventArgs.ErrorType.ErrPaperEnd:
                        {
                            ThrowMassage(new DeviceEventArgs(string.Format("{0} потерял бумагу и печатать не будет", device.ToString())));
                        }
                        break;
                    case DeviceEventArgs.ErrorType.ErrPaperTooMuch:
                        {
                            ThrowMassage(new DeviceEventArgs(string.Format("{0} Больше не может скушать в себя так много бумаги", device.ToString())));
                        }
                        break;
                    case DeviceEventArgs.ErrorType.ErrUsingAsembled:
                        {
                            ThrowMassage(new DeviceEventArgs(string.Format("Пользоваться разобранным устройством можно, но лучше сначала собрать")));
                        }
                        break;
                    case DeviceEventArgs.ErrorType.ErrUsingUnPlug:
                        {
                            ThrowMassage(new DeviceEventArgs(string.Format("Это не Хогвартс, без электричества не получится, включите устройство в сеть")));
                        }
                        break;
                    default:
                        ThrowMassage(new DeviceEventArgs("Мы не знаем, что произошло в отделе ошибок, но помощь уже в пути"));
                        break;
                }
            }
        }
    }
    class Device : Device_interface
    {
        private static string ClassName = "Device";
        public Device(int price, string manufacturer) : base(price, manufacturer) { }
        public Device(int price, string manufacturer, HandleDeviceEvents notify, HandleDeviceEvents errMsg = null) : base(price, manufacturer, notify, errMsg) { }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
    class Printer : Device, IPrinter
    {
        public readonly int MAXPAPERCOUNT = 10;
        private static string ClassName = "Printer";
        public Printer(int price, string manufacturer, int papercount = 10) : base(price, manufacturer)
        {
            Papercount = papercount;
        }
        public Printer(int price, string manufacturer, HandleDeviceEvents notify, HandleDeviceEvents errMsg = null, int papercount = 10) : base(price, manufacturer, notify, errMsg)
        {
            Papercount = papercount;
        }
        protected int papercount;
        public int Papercount
        {
            get => papercount;
            private set
            {
                if (value < 0)
                    ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperEnd));
                else if (value > MAXPAPERCOUNT)
                    ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperTooMuch));
                else
                    papercount = value;
            }
        }
        public void Print(int paper_used = 1)
        {
            if (UsageCheck())
            {
                int value = papercount - paper_used;
                Papercount = value;
                if (Papercount == value)
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Printing));
            }

        }
        public void PaperAdd(int paperadd = 1)
        {

            int value = papercount + paperadd;
            Papercount = value;
            if (Papercount == value)
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.PaperCash));
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
    class Scanner : Device, IScanner
    {
        private static string ClassName = "Scanner";
        public Scanner(int price, string manufacturer) : base(price, manufacturer) { }
        public Scanner(int price, string manufacturer, HandleDeviceEvents notify, HandleDeviceEvents errMsg = null) : base(price, manufacturer, notify, errMsg) { }
        virtual public void Scan()
        {
            if (UsageCheck())
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Scanning));
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
    class MFP : Device, I_MFP
    {
        private static string ClassName = "MFP";
        public readonly int MAXPAPERCOUNT = 11;
        public MFP(int price, string manufacturer, int papercount = 10) : base(price, manufacturer)
        {
            Papercount = papercount;
        }
        public MFP(int price, string manufacturer, HandleDeviceEvents notify, HandleDeviceEvents errMsg = null, int papercount = 10) : base(price, manufacturer, notify, errMsg)
        {
            Papercount = papercount;
        }
        int papercount;
        public int Papercount
        {
            get => papercount;
            private set
            {
                if (value < 0)
                    ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperEnd));
                else if (value > MAXPAPERCOUNT)
                    ErrMsg.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperTooMuch));
                else
                    papercount = value;
            }
        }
        public void Copy(int paper_used = 1)
        {
            if (UsageCheck())
            {
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.StartCopying));
                Scan();
                Print(paper_used);
            }
        }
        public void Scan()
        {
            if (UsageCheck())
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Scanning));
        }
        //не понимаю где реализовать логику изменения количества - в событии или в основном коде
        public void Print(int paper_used = 1)
        {
            if (UsageCheck())
            {
                int value = papercount - paper_used;
                Papercount = value;
                if (Papercount == value)
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Printing));
            }
        }
        public void PaperAdd(int paperadd = 1)
        {
            int value = papercount + paperadd;
            Papercount = value;
            if (Papercount == value)
                Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.PaperCash));
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
}


