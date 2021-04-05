namespace COMPort.Library.Checksums
{
    public class ChecksumXOR : ChecksumBase
    {
        protected override byte[] CalcChecksum(byte[] packet)
        {
            byte chSum = 0x00;
            foreach (var b in packet)
                chSum ^= b;
            return new [] { chSum };
        }

        protected override int GetChecksumLength() => 1;
    }
}
