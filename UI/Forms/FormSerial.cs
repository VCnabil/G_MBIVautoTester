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
            SERIAL_TIMER.Interval = 100;
            SERIAL_TIMER.Tick += SERIAL_TIMER_Tick;
            MNGR_SERIAL.Instance.OpenPortDefault();
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
        }
    }
}
