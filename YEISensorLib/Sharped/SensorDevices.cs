using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEISensorLib.RawApi;

namespace YEISensorLib.Sharped
{
    public class SensorDevices
    {
        /// <summary>
        /// Returns the first available sensor device
        /// </summary>
        /// <returns></returns>
        public static SensorDevice GetFirstAvailable()
        {
            var port = ThreeSpaceInterop.GetComPort(0);
            if(port != null) return new SensorDevice((ComPort)port);
            return null;
        }

        /// <summary>
        /// Return a list of all sensor devices.
        /// Ensure that you dispose of them.
        /// </summary>
        /// <returns>List of all connected threespace devices</returns>
        public static List<SensorDevice> GetDevices() //NOTE: I don't think this code works.  I think I botched handling their vector thing.
        {
            var ports = new List<ComPort>();
            var thisPort = ThreeSpaceInterop.GetComPort(0);
            uint index = 0;
            while (thisPort != null)
            {
                ports.Add((ComPort)thisPort);
                index++;
                thisPort = ThreeSpaceInterop.GetComPort(index);
            }
            var result = new List<SensorDevice>();
            foreach(var port in ports) result.Add(new SensorDevice(port));
            return result;
        }
    }
}
