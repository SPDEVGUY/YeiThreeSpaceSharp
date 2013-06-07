using System.Runtime.InteropServices;

namespace YEISensorLib.RawApi
{
    /// <summary>
    /// This is practically a direct translation from the yei_threespace_api.h  
    /// If you want object oriented approach, use the other classes.    
    /// 
    /// THIS IS ALL SHIT AND WRONG.  DON'T USE IT.  For reference only (reduce typing)
    /// You will need to properly translate the C++ header parameters to what is needed.
    /// Store the actual tested and functioning functions in ThreeSpaceInterop.
    /// Please name them properly as to C# naming standards.
    /// Implement them in the SensorDevice class internally.
    /// SensorDevice acts as an object oriented approach to controlling the devices.
    /// </summary>
    internal class ThreeSpaceInteropDoNotUse
    {

        ////Async Broadcaster
        /**
        * \brief Stops all aysnc sensors TODO:!!! Work in progress!!!!!
        */
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum stopAllTSSAysncSensors();

        /**
        * \brief Set the sensors to be turned off automaticly on quit !!! Work in progress!!!!!
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void setStopAllTSSAysncSensorsOnQuit(
            /* boolean/byte */ byte stopOnQuit
            );

        /**
        * \brief Scans the host computer for all serial ports (virtual or physical).
        * Ports returned are sorted into two lists based on whether the driver creating the port was a 3-Space driver.
        *
        * @param tss_port_list A pointer to an array filled with serial ports created by 3-Space drivers. These ports have 3-Space hardware connected to them. This pointer will have the address of the array written to it, if not NULL.
        * @param ukn_port_list A pointer to an array filled with serial ports not directly created by a 3-Space driver. These ports may have 3-Space hardware connected through RS-232 port. This pointer will have the address of the array written to it, if not NULL.
        * @param tss_list_size A pointer to the number of listings written to the array pointed to by tss_port_list. This pointer is written to, if not NULL.
        * @param ukn_list_size A pointer to the number of listings written to the array pointed to by ukn_port_list. This pointer is written to, if not NULL.
        * @param filter A mask of what 3-Space device types were desired. Ports appended to tss_port_list will only have devices connected that match the mask.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void getAvailableComPorts(
            out ComPort[] tss_port_list,
            out ComPort[] ukn_port_list,
            /* unsigned int */ out uint tss_list_size,
            /* unsigned int */ out uint ukn_list_size,
            FilterEnum filter
            );

        /**
        * \brief Scans the host computer for the first serial port it finds with a sensor (virtual or physical).
        * Port is returned by tss_port. Calling getAvailableComPorts or getFirstAvailableTSSComPort will reset the iteration through the sensors.
        *
        * @param tss_port A pointer to the first serial port with a 3-Space device that matches the filter.
        * @param filter A mask of what 3-Space device types were desired.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFirstAvailableTSSComPort(
            out ComPort tss_port,
            FilterEnum filter);

        /**
        * \brief Call after getFirstAvailableTSSComPort to get more sensors.
        * Port is returned by tss_port. getFirstAvailableTSSComPort must be called before calling getNextAvailableTSSComPort
        * Calling getAvailableComPorts or getFirstAvailableTSSComPort will reset the iteration through the sensors.
        *
        * @param tss_port A pointer to the next serial port with a 3-Space device that matches the filter used in getFirstAvailableTSSComPort.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getNextAvailableTSSComPort(
            out ComPort tss_port
            );

        /**
        * \brief Scans the host computer for the first serial port it finds with an unknown device (virtual or physical).
        * Port is returned by ukn_port. Calling getAvailableComPorts or getFirstAvailableUnknownComPort will reset the iteration through the unknown ports.
        *
        * @param ukn_port A pointer to the first serial port with an unknown device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFirstAvailableUnknownComPort(
            out ComPort ukn_port
            );

        /**
        * \brief Call after getFirstAvailableUnknownComPort to get more sensors.
        * Port is returned by tss_port. getFirstAvailableUnknownComPort must be called before calling getNextAvailableUnknownComPort
        * Calling getAvailableComPorts or getFirstAvailableUnknownComPort will reset the iteration through the unknown ports.
        *
        * @param ukn_port A pointer to the next serial port with an unknown device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getNextAvailableUnknownComPort(
            out ComPort ukn_port
            );

        /**
        * \brief Interrogates an indicated serial port for information about the device connected to it.
        * Optionaly, the port may be polled to gain further information about the connected device.
        * Polling a port may cause undesired effects if the device connected to the port is not a 3-Space device, as bytes will be written to the serial port.
        * Its suggested that any polling done in an application be an optional action invoked by the user with proper warnings in place.
        *
        * @param com_port The string name of the port to be interrogated.
        * @param com_info Information gathered about the port will be written to this structure.
        * @param poll_device If True, the port will have bytes written to it in order to gain information about the connected device. If False, only driver level information will be gathered.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void getComPortInfo(
            out byte com_port,
            out ComInfo com_info,
            /* boolean/byte */ byte poll_device
            );

        /**
        * \brief Given a serial port, the type of 3-Space device is determined from the passed in structure and the device is connected to.
        * An identifier for the device is returned.
        *
        * @param com_port The serial port to connect the device on.
        * @return An identifier for the created device.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern /* TSS_ID */ uint createTSDevice(
            ComPort com_port
            );

        /**
        * \brief Given a serial port and the type of 3-Space device, the device is connected to.
        * An identifier for the device is returned.
        *
        * @param com_port The serial port name.
        * @param type The type of 3-Space device connected on the serial port.
        * @return An identifier for the created device.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern /* TSS_ID */ uint createTSDeviceStr(
            out byte com_port,
            SensorTypeEnum type
            );

        /**
        * \brief Disconnects the 3-Space device associated with the inputted ID.
        * If connected through a serial port, that port is closed.
        *
        * @param id The identifier for the device to disconnect.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum closeTSDevice(
            /* TSS_ID */ uint id
            );

        /**
        * \brief Checks if a 3-Space device is currently connected to the computer.
        *
        * @param id The identifier for the device to check.
        * @param try_reconnect If True, an attempt will be made to reconnect the device if the device is not connected.
        * @return True if the device is (re)connected. False if the device is not connected.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern /* boolean/byte */ byte isTSDeviceThere(
            /* TSS_ID */ uint id,
                         /* boolean/byte */ byte try_reconnect
            );

        /**
        * \brief Retrieves a hexidecimal string representation of the 3-Space device's serial number (matching the representation on the case of the device).
        *
        * @param id The identifier for the 3-Space device.
        * @param hex_serial The char buffer to write the string into. Should have a length of 9 (8 for the hex string, 1 for a null terminator).
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getSerialNumberHex(
            /* TSS_ID */ uint id,
                         out byte hex_serial
            );

        /**
        * \brief Retrieves the type of 3-Space device connected as a string.
        *
        * @param id The identifier for the 3-Space device.
        * @param device_type An enum of TSS_Type that gives the devices base type specification, on wireless units can denote how it is connected.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getDeviceType(
            /* TSS_ID */ uint id,
                         out SensorTypeEnum device_type
            );

        /**
        * \brief Retrieves the friendly name of the device's serial port as reported by the port's driver.
        *
        * @param id The identifier for the 3-Space device.
        * @param friendly_name The char buffer to write the string into. The string length can vary depending on the device. Make the buffer rather large (255 perhaps).
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFriendlyName(
            /* TSS_ID */ uint id,
                         out byte friendly_name
            );

        /**
        * \brief Registers a 3-Space Wireless Sensor with a 3-Space Dongle and connects the wireless sensor.
        *
        * @param id The identifier for the 3-Space Dongle.
        * @param idx The index into the dongle's wireless table to register the wireless sensor to. May be between 0 and 15.
        * @param hw_id The serial number of the wireless sensor to register.
        * @param axis_dir An axis direction configuration to default the wireless sensor to. May be set to NULL if no default axis direction is desired.
        * @param wl_id The unique identifier for the wireless sensor will be written to the variable referenced by this pointer.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum addSensorToDongle(
            /* TSS_ID */ uint id,
                         /* unsigned char */ byte idx,
                         /* unsigned int */ uint hw_id,
                         out AxisDirections axis_dir,
                         /* TSS_ID */ out uint wl_id
            );

        /**
        * \brief Retrieves the unique identifier for a 3-Space Wireless Sensor already registered with a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space Dongle.
        * @param idx The index into the dongle's wireless table to register the wireless sensor to. May be between 0 and 15.
        * @param wl_id The unique identifier for the wireless sensor will be written to the variable referenced by this pointer.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getSensorFromDongle(
            /* TSS_ID */ uint id,
                         /* unsigned int */ uint idx,
                         /* TSS_ID */ out uint wl_id
            );

        // 0(0x00)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a TSS_Quaternion.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The orientation will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientQuat(
            /* TSS_ID */ uint id,
                         out Quaternion quat
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a TSS_Quaternion.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The orientation will be written to this array as four floats. The order of elements will be x, y, z, w.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientQuatf4(
            /* TSS_ID */ uint id,
                         out float quat
            );

        // 1(0x01)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a set of TSS_Euler angles.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param euler The orientation will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientEuler(
            /* TSS_ID */ uint id,
                         out Euler euler
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a set of TSS_Euler angles.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param euler The orientation will be written to this array of three floats. The order of elements will be x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientEulerf3(
            /* TSS_ID */ uint id,
                         out float euler
            );

        // 2(0x02)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a rotation matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mat The orientation will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientMat(
            /* TSS_ID */ uint id,
                         out Matrix mat
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a rotation matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mat The orientation will be written to this array of nine floats. The floats will be written in row-major.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientMatf9(
            /* TSS_ID */ uint id,
                         out float mat
            );

        // 3(0x03)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as an Axis Angle.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param axis_ang The orientation will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientAxisAngle(
            /* TSS_ID */ uint id,
                         out AxisAngle axis_ang
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as an Axis Angle.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param axis_ang The orientation will be written to this array of four floats. The floats are written as x, y, z, angle. The x, y, and z values are elements of a normalized vector.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientAxisAnglef4(
            /* TSS_ID */ uint id,
                         out float axis_ang
            );

        // 4(0x04)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a pair of two vectors (A Forward vector and a Down vector).
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param forward The forward vector will be written to this structure.
        * @param down The down vector will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientFwdDwn(
            /* TSS_ID */ uint id,
                         out Vector forward,
                         out Vector down
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a pair of two vectors (A Forward vector and a Down vector).
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param forward The forward vector will be written to this array of 3 floats. The floats will be ordered x, y, z.
        * @param down The down vector will be written to this array of 3 floats. The floats will be ordered x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredOrientFwdDwnf3f3(
            /* TSS_ID */ uint id,
                         out float forward,
                         out float down
            );

        // 5(0x05)
        /**
        * \brief Returns the gyro rates as determined by taking the difference between the current orientation and the previous one.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The quaternion representing this rate of change will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltGyroRate(
            /* TSS_ID */ uint id,
                         out Quaternion quat
            );

        /**
        * \brief Returns the gyro rates as determined by taking the difference between the current orientation and the previous one.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The quaternion representing this rate of change will be written to this array of four floats. The float are ordered x, y, z, w.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltGyroRatef4(
            /* TSS_ID */ uint id,
                         out float quat
            );

        // 6(0x06)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as a TSS_Quaternion.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The orientation will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientQuat(
            /* TSS_ID */ uint id,
                         out Quaternion quat
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as a TSS_Quaternion.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The orientation will be written to this array as four floats. The order of elements will be x, y, z, w.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientQuatf4(
            /* TSS_ID */ uint id,
                         out float quat
            );

        // 7(0x07)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as a set of TSS_Euler angles.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param euler The orientation will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientEuler(
            /* TSS_ID */ uint id,
                         out Euler euler
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as a set of TSS_Euler angles.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param euler The orientation will be written to this array of three floats. The order of elements will be x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientEulerf3(
            /* TSS_ID */ uint id,
                         out float euler
            );

        // 8(0x08)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as a rotation matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mat The orientation will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientMat(
            /* TSS_ID */ uint id,
                         out Matrix mat
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as a rotation matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mat The orientation will be written to this array of nine floats. The floats will be written in row-major.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientMatf9(
            /* TSS_ID */ uint id,
                         out float mat
            );

        // 9(0x09)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as an Axis Angle.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param axis_ang The orientation will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientAxisAngle(
            /* TSS_ID */ uint id,
                         out AxisAngle axis_ang
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as an Axis Angle.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param axis_ang The orientation will be written to this array of four floats. The floats are written as x, y, z, angle. The x, y, and z values are elements of a normalized vector.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientAxisAnglef4(
            /* TSS_ID */ uint id,
                         out float axis_ang
            );

        // 10(0x0a)
        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as a pair of two vectors (A Forward vector and a Down vector).
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param forward The forward vector will be written to this structure.
        * @param down The down vector will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientFwdDwn(
            /* TSS_ID */ uint id,
                         out Vector forward,
                         out Vector down
            );

        /**
        * \brief Retrieves the current orientation of a 3-Space Sensor relative to its detected gravity and north vectors as a pair of two vectors (A Forward vector and a Down vector).
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param forward The forward vector will be written to this array of 3 floats. The floats will be ordered x, y, z.
        * @param down The down vector will be written to this array of 3 floats. The floats will be ordered x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltOrientFwdDwnf3f3(
            /* TSS_ID */ uint id,
                         out float forward,
                         out float down
            );

        // 11(0x0b)
        /**
        * \brief Returns the forward and down vectors as determined by the Kalman filter, relative to the tare orientation.
        * This version returns the data as the forward and down vectors of the coordinate system defined by the sensor reference frame.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param forward The forward vector will be written to this structure.
        * @param down The down vector will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredFwdDwnVecSensFrame(
            /* TSS_ID */ uint id,
                         out Vector forward,
                         out Vector down
            );

        /**
        * \brief Returns the forward and down vectors as determined by the Kalman filter, relative to the tare orientation.
        * This version returns the data as the forward and down vectors of the coordinate system defined by the sensor reference frame.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param forward The forward vector will be written to this array of 3 floats. The floats will be ordered x, y, z.
        * @param down The down vector will be written to this array of 3 floats. The floats will be ordered x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltTaredFwdDwnVecSensFramef3f3(
            /* TSS_ID */ uint id,
                         out float forward,
                         out float down
            );

        // 12(0x0c)
        /**
        * \brief Returns the North and Earth vectors as determeined by the Kalman filter, relative to the global sensor references.
        * Returned vectors are in the coordinate system defined by the sensor reference frame.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param earth The earth (gravity) vector will be written to this structure.
        * @param north The north (magnetic north) vector will be written to this structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltNorthEarthVecSensFrame(
            /* TSS_ID */ uint id,
                         out Vector north,
                         out Vector earth
            );

        /**
        * \brief Returns the North and Earth vectors as determeined by the Kalman filter, relative to the global sensor references.
        * Returned vectors are in the coordinate system defined by the sensor reference frame.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param earth The earth (gravity) vector will be written to this array of 3 floats. The floats will be ordered x, y, z.
        * @param north The north (magnetic north) vector will be written to this array of 3 floats. The floats will be ordered x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFiltNorthEarthVecSensFramef3f3(
            /* TSS_ID */ uint id,
                         out float north,
                         out float earth
            );

        // 29(0x1d)
        /**
        * \brief Configures how the sensor will generate interrupts..
        * This call will error if the inputted id is not for a 3-Space Embedded Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param mode A value that specifies how the sensor will generate the interupt and for how long. 
        * If mode is 0, no generation will occur.
        * If mode is 1, the interrupt generator will set the specified pin low for 5 microseconds when new data is available.
        * If mode is 2, the interrupt generator will set the pin low until the interrupt status is read with the getInterruptStatus function.
        * @param pin A value that defines what pin on the sensor to generate the interupt on.
        * If pin is 0, the TXD pin is used. If pin is 1, the MISO pin is used to generate the interrupt.
        * The method of communication each pin belongs to cannot be uwed while that pin is being used for interrupt generation.
        * Note that the pin cannot be changed to TXD over the UART, and cannot be changed to MISO over SPI.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setInterruptType(
            /* TSS_ID */ uint id,
                         /* unsigned char */ byte mode,
                         /* unsigned char */ byte pin
            );

        // 30(0x1e)
        /**
        * \brief Returns how the sensor currently generates interrupts.
        * This call will error if the inputted id is not for a 3-Space Embedded Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param mode The value that specifies how the sensor will generate the interupt and for how long will be written to the referenced variable. 
        * If mode is 0, no generation will occur.
        * If mode is 1, the interrupt generator will set the specified pin low for 5 microseconds when new data is available.
        * If mode is 2, the interrupt generator will set the pin low until the interrupt status is read with the getInterruptStatus function.
        * @param pin The value that defines what pin on the sensor to generate the interupt on will be written to the referenced variable.
        * If pin is 0, the TXD pin is used. If pin is 1, the MISO pin is used to generate the interrupt.
        * The method of communication each pin belongs to cannot be uwed while that pin is being used for interrupt generation.
        * Note that the pin cannot be changed to TXD over the UART, and cannot be changed to MISO over SPI.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getInterruptType(
            /* TSS_ID */ uint id,
                         /* unsigned char */ out byte mode,
                         /* unsigned char */ out byte pin
            );

        // 31(0x1f)
        /**
        * \brief Read the current interrupt state.
        * Calling this command while interrupts are in mode '2' (see definition for setInterruptType for more details) will cause the interrupt pin to return to high.
        * This call will error if the inputted id is not for a 3-Space Embedded Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param status The value that specifies the sensor's interrupt status will be written to the referenced variable. 
        * An integer whose value can either be 0 or 1.
        * If the value is 0, there is no new data since the last orientation read.
        * If the value is 1, then new data is available.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getInterruptStatus(
            /* TSS_ID */ uint id,
                         /* unsigned char */ out byte status
            );

        // 32(0x20)
        /**
        * \brief Returns the normalized data of the 3-Space Sensor's on-board sensors.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param data The normalized data from the 3-Space Sensor's accelerometer, gyroscope, and compass are filled into the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAllSensorsNormalized(
            /* TSS_ID */ uint id,
                         out SensorData data
            );

        /**
        * \brief Returns the normalized data of the 3-Space Sensor's on-board sensors.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param data The normalized data from the 3-Space Sensor's accelerometer, gyroscope, and compass are written to the referenced array of 9 floats.
        * The floats are ordered gyro_x, gyro_y, gyro_z, accel_x, accel_y, accel_z, compass_x, compass_y, compass_z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAllSensorsNormalizedf9(
            /* TSS_ID */ uint id,
                         out float data
            );

        // 33(0x21)
        /**
        * \brief Returns the normalized gyro rates of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param gyro The normalized data from the 3-Space Sensor's on-board gyroscope is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getGyroNormalized(
            /* TSS_ID */ uint id,
                         out Vector gyro
            );

        /**
        * \brief Returns the normalized gyro rates of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param gyro The normalized data from the 3-Space Sensor's on-board gyroscope is written to the referenced array of 3 floats.
        * The floats are ordered: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getGyroNormalizedf3(
            /* TSS_ID */ uint id,
                         out float gyro
            );

        // 34(0x22)
        /**
        * \brief Returns the normalized accelerometer vector of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param accelerometer The normalized data from the 3-Space Sensor's on-board accelerometer is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerNormalized(
            /* TSS_ID */ uint id,
                         out Vector accelerometer
            );

        /**
        * \brief Returns the normalized accelerometer vector of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param accelerometer The normalized data from the 3-Space Sensor's on-board accelerometer is written to the referenced array of 3 floats.
        * The floats are ordered: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerNormalizedf3(
            /* TSS_ID */ uint id,
                         out float accelerometer
            );

        // 35(0x23)
        /**
        * \brief Returns the normalized compass vector of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param compass The normalized data from the 3-Space Sensor's on-board compass is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassNormalized(
            /* TSS_ID */ uint id,
                         out Vector compass
            );

        /**
        * \brief Returns the normalized compass vector of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param compass The normalized data from the 3-Space Sensor's on-board compass is written to the referenced array of 3 floats.
        * The floats are ordered: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassNormalizedf3(
            /* TSS_ID */ uint id,
                         out float compass
            );

        // 36(0x24)
        /**
        * \brief Returns the temperature of the sensor in Celsius.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param temp_c The temprature read by the sensor's thermometer in Celsius is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getTemperatureCelsius(
            /* TSS_ID */ uint id,
                         out float temp_c
            );

        // 37(0x25)
        /**
        * \brief Returns the temperature of the sensor in Fahrenheit.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param temp_f The temprature read by the sensor's thermometer in Fahrenheit is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getTemperatureFahrenheit(
            /* TSS_ID */ uint id,
                         out float temp_f
            );

        // 38(0x26)
        /**
        * \brief Returns a value indicating how much the compass and accelerometer are to be trusted to be unit vectors pointing in the proper directions at the moment.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param confidence The confidence value is written to the referenced variable.
        * This value ranges from 0 to 1, with 1 being fully trusted and 0 being not trusted at all.
        * This will change based on if the sensor is being moved and if it is near a strong magnetic field.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getConfidence(
            /* TSS_ID */ uint id,
                         out float confidence
            );

        // 39(0x27)
        /**
        * \brief Returns the unfiltered (but compensated) data of the 3-Space Sensor's on-board accelerometer.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param accelerometer The data from the 3-Space Sensor's on-board accelerometer is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerUnfiltered(
            /* TSS_ID */ uint id,
                         out Vector accelerometer
            );

        /**
        * \brief Returns the unfiltered (but compensated) data of the 3-Space Sensor's on-board accelerometer.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param accelerometer The data from the 3-Space Sensor's on-board accelerometer is written to the referenced array of 3 floats.
        * The floats are ordered: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerUnfilteredf3(
            /* TSS_ID */ uint id,
                         out float accelerometer
            );

        // 40(0x28)
        /**
        * \brief Returns the unfiltered (but compensated) data of the 3-Space Sensor's on-board compass.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param compass The data from the 3-Space Sensor's on-board compass is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassUnfiltered(
            /* TSS_ID */ uint id,
                         out Vector compass
            );

        /**
        * \brief Returns the unfiltered (but compensated) data of the 3-Space Sensor's on-board compass.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param compass The data from the 3-Space Sensor's on-board compass is written to the referenced array of 3 floats.
        * The floats are ordered: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassUnfilteredf3(
            /* TSS_ID */ uint id,
                         out float compass
            );

        // 57(0x39)
        /**
        * \brief When enabled, the 3-Space Sensor acts as a mass storage device on the host system.
        * This is useful for reading/writing log files to the sensor.
        * This call will error if the inputted id is not for a 3-Space Data-Logging Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum enableMassStorageMode(
            /* TSS_ID */ uint id
            );

        // 58(0x3a)
        /**
        * \brief When disabled, the 3-Space Sensor's mass storage features are hidden from the host machine.
        * This call will error if the inputted id is not for a 3-Space Data-Logging Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum disableMassStorageMode(
            /* TSS_ID */ uint id
            );

        // 59(0x3b)
        /**
        * \brief The SD card contained within the 3-Space Sensor is formatted to the FAT-32 file system and initialized for writing/reading.
        * This call will error if the inputted id is not for a 3-Space Data-Logging Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum formatInitSDCard(
            /* TSS_ID */ uint id
            );

        // 60(0x3c)
        /**
        * \brief Begins a data logging session based on the contents of the capture.cfg file.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum startLogSession(
            /* TSS_ID */ uint id
            );

        // 61(0x3d)
        /**
        * \brief Stops any active capture sessions.
        * This call will error if the inputted id is not for a 3-Space Data-Logging Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum stopLogSession(
            /* TSS_ID */ uint id
            );

        // 62(0x3e)
        /**
        * \brief Sets the current data and time of the 3-Space Sensor's internal clock to the inputted date and time.
        * This call will error if the inputted id is not for a 3-Space Data-Logging Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param clock_data The current time to set the clock to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setClock(
            /* TSS_ID */ uint id,
                         Clock clock_data
            );

        /**
        * \brief Sets the current data and time of the 3-Space Sensor's internal clock to the inputted date and time.
        * This call will error if the inputted id is not for a 3-Space Data-Logging Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param clock_data The current time to set the clock to as an array of six chars.
        * This array should be formatted as: month, day, year, hour, minute, second.
        * Reference ::TSS_Clock for a definition of the expected ranges of values for the array elements.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setClockc6(
            /* TSS_ID */ uint id,
                         /* unsigned char */ out byte clock_data
            );

        // 63(0x3f)
        /**
        * \brief Gets the current data and time of the 3-Space Sensor's internal clock.
        * This call will error if the inputted id is not for a 3-Space Data-Logging Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param clock_data The current time of the sensor's clock will be written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getClock(
            /* TSS_ID */ uint id,
                         out Clock clock_data
            );

        /**
        * \brief Gets the current data and time of the 3-Space Sensor's internal clock.
        * This call will error if the inputted id is not for a 3-Space Data-Logging Sensor.
        * This array will be formatted as: month, day, year, hour, minute, second.
        * @param id The identifier for the 3-Space device.
        * @param clock_data The current time of the sensor's clock will be written to the referenced array of six chars.
        * 
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getClockc6(
            /* TSS_ID */ uint id,
                         /* unsigned char */ out byte clock_data
            );

        // 64(0x40)
        /**
        * \brief Returns the unaltered data of the 3-Space Sensor's on-board sensors.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param data The unaltered data from the 3-Space Sensor's accelerometer, gyroscope, and compass are filled into the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAllSensorsRaw(
            /* TSS_ID */ uint id,
                         out SensorData data
            );

        /**
        * \brief Returns the unaltered data of the 3-Space Sensor's on-board sensors.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param data The unaltered data from the 3-Space Sensor's accelerometer, gyroscope, and compass are written to the referenced array of 9 floats.
        * The floats are ordered gyro_x, gyro_y, gyro_z, accel_x, accel_y, accel_z, compass_x, compass_y, compass_z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAllSensorsRawf9(
            /* TSS_ID */ uint id,
                         out float data
            );

        // 65(0x41)
        /**
        * \brief Returns the unaltered gyro rates of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param gyro The unaltered data from the 3-Space Sensor's on-board gyroscope is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getGyroRaw(
            /* TSS_ID */ uint id,
                         out Vector gyro
            );

        /**
        * \brief Returns the unaltered gyro rates of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param gyro The unaltered data from the 3-Space Sensor's on-board gyroscope is written to the referenced array of 3 floats.
        * The floats are ordered: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getGyroRawf3(
            /* TSS_ID */ uint id,
                         out float gyro
            );

        /**
        * \brief Writes unencrypted data to the 3-Space Sensor.
        * This call will error if the inputted id is not for a 3-Space device in Bootloader mode.
        *
        * @param id The identifier for the 3-Space device.
        * @param data The array of bytes to write to the 3-Space device.
        * @param data_size The number of bytes in the array.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum writeUnencryptedData(
            /* TSS_ID */ uint id,
                         /* unsigned char */ out byte data,
                         /* unsigned int */ uint data_size
            );

        // 66(0x42)
        /**
        * \brief Returns the unaltered data of the 3-Space Sensor's on-board accelerometer.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param accelerometer The unaltered data from the 3-Space Sensor's on-board accelerometer is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerRaw(
            /* TSS_ID */ uint id,
                         out Vector accelerometer
            );

        /**
        * \brief Returns the unaltered data of the 3-Space Sensor's on-board accelerometer.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param accelerometer The unaltered data from the 3-Space Sensor's on-board accelerometer is written to the referenced array of three floats.
        * The array is structured: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerRawf3(
            /* TSS_ID */ uint id,
                         out float accelerometer
            );

        /**
        * \brief Resets the device to use firmware loaded to the 3-Space device.
        * Will remain in bootloader mode if the firmware was corrupted.
        * This call will error if the inputted id is not for a 3-Space device in Bootloader mode.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setToFirmwareMode(
            /* TSS_ID */ uint id
            );

        // 67(0x43)
        /**
        * \brief Returns the unaltered compass vector of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param compass The unaltered data from the 3-Space Sensor's on-board compass is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassRaw(
            /* TSS_ID */ uint id,
                         out Vector compass
            );

        /**
        * \brief Returns the unaltered compass vector of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param compass The unaltered data from the 3-Space Sensor's on-board compass is written to the referenced array of 3 floats.
        * The floats are ordered: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassRawf3(
            /* TSS_ID */ uint id,
                         out float compass
            );

        /**
        * \brief Writes encrypted data to the 3-Space Sensor.
        * This call will error if the inputted id is not for a 3-Space device in Bootloader mode.
        *
        * @param id The identifier for the 3-Space device.
        * @param data The array of bytes to write to the 3-Space device.
        * @param data_size The number of bytes in the array.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum writeEncryptedData(
            /* TSS_ID */ uint id,
                         /* unsigned char */ out byte data,
                         /* unsigned int */ uint data_size
            );

        // 70(0x46)
        /**
        * \brief Tells the 3-Space device to stop writting.
        * This call will error if the inputted id is not for a 3-Space device in Bootloader mode.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum finishWrite(
            /* TSS_ID */ uint id
            );

        // 73(0x49)
        /**
        * \brief Gets information about the 3-Space device in bootloader mode.
        * This call will error if the inputted id is not for a 3-Space device in Bootloader mode.
        *
        * @param id The identifier for the 3-Space device.
        * @param data The information about the device will be written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getInformation(
            /* TSS_ID */ uint id,
                         out BootloaderData data
            );

        // 78(0x4e)
        /**
        * \brief Sets the serial number for the 3-Space device in bootloader mode.
        * This call will error if the inputted id is not for a 3-Space device in Bootloader mode.
        *
        * @param id The identifier for the 3-Space device.
        * @param serial The serial number to set to the device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setSerialNumber(
            /* TSS_ID */ uint id,
                         /* unsigned int */ uint serial
            );

        // 79(0x4f)
        /**
        * \brief Checks the firmware loaded into the 3-Space device is valid or not.
        * This call will error if the inputted id is not for a 3-Space device in Bootloader mode.
        *
        * @param id The identifier for the 3-Space device.
        * @param is_valid If True is written to this referenced variable, the firmware is valid.
        * If False is written, the firmware is corrupt in some way.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum isFirmwareValid(
            /* TSS_ID */ uint id,
                         /* boolean/byte */ out byte is_valid
            );

        // 83(0x53)
        /**
        * \brief Sets the memory address for writting firmware to the 3-Space device in bootloader mode.
        * This call will error if the inputted id is not for a 3-Space device in Bootloader mode.
        *
        * @param id The identifier for the 3-Space device.
        * @param start_addr Denotes the start position for writting the firmware.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setWriteStart(
            /* TSS_ID */ uint id,
                         /* unsigned int */ uint start_addr
            );

        // 96(0x60)
        /**
        * \brief Sets current filtered orientation derived from its detected gravity and north vectors as the tare orientation.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setTareCurrentOrient(
            /* TSS_ID */ uint id
            );

        // 97(0x61)
        /**
        * \brief Sets current filtered orientation to a passed in quaternion.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The quaternion to set the tare orientation to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setTareQuaternion(
            /* TSS_ID */ uint id,
                         Quaternion quat);

        /**
        * \brief Sets current filtered orientation to a passed in quaternion.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The quaternion to set the tare orientation to.
        * The array of four floats should be structured as such: x, y, z, w.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setTareQuaternionf4(
            /* TSS_ID */ uint id,
                         out float quat
            );

        // 98(0x62)
        /**
        * \brief Sets current filtered orientation to a passed in rotation matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to set the tare orientation to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setTareMatrix(
            /* TSS_ID */ uint id,
                         Matrix mat
            );

        /**
        * \brief Sets current filtered orientation to a passed in rotation matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to set the tare orientation to.
        * The array of nine floats should be arranged in row-major order.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setTareMatrixf9( /* TSS_ID */ uint id, out float mat);

        // 99(0x63)
        /**
        * \brief Sets the rho mode for the accelerometer to static, using the given value as rho.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param val The value to use as the new rho value.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setStaticRhoModeAccel( /* TSS_ID */ uint id, float val);

        // 100(0x64)
        /**
        * \brief Sets the rho mode for the accelerometer to confidence, using the given values as the min and the max.
        * The confidence factor will be used in real time to interpolate between the min and max to determine what rho value is used.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param min Represents the minimum Rho value to be used for calculation.
        * @param max Represents the maximmum Rho value to be used for calculation.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setConfidenceRhoModeAccel( /* TSS_ID */ uint id, float min, float max);

        // 101(0x65)
        /**
        * \brief Sets the rho mode for the compass to static, using the given value as rho.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param val The value to use as the new rho value.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setStaticRhoModeCompass( /* TSS_ID */ uint id, float val);

        // 102(0x66)
        /**
        * \brief Sets the rho mode for the compass to confidence, using the given values as the min and the max.
        * The confidence factor will be used in real time to interpolate between the min and max to determine what rho value is used.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param min Represents the minimum Rho value to be used for calculation.
        * @param max Represents the maximmum Rho value to be used for calculation.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setConfidenceRhoModeCompass( /* TSS_ID */ uint id, float min, float max);

        // 103(0x67)
        /**
        * \brief Sets the target update rate to the given rate.
        * If the filter takes less time to update during a given frame than this rate specifies, the frame will be padded out to take the specified amount of time.
        * If the filter takes more time to update than this rate, this rate will be ignored.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param rate Represents the desired update rate in microseconds.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setUpdateRate( /* TSS_ID */ uint id, /* unsigned int */ uint rate);

        // 104(0x68)
        /**
        * \brief Uses the current tared orientation to set up the reference vector for the nearest orthogonal orientation.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setRefVecCurrentOrient( /* TSS_ID */ uint id);

        // 105(0x69)
        /**
        * \brief Sets reference vector mode to either 'single static', 'single auto', 'single auto continuous', or 'multi'.
        * 
        * Single static mode uses a certain reference vector for the compass and another certain reference vector for the accelerometer at all times.
        * 
        * Single auto mode uses (0,-1,0) as the reference vector for the accelerometer at all times and uses the current average angle between the accelerometer and compass to calculate the compass reference vector.
        * After that this mode acts like single static mode.
        * 
        * Single auto continuous mode uses (0,-1,0) as the reference vector for the accelerometer at all times and uses the average angle between the accelerometer and compass to constantly redetermine the compass reference vector.
        * 
        * Multi mode uses a collection of reference vectors for the compass and accelerometer, and selects which ones to use before each step of the filter.
        * 
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mode The desired mode of operation. (0 for single static, 1 for single auto, 2 for single auto continuous, 3 for multi).
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setRefVecMode( /* TSS_ID */ uint id, /* unsigned char */ byte mode);

        // 106(0x6a)
        /**
        * \brief Sets oversample rate of data reads from the 3-Space Sensor's on-board sensors.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param rate The amount of oversampling to perform. 1 turns off oversampling. 2+ sets the number of samples per frame.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setOversampleRate( /* TSS_ID */ uint id, /* unsigned char */ byte rate);

        // 107(0x6b)
        /**
        * \brief Enables or disables the gyros (will be replaced with a still gyro reading if disabled).
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param do_enable Whether to enable or disable the gyro (False - Disabled, True - Enabled).
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setGyroEnabled( /* TSS_ID */ uint id, /* boolean/byte */ byte do_enable);

        // 108(0x6c)
        /**
        * \brief Enables or disables the accelerometer (the filter will not use this data if disabled).
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param do_enable Whether to enable or disable the accelerometer (False - Disabled, True - Enabled).
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setAccelerometerEnabled( /* TSS_ID */ uint id, /* boolean/byte */ byte do_enable);

        // 109(0x6d)
        /**
        * \brief Enables or disables the compass (the filter will not use this data if disabled).
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param do_enable Whether to enable or disable the compass (False - Disabled, True - Enabled).
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setCompassEnabled( /* TSS_ID */ uint id, /* boolean/byte */ byte do_enable);

        // 110(0x6e)
        /**
        * \brief Resets all reference vectors in the multi reference scheme to zero.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefVecZero( /* TSS_ID */ uint id);

        // 111(0x6f)
        /**
        * \brief Sets number of cell divisions and number of nearby vectors per cell for the multi reference lookup table.
        * 
        * By default, the multiple reference vector mode only deals with orientations obtainable by successive rotations of 90 degrees about any of the three axes.
        * This command can adjust it to accept orientations obtainable by 45 degree rotations. For 90 degrees, give a "number of cell divisions" of 4, and for 45 give 8.
        * In addition, the number of vectors near to any given orientation which the scheme will check can be adjusted as well.
        * If this value is 0, only the nearest orientation will be checked.
        * The largest this value can be is 32. 
        * Also note that the larger this value is, the longer the multiple reference vector mode will take to run each cycle.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param num_division The number of cell divisions.
        * @param num_vecs The number of nearby vectors.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefResolution( /* TSS_ID */
            uint id, /* unsigned char */ byte num_division, /* unsigned char */ byte num_vecs);

        // 112(0x70)
        /**
        * \brief Directly set compass multi reference vector at the specified index to the specified vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the compass to set the vector to.
        * @param vec The vector to set at idx.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefVecCompass( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, Vector vec);

        /**
        * \brief Directly set compass multi reference vector at the specified index to the specified vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the compass to set the vector to.
        * @param vec The vector to set at idx as an array of three floats.
        * The floats should be arranged: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefVecCompassf3( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out float vec);

        // 113(0x71)
        /**
        * \brief Directly set compass multi reference check vector at the specified index to the specified vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the compass to set the vector to.
        * @param vec The vector to set at idx.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefChkVecCompass( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, Vector vec);

        /**
        * \brief Directly set compass multi reference check vector at the specified index to the specified vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the compass to set the vector to.
        * @param vec The vector to set at idx as an array of three floats.
        * The floats should be arranged: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefChkVecCompassf3( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out float vec);

        // 114(0x72)
        /**
        * \brief Directly set accelerometer multi reference vector at the specified index to the specified vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the accelerometer to set the vector to.
        * @param vec The vector to set at idx.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefVecAccelerometer( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, Vector vec);

        /**
        * \brief Directly set accelerometer multi reference vector at the specified index to the specified vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the accelerometer to set the vector to.
        * @param vec The vector to set at idx as an array of three floats.
        * The floats should be arranged: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefVecAccelerometerf3( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out float vec);

        // 115(0x73)
        /**
        * \brief Directly set accelerometer multi reference check vector at the specified index to the specified vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the accelerometer to set the vector to.
        * @param vec The vector to set at idx.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefChkVecAccelerometer( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, Vector vec);

        /**
        * \brief Directly set accelerometer multi reference check vector at the specified index to the specified vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the accelerometer to set the vector to.
        * @param vec The vector to set at idx as an array of three floats.
        * The floats should be arranged: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefChkVecAccelerometerf3( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out float vec);

        // 116(0x74)
        /**
        * \brief Sets the axis direction configuration for a 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param axis_dir The axis direction configuration to set the sensor to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setAxisDirections( /* TSS_ID */ uint id, AxisDirections axis_dir);

        /**
        * \brief Sets the axis direction configuration for a 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param axis_dir An array of bytes that denotes the axis direction configuration to set the sensor to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setAxisDirectionsuc4( /* TSS_ID */ uint id, /* unsigned char */ out byte axis_dir);

        // 117(0x75)
        /**
        * \brief Sets what percentage of running average to use on the sensor's orientation. 
        * 
        * This is computed as follows:
        * 
        *   total_orient = total_orient * percent
        *     
        *   total_orient = total_orient + current_orient * (1 - percent)
        * 
        *   current_orient = total_orient
        *    
        * If the percentage is 0, the running average will be shut off completely.
        * 
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param val The percentage of the running average to use for the sensor's orientation.
        * Valid values are within the range 0.0 to 1.0.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setAvgPercent( /* TSS_ID */ uint id, float val);

        // 118(0x76)
        /**
        * \brief Sets the static compass reference vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param vec The vector to set the compass's reference vector to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setRefVecCompass( /* TSS_ID */ uint id, Vector vec);

        /**
        * \brief Sets the static compass reference vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param vec The vector to set the compass's reference vector to as an array of three floats.
        * The floats should be arranged: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setRefVecCompassf3( /* TSS_ID */ uint id, out float vec);

        // 119(0x77)
        /**
        * \brief Sets the static accelerometer reference vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param vec The vector to set the accelerometer's reference vector to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setRefVecAccelerometer( /* TSS_ID */ uint id, Vector vec);

        /**
        * \brief Sets the static accelerometer reference vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param vec The vector to set the accelerometer's reference vector to as an array of three floats.
        * The floats should be arranged: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setRefVecAccelerometerf3( /* TSS_ID */ uint id, out float vec);

        // 120(0x78)
        /**
        * \brief Resets Kalman filter's covariance and state matrices.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum resetKalmanFilter( /* TSS_ID */ uint id);

        // 121(0x79)
        /**
        * \brief Set which range of accelerometer data to use.
        * Higher ranges can detect and report larger accelerations, but are not as accurate for smaller ones.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param range The intended range for the accelerometer.
        * Valid values are: 0 for 2G, 1 for 4G, and 2 for 8G.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setAccelerometerRange( /* TSS_ID */ uint id, /* unsigned char */ byte range);

        // 122(0x7a)
        /**
        * \brief Set weighting power for multi reference vector weights.
        * Multi reference vector weights are all raised to the weight power before they are summed and used in the calculation for the final reference vector.
        * Setting this value nearer to 0 will cause the reference vectors to overlap more, and setting it nearer to infinity will cause the reference vectors to influence a smaller set of orientations.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param pow The weight power to use.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMultiRefWeightPwr( /* TSS_ID */ uint id, float pow);

        // 123(0x7b)
        /**
        * \brief Used to disable the orientation filter or set the orientation filter mode.
        * Changing this parameter can be useful for tuning filter-performance versus orientation-update rates.
        * When using the sensor as an IMU, it will improve performance to disable the orientation filter.
        * When using as an AHRS, where orientation is needed, full-Kalman, gyroless-filtered, or fast-filtered should be used.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mode The mode to set the sensor's filter to.
        * Possible values are:
        * <br>0 - Disables filter
        * <br>1 - Full Kalman Filter
        * <br>2 - Gyroless Filtered Orientation <i>Not Yet Supported</i>
        * <br>3 - Fast Filtered Orientation <i>Not Yet Supported</i>
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setFilterMode( /* TSS_ID */ uint id, /* unsigned char */ byte mode);

        // 124(0x7c)
        /**
        * \brief Selects the mode that the running-average method uses.
        * 'Normal' uses a static running-average.
        * 'Confidence' uses a running average that changes dynamically based upon the confidence factor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mode The mode to set the sensor's running average to.        
        * Possible values are:
        * <br>0 - Normal mode
        * <br>1 - Confidence Mode
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setRunningAvgMode( /* TSS_ID */ uint id, /* unsigned char */ byte mode);

        // 125(0x7d)
        /**
        * \brief Sets the measurment range used by the gyroscope sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param range The range mode to set the gyroscope to.
        * Possible values are:
        * <br>0 - +/- 250dps mode
        * <br>1 - +/- 500dps mode
        * <br>2 - +/- 2000dps mode  
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setGyroRange( /* TSS_ID */ uint id, /* unsigned char */ byte range);

        // 126(0x7e)
        /**
        * \brief Sets the measurement range used by the compass sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param range The range mode to set the compass to.
        * Possible values are:
        * <br>0 - +/- 0.88Ga mode
        * <br>1 - +/- 1.30Ga mode
        * <br>2 - +/- 1.90Ga mode
        * <br>3 - +/- 2.50Ga mode
        * <br>4 - +/- 4.00Ga mode
        * <br>5 - +/- 4.70Ga mode
        * <br>6 - +/- 5.60Ga mode
        * <br>7 - +/- 8.10Ga mode
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setCompassRange( /* TSS_ID */ uint id, /* unsigned char */ byte range);

        // 128(0x80)
        /**
        * \brief Reads the tare orientation as a quaternion.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The structure the tare oriention is written to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getTareOrientQuat( /* TSS_ID */ uint id, out Quaternion quat);

        /**
        * \brief Reads the tare orientation as a quaternion.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param quat The array of four floats the tare oriention is written to.
        * The array is structured: x, y, z, w.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getTareOrientQuatf4( /* TSS_ID */ uint id, out float quat);

        // 129(0x81)
        /**
        * \brief Reads the tare orientation as a rotation matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mat The structure the tare oriention is written to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getTareOrientMat( /* TSS_ID */ uint id, out Matrix mat);

        /**
        * \brief Reads the tare orientation as a rotation matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mat The array of nine floats the tare oriention is written to.
        * The array is in row major.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getTareOrientMatf9( /* TSS_ID */ uint id, out float mat);

        // 130(0x82)
        /**
        * \brief Reads the rho mode and rho data for the accelerometer.
        * Rho mode, min/static rho, and the max rho (if the rho mode is 'confidence') are returned.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mode The rho mode mode.
        * If the value is 0, the rho mode is 'static'.
        * If the value is 1, the rho mode is 'confidence'.
        * @param min_static If mode is static, this is the rho value.
        * If mode is confidence, this is the minimum rho value.
        * @param max If mode is static, zero will be written to this variable.
        * If mode is confidence, this is the maximum rho value.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getRhoDataAccelerometer( /* TSS_ID */
            uint id, /* unsigned char */ out byte mode, out float min_static, out float max);

        // 131(0x83)
        /**
        * \brief Reads the rho mode and rho data for the compass.
        * Rho mode, min/static rho, and the max rho (if the rho mode is 'confidence') are returned.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param mode The rho mode mode.
        * If the value is 0, the rho mode is 'static'.
        * If the value is 1, the rho mode is 'confidence'.
        * @param min_static If mode is static, this is the rho value.
        * If mode is confidence, this is the minimum rho value.
        * @param max If mode is static, zero will be written to this variable.
        * If mode is confidence, this is the maximum rho value.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getRhoDataCompass( /* TSS_ID */
            uint id, /* unsigned char */ out byte mode, out float min_static, out float max);

        // 132(0x84)
        /**
        * \brief Reads the amount of time the last frame took.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param rate The amount of time the last frame of the kalman filter took to process in microseconds is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getActualUpdateRate( /* TSS_ID */ uint id, /* unsigned int */ out uint rate);

        // 133(0x85)
        /**
        * \brief Reads the single mode compass reference vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param vec The reference vector is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getRefVecCompass( /* TSS_ID */ uint id, out Vector vec);

        /**
        * \brief Reads the single mode compass reference vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param vec The reference vector is written to the referenced array of three floats.
        * The array is formatted as such: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getRefVecCompassf3( /* TSS_ID */ uint id, out float vec);

        // 134(0x86)
        /**
        * \brief Reads the single mode accelerometer reference vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param vec The reference vector is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getRefVecAccelerometer( /* TSS_ID */ uint id, out Vector vec);

        /**
        * \brief Reads the single mode accelerometer reference vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param vec The reference vector is written to the referenced array of three floats.
        * The array is formatted as such: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getRefVecAccelerometerf3( /* TSS_ID */ uint id, out float vec);

        // 135(0x87)
        /**
        * \brief Reads the reference vector mode.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * Single static mode uses a certain reference vector for the compass and another certain reference vector for the accelerometer at all times.
        * 
        * Single auto mode uses (0,-1,0) as the reference vector for the accelerometer at all times and uses the current average angle between the accelerometer and compass to calculate the compass reference vector.
        * After that this mode acts like single static mode.
        * 
        * Single auto continuous mode uses (0,-1,0) as the reference vector for the accelerometer at all times and uses the average angle between the accelerometer and compass to constantly redetermine the compass reference vector.
        * 
        * Multi mode uses a collection of reference vectors for the compass and accelerometer, and selects which ones to use before each step of the filter.
        * 
        * @param id The identifier for the 3-Space device.
        * @param mode The reference vector mode is written to the referenced variable.
        * mode will be one of four possible values:
        * <br>0 - single static.
        * <br>1 - single auto.
        * <br>2 - single auto continuous.
        * <br>3 - multi.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getRefVecMode( /* TSS_ID */ uint id, /* unsigned char */ out byte mode);

        // 136(0x88)
        /**
        * \brief Reads the multi mode compass reference vector at index.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the desired reference vector.
        * @param vec The reference vector is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefVecCompass( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out Vector vec);

        /**
        * \brief Reads the multi mode compass reference vector at index.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the desired reference vector.
        * @param vec The reference vector is written to the referenced array of three floats.
        * The array is structured: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefVecCompassf3( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out float vec);

        // 137(0x89)
        /**
        * \brief Reads the multi mode compass reference check vector at index.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the desired reference check vector.
        * @param vec The reference check vector is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefChkVecCompass( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out Vector vec);

        /**
        * \brief Reads the multi mode compass reference check vector at index.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the desired reference check vector.
        * @param vec The reference check vector is written to the referenced array of three floats.
        * The array is structured: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefChkVecCompassf3( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out float vec);

        // 138(0x8a)
        /**
        * \brief Reads the multi mode accelerometer reference vector at index.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the desired reference vector.
        * @param vec The reference vector is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefVecAccelerometer( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out Vector vec);

        /**
        * \brief Reads the multi mode accelerometer reference vector at index.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the desired reference vector.
        * @param vec The reference vector is written to the referenced array of three floats.
        * The array is structured: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefVecAccelerometerf3( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out float vec);

        // 139(0x8b)
        /**
        * \brief Reads the multi mode accelerometer reference check vector at index.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the desired reference check vector.
        * @param vec The reference check vector is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefChkVecAccelerometer( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out Vector vec);

        /**
        * \brief Reads the multi mode accelerometer reference check vector at index.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the desired reference check vector.
        * @param vec The reference check vector is written to the referenced array of three floats.
        * The array is structured: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefChkVecAccelerometerf3( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, out float vec);

        // 140(0x8c)
        /**
        * \brief Reads the enabled state of the gyros.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param is_enabled The state of the gyro is written to the referenced variable.
        * True if the gyro is enabled.
        * False if the gyro is disabled.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getGyroEnabled( /* TSS_ID */ uint id, /* boolean/byte */ out byte is_enabled);

        // 141(0x8d)
        /**
        * \brief Reads the enabled state of the Accelerometer.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param is_enabled The state of the Accelerometer is written to the referenced variable.
        * True if the Accelerometer is enabled.
        * False if the Accelerometer is disabled.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerEnabled( /* TSS_ID */
            uint id, /* boolean/byte */ out byte is_enabled);

        // 142(0x8e)
        /**
        * \brief Reads the enabled state of the Compass.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param is_enabled The state of the Compass is written to the referenced variable.
        * True if the Compass is enabled.
        * False if the Compass is disabled.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassEnabled( /* TSS_ID */ uint id, /* boolean/byte */ out byte is_enabled);

        // 143(0x8f)
        /**
        * \brief Reads the current axis direction configuration of the 3-Space Sensor.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param axis_dir The axis direction configuration is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAxisDirections( /* TSS_ID */ uint id, out AxisDirections axis_dir);

        // 144(0x90)
        /**
        * \brief Read oversample rate.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param rate The oversample rate is written to the referenced structure.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getOversampleRate( /* TSS_ID */ uint id, /* unsigned char */ out byte rate);

        // 145(0x91)
        /**
        * \brief Reads what percentage of running average to use on the sensor's orientation.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * This is computed as follows:
        * <br>total_orient = total_orient * percent
        * <br>total_orient = total_orient + current_orient * (1 - percent)
        * <br>current_orient = total_orient
        * 
        * If the percentage is 0, the running average will be shut off completely.
        * @param id The identifier for the 3-Space device.
        * @param percent The percentage of the running average to use for the sensor's orientation.
        * Expected values are within the range 0.0 to 1.0.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAvgPercent( /* TSS_ID */ uint id, out float percent);

        // 146(0x92)
        /**
        * \brief Reads the current desired update rate.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * If the filter takes less time to update during a given frame than this rate specifies, the frame will be padded out to take the desired amount of time.
        * If the filter takes more time to update than this rate, this rate will be ignored.
        * 
        * @param id The identifier for the 3-Space device.
        * @param rate The amount of time desired between each frame of kalman filter calculations in microseconds is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getDesiredUpdateRate( /* TSS_ID */ uint id, /* unsigned int */ out uint rate);

        // 147(0x93)
        /**
        * \brief Read Kalman filter's covariance 3x3 matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param mat The covariance matrix is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getKalmanMat( /* TSS_ID */ uint id, out Matrix mat);

        /**
        * \brief Read Kalman filter's covariance 3x3 matrix.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param mat The covariance matrix is written to the referenced array of nine floats.
        * The floats in the array are ordered in row-major order.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getKalmanMatf9( /* TSS_ID */ uint id, out float mat);

        // 148(0x94)
        /**
        * \brief Read accelerometer sensitivity range.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * Higher ranges can detect and report larger accelerations, but are not as accurate for smaller ones.
        * @param id The identifier for the 3-Space device.
        * @param range The intended range for the accelerometer.
        * Expected values are:
        * <br>0 - 2G.
        * <br>16 - 4G.
        * <br>48 - 8G.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerRange( /* TSS_ID */ uint id, /* unsigned char */ out byte range);

        // 149(0x95)
        /**
        * \brief Get the weighting power for multi reference vector weights.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * Multi reference vector weights are all raised to the weight power before they are summed and used in the calculation for the final reference vector.
        * Setting this value nearer to 0 will cause the reference vectors to overlap more, and setting it nearer to infinity will cause the reference vectors to influence a smaller set of orientations.
        * @param id The identifier for the 3-Space device.
        * @param pow The weight power to use.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefWeightPwr( /* TSS_ID */ uint id, out float pow);

        // 150(0x96)
        /**
        * \brief Reads number of cell divisions and number of nearby vectors per cell for the multi reference lookup table.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * By default, the multiple reference vector mode only deals with orientations obtainable by successive rotations of 90 degrees about any of the three axes.
        * This command can adjust it to accept orientations obtainable by 45 degree rotations. For 90 degrees, give a "number of cell divisions" of 4, and for 45 give 8.
        * In addition, the number of vectors near to any given orientation which the scheme will check can be adjusted as well.
        * If this value is 0, only the nearest orientation will be checked.
        * The largest this value can be is 32.
        * Also note that the larger this value is, the longer the multiple reference vector mode will take to run each cycle.
        * @param id The identifier for the 3-Space device.
        * @param num_division The number of cell divisions is written to the referenced variable.
        * @param num_vecs The number of nearby vectors is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMultiRefResolution( /* TSS_ID */
            uint id, /* unsigned char */ out byte num_division, /* unsigned char */ out byte num_vecs);

        // 151(0x97)
        /**
        * \brief Read number of multi reference cells.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param num_cells The number of multi reference cells is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getNumMultiRefCells( /* TSS_ID */ uint id, /* unsigned int */ out uint num_cells);

        // 152(0x98)
        /**
        * \brief Read whether the Kalman Filter is enabled.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param is_enabled The Kalam Filter's state is written to the referenced variable.
        * True if the filter is enabled.
        * False if the filter is disabled.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getFilterMode( /* TSS_ID */ uint id, /* boolean/byte */ out byte is_enabled);

        // 153(0x99)
        /**
        * \brief Reads the mode that the running-average method uses.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * 'Normal' uses a static running-average.
        * 'Confidence' uses a running average that changes dynamically based upon the confidence factor.
        * @param id The identifier for the 3-Space device.
        * @param mode The sensor's running average mode is written to the referenced variable.        
        * Possible values are:
        * <br>0 - Normal mode
        * <br>1 - Confidence Mode
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getRunningAvgMode( /* TSS_ID */ uint id, /* unsigned char */ out byte mode);

        // 154(0x9a)
        /**
        * \brief Reads the current operation range of the 3-Space Sensor's gyroscope.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param range The range mode of the gyroscope is written to the referenced variable.
        * Expected values are:
        * <br>0 - +/- 250dps mode
        * <br>1 - +/- 500dps mode
        * <br>2 - +/- 2000dps mode
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getGyroRange( /* TSS_ID */ uint id, /* unsigned char */ out byte range);

        // 155(0x9b)
        /**
        * \brief Reads the current operation range of the 3-Space Sensor's compass.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param range The range mode of the compass is written to the referenced variable.
        * Expected values are:
        * <br>0 - +/- 0.88Ga mode
        * <br>1 - +/- 1.30Ga mode
        * <br>2 - +/- 1.90Ga mode
        * <br>3 - +/- 2.50Ga mode
        * <br>4 - +/- 4.00Ga mode
        * <br>5 - +/- 4.70Ga mode
        * <br>6 - +/- 5.60Ga mode
        * <br>7 - +/- 8.10Ga mode
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassRange( /* TSS_ID */ uint id, /* unsigned char */ out byte range);

        // 160(0xa0)
        /**
        * \brief Sets the compass calibration parameters to the given values.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by.
        * @param bias The vector indicating the bias of the calibration.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setCompassCalibrationParam( /* TSS_ID */ uint id, Matrix mat, Vector bias);

        /**
        * \brief Sets the compass calibration parameters to the given values.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by as an array of nine floats.
        * The floats should be arranged in row-major order.
        * @param bias The vector indicating the bias of the calibration as an array of three floats.
        * The floats should be arranged as: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setCompassCalibrationParamf9f3( /* TSS_ID */
            uint id, out float mat, out float bias);

        // 161(0xa1)
        /**
        * \brief Sets the accelerometer calibration parameters to the given values.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by.
        * @param bias The vector indicating the bias of the calibration.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setAccelerometerCalibrationParam( /* TSS_ID */
            uint id, Matrix mat, Vector bias);

        /**
        * \brief Sets the accelerometer calibration parameters to the given values.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by as an array of nine floats.
        * The floats should be arranged in row-major order.
        * @param bias The vector indicating the bias of the calibration as an array of three floats.
        * The floats should be arranged as: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setAccelerometerCalibrationParamf9f3( /* TSS_ID */
            uint id, out float mat, out float bias);

        // 162(0xa2)
        /**
        * \brief Gets the compass calibration parameters.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by is written to the referenced variable.
        * @param bias The bias of the calibration is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassCalibrationParam( /* TSS_ID */
            uint id, out Matrix mat, out Vector bias);

        /**
        * \brief Gets the compass calibration parameters.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by is written to the referenced array of nine floats.
        * The array is arranged in row-major order.
        * @param bias The bias of the calibration is written to the referenced array of three floats.
        * The array is arranged as: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getCompassCalibrationParamf9f3( /* TSS_ID */
            uint id, out float mat, out float bias);

        // 163(0xa3)
        /**
        * \brief Gets the accelerometer calibration parameters.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by is written to the referenced variable.
        * @param bias The bias of the calibration is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerCalibrationParam( /* TSS_ID */
            uint id, out Matrix mat, out Vector bias);

        /**
        * \brief Gets the accelerometer calibration parameters.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by is written to the referenced array of nine floats.
        * The array is arranged in row-major order.
        * @param bias The bias of the calibration is written to the referenced array of three floats.
        * The array is arranged as: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAccelerometerCalibrationParamf9f3( /* TSS_ID */
            uint id, out float mat, out float bias);

        // 164(0xa4)
        /**
        * \brief Gets the gyro calibration parameters.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by is written to the referenced variable.
        * @param bias The bias of the calibration is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getGyroCalibrationParam( /* TSS_ID */
            uint id, out Matrix mat, out Vector bias);

        /**
        * \brief Gets the gyro calibration parameters.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by is written to the referenced array of nine floats.
        * The array is arranged in row-major order.
        * @param bias The bias of the calibration is written to the referenced array of three floats.
        * The array is arranged as: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getGyroCalibrationParamf9f3( /* TSS_ID */ uint id, out float mat, out float bias);

        // 165(0xa5)
        /**
        * \brief Puts the sensor in gyro calibration mode.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * It will collect a few frames of data from the gyro to determine its bias.
        * It will return to normal operation after this or if the sensor is reset.
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum calibrateGyro( /* TSS_ID */ uint id);

        // 166(0xa6)
        /**
        * \brief Sets the gyro calibration parameters to the given values.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by.
        * @param bias The vector indicating the bias of the calibration.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setGyroCalibrationParam( /* TSS_ID */ uint id, Matrix mat, Vector bias);

        /**
        * \brief Sets the gyro calibration parameters to the given values.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * These consist of a bias which is applied to the raw data as a translation, and a matrix by which the value is multiplied by.
        * @param id The identifier for the 3-Space device.
        * @param mat The matrix to multiply the raw data by as an array of nine floats.
        * The floats should be arranged in row-major order.
        * @param bias The vector indicating the bias of the calibration as an array of three floats.
        * The floats should be arranged as: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setGyroCalibrationParamf9f3( /* TSS_ID */ uint id, out float mat, out float bias);

        // 167(0xa7)
        /**
        * \brief Set a vector entry in a 3-Space Sensor's vertex look-up table.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * This function is an experimental calibration function that is not guarenteed to be supported.
        * @param id The identifier for the 3-Space device.
        * @param type The type of on-board sensor data to log.
        * Possible values are:
        * <br>0 - Compass
        * <br>1 - Accelerometer
        * @param idx The index into the lookup table to write to.
        * The value may range from 0 to 1352.
        * @param val The vector to store in the table.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setLookupTblVertVal( /* TSS_ID */
            uint id, /* unsigned char */ byte type, /* unsigned short */ ushort idx, Vector val);

        /**
        * \brief Set a vector entry in a 3-Space Sensor's vertex look-up table.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * This function is an experimental calibration function that is not guarenteed to be supported.
        * @param id The identifier for the 3-Space device.
        * @param type The type of on-board sensor data to log.
        * Possible values are:
        * <br>0 - Compass
        * <br>1 - Accelerometer
        * @param idx The index into the lookup table to write to.
        * The value may range from 0 to 1352.
        * @param val The vector to store in the table as an array of three floats.
        * The array should be arranged: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setLookupTblVertValf3( /* TSS_ID */
            uint id, /* unsigned char */ byte type, /* unsigned short */ ushort idx, out float val);

        // 168(0xa8)
        /**
        * \brief Reads a vector entry in a 3-Space Sensor's vertex look-up table.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * This function is an experimental calibration function that is not guarenteed to be supported.
        * @param id The identifier for the 3-Space device.
        * @param type The type of on-board sensor data to log.
        * Possible values are:
        * <br>0 - Compass
        * <br>1 - Accelerometer
        * @param idx The index into the lookup table to write to.
        * The value may range from 0 to 1352.
        * @param val The vector stored in the table will be written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getLookupTblVertVal( /* TSS_ID */
            uint id, /* unsigned char */ byte type, /* unsigned short */ ushort idx, out Vector val);

        /**
        * \brief Reads a vector entry in a 3-Space Sensor's vertex look-up table.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * This function is an experimental calibration function that is not guarenteed to be supported.
        * @param id The identifier for the 3-Space device.
        * @param type The type of on-board sensor data to log.
        * Possible values are:
        * <br>0 - Compass
        * <br>1 - Accelerometer
        * @param idx The index into the lookup table to write to.
        * The value may range from 0 to 1352.
        * @param val The vector stored in the table will be written to the referenced array of three floats.
        * The array will be arranged: x, y, z.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getLookupTblVertValf3( /* TSS_ID */
            uint id, /* unsigned char */ byte type, /* unsigned short */ ushort idx, out float val);

        // 192(0xc0)
        /**
        * \brief Reads the Pan ID for the 3-Space Device.
        * This call will error if the inputted id is not for a 3-Space Dongle or 3-Space Wireless Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param pan_id The Pan ID of the 3-Space device is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getPanID( /* TSS_ID */ uint id, /* unsigned short */ out ushort pan_id);

        // 193(0xc1)
        /**
        * \brief Sets the Pan ID for the 3-Space Device.
        * This call will error if the inputted id is not for a 3-Space Dongle or 3-Space Wireless Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param pan_id The Pan ID to set to the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setPanID( /* TSS_ID */ uint id, /* unsigned short */ ushort pan_id);

        // 194(0xc2)
        /**
        * \brief Reads the wireless channel for the 3-Space Device.
        * This call will error if the inputted id is not for a 3-Space Dongle or 3-Space Wireless Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param channel The wireless channel of the 3-Space device is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getChannel( /* TSS_ID */ uint id, /* unsigned char */ out byte channel);

        // 195(0xc3)
        /**
        * \brief Sets the wireless channel for the 3-Space Device.
        * This call will error if the inputted id is not for a 3-Space Dongle or 3-Space Wireless Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param channel The wireless channel to set to the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setChannel( /* TSS_ID */ uint id, /* unsigned char */ byte channel);

        // 196(0xc4)
        /**
        * \brief Sets the mode of the device's LED.
        * This call will error if the inputted id is not for a 3-Space Dongle, 3-Space Wireless Sensor, or a 3-Space Bluetooth Sensor.
        *
        * The LED has two possible mode, 'static' and standard.
        * <ul>
        * <li>If the LED is in 'static' mode, this means that it will only display the color set by the command setLEDColor.
        * <li>If the LED is in 'standard' mode, it will display the standard LED colors as described below:
        * <ul>
        * <li>Upon receipt of a packet, the dongle will flash green temporarily.
        * <li>If the dongle transmits a packet that does not reach its destination, the dongle will flash red temporarily.
        * </ul></ul>
        * @param id The identifier for the 3-Space device.
        * @param mode The LED mode to set the device to.
        * If 0, the dongle will be set to 'standard' LED mode.
        * If 1, the dongle will be set to 'static' LED mode.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setLEDMode( /* TSS_ID */ uint id, /* unsigned char */ byte mode);

        // 197(0xc5)
        /**
        * \brief Commits wireless configuration settings to the device's non-volitile memory.
        * This call will error if the inputted id is not for a 3-Space Dongle or 3-Space Wireless Sensor.
        *
        * The settings committed with this command are:
        * <ul>
        * <li>PanID
        * <li>Wireless Channel
        * <li>Wireless Address
        * </ul>
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum commitWirelessSettings( /* TSS_ID */ uint id);

        // 198(0xc6)
        /**
        * \brief Reads the wireless address of the device.
        * This call will error if the inputted id is not for a 3-Space Dongle or 3-Space Wireless Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param address The device's wireless address will be written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getWirelessAddress( /* TSS_ID */ uint id, /* unsigned short */ out ushort address);

        // 199(0xc7)
        /**
        * \brief Sets the wireless address of the device.
        * This call will error if the inputted id is not for a 3-Space Dongle or 3-Space Wireless Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param address The wireless address to set the device to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setWirelessAddress( /* TSS_ID */ uint id, /* unsigned short */ ushort address);

        // 200(0xc8)
        /**
        * \brief Reads the mode of the device's LED.
        * This call will error if the inputted id is not for a 3-Space Dongle, 3-Space Wireless Sensor, or a 3-Space Bluetooth Sensor.
        *
        * The LED has two possible mode, 'static' and standard.
        * <ul>
        * <li>If the LED is in 'static' mode, this means that it will only display the color set by the command setLEDColor.
        * <li>If the LED is in 'standard' mode, it will display the standard LED colors as described below:
        * <ul>
        * <li>Upon receipt of a packet, the dongle will flash green temporarily.
        * <li>If the dongle transmits a packet that does not reach its destination, the dongle will flash red temporarily.
        * </ul></ul>
        * @param id The identifier for the 3-Space device.
        * @param mode The device's LED mode will be written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getLEDMode( /* TSS_ID */ uint id, /* unsigned char */ out byte mode);

        // 201(0xc9)
        /**
        * \brief Reads the current battery voltage for a 3-Space Sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor, 3-Space Data-Logging Sensor, or a 3-Space Bluetooth Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param bat_volt The device's battery voltage will be written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getBatteryVoltage( /* TSS_ID */ uint id, out float bat_volt);

        // 202(0xca)
        /**
        * \brief Reads the current battery life left for a 3-Space Sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor, 3-Space Data-Logging Sensor, or a 3-Space Bluetooth Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param bat_life The percentage of the device's battery life remaining will be written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getBatteryLife( /* TSS_ID */ uint id, /* unsigned short */ out ushort bat_life);

        // 203(0xcb)
        /**
        * \brief Reads a status number indicating the state of the 3-Space Sensor's battery.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor, 3-Space Data-Logging Sensor, or a 3-Space Bluetooth Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param bat_status The current status of the battery is written to the refernced variable. 
        * Expected values are:
        * <ul>
        * <li>1 represents fully charged.
        * <li>2 represents charging.
        * </ul>
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getBatteryStatus( /* TSS_ID */ uint id, /* unsigned char */ out byte bat_status);

        // 208(0xd0)
        /**
        * \brief Reads the hardware ID (sensor serial number) that is associated with the logical ID given by an index.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the dongle's wireless table to read the hardware ID from. 
        * @param hw_id The serial number of the wireless sensor currently listed in the dongle's wireless table under idx is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getWirelessHWID( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, /* unsigned int */ out uint hw_id);

        // 209(0xd1)
        /**
        * \brief Sets the logical ID given by an index to the hardware ID (sensor serial number) given by hw_id.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param idx The index of the dongle's wireless table to read the hardware ID from. 
        * @param hw_id The serial number of the wireless sensor being written to the dongle's wireless table.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setWirelessHWID( /* TSS_ID */
            uint id, /* unsigned char */ byte idx, /* unsigned int */ uint hw_id);

        // 210(0xd2)
        /**
        * \brief Returns a char for each channel representing the level of noise on each respective channel.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param channel_noise The noise of each wireless channel as an array of sixteen chars.
        * Each of the chars' values range from 0...255 with 0 meaning the channel is completely clear, and 255 meaning that it is unusably noisy.
        * The first element of the array corresponds to channel 11, and the last value corresponds to channel 26.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAllChannelNoise( /* TSS_ID */
            uint id, /* unsigned char */ out byte channel_noise);

        // 211(0xd3)
        /**
        * \brief Set the number of times the dongle will retry a failed transmission to a wireless sensor.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param retries The desired number of times to retry a transmission to a wireless sensor before giving up.
        * The maximum value, as well as the default value, is 3.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setWirelessRetries( /* TSS_ID */ uint id, /* unsigned char */ byte retries);

        // 212(0xd4)
        /**
        * \brief Reads the number of times the dongle will retry a failed transmission to a wireless sensor.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param retries The number of times to retry a transmission to a wireless sensor before giving up is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getWirelessRetries( /* TSS_ID */ uint id, /* unsigned char */ out byte retries);

        // 213(0xd5)
        /**
        * \brief Reads how many more commands slots are available in the dongle's wireless send queue.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * All commands sent to the dongle to be transmitted wirelessly are buffered until previous pending transmissions complete.
        * There are fifteen such slots, and while the dongle handles the management of them internally, no additional transmissions can be queued up if the maximum number of slots are occupied already.
        * If sending a large batch of commands to a single sensor, it is useful to check this to make sure the transmission can be initiated.
        * @param id The identifier for the 3-Space device.
        * @param slots The number of available wireless slots is written to the referenced variable.
        * The maximum number of slots possible is 15.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getWirelessSlotsOpen( /* TSS_ID */ uint id, /* unsigned char */ out byte slots);

        // 214(0xd6)
        /**
        * \brief Reads the relative strength of the most recent packet received by the dongle.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * Reads a value indicating the relative strength of the most recent packet received by the dongle.
        * The value scales linearly with distance from the dongle.
        * Low values do not necessarily mean that transmissions are less accurate, but rather the strength of the signal that transmitted the last packet was weak.
        * @param id The identifier for the 3-Space device.
        * @param recep_strg The signal strength of the last wireless packet recieved from a wireless sensor is written to the referenced variable.
        * The value ranges from 0 to 255.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getReceptionStrength( /* TSS_ID */ uint id, out float recep_strg);

        // 215(0xd7)
        /**
        * \brief Sets the update rate of the mouse and joystick HID devices.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param rate_ms The intended update rate of HID device polling in milliseconds.
        * The minimum valid value is 5 milliseconds, while the maximum valid value is 50 milliseconds.
        * The dongle's default rate is 25.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setHIDUpdateRate( /* TSS_ID */ uint id, /* unsigned char */ byte rate_ms);

        // 216(0xd8)
        /**
        * \brief Reads the update rate of the mouse and joystick HID devices.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param rate_ms The update rate of HID device polling in milliseconds is written to the referenced variable.
        * The minimum valid value is 5 milliseconds, while the maximum valid value is 50 milliseconds.
        * The dongle's default rate is 25.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getHIDUpdateRate( /* TSS_ID */ uint id, /* unsigned char */ out byte rate_ms);

        // 223(0xdf)
        /**
        * \brief Reads the version of the currenly installed firmware on the 3-Space device.
        *
        * @param id The identifier for the 3-Space device.
        * @param version The 3-Space device's firmware version is written as a char array string of length 13 (counting the NULL terminator).
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getSoftwareVersion( /* TSS_ID */ uint id, out byte version);

        // 224(0xe0)
        /**
        * \brief Restores all settings to factory settings.
        *
        * The settings are not committed to non-volatile memory by this command, so the commit settings command will have to be used if this is desired.
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum restoreFactorySettings( /* TSS_ID */ uint id);

        // 225(0xe1)
        /**
        * \brief Commits settings to non-volatile memory.
        *
        * This will cause them to persist even if the sensor is reset.
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum commitSettings( /* TSS_ID */ uint id);

        // 226(0xe2)
        /**
        * \brief Resets the device without powering it down.
        *
        * Any settings saved to non-volatile memory will be restored and the sensor.
        * During the reset, the sensor will be inactive for approximatly 5 seconds.
        * Certain setting changes only take place when a reset has occurred.
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum softwareReset( /* TSS_ID */ uint id);

        // 227(0xe3)
        /**
        * \brief Enables the watchdog timer with the given timeout rate.
        *
        * If a frame takes more than this amount of time, the sensor will automatically reset.
        * This is useful for dealing with sensor hangs or crashes, as the sensor would reset and continue normal operation.
        * @param id The identifier for the 3-Space device.
        * @param timeout The length of time in microseconds to wait before timing out when the watchdog timer is active.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum enableWatchdogTimer( /* TSS_ID */ uint id, /* unsigned int */ uint timeout);

        // 228(0xe4)
        /**
        * \brief Disables the watchdog timer.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum disableWatchdogTimer( /* TSS_ID */ uint id);

        // 229(0xe5)
        /**
        * \brief Puts the sensor into firmware update mode.
        *
        * This will cease normal operation until the firmware update mode is instructed to return the sensor to normal operation.
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum enterFirmwareUpdateMode( /* TSS_ID */ uint id);

        // 230(0xe6)
        /**
        * \brief Reads the version and type of hardware being used on the 3-Space device.
        *
        * @param id The identifier for the 3-Space device.
        * @param version The 3-Space device's hardware version is written as a char array string of length 33 (counting the NULL terminator).
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getHardwareVersion( /* TSS_ID */ uint id, out byte version);

        // 231(0xe7)
        /**
        * \brief Sets the baud rate of the physical UART.
        * This call will error if the inputted id is not for a 3-Space USB Sensor or a 3-Space Embedded Sensor.
        *
        * This setting does not need to be committed, but does not take effect until the sensor is reset.
        * The baud rate will be set to the valid value nearest the requested value.
        * @param id The identifier for the 3-Space device.
        * @param rate The desired baud rate of the UART.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setUARTRate( /* TSS_ID */ uint id, /* unsigned int */ uint rate);

        // 232(0xe8)
        /**
        * \brief Reads the baud rate of the physical UART.
        * This call will error if the inputted id is not for a 3-Space USB Sensor or a 3-Space Embedded Sensor.
        *
        * If the UART rate has been changed, but the sensor has not been reset, the read rate will reflect the newly set rate as opposed to the current operating rate.
        * @param id The identifier for the 3-Space device.
        * @param rate The current set baud rate of the UART will be written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getUARTRate( /* TSS_ID */ uint id, /* unsigned int */ out uint rate);

        // 233(0xe9)
        /**
        * \brief Sets the communication mode for USB.
        *
        * All modes present a COM port with which to communicate with the USB device.
        * FTDI and CDC each present a regular numbered port, whereas Unique presents a port named YEI_<serial number>.
        * The sensor will change modes immediately.
        * @param id The identifier for the 3-Space device.
        * @param mode The desired USB mode for the sensor. 
        * Valid values are: 
        * <ul>
        * <li>0 for CDC mode.
        * <li>1 for FTDI mode.
        * <li>2 for Unique mode.
        * </ul>
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setUSBMode( /* TSS_ID */ uint id, /* unsigned char */ byte mode);

        // 234(0xea)
        /**
        * \brief Gets the communication mode for USB.
        *
        * All modes present a COM port with which to communicate with the USB device.
        * FTDI and CDC each present a regular numbered port, whereas Unique presents a port named YEI_<serial number>.
        * The sensor will change modes immediately.
        * @param id The identifier for the 3-Space device.
        * @param mode The USB mode for the sensor is written to the referenced variable. 
        * Valid values are: 
        * <ul>
        * <li>0 for CDC mode.
        * <li>1 for FTDI mode.
        * <li>2 for Unique mode.
        * </ul>
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getUSBMode( /* TSS_ID */ uint id, /* unsigned char */ out byte mode);

        // 235(0xeb)
        /**
        * \brief Sets the clock speed of the device's MCU to the given value.
        *
        * @param id The identifier for the 3-Space device.
        * @param speed The desired clock speed of the MCU in Hz. 
        * As an example, if one desired to set the clock speed to 15 MHz, the speed argument should be set to 15000000. 
        * Valid values are: 
        * <ul>
        * <li>15000000 (15 MHz).
        * <li>30000000 (30 MHz).
        * <li>60000000 (60 MHz).
        * </ul>
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setClockSpeed( /* TSS_ID */ uint id, /* unsigned int */ uint speed);

        // 236(0xec)
        /**
        * \brief Reads the clock speed of the device's MCU.
        *
        * @param id The identifier for the 3-Space device.
        * @param speed The clock speed of the MCU in Hz is written to the referenced variable. 
        * Expected values are: 
        * <ul>
        * <li>15000000 (15 MHz).
        * <li>30000000 (30 MHz).
        * <li>60000000 (60 MHz).
        * </ul>
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getClockSpeed( /* TSS_ID */ uint id, /* unsigned int */ out uint speed);

        // 237(0xed)
        /**
        * \brief Reads the serial number of the sensor.
        *
        * The serial number is a unique number for any one sensor.
        * @param id The identifier for the 3-Space device.
        * @param serial The serial number of the 3-Space device is written to the referenced variable. 
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getSerialNumber( /* TSS_ID */ uint id, /* unsigned int */ out uint serial);

        // 238(0xee)
        /**
        * \brief Sets the color of the LED on the sensor to the given RGB color.
        *
        * @param id The identifier for the 3-Space device.
        * @param color The color the device's LED is being set to.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setLEDColor( /* TSS_ID */ uint id, Color color);

        /**
        * \brief Sets the color of the LED on the sensor to the given RGB color.
        *
        * @param id The identifier for the 3-Space device.
        * @param color The color the device's LED is being set to as an array of three floats.
        * Each float in the array has an expected range of 0.0 (no color contribution) to 1.0 (greatest color contribution).
        * The first float is the red channel, the second the green channel, and the third float is the blue channel. (red, green, blue)
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setLEDColorf3( /* TSS_ID */ uint id, out float color);

        // 239(0xef)
        /**
        * \brief Reads the color of the LED on the sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param color The color of the device's LED is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getLEDColor( /* TSS_ID */ uint id, out Color color);

        /**
        * \brief Reads the color of the LED on the sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @param color The color of the device's LED is written to an array of three floats.
        * Each float in the array has an expected range of 0.0 (no color contribution) to 1.0 (greatest color contribution).
        * The first float is the red channel, the second the green channel, and the third float is the blue channel. (red, green, blue)
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getLEDColorf3( /* TSS_ID */ uint id, out float color);

        // 240(0xf0)
        /**
        * \brief Turns the data feed to the joystick on and off.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * If disabled, the sensor will still enumerate as a joystick, but the joystick will not function.
        * This allows normal communication to occur at a faster rate.
        * @param id The identifier for the 3-Space device.
        * @param do_enabled If True, the joystick is enabled.
        * If False, the joystick is disabled.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setJoystickEnabled( /* TSS_ID */ uint id, /* boolean/byte */ byte do_enabled);

        /**
        * \brief Set the logical ID of the wireless unit will operate as the joystick or disables joystick data transmission.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        * 
        * Disabling joystick data transmission allows normal communication to occur at a faster rate.
        * @param id The identifier for the 3-Space device.
        * @param logical_id The logical ID (index in the dongle's wireless hardware table) associated with the wireless sensor that is to act as the data source for joystick HID streaming.
        * If this value is set to -1, there will be no joystick input from any wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setJoystickLogicalID( /* TSS_ID */ uint id, /* unsigned char */ byte logical_id);

        // 241(0xf1)
        /**
        * \brief Turns the data feed to the mouse on and off.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * If disabled, the sensor will still enumerate as a mouse, but the mouse will not function.
        * This allows normal communication to occur at a faster rate.
        * @param id The identifier for the 3-Space device.
        * @param do_enabled If True, the mouse is enabled.
        * If False, the mouse is disabled.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMouseEnabled( /* TSS_ID */ uint id, /* boolean/byte */ byte do_enabled);

        /**
        * \brief Set the logical ID of the wireless unit will operate as the mouse or disables mouse data transmission.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        * 
        * Disabling mouse data transmission allows normal communication to occur at a faster rate.
        * @param id The identifier for the 3-Space device.
        * @param logical_id The logical ID (index in the dongle's wireless hardware table) associated with the wireless sensor that is to act as the data source for mouse HID streaming.
        * If this value is set to -1, there will be no mouse input from any wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMouseLogicalID( /* TSS_ID */ uint id, /* unsigned char */ byte logical_id);

        // 242(0xf2)
        /**
        * \brief Reads the enabled status for the HID Joystick for a 3-Space device.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param is_enabled The joystick state is written to the referenced variable.
        * If True, the joystick is enabled.
        * If False, the joystick is disabled.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getJoystickEnabled( /* TSS_ID */ uint id, /* boolean/byte */ out byte is_enabled);

        /**
        * \brief Reads the logical ID of the wireless unit will operate as the joystick.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param logical_id The logical ID (index in the dongle's wireless hardware table) associated with the wireless sensor that is to act as the data source for joystick HID streaming is written to the referenced variable.
        * If this value is set to -1, there will be no joystick input from any wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getJoystickLogicalID( /* TSS_ID */
            uint id, /* unsigned char */ out byte logical_id);

        // 243(0xf3)
        /**
        * \brief Turns the data feed to the mouse on and off.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * If disabled, the sensor will still enumerate as a mouse, but the mouse will not function.
        * This allows normal communication to occur at a faster rate.
        * @param id The identifier for the 3-Space device.
        * @param is_enabled The mouse state is written to the referenced variable.
        * If True, the mouse is enabled.
        * If False, the mouse is disabled.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMouseEnabled( /* TSS_ID */ uint id, /* boolean/byte */ out byte is_enabled);

        /**
        * \brief Reads the logical ID of the wireless unit will operate as the mouse.
        * This call will error if the inputted id is not for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param logical_id The logical ID (index in the dongle's wireless hardware table) associated with the wireless sensor that is to act as the data source for mouse HID streaming is written to the referenced variable.
        * If this value is set to -1, there will be no mouse input from any wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMouseLogicalID( /* TSS_ID */ uint id, /* unsigned char */ out byte logical_id);

        // 244(0xf4)
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setControlMode( /* TSS_ID */
            uint id, /* unsigned char */ byte ctrl_class, /* unsigned char */ byte ctrl_idx, /* unsigned char */
            byte hdl_idx);

        // 245(0xf5)
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setControlData( /* TSS_ID */
            uint id, /* unsigned char */ byte ctrl_class, /* unsigned char */ byte ctrl_idx, /* unsigned char */
            byte data_idx, float data);

        // 246(0xf6)
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getControlMode( /* TSS_ID */
            uint id, /* unsigned char */ byte ctrl_class, /* unsigned char */ byte ctrl_idx, /* unsigned char */
            out byte hdl_idx);

        // 247(0xf7)
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getControlData( /* TSS_ID */
            uint id, /* unsigned char */ byte ctrl_class, /* unsigned char */ byte ctrl_idx, /* unsigned char */
            byte data_idx, out float data);

        // 248(0xf8)
        /**
        * \brief Sets how long, in frames, the gyros should be disabled after one of the physical buttons on the sensor is pressed.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * This setting helps to alleviate gyro disturbances caused by the buttons causing small shockwaves in the sensor.
        * @param id The identifier for the 3-Space device.
        * @param length How many frames to disable the gyros for when a button is pressed.
        * A setting of 0 means the gyros won't be disabled at all.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setButtonGyroDisableLength( /* TSS_ID */ uint id, /* unsigned char */ byte length);

        // 249(0xf9)
        /**
        * \brief Reads how long, in frames, the gyros are disabled after one of the physical buttons on the sensor is pressed.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * This setting helps to alleviate gyro disturbances caused by the buttons causing small shockwaves in the sensor.
        * @param id The identifier for the 3-Space device.
        * @param length How many frames the gyros is disabled for when a button is pressed is written to the referenced variable.
        * A setting of 0 means the gyros won't be disabled at all.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getButtonGyroDisableLength( /* TSS_ID */
            uint id, /* unsigned char */ out byte length);

        // 250(0xfa)
        /**
        * \brief Reads the current state of the sensor's physical buttons.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * @param id The identifier for the 3-Space device.
        * @param cur_state The 3-Space device's button state is written to the referenced variable.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getButtonState( /* TSS_ID */ uint id, out ButtonState cur_state);

        // 251(0xfb)
        /**
        * \brief Puts the emulated HID mouse in absolute or relative mode.
        * 
        * Please note that this change does not take effect immediately, and the sensor's settings must be committed and the sensor must be reset before the mouse will enter this mode.
        * @param id The identifier for the 3-Space device.
        * @param do_relative Sets whether the mouse is to be relative or not.
        * If the value is True, the mouse is relative.
        * If the value is False, the mouse is in absolute mode.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setMouseRelative( /* TSS_ID */ uint id, /* boolean/byte */ byte do_relative);

        // 252(0xfc)
        /**
        * \brief Reads the current emulated HID mouse absolute/relative state.
        * 
        * Note that if the sensor has not been reset since it has been put in this state, the mouse will not reflect this change yet, even though this command will.
        * @param id The identifier for the 3-Space device.
        * @param is_relative The absolute/relative state of the mouse is written to the referenced variable.
        * If the value is True, the mouse is relative.
        * If the value is False, the mouse is in absolute mode.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getMouseRelative( /* TSS_ID */ uint id, /* boolean/byte */ out byte is_relative);

        // 253(0xfd)
        /**
        * \brief Sets whether the joystick and mouse are present or removed.
        * 
        * If removed, they will not show up as devices on the target system at all.
        * For these changes to take effect, the sensor driver may need to be reinstalled.
        * @param id The identifier for the 3-Space device.
        * @param do_joy_present States whether the joystick is present on the system at all.
        * If True, the joystick is present.
        * If False, the joystick is not present.
        * @param do_mouse_present States whether the mouse is present on the system at all.
        * If True, the mouse is present.
        * If False, the mouse is not present.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setJoystickMousePresent( /* TSS_ID */
            uint id, /* boolean/byte */ byte do_joy_present, /* boolean/byte */ byte do_mouse_present);

        // 254(0xfe)
        /**
        * \brief Reads whether the joystick and mouse are present or removed.
        * 
        * If removed, they will not show up as devices on the target system at all.
        * @param id The identifier for the 3-Space device.
        * @param is_joy_present The state indicating whether the joystick is present on the system at all or not is written to the referenced variable.
        * If True, the joystick is present.
        * If False, the joystick is not present.
        * @param is_mouse_present The state indicating whether the mouse is present on the system at all or not is written to the referenced variable.
        * If True, the mouse is present.
        * If False, the mouse is not present.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getJoystickMousePresent( /* TSS_ID */
            uint id, /* boolean/byte */ out byte is_joy_present, /* boolean/byte */ out byte is_mouse_present);

        // Functions to replace commands 244(0xf4) and 245(0xf5)
        /**
        * \brief Sets an axis of the desired emulated input device as a 'Global Axis' style axis.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * Axis operating under this style use a reference vector and a consitent local vector to determine the state of the device's axis.
        * As the local vector rotates, it is projected onto the global vector.
        * Once the distance of that projection on the global vector exceeds the inputted "deadzone", the device will begin tranmitting non-zero values for the device's desired axis.
        * @param id The identifier for the 3-Space device.
        * @param joy_or_mouse A null-terminated string whose value may be either 'mouse' or 'joystick'.
        * This string defines whether the device in question is a mouse or joystick.
        * @param device_axis A null-terminated string whose value may be either 'X' or 'Y'.
        * This string defines what axis of the device is to be configured.
        * @param local_axis A normalized vector representing the sensor's local vector to track.
        * @param global_axis A normalized vector representing the global vector to project the local vector onto (should be orthoginal to the local vector).
        * @param deadzone Defines the minimum distance necissary for the device's axis to read a non-zero value.
        * @param scale Defines the linear scale for the values being returned for the axis.
        * @param power An exponental power used to further modify data being returned from the sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setGlobalAxis( /* TSS_ID */
            uint id, out byte joy_or_mouse, out byte device_axis, Vector local_axis, Vector global_axis,
            float deadzone, float scale, float power);

        /**
        * \brief Sets an axis of the desired emulated input device as a 'Screen Point Axis' style axis.
        * This call will error if the inputted id is for a 3-Space Dongle.
        * 
        * An axis operating under this style projects a vector along the sensor's direction vector into a mathmatical plane.
        * The collision point on the plane is then used to determine what the device's axis's current value is.
        * The direction vector is rotated based on the orientation of the sensor.
        * @param id The identifier for the 3-Space device.
        * @param joy_or_mouse A null-terminated string whose value may be either 'mouse' or 'joystick'.
        * This string defines whether the device in question is a mouse or joystick.
        * @param device_axis A null-terminated string whose value may be either 'X' or 'Y'.
        * This string defines what axis of the device is to be configured.
        * @param dist_from_screen A float whose value is the real world distance the sensor is from the user's screen.
        * Must be the same units as dist_on_axis.
        * @param dist_on_axis A float whose value is the real world length of the axis along the user's screen (width of screen for x-axis, height of screen for y-axis).
        * Must be the same units as dist_from_screen.
        * @param collision_component A null-terminated string whose value may be 'X', 'Y', or 'Z'.
        * This string defines what component of the look vector's collision point on the virtual plane to use for manipulating the device's axis.
        * @param sensor_dir A null-terminated string whose value may be 'X', 'Y', or 'Z'.
        * This string defines which of the sensor's local axis to use for creating the vector to collide with the virtual plane.
        * @param button_halt A float whose value is a pause time in milliseconds.
        * When a button is pressed on the emulated device, transmission of changes to the axis is paused for the inputted amount of time to prevent undesired motion detection when pressing buttons.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setScreenPointAxis( /* TSS_ID */
            uint id, out byte joy_or_mouse, out byte device_axis, float dist_from_screen, float dist_on_axis,
            out byte collision_component, out byte sensor_dir, float button_halt);

        /**
        * \brief Disables an axis on the passed in device.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param joy_or_mouse A null-terminated string whose value may be either 'mouse' or 'joystick'.
        * @param device_axis A null-terminated string whose value may be either 'X' or 'Y'.
        * This string defines what axis of the device is to be configured.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum disableAxis( /* TSS_ID */ uint id, out byte joy_or_mouse, out byte device_axis);

        /**
        * \brief Binds a sensor's physical button to an emulated device's button.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param joy_or_mouse A null-terminated string whose value may be either 'mouse' or 'joystick'.
        * @param but_idx The value that defines which button on the emulated device to configure.
        * This value is in the range 0 - 7.
        * @param button_bind A null-terminated string whose value may be 'left' or 'right'.
        * This string defines which physical button to bind to the emulated device's button to as defined by but_idx.
        * Designation of 'left' and 'right' assumes the sensor to be oriented such that the LED side of the sensor is facing up and the side of the sensor that contains the USB port is facing towards the user.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setPhysicalButton( /* TSS_ID */
            uint id, out byte joy_or_mouse, /* unsigned char */ byte but_idx, out byte button_bind);

        /**
        * \brief Sets up an emulated device's button such that it is 'pressed' when the sensor is shaken.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param joy_or_mouse A null-terminated string whose value may be either 'mouse' or 'joystick'.
        * @param but_idx The value that defines which button on the emulated device to configure.
        * This value is in the range 0 - 7.
        * @param threshold Defines how many Gs of force must be experienced by the sensor before the button is 'pressed'.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setShakeButton( /* TSS_ID */
            uint id, out byte joy_or_mouse, /* unsigned char */ byte but_idx, float threshold);

        /**
        * \brief Sets up a device's button such that it is 'pressed' when a reference vector aligns itself with a local vector.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param joy_or_mouse A null-terminated string whose value may be either 'mouse' or 'joystick'.
        * @param but_idx The value that defines which button on the emulated device to configure.
        * This value is in the range 0 - 7.
        * @param local_axis A normalized vector representing the sensor's local vector to track.
        * @param global_axis A normalized vector representing the global vector to move the local vector towards for "pressing" (should not be colinear to the local vector).
        * @param max_dist Defines how close the local vector's orientation must be to the global vector for the button to be 'pressed'.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum setOrientationButton( /* TSS_ID */
            uint id, out byte joy_or_mouse, /* unsigned char */ byte but_idx, Vector local_axis,
            Vector global_axis, float max_dist);

        /**
        * \brief Disables a button on the passed in emulated device.
        * This call will error if the inputted id is for a 3-Space Dongle.
        *
        * @param id The identifier for the 3-Space device.
        * @param joy_or_mouse A null-terminated string whose value may be either 'mouse' or 'joystick'.
        * @param but_idx The value that defines which button on the emulated device to configure.
        * This value is in the range 0 - 7.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum disableButton( /* TSS_ID */
            uint id, out byte joy_or_mouse, /* unsigned char */ byte but_idx);

        // Asynchronous calls
        /**
        * \brief Stops all asynchronous transmissions from the wireless sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * @param id The identifier for the 3-Space device.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum stopAsynchronous( /* TSS_ID */ uint id);

        /**
        * \brief Updates the internaly stored asynchronous data gathered from the connected 3-Space Dongles.
        *
        * This function must be called in order to retrieve updated asynchronous data.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum updateAsynchData();

        /**
        * \brief Retrieves asynchronous data for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * When retrieving data, the stored data is casted as an array of floats.
        * @param id The identifier for the 3-Space device.
        * @param data The asynchronous data is written to the referenced array of floats.
        * @param timestamp The timestamp the 3-Space Dongle gave the asynchronous data is written to the referenced variable.
        * @param required_size The required byte size that the referenced array needs to be.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAsynchDataFloat( /* TSS_ID */
            uint id, out float data, out int timestamp, out int required_size);

        /**
        * \brief Retrieves asynchronous data for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * When retrieving data, the stored data is casted as an array of  uintegers.
        * @param id The identifier for the 3-Space device.
        * @param data The asynchronous data is written to the referenced array of  uintegers.
        * @param timestamp The timestamp the 3-Space Dongle gave the asynchronous data is written to the referenced variable.
        * @param required_size The required byte size that the referenced array needs to be.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAsynchDataUInt( /* TSS_ID */
            uint id, /* unsigned int */ out uint data, out int timestamp, out int required_size);

        /**
        * \brief Retrieves asynchronous data for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * When retrieving data, the stored data is casted as an array of  ushort integers.
        * @param id The identifier for the 3-Space device.
        * @param data The asynchronous data is written to the referenced array of ushort integers.
        * @param timestamp The timestamp the 3-Space Dongle gave the asynchronous data is written to the referenced variable.
        * @param required_size The required byte size that the referenced array needs to be.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAsynchDataUShort( /* TSS_ID */
            uint id, /* unsigned short */ out ushort data, out int timestamp, out int required_size);

        /**
        * \brief Retrieves asynchronous data for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * When retrieving data, the stored data is casted as an array of characters.
        * @param id The identifier for the 3-Space device.
        * @param data The asynchronous data is written to the referenced array of characters.
        * @param timestamp The timestamp the 3-Space Dongle gave the asynchronous data is written to the referenced variable.
        * @param required_size The required byte size that the referenced array needs to be.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum getAsynchDataUChar( /* TSS_ID */
            uint id, /* unsigned char */ out byte data, out int timestamp, out int required_size);

        //// 0(0x00)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Tared Orientation of the sensor as a quaternion.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z, w)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltTaredOrientQuat( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 1(0x01)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Tared Orientation of the sensor as a set of euler angles.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (pitch, yaw, roll) in radians
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltTaredOrientEuler( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 2(0x02)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Tared Orientation of the sensor as a 3x3 matrix.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted in row-major order.
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltTaredOrientMatrix( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 3(0x03)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Tared Orientation of the sensor as an axis angle construct.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z, angle) with the angle being in radians
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltTaredOrientAxisAngle( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 4(0x04)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Tared Orientation of the sensor as a pair of two vectors (A Forward vector and a Down vector).
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (Forward_x, Forward_y, Forward_z, Down_x, Down_y, Down_z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltTaredOrientFwdDwn( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 6(0x06)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Untared Orientation of the sensor as a quaternion.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z, w)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltOrientQuat( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 7(0x07)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Untared Orientation of the sensor as a set of euler angles.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (pitch, yaw, roll) in radians
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltOrientEuler( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 8(0x08)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Untared Orientation of the sensor as a 3x3 matrix.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted in row-major order.
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltOrientMat( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 9(0x09)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Untared Orientation of the sensor as an axis angle construct.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z, angle) with the angle being in radians
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltOrientAxisAngle( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 10(0x0a)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Untared Orientation of the sensor as a pair of two vectors (A Forward vector and a Down vector).
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (Forward_x, Forward_y, Forward_z, Down_x, Down_y, Down_z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltOrientFwdDwn( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 11(0x0b)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current Filtered Tared Orientation of the sensor as a pair of two vectors (A Forward vector and a Down vector).
        * This version returns the data as the Forward and Down vectors of the coordinate system defined by the sensor reference frame.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (Forward_x, Forward_y, Forward_z, Down_x, Down_y, Down_z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltTaredFwdDwnVecSensFrame( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 12(0x0c)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current North and Earth vectors determined by the Filtered Tared Orientation of the sensor.
        * The data is returned such that the North and Earth vectors' coordinate system is defined by the sensor reference frame.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (North_x, North_y, North_z, Earth_x, Earth_y, Earth_z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetFiltNorthEarthVecSensFrame( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 32(0x20)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current normalized data being read from the collection of on-board sensors on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (Gyro_x, Gyro_y, Gyro_z, Accelerometer_x, Accelerometer_y, Accelerometer_z, Compass_x, Compass_y, Compass_z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetAllSensorsNormalized( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 33(0x21)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current normalized data being read from the gyroscope on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetGyroNormalized( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 34(0x22)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current normalized data being read from the accelerometer on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetAccelerometerNormalized( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 35(0x23)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current normalized data being read from the compass on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetCompassNormalized( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 39(0x27)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current unfiltered (but compensated) data being read from the accelerometer on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetAccelerometerUnfiltered( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 40(0x28)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current unfiltered (but compensated) data being read from the compass on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetCompassUnfiltered( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 64(0x40)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current raw data being read from the collection of on-board sensors on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (Gyro_x, Gyro_y, Gyro_z, Accelerometer_x, Accelerometer_y, Accelerometer_z, Compass_x, Compass_y, Compass_z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetAllSensorsRaw( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 65(0x41)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current raw data being read from the gyroscope on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetGyroRaw( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 66(0x42)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current raw data being read from the accelerometer on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetAccelerometerRaw( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 67(0x43)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current raw data being read from the compass on the wireless sensor.
        * When retrieving the data, the stored data is casted as an array of floats.
        * The array will be formatted as such: (x, y, z)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetCompassRaw( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

        //// 250(0xfa)
        /**
        * \brief Begins the asynchronous session for a 3-Space sensor.
        * This call will error if the inputted id is not for a 3-Space Wireless Sensor.
        *
        * This session will be transmitting the current state of the sensor's physical buttons.
        * When retrieving the data, the stored data is casted as an character.
        * The character value represents the button states as such: (1: right buton down, 2: left button down, 3: both buttons down)
        * @param id The identifier for the 3-Space device.
        * @param interval An integer whose value expresses how long, in milliseconds, the wireless sensor should wait before attempting to transmit an asynchronous packet.
        * @param duration An integer whose value expresses how long, in milliseconds, the wireless sensor should transmit asynchronous packets.
        * Once the elapsed time since the start of the asynchronous session exceeds duration, the sensor will stop transmitting asynchronous packets on its own.
        * If duration is 0xffff (65535 in decimal), the sensor will tranmit asynchronous packets until either the sensor is powered down or another asynchronous command is sent with a finite duration value.
        * A duration of 0 will effectively stop all asynchronous transmissions from the wireless sensor.
        * @return An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.
        */

        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern ResultEnum asynchGetButtonState( /* TSS_ID */
            uint id, /* unsigned short */ ushort interval, /* unsigned short */ ushort duration);

    }

}