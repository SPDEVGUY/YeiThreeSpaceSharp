/********************************************//**
 * \file yei_threespace_api.h
 * \brief  ThreeSpace API 2.0
 * \author Chris George
 * \author Daniel Morrison
 * \copyright Copyright 1998-2013, YEI Corporation.
 *
 * The YEI 3-Space C API is released under the YEI 3-Space Open Source License, which allows for both
 * non-commercial use and commercial use with certain restrictions.
 *
 * For Non-Commercial Use, your use of Covered Works is governed by the GNU GPL v.3, subject to the YEI 3-Space Open
 * Source Licensing Overview and Definitions.
 *
 * For Commercial Use, a YEI Commercial/Redistribution License is required, pursuant to the YEI 3-Space Open Source
 * Licensing Overview and Definitions. Commercial Use, for the purposes of this License, means the use, reproduction
 * and/or Distribution, either directly or indirectly, of the Covered Works or any portion thereof, or a Compilation,
 * Improvement, or Modification, for Pecuniary Gain. A YEI Commercial/Redistribution License may or may not require
 * payment, depending upon the intended use.
 *
 * Full details of the YEI 3-Space Open Source License can be found in license.txt
 * License also available online at http://www.yeitechnology.com/yei-3-space-open-source-license
 ***********************************************/
#ifndef YEI_THREESPACE_API_H_INCLUDED
#define YEI_THREESPACE_API_H_INCLUDED
#include <stdio.h>
#include <string.h>
#include <windows.h>
#include <process.h>
#include <math.h>


