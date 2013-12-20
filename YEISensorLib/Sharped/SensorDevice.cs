using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEISensorLib.RawApi;

namespace YEISensorLib.Sharped
{
    public class SensorDevice : IDisposable
    {
        private bool _isDisposed;

        private ComPort _port;
        private readonly uint _deviceId;

        /// <summary>
        /// Returns true if the sensor is connected.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// A bool indicating the sensor device is actually a dongle.
        /// </summary>
        public bool IsDongle { get; private set; }

        /// <summary>
        /// The hex serial number of the sensor
        /// </summary>
        public string SerialNumber { get; private set; }

        /// <summary>
        /// The type of connected sensor
        /// </summary>
        public SensorTypeEnum SensorType { get { return _port.SensorType; } }
        
        /// <summary>
        /// The port name of the sensor
        /// </summary>
        public string PortName { get { return _port.PortName; } }

        /// <summary>
        /// The friendly port name
        /// </summary>
        public string FriendlyPortName { get { return _port.FriendlyName; } }

        public Vector3F Gyro;
        public Vector3F Accelerometer;
        public Vector3F Compass;
        public Euler Euler;
        public Quaternion Quaternion;
        public uint TimeStamp;

        /// <summary>
        /// Create a sensor using the provided ComPort.
        /// </summary>
        /// <param name="port">The port to connect with.</param>
        public SensorDevice(ComPort port)
        {
            _port = port;
            _deviceId = ThreeSpaceInterop.CreateDevice(port.PortName, TimeStampModeEnum.Sensor);
            IsConnected = _deviceId != Defines.NO_DEVICE_ID;
            if (IsConnected)
            {
                LoadSerialNumber();
                IsDongle = port.SensorType == SensorTypeEnum.WirelessDongle;
            }
        }

        /// <summary>
        /// Returns the filtered tared quaternion direct from the sensor.
        /// </summary>
        /// <returns></returns>
        public bool GetQuaternion()
        {
            if (!IsConnected || IsDongle) return false;
            var result = ThreeSpaceInterop.GetTaredOrientationAsQuaternion(_deviceId, out Quaternion, out TimeStamp);

            return result == ResultEnum.NoError;
        }

        /// <summary>
        /// Returns the filtered tared euler angles from the sensor.
        /// </summary>
        /// <returns></returns>
        public bool GetEulerAngles()
        {
            if (!IsConnected || IsDongle) return false;
            var result = ThreeSpaceInterop.GetTaredOrientationAsEulerAngles(_deviceId, out Euler, out TimeStamp);

            return result == ResultEnum.NoError;
        }

        /// <summary>
        /// Returns the filtered tared quaternion direct from the sensor.
        /// </summary>
        /// <returns></returns>
        public bool GetNormalizedSensorData()
        {
            if (!IsConnected || IsDongle) return false;
            var result = ThreeSpaceInterop.GetAllNormalizedComponentSensorData(_deviceId, out Gyro, out Accelerometer, out Compass, out TimeStamp);

            return result == ResultEnum.NoError;
        }

        /// <summary>
        /// Tare the device to the current orientation
        /// </summary>
        public void Tare()
        {
            uint timestamp;
            ThreeSpaceInterop.TareWithCurrentOrientation(_deviceId, out timestamp);
        }

       

        public void SetLedColour(Color color)
        {
            var resultCode = ThreeSpaceInterop.SetLedColor(_deviceId, new [] {color.R,color.G,color.B}, 0);
        }
        public Color GetLedColour()
        {
            uint ignored;

            Color result =new Color();
            result.R = 0;
            result.G = 0;
            result.B = 0;
            var resultCode = ThreeSpaceInterop.GetLedColor(_deviceId, out result, out ignored);

            return result;
        }

        private void LoadSerialNumber()
        {
            var buffer = new byte[9];
            uint timeStamp;
            ThreeSpaceInterop.GetSerialNumber(_deviceId, buffer, out timeStamp);
            SerialNumber = BitConverter.ToString(buffer);
        }


        private SensorDevice() { }

        ~SensorDevice()
        {
            if(!_isDisposed) Dispose();
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            if (IsConnected)
            {
                ThreeSpaceInterop.CloseDevice(_deviceId);
                IsConnected = false;
            }
            _isDisposed = true;
        }
    }
}
