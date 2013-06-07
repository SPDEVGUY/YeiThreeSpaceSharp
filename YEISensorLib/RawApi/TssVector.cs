using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief A storage structure for three dimensional coordinates or values.
    * 
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Vector
    {
        /**
        * \brief The first component of the vector.
        */
        public float X;
        /**
        * \brief The second component of the vector.
        */
        public float Y;
        /**
        * \brief The third component of the vector.
        */
        public float Z;
    }
}
