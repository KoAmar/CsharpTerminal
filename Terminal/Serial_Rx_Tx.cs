// 
// Serial COM Port receive message event handler
// 12/16/2017, Dale Gambill
// When a line of text arrives from the COM port terminated by a \n character, this module will pass the message to
// the function specified by the application.   The application can also send a line of text.
//
// IMPORTANT: The dot net function below, comPort.ReadLine(), will not throw an error if there is no data, but might throw 'System.TimeoutException', if the data
// is not lines of text terminated by /n.  This would be because ReadLine() cannot find a line terminator in the wrong type of data.
// This code is intended for use with lines of text only.  It is not intended for use with any other type of data.
//

using System;
using System.Diagnostics;
using System.IO.Ports;

namespace Terminal
{
    public class SerialComPort
    {
        private readonly SerialPort _comPort;

        // constructor
        public SerialComPort()
        {
            _comPort = new SerialPort();
        }

        ~SerialComPort()
        {
            Close();
        }


        // User must register function to call when a line of text terminated by \n has been received
        public delegate void ReceiveCallback(string receivedMessage);
        public event ReceiveCallback OnMessageReceived;
        public void RegisterReceiveCallback(ReceiveCallback functionToCall)
        {
            OnMessageReceived += functionToCall;
        }
        public void DeRegisterReceiveCallback(ReceiveCallback functionToCall)
        {
            OnMessageReceived -= functionToCall;
        }

        public void SendLine(string aString)
        {
            try
            {
                if (_comPort.IsOpen)
                {
                    _comPort.Write(aString);
                }
            }
            catch (Exception exp)
            {
                Debug.Print(exp.Message);
            }
        }

        public string Open(string portName, string baudRate, string dataBits, string parity, string stopBits)
        {
            try
            {
                _comPort.WriteBufferSize = 4096;
                _comPort.ReadBufferSize = 4096;
                _comPort.WriteTimeout = 500;
                _comPort.ReadTimeout = 500;
                _comPort.DtrEnable = true;
                _comPort.Handshake = Handshake.None;
                _comPort.PortName = portName.TrimEnd();
                _comPort.BaudRate = Convert.ToInt32(baudRate);
                _comPort.DataBits = Convert.ToInt32(dataBits);
                switch (parity)
                {
                    case "None":
                        _comPort.Parity = Parity.None;
                        break;
                    case "Even":
                        _comPort.Parity = Parity.Even;
                        break;
                    case "Odd":
                        _comPort.Parity = Parity.Odd;
                        break;
                }
                switch (stopBits)
                {
                    case "One":
                        _comPort.StopBits = StopBits.One;
                        break;
                    case "Two":
                        _comPort.StopBits = StopBits.Two;
                        break;
                }
                _comPort.Open();
                _comPort.DataReceived += DataReceivedHandler;
            }
            catch (Exception error)
            {
                return error.Message + "\r\n";
            }
            return _comPort.IsOpen ? $"{_comPort.PortName} Opened \r\n" : $"{_comPort.PortName} Open Failed \r\n";
        }

        public string Close()
        {
            try
            {
                _comPort.Close();
            }
            catch (Exception error)
            {
                return error.Message + "\r\n";
            }
            return $"{_comPort.PortName} Closed\r\n";
        }

        public bool IsOpen()
        {
            return _comPort.IsOpen;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_comPort.IsOpen)
            {
                return;
            }

            try
            {
                var inData = _comPort.ReadLine();
                inData += "\n";
                OnMessageReceived?.Invoke(inData);
            }
            catch (Exception error)
            {
                Debug.Print(error.Message);
            }

        }
    }
}
