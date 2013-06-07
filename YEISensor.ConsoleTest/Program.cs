using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEISensorLib.RawApi;
using YEISensorLib.Sharped;

namespace YEISensor.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var device = SensorDevices.GetFirstAvailable(FilterEnum.FindUSB))
            {
                Console.WriteLine("Port:            {0}", device.PortName);
                Console.WriteLine("Friendly Name:   {0}", device.FriendlyPortName);
                Console.WriteLine("Sensor Type:     {0}", device.SensorType);
                Console.WriteLine("Connected:       {0}", device.IsConnected);
                Console.WriteLine("Serial:          {0}", device.SerialNumber);

                var line = string.Empty;
                while (line == string.Empty)
                {
                    var quat = device.GetQuaternion();
                    Console.WriteLine("Quaternion:  {0:0.000},{1:0.000},{2:0.000},{3:0.000}", quat.W, quat.X, quat.Y, quat.Z);

                    var euler = device.GetEulerAngles();
                    Console.WriteLine("Euler:       {0:0.000},{1:0.000},{2:0.000}", euler.X, euler.Y, euler.Z);

                    var sensorData = device.GetNormalizedSensorData();
                    Console.WriteLine("Sensor data...");
                    Console.WriteLine("Gyro:        {0:0.000},{1:0.000},{2:0.000}", sensorData.GyroX, sensorData.GyroY, sensorData.GyroZ);
                    Console.WriteLine("Accel:       {0:0.000},{1:0.000},{2:0.000}", sensorData.AccelerometerX, sensorData.AccelerometerY, sensorData.AccelerometerZ);
                    Console.WriteLine("Compass:     {0:0.000},{1:0.000},{2:0.000}", sensorData.CompassX, sensorData.CompassY, sensorData.CompassZ);


                    var color = new Color
                                    {
                                        R = (sensorData.AccelerometerX +1) /2,
                                        G = (sensorData.AccelerometerY + 1) / 2,
                                        B = (sensorData.AccelerometerZ + 1) / 2,
                                    };
                    device.SetLedColour(color);
                    color = device.GetLedColour();
                    Console.WriteLine("LED:     {0:0.000},{1:0.000},{2:0.000}", color.R, color.G, color.B);


                    line = Console.ReadLine();
                }

            }


            Console.ReadLine();

        }
    }
}
