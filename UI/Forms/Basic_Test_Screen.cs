using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Kvaser.CanLib.Canlib;
using Kvaser.CanLib;
namespace G_MBIVautoTester.UI.Forms
{
    public partial class Basic_Test_Screen : Form
    {
        [DllImport("winmm.dll", EntryPoint = "joyGetPos")]
        public static extern int JoyGetPos(int uJoyID, ref JOYINFO pji);
        [StructLayout(LayoutKind.Sequential)]
        public struct JOYINFO
        {
            public int dwXpos;
            public int dwYpos;
            public int dwZpos;
            public int dwUpos;
            public int dwRpos;
            public int dwVpos;
            public int dwButtons;
            public int dwButtonNumber;
            public int dwPOV;
            public int dwReserved1;
            public int dwReserved2;
        }
        private SerialPort comPort;
        private bool comPort_isOpen = false;
        private string sCompleteSerialString = "";
        private bool bStringRXStart = false;
        private bool bStringRXEnd = false;
        private bool bSingleStep = false;
        private JOYINFO joypos1;
        private int last_gamepad_troll_change = 0;
        private int Digital_Inputs = 0;
        private int last_gamepad_dock_trans_change = 0;
        private int baseline_p = 0;
        private int baseline_s = 0;
        private int global_CAN1_Err_Counter = 0;
        private int global_CAN2_Err_Counter = 0;
        private int global_CAN3_Err_Counter = 0;
        private int global_i = 0;
        private int global_PropCU_Err_Counter = 101;
        private int global_SteerCU_Err_Counter = 101;
        private int global_FF00_Err_Counter = 101;
        private int global_FF01_Err_Counter = 101;
        private int global_FF03_Err_Counter = 101;
        private int global_FF04_Err_Counter = 101;
        private int global_FF05_Err_Counter = 101;
        private int global_last_serial_message_received = 101;
        private int global_automated_hbridge_step = 0;
        private int fram_cmd = 0;
        private int fram_data = 0;
        private int fram_address = 0;
        Canlib.canStatus can_status = Canlib.canStatus.canOK;
        public int hnd0, hnd1;
        public int ffa1_cuid, ffa1_progcmd, ffa1_paramid, ffa1_data, ffa1_progcmdreply;
        int errorsCnt = 0;
        string _errormessage = "";
        public Basic_Test_Screen()
        {
            InitializeComponent();
            Ain1_Max.Text = "---";
            Ain2_Max.Text = "---";
            Ain3_Max.Text = "---";
            Ain4_Max.Text = "---";
            Ain5_Max.Text = "---";
            Ain6_Max.Text = "---";
            Ain7_Max.Text = "---";
            Ain8_Max.Text = "---";
            Ain9_Max.Text = "---";
            Ain10_Max.Text = "---";
            Ain11_Max.Text = "---";
            Ain12_Max.Text = "---";
            Ain13_Max.Text = "---";
            Ain14_Max.Text = "---";
            Ain15_Max.Text = "---";
            Ain16_Max.Text = "---";
            Ain1_Min.Text = "---";
            Ain2_Min.Text = "---";
            Ain3_Min.Text = "---";
            Ain4_Min.Text = "---";
            Ain5_Min.Text = "---";
            Ain6_Min.Text = "---";
            Ain7_Min.Text = "---";
            Ain8_Min.Text = "---";
            Ain9_Min.Text = "---";
            Ain10_Min.Text = "---";
            Ain11_Min.Text = "---";
            Ain12_Min.Text = "---";
            Ain13_Min.Text = "---";
            Ain14_Min.Text = "---";
            Ain15_Min.Text = "---";
            Ain16_Min.Text = "---";
            object obj_buf;
            Canlib.canInitializeLibrary();
            Canlib.canStatus statusConnected = Canlib.canGetNumberOfChannels(out int numberOfChannels);
            int __channelsFound = numberOfChannels;
            if (statusConnected != 0)
            {
                errorsCnt++;
                _errormessage = "NManager o Kvaser device connected" + errorsCnt.ToString();
                return;
            }
            hnd0 = Canlib.canOpenChannel(0, Canlib.canOPEN_ACCEPT_VIRTUAL);
            hnd1 = Canlib.canOpenChannel(1, Canlib.canOPEN_ACCEPT_VIRTUAL);
            Canlib.canGetChannelData(0, Canlib.canCHANNELDATA_CHANNEL_NAME, out obj_buf);
            string channelName = obj_buf.ToString();
            string channelType = obj_buf.GetType().ToString();
            MessageBox.Show("Channel Name: " + channelName + "\nChannel Type: " + channelType);
            Canlib.canStatus statusSetParams_hnd0 = Canlib.canSetBusParams(hnd0, Canlib.canBITRATE_250K, 0, 0, 0, 0);
            Canlib.canStatus statusSetParams_hnd1 = Canlib.canSetBusParams(hnd1, Canlib.canBITRATE_250K, 0, 0, 0, 0);
            Canlib.canStatus statusBusOn_hnd0 = Canlib.canBusOn(hnd0);
            Canlib.canStatus statusBusOn_hnd1 = Canlib.canBusOn(hnd1);
            Canlib.canSetNotify(hnd1, this.Handle, Canlib.canNOTIFY_RX);
          //  Timer1.Enabled = true;
            TX_PB_Cmd_Text.Text = "0";
            TX_PN_Cmd_Text.Text = "0";
            TX_PI_Cmd_Text.Text = "0";
            TX_SB_Cmd_Text.Text = "0";
            TX_SN_Cmd_Text.Text = "0";
            TX_SI_Cmd_Text.Text = "0";
            CAN1RX.Text = "OFFLINE";
            CAN2RX.Text = "OFFLINE";
            CAN3RX.Text = "OFFLINE";
            Button_0.Click += Button_0_Click;
            Button_minus50.Click += Button_minus50_Click;
            Button_minus100.Click += Button_minus100_Click;
            Button_50.Click += Button_50_Click;
            Button_100.Click += Button_100_Click;
            Button1_clearAIN.Click += Button1_ClearAIN_Click;
            Button8_SendSingleTX.Click += Button8_SendSingleTX_Click;
            Button4_SendSingleCAN.Click += Button4_SendSingleCAN_Click;
            Button9_ReadCan2.Click += Button9_ReadCan2_Click;
            Button10_WriteCan2.Click += Button10_WriteCan2_Click;
            Button_RefreshPorts.Click += Button_RefreshPorts_Click;
            Button_OpenPort.Click += Button_OpenPort_Click;
            Button_ClosePort.Click += Button_ClosePort_Click;
            Button_ClearStatus.Click += Button_ClearStatus_Click;
            Eng_25.Click += Eng_25_Click;
            Eng_50.Click += Eng_50_Click;
            Eng_75.Click += Eng_75_Click;
            Eng_100.Click += Eng_100_Click;
            TX_PB_Cmd.Scroll += TX_PB_Cmd_Scroll;
            TX_PN_Cmd.Scroll += TX_PN_Cmd_Scroll;
            TX_PI_Cmd.Scroll += TX_PI_Cmd_Scroll;
            TX_SB_Cmd.Scroll += TX_SB_Cmd_Scroll;
            TX_SN_Cmd.Scroll += TX_SN_Cmd_Scroll;
            TX_SI_Cmd.Scroll += TX_SI_Cmd_Scroll;
            disable_PI.CheckedChanged += Disable_PI_CheckedChanged;
            disable_PN.CheckedChanged += Disable_PN_CheckedChanged;
            disable_PB.CheckedChanged += Disable_PB_CheckedChanged;
            disable_SN.CheckedChanged += Disable_SN_CheckedChanged;
            disable_SB.CheckedChanged += Disable_SB_CheckedChanged;
            disable_SI.CheckedChanged += Disable_SI_CheckedChanged;
            P_Eng.Scroll += P_Eng_Scroll;
            S_Eng.Scroll += S_Eng_Scroll;
            P_Eng_Disable.CheckedChanged += P_Eng_Disable_CheckedChanged;
            S_Eng_Disable.CheckedChanged += S_Eng_Disable_CheckedChanged;
            Safety_Trip_ON.CheckedChanged += Safety_Trip_ON_CheckedChanged;
            Safety_Trip_OFF.CheckedChanged += Safety_Trip_OFF_CheckedChanged;
            timer1_BasicTestScreen.Enabled = true;
            timer1_BasicTestScreen.Interval = 300;
            timer2_BasicTestScreen.Enabled = true;
            timer2_BasicTestScreen.Interval = 350;

            timer1_BasicTestScreen.Tick += Timer1_Tick;
            timer2_BasicTestScreen.Tick += Timer2_Tick;

            timer1_BasicTestScreen.Start();
            timer2_BasicTestScreen.Start();
        }
        #region Button Click Events
        private void Button_0_Click(object sender, EventArgs e)
        {
            update_slider_bars(0);
        }
        private void Button_minus50_Click(object sender, EventArgs e)
        {
            update_slider_bars(-50);
        }
        private void Button_minus100_Click(object sender, EventArgs e)
        {
            update_slider_bars(-100);
        }
        private void Button_50_Click(object sender, EventArgs e)
        {
            update_slider_bars(50);
        }
        private void Button_100_Click(object sender, EventArgs e)
        {
            update_slider_bars(100);
        }
        private void Button1_ClearAIN_Click(object sender, EventArgs e)
        {
            Ain1_Max.Text = "---";
            Ain2_Max.Text = "---";
            Ain3_Max.Text = "---";
            Ain4_Max.Text = "---";
            Ain5_Max.Text = "---";
            Ain6_Max.Text = "---";
            Ain7_Max.Text = "---";
            Ain8_Max.Text = "---";
            Ain9_Max.Text = "---";
            Ain10_Max.Text = "---";
            Ain11_Max.Text = "---";
            Ain12_Max.Text = "---";
            Ain13_Max.Text = "---";
            Ain14_Max.Text = "---";
            Ain15_Max.Text = "---";
            Ain16_Max.Text = "---";
            Ain1_Min.Text = "---";
            Ain2_Min.Text = "---";
            Ain3_Min.Text = "---";
            Ain4_Min.Text = "---";
            Ain5_Min.Text = "---";
            Ain6_Min.Text = "---";
            Ain7_Min.Text = "---";
            Ain8_Min.Text = "---";
            Ain9_Min.Text = "---";
            Ain10_Min.Text = "---";
            Ain11_Min.Text = "---";
            Ain12_Min.Text = "---";
            Ain13_Min.Text = "---";
            Ain14_Min.Text = "---";
            Ain15_Min.Text = "---";
            Ain16_Min.Text = "---";
        }
        private void Button4_SendSingleCAN_Click(object sender, EventArgs e)
        {
            byte[] msg = new byte[7];
            for (int i = 0; i < 100; i++)
            {
                Canlib.canWrite(hnd0, 123, msg, 6, 0);
            }
        }
        private void Button8_SendSingleTX_Click(object sender, EventArgs e)
        {
            SendSerialData();
        }
        private void Button9_ReadCan2_Click(object sender, EventArgs e)
        {
            fram_dt.Text = "-----";
            fram_cmd = 86;
            fram_address = Convert.ToInt32(fram_add.Text);
            TransmitCANMessages();
        }
        private void Button10_WriteCan2_Click(object sender, EventArgs e)
        {
            fram_dt.Text = "-----";
            fram_cmd = 85;
            fram_address = Convert.ToInt32(fram_add.Text);
            TransmitCANMessages();
        }
        private void Button_RefreshPorts_Click(object sender, EventArgs e)
        {
            GetSerialPortNames();
        }
        private void Button_OpenPort_Click(object sender, EventArgs e)
        {
            OpenSerialData();
        }
        private void Button_ClosePort_Click(object sender, EventArgs e)
        {
            if (comPort_isOpen)
            {
                comPort.Close();
                comPort_isOpen = false;
                Status_Message.Text = "COM port closed";
            }
            else
            {
                Status_Message.Text = "COM port is already closed";
            }
        }
        private void Button_ClearStatus_Click(object sender, EventArgs e)
        {
            Status_Message.Text = "";
        }
        private void Eng_100_Click(object sender, EventArgs e)
        {
            UpdateEngineBars(2000);
        }
        private void Eng_75_Click(object sender, EventArgs e)
        {
            UpdateEngineBars(1500);
        }
        private void Eng_50_Click(object sender, EventArgs e)
        {
            UpdateEngineBars(1000);
        }
        private void Eng_25_Click(object sender, EventArgs e)
        {
            UpdateEngineBars(500);
        }
        private void Eng_0_Click(object sender, EventArgs e)
        {
            UpdateEngineBars(0);
        }
        #endregion
        #region Sliders Valves
        private void TX_PB_Cmd_Scroll(object sender, EventArgs e)
        {
            TX_PB_Cmd_Text.Text = TX_PB_Cmd.Value.ToString();
        }
        private void TX_PN_Cmd_Scroll(object sender, EventArgs e)
        {
            TX_PN_Cmd_Text.Text = TX_PN_Cmd.Value.ToString();
        }
        private void TX_PI_Cmd_Scroll(object sender, EventArgs e)
        {
            TX_PI_Cmd_Text.Text = TX_PI_Cmd.Value.ToString();
        }
        private void TX_SB_Cmd_Scroll(object sender, EventArgs e)
        {
            TX_SB_Cmd_Text.Text = TX_SB_Cmd.Value.ToString();
        }
        private void TX_SN_Cmd_Scroll(object sender, EventArgs e)
        {
            TX_SN_Cmd_Text.Text = TX_SN_Cmd.Value.ToString();
        }
        private void TX_SI_Cmd_Scroll(object sender, EventArgs e)
        {
            TX_SI_Cmd_Text.Text = TX_SI_Cmd.Value.ToString();
        }
        private void Disable_PI_CheckedChanged(object sender, EventArgs e)
        {
            if (disable_PI.Checked)
            {
                TX_PI_Cmd.Value = 0;
                TX_PI_Cmd_Text.Text = TX_PI_Cmd.Value.ToString();
                TX_PI_Cmd.Enabled = false;
                TX_PI_Cmd_Text.Enabled = false;
            }
            else
            {
                TX_PI_Cmd.Enabled = true;
                TX_PI_Cmd_Text.Enabled = true;
            }
        }
        private void Disable_PN_CheckedChanged(object sender, EventArgs e)
        {
            if (disable_PN.Checked)
            {
                TX_PN_Cmd.Value = 0;
                TX_PN_Cmd_Text.Text = TX_PN_Cmd.Value.ToString();
                TX_PN_Cmd.Enabled = false;
                TX_PN_Cmd_Text.Enabled = false;
            }
            else
            {
                TX_PN_Cmd.Enabled = true;
                TX_PN_Cmd_Text.Enabled = true;
            }
        }
        private void Disable_PB_CheckedChanged(object sender, EventArgs e)
        {
            if (disable_PB.Checked)
            {
                TX_PB_Cmd.Value = 0;
                TX_PB_Cmd_Text.Text = TX_PB_Cmd.Value.ToString();
                TX_PB_Cmd.Enabled = false;
                TX_PB_Cmd_Text.Enabled = false;
            }
            else
            {
                TX_PB_Cmd.Enabled = true;
                TX_PB_Cmd_Text.Enabled = true;
            }
        }
        private void Disable_SN_CheckedChanged(object sender, EventArgs e)
        {
            if (disable_SN.Checked)
            {
                TX_SN_Cmd.Value = 0;
                TX_SN_Cmd_Text.Text = TX_SN_Cmd.Value.ToString();
                TX_SN_Cmd.Enabled = false;
                TX_SN_Cmd_Text.Enabled = false;
            }
            else
            {
                TX_SN_Cmd.Enabled = true;
                TX_SN_Cmd_Text.Enabled = true;
            }
        }
        private void Disable_SB_CheckedChanged(object sender, EventArgs e)
        {
            if (disable_SB.Checked)
            {
                TX_SB_Cmd.Value = 0;
                TX_SB_Cmd_Text.Text = TX_SB_Cmd.Value.ToString();
                TX_SB_Cmd.Enabled = false;
                TX_SB_Cmd_Text.Enabled = false;
            }
            else
            {
                TX_SB_Cmd.Enabled = true;
                TX_SB_Cmd_Text.Enabled = true;
            }
        }
        private void Disable_SI_CheckedChanged(object sender, EventArgs e)
        {
            if (disable_SI.Checked)
            {
                TX_SI_Cmd.Value = 0;
                TX_SI_Cmd_Text.Text = TX_SI_Cmd.Value.ToString();
                TX_SI_Cmd.Enabled = false;
                TX_SI_Cmd_Text.Enabled = false;
            }
            else
            {
                TX_SI_Cmd.Enabled = true;
                TX_SI_Cmd_Text.Enabled = true;
            }
        }
        #endregion
        #region Engin Bars
        private void P_Eng_Scroll(object sender, EventArgs e)
        {
            P_Eng_Text.Text = (P_Eng.Value / 20).ToString() + "%";
        }
        private void S_Eng_Scroll(object sender, EventArgs e)
        {
            S_Eng_Text.Text = (S_Eng.Value / 20).ToString() + "%";
        }
        private void P_Eng_Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (P_Eng_Disable.Checked)
            {
                P_Eng.Value = 0;
                P_Eng_Text.Text = P_Eng.Value.ToString();
                P_Eng.Enabled = false;
                P_Eng_Text.Enabled = false;
            }
            else
            {
                P_Eng.Enabled = true;
                P_Eng_Text.Enabled = true;
            }
        }
        private void S_Eng_Disable_CheckedChanged(object sender, EventArgs e)
        {
            if (S_Eng_Disable.Checked)
            {
                S_Eng.Value = 0;
                S_Eng_Text.Text = S_Eng.Value.ToString();
                S_Eng.Enabled = false;
                S_Eng_Text.Enabled = false;
            }
            else
            {
                S_Eng.Enabled = true;
                S_Eng_Text.Enabled = true;
            }
        }
        #endregion
        #region SafetyTrip
        private void Safety_Trip_ON_CheckedChanged(object sender, EventArgs e)
        {
            if (Safety_Trip_ON.Checked)
            {
                Safety_Trip_OFF.Checked = false;
            }
        }
        private void Safety_Trip_OFF_CheckedChanged(object sender, EventArgs e)
        {
            if (Safety_Trip_OFF.Checked)
            {
                Safety_Trip_ON.Checked = false;
            }
        }
        #endregion
        #region TIMERS
        private void Timer1_Tick(object sender, EventArgs e)
        {
            object chinfo = null;
            canStatus status = Canlib.canGetChannelData(hnd0, Canlib.canCHANNELDATA_CARD_TYPE, out chinfo);
            if (chinfo.Equals(Canlib.canHWTYPE_LEAF))
            {
                if (global_i == 1)
                {
                    RadioButton1.Checked = true;
                    global_i = 0;
                }
                else
                {
                    RadioButton1.Checked = false;
                    global_i = 1;
                }
            }
            else
            {
                RadioButton1.Checked = false;
                global_i = 1;
            }
            if (TX_Active.Checked)
            {
                SendSerialData();
            }
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            global_last_serial_message_received += 1;
            if (global_last_serial_message_received > 25)
            {
                global_last_serial_message_received = 25;
                GUI_Serial_Fault.Checked = true;
                MBIV_Serial_Fault.Checked = true;
                Checksum.Text = "";
            }
            else
            {
                GUI_Serial_Fault.Checked = false;
            }
            if (bStringRXEnd && !bSingleStep)
            {
                ParseSerialString();
            }
            ProcessCANMessages();
            PerformAutomatedTest();
        }
        #endregion
        private bool GETBIT(long data, int bitNum)
        {
            long mask = 1L << bitNum;
            long retVal = data & mask;
            return retVal > 0;
        }
        private void SetBit(ref byte data, int bitNum)
        {
            byte mask = (byte)(1 << bitNum);
            data |= mask;
        }
        private int GetPGN(int id)
        {
            const int mask = 16777215;
            return (id & mask) >> 8;
        }
        private int GetSRC(int id)
        {
            const int mask = 255;
            return id & mask;
        }
        private int GetWord(byte lowByte, byte highByte)
        {
            int hb = highByte;
            hb <<= 8;
            int lb = lowByte;
            return hb | lb;
        }
        private void SendWord(int data, ref byte lowByte, ref byte highByte)
        {
            const int mask = 255;
            highByte = (byte)(data >> 8);
            lowByte = (byte)(data & mask);
        }
        private long map(long x, long inMin, long inMax, long outMin, long outMax)
        {
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
        public void TransmitCANMessages()
        {
            byte[] msg = new byte[8];
            int id;
            int dlc = 8;
            for (int index = 0; index < 8; index++)
            {
                msg[index] = 0;
            }
            msg[0] = (byte)fram_cmd;
            msg[1] = (byte)fram_address;
            msg[2] = (byte)fram_data;
            id = 0x18AABB02;
            Canlib.canWrite(hnd0, id, msg, dlc, 4);
        }
        void ProcessCANMessages()
        {
            int id = 0, dlc = 0, flag = 0;
            long time = 0;
            byte[] msg = new byte[8];
            const int MAX_ERR_COUNTER = 15;
            global_CAN1_Err_Counter++;
            global_CAN2_Err_Counter++;
            global_CAN3_Err_Counter++;
            if (global_CAN1_Err_Counter >= MAX_ERR_COUNTER)
            {
                global_CAN1_Err_Counter = MAX_ERR_COUNTER;
                CAN1RX.Text = "OFFLINE";
            }
            if (global_CAN2_Err_Counter >= MAX_ERR_COUNTER)
            {
                global_CAN2_Err_Counter = MAX_ERR_COUNTER;
                CAN2RX.Text = "OFFLINE";
            }
            if (global_CAN3_Err_Counter >= MAX_ERR_COUNTER)
            {
                global_CAN3_Err_Counter = MAX_ERR_COUNTER;
                CAN3RX.Text = "OFFLINE";
            }
            while (can_status == Canlib.canStatus.canOK)
            {
                can_status = Canlib.canRead(hnd0, out id, msg, out dlc, out flag, out time);
                if (GetPGN(id) == 65450 && GetSRC(id) == 1)
                {
                    CAN1RX.Text = "OK";
                    global_CAN1_Err_Counter = 0;
                }
                if (GetPGN(id) == 65450 && GetSRC(id) == 2)
                {
                    CAN2RX.Text = "OK";
                    global_CAN2_Err_Counter = 0;
                }
                if (GetPGN(id) == 65450 && GetSRC(id) == 3)
                {
                    CAN3RX.Text = "OK";
                    global_CAN3_Err_Counter = 0;
                }
                if (GetPGN(id) == 65467 && GetSRC(id) == 2)
                {
                    fram_dt.Text = msg[0].ToString();
                }
            }
        }
        private void SerialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (!bStringRXEnd)
            {
                ReceivedText();
            }
        }
        private void ReceivedText()
        {
            if (!bStringRXStart)
            {
                while (comPort.BytesToRead > 0)
                {
                    int ch = comPort.ReadChar();
                    if (ch == '$')
                    {
                        bStringRXStart = true;
                        sCompleteSerialString = Convert.ToString((char)ch);
                        break;
                    }
                }
            }
            if (!bStringRXStart) return;
            while (comPort.BytesToRead > 0)
            {
                int ch = comPort.ReadChar();
                if (ch == '\r')
                {
                    bStringRXStart = false;
                    bStringRXEnd = true;
                    break;
                }
                sCompleteSerialString += Convert.ToString((char)ch);
            }
            Invoke(new MethodInvoker(delegate
            {
                SerialData.Text = sCompleteSerialString;
            }));
        }
        void OpenSerialData()
        {
            if (!comPort_isOpen)
            {
                if (AvailableSerialPorts.SelectedIndex != -1)
                {
                    Status_Message.Text = AvailableSerialPorts.SelectedItem.ToString() + " is now open";
                    comPort = new SerialPort(AvailableSerialPorts.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);
                    comPort.Open();
                    comPort_isOpen = true;
                    comPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
                }
                else
                {
                    Status_Message.Text = "Unable to open COM port as none was selected";
                }
            }
            else
            {
                Status_Message.Text = "Unable to open COM port as one is already open";
            }
        }
   
