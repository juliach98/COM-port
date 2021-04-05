using System;
using System.IO.Ports;

namespace COMPort.Library
{
    public class ASCIIComIO : ComIOBase<string>
    {
        internal ASCIIComIO(SerialPort port)
            : base(port)
        {

        }

        protected override string TryReceive()
        {
            try
            {
                return _port.ReadTo("\0");
            }
            catch (TimeoutException ex)
            {
                return null;
            }

        }

        protected override void TrySend(string data)
        {
            _port.Write(data + "\0");
        }
    }
}
