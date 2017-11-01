using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Practices.COM.ViewModels
{
    class MainWindowViewModel : Prism.Mvvm.BindableBase
    {
        private List<string> portList;

        public List<string> PortList
        {
            get { return portList; }
            set
            {
                portList = value;
                RaisePropertyChanged(nameof(PortList));
            }
        }

        private List<string> baudRateList;

        public List<string> BaudRateList
        {
            get { return baudRateList; }
            set
            {
                baudRateList = value;
                RaisePropertyChanged(nameof(BaudRateList));
            }
        }

        private List<string> dataBitsList;

        public List<string> DataBitsList
        {
            get { return dataBitsList; }
            set
            {
                dataBitsList = value;
                RaisePropertyChanged(nameof(DataBitsList));
            }
        }

        private List<string> stopBitsList;

        public List<string> StopBitsList
        {
            get { return stopBitsList; }
            set
            {
                stopBitsList = value;
                RaisePropertyChanged(nameof(StopBitsList));
            }
        }

        private List<string> parityCheckList;

        public List<string> ParityCheckList
        {
            get { return parityCheckList; }
            set
            {
                parityCheckList = value;
                RaisePropertyChanged(nameof(ParityCheckList));
            }
        }

        private string baudRate;

        public string BaudRate
        {
            get { return baudRate; }
            set
            {
                baudRate = value;
                RaisePropertyChanged(nameof(BaudRate));
            }
        }

        private string port;

        public string Port
        {
            get { return port; }
            set
            {
                port = value;
                RaisePropertyChanged(nameof(Port));
            }
        }

        private string parityCheck;

        public string ParityCheck
        {
            get { return parityCheck; }
            set
            {
                parityCheck = value;
                this.RaisePropertyChanged(nameof(ParityCheck));
            }
        }

        private string dataBits;

        public string DataBits
        {
            get { return dataBits; }
            set
            {
                dataBits = value;
                this.RaisePropertyChanged(nameof(DataBits));
            }
        }

        private string stopBits;

        public string StopBits
        {
            get { return stopBits; }
            set
            {
                stopBits = value;
                this.RaisePropertyChanged(nameof(StopBits));
            }
        }

        private bool dtrEnabled;

        public bool IsDtrEnabled
        {
            get { return dtrEnabled; } 
            set
            {
                dtrEnabled = value;
                RaisePropertyChanged(nameof(IsDtrEnabled));
            }
        }

        private bool rtsEnabled;

        public bool IsRtsEnabled
        {
            get { return rtsEnabled; }
            set
            {
                rtsEnabled = value;
                RaisePropertyChanged(nameof(IsRtsEnabled));
            }
        }

        private bool crcEnabled;

        public bool IsCrcEnabled
        {
            get { return crcEnabled; }
            set
            {
                crcEnabled = value;
                RaisePropertyChanged(nameof(IsCrcEnabled));
            }
        }

        private bool openPortEnabled;

        public bool IsOpenPortEnabled
        {
            get { return openPortEnabled; }
            set
            {
                openPortEnabled = value;
                RaisePropertyChanged(nameof(IsOpenPortEnabled));
            }
        }

        private int messageTotal;

        public int MessageTotal
        {
            get { return messageTotal; }
            set
            {
                messageTotal = value;
                RaisePropertyChanged(nameof(MessageTotal));
            }
        }

        private string messageContent;
        
        public string MessageContent
        {
            get { return messageContent; }
            set
            {
                messageContent = value;
                RaisePropertyChanged(nameof(MessageContent));
            }
        }

        private double messagePerSecond;

        public double MessagePerSecond
        {
            get { return messagePerSecond; }
            set
            {
                messagePerSecond = value;
                RaisePropertyChanged(nameof(MessagePerSecond));
            }
        }

        private bool timerEnabled = true;

        public bool IsTimerEnabled
        {
            get { return timerEnabled; }
            set
            {
                timerEnabled = value;
                RaisePropertyChanged(nameof(IsTimerEnabled));
            }
        }

        public Prism.Commands.DelegateCommand OpenPortCommand { get; set; }

        public DelegateCommand ClosePortCommand { get; set; }

        public DelegateCommand ClearMessageContentCommand { get; set; }

        public DelegateCommand<string> ReadDataCommand { get; set; }        

        private Utilities.SerialPortReader spReader;

        private Dictionary<string, StopBits> stopBitsDict;

        public MainWindowViewModel()
        {
            portList = new List<string>();
            for (int i = 1; i <= 16; i++)
            {
                portList.Add($"COM{i}");
            }
            baudRateList = "300:600:1200:2400:4800:9600:19200:38400:57600:115200".Split(':').ToList();
            dataBitsList = "5:6:7:8".Split(':').ToList();
            stopBitsList = "0:1:1.5:2".Split(':').ToList();
            parityCheckList = "None:Odd:Even:Mark:Space".Split(':').ToList();
            stopBitsDict = new Dictionary<string, System.IO.Ports.StopBits>();
            stopBitsDict.Add("0", System.IO.Ports.StopBits.None);
            stopBitsDict.Add("1", System.IO.Ports.StopBits.One);
            stopBitsDict.Add("1.5", System.IO.Ports.StopBits.OnePointFive);
            stopBitsDict.Add("2", System.IO.Ports.StopBits.Two);
            port = "COM1";
            baudRate = "9600";
            parityCheck = "None";
            dataBits = "8";
            stopBits = "1";
            crcEnabled = false;
            MessageContent = "";
            openPortEnabled = true;
            OpenPortCommand = new Prism.Commands.DelegateCommand(() =>
            {
                try
                {
                    spReader = new Utilities.SerialPortReader(port, Convert.ToInt32(baudRate),
                                (Parity)Enum.Parse(typeof(Parity), parityCheck),
                                Convert.ToInt32(dataBits),
                                stopBitsDict[stopBits],
                                new SerialDataReceivedEventHandler(SerialPortDataReceivedEventHandler));
                    spReader.Open();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"打开串口失败，{ex.Message}");
                    return;
                }
                IsOpenPortEnabled = false;
                timeStub = Environment.TickCount;
                MessageTotal = 0;
                IsTimerEnabled = false;
            });

            ClosePortCommand = new DelegateCommand(() =>
            {
                spReader.Close();
                IsOpenPortEnabled = true;
                IsTimerEnabled = true;
            });

            ClearMessageContentCommand = new DelegateCommand(() =>
            {
                MessageContent = "";
            });

            ReadDataCommand = new DelegateCommand<string>((t) => 
            {
                int timeSpan = Convert.ToInt32(t);
                IsTimerEnabled = false;
                OpenPortCommand.Execute();
                Task.Run(() => 
                {
                    Thread.Sleep(timeSpan);
                    ClosePortCommand.Execute();
                    IsTimerEnabled = true;
                });
            });
            
        }

        private int timeStub = 0;

        

        private void AppendMessage(string message, string boundary = "")
        {
            MessageContent += boundary + message;
        }

        private void SerialPortDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] rcvBytes = null;
            rcvBytes = ReadDeviceCOMBytes(sender as SerialPort);
            AppendMessage(Encoding.UTF8.GetString(rcvBytes));
            MessageTotal += 1;
            MessagePerSecond = (int)((double)MessageTotal / (Environment.TickCount + 1 - timeStub) * 1000 * 100) / 100.0f;
        }

        private byte[] ReadDeviceCOMBytes(SerialPort serialPort)
        {
            try
            {
                List<Byte> byteList = new List<Byte>();
                byte[] rcvBytes = null;
                int rcvCount = 0;
                int count = 0;
                do
                {
                    count = serialPort.BytesToRead;
                    rcvBytes = new byte[count];
                    rcvCount = serialPort.Read(rcvBytes, 0, count);
                    foreach (byte by in rcvBytes)
                    {
                        byteList.Add(by);
                        //if (byteList.Count == 11)
                        //    break;
                    }
                } while (rcvCount < count);

                return byteList.ToArray();
            }
            catch
            {
                return null;
            }
        }
    }
}
