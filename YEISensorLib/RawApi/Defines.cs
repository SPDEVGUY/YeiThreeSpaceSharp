using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    public class Defines ///AKA TSS_Device_Id_Mask
    {
        public const uint INF_DURATION = 0xffffffff;
        public const uint SENSOR_ID = 0x4000000;

        public const uint NO_DEVICE_ID = 0x00800000;
        public const uint BOOTLOADER_ID = 0x01000000;
        public const uint DONGLE_ID = 0x02000000;
        public const uint EMBEDDED_ID = 0x08000000;
        public const uint WIRELESS_ID = 0x10000000;
        public const uint WIRELESS_W_ID = 0x20000000;
        public const uint DATALOGGER_ID = 0x40000000;
        public const uint BLUETOOTH_ID = 0x80000000;
        public const uint NO_DONGLE_ID = 0xfd000000;
        public const uint ALL_SENSORS_ID = 0xff000000;
    }
}
