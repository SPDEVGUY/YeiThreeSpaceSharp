using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief A storage structure for information about the state of a 3-Space device in 'bootloader' mode.
    *
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct BootloaderData
    {
        /**
        * \brief TODO: Write description...
        */
        /* unsigned int */
        public uint jTagId;
        /**
        * \brief TODO: Write description...
        */
        /* unsigned int */
        public uint ProgramOffset;
        /**
        * \brief TODO: Write description...
        */
        /* unsigned int */
        public uint FlashSize;
        /**
        * \brief TODO: Write description...
        */
        /* unsigned short */
        public ushort PageSize;
        /**
        * \brief TODO: Write description...
        */
        /* unsigned int */
        public uint UserPageOffset;
        /**
        * \brief TODO: Write description...
        */
        /* unsigned short */
        public ushort UserPageSize;
        /**
        * \brief TODO: Write description...
        */
        /* unsigned short */
        public ushort AppFirmwareVer;
        /**
        * \brief TODO: Write description...
        */
        /* unsigned short */
        public ushort FirmwareVer;
    }

}
