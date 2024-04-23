using G_MBIVautoTester._DataObjects;
using G_MBIVautoTester._DataObjects.DataComm;
using G_MBIVautoTester._Globalz;
using G_MBIVautoTester.UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G_MBIVautoTester.UI.Pages
{
    public partial class Form_Page4 : Form
    {
        DATA_TX DATA_TX;
        DATA_RX DATA_RX;
        DATA_LABJAK_v2 _MAINLabjackObj;
        private System.Windows.Forms.Timer SERIAL_TIMER_FormSerial = new System.Windows.Forms.Timer();
        private DateTime lastReceivedTime = DateTime.MinValue;
        bool isConnected_Serial = false;
        bool isConnected_Labjack = false;
        public Form_Page4()
        {
            InitializeComponent();
            DATA_TX = new DATA_TX();
            DATA_RX = new DATA_RX();
            _MAINLabjackObj = new DATA_LABJAK_v2();
            ConsoleStandaloneForm console = new ConsoleStandaloneForm();
            console.Show();
            SERIAL_TIMER_FormSerial.Interval = 300;
            SERIAL_TIMER_FormSerial.Tick += SERIAL_TIMER_Tick;
            MNGR_SERIAL.Instance.OpenPortDefault();
            MNGR_SERIAL.Instance.MessageReceived += Instance_MessageReceived_serial;
            SERIAL_TIMER_FormSerial.Start();
 
 
            isConnected_Serial = MNGR_SERIAL.Instance.Get_CommIsOpen();
            if (isConnected_Serial)
            {
                btn_openSerial.Text = "Close it";

            }
            else
            {
                btn_openSerial.Text = "Open it";
            }

            isConnected_Labjack = MNGR_LABJAK.Instance.GetIsOnBus();
            if (isConnected_Labjack)
            {
                btn_openLabjack.Text = "turn off";
            }
            else
            {
                btn_openLabjack.Text = "turn on";
            }
            MNGR_LABJAK.Instance.Init_dataObj2(_MAINLabjackObj);



            cb_cmdDiO_0_led1.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            cb_cmdDiO_1_led2.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            cb_cmdDiO_2_alarm.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            btn_openSerial.Click += new EventHandler(btn_openSerial_Click);
            btn_openLabjack.Click += new EventHandler(btn_openLabjack_Click);

        }

        private void SERIAL_TIMER_Tick(object sender, EventArgs e)
        {
            _mustUpdate_ComAndLabjack_Status();
            //label1.Text = MNGR_SERIAL.Instance.GetLatest_Valide_MessageBody();
            string Full_TX = DATA_TX.CREATE_FullString_for_TX();
            lbl_TX.Text = Full_TX;
            MNGR_SERIAL.Instance.WriteData(Full_TX);

            MNGR_LABJAK.Instance.READ_LABjackv2_AINS();
            _mustUpdate_ReadingsLabjackStatus();

        }



        private void a_Dio_cmd_CheckChanged(object sender, EventArgs e)
        {
            DATA_TX.SetDIO(cb_cmdDiO_0_led1.Checked, cb_cmdDiO_1_led2.Checked, cb_cmdDiO_2_alarm.Checked, cb_cmd_diosafety_9.Checked);
        }

        #region MEssageReceived_update ints arra
        private void Instance_MessageReceived_serial(string message)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan interval = TimeSpan.Zero;

                if (lastReceivedTime != DateTime.MinValue)
                {
                    interval = currentTime - lastReceivedTime;
                }
                lastReceivedTime = currentTime;

                if (InvokeRequired)
                {
                    Invoke(new Action(() => {
                        if (!this.IsDisposed)
                        {
                            DisplayMessage(message, interval);

                        }
                    }));
                }
                else
                {
                    if (!this.IsDisposed)
                    {
                        DisplayMessage(message, interval);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Handle the case where the form or a control is disposed.
                // This catch is just for extra safety and specific logging if needed.
            }
        }
        private void DisplayMessage(string arg_dollaredBody, TimeSpan arginterval)
        {
            lbl_RX.Text = $"{arg_dollaredBody} (Interval: {arginterval.TotalMilliseconds} ms)";
            DATA_RX.Update_INTarra_FromCommaDelimitedString(arg_dollaredBody);
     
        }


        void _mustUpdate_ComAndLabjack_Status()
        {

            if (MNGR_SERIAL.Instance.Get_CommIsOpen())
            {
                lbl_MBSerialStatus.BackColor = Color.SeaGreen;
                lbl_MBSerialStatus.Text = "COMM ON";
                lbl_MBSerialStatus.ForeColor = Color.Black;
                isConnected_Serial = true;
                btn_openSerial.Text = "Close it";
            }
            else
            {
                lbl_MBSerialStatus.BackColor = Color.Salmon;
                lbl_MBSerialStatus.Text = "COMM OFF";
                lbl_MBSerialStatus.ForeColor = Color.White;
                isConnected_Serial = false;
                btn_openSerial.Text = "Open it";
            }

            if (MNGR_LABJAK.Instance.GetIsOnBus())
            {
                lbl_LabjackStatus.BackColor = Color.SeaGreen;
                lbl_LabjackStatus.Text = "Labjack ON";
                lbl_LabjackStatus.ForeColor = Color.Black;
                isConnected_Labjack = true;
                btn_openLabjack.Text = "turn off";
            }
            else
            {
                lbl_LabjackStatus.BackColor = Color.Salmon;
                lbl_LabjackStatus.Text = "Labjack OFF";
                lbl_LabjackStatus.ForeColor = Color.White;
                isConnected_Labjack = false;
                btn_openLabjack.Text = "turn on";
            }


        }
        void _mustUpdate_ReadingsLabjackStatus() {
            if (_MAINLabjackObj.AINs_values[2] == 0)
            {
                lbl_LED1_EIO0.BackColor = Color.SeaGreen;
                lbl_LED1_EIO0.Text = "LED1 Actual Output: ON";
            }
            else
            {
                lbl_LED1_EIO0.BackColor = Color.Salmon;
                lbl_LED1_EIO0.Text = "LED1 Actual Output: ff";
            }

            if (_MAINLabjackObj.AINs_values[3] == 0)
            {
                lbl_LED2_EIO1.BackColor = Color.SeaGreen;
                lbl_LED2_EIO1.Text = "LED2 Actual Output: ON";
            }
            else
            {
                lbl_LED2_EIO1.BackColor = Color.Salmon;
                lbl_LED2_EIO1.Text = "LED2 Actual Output: ff";
            }

            if (_MAINLabjackObj.AINs_values[0] < 5)
            {
                lbl_Alarm_AIN0.BackColor = Color.SeaGreen;
                lbl_Alarm_AIN0.Text = "Alarm: ON";
            }
            else
            {

                lbl_Alarm_AIN0.BackColor = Color.Salmon;
                lbl_Alarm_AIN0.Text = "Alarm: OFF";
            }
        }
        #endregion

        #region buttons com and labjack open cose
        private void btn_openLabjack_Click(object sender, EventArgs e)
        {
            isConnected_Labjack = MNGR_LABJAK.Instance.GetIsOnBus();
            if (isConnected_Labjack)
            {
                MNGR_LABJAK.Instance.Close();
                btn_openLabjack.Text = "turn on";
            }
            else
            {
                MNGR_LABJAK.Instance.Init_cONNECTION();
                MNGR_LABJAK.Instance.Init_dataObj2(_MAINLabjackObj);
                btn_openLabjack.Text = "turn off";
            }

        }
        private void btn_openSerial_Click(object sender, EventArgs e)
        {
            isConnected_Serial = MNGR_SERIAL.Instance.Get_CommIsOpen();

            if (isConnected_Serial)
            {

                btn_openSerial.Text = "Open it";
                MNGR_SERIAL.Instance.ClosePort();
            }
            else
            {
                btn_openSerial.Text = "Close it";
                MNGR_SERIAL.Instance.OpenPortDefault();
            }
        }

        #endregion

    }
}


//private void Instance_MessageReceived(string message)
//{
//    try
//    {
//        if (InvokeRequired)
//        {
//            // Safely invoke the method using a lambda that checks if the form is still not disposed
//            Invoke(new Action(() => {
//                if (!this.IsDisposed)
//                    DisplayMessage(message);
//            }));
//        }
//        else
//        {
//            if (!this.IsDisposed)
//                DisplayMessage(message);
//        }
//    }
//    catch (ObjectDisposedException)
//    {
//        // Handle the case where the form or a control is disposed.
//        // This catch is just for extra safety and specific logging if needed.
//    }
//}
//private void DisplayMessage(string arg_dollaredBody)
//{
//    // Update your UI control here
//    lbl_RX.Text = arg_dollaredBody;  // Assuming lbl_Reception is a label for displaying the message
//    DATA_RX.Update_INTarra_FromCommaDelimitedString(arg_dollaredBody);
//}
