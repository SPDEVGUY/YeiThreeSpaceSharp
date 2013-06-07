using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    public enum FilterEnum
    {
        FindALL = -1,
        FindBTL = 1,
        FindUSB = 2,
        FindDNG = 4,
        FindWL = 8,
        FindEM = 16,
        FindDL = 32,
        FindBT = 64
    }
}