        void ParseSerialString()
        {
            ////if string contains * 
            //if (sCompleteSerialString.Contains("*"))
            //{
            //    //remove spaces after 
            //    sCompleteSerialString = sCompleteSerialString.Replace(" * ", "*");

            //}

            string returnStr = "";
            Console.WriteLine("RX:" + sCompleteSerialString);
            if (sCompleteSerialString.IndexOf("$") < 0 || sCompleteSerialString.IndexOf("*") < 0 || sCompleteSerialString.IndexOf("*") < sCompleteSerialString.IndexOf("$"))
            {
                returnStr = "$CORRUPTED,DATA*AA";
                bStringRXEnd = false;
                return;
            }
 

            returnStr = sCompleteSerialString;
            int i = returnStr.IndexOf("$");
            string f = returnStr.Substring(i + 1, returnStr.IndexOf("*", i + 1) - i - 1);
            string Computed_Checksum = GetChecksum(f);
            i = returnStr.IndexOf("*");
            f = returnStr.Substring(i + 1, returnStr.Length - i - 1);
            string Extracted_Checksum = f;
            if (String.Compare(Computed_Checksum, Extracted_Checksum) == 0)
            {
                Checksum.Text = "OK";
                string[] strArray;
                string newString = returnStr.Replace("*", ",");
                strArray = newString.Split(',');
                if (strArray[0] == "$VCIA")
                {
                    TMS570_Version.Text = strArray[1];
                    Ain1.Text = strArray[2];
                    Ain2.Text = strArray[3];
                    Ain3.Text = strArray[4];
                    Ain4.Text = strArray[5];
                    Ain5.Text = strArray[6];
                    Ain6.Text = strArray[7];
                    Ain7.Text = strArray[8];
                    Ain8.Text = strArray[9];
                    Ain9.Text = strArray[10];
                    Ain10.Text = strArray[11];
                    Ain11.Text = strArray[12];
                    Ain12.Text = strArray[13];
                    Ain13.Text = strArray[14];
                    Ain14.Text = strArray[15];
                    Ain15.Text = strArray[16];
                    Ain16.Text = strArray[17];
                    UpdateAllAinMinMax();
                    int Digital_Inputs = Convert.ToInt32(strArray[18]);
                    GP_0.Checked = GETBIT(Digital_Inputs, 0) == false;
                    GP_1.Checked = GETBIT(Digital_Inputs, 1) == false;
                    GP_2.Checked = GETBIT(Digital_Inputs, 2) == false;
                    GP_3.Checked = GETBIT(Digital_Inputs, 3) == false;
                    GP_4.Checked = GETBIT(Digital_Inputs, 4) == false;
                    GP_5.Checked = GETBIT(Digital_Inputs, 5) == false;
                    GP_6.Checked = GETBIT(Digital_Inputs, 6) == false;
                    GP_7.Checked = GETBIT(Digital_Inputs, 7) == false;
                    AP_DI.Checked = GETBIT(Digital_Inputs, 8) == false;
                    MBIV_Serial_Fault.Checked = Convert.ToInt32(strArray[19]) == 1;

                    if (Convert.ToInt32(strArray[19]) == 1)
                        MBIV_Serial_Fault.Checked = true;
                    else
                        MBIV_Serial_Fault.Checked = false;


                    AP_Input.Text = Convert.ToString(Convert.ToInt32(strArray[20]));
                    global_last_serial_message_received = 0;
                    PNOZ_FDBK.Text = strArray[21];
                    SNOZ_FDBK.Text = strArray[22];
                    PINT_FDBK.Text = strArray[23];
                    SBKT_FDBK.Text = strArray[24];
                    PBKT_FDBK.Text = strArray[25];
                    SINT_FDBK.Text = strArray[26];
                    PNOZ_FDBK_Map.Text = String.Format("{0:###0.00}", map(Convert.ToInt32(strArray[21]), 17, 1200, 0, 150) / 100.0);
                    SNOZ_FDBK_Map.Text = String.Format("{0:###0.00}", map(Convert.ToInt32(strArray[22]), 17, 1200, 0, 147) / 100.0);
                    PBKT_FDBK_Map.Text = String.Format("{0:###0.00}", map(Convert.ToInt32(strArray[25]), 17, 1200, 0, 147) / 100.0);
                    SBKT_FDBK_Map.Text = String.Format("{0:###0.00}", map(Convert.ToInt32(strArray[24]), 17, 1200, 0, 147) / 100.0);
                    PINT_FDBK_Map.Text = String.Format("{0:###0.00}", map(Convert.ToInt32(strArray[23]), 17, 1200, 0, 147) / 100.0);
                    SINT_FDBK_Map.Text = String.Format("{0:###0.00}", map(Convert.ToInt32(strArray[26]), 17, 1200, 0, 147) / 100.0);
                    PB_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 0) == false;
                    SI_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 1) == false;
                    PI_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 2) == false;
                    SB_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 3) == false;
                    PN_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 4) == false;
                    SN_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 5) == false;
                }
            }
            else
            {
                Checksum.Text = "BAD";
            }
            SerialData.Text = returnStr;
            bStringRXEnd = false;
            bSingleStep = false;
        }


        /*
         
        
    Sub ParseSerialString_V2()
        Dim returnStr As String = ""

        Console.WriteLine("RX:" + sCompleteSerialString)

        If (sCompleteSerialString.IndexOf("$") < 0) Or (sCompleteSerialString.IndexOf("*") < 0) Or (sCompleteSerialString.IndexOf("*") < sCompleteSerialString.IndexOf("$")) Then
            returnStr = "$CORRUPTED,DATA*AA"
            bStringRXEnd = False
            Return
        End If

        returnStr = sCompleteSerialString

        'computing the serial strign checksum
        Dim i As Integer = returnStr.IndexOf("$")
        'fix this next line here
        Dim f As String = returnStr.Substring(i + 1, returnStr.IndexOf("*", i + 1) - i - 1)
        Dim Computed_Checksum As String = GetChecksum(f)

        'extracting the serial string checksum
        i = returnStr.IndexOf("*")
        f = returnStr.Substring(i + 1, returnStr.Length - i - 1)
        Dim Extracted_Checksum As String = f

        If (String.Compare(Computed_Checksum, Extracted_Checksum) = 0) Then
            Checksum.Text = "OK"

            'update all info boxes with data from serial transmission
            Dim strArray() As String
            Dim newString As String = Replace(returnStr, "*", ",")
            strArray = Split(newString, ",")

            If (strArray(0) = "$VCIA") Then
                TMS570_Version.Text = strArray(1)
                Ain1.Text = strArray(2)
                Ain2.Text = strArray(3)
                Ain3.Text = strArray(4)
                Ain4.Text = strArray(5)
                Ain5.Text = strArray(6)
                Ain6.Text = strArray(7)
                Ain7.Text = strArray(8)
                Ain8.Text = strArray(9)

                Ain9.Text = strArray(10)
                Ain10.Text = strArray(11)
                Ain11.Text = strArray(12)
                Ain12.Text = strArray(13)
                Ain13.Text = strArray(14)
                Ain14.Text = strArray(15)
                Ain15.Text = strArray(16)
                Ain16.Text = strArray(17)

                'update min/max fields
                update_ain_min_max()

                Digital_Inputs = CInt(strArray(18))

                If (GETBIT(Digital_Inputs, 0) = False) Then Me.GP_0.Checked = True Else Me.GP_0.Checked = False
                If (GETBIT(Digital_Inputs, 1) = False) Then Me.GP_1.Checked = True Else Me.GP_1.Checked = False
                If (GETBIT(Digital_Inputs, 2) = False) Then Me.GP_2.Checked = True Else Me.GP_2.Checked = False
                If (GETBIT(Digital_Inputs, 3) = False) Then Me.GP_3.Checked = True Else Me.GP_3.Checked = False
                If (GETBIT(Digital_Inputs, 4) = False) Then Me.GP_4.Checked = True Else Me.GP_4.Checked = False
                If (GETBIT(Digital_Inputs, 5) = False) Then Me.GP_5.Checked = True Else Me.GP_5.Checked = False
                If (GETBIT(Digital_Inputs, 6) = False) Then Me.GP_6.Checked = True Else Me.GP_6.Checked = False
                If (GETBIT(Digital_Inputs, 7) = False) Then Me.GP_7.Checked = True Else Me.GP_7.Checked = False
                If (GETBIT(Digital_Inputs, 8) = False) Then Me.AP_DI.Checked = True Else Me.AP_DI.Checked = False

                If (CInt(strArray(19) = 1)) Then
                    MBIV_Serial_Fault.Checked = True
                Else
                    MBIV_Serial_Fault.Checked = False
                End If

                AP_Input.Text = CInt(strArray(20))

                global_last_serial_message_received = 0

                PNOZ_FDBK.Text = strArray(21)
                SNOZ_FDBK.Text = strArray(22)
                PBKT_FDBK.Text = strArray(25)
                SBKT_FDBK.Text = strArray(24)
                PINT_FDBK.Text = strArray(23)
                SINT_FDBK.Text = strArray(26)

                PNOZ_FDBK_Map.Text = Format(map(Int(strArray(21)), 17, 1200, 0, 150) / 100, "###0.00")
                SNOZ_FDBK_Map.Text = Format(map(Int(strArray(22)), 17, 1200, 0, 147) / 100, "###0.00")
                PBKT_FDBK_Map.Text = Format(map(Int(strArray(25)), 17, 1200, 0, 147) / 100, "###0.00")
                SBKT_FDBK_Map.Text = Format(map(Int(strArray(24)), 17, 1200, 0, 147) / 100, "###0.00")
                PINT_FDBK_Map.Text = Format(map(Int(strArray(23)), 17, 1200, 0, 147) / 100, "###0.00")
                SINT_FDBK_Map.Text = Format(map(Int(strArray(26)), 17, 1200, 0, 147) / 100, "###0.00")

                If (GETBIT(strArray(27), 4) = False) Then Me.PN_Fault.Checked = True Else Me.PN_Fault.Checked = False
                If (GETBIT(strArray(27), 2) = False) Then Me.PI_Fault.Checked = True Else Me.PI_Fault.Checked = False
                If (GETBIT(strArray(27), 0) = False) Then Me.PB_Fault.Checked = True Else Me.PB_Fault.Checked = False
                If (GETBIT(strArray(27), 3) = False) Then Me.SB_Fault.Checked = True Else Me.SB_Fault.Checked = False
                If (GETBIT(strArray(27), 5) = False) Then Me.SN_Fault.Checked = True Else Me.SN_Fault.Checked = False
                If (GETBIT(strArray(27), 1) = False) Then Me.SI_Fault.Checked = True Else Me.SI_Fault.Checked = False
            End If

        Else
            Checksum.Text = "BAD"
            'Status_Message.Text = "BAD Serial Checksum: [" + Extracted_Checksum + "] vs [" + Computed_Checksum + "]"

        End If

        SerialData.Text = returnStr
        bStringRXEnd = False

        bSingleStep = False 'this is a debug code

    End Sub

         */


 



        void SendSerialData()
        {
            byte rx_Cmd_Digital_Output = 0;
            if (rx_Cmd_Digital_Output_0.Checked) SetBit(ref rx_Cmd_Digital_Output, 0);
            if (rx_Cmd_Digital_Output_1.Checked) SetBit(ref rx_Cmd_Digital_Output, 1);
            if (rx_Cmd_Digital_Output_2.Checked) SetBit(ref rx_Cmd_Digital_Output, 2);
            string StringValue9 = Safety_Trip_ON.Checked ? "1" : "0";
            if (comPort_isOpen)
            {
                int value = rx_Cmd_Digital_Output;
                string StringValue = value.ToString();
                int value1 = TX_PB_Cmd.Value + 100;
                string StringValue1 = value1.ToString();
                int value2 = TX_PN_Cmd.Value + 100;
                string StringValue2 = value2.ToString();
                int value3 = disable_PI.Checked ? 100 : TX_PI_Cmd.Value + 100;
                string StringValue3 = value3.ToString();
                int value4 = TX_SB_Cmd.Value + 100;
                string StringValue4 = value4.ToString();
                int value5 = TX_SN_Cmd.Value + 100;
                string StringValue5 = value5.ToString();
                int value6 = TX_SI_Cmd.Value + 100;
                string StringValue6 = value6.ToString();
                int value7 = P_Eng.Value;
                string StringValue7 = value7.ToString();
                int value8 = S_Eng.Value;
                string StringValue8 = value8.ToString();
                string AllStringValues = StringValue + "," +
                                         StringValue1 + "," +
                                         StringValue2 + "," +
                                         StringValue3 + "," +
                                         StringValue4 + "," +
                                         StringValue5 + "," +
                                         StringValue6 + "," +
                                         StringValue7 + "," +
                                         StringValue8 + "," +
                                         StringValue9;
                string StringChecksum = GetChecksum("$AAAA," + AllStringValues);
                comPort.WriteLine("$AAAA," + AllStringValues + "*" + StringChecksum);
                SerialTXData.Text = "$_YO_," + AllStringValues + "*" + StringChecksum;
                Console.WriteLine("TX:" + SerialTXData.Text);
            }
        }
        private int Asc(char character)
        {
            return (int)character;
        }
        private string Hex(long number)
        {
            return number.ToString("X");
        }
        private string GetChecksum(string input)
        {
            //if (string.IsNullOrEmpty(input)) return "";
            //int last = Asc(input[0]);
            //for (int current = 1; current < input.Length; current++)
            //{
            //    last ^= Asc(input[current]);
            //}
            //string hexValue = Hex(last);
            //return hexValue.Length == 1 ? "0" + hexValue : hexValue;


            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string must not be null or empty.");

            if (input.Length < 2)  // Ensuring there's at least some data to process
                throw new ArgumentException("Input string is too short to process.");

            short csum = 0;
            int idx = 1;  // Assuming you always want to start processing from the second character

            while (idx < input.Length && input[idx] != '*')
            {
                csum ^= (short)input[idx];  // XOR each character's ASCII value with the checksum
                idx++;
            }

            if (idx == input.Length && input[idx - 1] != '*') {

                // throw new ArgumentException("Input string does not contain an end marker '*'.");
                return "";
            }

            return csum.ToString("X2");  // Convert the checksum to a two-digit hexadecimal string


        }
        void UpdateAinMinMax(ref TextBox raw, ref TextBox min, ref TextBox max)
        {
            if (max.Text == "---")
            {
                min.Text = raw.Text;
                max.Text = raw.Text;
            }
            if (int.TryParse(raw.Text, out int rawValue))
            {
                if (int.TryParse(min.Text, out int minValue) && minValue > rawValue)
                {
                    min.Text = raw.Text;
                }
                if (int.TryParse(max.Text, out int maxValue) && maxValue < rawValue)
                {
                    max.Text = raw.Text;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid integer in the raw value text box.");
            }
        }
        public void UpdateAllAinMinMax()
        {
            UpdateAinMinMax(ref Ain1, ref Ain1_Min, ref Ain1_Max);
            UpdateAinMinMax(ref Ain2, ref Ain2_Min, ref Ain2_Max);
            UpdateAinMinMax(ref Ain3, ref Ain3_Min, ref Ain3_Max);
            UpdateAinMinMax(ref Ain4, ref Ain4_Min, ref Ain4_Max);
            UpdateAinMinMax(ref Ain5, ref Ain5_Min, ref Ain5_Max);
            UpdateAinMinMax(ref Ain6, ref Ain6_Min, ref Ain6_Max);
            UpdateAinMinMax(ref Ain7, ref Ain7_Min, ref Ain7_Max);
            UpdateAinMinMax(ref Ain8, ref Ain8_Min, ref Ain8_Max);
            UpdateAinMinMax(ref Ain9, ref Ain9_Min, ref Ain9_Max);
            UpdateAinMinMax(ref Ain10, ref Ain10_Min, ref Ain10_Max);
            UpdateAinMinMax(ref Ain11, ref Ain11_Min, ref Ain11_Max);
            UpdateAinMinMax(ref Ain12, ref Ain12_Min, ref Ain12_Max);
            UpdateAinMinMax(ref Ain13, ref Ain13_Min, ref Ain13_Max);
            UpdateAinMinMax(ref Ain14, ref Ain14_Min, ref Ain14_Max);
            UpdateAinMinMax(ref Ain15, ref Ain15_Min, ref Ain15_Max);
            UpdateAinMinMax(ref Ain16, ref Ain16_Min, ref Ain16_Max);
        }
        void UpdateEngineBars(int value)
        {
            if (!P_Eng_Disable.Checked)
            {
                P_Eng.Value = value;
            }
            if (!S_Eng_Disable.Checked)
            {
                S_Eng.Value = value;
            }
            P_Eng_Text.Text = (P_Eng.Value / 20).ToString() + "%";
            S_Eng_Text.Text = (S_Eng.Value / 20).ToString() + "%";
        }
        void update_slider_bars(int value)
        {
            if (!disable_PB.Checked)
            {
                TX_PB_Cmd.Value = value;
            }
            if (!disable_PN.Checked)
            {
                TX_PN_Cmd.Value = value;
            }
            if (!disable_PI.Checked)
            {
                TX_PI_Cmd.Value = value;
            }
            if (!disable_SB.Checked)
            {
                TX_SB_Cmd.Value = value;
            }
            if (!disable_SN.Checked)
            {
                TX_SN_Cmd.Value = value;
            }
            if (!disable_SI.Checked)
            {
                TX_SI_Cmd.Value = value;
            }
            TX_PB_Cmd_Text.Text = TX_PB_Cmd.Value.ToString();
            TX_PN_Cmd_Text.Text = TX_PN_Cmd.Value.ToString();
            TX_PI_Cmd_Text.Text = TX_PI_Cmd.Value.ToString();
            TX_SB_Cmd_Text.Text = TX_SB_Cmd.Value.ToString();
            TX_SN_Cmd_Text.Text = TX_SN_Cmd.Value.ToString();
            TX_SI_Cmd_Text.Text = TX_SI_Cmd.Value.ToString();
        }
        void PerformAutomatedTest()
        {
            if (Automated_Valve_Driver.Checked)
            {
                global_automated_hbridge_step += 1;
                if (global_automated_hbridge_step >= 0 && global_automated_hbridge_step <= 20)
                {
                    update_slider_bars(Automated_Valve_Driver_Step.Value);
                }
                else if (global_automated_hbridge_step > 20 && global_automated_hbridge_step <= 40)
                {
                    update_slider_bars(-Automated_Valve_Driver_Step.Value);
                }
                else
                {
                    global_automated_hbridge_step = 0;
                }
            }
        }
        void GetSerialPortNames()
        {
            AvailableSerialPorts.Items.Clear();
            foreach (string sp in System.IO.Ports.SerialPort.GetPortNames())
            {
                AvailableSerialPorts.Items.Add(sp);
            }
        }
    }
}

