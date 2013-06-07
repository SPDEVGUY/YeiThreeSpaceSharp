using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{


    /**
    * \brief A storage structure for orientations expressed as an Axis/Angle pair.
    *
    * The stored orientation holds a normalized three dimensional vector and an angle in radians.
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AxisAngle
    {
        /**
        * \brief The x element of the stored axis.
        */
        public float X;
        /**
        * \brief The y element of the stored axis.
        */
        public float Y;
        /**
        * \brief The z element of the stored axis.
        */
        public float Z;
        /**
        * \brief The angle rotated about the stored axis.
        */
        public float Angle;
    }
}
