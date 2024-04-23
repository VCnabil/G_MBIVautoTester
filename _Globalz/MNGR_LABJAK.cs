using LabJack;
using System;
using G_MBIVautoTester._Globalz;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_MBIVautoTester._DataObjects;
using G_MBIVautoTester._DataObjects.DataComm;

namespace G_MBIVautoTester._Globalz
{
    public class MNGR_LABJAK
    {
        DATA_LABJAK_v1 _lbjkDataObj;
        DATA_LABJAK_v2 dATA_LABJAK_V2;
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

            Init_cONNECTION();
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
        public void Init_cONNECTION()
        {
            if(isOnBus)
            {
                return;
            }
           

            LJM.OpenS("ANY", "ANY", "ANY", ref handle);
            LJM.GetHandleInfo(handle, ref devType, ref conType, ref serNum, ref ipAddr, ref port, ref maxBytesPerMB);
            LJM.NumberToIP(ipAddr, ref ipAddrStr);
            string Line_label1 = "Opened a LabJack with Device type: " + devType + ", Connection type: " + conType + "," + "\n";
            string Line_label2 = "Serial number: " + serNum + ", IP address: " + ipAddrStr + ", Port: " + port + "," + "\n";
            string Line_label3 = "Max bytes per MB: " + maxBytesPerMB + "\n";
            StringBuilder strBuilder_connectionInfo = new StringBuilder();
            strBuilder_connectionInfo.Append(Line_label1 + Line_label2 + Line_label3);
            string connectionInfo = strBuilder_connectionInfo.ToString();

            if (maxBytesPerMB > 1) { 
                isOnBus = true;
            }
            else
            {
                isOnBus = false;
            }
        }
        public void Init_dataObj(DATA_LABJAK_v1 argDataref) {
            _lbjkDataObj = argDataref;
        }
        public void Init_dataObj2(DATA_LABJAK_v2 argDataref2)
        {
            dATA_LABJAK_V2 = argDataref2;
        }
        void Initold2() {

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


 
            isOnBus = true;
        }



        public void WRITE_to_JACKv2() {
            if (!isOnBus || dATA_LABJAK_V2 == null)
            {
                return;
            }

            int errorAddress1 = 0;
            LJM.eWriteNames(handle, dATA_LABJAK_V2.Num_MUXDAC_enries, dATA_LABJAK_V2.MUXDAC_names, dATA_LABJAK_V2.MUXDAC_values, ref errorAddress1);
        }


        public void WRITE_to_JACK()
        {
            if (!isOnBus)
            {
                return;
            }

            double qrg_EIO2onOff = 0;
            double qrg_EIO3onOff = 0;
            double qrg_EIO4onOff = 0;
            double qrg_EIO5onOff = 0;
            double qrg_DAC0 = 0;
            qrg_EIO2onOff = _lbjkDataObj.Labjack_EIO2_Write;
            qrg_EIO3onOff = _lbjkDataObj.Labjack_EIO3_Write;
            qrg_EIO4onOff = _lbjkDataObj.Labjack_EIO4_Write;
            qrg_EIO5onOff = _lbjkDataObj.Labjack_EIO5_Write;
            qrg_DAC0 = _lbjkDataObj.Labjack_DAC0_Write;
            double qrg_FIO0 = _lbjkDataObj.Labjack_FIO0_Write;
            double qrg_FIO1 = _lbjkDataObj.Labjack_FIO1_Write;
            double qrg_FIO2 = _lbjkDataObj.Labjack_FIO2_Write;
            double qrg_FIO3 = _lbjkDataObj.Labjack_FIO3_Write;
            string[] aNmesWrite = { "EIO2", "EIO3", "EIO4", "EIO5", "DAC0", "FIO0", "FIO1", "FIO2", "FIO3" };
            double[] aValuesWrite = { qrg_EIO2onOff, qrg_EIO3onOff, qrg_EIO4onOff, qrg_EIO5onOff, qrg_DAC0, qrg_FIO0, qrg_FIO1, qrg_FIO2, qrg_FIO3 };
            int errorAddress1 = 0;
            LJM.eWriteNames(handle, 9, aNmesWrite, aValuesWrite, ref errorAddress1);




        }

