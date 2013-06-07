using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief Stores a 3-channel color.
    * 
    */

    [StructLayout(LayoutKind.Sequential,CharSet = CharSet.Ansi)]
    public struct Color
    {
        /**
        * \brief The red channel of the color. May be between 0.0 and 1.0.
        */
        public float R;
        /**
        * \brief The green channel of the color. May be between 0.0 and 1.0.
        */
        public float G;
        /**
        * \brief The blue channel of the color. May be between 0.0 and 1.0.
        */
        public float B;
    }
}
