using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Com
{
    public partial class Form1 : Form
    {
        private Thread _readThread;
        private SerialPort _serialPort;
        private string _portName;
        private int _baudRate;
        private bool _readerContinue;

        [DllImport("foo.dll")]
        private static extern int simple_sum(int x, int y);

        public Form1()
        {
            InitializeComponent();
            logTextBox.Text = string.Empty;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReaderStop();
        }

        private void ReaderStart()
        {
            if (_readThread != null)
            {
                return;
            }

            _readThread = new Thread(Read);
            _readThread.Start();
        }

        private void ReaderStop()
        {
            if (_readThread == null)
            {
                return;
            }

            _readerContinue = false;
            _readThread.Join();
            _serialPort.Close();
        }

        private void Read()
        {
            while (_readerContinue)
            {
                try
                {
                    var message = _serialPort.ReadLine();
                    Console.WriteLine(message);
                    logTextBox.Text += message + Environment.NewLine;
                }
                catch (TimeoutException)
                {
                }
            }
        }

        private void OpenPort()
        {
            _serialPort = new SerialPort(GetPortName(), GetBaudRate(), Parity.None, 8, StopBits.One)
            {
                WriteTimeout = 500, ReadTimeout = 500, Handshake = Handshake.None
            };
            _serialPort.Open();
            _readerContinue = true;
            ReaderStart();
        }

        private void ClosePort()
        {
            _serialPort.Close();
        }

        private int GetBaudRate()
        {
            var tryParse = int.TryParse(baudRateTB.Text, out var result);
            if (tryParse)
            {
                _baudRate = result;
            }
            else
            {
                MessageBox.Show(@"Error in baud rate value!");
            }

            return result;
        }

        private string GetPortName()
        {
            var portName = portNameTB.Text;
            return portName;
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            //_serialPort.Open();
            //_serialPort.Write("HELLO WORLD\r\n");
            //_serialPort.Close();
            MessageBox.Show(simple_sum(1, 1).ToString());
        }

        private void portNameTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            _portName = GetPortName();
        }

        private void baudRateTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            _baudRate = GetBaudRate();
        }

        private void openClosePortBtn_Click(object sender, EventArgs e)
        {
            if (_serialPort == null || !_serialPort.IsOpen)
            {
                OpenPort();
                openClosePortBtn.Text = @"Close Port";
            }
            else
            {
                ClosePort();
                openClosePortBtn.Text = @"Open Port";
            }
        }
    }
}