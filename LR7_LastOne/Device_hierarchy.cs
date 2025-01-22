using System;
using System.Drawing;
using System.IO;

namespace LR7_LastOne
{
    delegate void HandleWriter(string msg);
    delegate void DeviceHandler(Device_interface sender, DeviceEventArgs e);//??несогласованность по доступности
    class Program_
    {
        public static void Main_()
        {
            Device_interface.HandleDeviceEvents notify = new Device_interface.HandleDeviceEvents();
            try
            {
                Device[] array = FileReader.GetDevices(notify, "file.txt");
                foreach (Device d in array)
                {
                    if (d is MFP mfp)
                    {
                        mfp.Copy();
                    }
                    else if (d is Printer printer)
                    {
                        printer.Print();
                    }
                    else if (d is Scanner scanner)
                    {
                        scanner.Scan();
                    }
                    else if (d is Device device)
                    {
                        device.SwitchPlug();
                    }
                }

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }

    //назначение класса абстрактным чтобы не было соблазна создавать его экземпляров
    abstract class Device_interface : IDevice
    {
        //переменные
        protected HandleDeviceEvents Notify;
        protected int price;
        protected string manufacturer;
        protected bool isPluggedIn;
        protected bool isAssembled;
        public readonly int MINPRICE = 1000;
        public readonly int MAXPRICE = 10000;
        public readonly byte MAXMANUFACTURERNAMELENGTH = 15;
        //свойства
        virtual public int Price
        {
            get => price;
            private set
            {
                price = value;//criterror не позволит создаться объекту. по идее. нужно передать в обработчик событий значение не принятой цены
                if (value < MINPRICE)
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrPrice_Less_Then_Min));
                else if (value > MAXPRICE)
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrPrice_More_Then_Max));

            }
        }
        virtual public string Manufacturer
        {
            get => manufacturer;
            private set
            {
                manufacturer = value;
                if (value.Length > MAXMANUFACTURERNAMELENGTH)
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrManufName_TooLong));
            }
        }
        virtual public bool IsPluggedIn { get => isPluggedIn;  set => isPluggedIn = value; }
        virtual public bool IsAssembled { get => isAssembled;  set => isAssembled = value; }
        //конструкторы
        protected Device_interface(int price, string manufacturer)
        {
            Notify = new HandleDeviceEvents();
            Manufacturer = manufacturer;
            isPluggedIn = false;
            isAssembled = false;
            Price = price;
        }
        protected Device_interface(int price, string manufacturer, HandleDeviceEvents notify)
        {
            Notify = notify;
            Manufacturer = manufacturer;
            isPluggedIn = false;
            isAssembled = false;
            Price = price;
        }
        protected Device_interface(int price, string manufacturer, DeviceHandler notify, HandleWriter msg) : this(price, manufacturer)
        {
            Notify = new HandleDeviceEvents(notify, msg);
        }
        //методы работают исключительно через события или нет?
        virtual public void SwitchPlug()
        {
            IsPluggedIn = !IsPluggedIn;
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Plug_changed));
        }
        virtual public void Assemble()
        {
            IsAssembled = true;
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Assembled));
        }
        virtual public void DisassambleSam()
        {
            IsAssembled = false;
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.DisassSam));
        }
        virtual public void Disassamble_shop()
        {
            IsAssembled = false;
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.DisassShop));
        }
        virtual public void ThrowDevice()
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.ThrowDev));
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
            public void ThrowMassage(string msg)
            {
                if (Msg == null)
                    throw new ArgumentException("Error: Оператор вывода ошибок отсутствует");
                Msg?.Invoke(msg);
            }
            public void Hello() { }
            public HandleDeviceEvents()
            {
                RegisterHandler(HandleMassages);
                RegisterHandler(HandleProperties);
                RegisterHandler(HandleCritErrors);
                RegisterHandler(HandleCritErrors);
                RegisterWriter(Console);
            }
            /*            public HandleDeviceEvents(HandleDeviceEvents notify)
                        {
                            UnRegisterHandler();
                            UnRegisterErrorWriter();
                            RegisterHandler(notify.);
                            RegisterErrorWriter(msg);
                        }*/
            public HandleDeviceEvents(DeviceHandler notify, HandleWriter msg)
            {
                UnRegisterHandler();
                UnRegisterWriter();
                RegisterHandler(notify);
                RegisterWriter(msg);
            }
            public static void Console(string msg)
            {
                System.Console.WriteLine(msg);
            }
            protected void HandleMassages(Device_interface device, DeviceEventArgs e)
            {
                if (e.message != null)
                    ThrowMassage(e.message);
            }
            protected void HandleProperties(Device_interface device, DeviceEventArgs e)
            {

                switch (e.property)
                {
                    case null:
                        break;
                    case DeviceEventArgs.PropertyType.Assembled:
                        {
                            ThrowMassage($"Assemble changed of {device.ToString()}");
                        }
                        break;
                    case DeviceEventArgs.PropertyType.Plug_changed:
                        {
                            ThrowMassage("Plug changed of {device.ToString()}");
                        }
                        break;
                    case DeviceEventArgs.PropertyType.Copying:
                        {
                            ThrowMassage("{device.ToString()} is copying");
                        }
                        break;
                    case DeviceEventArgs.PropertyType.Printing:
                        {
                            ThrowMassage("{device.ToString()} is printing");
                        }
                        break;
                    case DeviceEventArgs.PropertyType.Scanning:
                        {
                            ThrowMassage("{device.ToString()} is scanning");
                        }
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
                            throw new ArgumentException(string.Format("{0} {1} can't have so little price", device.ToString(), device.Manufacturer));
                        }
                    case DeviceEventArgs.CritErrorType.ErrPrice_More_Then_Max:
                        {
                            throw new ArgumentException(string.Format("{0} {1} can't have so high price", device.ToString(), device.Manufacturer));
                        }
                    case DeviceEventArgs.CritErrorType.ErrManufName_TooLong:
                        {
                            throw new ArgumentException(string.Format("{0} can't have so long  manufacturer's name: {1}", device.ToString(), device.Manufacturer));
                        }
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
                            ThrowMassage(string.Format("{0} lost its paper", device.ToString()));
                        }
                        break;
                    case DeviceEventArgs.ErrorType.ErrPaperTooMuch:
                        {
                            ThrowMassage(string.Format("{0} don't accept so much paper. Its more then {1}", device.ToString(), ((MFP)device).MAXPAPERCOUNT));
                        }
                        break;
                }
                ThrowMassage("Мы не знаем, что произошло, но помощь уже в пути");
            }
        }
    }
    class Device : Device_interface
    {
        private static string ClassName = "Device";
        public Device(int price, string manufacturer) : base(price, manufacturer) { }
        public Device(int price, string manufacturer, HandleDeviceEvents notify) : base(price, manufacturer, notify) { }
        public Device(int price, string manufacturer, DeviceHandler notify, HandleWriter msg) : base(price, manufacturer, notify, msg) { }

        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
    class Printer : Device, IPrinter
    {
        public readonly int MAXPAPERCOUNT = 100;
        private static string ClassName = "Printer";

        public Printer(int price, string manufacturer, int papercount = 10) : base(price, manufacturer)
        {
            Papercount = papercount;
        }
        public Printer(int price, string manufacturer, HandleDeviceEvents notify, int papercount = 10) : base(price, manufacturer, notify)
        {
            Papercount = papercount;
        }
        public Printer(int price, string manufacturer, DeviceHandler notify, HandleWriter msg, int papercount = 10) : base(price, manufacturer, notify, msg)
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
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperEnd));
                else if (value > MAXPAPERCOUNT)
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperTooMuch));
            }
        }
        public void Print(int paper_used = 1)
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Printing));
            papercount -= paper_used;
        }
        public void PaperAdd(int paper = 1)
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.PaperCash));
            papercount += paper;
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
    class Scanner : Device, IScanner
    {
        private static string ClassName = "Scanner";
        public Scanner(int price, string manufacturer) : base(price, manufacturer) { }
        public Scanner(int price, string manufacturer, HandleDeviceEvents notify) : base(price, manufacturer, notify) { }
        public Scanner(int price, string manufacturer, DeviceHandler notify, HandleWriter msg) : base(price, manufacturer, notify, msg) { }
        virtual public void Scan()
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Scanning));
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
    class MFP : Device, I_MFP
    {
        private static string ClassName = "MFP";
        public readonly int MAXPAPERCOUNT = 100;
        public MFP(int price, string manufacturer, int papercount = 10) : base(price, manufacturer)
        {
            Papercount = papercount;
        }
        public MFP(int price, string manufacturer, HandleDeviceEvents notify, int papercount = 10) : base(price, manufacturer, notify)
        {
            Papercount = papercount;
        }
        public MFP(int price, string manufacturer, DeviceHandler notify, HandleWriter msg, int papercount = 10) : base(price, manufacturer, notify, msg)
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
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperEnd));
                else if (value > MAXPAPERCOUNT)
                    Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperTooMuch));
                papercount = value;
            }
        }

        public void Copy()
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Copying));
        }
        public void Scan()
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Scanning));
        }
        //не понимаю где реализовать логику изменения количества - в событии или в основном коде
        public void Print(int paper_used = 1)
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Printing));
            Papercount -= paper_used;
        }
        public void PaperAdd(int paper = 1)
        {
            Notify.RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.PaperCash));
            Papercount += paper;
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
}


