using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace YEISensorLib.RawApi
{
    /*
     * DO NOT IMPLEMENT THESE:
     *      The following calls have been found to be broken (internally in the driver):
     *          GetTemperature (both F* and C*)
     *          GetConfidence
     * */

    /// <summary>
    /// This is a piecemiel translation as I attempt to figure stuff out slowly. 
    /// </summary>
    public class ThreeSpaceInterop
    {


        /// <summary>
        /// Check if the provided device id is connected, if specified by reconnect then attempts to reconnect
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="reconnect"></param>
        /// <returns>true if connected</returns>
        public static bool IsConnected(uint deviceId, bool reconnect)
        {
            return IsConnectedRaw(deviceId, reconnect ? 1 : 0) == 1;
        }
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_isConnected")]
        protected static extern /* TSS_ID */ int IsConnectedRaw(
            uint deviceId,
            int reconnect //0 = false, 1 = true
            );


        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi,
            EntryPoint = "tss_resetThreeSpaceAPI")]
        protected static extern /* TSS_ID */ int ResetThreeSpaceApi();

        /// <summary>
        /// Creates a three space sensor using the ComPort.PortName and sensor type.
        /// NOTE: unless the result is Defines.NO_DEVICE_ID you must dispose using CloseDevice
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="timeStampMode"></param>
        /// <returns></returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_createTSDeviceStr")]
        public static extern /* TSS_ID */ uint CreateDevice(
            string portName,
            TimeStampModeEnum timeStampMode
            );


        /// <summary>
        /// Disconnects the 3-Space device associated with the inputted ID.
        /// If connected through a serial port, that port is closed.
        /// </summary>
        /// <param name="deviceId">The identifier for the device to disconnect.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_closeTSDevice")]
        public static extern ResultEnum CloseDevice(
            uint deviceId
            );


        /// <summary>
        /// Retrieves a hexidecimal string representation of the 3-Space device's serial number (matching the representation on the case of the device).
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="serial">The char buffer to write the string into. Should have a length of 9 (8 for the hex string, 1 for a null terminator).</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_getSerialNumber")]
        public static extern ResultEnum GetSerialNumber(
            uint deviceId,
            byte[] serialNumber,
            out uint timestamp
            );


        /// <summary>
        ///  Scans the host computer for all serial ports (virtual or physical).
        ///  Ports returned are sorted into two lists based on whether the driver creating the port was a 3-Space driver.
        /// </summary>
        /// <param name="portInfo">A comport struct to fill</param>
        /// <param name="listSize">Number of structs to fill (specify 1)</param>
        /// <param name="portIndex">index of comport (Zero based)</param>
        /// <param name="filter">A mask of what 3-Space device types were desired. Ports appended to tss_port_list will only have devices connected that match the mask. (FilterEnum)</param>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_getComPorts")]
        protected static extern int GetAvailableComPortRaw(
            ref ComPort portInfo,
            uint listSize,
            uint portIndex,
            uint filter
            );
        
        /// <summary>
        /// Returns the the 3 space sensor at comport or null
        /// </summary>
        /// <returns></returns>
        public static ComPort? GetComPort(uint index)
        {
            var result = new ComPort();
            var nFound = GetAvailableComPortRaw(ref result, 1, index, (uint)FilterEnum.AnyKnown);
            if (nFound == 0) return null;
            return result;
        }



        /// <summary>
        /// Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a TSS_Quaternion.
        /// This call will error if the inputted id is for a 3-Space Dongle.
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="quaternion">The orientation will be written to this structure.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_getTaredOrientationAsQuaternion")]
        public static extern ResultEnum GetTaredOrientationAsQuaternion(
            uint deviceId,
            out Quaternion quaternion,
            out uint timeStamp
            );

        /// <summary>
        /// Tare the device with it's current orientation
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_tareWithCurrentOrientation")]
        public static extern ResultEnum TareWithCurrentOrientation(
            uint deviceId,
            out uint timeStamp
            );


        /// <summary>
        /// Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a set of TSS_Euler angles.
        /// This call will error if the inputted id is for a 3-Space Dongle.
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="euler">The orientation will be written to this structure.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_getTaredOrientationAsEulerAngles")]
        public static extern ResultEnum GetTaredOrientationAsEulerAngles(
            uint deviceId,
            out Euler euler,
            out uint timeStamp
            );


        /// <summary>
        /// Returns the normalized data of the 3-Space Sensor's on-board sensors.
        /// This call will error if the inputted id is for a 3-Space Dongle.
        /// </summary>
        /// <param name="id">The identifier for the 3-Space device.</param>
        /// <param name="data">The normalized data from the 3-Space Sensor's accelerometer, gyroscope, and compass are filled into the referenced structure.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_getAllNormalizedComponentSensorData")]
        public static extern ResultEnum GetAllNormalizedComponentSensorData(
            uint id,
            out Vector3F gyro,
            out Vector3F accelerometer,
            out Vector3F compass,
            out uint timeStamp
            );


        /// <summary>
        /// Sets the color of the LED on the sensor to the given RGB color.
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="color">The color the device's LED is being set to.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "tss_setLEDColor")]
        public static extern ResultEnum SetLedColor( 
            uint deviceId,
            float[] color,
            uint timeStampZero
            );


        /// <summary>
        /// Reads the color of the LED on the sensor.
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="color">The color of the device's LED is written to the referenced variable.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tss_getLEDColor")]
        public static extern ResultEnum GetLedColor(
            uint deviceId, 
            out Color color,
            out uint timeStamp
            );


        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tss_getButtonState")]
        public static extern ResultEnum GetButtonState(
            uint deviceId, 
            out ButtonState state
            );
    }

}