using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    public enum FilterEnum : uint //AKA TSS_Find
    {
        All = 0xffffffff, //TSS_FIND_ALL
        BootLoader = 0x00000001, //TSS_FIND_BTL
        USB = 0x00000002, //TSS_FIND_USB
        WirelessDongle = 0x00000004, //TSS_FIND_DNG
        Wireless = 0x00000008, //TSS_FIND_WL
        Embedded = 0x00000010, //TSS_FIND_EM
        DataLogging = 0x00000020, //TSS_FIND_DL
        Bluetooth = 0x00000040, //TSS_FIND_BT
        AnyUnknown = 0x80000000,//TSS_FIND_UNKNOWN
        AnyKnown = 0x7fffffff//TSS_FIND_ALL_KNOWN
    }
}
