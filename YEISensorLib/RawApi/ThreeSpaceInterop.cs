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
        /// Scans the host computer for the first serial port it finds with a sensor (virtual or physical).
        /// Port is returned by tss_port. Calling getAvailableComPorts or getFirstAvailableTSSComPort will reset the iteration through the sensors.
        /// </summary>
        /// <param name="port">A pointer to the first serial port with a 3-Space device that matches the filter.</param>
        /// <param name="filter">A mask of what 3-Space device types were desired. (FilterEnum)</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "getFirstAvailableTSSComPort")]
        public static extern ResultEnum GetFirstAvailableComPort(
            out ComPort port,
            int filter
        );

        /// <summary>
        /// Creates a three space sensor using the ComPort.PortName and sensor type.
        /// NOTE: unless the result is Defines.NO_DEVICE_ID you must dispose using CloseDevice
        /// </summary>
        /// <param name="PortName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "createTSDeviceStr")]
        public static extern /* TSS_ID */ uint CreateDevice(
            string PortName,
            SensorTypeEnum type
            );


        /// <summary>
        /// Disconnects the 3-Space device associated with the inputted ID.
        /// If connected through a serial port, that port is closed.
        /// </summary>
        /// <param name="deviceId">The identifier for the device to disconnect.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "closeTSDevice")]
        public static extern ResultEnum CloseDevice(
            uint deviceId
            );


        /// <summary>
        /// Retrieves a hexidecimal string representation of the 3-Space device's serial number (matching the representation on the case of the device).
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="serial">The char buffer to write the string into. Should have a length of 9 (8 for the hex string, 1 for a null terminator).</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "getSerialNumberHex")]
        public static extern ResultEnum GetSerialNumber(
            uint deviceId,
            byte[] serial
            );


        /// <summary>
        ///  Scans the host computer for all serial ports (virtual or physical).
        ///  Ports returned are sorted into two lists based on whether the driver creating the port was a 3-Space driver.
        /// </summary>
        /// <param name="sensorPortList">A pointer to an array filled with serial ports created by 3-Space drivers. These ports have 3-Space hardware connected to them. This pointer will have the address of the array written to it, if not NULL.</param>
        /// <param name="unknownPortList">A pointer to an array filled with serial ports not directly created by a 3-Space driver. These ports may have 3-Space hardware connected through RS-232 port. This pointer will have the address of the array written to it, if not NULL.</param>
        /// <param name="sensorPortListSize">A pointer to the number of listings written to the array pointed to by tss_port_list. This pointer is written to, if not NULL.</param>
        /// <param name="unknownPortListSize">A pointer to the number of listings written to the array pointed to by ukn_port_list. This pointer is written to, if not NULL.</param>
        /// <param name="filter">A mask of what 3-Space device types were desired. Ports appended to tss_port_list will only have devices connected that match the mask. (FilterEnum)</param>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "getAvailableComPorts")]
        public static extern void GetAvailableComPortsRaw(
            ref IntPtr sensorPortList,
            ref IntPtr unknownPortList,
            ref uint sensorPortListSize,
            ref uint unknownPortListSize,
            int filter
            );

        /// <summary>
        /// Scans the host computer for all comports that may be hosting threespace devices.
        /// </summary>
        /// <param name="sensorPorts">sensor com ports with threespace devices</param>
        /// <param name="unknownPorts">unknown com ports</param>
        public static List<ComPort> GetAvailableComPorts()
        {//Warning: I'm not sure if this is supposed to dispose of the results or not =/
            IntPtr sensorPortPointer = new IntPtr();
            var result = new List<ComPort>();
            IntPtr unknownPortPointer = new IntPtr();
            uint sensorPortSize = 0;
            uint unknownPortSize = 0;


            GetAvailableComPortsRaw(
                ref sensorPortPointer, ref unknownPortPointer, ref sensorPortSize, ref unknownPortSize,
                (int)FilterEnum.FindALL);

            return sensorPortPointer.ToListFromArray<ComPort>(sensorPortSize);
        }


        /// <summary>
        /// Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a TSS_Quaternion.
        /// This call will error if the inputted id is for a 3-Space Dongle.
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="quaternion">The orientation will be written to this structure.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "getFiltTaredOrientQuat")]
        public static extern ResultEnum GetFilteredTaredOrientedQuaterion(
            uint deviceId,
            out Quaternion quaternion
            );


        /// <summary>
        /// Retrieves the current orientation of a 3-Space Sensor relative to its tare orientation as a set of TSS_Euler angles.
        /// This call will error if the inputted id is for a 3-Space Dongle.
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="euler">The orientation will be written to this structure.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "getFiltTaredOrientEuler")]
        public static extern ResultEnum GetFilteredTaredOrientedEuler(
            uint deviceId,
            out Euler euler
            );


        /// <summary>
        /// Returns the normalized data of the 3-Space Sensor's on-board sensors.
        /// This call will error if the inputted id is for a 3-Space Dongle.
        /// </summary>
        /// <param name="id">The identifier for the 3-Space device.</param>
        /// <param name="data">The normalized data from the 3-Space Sensor's accelerometer, gyroscope, and compass are filled into the referenced structure.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "getAllSensorsNormalized")]
        public static extern ResultEnum GetNormalizedSensorData(
            uint id,
            ref SensorData data
            );


        /// <summary>
        /// Sets the color of the LED on the sensor to the given RGB color.
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="color">The color the device's LED is being set to.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "setLEDColor")]
        public static extern ResultEnum SetLedColor( 
            uint deviceId, 
            Color color
            );


        /// <summary>
        /// Reads the color of the LED on the sensor.
        /// </summary>
        /// <param name="deviceId">The identifier for the 3-Space device.</param>
        /// <param name="color">The color of the device's LED is written to the referenced variable.</param>
        /// <returns>An error code indicating either success or failure to execute the call. The code will also indicate the reason for the failure.</returns>
        [DllImport("ThreeSpace_API.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "getLEDColor")]
        public static extern ResultEnum GetLedColor(
            uint deviceId, 
            out Color color
            );
    }

}