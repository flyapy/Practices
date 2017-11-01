using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practices.SerialPort.Utilities
{
    class SerialPortReader
    {
        private System.IO.Ports.SerialPort serialPort;
        public SerialPortReader(string portName)
        {
            serialPort = new System.IO.Ports.SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
        }

        private bool looping = true;

        
        public void StartDataSending(string data)
        {
            looping = true;
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    Task.Run(() => 
                    {
                        while (looping)
                        {
                            //serialPort.Write("=0005.000-");
                            serialPort.Write(data);
                            Thread.Sleep(20);
                        }
                    });
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"串口通信异常，{ex.ToString()}");
            }
        }


        public void StopDataSending()
        {
            looping = false;
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }






}
