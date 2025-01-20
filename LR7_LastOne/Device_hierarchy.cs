using LR7_LastOne;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static LR7_LastOne.Device_interface;

namespace LR7_LastOne
{
    class Program_
    {
        public static void Main_()
        {

            try
            {
                Device[] array =  FileReader.GetDevices(HandleDeviceEvents.GUI,"file.txt");
                foreach(Device d in array)
                {
                    if (d is MFP mfp)
                    {
                        mfp.Copy();
                    }
                    else if (d is Printer printer)
                    {
                        printer.Print();
                    }
                    else if(d is Scanner scanner)
                    {
                        scanner.Scan();
                    }
                    else if(d is Device device)
                    {
                        device.SwitchPlug();
                    }
                }

            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }

    //назначение класса абстрактным чтобы не было соблазна создавать его экземпляров
    abstract class Device_interface : IDevice
    {
        public delegate string DeviceStringBuilder(Device_interface sender, DeviceEventArgs e);//??несогласованность по доступности
        public delegate void DeviceHandler(string str);
        public DeviceStringBuilder StringBuilder;
        public event DeviceHandler Notify;
        protected int price;
        protected string manufacturer;
        protected bool isPluggedIn;
        protected bool isAssembled;
        public readonly int MINPRICE = 1000;
        public readonly int MAXPRICE = 10000;
        public readonly byte MAXMANUFACTURERNAMELENGTH = 15;

        virtual public int Price
        {
            get => price;
            private set
            {
                if (value < MINPRICE)
                    RaiseStringEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrPrice_Less_Then_Min));
                else if (value > MAXPRICE)
                    RaiseStringEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrPrice_More_Then_Max));
                else
                    price = value;

            }

        }
        virtual public string Manufacturer { get => manufacturer;
            private set
            {
                if (value.Length > MAXMANUFACTURERNAMELENGTH)
                    RaiseStringEvent(this, new DeviceEventArgs(DeviceEventArgs.CritErrorType.ErrManufName_TooLong));
                manufacturer = value;
            }
        }
        virtual public bool IsPluggedIn { get => isPluggedIn; private set => isPluggedIn = value; }
        virtual public bool IsAssembled { get => isAssembled; private set => isAssembled = value; }
        virtual public void SwitchPlug()
        {
            IsPluggedIn = !IsPluggedIn;
            RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Plug_changed));
        }
        virtual public void Assemble()
        {
            IsAssembled = true;
            RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Assemble_changed));
        }

        public void RegisterStringer(DeviceStringBuilder stringbuilder) { StringBuilder += stringbuilder; }
        public void UnRegisterStringer(DeviceStringBuilder stringbuilder) { StringBuilder -= stringbuilder; }
        public void UnRegisterStringer() => StringBuilder = null;
        public void RegisterHandler(DeviceHandler notify) { Notify += notify; }
        public void UnRegisterHandler(DeviceHandler notify) { Notify -= notify; }
        public void UnRegisterHandler() => Notify = null; 
        protected void RaiseLogEvent(Device_interface sender, DeviceEventArgs e)
        {
            if (Notify == null)
                throw new ArgumentException("Error: Oбработчик событий отсутствует");
            Notify?.Invoke(RaiseStringEvent(sender, e));
        }
        protected string RaiseStringEvent(Device_interface sender, DeviceEventArgs e) 
        {
            if (StringBuilder == null)
                throw new ArgumentException("Error: Oбработчик событий отсутствует");
            return StringBuilder?.Invoke(sender, e);
        }

        protected Device_interface(int price, string manufacturer)
        {
            RegisterStringer(HandleDeviceEvents.GetHandleAll());
            RegisterHandler(HandleDeviceEvents.GUI);
            Manufacturer = manufacturer;
            isPluggedIn = false;
            isAssembled = false;
            Price = price;
        }
        protected Device_interface(int price, string manufacturer, DeviceStringBuilder stringbuilder, DeviceHandler notify) : this(price, manufacturer) 
        {
            UnRegisterStringer();
            RegisterStringer(stringbuilder);
            UnRegisterHandler();
            RegisterHandler(notify);
        }
        virtual public bool Disassamble_by_yourself() 
        { 

            return true; 
        }
        virtual public void Disassamble_shop()
        {

        }
    }
    class Device : Device_interface
    {
        private static string ClassName = "Device";
        override public bool IsPluggedIn { get => isPluggedIn; }
        override public bool IsAssembled { get => isAssembled; }
      
        public Device(int price, string manufacturer) : base(price, manufacturer) { }
        public Device(int price, string manufacturer, DeviceStringBuilder stringbuilder, DeviceHandler notify) : base(price, manufacturer, stringbuilder, notify) { }
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
        public Printer(int price, string manufacturer,  DeviceStringBuilder stringbuilder, DeviceHandler notify, int papercount = 10) : base(price, manufacturer, stringbuilder, notify)
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
                    RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperEnd));
                else if (value > MAXPAPERCOUNT)
                    RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperTooMuch));
            }
        }
        virtual public void Print(int paper_used = 1)
        {
            RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Printing));
            papercount-=paper_used;
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
    class Scanner: Device, IScanner
    {
        private static string ClassName = "Scanner";
        public Scanner(int price, string manufacturer): base(price, manufacturer) { }
        public Scanner(int price, string manufacturer, DeviceStringBuilder stringbuilder, DeviceHandler notify) : base(price, manufacturer, stringbuilder, notify) { }
        virtual public void Scan()
        {
            RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Scanning));
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
    class MFP: Device, I_MFP
    {
        private static string ClassName = "MFP";
        public readonly int MAXPAPERCOUNT = 100;
        public MFP(int price, string manufacturer, int papercount = 10):base(price, manufacturer)
        {
            Papercount = papercount;
        }
        public MFP(int price, string manufacturer, DeviceStringBuilder stringbuilder, DeviceHandler notify, int papercount = 10) : base(price, manufacturer, stringbuilder, notify)
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
                    RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperEnd));
                else if(value > MAXPAPERCOUNT)
                    RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.ErrorType.ErrPaperTooMuch));
            }
        }

        public void Copy()
        {
            RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Copying));
            Scan();
            Print();
        }
         public void Scan()
        {
            RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Scanning));
        }
        public void Print(int paper_used = 1)
        {
            RaiseLogEvent(this, new DeviceEventArgs(DeviceEventArgs.PropertyType.Printing));
            papercount -= paper_used;
        }
        public override string ToString() { return ClassName; }
        public static string GetClass() { return ClassName; }
    }
}


