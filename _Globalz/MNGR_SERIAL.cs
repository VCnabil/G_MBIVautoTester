using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace G_MBIVautoTester._Globalz
{
    public class MNGR_SERIAL
    {
        private static readonly Lazy<MNGR_SERIAL> instance = new Lazy<MNGR_SERIAL>(() => new MNGR_SERIAL());
        private StringBuilder incomingDataBuffer ;
        private StringBuilder incomingData;
        private int incommingDataCounter = 0;
        private SerialPort serialPort;
        private string lastCompleteMessage = string.Empty;
        private string latestComplete_Validated_MessageBody = string.Empty;
        private string latestCompleteMessageExtratedCHecksum = string.Empty;
        public static MNGR_SERIAL Instance{get { return instance.Value; }}
        private MNGR_SERIAL()
        {
            incomingDataBuffer = new StringBuilder();
            incomingData = new StringBuilder();
            incommingDataCounter = 0;
            serialPort = new SerialPort
            {
                PortName ="COM7", // "COM7 is default here on my pc for serial to MBIV serial connection. but it can be over written later with OpenPort(portname)"
                BaudRate = 19200,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None
            };
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }
        public void OpenPortDefault()
        {
            EventsManagerLib.Call_LogConsole("1. Opening default port");
            if (serialPort.IsOpen) { serialPort.Close();}
            incommingDataCounter = 0;
            serialPort.Open();
        }
        private void OpenPort(string portName)
        {
            if (serialPort.IsOpen){serialPort.Close();}
            incommingDataCounter = 0;
            serialPort.PortName = portName;
            serialPort.Open();
        }

        public void ClosePort()
        {
            if (serialPort.IsOpen)
            {
                serialPort.DataReceived -= new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                serialPort.Close();
                incommingDataCounter = 0;
            }
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            incomingDataBuffer.Append(sp.ReadExisting());
            ProcessBuffer();
        }

        private void ProcessBuffer()
        {
            // This regex matches the pattern of a complete message ending with *XX where XX are hex digits
            var regex = new Regex(@"\*[\dA-Fa-f]{2}");
            string buffer = incomingDataBuffer.ToString();
            Match match = regex.Match(buffer);
            // Initialize the last complete message to an empty string at the start of processing
            string mostRecentMessage = string.Empty;


            while (match.Success)
            {
                // Extract the complete message up to the end of the checksum
                int endIndex = match.Index + match.Length;
                string completeData = buffer.Substring(0, endIndex);
                // Update the most recent message
                mostRecentMessage = completeData;
                // Log or process the complete message
                EventsManagerLib.Call_LogConsole("success: "+completeData);
                // Prepare for the next message
                buffer = buffer.Substring(endIndex);
                match = regex.Match(buffer);
            }

            // Keep the last complete message
            if (!string.IsNullOrEmpty(mostRecentMessage))
            {
                lastCompleteMessage = mostRecentMessage;

                // Extract the message body and checksum
                int index = mostRecentMessage.IndexOf("*");
                if (index != -1 && mostRecentMessage.Length > index + 2)
                {
                  string  _latestCompleteMessageBody = mostRecentMessage.Substring(0, index);
                  string _latestCompleteMessageExtratedCHecksum = mostRecentMessage.Substring(index + 1, 2);
                    bool isValid = Helpers.Vealidate_Checksum(_latestCompleteMessageBody, _latestCompleteMessageExtratedCHecksum);
                    if (isValid)
                    {
                        latestComplete_Validated_MessageBody = _latestCompleteMessageBody;
                        latestCompleteMessageExtratedCHecksum = _latestCompleteMessageExtratedCHecksum;
                    }
                }
            }
            // Keep the unprocessed part in the buffer
            incomingDataBuffer.Clear();
            incomingDataBuffer.Append(buffer);
        }

        public string GetLatest_Valide_MessageBody()
        {
            return latestComplete_Validated_MessageBody;
        }
    
 
        public void WriteData(string data)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(data);
            }
            else
            {
                throw new InvalidOperationException("Attempt to write to closed serial port.");
            }
        }
    }
}
