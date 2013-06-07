using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{


    /**
    * \brief Stores a timestamp retrieved from 3-Space Sensor Data-Logging units.
    * 
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Clock
    {
        /**
        * \brief The Month recorded by the timestamp.
        *
        * Expected values can range from 1 - 12. 1 = January, 2 = February, ...
        */
        /* unsigned char */
        public byte Month;
        /**
        * \brief The Day recorded by the timestamp.
        *
        * Expected values can range from 0 - 39.
        */
        /* unsigned char */
        public byte Day;
        /**
        * \brief The Year recorded by the timestamp.
        *
        * Expected values can range from 0 - 159.
        */
        /* unsigned char */
        public byte Year;
        /**
        * \brief The Hour recorded by the timestamp.
        *
        * Expected values can range from 0 - 23.
        */
        /* unsigned char */
        public byte hour;
        /**
        * \brief The Minute recorded by the timestamp.
        *
        * Expected values can range from 0 - 59.
        */
        /* unsigned char */
        public byte Minute;
        /**
        * \brief The Second recorded by the timestamp.
        *
        * Expected values can range from 0 - 59.
        */
        /* unsigned char */
        public byte Second;
    }
}
