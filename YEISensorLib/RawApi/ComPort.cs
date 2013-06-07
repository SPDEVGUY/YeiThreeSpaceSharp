using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    /**
    * \brief This structure stores all attributes of a serial port (virtual or otherwise) relevant to the API.
    *
    */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ComPort
    {
        /**
        * \brief The system name for the serial port.
        */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string PortName;
        /**
        * \brief The human-friendly name for the serial port.
        */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string FriendlyName;
        /**
        * \brief The type of 3-Space device connected through the serial port.
        */
        public SensorTypeEnum SensorType;
    }
}
