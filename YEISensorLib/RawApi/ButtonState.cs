using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    /**
    * \brief Stores the state of the physical buttons on a 3-Space sensor.
    * 
    * Designation of "left" and "right" are based on the assumed that the sensor' side with physical buttons is facing towards the point of reference with the LED on that side pointed upward.
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ButtonState
    {
        /**
        * \brief True if the left button is pressed. False if not.
        */
        /* boolean/byte */
        public byte LeftPressed;
        /**
        * \brief True if the right button is pressed. False if not.
        */
        /* boolean/byte */
        public byte RightPressed;

        /// <summary>
        /// Timestamp of the data
        /// </summary>
        public uint TimeStamp;
    }
}