#ifdef __cplusplus
    extern "C"
    {
#endif

#if defined(__GNUC__)
#define _inline inline
#endif

#if defined(TSS_STATIC_LIB)
#define TSS_EXPORT
#elif defined(_MSC_VER)
#define TSS_EXPORT __declspec(dllexport)
#elif defined(__GNUC__)
#define TSS_EXPORT __attribute__ ((dllexport))
#endif

/********************************************//**
 * Macro for infinite duration for streaming setup.
 ***********************************************/
#define TSS_INFINITE_DURATION 0xffffffff


/********************************************//**
 * YEI 3-Space API device identifier, a common parameter needed for all 3-Space API calls.
 ***********************************************/
typedef unsigned int TSS_Device_Id;

/********************************************//**
 * An enum expressing the masks used to quickly determine the type of a sensor based on their device ID.
 ***********************************************/
typedef enum TSS_Device_Id_Mask
{
    TSS_NO_DEVICE_ID    = 0x00800000,              /**< Invalid Device ID */
    TSS_BOOTLOADER_ID   = 0x01000000,              /**< Bootloader */
    TSS_DONGLE_ID       = 0x02000000,              /**< Wireless Dongle */
    TSS_USB_ID          = 0x04000000,              /**< USB, Micro USB, or Watertight USB */
    TSS_EMBEDDED_ID     = 0x08000000,              /**< Embedded */
    TSS_WIRELESS_ID     = 0x10000000,              /**< Wireless 2.4GHz DSSS plugged via USB */
    TSS_WIRELESS_W_ID   = 0x20000000,              /**< Wireless 2.4GHz DSSS communicating wirelessly */
    TSS_DATALOGGER_ID   = 0x40000000,              /**< Data-logging */
    TSS_BLUETOOTH_ID    = 0x80000000,              /**< Bluetooth */
    TSS_NO_DONGLE_ID    = 0xfd000000,              /**< Convenience ID, includes all but the Wireless Dongle */
    TSS_ALL_SENSORS_ID  = 0xff000000               /**< Convenience ID, includes all valid id masks */
}TSS_Device_Id_Mask;

/********************************************//**
 * An enum expressing API Timestamp Modes.
 ***********************************************/
typedef enum TSS_Timestamp_Mode{
    TSS_TIMESTAMP_NONE,   /**< Disables timestamp, timestamp will return 0 */
    TSS_TIMESTAMP_SENSOR, /**< 3-Space device's timestamp, this can be set with tss_updateCurrentTimestamp */
    TSS_TIMESTAMP_SYSTEM  /**< The data is timestamped on arrival to the system using the high-resolution performance counter */
}TSS_Timestamp_Mode;

/********************************************//**
 * An enum expressing the different types of errors a 3-Space API call can return.
 ***********************************************/
typedef enum TSS_Error
{
    TSS_NO_ERROR,                   /**< The API call successfully executed */
    TSS_ERROR_COMMAND_FAIL,         /**< The command returned a failed response */
    TSS_INVALID_COMMAND,            /**< The API call was made on a device type that does not support the attempted command */
    TSS_INVALID_ID,                 /**< The TSS_Device_Id parameter passed in to an API call is not associated with a connected 3-Space device */
    TSS_ERROR_PARAMETER,            /**< General parameter fail */
    TSS_ERROR_TIMEOUT,              /**< The command's timeout has been reached */
    TSS_ERROR_WRITE,                /**< The API call executed failed to write all the data necessary to execute the command to the intended 3-Space device */
    TSS_ERROR_READ,                 /**< The API call executed failed to read all the data necessary to execute the command to the intended 3-Space device */
    TSS_ERROR_STREAM_SLOTS_FULL,    /**< The 3-Space device's stream slots are full */
    TSS_ERROR_STREAM_CONFIG,        /**< The 3-Space device's stream configuration is corrupted */
    TSS_ERROR_MEMORY,               /**< A memory error occurred in the API */
    TSS_ERROR_FIRMWARE_INCOMPATIBLE /**< 3-Space device firmware does not support that command, firmware update highly recommended */
}TSS_Error;

static const char* const TSS_Error_String[] = {
    "TSS_NO_ERROR",
    "TSS_ERROR_COMMAND_FAIL",
    "TSS_INVALID_COMMAND",
    "TSS_INVALID_ID",
    "TSS_ERROR_PARAMETER",
    "TSS_ERROR_TIMEOUT",
    "TSS_ERROR_WRITE",
    "TSS_ERROR_READ",
    "TSS_ERROR_STREAM_SLOTS_FULL",
    "TSS_ERROR_STREAM_CONFIG",
    "TSS_ERROR_MEMORY",
    "TSS_ERROR_FIRMWARE_INCOMPATIBLE"
};

/********************************************//**
 * An enum denoting the compatibility level of the 3-Space device.
 ***********************************************/
typedef enum TSS_Firmware_Compatibility
{
    TSS_FW_NOT_COMPATIBLE,       /**< Firmware is not compatible with the API and should be updated */
    TSS_FW_20R7_COMPATIBLE,      /**< 3-Space device compatible with 2.0R7 commands and up */
    TSS_FW_20R10_COMPATIBLE,      /**< 3-Space device compatible with 2.0R10 commands and up */
    TSS_FW_20R13_COMPATIBLE      /**< 3-Space device compatible with 2.0R13 commands and up */
}TSS_Firmware_Compatibility;

static const char* const TSS_Firmware_Version_String[] = {
    "00Jan2000A00",
    "25Apr2013A00",
    "21Jun2013A00"
    "28Aug2013A00"
};

/********************************************//**
 * An enum expressing the command list of Streamable Commands.
 ***********************************************/
typedef enum TSS_Stream_Command_Enum
{
    TSS_GET_TARED_ORIENTATION_AS_QUATERNION                 = 0x00,
    TSS_GET_TARED_ORIENTATION_AS_EULER_ANGLES               = 0x01,
    TSS_GET_TARED_ORIENTATION_AS_ROTATION_MATRIX            = 0x02,
    TSS_GET_TARED_ORIENTATION_AS_AXIS_ANGLE                 = 0x03,
    TSS_GET_TARED_ORIENTATION_AS_TWO_VECTOR                 = 0x04,
    TSS_GET_DIFFERENCE_QUATERNION                           = 0x05,
    TSS_GET_UNTARED_ORIENTATION_AS_QUATERNION               = 0x06,
    TSS_GET_UNTARED_ORIENTATION_AS_EULER_ANGLES             = 0x07,
    TSS_GET_UNTARED_ORIENTATION_AS_ROTATION_MATRIX          = 0x08,
    TSS_GET_UNTARED_ORIENTATION_AS_AXIS_ANGLE               = 0x09,
    TSS_GET_UNTARED_ORIENTATION_AS_TWO_VECTOR               = 0x0a,
    TSS_GET_TARED_TWO_VECTOR_IN_SENSOR_FRAME                = 0x0b,
    TSS_GET_UNTARED_TWO_VECTOR_IN_SENSOR_FRAME              = 0x0c,
    TSS_GET_ALL_NORMALIZED_COMPONENT_SENSOR_DATA            = 0x20,
    TSS_GET_NORMALIZED_GYRO_RATE                            = 0x21,
    TSS_GET_NORMALIZED_ACCELEROMETER_VECTOR                 = 0x22,
    TSS_GET_NORMALIZED_COMPASS_VECTOR                       = 0x23,
    TSS_GET_ALL_CORRECTED_COMPONENT_SENSOR_DATA             = 0x25,
    TSS_GET_CORRECTED_GYRO_RATE                             = 0x26,
    TSS_GET_CORRECTED_ACCELEROMETER_VECTOR                  = 0x27,
    TSS_GET_CORRECTED_COMPASS_VECTOR                        = 0x28,
    TSS_GET_CORRECTED_LINEAR_ACCELERATION_IN_GLOBAL_SPACE   = 0x29,
    TSS_GET_TEMPERATURE_C                                   = 0x2b,
    TSS_GET_TEMPERATURE_F                                   = 0x2c,
    TSS_GET_CONFIDENCE_FACTOR                               = 0x2d,
    TSS_GET_ALL_RAW_COMPONENT_SENSOR_DATA                   = 0x40,
    TSS_GET_RAW_GYROSCOPE_RATE                              = 0x41,
    TSS_GET_RAW_ACCELEROMETER_DATA                          = 0x42,
    TSS_GET_RAW_COMPASS_DATA                                = 0x43,
    TSS_GET_BATTERY_VOLTAGE                                 = 0xc9,
    TSS_GET_BATTERY_PERCENT_REMAINING                       = 0xca,
    TSS_GET_BATTERY_STATUS                                  = 0xcb,
    TSS_GET_BUTTON_STATE                                    = 0xfa,
    TSS_NULL                                                = 0xff
}TSS_Stream_Command_Enum;

/********************************************//**
 * YEI 3-Space API streamable command.
 ***********************************************/
typedef unsigned char TSS_Stream_Command;

/********************************************//**
 * An enum expressing the find flags for the tss_getComPorts filter parameter.
 ***********************************************/
typedef enum TSS_Find
{
    TSS_FIND_BTL        = 0x00000001, /**< Find Bootloader */
    TSS_FIND_USB        = 0x00000002, /**< Find USB, Micro USB, or Watertight USB */
    TSS_FIND_DNG        = 0x00000004, /**< Find Wireless Dongle */
    TSS_FIND_WL         = 0x00000008, /**< Find Wireless 2.4GHz DSSS plugged via USB */
    TSS_FIND_EM         = 0x00000010, /**< Find Embedded */
    TSS_FIND_DL         = 0x00000020, /**< Find Data-logging */
    TSS_FIND_BT         = 0x00000040, /**< Find Bluetooth */
    TSS_FIND_UNKNOWN    = 0x80000000, /**< Find serial ports that may have 3-Space devices connected to them */
    TSS_FIND_ALL_KNOWN  = 0x7fffffff, /**< Find all known 3-Space devices */
    TSS_FIND_ALL        = 0xffffffff  /**< Find all com ports including "unknown" serial ports that may have 3-Space devices connected */
}TSS_Find;

/********************************************//**
 * An enum expressing the types of 3-Space devices.
 ***********************************************/
typedef enum TSS_Type
{
    TSS_UNKNOWN,    /**< Device type was not able to be determined */
    TSS_BTL,        /**< Bootloader */
    TSS_USB,        /**< USB, Micro USB, or Watertight USB */
    TSS_DNG,        /**< Wireless Dongle */
    TSS_WL,         /**< Wireless 2.4GHz DSSS plugged via USB */
    TSS_WL_W,       /**< Wireless 2.4GHz DSSS communicating wirelessly */
    TSS_EM,         /**< Embedded */
    TSS_DL,         /**< Data-logging */
    TSS_BT,         /**< Bluetooth */
}TSS_Type;

static const char* const TSS_Type_String[] = {
    "TSS_UNKNOWN",
    "TSS_BTL",
    "TSS_USB",
    "TSS_DNG",
    "TSS_WL",        //wireless wired (connected to PC)
    "TSS_WL_W",    //wireless wireless
    "TSS_EM",
    "TSS_DL",
    "TSS_BT",
};

/********************************************//**
 * An enum expressing the alternate directions for each of the natural axes of the sensor.
 ***********************************************/
typedef enum TSS_Axis_Direction
{
    TSS_XYZ, /**< X: Right, Y: Up, Z: Forward (left-handed system, standard operation) */
    TSS_XZY, /**< X: Right, Y: Forward, Z: Up (right-handed system) */
    TSS_YXZ, /**< X: Up, Y: Right, Z: Forward (right-handed system) */
    TSS_YZX, /**< X: Forward, Y: Right, Z: Up (left-handed system) */
    TSS_ZXY, /**< X: Up, Y: Forward, Z: Right (left-handed system) */
    TSS_ZYX, /**< X: Forward, Y: Up, Z: Right (right-handed system) */
}TSS_Axis_Direction;

/********************************************//**
 * An enum expressing the different available filter modes.
 ***********************************************/
typedef enum TSS_Filter_Mode
{
    TSS_FILTER_IMU,                        /**< IMU Mode */
    TSS_FILTER_KALMAN,                     /**< Kalman Filtered Mode */
    TSS_FILTER_ALTERNATING_KALMAN,         /**< Alternating Kalman Filtered Mode */
    TSS_FILTER_COMPLEMENTARY,              /**< Complementary Filter Mode */
    TSS_FILTER_QUATERNION_GRADIENT_DECENT, /**< Quaternion Gradient Descent Filter Mode */
}TSS_Filter_Mode;

/********************************************//**
 * A structure that contains basic information about a com port.
 ***********************************************/
typedef struct TSS_ComPort
{
    char com_port[32];      /**< The com port string. */
    char friendly_name[64]; /**< The frienly name of the com port. */
    int sensor_type;        /**< The type of 3-Space device connected through the com port. */ //Change?
}TSS_ComPort;

/********************************************//**
 * A structure that contains information about the connected 3-Space device.
 ***********************************************/
typedef struct TSS_ComInfo
{
	TSS_Type device_type; /**< The type of 3-Space device connected through the com port. */
	unsigned int serial_number; /**< The serial number for the 3-Space device connected through the com port. */
	char firmware_version[13]; /**< The version of the firmware installed on the connected 3-Space device. */
	char hardware_version[33]; /**< The hardware revision and type of the connected 3-Space device. */
    TSS_Firmware_Compatibility fw_compatibility; /**< Firmware compatibility level (Note level may be lower than current if no functional changes were made). */
}TSS_ComInfo;

/********************************************//**
 * \brief Streaming data callback.
 *
 *  One of the methods of handling stream data is to create a callback, this is the template function.
 *  tss_setNewDataCallBack sets the callback.
 * \param device The device ID of the sensor.
 * \param output_data The data the sensor returned.
 * \param output_data_len The size of the data returned.
 * \param timestamp The timestamp in microseconds if timestamps are enabled.
 ***********************************************/
typedef void CALLBACK TSS_CallBack(TSS_Device_Id device, char * output_data, unsigned int output_data_len, unsigned int * timestamp);

// Allow to call those directly when not using a DLL
#ifdef TSS_STATIC_LIB
TSS_EXPORT int tss_initThreeSpaceAPI();
TSS_EXPORT int tss_delThreeSpaceAPI();
#endif // TSS_STATIC_LIB

//convenience functions
/********************************************//**
 * \brief Convenience function to parse axis_byte into its separate components.
 * \param axis_byte The byte returned from tss_getAxisDirections.
 * \param axis_order Writes the axis order of the natural axes of the sensor.
 * \param neg_x Writes whether the First resulting component is negative, 0 is false 1 is true.
 * \param neg_y Writes whether the Second resulting component is negative, 0 is false 1 is true.
 * \param neg_z Writes whether the Third resulting component is negative, 0 is false 1 is true.
 ***********************************************/
TSS_EXPORT void tss_parseAxisDirections(unsigned char axis_byte, TSS_Axis_Direction * axis_order, char * neg_x, char * neg_y, char * neg_z);

/********************************************//**
 * \brief Convenience function to generate an axis_byte from its separate components.
 * \param axis_order The axis order of the natural axes.
 * \param neg_x Whether the First resulting component is negative, 0 is false 1 is true.
 * \param neg_y Whether the Second resulting component is negative, 0 is false 1 is true.
 * \param neg_z Whether the Third resulting component is negative, 0 is false 1 is true.
 * \return The byte that can be sent to tss_setAxisDirections or stored.
 ***********************************************/
TSS_EXPORT unsigned char tss_generateAxisDirections(TSS_Axis_Direction axis_order, char neg_x, char neg_y, char neg_z);

//API specific functions
// Sets the amount of retries on wireless commands via software, note there is 3 hardware retries by default
/********************************************//**
 * \brief Sets the amount of retries on wireless commands via software.
 *
 * Note: There are 3 hardware retries by default in addition to the soft retries here.
 * \param retries The amount of soft retries.
 ***********************************************/
TSS_EXPORT void tss_setSoftwareWirelessRetries(unsigned int retries);

/********************************************//**
 * \brief Gets the amount of retries on wireless commands via software.
 * \return The amount of soft retries.
 ***********************************************/
TSS_EXPORT unsigned int tss_getSoftwareWirelessRetries();

/********************************************//**
 * \brief Sets the default baud rate for newly created 3-Space devices.
 * \param baud_rate The desired baud rate.
 ***********************************************/
TSS_EXPORT void tss_setDefaultCreateDeviceBaudRate(int baud_rate);

/********************************************//**
 * \brief Gets the default baud rate for newly created 3-Space devices.
 * \return The default baud rate.
 ***********************************************/
TSS_EXPORT int tss_getDefaultCreateDeviceBaudRate();

/********************************************//**
 * \brief Gets the com ports of YEI 3-Space devices and other available com ports.
 * \param ports_vec The list of (or single) TSS_ComPort structures to return the results.
 * \param ports_vec_size The size of the list entered.
 * \param offset This allows you to call this function multiple times to get all the com ports, increment the offset to continue getting more 3-Space devices.
 * \param filter Pass in a combination of TSS_Find enums you want to search for.
 * \return The number of com ports found or equal to the size of ports_vec_size.
 ***********************************************/
TSS_EXPORT int tss_getComPorts(TSS_ComPort * ports_vec, unsigned int ports_vec_size, unsigned int offset, unsigned int filter);

/********************************************//**
 * \brief Gets information from the 3-Space device connected to the com port.
 *
 * This can be used to check if there is anything connected to a com port and if its a 3-Space device.
 * It writes the TSS_ComInfo structure so you can get the device type, serial number, hardware, and firmware.
 * \param com_port The com port string for the port to be queried.
 * \param com_info Writes this structure to return the com port information gathered.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_getTSDeviceInfoFromComPort(const char* com_port, TSS_ComInfo * com_info);

/********************************************//**
 * \brief Gets information from the 3-Space device associated with the device ID.
 *
 * It writes the TSS_ComInfo structure so you can check the device type, serial number, hardware, and firmware of the 3-Space device.
 * \param device The device ID of the 3-Space device to check.
 * \param com_info Writes this structure to return the com port information gathered.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_getTSDeviceInfo(TSS_Device_Id device, TSS_ComInfo * com_info);

/********************************************//**
 * \brief Creates a device ID with wide char support.
 *
 * This creates and performs initial setup on the connected 3-Space device.
 * The returned device ID is used to interact with the 3-Space device.
 * When a device ID is created, other programs cannot use that com port until tss_closeTSDevice is called or the application is closed.
 * \param com_port The com port string for the com port to be queried.
 * \param mode The desired timestamp mode the 3-Space device is to be configured with.
 * \return A device ID, TSS_NO_DEVICE_ID is returned if the creation failed.
 ***********************************************/
TSS_EXPORT TSS_Device_Id tss_createTSDeviceU16Str(const wchar_t* com_port, TSS_Timestamp_Mode mode);

/********************************************//**
 * \brief Creates a device ID.
 *
 * This creates and performs initial setup on the connected 3-Space device.
 * The returned device ID is used to interact with the 3-Space device.
 * When a device ID is created, other programs cannot use that com port until tss_closeTSDevice is called or the application is closed.
 * \param com_port The com port string for the com port to be queried.
 * \param mode The desired timestamp mode the 3-Space device is to be configured with.
 * \return A device ID, TSS_NO_DEVICE_ID is returned if the creation failed.
 ***********************************************/
TSS_EXPORT TSS_Device_Id tss_createTSDeviceStr(const char* com_port, TSS_Timestamp_Mode mode);

/********************************************//**
 * \brief Creates a device ID with extended parameter and wide char support.
 *
 * This creates and performs initial setup on the connected 3-Space device.
 * The returned device ID is used to interact with the 3-Space device.
 * When a device ID is created, other programs cannot use that com port until tss_closeTSDevice is called or the application is closed.
 * \param com_port The com port string for the com port to be queried.
 * \param baud_rate The baud rate of the connected 3-Space device.
 * \param mode The desired timestamp mode the 3-Space device is to be configured with.
 * \return A device ID, TSS_NO_DEVICE_ID is returned if the creation failed.
 ***********************************************/
TSS_EXPORT TSS_Device_Id tss_createTSDeviceU16StrEx(const wchar_t* com_port, int baud_rate, TSS_Timestamp_Mode mode);

/********************************************//**
 * \brief Creates a device ID with extended parameter.
 *
 * This creates and performs initial setup on the connected 3-Space device.
 * The returned device ID is used to interact with the 3-Space device.
 * When a device ID is created, other programs cannot use that com port until tss_closeTSDevice is called or the application is closed.
 * \param com_port The com port string for the com port to be queried.
 * \param baud_rate The baud rate of the connected 3-Space device.
 * \param mode The desired timestamp mode the 3-Space device is to be configured with.
 * \return A device ID, TSS_NO_DEVICE_ID is returned if the creation failed.
 ***********************************************/
TSS_EXPORT TSS_Device_Id tss_createTSDeviceStrEx(const char* com_port, int baud_rate, TSS_Timestamp_Mode mode);

/********************************************//**
 * \brief Closes and frees the com port.
 *
 * Once the com port is freed, other programs can use the com port if necessary.
 * \param device The device ID for the 3-Space device.
 ***********************************************/
TSS_EXPORT TSS_Error tss_closeTSDevice(TSS_Device_Id device);

/********************************************//**
 * \brief Creates a device ID from a dongle.
 *
 * This creates and performs initial setup or returns an already created device ID for a wireless sensor from a dongle.
 * The returned device ID is used to interact with the 3-Space device.
 * The wireless sensor must be paired with the dongle or this will fail.
 * The timestamp mode is inherited from the dongle's settings.
 * \param device The device ID for the dongle.
 * \param logical_id The logical identifier of the desired wireless sensor, 0-14 is the valid range.
 * \param w_ts_device Writes the device ID, TSS_NO_DEVICE_ID is written if the creation failed.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_getSensorFromDongle(TSS_Device_Id device, int logical_id, unsigned int * w_ts_device);

/********************************************//**
 * \brief Sets a wireless sensor to a dongle.
 *
 * This creates and performs initial setup for a wireless sensor and pairs it to a dongle.
 * The wireless sensor must have the same wireless PanID and Channel as the dongle or this will fail.
 * The timestamp mode is inherited from the dongle's settings.
 * \param device The device ID for the dongle.
 * \param logical_id The desired logical identifier for the wireless sensor, 0-14 is the valid range.
 * \param w_serial_number The serial number of the wireless sensor to be paired to the dongle.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_setSensorToDongle(TSS_Device_Id device, int logical_id, unsigned int w_serial_number);

/********************************************//**
 * \brief Sets the timestamp mode on an existing 3-Space device.
 * \param device The device ID for the 3-Space device.
 * \param mode The desired timestamp mode the 3-Space device is to be configured with.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_setTimestampMode(TSS_Device_Id device, TSS_Timestamp_Mode mode);

/********************************************//**
 * \brief Gets the last stream data from a sensor.
 *
 * Non-blocking method of handling stream data, function will immediately return data.
 * Good for real-time applications.
 * \param device The device ID for the sensor.
 * \param output_data The data the sensor returned.
 * \param output_data_len The size of the data returned.
 * \param timestamp The timestamp in microseconds if timestamps are enabled.
 * \return TSS_NO_ERROR(0) if successful, TSS_ERROR_READ if called before any data has returned and
 * non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_getLastStreamData(TSS_Device_Id device, char * output_data, unsigned int output_data_len, unsigned int * timestamp);

/********************************************//**
 * \brief Gets the last stream data from a sensor.
 *
 * Non-blocking method of handling stream data, function will immediately return data.
 * Good for real-time applications.
 * This second method is for hooking to UDK and other applications that will not allow multiple casts from the same function.
 * \param device The device ID for the sensor.
 * \param output_data The data the sensor returned.
 * \param output_data_len The size of the data returned.
 * \param timestamp The timestamp in microseconds if timestamps are enabled.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_getLastStreamData2(TSS_Device_Id device, char * output_data, unsigned int output_data_len, unsigned int * timestamp);

/********************************************//**
 * \brief Gets the latest stream data from a sensor.
 *
 * Blocking method of handling stream data, function will wait until new data arrives.
 * Good for data logging.
 * \param device The device ID for the sensor.
 * \param output_data The data the sensor returned.
 * \param output_data_len The size of the data returned.
 * \param timeout The time in milliseconds to wait until the function will timeout.
 * \param timestamp The timestamp in microseconds if timestamps are enabled.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_getLatestStreamData(TSS_Device_Id device, char * output_data, unsigned int output_data_len, unsigned int timeout, unsigned int * timestamp);

/********************************************//**
 * \brief Sets a new data callback for a sensor.
 *
 * This is used to setup the callback method.
 * The callback will be called every time a new stream data form the sensor arrives.
 * \param device The device ID for the sensor.
 * \param callback_func The function to call when new data arrives.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_setNewDataCallBack(TSS_Device_Id device, TSS_CallBack callback_func);

/********************************************//**
 * \brief Sends a raw command to a 3-Space device formatted for converting to and from big endian.
 *
 * Sending raw commands allows users to call functions that are in custom or beta firmwares that are not exposed in the API.
 * in_data_detail and rtn_data_detail are formatter strings, their input is as follows:
 *  i : int
 *  I : unsigned int
 *  f : float
 *  h : short
 *  H : unsigned short
 *  b : signed char
 *  B : unsigned char
 *  ? : bool (char)
 *  s : char
 *  x : pad byte
 * For example, the command for getting the tared orientation as a quaternion would be formatted as follows:
 *  float quat[4];
 *  unsigned int timestamp;
 *  tss_sendRawCommandFormatted(device, 0x00, NULL, "", (char*)quat, "ffff", &timestamp);
 * \param device The device ID for the sensor.
 * \param command The command byte of the sensor function.
 * \param in_data Pointer to the input data for the command. Pass NULL if the command has no input.
 * \param in_data_detail The formatter string for the input data. Pass "" if the command has no input.
 * \param rtn_data Pointer to the return data for the command. Pass NULL if the command has no output.
 * \param rtn_data_detail The formatter string for the return data. Pass "" if the command has no output.
 * \param timestamp Optionally can get the timestamp of the command packet. Pass NULL if the timestamp is not wanted.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_sendRawCommandFormatted(   TSS_Device_Id device,
                                                    unsigned char command,
                                                    char* in_data,
                                                    char* in_data_detail,
                                                    char * rtn_data,
                                                    char* rtn_data_detail,
                                                    unsigned int * timestamp);

/********************************************//**
 * \brief Sends a raw command to a 3-Space device un-formatted.
 *
 * Sending raw commands allows users to call functions that are in custom or beta firmwares that are not exposed in the API.
 * This function does not convert the bytes to and from big endian, data must be formatted by the user.
 * For example, the command for getting the tared orientation as a quaternion would be formatted as follows:
 *  float quat[4];
 *  unsigned int timestamp;
 *  tss_sendRawCommand(device, 0x00, NULL, 0, (char*)quat, 16, &timestamp);
 * \param device The device ID for the sensor.
 * \param command The command byte of the sensor function.
 * \param in_data Pointer to the input data for the command. Pass NULL if the command has no input.
 * \param in_data_detail The formatter string for the input data. Pass "" if the command has no input.
 * \param rtn_data Pointer to the return data for the command. Pass NULL if the command has no output.
 * \param rtn_data_detail The formatter string for the return data. Pass "" if the command has no output.
 * \param timestamp Optionally can get the timestamp of the command packet. Pass NULL if the timestamp is not wanted.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_sendRawCommand(TSS_Device_Id device,
                                        unsigned char command,
                                        char* in_data,
                                        int in_data_size,
                                        char * rtn_data,
                                        int rtn_data_size,
                                        unsigned int * timestamp);

//Remove debug func
//TSS_EXPORT TSS_Error tss_getRecordCount(TSS_Device_Id device);

/********************************************//**
 * \brief Tests to check if a 3-Space device is connected.
 *
 * This sends a command to check if the device is still available.
 * \param device The device ID for the 3-Space device.
 * \param reconnect Whether to attempt a serial reconnection, 0 is false 1 is true.
 * \return 1 if connected, 0 if unreachable/not connected.
 ***********************************************/
TSS_EXPORT int tss_isConnected(TSS_Device_Id device, int reconnect);

/********************************************//**
 * \brief Gets the device type of a 3-Space device.
 * \param device The device ID for the 3-Space device.
 * \param device_type Writes the type of the 3-Space device.
 * \return TSS_NO_ERROR(0) if successful, non zero if an error occurred.
 ***********************************************/
TSS_EXPORT TSS_Error tss_getTSDeviceType(TSS_Device_Id device, TSS_Type * device_type);

/********************************************//**
 * \brief Reset the ThreeSpace API
 *
 * This will stop streaming and close all created device ids.
 * All device ids created before the reset are invalid and should be discarded.
 * \return 0 if no error else nonzero for error
 ***********************************************/
TSS_EXPORT int tss_resetThreeSpaceAPI();

/********************************************//**
 * \brief Create ts devices by serial number
 *
 * This convenience function will take a list of serial numbers and will write a list of device ids
 * \param serial_list The list of serial devices to find
 * \param device_list The list of devices ids that will be written to, this list must be the same size as the serial_list
 * \param list_length The  length of serial_list, which should be the same length as the device_list
 * \param search_wireless If set to non-zero(true) this will create any dongles it finds automaticly and will search its
    logical table to see if it can wirelessly communicate with any matching serial numbered devices
 * \param search_unknown_devices If set to non-zero(true) this will search all available comports. Com ports that cannot
    be passively identified as ThreeSpace devices will have bytes written to them to get more information. This is for physical comports and 3rd party serial to usb adapters
 * \param mode The desired timestamp mode the 3-Space device is to be configured with.
 * if TSS_NO_DEVICE_ID is returned then sensor not found
 * \return number of found devices, -1 if error
 ***********************************************/
TSS_EXPORT int tss_createTSDevicesBySerialNumber(	unsigned int * serial_list,
													TSS_Device_Id * device_list,
													int list_length,
													int search_wireless,
													int search_unknown_devices,
													TSS_Timestamp_Mode mode);

/////********************************************//**
// * \brief get device IDs
// *
// * This will stop streaming and close all created device ids.
// * All device ids created before the reset are invalid and should be discarded.
// * \return 0 if no error else nonzero for error
// ***********************************************/
//TSS_EXPORT int tss_getDeviceIDs( TSS_Device_Id * device_list, int filter);

//Orientation Commands
//   0(0x00)
TSS_EXPORT TSS_Error tss_getTaredOrientationAsQuaternion(TSS_Device_Id device, float * quat4, unsigned int * timestamp);
//   1(0x01)
TSS_EXPORT TSS_Error tss_getTaredOrientationAsEulerAngles(TSS_Device_Id device, float * euler3, unsigned int * timestamp);
//   2(0x02)
TSS_EXPORT TSS_Error tss_getTaredOrientationAsRotationMatrix(TSS_Device_Id device, float * matrix9, unsigned int * timestamp);
//   3(0x03)
TSS_EXPORT TSS_Error tss_getTaredOrientationAsAxisAngle(TSS_Device_Id device, float * axis_angle4, unsigned int * timestamp);
//   4(0x04)
TSS_EXPORT TSS_Error tss_getTaredOrientationAsTwoVector(TSS_Device_Id device, float * forward3, float * down3, unsigned int * timestamp);
//   5(0x05)
TSS_EXPORT TSS_Error tss_getDifferenceQuaternion(TSS_Device_Id device, float * quat4, unsigned int * timestamp);
//   6(0x06)
TSS_EXPORT TSS_Error tss_getUntaredOrientationAsQuaternion(TSS_Device_Id device, float * quat4, unsigned int * timestamp);
//   7(0x07)
TSS_EXPORT TSS_Error tss_getUntaredOrientationAsEulerAngles(TSS_Device_Id device, float * euler3, unsigned int * timestamp);
//   8(0x08)
TSS_EXPORT TSS_Error tss_getUntaredOrientationAsRotationMatrix(TSS_Device_Id device, float * matrix9, unsigned int * timestamp);
//   9(0x09)
TSS_EXPORT TSS_Error tss_getUntaredOrientationAsAxisAngle(TSS_Device_Id device, float * axis_angle4, unsigned int * timestamp);
//  10(0x0a)
TSS_EXPORT TSS_Error tss_getUntaredOrientationAsTwoVector(TSS_Device_Id device, float * north3, float * gravity3, unsigned int * timestamp);
//  11(0x0b)
TSS_EXPORT TSS_Error tss_getTaredTwoVectorInSensorFrame(TSS_Device_Id device, float * forward3, float * down3, unsigned int * timestamp);
//  12(0x0c)
TSS_EXPORT TSS_Error tss_getUntaredTwoVectorInSensorFrame(TSS_Device_Id device, float * north3, float * gravity3, unsigned int * timestamp);

//Embedded Commands
//  29(0x1d)
TSS_EXPORT TSS_Error tss_setInterruptType(TSS_Device_Id device, unsigned char mode, unsigned char pin, unsigned int * timestamp);
//  30(0x1e)
TSS_EXPORT TSS_Error tss_getInterruptType(TSS_Device_Id device, unsigned char * mode, unsigned char * pin, unsigned int * timestamp);
//  31(0x1f)
TSS_EXPORT TSS_Error tss_getInterruptStatus(TSS_Device_Id device, unsigned char * status, unsigned int * timestamp);

//Normalized Data Commands
//  32(0x20)
TSS_EXPORT TSS_Error tss_getAllNormalizedComponentSensorData(TSS_Device_Id device, float * gyro_rate3, float * gravity_direction3, float * north_direction3, unsigned int * timestamp);
//  33(0x21)
TSS_EXPORT TSS_Error tss_getNormalizedGyroRate(TSS_Device_Id device, float * gyro_rate3, unsigned int * timestamp);
//  34(0x22)
TSS_EXPORT TSS_Error tss_getNormalizedAccelerometerVector(TSS_Device_Id device, float * gravity_direction3, unsigned int * timestamp);
//  35(0x23)
TSS_EXPORT TSS_Error tss_getNormalizedCompassVector(TSS_Device_Id device, float * north_direction3, unsigned int * timestamp);

//Corrected Data Commands
//  37(0x25)
TSS_EXPORT TSS_Error tss_getAllCorrectedComponentSensorData(TSS_Device_Id device, float * gyro_rate3, float * accelerometer3, float * compass3, unsigned int * timestamp);
//  38(0x26)
TSS_EXPORT TSS_Error tss_getCorrectedGyroRate(TSS_Device_Id device, float * gyro_rate3, unsigned int * timestamp);
//  39(0x27)
TSS_EXPORT TSS_Error tss_getCorrectedAccelerometerVector(TSS_Device_Id device, float * accelerometer3, unsigned int * timestamp);
//  40(0x28)
TSS_EXPORT TSS_Error tss_getCorrectedCompassVector(TSS_Device_Id device, float * compass3, unsigned int * timestamp);
//  41(0x29)
TSS_EXPORT TSS_Error tss_getCorrectedLinearAccelerationInGlobalSpace(TSS_Device_Id device, float * accelerometer3, unsigned int * timestamp);

//Other Data Commands
//  43(0x2b)
TSS_EXPORT TSS_Error tss_getTemperatureC(TSS_Device_Id device, float * temperature, unsigned int * timestamp);
//  44(0x2c)
TSS_EXPORT TSS_Error tss_getTemperatureF(TSS_Device_Id device, float * temperature, unsigned int * timestamp);
//  45(0x2d)
TSS_EXPORT TSS_Error tss_getConfidenceFactor(TSS_Device_Id device, float * confidence, unsigned int * timestamp);

//Data-Logging Commands
//  57(0x39)
TSS_EXPORT TSS_Error tss_turnOnMassStorage(TSS_Device_Id device, unsigned int * timestamp);
//  58(0x3a)
TSS_EXPORT TSS_Error tss_turnOffMassStorage(TSS_Device_Id device, unsigned int * timestamp);
//  59(0x3b)
TSS_EXPORT TSS_Error tss_formatAndInitializeSDCard(TSS_Device_Id device, unsigned int * timestamp);
//  60(0x3c)
TSS_EXPORT TSS_Error tss_beginDataLoggingSession(TSS_Device_Id device, unsigned int * timestamp);
//  61(0x3d)
TSS_EXPORT TSS_Error tss_endDataLoggingSession(TSS_Device_Id device, unsigned int * timestamp);
//  62(0x3e)
TSS_EXPORT TSS_Error tss_setClockValues(TSS_Device_Id device,   unsigned char month,
                                                                unsigned char day,
                                                                unsigned char year,
                                                                unsigned char hour,
                                                                unsigned char minute,
                                                                unsigned char second,
                                                                unsigned int * timestamp);
//  63(0x3f)
TSS_EXPORT TSS_Error tss_getClockValues(TSS_Device_Id device,    unsigned char * month,
                                                                unsigned char * day,
                                                                unsigned char * year,
                                                                unsigned char * hour,
                                                                unsigned char * minute,
                                                                unsigned char * second,
                                                                unsigned int * timestamp);

//Raw Data Commands
//  64(0x40)
TSS_EXPORT TSS_Error tss_getAllRawComponentSensorData(TSS_Device_Id device, float * gyro_rate3, float * accelerometer3, float * compass3, unsigned int * timestamp);
//  65(0x41)
TSS_EXPORT TSS_Error tss_getRawGyroscopeRate(TSS_Device_Id device, float * gyro_rate3, unsigned int * timestamp);
//  66(0x42)
TSS_EXPORT TSS_Error tss_getRawAccelerometerData(TSS_Device_Id device, float * accelerometer3, unsigned int * timestamp);
//  67(0x43)
TSS_EXPORT TSS_Error tss_getRawCompassData(TSS_Device_Id device, float * compass3, unsigned int * timestamp);

//Streaming Commands
//  80(0x50)
TSS_EXPORT TSS_Error tss_setStreamingSlots(TSS_Device_Id device, const TSS_Stream_Command * slots8, unsigned int * timestamp);
//  81(0x51)
TSS_EXPORT TSS_Error tss_getStreamingSlots(TSS_Device_Id device, TSS_Stream_Command * slots8, unsigned int * timestamp);
//  82(0x52)
TSS_EXPORT TSS_Error tss_setStreamingTiming(TSS_Device_Id device, unsigned int interval, unsigned int duration, unsigned int delay, unsigned int * timestamp);
//  83(0x53)
TSS_EXPORT TSS_Error tss_getStreamingTiming(TSS_Device_Id device, unsigned int * interval, unsigned int * duration, unsigned int * delay, unsigned int * timestamp);
//  84(0x54)
TSS_EXPORT TSS_Error tss_getStreamingBatch(TSS_Device_Id device, char * output_data, unsigned int output_data_len, unsigned int * timestamp);
//  85(0x55)
TSS_EXPORT TSS_Error tss_startStreaming(TSS_Device_Id device, unsigned int * timestamp);
//  86(0x56)
TSS_EXPORT TSS_Error tss_stopStreaming(TSS_Device_Id device, unsigned int * timestamp);
//  95(0x5f)
TSS_EXPORT TSS_Error tss_updateCurrentTimestamp(TSS_Device_Id device, unsigned int set_timestamp, unsigned int * timestamp);

//Configuration Write Commands
//  16(0x10)
TSS_EXPORT TSS_Error tss_setEulerAngleDecompositionOrder(TSS_Device_Id device, unsigned char order, unsigned int * timestamp);
//  17(0x11)
TSS_EXPORT TSS_Error tss_setMagnetoresistiveThreshold(TSS_Device_Id device, float threshold, unsigned int trust_frames, float lockout_decay, float perturbation_detection_value, unsigned int * timestamp);
//  18(0x12)
TSS_EXPORT TSS_Error tss_setAccelerometerResistanceThreshold(TSS_Device_Id device, float threshold, unsigned int lockout_frames, unsigned int * timestamp);
//  19(0x13)
TSS_EXPORT TSS_Error tss_offsetWithCurrentOrientation(TSS_Device_Id device, unsigned int * timestamp);
//  20(0x14)
TSS_EXPORT TSS_Error tss_resetBaseOffset(TSS_Device_Id device, unsigned int * timestamp);
//  21(0x15)
TSS_EXPORT TSS_Error tss_offsetWithQuaternion(TSS_Device_Id device, const float * quat4, unsigned int * timestamp);
//  22(0x16)
TSS_EXPORT TSS_Error tss_setBaseOffsetWithCurrentOrientation(TSS_Device_Id device, unsigned int * timestamp);
//  96(0x60)
TSS_EXPORT TSS_Error tss_tareWithCurrentOrientation(TSS_Device_Id device, unsigned int * timestamp);
//  97(0x61)
TSS_EXPORT TSS_Error tss_tareWithQuaternion(TSS_Device_Id device, const float * quat4, unsigned int * timestamp);
//  98(0x62)
TSS_EXPORT TSS_Error tss_tareWithRotationMatrix(TSS_Device_Id device, const float * matrix9, unsigned int * timestamp);
//  99(0x63)
//TSS_EXPORT TSS_Error tss_setStaticAccelerometerRhoMode(TSS_Device_Id device, float rho_value, unsigned int * timestamp);
TSS_EXPORT TSS_Error tss_setStaticAccelerometerTrustValue(TSS_Device_Id device, float trust_value, unsigned int * timestamp);
// 100(0x64)
//TSS_EXPORT TSS_Error tss_setConfidenceAccelerometerRhoMode(TSS_Device_Id device, float min_rho_value, float max_rho_value, unsigned int * timestamp);
TSS_EXPORT TSS_Error tss_setConfidenceAccelerometerTrustValues(TSS_Device_Id device, float min_trust_value, float max_trust_value, unsigned int * timestamp);
// 101(0x65)
//TSS_EXPORT TSS_Error tss_setStaticCompassRhoMode(TSS_Device_Id device, float rho_value, unsigned int * timestamp);
TSS_EXPORT TSS_Error tss_setStaticCompassTrustValue(TSS_Device_Id device, float trust_value, unsigned int * timestamp);
// 102(0x66)
//TSS_EXPORT TSS_Error tss_setConfidenceCompassRhoMode(TSS_Device_Id device, float min_rho_value, float max_rho_value, unsigned int * timestamp);
TSS_EXPORT TSS_Error tss_setConfidenceCompassTrustValues(TSS_Device_Id device, float min_trust_value, float max_trust_value, unsigned int * timestamp);
// 103(0x67)
TSS_EXPORT TSS_Error tss_setDesiredUpdateRate(TSS_Device_Id device, unsigned int update_rate, unsigned int * timestamp);
// 105(0x69)
TSS_EXPORT TSS_Error tss_setReferenceVectorMode(TSS_Device_Id device, unsigned char mode, unsigned int * timestamp);
// 106(0x6a)
TSS_EXPORT TSS_Error tss_setOversampleRate(TSS_Device_Id device, unsigned char samples_per_iteration, unsigned int * timestamp);
// 107(0x6b)
TSS_EXPORT TSS_Error tss_setGyroscopeEnabled(TSS_Device_Id device, unsigned char enabled, unsigned int * timestamp);
// 108(0x6c)
TSS_EXPORT TSS_Error tss_setAccelerometerEnabled(TSS_Device_Id device, unsigned char enabled, unsigned int * timestamp);
// 109(0x6d)
TSS_EXPORT TSS_Error tss_setCompassEnabled(TSS_Device_Id device, unsigned char enabled, unsigned int * timestamp);
// 116(0x74)
TSS_EXPORT TSS_Error tss_setAxisDirections(TSS_Device_Id device, unsigned char axis_direction_byte, unsigned int * timestamp);
// 117(0x75)
TSS_EXPORT TSS_Error tss_setRunningAveragePercent(TSS_Device_Id device, float running_average_percent, unsigned int * timestamp);
// 118(0x76)
TSS_EXPORT TSS_Error tss_setCompassReferenceVector(TSS_Device_Id device, const float * reference_vector3, unsigned int * timestamp);
// 119(0x77)
TSS_EXPORT TSS_Error tss_setAccelerometerReferenceVector(TSS_Device_Id device, const float * reference_vector3, unsigned int * timestamp);
// 120(0x78)
TSS_EXPORT TSS_Error tss_resetKalmanFilter(TSS_Device_Id device, unsigned int * timestamp);
// 121(0x79)
TSS_EXPORT TSS_Error tss_setAccelerometerRange(TSS_Device_Id device, unsigned char accelerometer_range_setting, unsigned int * timestamp);
// 123(0x7b)
TSS_EXPORT TSS_Error tss_setFilterMode(TSS_Device_Id device, unsigned char mode, unsigned int * timestamp);
// 124(0x7c)
TSS_EXPORT TSS_Error tss_setRunningAverageMode(TSS_Device_Id device, unsigned char mode, unsigned int * timestamp);
// 125(0x7d)
TSS_EXPORT TSS_Error tss_setGyroscopeRange(TSS_Device_Id device, unsigned char gyroscope_range_setting, unsigned int * timestamp);
// 126(0x7e)
TSS_EXPORT TSS_Error tss_setCompassRange(TSS_Device_Id device, unsigned char compass_range_setting, unsigned int * timestamp);

// Configuration Read Commands
// 128(0x80)  Get tare orientation as quaternion
TSS_EXPORT TSS_Error tss_getTareAsQuaternion(TSS_Device_Id device, float * quat4, unsigned int * timestamp);
// 129(0x81)  Get tare orientation as rotation matrix
TSS_EXPORT TSS_Error tss_getTareAsRotationMatrix(TSS_Device_Id device, float * matrix9, unsigned int * timestamp);
// 130(0x82)
//TSS_EXPORT TSS_Error tss_getAccelerometerRhoValue(TSS_Device_Id device, unsigned char * rho_mode, float * rho_min, float * rho_max, unsigned int * timestamp);
TSS_EXPORT TSS_Error tss_getAccelerometerTrustValues(TSS_Device_Id device, float * min_trust_value, float * max_trust_value, unsigned int * timestamp);
// 131(0x83)
//TSS_EXPORT TSS_Error tss_getCompassRhoValue(TSS_Device_Id device, unsigned char * rho_mode, float * rho_min, float * rho_max, unsigned int * timestamp);
TSS_EXPORT TSS_Error tss_getCompassTrustValues(TSS_Device_Id device, float * min_trust_value, float * max_trust_value, unsigned int * timestamp);
// 132(0x84)
TSS_EXPORT TSS_Error tss_getCurrentUpdateRate(TSS_Device_Id device, unsigned int * last_update, unsigned int * timestamp);
// 133(0x85)
TSS_EXPORT TSS_Error tss_getCompassReferenceVector(TSS_Device_Id device, float * reference_vector, unsigned int * timestamp);
// 134(0x86)
TSS_EXPORT TSS_Error tss_getAccelerometerReferenceVector(TSS_Device_Id device, float * reference_vector, unsigned int * timestamp);
// 135(0x87)
TSS_EXPORT TSS_Error tss_getReferenceVectorMode(TSS_Device_Id device, unsigned char * mode, unsigned int * timestamp);
// 140(0x8c)
TSS_EXPORT TSS_Error tss_getGyroscopeEnabledState(TSS_Device_Id device, unsigned char * enabled, unsigned int * timestamp);
// 141(0x8d)
TSS_EXPORT TSS_Error tss_getAccelerometerEnabledState(TSS_Device_Id device, unsigned char * enabled, unsigned int * timestamp);
// 142(0x8e)
TSS_EXPORT TSS_Error tss_getCompassEnabledState(TSS_Device_Id device, unsigned char * enabled, unsigned int * timestamp);
// 143(0x8f)
TSS_EXPORT TSS_Error tss_getAxisDirections(TSS_Device_Id device, unsigned char * axis_directions, unsigned int * timestamp);
// 144(0x90)
TSS_EXPORT TSS_Error tss_getOversampleRate(TSS_Device_Id device, unsigned char * oversample_rate, unsigned int * timestamp);
// 145(0x91)
TSS_EXPORT TSS_Error tss_getRunningAveragePercent(TSS_Device_Id device, float * running_average_percent, unsigned int * timestamp);
// 146(0x92)
TSS_EXPORT TSS_Error tss_getDesiredUpdateRate(TSS_Device_Id device, unsigned int * update_rate, unsigned int * timestamp);
// 148(0x94)
TSS_EXPORT TSS_Error tss_getAccelerometerRange(TSS_Device_Id device, unsigned char * accelerometer_range_setting, unsigned int * timestamp);
// 152(0x98)
TSS_EXPORT TSS_Error tss_getFilterMode(TSS_Device_Id device, unsigned char * mode, unsigned int * timestamp);
// 153(0x99)
TSS_EXPORT TSS_Error tss_getRunningAverageMode(TSS_Device_Id device, unsigned char * mode, unsigned int * timestamp);
// 154(0x9a)
TSS_EXPORT TSS_Error tss_getGyroscopeRange(TSS_Device_Id device, unsigned char * gyroscope_range_setting, unsigned int * timestamp);
// 155(0x9b)
TSS_EXPORT TSS_Error tss_getCompassRange(TSS_Device_Id device, unsigned char * compass_range_setting, unsigned int * timestamp);
// 156(0x9c)
TSS_EXPORT TSS_Error tss_getEulerAngleDecompositionOrder(TSS_Device_Id device, unsigned char * order, unsigned int * timestamp);
// 157(0x9d)
TSS_EXPORT TSS_Error tss_getMagnetoresistiveThreshold(TSS_Device_Id device, float * threshold, unsigned int * trust_frames, float * lockout_decay, float * perturbation_detection_value, unsigned int * timestamp);
// 158(0x9e)
TSS_EXPORT TSS_Error tss_getAccelerometerResistanceThreshold(TSS_Device_Id device, float * threshold, unsigned int * lockout_frames, unsigned int * timestamp);
// 159(0x9f)
TSS_EXPORT TSS_Error tss_getOffsetOrientationAsQuaternion(TSS_Device_Id device, float * quat4, unsigned int * timestamp);

//Calibration Commands
// 160(0xa0)
TSS_EXPORT TSS_Error tss_setCompassCalibrationCoefficients(TSS_Device_Id device, const float * matrix9, const float * bias3, unsigned int * timestamp);
// 161(0xa1)
TSS_EXPORT TSS_Error tss_setAccelerometerCalibrationCoefficients(TSS_Device_Id device,const float * matrix9, const float * bias3, unsigned int * timestamp);
// 162(0xa2)
TSS_EXPORT TSS_Error tss_getCompassCalibrationCoefficients(TSS_Device_Id device, float * matrix9, float * bias3, unsigned int * timestamp);
// 163(0xa3)
TSS_EXPORT TSS_Error tss_getAccelerometerCalibrationCoefficients(TSS_Device_Id device, float * matrix9, float * bias3, unsigned int * timestamp);
// 164(0xa4)
TSS_EXPORT TSS_Error tss_getGyroscopeCalibrationCoefficients(TSS_Device_Id device, float * matrix9, float * bias3, unsigned int * timestamp);
// 165(0xa5)
TSS_EXPORT TSS_Error tss_beginGyroscopeAutoCalibration(TSS_Device_Id device, unsigned int * timestamp);
// 166(0xa6)
TSS_EXPORT TSS_Error tss_setGyroscopeCalibrationCoefficients(TSS_Device_Id device, const float * matrix9, const float * bias3, unsigned int * timestamp);
// 169(0xa9)
TSS_EXPORT TSS_Error tss_setCalibrationMode(TSS_Device_Id device, unsigned char mode, unsigned int * timestamp);
// 170(0xaa)
TSS_EXPORT TSS_Error tss_getCalibrationMode(TSS_Device_Id device, unsigned char * mode, unsigned int * timestamp);
// 171(0xab)
TSS_EXPORT TSS_Error tss_setOrthoCalibrationDataPointFromCurrentOrientation(TSS_Device_Id device, unsigned int * timestamp);
// 172(0xac)
TSS_EXPORT TSS_Error tss_setOrthoCalibrationDataPointFromVector(TSS_Device_Id device, unsigned char type, unsigned char index, const float * vector3, unsigned int * timestamp);
// 173(0xad)
TSS_EXPORT TSS_Error tss_getOrthoCalibrationDataPoint(TSS_Device_Id device, unsigned char type, unsigned char index, float * vector3, unsigned int * timestamp);
// 174(0xae)
TSS_EXPORT TSS_Error tss_performOrthoCalibration(TSS_Device_Id device, unsigned int * timestamp);
// 175(0xaf)
TSS_EXPORT TSS_Error tss_clearOrthoCalibrationData(TSS_Device_Id device, unsigned int * timestamp);

//Dongle Commands
// 176(0xb0)
TSS_EXPORT TSS_Error tss_setWirelessStreamingAutoFlushMode(TSS_Device_Id device, unsigned char mode, unsigned int * timestamp);
// 177(0xb1)
TSS_EXPORT TSS_Error tss_getWirelessStreamingAutoFlushMode(TSS_Device_Id device, unsigned char * mode, unsigned int * timestamp);
// 178(0xb2)
TSS_EXPORT TSS_Error tss_setWirelessStreamingManualFlushBitfield(TSS_Device_Id device, unsigned short manual_flush_bitfield, unsigned int * timestamp);
// 179(0xb3)
TSS_EXPORT TSS_Error tss_getWirelessStreamingManualFlushBitfield(TSS_Device_Id device, unsigned short * manual_flush_bitfield, unsigned int * timestamp);
// 180(0xb4)
TSS_EXPORT TSS_Error tss_getManualFlushSingle(TSS_Device_Id device, unsigned char index, char * data, int in_data_size, int * out_data_size, unsigned int * timestamp);
// 181(0xb5)
TSS_EXPORT TSS_Error tss_getManualFlushBulk(TSS_Device_Id device, char * data, int in_data_size, int * out_data_size, unsigned int * timestamp);
// 182(0xb6)
TSS_EXPORT TSS_Error tss_broadcastSynchronizationPulse(TSS_Device_Id device, unsigned int * timestamp);
// 208(0xd0)
TSS_EXPORT TSS_Error tss_getSerialNumberAtLogicalID(TSS_Device_Id device, unsigned char logical_id, unsigned int * serial_number, unsigned int * timestamp);
// 209(0xd1)
TSS_EXPORT TSS_Error tss_setSerialNumberAtLogicalID(TSS_Device_Id device, unsigned char logical_id, unsigned int serial_number, unsigned int * timestamp);
// 210(0xd2)
TSS_EXPORT TSS_Error tss_getWirelessChannelNoiseLevels(TSS_Device_Id device, unsigned char * channel_strengths16, unsigned int * timestamp);
// 211(0xd3)
TSS_EXPORT TSS_Error tss_setWirelessRetries(TSS_Device_Id device, unsigned char retries, unsigned int * timestamp);
// 212(0xd4)
TSS_EXPORT TSS_Error tss_getWirelessRetries(TSS_Device_Id device, unsigned char * retries, unsigned int * timestamp);
// 213(0xd5)
TSS_EXPORT TSS_Error tss_getWirelessSlotsOpen(TSS_Device_Id device, unsigned char * slots_open, unsigned int * timestamp);
// 214(0xd6)
TSS_EXPORT TSS_Error tss_getSignalStrength(TSS_Device_Id device, unsigned char * last_packet_signal_strength, unsigned int * timestamp);
// 219(0xdb)
TSS_EXPORT TSS_Error tss_setWirelessResponseHeaderBitfield(TSS_Device_Id device, unsigned int header_bitfield, unsigned int * timestamp);
// 220(0xdc)
TSS_EXPORT TSS_Error tss_getWirelessResponseHeaderBitfield(TSS_Device_Id device, unsigned int * header_bitfield, unsigned int * timestamp);

//Wireless Sensor & Dongle Commands
// 192(0xc0)
TSS_EXPORT TSS_Error tss_getWirelessPanID(TSS_Device_Id device, unsigned short * pan_id, unsigned int * timestamp);
// 193(0xc1)
TSS_EXPORT TSS_Error tss_setWirelessPanID(TSS_Device_Id device, unsigned short pan_id, unsigned int * timestamp);
// 194(0xc2)
TSS_EXPORT TSS_Error tss_getWirelessChannel(TSS_Device_Id device, unsigned char * channel, unsigned int * timestamp);
// 195(0xc3)
TSS_EXPORT TSS_Error tss_setWirelessChannel(TSS_Device_Id device, unsigned char channel, unsigned int * timestamp);
// 197(0xc5)
TSS_EXPORT TSS_Error tss_commitWirelessSettings(TSS_Device_Id device, unsigned int * timestamp);
// 198(0xc6)
TSS_EXPORT TSS_Error tss_getWirelessAddress(TSS_Device_Id device, unsigned short * address, unsigned int * timestamp);

//Battery Commands
// 201(0xc9)
TSS_EXPORT TSS_Error tss_getBatteryVoltage(TSS_Device_Id device, float * battery_voltage, unsigned int * timestamp);
// 202(0xca)
TSS_EXPORT TSS_Error tss_getBatteryPercentRemaining(TSS_Device_Id device, unsigned char * battery_percent, unsigned int * timestamp);
// 203(0xcb)
TSS_EXPORT TSS_Error tss_getBatteryStatus(TSS_Device_Id device, unsigned char * battery_charge_status, unsigned int * timestamp);

//General Commands
// 196(0xc4)
TSS_EXPORT TSS_Error tss_setLEDMode(TSS_Device_Id device, unsigned char mode, unsigned int * timestamp);
// 200(0xc8)
TSS_EXPORT TSS_Error tss_getLEDMode(TSS_Device_Id device, unsigned char * mode, unsigned int * timestamp);
// 221(0xdd)
TSS_EXPORT TSS_Error tss_setWiredResponseHeaderBitfield(TSS_Device_Id device, unsigned int header_bitfield, unsigned int * timestamp);
// 222(0xde)
TSS_EXPORT TSS_Error tss_getWiredResponseHeaderBitfield(TSS_Device_Id device, unsigned int * header_bitfield, unsigned int * timestamp);
// 223(0xdf) note: adds null terminator
TSS_EXPORT TSS_Error tss_getFirmwareVersionString(TSS_Device_Id device, char * firmware_version13, unsigned int * timestamp);
// 224(0xe0)
TSS_EXPORT TSS_Error tss_restoreFactorySettings(TSS_Device_Id device, unsigned int * timestamp);
// 225(0xe1)
TSS_EXPORT TSS_Error tss_commitSettings(TSS_Device_Id device, unsigned int * timestamp);
// 226(0xe2)
TSS_EXPORT TSS_Error tss_softwareReset(TSS_Device_Id device, unsigned int * timestamp);
// 227(0xe3)
TSS_EXPORT TSS_Error tss_setSleepMode(TSS_Device_Id device,unsigned char mode, unsigned int * timestamp);
// 228(0xe4)
TSS_EXPORT TSS_Error tss_getSleepMode(TSS_Device_Id device, unsigned char * mode, unsigned int * timestamp);
// 229(0xe5)
TSS_EXPORT TSS_Error tss_enterBootloaderMode(TSS_Device_Id device, unsigned int * timestamp);
// 230(0xe6) note: adds null terminator
TSS_EXPORT TSS_Error tss_getHardwareVersionString(TSS_Device_Id device, char * hardware_version33, unsigned int * timestamp);
// 231(0xe7)
TSS_EXPORT TSS_Error tss_setUARTBaudRate(TSS_Device_Id device, unsigned int baud_rate, unsigned int * timestamp);
// 232(0xe8)
TSS_EXPORT TSS_Error tss_getUARTBaudRate(TSS_Device_Id device, unsigned int * baud_rate, unsigned int * timestamp);
// 233(0xe9)
TSS_EXPORT TSS_Error tss_setUSBMode(TSS_Device_Id device, unsigned int mode, unsigned int * timestamp);
// 234(0xea)
TSS_EXPORT TSS_Error tss_getUSBMode(TSS_Device_Id device, unsigned int * mode, unsigned int * timestamp);
// 237(0xed)
TSS_EXPORT TSS_Error tss_getSerialNumber(TSS_Device_Id device, unsigned int * serial_number, unsigned int * timestamp);
// 238(0xee)
TSS_EXPORT TSS_Error tss_setLEDColor(TSS_Device_Id device,const float * rgb_color3, unsigned int * timestamp);
// 239(0xef)
TSS_EXPORT TSS_Error tss_getLEDColor(TSS_Device_Id device, float * rgb_color3, unsigned int * timestamp);

//Wireless HID Commands
TSS_EXPORT TSS_Error tss_setWirelessHIDUpdateRate(TSS_Device_Id device, unsigned char HID_update_rate, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getWirelessHIDUpdateRate(TSS_Device_Id device, unsigned char * HID_update_rate, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_setWirelessHIDAsynchronousMode(TSS_Device_Id device, unsigned char HID_communication_mode , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getWirelessHIDAsynchronousMode(TSS_Device_Id device, unsigned char * HID_communication_mode , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getWirelessHIDAsynchronousMode(TSS_Device_Id device, unsigned char * HID_communication_mode , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_setJoystickLogicalID(TSS_Device_Id device, unsigned char logical_ID , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_setMouseLogicalID(TSS_Device_Id device, unsigned char * logical_ID , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getJoystickLogicalID(TSS_Device_Id device, unsigned char * logical_ID , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getMouseLogicalID(TSS_Device_Id device, unsigned char * logical_ID , unsigned int * timestamp);

//Wired HID Commands
TSS_EXPORT TSS_Error tss_setJoystickEnabled(TSS_Device_Id device, unsigned char enabled_state , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_setMouseEnabled(TSS_Device_Id device, unsigned char enabled_state , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getJoystickEnabled(TSS_Device_Id device, unsigned char * enabled_state , unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getMouseEnabled(TSS_Device_Id device, unsigned char * enabled_state , unsigned int * timestamp);

//General HID Commands
TSS_EXPORT TSS_Error tss_setControlMode(TSS_Device_Id device, unsigned char control_class, unsigned char control_index, unsigned char handler_index, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_setControlData(TSS_Device_Id device, unsigned char control_class, unsigned char control_index, unsigned char data_point_index, float data_point, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getControlMode(TSS_Device_Id device, unsigned char control_class, unsigned char control_index, unsigned char * handler_index, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getControlData(TSS_Device_Id device, unsigned char control_class, unsigned char control_index, unsigned char data_point_index, float * data_point, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_setButtonGyroDisableLength(TSS_Device_Id device, unsigned char number_of_frames, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getButtonGyroDisableLength(TSS_Device_Id device, unsigned char * number_of_frames, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getButtonState(TSS_Device_Id device, unsigned char * button_state, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_setMouseAbsoluteRelativeMode(TSS_Device_Id device, unsigned char mode, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getMouseAbsoluteRelativeMode(TSS_Device_Id device, unsigned char * mode, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_setJoystickAndMousePresentRemoved(TSS_Device_Id device, unsigned char joystick, unsigned char mouse, unsigned int * timestamp);

TSS_EXPORT TSS_Error tss_getJoystickAndMousePresentRemoved(TSS_Device_Id device, unsigned char * joystick, unsigned char * mouse, unsigned int * timestamp);



#ifdef __cplusplus
    }
#endif

#endif // YEI_THREESPACE_API_H_INCLUDED
