using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief A storage structure for a dump of all data from a 3-Space Sensor's on-board sensors.
    *
    * The three on-board sensors used on a 3-Space Sensor are a gyroscope, and accelerometer, and a compass.
    * The gyroscope expresses rates of rotations about each of its major axes.
    * The accelerometer expresses any experienced accleration about each of its major axes.
    * The compass expresses a three dimensional vector pointed towards magnetic north.
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SensorData
    {
        /**
        * \brief The rate of rotation about the gyro's X-Axis.
        */
        public float GyroX;
        /**
        * \brief The rate of rotation about the gyro's Y-Axis.
        */
        public float GyroY;
        /**
        * \brief The rate of rotation about the gyro's Z-Axis.
        */
        public float GyroZ;
        /**
        * \brief The amount of acceleration experienced on the accelerometer's X-Axis.
        */
        public float AccelerometerX;
        /**
        * \brief The amount of acceleration experienced on the accelerometer's Y-Axis.
        */
        public float AccelerometerY;
        /**
        * \brief The amount of acceleration experienced on the accelerometer's Z-Axis.
        */
        public float AccelerometerZ;
        /**
        * \brief The x component of the magnetic north vector.
        */
        public float CompassX;
        /**
        * \brief The y component of the magnetic north vector.
        */
        public float CompassY;
        /**
        * \brief The z component of the magnetic north vector.
        */
        public float CompassZ;
    }
}
