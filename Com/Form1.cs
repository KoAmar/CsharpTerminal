using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Com
{
    public partial class Form1 : Form
    {
        private Thread _readThread;
        private SerialPort _serialPort;
        private string _portName = "COM3";
        private int _baudRate = 9600;
        private bool _continue;

        public Form1()
        {
            InitializeComponent();
            _serialPort = new SerialPort(_portName, _baudRate, Parity.None, 8, StopBits.One)
            {
                WriteTimeout = 500,
                ReadTimeout = 500,
                Handshake = Handshake.None
            };
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            _serialPort.Open();
            _serialPort.Write("HELLO WORLD\r\n");
            _serialPort.Close();
        }


        public void ReaderStart()
        {
            if (_readThread != null) { return; }
            _readThread = new Thread(Read);
            _readThread.Start();
        }

        public void ReaderStop()
        {
            _readThread.Join();
            _serialPort.Close();
        }

        private void Read()
        {
            while (_continue)
            {
                try
                {
                    var message = _serialPort.ReadLine();
                    Console.WriteLine(message);
                    logTextBox.Text += message + Environment.NewLine;
                }
                catch (TimeoutException) { }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
