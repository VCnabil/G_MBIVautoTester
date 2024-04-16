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

        public delegate void MessageReceivedHandler(string message);
        public event MessageReceivedHandler MessageReceived;

        protected virtual void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(message);
        }

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
              
                //OnMessageReceived(mostRecentMessage);  // Fire the event
                // Prepare for the next message
                buffer = buffer.Substring(endIndex);
                match = regex.Match(buffer);
            }

            // Keep the last complete message
            if (!string.IsNullOrEmpty(mostRecentMessage))
            {
                lastCompleteMessage = mostRecentMessage;

              





                // Extract the message body and checksum
                int indexDollar = mostRecentMessage.IndexOf("$");//at index 1 
                int index = mostRecentMessage.IndexOf("*");//at indesx 128
               //EventsManagerLib.Call_LogConsole("2. Last complete message: " + mostRecentMessage + "has a $ at index: " + indexDollar + " and * at index: " + index);

                if(indexDollar > 0 && index > indexDollar +1)
                {
                    latestComplete_Validated_MessageBody = mostRecentMessage.Substring(indexDollar, index - indexDollar);
                    latestCompleteMessageExtratedCHecksum = mostRecentMessage.Substring(index + 1);
                  //  EventsManagerLib.Call_LogConsole("3. Last complete message body: " + latestComplete_Validated_MessageBody + " and checksum: " + latestCompleteMessageExtratedCHecksum);
                    bool isChecksumValid =  Helpers.Vealidate_Checksum(latestComplete_Validated_MessageBody, latestCompleteMessageExtratedCHecksum);
                    if(isChecksumValid)
                    {
                   //     EventsManagerLib.Call_LogConsole("4. Last complete message: " + mostRecentMessage + " has a valid checksum");
                        OnMessageReceived(latestComplete_Validated_MessageBody);
                    }
                    else
                    {
                        EventsManagerLib.Call_LogConsole("4. Last complete message: " + mostRecentMessage + " has an invalid checksum");
                    }

                }
                else
                {
                    EventsManagerLib.Call_LogConsole("4. Last complete message: " + mostRecentMessage + " has no $ or *");
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
