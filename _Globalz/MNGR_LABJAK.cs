using LabJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_MBIVautoTester._Globalz
{
    public class MNGR_LABJAK
    {
        private static readonly Lazy<MNGR_LABJAK> _instance = new Lazy<MNGR_LABJAK>(() => new MNGR_LABJAK());
        public static MNGR_LABJAK Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        int handle = 0;
        int devType = 0;
        int conType = 0;
        int serNum = 0;
        int ipAddr = 0;
        int port = 0;
        int maxBytesPerMB = 0;
        string ipAddrStr = "";
        double _valueAIN_0 = 0;
        double _valueAIN_1 = 0;
        bool isOnBus = false;
        private MNGR_LABJAK()
        {
            devType = 0;
            conType = 0;
            serNum = 0;
            ipAddr = 0;
            port = 0;
            maxBytesPerMB = 0;
            ipAddrStr = "";
            _valueAIN_0 = 0;
            _valueAIN_1 = 0;
            isOnBus = false;

            Init();
        }
        public bool GetIsOnBus()
        {
            return isOnBus;
        }
        public int GetSerNum()
        {
            return serNum;
        }
        public double GetAIN_0()
        {
            return _valueAIN_0;
        }
        public double GetAIN_1()
        {
            return _valueAIN_1;
        }
        public void Init()
        {
            isOnBus = false;
            LJM.OpenS("ANY", "ANY", "ANY", ref handle);
            LJM.GetHandleInfo(handle, ref devType, ref conType, ref serNum, ref ipAddr, ref port, ref maxBytesPerMB);
            LJM.NumberToIP(ipAddr, ref ipAddrStr);
            string Line_label1 = "Opened a LabJack with Device type: " + devType + ", Connection type: " + conType + "," + "\n";
            string Line_label2 = "Serial number: " + serNum + ", IP address: " + ipAddrStr + ", Port: " + port + "," + "\n";
            string Line_label3 = "Max bytes per MB: " + maxBytesPerMB + "\n";
            StringBuilder strBuilder_connectionInfo = new StringBuilder();
            strBuilder_connectionInfo.Append(Line_label1 + Line_label2 + Line_label3);
            string connectionInfo = strBuilder_connectionInfo.ToString();
            int errorAddress = 0;
            int numFrames = 6;
            string[] names = new string[6] {"AIN0_NEGATIVE_CH", "AIN0_RANGE", "AIN0_RESOLUTION_INDEX",
                    "AIN1_NEGATIVE_CH", "AIN1_RANGE", "AIN1_RESOLUTION_INDEX"};
            double[] aValues = new double[6] { 199, 10, 0, 199, 10, 0 };
            LJM.eWriteNames(handle, numFrames, names, aValues, ref errorAddress);
            isOnBus = true;
        }
        public void DO_ReadAIN_0_1(double argWrite0_5)
        {
            string[] aNames = { "AIN0", "AIN1" };
            double[] ainValues = new double[2];
            int errorAddress = 0;
            LJM.LJMERROR LJMError = LJM.eReadNames(handle, 2, aNames, ainValues, ref errorAddress);
            _valueAIN_0 = ainValues[0];
            _valueAIN_1 = 0;
            string name = "DAC0";
            double value = argWrite0_5;
            LJM.eWriteName(handle, name, value);
        }
        public void Close()
        {
            LJM.CloseAll();
            isOnBus = false;
        }
    }
}
