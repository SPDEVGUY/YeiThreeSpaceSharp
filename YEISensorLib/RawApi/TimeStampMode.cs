using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    public enum TimeStampModeEnum : uint //AKA TSS_Timestamp_Mode
    {
        None,//TSS_TIMESTAMP_NONE,   /**< Disables timestamp, timestamp will return 0 */
        Sensor,//TSS_TIMESTAMP_SENSOR, /**< 3-Space device's timestamp, this can be set with tss_updateCurrentTimestamp */
        System//TSS_TIMESTAMP_SYSTEM  /**< The data is timestamped on arrival to the system using the high-resolution performance counter */
    }
}