        public void READ_from_JAck() {



            string[] aNames = { "AIN0", "AIN1", "AIN2", "AIN3", "AIN4", "AIN5", "AIN6", "AIN7", "AIN8", "AIN9", "AIN10", "AIN11", "AIN12", "EIO0", "EIO1" };
            double[] ainValues = new double[15];
            int errorAddress = 0;
            LJM.LJMERROR LJMError = LJM.eReadNames(handle, 15, aNames, ainValues, ref errorAddress);
            _lbjkDataObj.Lbjk_VoltsRead_0_alarm = ainValues[0];
            _lbjkDataObj.Labjack_VoltsRead_1 = ainValues[1];
            _lbjkDataObj.Labjack_VoltsRead_2 = ainValues[2];
            _lbjkDataObj.Labjack_VoltsRead_3 = ainValues[3];
            _lbjkDataObj.Labjack_VoltsRead_4 = ainValues[4];
            _lbjkDataObj.Labjack_VoltsRead_5 = ainValues[5];
            _lbjkDataObj.Labjack_VoltsRead_6 = ainValues[6];
            _lbjkDataObj.Labjack_VoltsRead_7 = ainValues[7];
            _lbjkDataObj.Labjack_VoltsRead_8 = ainValues[8];
            _lbjkDataObj.Labjack_VoltsRead_9 = ainValues[9];
            _lbjkDataObj.Labjack_VoltsRead_10 = ainValues[10];
            _lbjkDataObj.Labjack_VoltsRead_11 = ainValues[11];
            _lbjkDataObj.Labjack_VoltsRead_12 = ainValues[12];
            _lbjkDataObj.Lbjk_EIO0_Read_led1 = ainValues[13];
            _lbjkDataObj.Lbjk_EIO1_Read_led2 = ainValues[14];
        }


        public void Update_dataObj_withAINdata()
        {
            if(!isOnBus)
            {
                return;
            }

            double qrg_EIO2onOff = 0;
            double qrg_EIO3onOff = 0;
            double qrg_EIO4onOff = 0;
            double qrg_EIO5onOff = 0;
            double qrg_DAC0 = 0;
            qrg_EIO2onOff = _lbjkDataObj.Labjack_EIO2_Write;
            qrg_EIO3onOff = _lbjkDataObj.Labjack_EIO3_Write;
            qrg_EIO4onOff = _lbjkDataObj.Labjack_EIO4_Write;
            qrg_EIO5onOff = _lbjkDataObj.Labjack_EIO5_Write;
            qrg_DAC0 = _lbjkDataObj.Labjack_DAC0_Write;
            double qrg_FIO0 = _lbjkDataObj.Labjack_FIO0_Write;
            double qrg_FIO1 = _lbjkDataObj.Labjack_FIO1_Write;
            double qrg_FIO2 = _lbjkDataObj.Labjack_FIO2_Write;
            double qrg_FIO3 = _lbjkDataObj.Labjack_FIO3_Write;
            string[] aNmesWrite = { "EIO2" , "EIO3", "EIO4", "EIO5" ,"DAC0", "FIO0", "FIO1", "FIO2", "FIO3" };
            double[] aValuesWrite = { qrg_EIO2onOff, qrg_EIO3onOff, qrg_EIO4onOff, qrg_EIO5onOff , qrg_DAC0, qrg_FIO0, qrg_FIO1, qrg_FIO2, qrg_FIO3 };
            int errorAddress1 = 0;
            LJM.eWriteNames(handle, 9, aNmesWrite, aValuesWrite, ref errorAddress1);



            string[] aNames = { "AIN0", "AIN1", "AIN2", "AIN3", "AIN4", "AIN5", "AIN6", "AIN7", "AIN8", "AIN9", "AIN10", "AIN11", "AIN12" ,"EIO0","EIO1"};
            double[] ainValues = new double[15];
            int errorAddress = 0;
            LJM.LJMERROR LJMError = LJM.eReadNames(handle, 15, aNames, ainValues, ref errorAddress);
            _lbjkDataObj.Lbjk_VoltsRead_0_alarm = ainValues[0];
            _lbjkDataObj.Labjack_VoltsRead_1 = ainValues[1];
            _lbjkDataObj.Labjack_VoltsRead_2 = ainValues[2];
            _lbjkDataObj.Labjack_VoltsRead_3 = ainValues[3];
            _lbjkDataObj.Labjack_VoltsRead_4 = ainValues[4];
            _lbjkDataObj.Labjack_VoltsRead_5 = ainValues[5];
            _lbjkDataObj.Labjack_VoltsRead_6 = ainValues[6];
            _lbjkDataObj.Labjack_VoltsRead_7 = ainValues[7];
            _lbjkDataObj.Labjack_VoltsRead_8 = ainValues[8];
            _lbjkDataObj.Labjack_VoltsRead_9 = ainValues[9];
            _lbjkDataObj.Labjack_VoltsRead_10 = ainValues[10];
            _lbjkDataObj.Labjack_VoltsRead_11 = ainValues[11];
            _lbjkDataObj.Labjack_VoltsRead_12 = ainValues[12];
            _lbjkDataObj.Lbjk_EIO0_Read_led1 = ainValues[13];
            _lbjkDataObj.Lbjk_EIO1_Read_led2 = ainValues[14];
           
        }
        public void DO_ReadAIN_0_1(double argWrite0_5)
        {
            if (!isOnBus)
            {
                return;
            }
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
