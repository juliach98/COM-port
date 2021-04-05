using System;
using System.Text;

namespace COMPort.Library.Extensions
{
    public static class ByteArrayExtensions
    {
        public static void WriteUshort(this byte[] array, int index, ushort data)
        {
            unchecked
            {
                array[index] = (byte)data;
                array[index + 1] = (byte)(data >> 8);
            }
        }

        public static void WriteArray(this byte[] array, int index, byte[] otherArray)
        {
            otherArray.CopyTo(array, index);
        }

        public static ushort ReadUshort(this byte[] array, int index)
        {
            return (ushort)(array[index] | (array[index + 1] << 8));
        }

        public static string ToUTF8String(this byte[] array)
        {
            return Encoding.UTF8.GetString(array);
        }
    }
}
