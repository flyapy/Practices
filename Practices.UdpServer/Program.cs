using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practices.UdpServer
{
    class Program
    {
        static Socket server;

        static Socket remoteEP;

        static void Main(string[] args)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(new IPEndPoint(IPAddress.Parse("10.20.23.63"), 6001));//绑定端口号和IP

            remoteEP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            remoteEP.Bind(new IPEndPoint(IPAddress.Parse("10.20.23.63"), 6000));

            System.Console.WriteLine("服务端已经开启");
            Thread t = new Thread(ReciveMsg);//开启接收消息线程
            t.Start();
            Thread t2 = new Thread(sendMsg);//开启发送消息线程
            t2.Start();

        }

        /// <summary>
        /// 向特定ip的主机的端口发送数据报
        /// </summary>
        static void sendMsg()
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse("10.20.23.63"), 6000);
            while (true)
            {
                string msg = System.Console.ReadLine();
                server.SendTo(Encoding.UTF8.GetBytes(msg), point);
            }
        }

        /// <summary>
        /// 接收发送给本机ip对应端口号的数据报
        /// </summary>
        static void ReciveMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                byte[] buffer = new byte[1024];
                int length = remoteEP.ReceiveFrom(buffer, ref point);//接收数据报
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                System.Console.WriteLine($"[{point.ToString()}]:{message}");
            }
        }
    }
}
