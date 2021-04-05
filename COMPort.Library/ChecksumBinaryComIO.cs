using System.IO.Ports;
using COMPort.Library.Checksums;

namespace COMPort.Library
{
    public class ChecksumBinaryComIO : BinaryComIO
    {
        private readonly IChecksum _checksumMethod;

        public ChecksumBinaryComIO(SerialPort port, IChecksum checksumMethod)
            : base(port)
        {
            _checksumMethod = checksumMethod;
        }

        protected override byte[] TryReceive()
        {
            var received = base.TryReceive();
            var packet = _checksumMethod.CheckChecksumAndUnwrap(received);
            return packet;
        }

        protected override void TrySend(byte[] data)
        {
            var packet = _checksumMethod.AppendChecksum(data);
            base.TrySend(packet);
        }
    }
}
