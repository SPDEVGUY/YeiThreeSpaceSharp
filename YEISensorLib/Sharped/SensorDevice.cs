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

        /// <summary>
        /// Create a sensor using the provided ComPort.
        /// </summary>
        /// <param name="port">The port to connect with.</param>
        public SensorDevice(ComPort port)
        {
            _port = port;
            _deviceId = ThreeSpaceInterop.CreateDevice(port.PortName, port.SensorType);
            IsConnected = _deviceId != Defines.NO_DEVICE_ID;
            if (IsConnected)
            {
                LoadSerialNumber();
                IsDongle = port.SensorType == SensorTypeEnum.DNG;
            }
        }

        /// <summary>
        /// Returns the filtered tared quaternion direct from the sensor.
        /// </summary>
        /// <returns></returns>
        public Quaternion GetQuaternion()
        {
            if (!IsConnected || IsDongle) return new Quaternion();
            Quaternion result;
            ThreeSpaceInterop.GetFilteredTaredOrientedQuaterion(_deviceId, out result);

            return result;
        }

        /// <summary>
        /// Returns the filtered tared euler angles from the sensor.
        /// </summary>
        /// <returns></returns>
        public Euler GetEulerAngles()
        {
            if (!IsConnected || IsDongle) return new Euler();
            Euler result;
            ThreeSpaceInterop.GetFilteredTaredOrientedEuler(_deviceId, out result);

            return result;
        }

        /// <summary>
        /// Returns the filtered tared quaternion direct from the sensor.
        /// </summary>
        /// <returns></returns>
        public SensorData GetNormalizedSensorData()
        {
            if (!IsConnected || IsDongle) return new SensorData();
            SensorData result = new SensorData();
            ThreeSpaceInterop.GetNormalizedSensorData(_deviceId, ref result);

            return result;
        }

        /// <summary>
        /// Returns a confidence level, 0 being not trustworthy, and 1 being fully trustworthy.
        /// This is the result of movement and being near a magnetic field.
        /// </summary>
        /// <returns></returns>
        public double GetConfidence()
        {
            if (!IsConnected || IsDongle) return 0;
            float result ;
            ThreeSpaceInterop.GetConfidence(_deviceId, out result);

            return result;
        }

        public void SetLedColour(Color color)
        {
            var resultCode = ThreeSpaceInterop.SetLedColor(_deviceId, color);
        }
        public Color GetLedColour()
        {
            Color result =new Color();
            result.R = 0;
            result.G = 0;
            result.B = 0;
            var resultCode = ThreeSpaceInterop.GetLedColor(_deviceId, out result);

            return result;
        }

        private void LoadSerialNumber()
        {
            var buffer = new byte[9];
            ThreeSpaceInterop.GetSerialNumber(_deviceId, buffer);
            SerialNumber = Encoding.ASCII.GetString(buffer);
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
