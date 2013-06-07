using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YEISensorLib.RawApi
{

    /**
    * \brief A storage structure for orientations expressed as a 3x3 TSS_Matrix.
    *
    * Matricies returned from API calls are always in row-major.
    * Fields for the structure are named based on the following template: m[ROW][COLUMN].
    * This means that matrix field m12 corresponds to the value in row 1 column 2.
    */

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Matrix
    {
        /**
        * \brief The value in row 0 column 0.
        */
        public float m00;
        /**
        * \brief The value in row 0 column 1.
        */
        public float m01;
        /**
        * \brief The value in row 0 column 2.
        */
        public float m02;
        /**
        * \brief The value in row 1 column 0.
        */
        public float m10;
        /**
        * \brief The value in row 1 column 1.
        */
        public float m11;
        /**
        * \brief The value in row 1 column 2.
        */
        public float m12;
        /**
        * \brief The value in row 2 column 0.
        */
        public float m20;
        /**
        * \brief The value in row 2 column 1.
        */
        public float m21;
        /**
        * \brief The value in row 2 column 2.
        */
        public float m22;
    }
}
