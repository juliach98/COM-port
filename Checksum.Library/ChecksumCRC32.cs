namespace Checksum.Library
{
    public class ChecksumCRC32 : IChecksum<uint>
    {
        private uint[] table;

        public ChecksumCRC32()
        {
            uint poly = 0xedb88320;
            table = new uint[256];
            uint temp = 0;
            for (uint i = 0; i < table.Length; ++i)
            {
                temp = i;
                for (int j = 8; j > 0; --j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (uint)((temp >> 1) ^ poly);
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                table[i] = temp;
            }
        }

        public uint CalcChecksum(byte[] data)
        {
            uint crc = 0xffffffff;
            foreach (var b in data)
            {
                byte index = (byte)(((crc) & 0xff) ^ b);
                crc = (crc >> 8) ^ table[index];
            }
            return ~crc;
        }
    }
}
