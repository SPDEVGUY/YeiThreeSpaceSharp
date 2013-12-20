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
    public struct Vector3F
    {
        public float X;
        public float Y;
        public float Z;
    }
}
