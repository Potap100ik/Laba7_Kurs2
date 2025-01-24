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
        public static bool FileOpen(string filepath, long maxSizeBytes)
        {
            FileInfo fileInfo = new FileInfo(filepath);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException($"File {filepath} doesn't exists");
            }
            else if (fileInfo.Length >= maxSizeBytes)
            {
                throw new FileNotFoundException($"File {filepath} is too big for this program. File size: {fileInfo.Length}; Max: {maxSizeBytes}");
            }
            return true;
        }
        public static Device[] GetDevices(Device_interface.HandleDeviceEvents notify, Device_interface.HandleDeviceEvents errMsg = null, string path = "data.txt")
        {
            if (!FileOpen(path, 2 * 1024 * 1024))
                return null;
            if (errMsg == null)
                errMsg = notify;
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
                            result_file[j] = new Device(price, manufacturer, notify, errMsg);
                        }
                        else if (line[0] == Printer.GetClass())
                        {
                            result_file[j] = new Printer(price, manufacturer, notify, errMsg);
                        }
                        else if (line[0] == Scanner.GetClass())
                        {
                            result_file[j] = new Scanner(price, manufacturer, notify, errMsg);
                        }
                        else if (line[0] == MFP.GetClass())
                        {
                            result_file[j] = new MFP(price, manufacturer, notify, errMsg);
                        }
                        else
                        {
                            notify.ThrowMassage(new DeviceEventArgs($"FileError: Неверный формат строки: {lines[i]}"));
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        result_file[j] = null;
                        errMsg.ThrowMassage(new DeviceEventArgs("FileError: " + ex.Message));
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
        public string message { get; private set; }
        public bool? status { get; private set; }
        public DeviceEventArgs(string message = null, bool? status = null)
        {
            property = null;
            error = null;
            criterror = null;
            this.message = message;
            this.status = status;
        }
        public DeviceEventArgs(PropertyType notify, string message = null, bool? status = null) : this(message, status) { property = notify; }
        public DeviceEventArgs(ErrorType notify, string message = null, bool? status = null) : this(message, status) { error = notify; }
        public DeviceEventArgs(CritErrorType notify, string message = null, bool? status = null) : this(message, status) { criterror = notify; }
        public override string ToString()
        {
            return message != null ? message : "no messages";
        }
        public enum CritErrorType
        {
            ErrPrice_Less_Then_Min,
            ErrPrice_More_Then_Max,
            ErrManufName_TooLong,
            ErrManufName_Empty
        }
        public enum ErrorType
        {
            ErrPaperTooMuch,
            ErrPaperEnd,
            ErrUsingAsembled,
            ErrUsingUnPlug
        }
        public enum PropertyType
        {
            Plug_changed,
            Assembled,                          
            ThrowDev,
            DisassShop,
            DisassSam,
            Printing,
            Scanning,
            StartCopying,
            PaperCash
        }
    }
}
