using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    
    /**
    * \brief A storage structure for orientations expressed in fourth dimensional space.
    *
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Quaternion
    {
        /**
        * \brief The x component of the quaternion.
        */
        public float X;
        /**
        * \brief The y component of the quaternion.
        */
        public float Y;
        /**
        * \brief The z component of the quaternion.
        */
        public float Z;
        /**
        * \brief The z component of the quaternion.
        */
        public float W;
    }
}
