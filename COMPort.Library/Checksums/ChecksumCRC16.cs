using System;

namespace COMPort.Library.Checksums
{
    public class ChecksumCRC16 : ChecksumBase
    {
        protected override byte[] CalcChecksum(byte[] packet)
        {
            ushort crc = 0xFFFF;
            foreach(var b in packet)
            {
                crc ^= (ushort)b;

                for (int i = 8; i != 0; i--)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return BitConverter.GetBytes(crc);
        }

        protected override int GetChecksumLength() => 2;
    }
}
