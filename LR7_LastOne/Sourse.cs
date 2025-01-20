using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LR7_LastOne.Device_interface;

namespace LR7_LastOne
{
   class FileReader
    {
        public static Device[] GetDevices(DeviceHandler notify, string path = "data.txt")
        {

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File {path} doesn't exists");
            }
            string[] lines = File.ReadAllLines(path); // Чтение всех строк файла
            Device[] result_file = new Device[lines.Length];
            int price;
            string manufacturer;
            int j = 0;
            for (int i = 0; i < lines.Length; ++i, ++j)
            {
                string[] line = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (int.TryParse(line[1], out price) && line[2].Length != 0)
                {
                    manufacturer = line[2];
                    try
                    {
                        if (line[0] == Device.GetClass())
                        {
                            result_file[j] = new Device(price, manufacturer);
                        }
                        else if (line[0] == Printer.GetClass())
                        {
                            result_file[j] = new Printer(price, manufacturer);
                        }
                        else if (line[0] == Scanner.GetClass())
                        {
                            result_file[j] = new Scanner(price, manufacturer);
                        }
                        else if (line[0] == MFP.GetClass())
                        {
                            result_file[j] = new MFP(price, manufacturer);
                        }
                        else
                        {
                            notify($"Неверный формат строки: {lines[i]}");
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        result_file[j] = null;
                        Console.WriteLine();
                    }
                }
                if (result_file[j] == null) --j;

            }
            int elem_count = j;
            Device[] result = new Device[elem_count];
            for (int i = 0; i < elem_count; ++i)
            {
                result[i] = result_file[i];
            }
            return result;
        }

    }
    static class HandleDeviceEvents
    {
        public static void GUI(string msg)
        {
            Console.WriteLine(msg);
        }
        public static Device_interface.DeviceStringBuilder GetHandleAll()
        {
            // Device_interface.DeviceStringBuilder result = HandleAll;
            return HandleAll;
        }
        public static string HandleAll(Device_interface device, DeviceEventArgs e)//для 
        {
            switch (e.property)
            {
                case null:
                    break;
                case DeviceEventArgs.PropertyType.Assemble_changed:
                    {
                        return $"Assemble changed of {device.ToString()}";
                    }
                case DeviceEventArgs.PropertyType.Plug_changed:
                    {
                        return $"Plug changed of {device.ToString()}";
                    }
                case DeviceEventArgs.PropertyType.Copying:
                    {
                        return $"{device.ToString()} is copying";
                    }
                case DeviceEventArgs.PropertyType.Printing:
                    {
                        return $"{device.ToString()} is printing";
                    }
                case DeviceEventArgs.PropertyType.Scanning:
                    {
                        return $"{device.ToString()} is scanning";
                    }
            }

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


            switch (e.error)
            {
                case null:
                    break;
                case DeviceEventArgs.ErrorType.ErrPaperEnd:
                    {
                        return $"Printer lost its paper";
                    }
                case DeviceEventArgs.ErrorType.ErrPaperTooMuch:
                    {
                        return $"MFP don't accept so much paper. Its more then {((MFP)device).MAXPAPERCOUNT}";
                    }

            }
            return "Мы не знаем, что произошло, но помощь уже в пути";
        }
    }
    public class DeviceEventArgs//несогласованность по доступности
    {
        public DeviceEventArgs(PropertyType notify)
        {
            property = notify;
            error = null;
            criterror = null;
        }
        public DeviceEventArgs(ErrorType notify)
        {
            property = null;
            error = notify;
            criterror = null;
        }
        public DeviceEventArgs(CritErrorType notify)
        {
            property = null;
            error = null;
            criterror = notify;
        }

        public ErrorType? error { get; private set; }
        public PropertyType? property { get; private set; }
        public CritErrorType? criterror { get; private set; }
        public enum CritErrorType
        {
            ErrPrice_Less_Then_Min,
            ErrPrice_More_Then_Max,
            ErrManufName_TooLong
        }
        public enum ErrorType
        {
            ErrPaperTooMuch,
            ErrPaperEnd
        }
        public enum PropertyType
        {
            Plug_changed,
            Assemble_changed,
            Printing,
            Scanning,
            Copying
        }
    }
}
