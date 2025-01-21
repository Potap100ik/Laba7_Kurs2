using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR7_LastOne
{
    public partial class MainForm : Form
    {
         class UserEventArgs
        {

            public enum Errors
            {
                ErrMainDevEmpty,
                ErrOptionalLost,
            }
           public enum Status
            {
                ShoppingDNS,
                Shopping_OtBaldy,
                Inventary, 
                StartPosition
            }
           public enum Move
            {
                ThrowDev,
                DisassShop,
                DisassSam,
                Assem,
                PlugSwitch,
                PaperCash,
                Print,
                Scan,
                Copy
            }
        }
        private void UsErrorHandle(UserEventArgs.Errors err)
        {
            switch (err)
            {

                case UserEventArgs.Errors.ErrMainDevEmpty:
                    {
                        ErrNotify.ThrowMassage("Главное устройство не выбрано, незачем так тыкать");
                    }
                    break;
                case UserEventArgs.Errors.ErrOptionalLost:
                    {
                        ErrNotify.ThrowMassage("Устройство не так многофункционально как кажется. Эту операцию я не могу вам позволить");
                    }
                    break;
            }
        }
        private void UsMoveCompile(UserEventArgs.Move move)
        {
            if (MainDev == null)
                UsErrorHandle(UserEventArgs.Errors.ErrMainDevEmpty);
            switch (move)
            {
                case UserEventArgs.Move.PlugSwitch:
                    {
                        
                    }
                    break;
                case UserEventArgs.Move.DisassSam:
                    {

                    }
                    break;
                case UserEventArgs.Move.DisassShop:
                    {

                    }
                    break;
                case UserEventArgs.Move.ThrowDev:
                    {

                    }
                    break;
                case UserEventArgs.Move.PaperCash:
                    {

                    }
                    break;
                case UserEventArgs.Move.Print:
                    {

                    }
                    break;
                case UserEventArgs.Move.Scan:
                    {

                    }
                    break;
                case UserEventArgs.Move.Copy:
                    {

                    }
                    break;


            }
        }

        private void UsStatusSwitch(UserEventArgs.Status stat)
        {
            Status = stat;
            switch (stat)
            {
                case UserEventArgs.Status.ShoppingDNS:
                    {
                        dgvListOfDevices.Rows.Clear();
                        gbShop_OtBaldy.Enabled = false;
                        bSwitchMainDevice.Enabled = false;

                        gbBuyLeave.Enabled = true;
                        gbListOfDevices.Enabled = true;
                        try
                        {
                            Device[] dev = GetDevices();
                            if (dev != null)
                            {
                                LetsShopping(dev);
                            }
                            else
                            {
                                ErrNotify.ThrowMassage("Что-то пошло не так при считывании данных с файла");
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrNotify.ThrowMassage(ex.Message);
                        }

                    }
                    break;
                case UserEventArgs.Status.Shopping_OtBaldy:
                    {
                        dgvListOfDevices.Rows.Clear();
                        gbListOfDevices.Enabled = false;
                        bSwitchMainDevice.Enabled = false;

                        gbShop_OtBaldy.Enabled = true;
                        gbBuyLeave.Enabled = true;

                    }
                    break;
                case UserEventArgs.Status.Inventary:
                    {
                        dgvListOfDevices.Rows.Clear();
                        gbListOfDevices.Enabled = true;
                        bSwitchMainDevice.Enabled = true;
                        gbShop_OtBaldy.Enabled = false;
                        gbBuyLeave.Enabled = false;
                        foreach (var dev in DevInventary)
                        {
                            dgvListOfDevices.Rows.Add(dev.ToString(), dev.Manufacturer, dev.Price, GetPlugStat(dev), GetAssembleStat(dev)); ;
                        }
                    }
                    break;
                case UserEventArgs.Status.StartPosition:
                    {
                        dgvListOfDevices.Rows.Clear();
                        gbBuyLeave.Enabled = false;
                        gbListOfDevices.Enabled = false;
                        gbShop_OtBaldy.Enabled = false;
                        bSwitchMainDevice.Enabled = false;
                    }
                    break;
            }
        }

    }
}
