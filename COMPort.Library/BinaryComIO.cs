using System;
using System.IO.Ports;
using COMPort.Library.Extensions;

namespace COMPort.Library
{
    public class BinaryComIO : ComIOBase<byte[]>
    {
        private byte[] _buffer = new byte[ushort.MaxValue + 2];
        private int index;


        internal BinaryComIO(SerialPort port)
            : base(port)
        {

        }

        protected override byte[] TryReceive()
        {
            try
            {
                index += _port.Read(_buffer, index, _buffer.Length - index);
                if(index > 2)
                {
                    var length = _buffer.ReadUshort(0);
                    if(index - 2 >= length)
                    {
                        var data = new byte[length];
                        Array.Copy(_buffer, 2, data, 0, length);
                        index = 0;
                        return data;
                    }
                }
            }
            catch(TimeoutException)
            {
                index = 0;
            }
            return null;
        }

        protected override void TrySend(byte[] data)
        {
            var length = (ushort) data.Length;
            var realData = new byte[data.Length + 2];
            realData.WriteUshort(0, length);
            realData.WriteArray(2, data);
            _port.Write(realData, 0, realData.Length);
        }
    }
}
