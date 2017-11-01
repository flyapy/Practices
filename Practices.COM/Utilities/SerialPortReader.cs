using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practices.COM.Utilities
{
    class SerialPortReader
    {
        private SerialPort serialPort;

        public SerialPortReader(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, SerialDataReceivedEventHandler handler)
        {
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

            serialPort.DataReceived += handler;

        }

        public void Open()
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
        }

        public void Close()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }


        
    }
}
