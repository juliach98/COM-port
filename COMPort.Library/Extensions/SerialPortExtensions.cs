using System;
using System.IO.Ports;

namespace COMPort.Library.Extensions
{
    internal static class SerialPortExtensions
    {
        public static int ReadToArray(this SerialPort port, byte[] array, TimeSpan timeout)
        {
            var index = 0;
            var startedReadAt = DateTime.Now;
            while(index < array.Length && DateTime.Now < startedReadAt + timeout)
            {
                index += port.Read(array, index, array.Length - index);
            }
            return index;
        }
    }
}
