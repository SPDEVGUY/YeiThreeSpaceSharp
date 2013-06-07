using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief An enum expressing the different possible configurations of axis assignment between the axes of rotation the physical 3-Space sensor experiences (<i>Natural Axes</i>) and the axes of rotation depiced by the sensor's output data (<i>Output Axes</i>).
    * 
    * When dealing with axis directions, we must first define two concepts, <i>Natural Axes</i> and <i>Output Axes</i>.
    * <i>Natural Axes</i> are the axes of rotation about which the physical 3-Space sensor experiences changes in orientation.
    * <i>Output Axes</i> are the axes of rotation about which data received from a 3-Space sensor depicts.
    * This distinction is necessary because, for instance, it is possible to use the axis directions setting to express rotations about the physical 3-Space Sensor's X-Axis (the <i>Natural</i> X-Axis) as rotations about the Y-Axis in the sensor's output data (the <i>Output</i> Y-Axis).
    * 
    * When interpreting the axis directions, a combination of the three letter "X", "Y", and "Z" are used.
    * Depending on the order of arrangement of the three letter, a different mapping of <i>Natural Axes</i> to <i>Output Axes</i> is expressed.
    * The position of the given letter in the three letter set expresses the <i>Natural Axis</i> and the letter residing at that position expresses the <i>Output Axis</i>. For instance, an axis directions setting of "YZX" indicates that the <i>Natural</i> X-Axis is being mapped the <i>Output</i> Y-Axis, the <i>Natural</i> Y-Axis is being mapped to the <i>Output</i> Z-Axis, and the <i>Natural</i> Z-Axis is being mapped to the <i>Output</i> X-Axis. This means that data dealing with rotations experienced by the sensor hardware on its X-Axis are being mapped to the Y-Axis of the output data,  data dealing with rotations experienced by the sensor hardware on its Y-Axis are being mapped to the Z-Axis of the output data, and data dealing with rotations experienced by the sensor hardware on its Z-Axis are being mapped to the X-Axis of the output data.
    */
    public enum AxisOrder
    {
        /**
        * \brief Map the <i>Natural</i> X-Axis to the <i>Output</i> X-Axis, <i>Natural</i> Y-Axis to the <i>Output</i> Y-Axis, and <i>Natural</i> Z-Axis to the <i>Output</i> Z-Axis.
        */
        XYZ = 0,
        /**
        * \brief Map the <i>Natural</i> X-Axis to the <i>Output</i> X-Axis, <i>Natural</i> Y-Axis to the <i>Output</i> Z-Axis, and <i>Natural</i> Z-Axis to the <i>Output</i> Y-Axis.
        */
        XZY,
        /**
        * \brief Map the <i>Natural</i> X-Axis to the <i>Output</i> Y-Axis, <i>Natural</i> Y-Axis to the <i>Output</i> X-Axis, and <i>Natural</i> Z-Axis to the <i>Output</i> Z-Axis.
        */
        YXZ,
        /**
        * \brief Map the <i>Natural</i> X-Axis to the <i>Output</i> Y-Axis, <i>Natural</i> Y-Axis to the <i>Output</i> Z-Axis, and <i>Natural</i> Z-Axis to the <i>Output</i> X-Axis.
        */
        YZX,
        /**
        * \brief Map the <i>Natural</i> X-Axis to the <i>Output</i> Z-Axis, <i>Natural</i> Y-Axis to the <i>Output</i> X-Axis, and <i>Natural</i> Z-Axis to the <i>Output</i> Y-Axis.
        */
        ZXY,
        /**
        * \brief Map the <i>Natural</i> X-Axis to the <i>Output</i> Z-Axis, <i>Natural</i> Y-Axis to the <i>Output</i> Y-Axis, and <i>Natural</i> Z-Axis to the <i>Output</i> X-Axis.
        */
        ZYX
    }
}
