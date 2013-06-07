using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief Stores a full configuration of a 3-Space sensor's 'Axis Directions'. 
    * This includes both remapping axes of rotation and negating the mapped axes.
    *
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct AxisDirections
    {
        /**
        * \brief The mapping of axes from a 3-Space sensor's <i>Natural Axes</i> to the sensor's <i>Output Axes</i>.
        * This is easily assigned by using the ::Axis_Order enum.
        */
        /* unsigned char */
        public byte AxisOrder;
        /**
        * \brief Whether to negate the X-Axis of the sensor's <i>Output Axis</i>.
        */
        /* boolean/byte */
        public byte NegateX;
        /**
        * \brief Whether to negate the Y-Axis of the sensor's <i>Output Axis</i>.
        */
        /* boolean/byte */
        public byte NegateY;
        /**
        * \brief Whether to negate the Z-Axis of the sensor's <i>Output Axis</i>.
        */
        /* boolean/byte */
        public byte NegateZ;
    }
}
