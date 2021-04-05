namespace Checksum.Library
{
    public class ChecksumCRC16 : IChecksum<ushort>
    {
        public ushort CalcChecksum(byte[] data)
        {
            ushort crc = 0xFFFF;
            foreach (var b in data)
            {
                crc ^= b;

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
            return (ushort)~crc;
        }
    }
}
