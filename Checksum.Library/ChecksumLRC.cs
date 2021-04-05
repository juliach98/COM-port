namespace Checksum.Library
{
    public class ChecksumLRC : IChecksum<byte>
    {
        public byte CalcChecksum(byte[] data)
        {
           byte chSum = 0x00;
            foreach (var b in data)
                chSum += b;
            return (byte)-chSum;
        }
    }
}
