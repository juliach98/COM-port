namespace Checksum.Library
{
    public interface IChecksum<T>
    {
        T CalcChecksum(byte[] data);
    }
}
