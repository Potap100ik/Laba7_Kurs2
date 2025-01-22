using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LR7_LastOne
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            this.Width = 1150;
            this.Height = 452;
        }
        //батоны все здесь
        private void bEnterDNS_Click(object sender, EventArgs e)
        {
            FormStatusArg.UserStat = HandleDeviceEventsWF.FormHandleArgs.Status.ShoppingDNS;
        }

        private void bBuy_OtBaldy_Click(object sender, EventArgs e)
        {
            FormStatusArg.UserStat = HandleDeviceEventsWF.FormHandleArgs.Status.Shopping_OtBaldy;
        }

        private void bShowInventory_Click(object sender, EventArgs e)
        {
            FormStatusArg.UserStat = HandleDeviceEventsWF.FormHandleArgs.Status.Inventary;
        }

        private void bSwitchMainDevice_Click(object sender, EventArgs e)
        {
            FormStatusArg.UserProp = HandleDeviceEventsWF.FormHandleArgs.UserProperties.MainSwitch;
        }

        private void bBuyForFree_Click(object sender, EventArgs e)
        {
            FormStatusArg.UserProp = HandleDeviceEventsWF.FormHandleArgs.UserProperties.BuyDevice;
        }

        private void bLeaveDNS_Click(object sender, EventArgs e)
        {
            FormStatusArg.UserStat = HandleDeviceEventsWF.FormHandleArgs.Status.Inventary;
        }

        private void bPlugSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                {
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
                else
                    MainDev.SwitchPlug();

            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }

        private void bAssem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                {
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
                else
                    MainDev.Assemble();

            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }

        private void bDisassSam_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                {
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
                else
                    MainDev.DisassambleSam();

            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }

        private void bDisassShop_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                {
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
                else
                    MainDev.Disassamble_shop();

            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }

        private void bScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                {
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
                else
                if (MainDev is Scanner scanner)
                    scanner.Scan();//допускаем ведь есть try и логика кнопок
                else
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrOptionalLost));
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                {
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
                else
                if (MainDev is Printer printer)
                    printer.Print();//допускаем ведь есть try и логика кнопок
                else
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrOptionalLost));

            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }

        private void bThrow_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                {
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
               else
                    MainDev.ThrowDevice();
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                else
                {
                    if (MainDev is MFP mfp)
                        mfp.Copy();//допускаем ведь есть try и логика кнопок
                    else
                        ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrOptionalLost));
                }
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }

        private void bPaperCash_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainDev == null)
                {
                    ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrMainDevEmpty));
                    return;
                }
                else
                {
                    if (MainDev is Printer printer)
                        printer.PaperAdd();//допускаем ведь есть try и логика кнопок
                    else
                        ErrNotify.RaiseLogEvent(MainDev, new HandleDeviceEventsWF.UserEventArgs(HandleDeviceEventsWF.UserEventArgs.UserErrors.ErrOptionalLost));
                }
            }
            catch (Exception ex)
            {
                ErrNotify.ThrowMassage(ex.Message);
            }
        }
    }
}
