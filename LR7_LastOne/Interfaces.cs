using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR7_LastOne
{
    interface IDevice
    {
        int Price { get;  }
        string Manufacturer { get;  }
        bool IsPluggedIn { get;  }
        bool IsAssembled { get;  }
        void SwitchPlug();
        void Assemble();
        void DisassambleSam();
        void Disassamble_shop();


    } 
    interface IPrinter: IDevice
    {
        int Papercount { get; }
       void Print(int paper_used);
        void PaperAdd(int paper);
    }
    interface IScanner: IDevice
    {
        void Scan();
    }
    interface I_MFP: IScanner, IPrinter
    {
        void Copy();
    }
}