using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{
    public static class IntPtrExtensions
    {
        public static List<T> ToListFromArray<T>(this IntPtr basePointer, uint count)
        {
            var result = new List<T>();
            var sizeOfType = Marshal.SizeOf(typeof(T));
            if (count > 0)
            {
                for (var i = 0; i < count; i++)
                {
                    var currentPointer = basePointer + (sizeOfType * i);
                    var item = (T)Marshal.PtrToStructure(currentPointer, typeof(T));
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
