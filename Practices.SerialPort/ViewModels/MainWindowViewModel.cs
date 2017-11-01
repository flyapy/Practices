using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practices.SerialPort.ViewModels
{
    class MainWindowViewModel: Prism.Mvvm.BindableBase
    {
        private List<string> portNameList;

        public List<string> PortNameList
        {
            get { return portNameList; }
            set
            {
                portNameList = value;
                RaisePropertyChanged(nameof(PortNameList));
            }
        }

        private string portName;

        public string PortName
        {
            get { return portName; }
            set
            {
                portName = value;
                RaisePropertyChanged(nameof(PortName));
            }
        }

        private string dataToSend;

        public string DataToSend
        {
            get { return dataToSend; }
            set
            {
                dataToSend = value;
                RaisePropertyChanged(nameof(DataToSend));
            }
        }

        private bool startDataSendingEnabled;

        public bool IsStartDataSendingEnabled
        {
            get { return startDataSendingEnabled; }
            set
            {
                startDataSendingEnabled = value;
                RaisePropertyChanged(nameof(IsStartDataSendingEnabled));
            }
        }

        private string currentDateTime;

        public string CurrentDateTime
        {
            get { return currentDateTime; }
            set
            {
                currentDateTime = value;
                RaisePropertyChanged(nameof(CurrentDateTime));
            }
        }


        private List<int> dataSendingIntervalList;

        public List<int> DataSendingIntervalList
        {
            get { return dataSendingIntervalList; }
            set
            {
                dataSendingIntervalList = value;
                RaisePropertyChanged(nameof(DataSendingIntervalList));
            }
        }

        private int dataSendingInterval;

        public int DataSendingInterval
        {
            get { return dataSendingInterval; }
            set
            {
                dataSendingInterval = value;
                RaisePropertyChanged(nameof(DataSendingInterval));
            }
        }


        private Utilities.SerialPortReader serialPortReader;

        public Prism.Commands.DelegateCommand StartDataSendingCommand { get; set; }

        public Prism.Commands.DelegateCommand StopDataSendingCommand { get; set; }


        private System.Timers.Timer clockTimer;

        public MainWindowViewModel()
        {
            portNameList = new List<string>();
            for (int i = 1; i <= 16; i++)
            {
                portNameList.Add($"COM{i}");
            }
            portName = portNameList[0];

            dataToSend = "=0005.000-";

            currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            startDataSendingEnabled = true;

            clockTimer = new System.Timers.Timer();
            clockTimer.Interval = 1000;
            clockTimer.Elapsed += TimeTickTimer_Elapsed;
            clockTimer.Start();

            dataSendingIntervalList = new List<int>();
            for (int i = 1; i <= 6; i++)
            {
                dataSendingIntervalList.Add(i * 10);
            }

            dataSendingInterval = 20;

            StartDataSendingCommand = new Prism.Commands.DelegateCommand(() =>
            {
                try
                {
                    serialPortReader = new Utilities.SerialPortReader(portName);
                    serialPortReader.StartDataSending(dataToSend);
                    IsStartDataSendingEnabled = false;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });

            StopDataSendingCommand = new Prism.Commands.DelegateCommand(() => 
            {
                try
                {
                    serialPortReader.StopDataSending();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message); 
                } 
                finally
                {
                    IsStartDataSendingEnabled = true;
                }
            });
        }

        private void TimeTickTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
