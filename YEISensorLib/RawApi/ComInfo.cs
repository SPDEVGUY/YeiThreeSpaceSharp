using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief This structure stores an expanded collection attributes of a serial port (virtual or otherwise) and the possible 3-Space device connected to it.
    *
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ComInfo
    {
        /**
        * \brief The human-friendly name for the serial port.
        */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string FriendlyName;
        /**
        * \brief The type of 3-Space device connected through the serial port.
        */
        public SensorTypeEnum SensorType;
        /**
        * \brief The serial number for the 3-Space device connected through the serial port.
        */
        /* unsigned int */
        public uint SerialNumber;
        /**
        * \brief The version of the firmware installed on the connected 3-Space device.
        */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string SoftwareVer;
        /**
        * \brief The hardware revision and type of the connected 3-Space device.
        */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string HardwareVer;
        /**
        * \brief Expressing whether the connected device is in its 'bootloader' mode or not.
        */
        /* boolean/byte */
        public byte IsBootloader;
    }
}
