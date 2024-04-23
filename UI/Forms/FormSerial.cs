using G_MBIVautoTester._DataObjects;
using G_MBIVautoTester._Globalz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace G_MBIVautoTester.UI.Forms
{
    public partial class FormSerial : Form
    {
        DATA_RX DATA_RX;
        DATA_TX DATA_TX;
        DATA_LABJAK_v1 DATA_LABJAK;
        private int direction = 1; // Direction of slider movement: 1 for forward, -1 for backward
        private int currentRadioButton = 0; // Index of currently selected radio button
        private int cycles = 0; // Count of complete back-and-forth cycles
        private int maxCycles = 5; // Total cycles to perform for each radio button


        public FormSerial()
        {

            DATA_RX = new DATA_RX();
            DATA_TX = new DATA_TX();
            DATA_LABJAK = new DATA_LABJAK_v1();

            InitializeComponent();

            ConsoleStandaloneForm console = new ConsoleStandaloneForm();
            console.Show();
            SERIAL_TIMER_FormSerial.Interval =100;
            SERIAL_TIMER_FormSerial.Tick += SERIAL_TIMER_Tick;
            MNGR_SERIAL.Instance.OpenPortDefault();
            MNGR_SERIAL.Instance.MessageReceived += Instance_MessageReceived;
            SERIAL_TIMER_FormSerial.Start();

         //   MNGR_LABJAK.Instance.Init_dataObj(DATA_LABJAK);

            cb_cmdDiO_0_led1.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            cb_cmdDiO_1_led2.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            cb_cmdDiO_2_alarm.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            cb_cmd_diosafety_9.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);

            tkb_PB.ValueChanged += new EventHandler(tkb_PB_ValueChanged);
            tkb_PN.ValueChanged += new EventHandler(tkb_PN_ValueChanged);
            tkb_PI.ValueChanged += new EventHandler(tkb_PI_ValueChanged);
            tkb_SB.ValueChanged += new EventHandler(tkb_SB_ValueChanged);
            tkb_SN.ValueChanged += new EventHandler(tkb_SN_ValueChanged);
            tkb_SI.ValueChanged += new EventHandler(tkb_SI_ValueChanged);
            tkb_E1.ValueChanged += new EventHandler(tkb_E1_ValueChanged);
            tkb_E2.ValueChanged += new EventHandler(tkb_E2_ValueChanged);


            lbl_MBSerialStatus.BackColor = Color.Salmon;
            lbl_LabjackStatus.BackColor = Color.Salmon;
            lbl_MBSerialStatus.Text = "COMM OFF";
            lbl_LabjackStatus.Text = "Labjack OFF ";//lolz
            lbl_MBSerialStatus.ForeColor = Color.White;
            lbl_LabjackStatus.ForeColor = Color.White;

            tkb_DAC0.ValueChanged += new EventHandler(tkb_DAC0_ValueChanged);

            //cb_Xfer1.CheckedChanged += new EventHandler(cb_Command_changed);
            //cb_Xfer2.CheckedChanged += new EventHandler(cb_Command_changed);
            //cb_DKtr1.CheckedChanged += new EventHandler(cb_Command_changed);
            //cb_DKtr2.CheckedChanged += new EventHandler(cb_Command_changed);

            //radioButton1.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton2.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton3.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton4.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton5.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton6.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton7.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton8.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton9.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //rb7.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton11.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton12.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //radioButton13.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //rb2.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //rb0.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //rb1.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            //rb3.CheckedChanged += new EventHandler(radioButton_CheckedChanged);

            rb0.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb1.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb2.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb4.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb5.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb6.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb7.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb8.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb9.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb10.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb11.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb12.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb13.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb14.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb15.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb16.CheckedChanged += new EventHandler(radioButton_CheckedChanged);

        }

        private void cb_Command_changed(object sender, EventArgs e)
        {

        }

        bool muxbit0 = true;
        bool muxbit1 = true;
        bool muxbit2 = true;
        bool muxbit3 = true;
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rb15.Checked) {
                muxbit0 = false;
                muxbit1 = false;
                muxbit2 = false;
                muxbit3 = false;



     
            }
            if (rb16.Checked)
            {


                muxbit0 = true;
                muxbit1 = false;
                muxbit2 = false;
                muxbit3 = false;
            }
            if (rb14.Checked)
            {

                muxbit0 = false;
                muxbit1 = true;
                muxbit2 = false;
                muxbit3 = false;
            }
            if (rb13.Checked)
            {

                muxbit0 = true;
                muxbit1 = true;
                muxbit2 = false;
                muxbit3 = false;
            }
            if (rb12.Checked)
            {


                muxbit0 = false;
                muxbit1 = false;
                muxbit2 = true;
                muxbit3 = false;
            }
            if (rb11.Checked)
            {


                muxbit0 = true;
                muxbit1 = false;
                muxbit2 = true;
                muxbit3 = false;
            }
            if (rb10.Checked)
            {

                muxbit0 = false;
                muxbit1 = true;
                muxbit2 = true;
                muxbit3 = false;
            }
            if (rb9.Checked)
            {

                muxbit0 = true;
                muxbit1 = true;
                muxbit2 = true;
                muxbit3 = false;
            }
            if (rb8.Checked)
            {


                muxbit0 = false;
                muxbit1 = false;
                muxbit2 = false;
                muxbit3 = true;
            }
            if (rb7.Checked)
            {
                muxbit0 = true;
                muxbit1 = false;
                muxbit2 = false;
                muxbit3 = true;
            }
            if (rb6.Checked)
            {

                muxbit0 = false;
                muxbit1 = true;
                muxbit2 = false;
                muxbit3 = true;

            }
            if (rb5.Checked)
            {
  
                muxbit0 = true;
                muxbit1 = true;
                muxbit2 = false;
                muxbit3 = true;
            }
            if (rb4.Checked)
            {

                muxbit0 = false;
                muxbit1 = false;
                muxbit2 = true;
                muxbit3 = true;
            }
            if (rb2.Checked)
            {

                muxbit0 = true;
                muxbit1 = false;
                muxbit2 = true;
                muxbit3 = true;
            }

            if (rb1.Checked)
            {
                muxbit0 = false;
                muxbit1 = true;
                muxbit2 = true;
                muxbit3 = true;
            }

            if (rb0.Checked)
            {

                muxbit0 = true;
                muxbit1 = true;
                muxbit2 = true;
                muxbit3 = true;
            }

            cb_EIO2.Checked = muxbit0;
            cb_EIO3.Checked = muxbit1;
            cb_EIO4.Checked = muxbit2;
            cb_EIO5.Checked = muxbit3;
        }

        private void tkb_DAC0_ValueChanged(object sender, EventArgs e)
        {
            double val = tkb_DAC0.Value;
            double Converted = val / 100;
            lbl_DAC0.Text = "DAC0: " + Converted.ToString();
            DATA_LABJAK.Labjack_DAC0_Write = Converted;

        }

        private void Instance_MessageReceived(string message)
        {
            try
            {
                if (InvokeRequired)
                {
                    // Safely invoke the method using a lambda that checks if the form is still not disposed
                    Invoke(new Action(() => {
                        if (!this.IsDisposed)
                            DisplayMessage(message);
                    }));
                }
                else
                {
                    if (!this.IsDisposed)
                        DisplayMessage(message);
                }
            }
            catch (ObjectDisposedException)
            {
                // Handle the case where the form or a control is disposed.
                // This catch is just for extra safety and specific logging if needed.
            }
        }
        private void DisplayMessage(string arg_dollaredBody)
        {
            // Update your UI control here
            lbl_RX.Text = arg_dollaredBody;  // Assuming lbl_Reception is a label for displaying the message
            DATA_RX.Update_INTarra_FromCommaDelimitedString(arg_dollaredBody);
        }


        #region liveEvents updateDATA_TX
        private void a_Dio_cmd_CheckChanged(object sender, EventArgs e)
        {
             DATA_TX.SetDIO(cb_cmdDiO_0_led1.Checked, cb_cmdDiO_1_led2.Checked, cb_cmdDiO_2_alarm.Checked, cb_cmd_diosafety_9.Checked);
        }

        private void tkb_PB_ValueChanged(object sender, EventArgs e)
        {
            lbl_PB.Text = "PB: " + tkb_PB.Value.ToString() + "";
            DATA_TX.PB_1 = tkb_PB.Value;
        }
        private void tkb_PN_ValueChanged(object sender, EventArgs e)
        {
            lbl_PN.Text = "PN: " + tkb_PN.Value.ToString() + "";
            DATA_TX.PN_2 = tkb_PN.Value;
        }
        private void tkb_PI_ValueChanged(object sender, EventArgs e)
        {
            lbl_PI.Text = "PI: " + tkb_PI.Value.ToString() + "";
            DATA_TX.PI_3 = tkb_PI.Value;
        }
        private void tkb_SB_ValueChanged(object sender, EventArgs e)
        {
            lbl_SB.Text = "SB: " + tkb_SB.Value.ToString() + "";
            DATA_TX.SB_4 = tkb_SB.Value;
        }
        private void tkb_SN_ValueChanged(object sender, EventArgs e)
        {
            lbl_SN.Text = "SN: " + tkb_SN.Value.ToString() + "";
            DATA_TX.SN_5= tkb_SN.Value;
        }
        private void tkb_SI_ValueChanged(object sender, EventArgs e)
        {
            lbl_SI.Text = "SI: " + tkb_SI.Value.ToString() + "";
            DATA_TX.SI_6 = tkb_SI.Value;
        }
        private void tkb_E1_ValueChanged(object sender, EventArgs e)
        {
            lbl_E1.Text = "PE: " + tkb_E1.Value.ToString() + "";
            DATA_TX.PE_7 = tkb_E1.Value;
        }
        private void tkb_E2_ValueChanged(object sender, EventArgs e)
        {
            lbl_E2.Text = "SE: " + tkb_E2.Value.ToString() + "";
            DATA_TX.SE_8 = tkb_E2.Value;
        }

        #endregion
        private void SERIAL_TIMER_Tick(object sender, EventArgs e)
        {
             //label1.Text = MNGR_SERIAL.Instance.GetLatest_Valide_MessageBody();
            string Full_TX = DATA_TX.CREATE_FullString_for_TX();
            lbl_TX.Text = Full_TX;
            MNGR_SERIAL.Instance.WriteData(Full_TX);

            //lbl_RX.Text = MNGR_SERIAL.Instance.GetLatest_Valide_MessageBody();
            lbl_version.Text = DATA_RX.Version;
            LBL_AD1.Text = "AD01:"+DATA_RX.AIN1.ToString();
            LBL_AD2.Text = "AD02:"+DATA_RX.AIN2.ToString();
            LBL_AD3.Text = "AD03:"+DATA_RX.AIN3.ToString();
            LBL_AD4.Text = "AD04:"+DATA_RX.AIN4.ToString();
            LBL_AD5.Text = "AD05:"+DATA_RX.AIN5.ToString();
            LBL_AD6.Text = "AD06:"+DATA_RX.AIN6.ToString();
            LBL_AD7.Text ="AD07:"+ DATA_RX.AIN7.ToString(); 
            LBL_AD8.Text = "AD08:"+DATA_RX.AIN8.ToString();
            lbl_AD9.Text = "AD09:"+DATA_RX.AIN9.ToString();
            lbl_AD10.Text = "AD10:"+DATA_RX.AIN10.ToString();
            lbl_AD11.Text = "AD11:"+DATA_RX.AIN11.ToString();
            lbl_AD12.Text = "AD12:"+DATA_RX.AIN12.ToString();
            lbl_AD13.Text = "AD13:"+DATA_RX.AIN13.ToString();
            lbl_AD14.Text = "AD14:"+DATA_RX.AIN14.ToString();
            lbl_AD15.Text = "AD15:"+DATA_RX.AIN15.ToString();
            lbl_AD16.Text = "AD16:"+DATA_RX.AIN16.ToString();

            cb_Clutch1.Checked = DATA_RX.GP7_pClutch;
            cb_Clutch2.Checked = DATA_RX.GP0_sClutch;
            cb_DKtr1.Checked = DATA_RX.GP3_Dktr1;
            cb_DKtr2.Checked = DATA_RX.GP4_DKtr2;
            cb_Xfer1.Checked = DATA_RX.GP5_Xfer1;
            cb_Xfer2.Checked = DATA_RX.GP6_Xfer2;

            PNOZ_FDBK.Text = "PNOZ:"+DATA_RX.PNOZ_FDBK_21.ToString();
            SNOZ_FDBK.Text = "SNOZ:"+DATA_RX.SNOZ_FDBK_22.ToString();
            PINT_FDBK.Text = "PINT:"+DATA_RX.PINT_FDBK_23.ToString();
            SBKT_FDBK.Text = "SBKT:"+DATA_RX.SBKT_FDBK_24.ToString();
            PBKT_FDBK.Text = "PBKT:"+DATA_RX.PBKT_FDBK_25.ToString();
            SINT_FDBK.Text = "SINT:"+DATA_RX.SINT_FDBK_26.ToString();

            if (MNGR_SERIAL.Instance.Get_CommIsOpen())
            {
                lbl_MBSerialStatus.BackColor = Color.SeaGreen;
                lbl_MBSerialStatus.Text = "COMM ON";
                lbl_MBSerialStatus.ForeColor = Color.Black;
            }
            else { 
                lbl_MBSerialStatus.BackColor = Color.Salmon;
                lbl_MBSerialStatus.Text = "COMM OFF";
                lbl_MBSerialStatus.ForeColor = Color.White;
            }

            if(MNGR_LABJAK.Instance.GetIsOnBus())
            {
                lbl_LabjackStatus.BackColor = Color.SeaGreen;
                lbl_LabjackStatus.Text = "Labjack ON";
                lbl_LabjackStatus.ForeColor = Color.Black;
            }
            else
            {
                lbl_LabjackStatus.BackColor = Color.Salmon;
                lbl_LabjackStatus.Text = "Labjack OFF";
                lbl_LabjackStatus.ForeColor = Color.White;
            }


            //DATA_LABJAK.Labjack_EIO2_Write = cb_EIO2.Checked ? 1 : 0;
            //DATA_LABJAK.Labjack_EIO3_Write = cb_EIO3.Checked ? 1 : 0;
            //DATA_LABJAK.Labjack_EIO4_Write = cb_EIO4.Checked ? 1 : 0;
            //DATA_LABJAK.Labjack_EIO5_Write = cb_EIO5.Checked ? 1 : 0;

            DATA_LABJAK.Labjack_EIO2_Write = muxbit0 ? 0 : 1;
            DATA_LABJAK.Labjack_EIO3_Write = muxbit1 ? 0 : 1;
            DATA_LABJAK.Labjack_EIO4_Write = muxbit2 ? 0 : 1;
            DATA_LABJAK.Labjack_EIO5_Write = muxbit3 ? 0 : 1;


            double val = tkb_DAC0.Value;
            double Converted = val / 100;
            lbl_DAC0.Text = "DAC0: " + Converted.ToString();
            DATA_LABJAK.Labjack_DAC0_Write = Converted;


            double xfer1Val = cb_xfer1CMD.Checked ? 1 : 0;
            double xfer2Val = cb_xfer2CMD.Checked ? 1 : 0;
            double dktr1Val = cb_DK1CMD.Checked ? 1 : 0;
            double dktr2Val = cb_DK2CMD.Checked ? 1 : 0;
            DATA_LABJAK.Labjack_FIO0_Write = xfer1Val;
            DATA_LABJAK.Labjack_FIO1_Write = xfer2Val;
            DATA_LABJAK.Labjack_FIO2_Write = dktr1Val;
            DATA_LABJAK.Labjack_FIO3_Write = dktr2Val;

            MNGR_LABJAK.Instance.Update_dataObj_withAINdata();
            lbl_lbjk_0.Text = "ain0: " + DATA_LABJAK.Lbjk_VoltsRead_0_alarm.ToString();
            lbl_lbjk_1.Text = "ain1: " + DATA_LABJAK.Labjack_VoltsRead_1.ToString();
            lbl_lbjk_2.Text = "ain2: " + DATA_LABJAK.Labjack_VoltsRead_2.ToString();
            lbl_lbjk_3.Text = "ain3: " + DATA_LABJAK.Labjack_VoltsRead_3.ToString();
            lbl_lbjk_4.Text = "ain4: " + DATA_LABJAK.Labjack_VoltsRead_4.ToString();
            lbl_lbjk_5.Text = "ain5: " + DATA_LABJAK.Labjack_VoltsRead_5.ToString();
            lbl_lbjk_6.Text = "ain6: " + DATA_LABJAK.Labjack_VoltsRead_6.ToString();
            lbl_lbjk_7.Text = "ain7: " + DATA_LABJAK.Labjack_VoltsRead_7.ToString();
            lbl_lbjk_8.Text = "ain8: " + DATA_LABJAK.Labjack_VoltsRead_8.ToString();
            lbl_lbjk_9.Text = "ain9: " + DATA_LABJAK.Labjack_VoltsRead_9.ToString();
            lbl_lbjk_10.Text = "ain10: " + DATA_LABJAK.Labjack_VoltsRead_10.ToString();
            lbl_lbjk_11.Text = "ain11: " + DATA_LABJAK.Labjack_VoltsRead_11.ToString();
            lbl_lbjk_12.Text = "ain12: " + DATA_LABJAK.Labjack_VoltsRead_12.ToString();


            if(DATA_LABJAK.Lbjk_EIO0_Read_led1 == 0)
            {
                lbl_LED1_EIO0.BackColor = Color.SeaGreen;
                lbl_LED1_EIO0.Text = "LED1 Actual Output: ON";
            }
            else
            {
                lbl_LED1_EIO0.BackColor = Color.Salmon;
                lbl_LED1_EIO0.Text = "LED1 Actual Output: ff";
            }
            
            if (DATA_LABJAK.Lbjk_EIO1_Read_led2 == 0)
            {
                lbl_LED2_EIO1.BackColor = Color.SeaGreen;
                lbl_LED2_EIO1.Text = "LED2 Actual Output: ON";
            }
            else
            {
                lbl_LED2_EIO1.BackColor = Color.Salmon;
                lbl_LED2_EIO1.Text = "LED2 Actual Output: ff";
            }

            if (DATA_LABJAK.Lbjk_VoltsRead_0_alarm < 5)
            {
                lbl_Alarm_AIN0.BackColor = Color.SeaGreen;
                lbl_Alarm_AIN0.Text = "Alarm: ON";
            }
            else { 
            
                    lbl_Alarm_AIN0.BackColor = Color.Salmon;
                lbl_Alarm_AIN0.Text = "Alarm: OFF";
            }
         
        }


    }
}
