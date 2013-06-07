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
    public enum SensorTypeEnum
    {
        BTL,
        Usb,
        DNG,
        WirelessWired,		//wireless wired (connected to PC)
        Wireless,	//wireless wireless
        EM,
        DL,
        BT,
        Unknown
    }
}
