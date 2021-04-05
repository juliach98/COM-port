using COMPort.Library;
using System;
using System.IO.Ports;
using System.Text;

namespace COMPort.App
{
    internal enum PortMode
    {
        Binary,
        ASCII
    }

    internal class ComPortService
    { 
        public static ComPortService Instance { get; } = new ComPortService();

        public event Action<bool> StateChanged;
        public event Action<Exception> Error;
        public event Action<string> DataReceived;

        private bool _isOpened;
        private Parity _parity = Parity.None;
        private int _baudRate;
        private PortMode _portMode;
        private string _portName;
        private BinaryComIO _binaryComIo;
        private ASCIIComIO _asciiComIo;

        public void SetPortMode(PortMode portMode)
        {
            _portMode = portMode;
            if(_isOpened)
                Open();
        }

        public void SetBaudRate(int baudRate)
        {
            _baudRate = baudRate;
            if (_isOpened)
                Open();
        }

        public void SetPortName(string portName)
        {
            _portName = portName;
            if (_isOpened)
                Open();
        }

        public void SetParity(Parity parity)
        {
            _parity = parity;
            if (_isOpened)
                Open();
        }

        public void SendData(string data)
        {
            if(_isOpened)
            {
                if (_portMode == PortMode.ASCII)
                    _asciiComIo.SendPacket(data);
                else if (_portMode == PortMode.Binary)
                    _binaryComIo.SendPacket(Encoding.UTF8.GetBytes(data));
            }
        }

        private void BinaryDataReceived(byte[] data)
        {
            DataReceived?.Invoke(Encoding.UTF8.GetString(data));
        }

        private void StringDataReceived(string data)
        {
            DataReceived?.Invoke(data);
        }

        private void ErrorReceived(Exception error)
        {
            Error?.Invoke(error);
        }

        private void ChangeMode(bool isOpened)
        {
            _isOpened = isOpened;
            StateChanged?.Invoke(isOpened);
        }

        public string[] GetAvailablePorts()
        {
            return ComIOManager.GetAvailablePorts();
        }

        public void Open()
        {
            if (_isOpened)
                Close();

            if(_portMode == PortMode.ASCII)
            {
                _asciiComIo = ComIOManager.MakeASCIIIO(_portName, _baudRate, _parity);
                _asciiComIo.PacketReceived += StringDataReceived;
                _asciiComIo.Error += ErrorReceived;
                _asciiComIo.Open();
            }
            else if(_portMode == PortMode.Binary)
            {
                _binaryComIo = ComIOManager.MakeBinaryIO(_portName, _baudRate, _parity);
                _binaryComIo.PacketReceived += BinaryDataReceived;
                _binaryComIo.Error += ErrorReceived;
                _binaryComIo.Open();
            }
            ChangeMode(true);
        }

        public void Close()
        {
            if (_isOpened)
            {
                ChangeMode(false);
                if (_asciiComIo != null)
                {
                    _asciiComIo.PacketReceived -= StringDataReceived;
                    _asciiComIo.Error -= ErrorReceived;
                    _asciiComIo.Close();
                    _asciiComIo = null;
                }
                if(_binaryComIo != null)
                {
                    _binaryComIo = null;
                    _binaryComIo.PacketReceived -= BinaryDataReceived;
                    _binaryComIo.Error -= ErrorReceived;
                    _binaryComIo.Close();
                    _binaryComIo = null;
                }
            }
        }
    }
}
