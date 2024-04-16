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
        public FormSerial()
        {

            DATA_RX = new DATA_RX();
            DATA_TX = new DATA_TX();

            InitializeComponent();
            ConsoleStandaloneForm console = new ConsoleStandaloneForm();
            console.Show();
            SERIAL_TIMER.Interval =100;
            SERIAL_TIMER.Tick += SERIAL_TIMER_Tick;
            MNGR_SERIAL.Instance.OpenPortDefault();
            MNGR_SERIAL.Instance.MessageReceived += Instance_MessageReceived;
            SERIAL_TIMER.Start();

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

        }

        private void Instance_MessageReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayMessage(message)));
            }
            else
            {
                DisplayMessage(message);
            }
        }
        private void DisplayMessage(string arg_dollaredBody)
        {
            // Update your UI control here
            lbl_RX.Text = arg_dollaredBody;  // Assuming lbl_Reception is a label for displaying the message
            DATA_RX.Update_FromCommaDelimitedString(arg_dollaredBody);
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

        }
    }
}
