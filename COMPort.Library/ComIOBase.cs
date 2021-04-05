using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Threading;

namespace COMPort.Library
{
    public abstract class ComIOBase<TData> : IDisposable where TData : class
    {
        protected readonly SerialPort _port;
        private Thread _sendThread;
        private Thread _receiveThread;
        private BlockingCollection<TData> _sendQueue;
        private volatile bool _isOpen;
        private CancellationTokenSource _sendCancellationTokenSource;

        public event Action<TData> PacketReceived;
        public event Action<Exception> Error;

        internal ComIOBase(SerialPort port)
        {
            _port = port ?? throw new ArgumentNullException("Port can not be null");
            _port.ReadTimeout = 1000;
            _isOpen = false;
            _sendQueue = new BlockingCollection<TData>();
        }

        public void Open()
        {
            if (!_isOpen)
            {
                _port.Open();
                _isOpen = true;
                _sendCancellationTokenSource = new CancellationTokenSource();
                _sendQueue = new BlockingCollection<TData>();
                _sendThread = new Thread(SendThreadProc) { IsBackground = true };
                _sendThread.Start();
                _receiveThread = new Thread(ReceiveThreadProc) { IsBackground = true };
                _receiveThread.Start();
            }
        }

        private void ReceiveThreadProc()
        {
            while(_isOpen)
            {
                try
                {
                    var res = TryReceive();
                    if (res != null)
                        PacketReceived?.Invoke(res);
                }
                catch (InvalidOperationException ex)
                {
                    Close();
                    Error?.Invoke(ex);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Close();
                    Error?.Invoke(ex);
                }
                catch (OperationCanceledException ex)
                {
                    Close();
                    Error?.Invoke(ex);
                }
                catch (Exception ex)
                {
                    Error?.Invoke(ex);
                }
            }
        }

        protected abstract TData TryReceive();

        protected abstract void TrySend(TData data);

        public void SendPacket(TData data)
        {
            _sendQueue.Add(data);
        }

        private void SendThreadProc()
        {
            while (_isOpen)
            {
                try
                {
                    var data = _sendQueue.Take(_sendCancellationTokenSource.Token);
                    TrySend(data);
                }
                catch (ObjectDisposedException)
                {
                }
                catch (Exception ex)
                {
                    Error?.Invoke(ex);
                }
            }
        }

        public void Close()
        {
            if (_isOpen)
            {
                _isOpen = false;
                _sendCancellationTokenSource.Cancel();
                _port.Close();
                _receiveThread.Join();
                _sendThread.Join();
            }
        }

        public void Dispose()
        {
            Close();
            _port.Dispose();
            PacketReceived = null;
        }
    }
}