YeiThreeSpaceSharp
==================

C# Implementation of the YEI ThreeSpace API.

C/C++ Source: http://www.yeitechnology.com/3-space-sensor-application-programming-interface

Current implementation is based off the 2.0.5.2 C API
http://forum.yeitechnology.com/viewtopic.php?f=12&t=27

^-- Download the firmware from there, and ensure your device is updated

This code has been tested and confirmed working with the USB/RS232 sensor model.
(My specific model is the screw down case USB v2)
http://www.yeitechnology.com/3-space-product-family-category/usb

Happy hacking!

Feel free to implement more of the API as needed.  I just wanted the euler angles and a way to set the LED.

Current functionality:
- Get first available sensor
- Get all sensors
- Wrapped sensor devices in OOP style C#.
- Get normalized sensor data (Gyro, Accel, Compass)
- Get Current Euler Angles
- Get Current Quaternion 
- Tare device at current orientation
- Get LED Colour
- Set LED Colour

YEISensorLib - The wrapped ThreeSpace_API.dll
---------------

Sample usage, with only one device:
using (var device = SensorDevices.GetFirstAvailable())
{
   device.Tare(); //Initialize
   var line = string.Empty;
   while (line == string.Empty)
   {
      var sensorDataResult = device.GetNormalizedSensorData();

      Console.WriteLine("Gyro:        {0:0.000},{1:0.000},{2:0.000}", 
            device.Gyro.X, device.Gyro.Y, device.Gyro.Z);

      Console.WriteLine("Accel:       {0:0.000},{1:0.000},{2:0.000}", 
            device.Accelerometer.X, device.Accelerometer.Y, device.Accelerometer.Z);

      Console.WriteLine("Compass:     {0:0.000},{1:0.000},{2:0.000}", 
            device.Compass.X, device.Compass.Y, device.Compass.Z);

      Console.WriteLine("TimeStamp:   {0}", device.TimeStamp);

      line = Console.ReadLine();
   }
}


-- My name was set wrong in my config (fixed it).  I'm actually Kevin Cole!  Hello from Edmonton, Alberta, Canada!