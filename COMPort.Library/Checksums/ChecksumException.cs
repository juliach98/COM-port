using System;

namespace COMPort.Library.Checksums
{
    public class ChecksumException : Exception
    {
        public ChecksumException()
            : base()
        {

        }
        public ChecksumException(string message)
            : base(message)
        {

        }
    }
}
