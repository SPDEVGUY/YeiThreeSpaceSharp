using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief An enum expressing the different types of errors a 3-Space API call can return.
    * 
    */
    public enum ResultEnum
    {
        /**
        * \brief The API call successfuly executed.
        */
        NoError = 0,
        /**
        * \brief The API call was made on a device type that does not suppport the attemped command.
        */
        InvalidCommand,
        /**
        * \brief The TSS_ID parameter passed in to an API call is not associated with a connected 3-Space device.
        */
        InvalidId,
        /**
        * \brief The index passed into a Wireless Sensor API call is used to look up a sensor serial number for a given dongle. If either the index is not 0<= index < 15 (the valid range of the index) or the stored serial number is 0 (meaning no wireless sensor is assigned to the index) this error is returned.
        */
        InvalidIndex,
        /**
        * \brief The API call executed had to fall back to an alternate implimentation to execute because the 3-Space device being used is on an old build of firmware. Firmware updating is suggested if this error is returned.
        */
        WarningOldFirmware,  // Function can complete but is emulated, upgrading is highly recomended
        /**
        * \brief The API call executed could not be completed because the connected 3-Space device has firmware that is too old to support the call. Firmware updating is suggested if this error is returned.
        */
        ErrorOldFirmware,    // Function is not available on this firmware, update it
        /**
        * \brief When creating a 3-Space device that is connected over USB there was an issue communicating with the intended device.
        */
        ErrorWithUsbConnection,
        /**
        * \brief When creating a 3-Space device that is communicating through a 3-Space Dongle there was an issue communicating with the intended device.
        */
        ErrorWithWirelessConnection,
        /**
        * \brief The API call executed failed to write all the data necisary to execute the command to the intended serial port.
        */
        ErrorWriting,
        /**
        * \brief The API call executed failed to read all the data necisary to execute the command to the intended serial port.
        */
        ErrorReading,
        /**
        * \brief no sensor found when looking for sensors
        */
        ErrorNoSensor
    }
}
