using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    
    /**
    * \brief A storage structure for orientations expressed as rotations about the three cartesian axes.
    *
    * TSS_Euler values returned from API calls always return with the assumed rotation order X (Pitch), Y (Yaw), Z (Roll).
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Euler
    {
        /**
        * \brief The x component of the TSS_Euler.
        */
        public float X;
        /**
        * \brief The y component of the TSS_Euler.
        */
        public float Y;
        /**
        * \brief The z component of the TSS_Euler.
        */
        public float Z;
    }
}
