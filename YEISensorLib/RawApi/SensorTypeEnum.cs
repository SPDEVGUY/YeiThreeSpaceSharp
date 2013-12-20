using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    /**
    * \brief An enum expressing the different types of 3-Space Sensors.
    * 
    */
    public enum SensorTypeEnum //AKA TSS_Type
    {
        Unknown,
        Bootloader,
        Usb,
        WirelessDongle,
        Wireless,	//wireless wireless
        WirelessWired,		//wireless wired (connected to PC)
        Embedded,
        DataLogger,
        BlueTooth
    }
}
