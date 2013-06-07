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
        public static SensorDevice GetFirstAvailable(FilterEnum filter)
        {
            ComPort port;
            ThreeSpaceInterop.GetFirstAvailableComPort(out port, (int)filter);
            if(!string.IsNullOrEmpty(port.PortName)) return new SensorDevice(port);
            return null;
        }

        /// <summary>
        /// Return a list of all sensor devices.
        /// Ensure that you dispose of them.
        /// </summary>
        /// <returns>List of all connected threespace devices</returns>
        public static List<SensorDevice> GetDevices()
        {
            var ports = ThreeSpaceInterop.GetAvailableComPorts();
            var result = new List<SensorDevice>();
            foreach(var port in ports) result.Add(new SensorDevice(port));
            return result;
        }
    }
}
