using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LR7_LastOne.Device_interface;

namespace LR7_LastOne
{
    class FileReader
    {
        public static Device[] GetDevices(Device_interface.HandleDeviceEvents notify, string path = "data.txt")
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
                            result_file[j] = new Device(price, manufacturer, notify);
                        }
                        else if (line[0] == Printer.GetClass())
                        {
                            result_file[j] = new Printer(price, manufacturer, notify);
                        }
                        else if (line[0] == Scanner.GetClass())
                        {
                            result_file[j] = new Scanner(price, manufacturer, notify);
                        }
                        else if (line[0] == MFP.GetClass())
                        {
                            result_file[j] = new MFP(price, manufacturer, notify);
                        }
                        else
                        {
                            notify.ThrowMassage(($"Неверный формат строки: {lines[i]}"));
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        result_file[j] = null;
                        Console.WriteLine();
                        notify.ThrowMassage(ex.Message);
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
   
    public class DeviceEventArgs//несогласованность по доступности
    {
        public ErrorType? error { get; private set; }
        public PropertyType? property { get; private set; }
        public CritErrorType? criterror { get; private set; }
        public string message;
        public DeviceEventArgs(string message = null)
        {
            property = null;
            error = null;
            criterror = null;
        }
        public DeviceEventArgs(PropertyType notify, string message = null) : this(message) { property = notify; }
        public DeviceEventArgs(ErrorType notify, string message = null) : this(message) { error = notify; }
        public DeviceEventArgs(CritErrorType notify, string message = null) : this(message) { criterror = notify; }
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
