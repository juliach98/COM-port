using System;

namespace COMPort.Library.Checksums
{
    public abstract class ChecksumBase : IChecksum
    {
        protected abstract byte[] CalcChecksum(byte[] packet);

        protected abstract int GetChecksumLength();
         
        public byte[] AppendChecksum(byte[] packet)
        {
            var checksum = CalcChecksum(packet);
            var result = new byte[packet.Length + GetChecksumLength()];
            packet.CopyTo(result, 0);
            checksum.CopyTo(result, packet.Length);
            return result;
        }

        public byte[] CheckChecksumAndUnwrap(byte[] packetWithChecksum)
        {
            var length = GetChecksumLength();
            var packet = new byte[packetWithChecksum.Length - length];
            var checksum = new byte[length];
            Array.Copy(packetWithChecksum, 0, packet, 0, packet.Length);
            Array.Copy(packetWithChecksum, packet.Length, checksum, 0, length);
            var resultChecksum = CalcChecksum(packet);
            for (var i = 0; i < length; i++)
                if (checksum[i] != resultChecksum[i])
                    throw new ChecksumException();
            return packet;
        }
    }
}
