using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Terminal
{
    public partial class Main : Form
    {
        private readonly SerialComPort _serialPort;
        private string _receivedData;
        private bool _dataReady;
        private StreamReader _file;

        public Main()
        {
            InitializeComponent();
            _file = null;
            _serialPort = new SerialComPort();
            _serialPort.RegisterReceiveCallback(ReceiveDataHandler);

            var receivedDataTimer = new Timer { Interval = 25 }; // 25 ms
            receivedDataTimer.Tick += ReceivedDataTimerTick;
            receivedDataTimer.Start();

            var replayLogTimer = new Timer { Interval = 1000 }; // 1000 ms
            replayLogTimer.Tick += ReplayLogTimerTick;
            replayLogTimer.Start();

            var version = Assembly.GetEntryAssembly()?.GetName().Version;
            Text = "TERMINAL - Serial Data Terminal v" + version;
        }

        [Localizable(false)]
        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void ReceiveDataHandler(string data)
        {
            if (_dataReady)
            {
                Debug.Print("Received data was thrown away because line buffer not emptied");
            }
            else
            {
                _dataReady = true;
                _receivedData = data;
            }
        }

        private void ReceivedDataTimerTick(object sender, EventArgs e)
        {
            if (!_dataReady) return;
            _dataReady = false;
            UpdateDataWindow(_receivedData);
        }

        private void ReplayLogTimerTick(object sender, EventArgs e)
        {
            if (_file == null) return;
            try
            {
                var message = _file.ReadLine();
                if (!_file.EndOfStream)
                {
                    _serialPort.SendLine(message + "\r\n");
                }
                else
                {
                    _file.BaseStream.Seek(0, 0);  // start over reading the file
                }
            }
            catch (Exception error)
            {
                Debug.Print(error.Message);
            }
        }

        // I did not use a .net queue here because it is CPU expensive and I don't really need it
        // If the program checks the line buffer every 25 ms,
        // it will handle roughly up to 40 lines per second without a line buffer over-write
        private void UpdateDataWindow(string message)
        {
            tbDataWindow.Text += message;
            tbDataWindow.SelectionStart = tbDataWindow.TextLength;
            tbDataWindow.ScrollToCaret();
        }

        private void BAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                  "Written by Dale Gambill (same as 'Software Guy For You' on Youtube)\r\n\r\n" +
                  "TERMINAL demonstrates event-handling of serial COM port data as follows:\r\n\r\n" +
                  "1 - getting messages from a COM port via a callback function\r\n" +
                  "2 - sending messages from a file to the COM port, one at a time\r\n" +
                  "3 - sending log file messages and receiving messages at the same time\r\n\r\n" +
                  "Source code at: Github URL: https://github.com/dalegambill/CsharpTerminal\r\n",
                  @"About"
              );
        }

        private void BOpenComPort_Click(object sender, EventArgs e)
        {
            // Handles the Open/Close button, which toggles its label, depending on previous state.
            string status;
            if (bOpenComPort.Text == @"Open COM Port")
            {
                status = _serialPort.Open(tbComPort.Text, tbBaudRate.Text, "8", "None", "One");
                if (status.Contains("Opened"))
                {
                    bOpenComPort.Text = @"Close COM Port";
                }
            }
            else
            {
                status = _serialPort.Close();
                bOpenComPort.Text = @"Open COM Port";
            }
            UpdateDataWindow(status);
        }

        private void BClearRxData_Click(object sender, EventArgs e)
        {
            tbDataWindow.Clear();
        }

        private void BSendMessage_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen())
            {
                var message = tbMessageToSend.Text + "\r\n";
                _serialPort.SendLine(message);
                UpdateDataWindow(message);
            }
            else
            {
                UpdateDataWindow("Open COM port first\r\n");
            }
        }

        private void BTutorials_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCouhHzMMU9c-Qh-TkZl5GDg");
        }

        private void BReplayLog_Click(object sender, EventArgs e)
        {
            if (!_serialPort.IsOpen())
            {
                UpdateDataWindow("Open COM port first\r\n");
                return;
            }
            if (bReplayLog.Text == @"Replay Log")
            {
                var openFileDialog = new OpenFileDialog();
                var result = openFileDialog.ShowDialog();
                if (result != DialogResult.OK) return;
                _file = new StreamReader(openFileDialog.FileName);
                bReplayLog.Text = @"Stop Replay Log";
                UpdateDataWindow("Replaying to COM port: " + openFileDialog.FileName + "\r\n");
            }
            else
            {
                if (_file == null) return;
                _file.Close();
                _file = null;
                bReplayLog.Text = @"Replay Log";
            }
        }
    }
}
