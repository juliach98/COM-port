using COMPort.Library.Checksums;
using System.IO.Ports;

namespace COMPort.Library
{
    public static class ComIOManager
    {
        public static ChecksumBinaryComIO MakeChecksumBinaryIO(IChecksum checksumMethod, string portName, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            var port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            return new ChecksumBinaryComIO(port, checksumMethod);
        }

        public static BinaryComIO MakeBinaryIO(string portName, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            var port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            return new BinaryComIO(port);
        }

        public static ASCIIComIO MakeASCIIIO(string portName, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            var port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            return new ASCIIComIO(port);
        }

        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
