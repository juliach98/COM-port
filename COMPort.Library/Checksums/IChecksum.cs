namespace COMPort.Library.Checksums
{
    public interface IChecksum
    {
        byte[] AppendChecksum(byte[] packet);

        byte[] CheckChecksumAndUnwrap(byte[] packetWithChecksum);
    }
}