/* 

        public string ReceiveSerialData()
        {
            string returnStr = "";
            if (comPort_isOpen == true)
            {
                int counter = 0;
                try
                {
                    comPort.ReadTimeout = 50;
                    do
                    {
                        counter += 1;
                        int Incoming = comPort.ReadChar();
                        if (((counter == 1) && (Incoming != 36)) || ((counter > 255) || (Incoming == 13)))
                        {
                            break;
                        }
                        else
                        {
                            if (counter == 1)
                            {
                                SerialData.Text = "";
                            }
                            returnStr += Convert.ToChar(Incoming);
                        }
                    } while (true);
                    if (returnStr.IndexOf("$") < 0 || returnStr.IndexOf("*") < 0)
                    {
                        returnStr = "$CORRUPTED,DATA*AA";
                    }
                    int i = returnStr.IndexOf("$");
                    string f = returnStr.Substring(i + 1, returnStr.IndexOf("*", i + 1) - i - 1);
                    string Computed_Checksum = GetChecksum(f);
                    i = returnStr.IndexOf("*");
                    f = returnStr.Substring(i + 1, returnStr.Length - i - 1);
                    string Extracted_Checksum = f;
                    if (Computed_Checksum == Extracted_Checksum)
                    {
                        Checksum.Text = "OK";
                        string[] strArray;
                        string newString = returnStr.Replace("*", ",");
                        strArray = newString.Split(',');
                        if (strArray[0] == "$VCIA")
                        {
                            TMS570_Version.Text = strArray[1];
                            Ain1.Text = strArray[2];
                            Ain2.Text = strArray[3];
                            Ain3.Text = strArray[4];
                            Ain4.Text = strArray[5];
                            Ain5.Text = strArray[6];
                            Ain6.Text = strArray[7];
                            Ain7.Text = strArray[8];
                            Ain8.Text = strArray[9];
                            Ain9.Text = strArray[10];
                            Ain10.Text = strArray[11];
                            Ain11.Text = strArray[12];
                            Ain12.Text = strArray[13];
                            Ain13.Text = strArray[14];
                            Ain14.Text = strArray[15];
                            Ain15.Text = strArray[16];
                            Ain16.Text = strArray[17];
                            UpdateAllAinMinMax();
                            int Digital_Inputs = Convert.ToInt32(strArray[18]);
                            GP_0.Checked = GETBIT(Digital_Inputs, 0) == false;
                            GP_1.Checked = GETBIT(Digital_Inputs, 1) == false;
                            GP_2.Checked = GETBIT(Digital_Inputs, 2) == false;
                            GP_3.Checked = GETBIT(Digital_Inputs, 3) == false;
                            GP_4.Checked = GETBIT(Digital_Inputs, 4) == false;
                            GP_5.Checked = GETBIT(Digital_Inputs, 5) == false;
                            GP_6.Checked = GETBIT(Digital_Inputs, 6) == false;
                            GP_7.Checked = GETBIT(Digital_Inputs, 7) == false;
                            AP_DI.Checked = GETBIT(Digital_Inputs, 8) == false;
                            MBIV_Serial_Fault.Checked = Convert.ToInt32(strArray[19]) == 1;
                            AP_Input.Text = Convert.ToString(Convert.ToInt32(strArray[20]));
                            global_last_serial_message_received = 0;
                            PNOZ_FDBK.Text = strArray[21];
                            SNOZ_FDBK.Text = strArray[22];
                            PBKT_FDBK.Text = strArray[25];
                            SBKT_FDBK.Text = strArray[24];
                            PINT_FDBK.Text = strArray[23];
                            SINT_FDBK.Text = strArray[26];
                            PNOZ_FDBK_Map.Text = (map(Convert.ToInt32(strArray[21]), 17, 1200, 0, 150) / 100.0).ToString("F2");
                            SNOZ_FDBK_Map.Text = (map(Convert.ToInt32(strArray[22]), 17, 1200, 0, 147) / 100.0).ToString("F2");
                            PBKT_FDBK_Map.Text = (map(Convert.ToInt32(strArray[25]), 17, 1200, 0, 147) / 100.0).ToString("F2");
                            SBKT_FDBK_Map.Text = (map(Convert.ToInt32(strArray[24]), 17, 1200, 0, 147) / 100.0).ToString("F2");
                            PINT_FDBK_Map.Text = (map(Convert.ToInt32(strArray[23]), 17, 1200, 0, 147) / 100.0).ToString("F2");
                            SINT_FDBK_Map.Text = (map(Convert.ToInt32(strArray[26]), 17, 1200, 0, 147) / 100.0).ToString("F2");
                            PN_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 4) == false;
                            PI_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 2) == false;
                            PB_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 0) == false;
                            SB_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 3) == false;
                            SN_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 5) == false;
                            SI_Fault.Checked = GETBIT(Convert.ToInt32(strArray[27]), 1) == false;
                        }
                        else
                        {
                            Checksum.Text = "BAD";
                            Status_Message.Text = "BAD Serial Checksum: " + Extracted_Checksum + " vs " + Computed_Checksum;
                        }
                        SerialData.Text = returnStr;
                    }
                }
                catch (TimeoutException ex)
                {
                }
                finally
                {
                }
            }
            return returnStr;
        }

 
*/