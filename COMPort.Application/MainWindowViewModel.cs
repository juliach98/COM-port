using System;
using System.Linq;

namespace COMPort.App
{
    class MainWindowViewModel : ViewModelBase
    {
        private readonly ComPortService _comService;

        public Command CommandOpenClose { get; } = new Command();
        public Command CommandSend { get; } = new Command();

        private string _textToSend = "";
        public string TextToSend
        {
            get => _textToSend;
            set => SetProperty(ref _textToSend, value);
        }

        private string _receivedText = "";
        public string ReceivedText
        {
            get => _receivedText;
            set => SetProperty(ref _receivedText, value);
        }

        private bool _isOpened = false;
        public bool IsOpened
        {
            get => _isOpened;
            set => SetProperty(ref _isOpened, value);
        }

        private string[] _availablePorts;
        public string[] AvailablePorts
        {
            get => _availablePorts;
            set => SetProperty(ref _availablePorts, value);
        }

        private string _selectedPort = "";
        public string SelectedPort
        {
            get => _selectedPort;
            set => SetProperty(ref _selectedPort, value);
        }

        public int[] AvailableBaudRates { get; } = new[] { 9600, 19200, 38400, 57600, 115200 };
        private int _selectedBaudRate = 9600;
        public int SelectedBaudRate
        {
            get => _selectedBaudRate;
            set => SetProperty(ref _selectedBaudRate, value);
        }

        public PortMode[] AvailableModes { get; } = Enum.GetValues(typeof(PortMode)).Cast<PortMode>().ToArray();
        private PortMode _selectedPortMode = PortMode.ASCII;
        public PortMode SelectedPortMode
        {
            get => _selectedPortMode;
            set => SetProperty(ref _selectedPortMode, value);
        }

        public MainWindowViewModel()
        {
            _comService = ComPortService.Instance;
            _comService.StateChanged += _comService_StateChanged;
            _comService.DataReceived += _comService_DataReceived;
            _comService.Error += _comService_Error;
            CommandOpenClose.Action = OpenClose;
            CommandSend.Action = Send;
            AvailablePorts = _comService.GetAvailablePorts();
        }

        private void OpenClose()
        {
            AvailablePorts = _comService.GetAvailablePorts();
            if (IsOpened)
            {
                _comService.Close();
            }
            else
            {
                _comService.SetPortName(SelectedPort);
                _comService.SetBaudRate(SelectedBaudRate);
                _comService.SetPortMode(SelectedPortMode);
                _comService.Open();
            }
        }

        private void Send()
        {
            _comService.SendData(TextToSend);
            TextToSend = string.Empty;
        }

        private void _comService_Error(Exception exception)
        {
            if (exception is InvalidOperationException)
                _comService.Close();
        }

        private void _comService_DataReceived(string data)
        {
            ReceivedText += data + "\n";
        }

        private void _comService_StateChanged(bool state)
        {
            IsOpened = state;
        }
    } 
}
