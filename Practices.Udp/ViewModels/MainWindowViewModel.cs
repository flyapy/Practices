using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practices.Udp.Commands;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Practices.Udp.ViewModels
{
    class MainWindowViewModel : NotificationObject
    {
        
        public DelegateCommand LaunchUdpServerCommand { get; set; }

        public DelegateCommand SendMessageCommand { get; set; }

        public DelegateCommand StartListeningCommand { get; set; }

        public DelegateCommand StopListeningCommand { get; set; }

        public DelegateCommand ApplyRemoteSettingCommand { get; set; }

        public DelegateCommand ClearMessageCacheCommand { get; set; }

        private string remoteIp;

        private int remotePort;

        private string localIp;

        private int localPort;

        private string messageToSend = string.Empty;

        public string MessageToSend
        {
            get
            {
                return messageToSend;
            }
            set
            {
                messageToSend = value;
                RaisePropertyChanged(nameof(MessageToSend));
            }

        }

        public string LocalIp
        {
            get
            {
                return localIp;
            }
            set
            {
                localIp = value;
                RaisePropertyChanged(nameof(LocalIp));
            }
        }

        public int LocalPort
        {
            get
            {
                return localPort;
            }
            set
            {
                localPort = value;
                RaisePropertyChanged(nameof(LocalPort));
            }
        }

        public string RemoteIp
        {
            get { return remoteIp; }
            set
            {
                remoteIp = value;
                this.RaisePropertyChanged(nameof(RemoteIp));
            }
        }

        public int RemotePort
        {
            get { return remotePort; }
            set
            {
                remotePort = value;
                this.RaisePropertyChanged(nameof(RemoteIp));
            }
        }

        private string messageInfo;

        public string MessageInfo
        {
            get { return messageInfo; }
            set
            {
                messageInfo = value;
                RaisePropertyChanged(nameof(MessageInfo));
            }
        }


        public MainWindowViewModel()
        {
            messageInfo = string.Empty;
            remoteIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Last().ToString();
            remotePort = 8001;
            localIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Last().ToString();
            localPort = 8000;
            this.StartListeningCommand = new DelegateCommand
            {
                ExecuteAction = new Action<object>((o) =>
                {
                    StartListening();
                })
            };
            StopListeningCommand = new DelegateCommand
            {
                ExecuteAction = new Action<object>((o) =>
                {
                    StopListening();
                })
            };
            this.SendMessageCommand = new DelegateCommand
            {
                ExecuteAction = new Action<object>((o) =>
                {
                    SendMessage();
                })
            };

            ClearMessageCacheCommand = new DelegateCommand
            {
                ExecuteAction = new Action<object>((o) =>
                {
                    ClearMessageCache();
                })
            };

        }

        private void ClearMessageCache()
        {
            MessageInfo = string.Empty;
        }

        public void SendMessage()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            UdpClient udpClient = new UdpClient();
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(messageToSend);
            udpClient.Send(buf, buf.Length, ep);
            udpClient.Close();
            AppendMessage(messageToSend, $"{localIp}:{localPort}");
            MessageToSend = string.Empty;
        }

        private UdpClient udpRecv;

        private System.Threading.Thread thrRecv;

        private bool udpRecvRunning = false;

        public bool IsUdpRecvRunning
        {
            get
            {
                return udpRecvRunning;
            }
            set
            {
                udpRecvRunning = value;
                RaisePropertyChanged(nameof(IsUdpRecvRunning));
            }
        }

        public void StartListening()
        {
            if (!udpRecvRunning)
            {
                IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(LocalIp), localPort); // 本机IP和监听端口号
                try
                {
                    udpRecv = new UdpClient(localEP);
                }
                catch (Exception ex)
                {
                    AppendMessage(string.Format("{0}，{1}", ex.GetType().Name, ex.Message), "系统消息");
                    return;
                }
                thrRecv = new Thread(ReceiveMessage);
                thrRecv.IsBackground = true;
                thrRecv.Start();
                IsUdpRecvRunning = true;
                AppendMessage("UDP监听器已成功启动", "系统消息");
            }

        }

        public void StopListening()
        {
            thrRecv.Abort(); // 必须先关闭这个线程，否则会异常
            udpRecv.Close();
            IsUdpRecvRunning = false;
            AppendMessage("UDP监听器已成功关闭", "系统消息");
        }

        private void ReceiveMessage()
        {
            IPEndPoint remoteIpEp = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                try
                {
                    byte[] bytRecv = udpRecv.Receive(ref remoteIpEp);
                    string message = Encoding.UTF8.GetString(bytRecv, 0, bytRecv.Length);
                    AppendMessage(message, remoteIpEp.ToString());
                }
                catch (Exception ex)
                {
                    AppendMessage(ex.Message, "系统消息");
                    break;
                }
            }
        }

        private void ClearInput()
        {
            MessageToSend = string.Empty;
        }

        private void AppendMessage(string message, string provider)
        {
            AppendMessage($"[{provider}] {DateTime.Now}{Environment.NewLine}{message}");
            
        }

        private void AppendMessage(string message)
        {
            MessageInfo += message + Environment.NewLine;
        }
    }

}
