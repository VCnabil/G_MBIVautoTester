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
    public partial class Form_Page3 : Form
    {

        private System.Windows.Forms.Timer TimerTICKER_sendJackfast = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer TimerTICKER_readJackslow = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timer_oneSecond = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timer_powerLevelAdjustment = new System.Windows.Forms.Timer();

        DATA_RX DATA_RX;
        bool isConnected_Serial = false;
        bool isConnected_Labjack = false;
        DATA_LABJAK_v2 _MAINLabjackObj;
        bool muxbit0 = true;
        bool muxbit1 = true;
        bool muxbit2 = true;
        bool muxbit3 = true;
        double _RAW_DACTOSEND = 0.0;


        int sample_cnt = 0;
        int MAXsamples = 10;

        Label[] _lbls_ADOs;
        int[] _ints_ADOS;

        int _ACTIVEAIN = 1;
        private DateTime lastReceivedTime = DateTime.MinValue;
        private System.Windows.Forms.Timer timer_delay = new System.Windows.Forms.Timer();

        private bool allowedToFilterMinMax = true;
        int minValue, maxValue;
        private int _ACTIVELEVEL = 0;
        Label[,] _labels2D_minmax;
        Label[] floatingColumn;
        int indexFloating = 16;
        int _minFloating, _maxFloating;
        public Form_Page3()
        {
            InitializeComponent();
   
            minValue = int.MaxValue;
            maxValue = int.MinValue;

            ConsoleStandaloneForm console = new ConsoleStandaloneForm();
            console.Show();
            TimerTICKER_sendJackfast.Interval = 120;
            TimerTICKER_sendJackfast.Tick += new EventHandler(TimerTICKER_TickSendFastLABJACK);

            TimerTICKER_readJackslow.Interval = 320;
            TimerTICKER_readJackslow.Tick += new EventHandler(TimerTICKER_TickReadSlowLABJACK);
            timer_delay.Interval = 700;  // 700 ms
            timer_delay.Tick += new EventHandler(timer_delay_Tick);

            timer_oneSecond.Interval = 3000;
            timer_oneSecond.Tick += new EventHandler(timer_oneSecond_Tick);

            timer_powerLevelAdjustment.Interval = 500; // 500 ms for power level adjustment
            timer_powerLevelAdjustment.Tick += new EventHandler(timer_powerLevelAdjustment_Tick);


            DATA_RX = new DATA_RX();
            _MAINLabjackObj = new DATA_LABJAK_v2();
            _lbls_ADOs = new Label[] { LBL_AD0, LBL_AD1, LBL_AD2, LBL_AD3, LBL_AD4, LBL_AD5, LBL_AD6, LBL_AD7, LBL_AD8, LBL_AD9, lbl_AD10, lbl_AD11, lbl_AD12, lbl_AD13, lbl_AD14, lbl_AD15, lbl_AD16 };
            _labels2D_minmax = new Label[,]
             {
                { label_0_0, label_0_1, label_0_2 },
                { label_1_0, label_1_1, label_1_2 },
                { label_2_0, label_2_1, label_2_2 },
                { label_3_0, label_3_1, label_3_2 },
                { label_4_0, label_4_1, label_4_2 },
                { label_5_0, label_5_1, label_5_2 },
                { label_6_0, label_6_1, label_6_2 },
                { label_7_0, label_7_1, label_7_2 },
                { label_8_0, label_8_1, label_8_2 },
                { label_9_0, label_9_1, label_9_2 },
                { label_10_0, label_10_1, label_10_2 },
                { label_11_0, label_11_1, label_11_2 },
                { label_12_0, label_12_1, label_12_2 },
                { label_13_0, label_13_1, label_13_2 },
                { label_14_0, label_14_1, label_14_2 },
                { label_15_0, label_15_1, label_15_2 },
                { label_16_0, label_16_1, label_16_2 }
             };
             floatingColumn = new Label[] { label_0_3, label_1_3, label_2_3, label_3_3, label_4_3, label_5_3, label_6_3, label_7_3, label_8_3, label_9_3, label_10_3, label_11_3, label_12_3, label_13_3, label_14_3, label_15_3, label_16_3 };
            _ints_ADOS = DATA_RX.GET_allAINS();
            for (int i = 0; i < _lbls_ADOs.Length; i++)
            {
                _lbls_ADOs[i].Text = "AD"+i+": "+ _ints_ADOS[i].ToString();
            }
            btn_openSerial.Click += new EventHandler(btn_openSerial_Click);
            btn_openLabjack.Click += new EventHandler(btn_openLabjack_Click);

            MNGR_SERIAL.Instance.OpenPortDefault();
            MNGR_SERIAL.Instance.MessageReceived += Instance_MessageReceived_serial;
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
            TimerTICKER_sendJackfast.Enabled = true;
            TimerTICKER_sendJackfast.Start();

            TimerTICKER_readJackslow.Enabled = true;
            TimerTICKER_readJackslow.Start();


            button1.Click += new EventHandler(button1_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartProcess();
        }

        private void timer_powerLevelAdjustment_Tick(object sender, EventArgs e)
        {
            timer_powerLevelAdjustment.Stop(); // Stop the power level adjustment timer

        }

        private void timer_oneSecond_Tick(object sender, EventArgs e)
        {
            timer_oneSecond.Stop();
        }

        private void timer_delay_Tick(object sender, EventArgs e)
        {
            // Toggle the flag to allow buffer reading
            allowedToFilterMinMax = true;
            timer_delay.Stop();  // Stop the delay timer until it's needed again
        }

        private void TimerTICKER_TickSendFastLABJACK(object sender, EventArgs e)
        {
            _mustUpdate_ComAndLabjack_Status();

            if (!isConnected_Labjack || !isConnected_Serial)
            {
                return;
            }


            _gatherAndSendDataToLabjack();


        }
        private void TimerTICKER_TickReadSlowLABJACK(object sender, EventArgs e)
        {
            if (allowedToFilterMinMax)
            {
                int value = _ints_ADOS[_ACTIVEAIN];
                minValue = Math.Min(minValue, value);
                maxValue = Math.Max(maxValue, value);
            }
        }
        //private void StartProcess()
        //{
        //    for (int power = 0; power < 3; power++)  
        //    {
        //        _ACTIVELEVEL = power;
        //        //   _RAW_DACTOSEND = 2.5;

        //        if (power == 0)
        //        {
        //            _RAW_DACTOSEND = 0;
        //        }
        //        else if (power == 1)
        //        {
        //            _RAW_DACTOSEND = 2.5;
        //        }
        //        else
        //            if (power == 2)
        //        {
        //            _RAW_DACTOSEND = 5.0;
        //        }

        //        timer_powerLevelAdjustment.Start(); 
        //        while (timer_powerLevelAdjustment.Enabled) { Application.DoEvents(); }  
        //        for (int i = 1; i < 17; i++)
        //        {
        //            _ACTIVEAIN = i;
        //            timer_delay.Start();
        //            while (timer_delay.Enabled) { Application.DoEvents(); }
        //            minValue = int.MaxValue;
        //            maxValue = int.MinValue;
        //            allowedToFilterMinMax = true;
        //            TimerTICKER_readJackslow.Start();
        //            timer_oneSecond.Start();
        //            while (timer_oneSecond.Enabled) { Application.DoEvents(); }
        //            allowedToFilterMinMax = false;
        //            TimerTICKER_readJackslow.Stop();
        //            lbl_calcMinmax.Text = $"Power Level {_ACTIVELEVEL}, Index {i}: Min={minValue}, Max={maxValue}";
        //        }
        //    }
        //}

        private async void StartProcess()
        {
            for (int power = 0; power < 3; power++)
            {
                _ACTIVELEVEL = power;
                if (power == 0)
                {
                    _RAW_DACTOSEND = 0;
                }
                else if (power == 1)
                {
                    _RAW_DACTOSEND = 2.5;
                }
                else if (power == 2)
                {
                    _RAW_DACTOSEND = 5.0;
                }
                minValue = int.MaxValue;
                maxValue = int.MinValue;
                await SetPowerLevel();
                for (int i = 1; i < 17; i++)
                {
                    _ACTIVEAIN = i;
                    await WaitForDataCollection();
                    lbl_calcMinmax.Text = $"Power Level {_ACTIVELEVEL}, Index {i}: Min={minValue}, Max={maxValue}";
                    _labels2D_minmax[i, _ACTIVELEVEL].Text = $"{minValue}|{maxValue}";
                }
            }
        }

        private async Task SetPowerLevel()
        {
            timer_powerLevelAdjustment.Start();
            await Task.Delay(timer_powerLevelAdjustment.Interval);
            timer_powerLevelAdjustment.Stop();
        }

        private async Task WaitForDataCollection()
        {
            minValue = int.MaxValue;
            maxValue = int.MinValue;
            allowedToFilterMinMax = true;

            TimerTICKER_readJackslow.Start();
            timer_oneSecond.Start();
            await Task.Delay(timer_oneSecond.Interval);
            TimerTICKER_readJackslow.Stop();
            timer_oneSecond.Stop();
            allowedToFilterMinMax = false;
        }





        void _mustUpdate_ComAndLabjack_Status()
        {

            if (MNGR_SERIAL.Instance.Get_CommIsOpen())
            {
                lbl_MBSerialStatus.BackColor = Color.SeaGreen;
                lbl_MBSerialStatus.Text = "COMM ON";
                lbl_MBSerialStatus.ForeColor = Color.Black;
                isConnected_Serial= true;
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

        void _gatherAndSendDataToLabjack() {

            UpdateMux_per_channel(_ACTIVEAIN);

            _MAINLabjackObj.MUXDAC_values[0] = muxbit0 ? 0 : 1;
            _MAINLabjackObj.MUXDAC_values[1] = muxbit1 ? 0 : 1;
            _MAINLabjackObj.MUXDAC_values[2] = muxbit2 ? 0 : 1;
            _MAINLabjackObj.MUXDAC_values[3] = muxbit3 ? 0 : 1;
            _MAINLabjackObj.MUXDAC_values[4] = _RAW_DACTOSEND;
            MNGR_LABJAK.Instance.WRITE_to_JACKv2();
        }
        void UpdateMux_per_channel(int arg_Channel)
        {
            switch (arg_Channel)
            {
                case 1:

                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
                case 2:

                    muxbit0 = false;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
                case 3:
                    muxbit0 = true;
                    muxbit1 = false;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
                case 4:
                    muxbit0 = false;
                    muxbit1 = false;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;

                case 5:
                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = false;
                    muxbit3 = true;
                    break;

                case 6:
                    muxbit0 = false;
                    muxbit1 = true;
                    muxbit2 = false;
                    muxbit3 = true;
                    break;
                case 7:
                    muxbit0 = true;
                    muxbit1 = false;
                    muxbit2 = false;
                    muxbit3 = true;
                    break;
                case 8:
                    muxbit0 = false;
                    muxbit1 = false;
                    muxbit2 = false;
                    muxbit3 = true;
                    break;
                case 9:
                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = false;
                    break;
                case 10:
                    muxbit0 = false;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = false;
                    break;
                case 11:
                    muxbit0 = true;
                    muxbit1 = false;
                    muxbit2 = true;
                    muxbit3 = false;
                    break;
                case 12:
                    muxbit0 = false;
                    muxbit1 = false;
                    muxbit2 = true;
                    muxbit3 = false;
                    break;
                case 13:

                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = false;
                    muxbit3 = false;
                    break;

                case 14:
                    muxbit0 = false;
                    muxbit1 = true;
                    muxbit2 = false;
                    muxbit3 = false;
                    break;
                case 15:
                    muxbit0 = false;
                    muxbit1 = false;
                    muxbit2 = false;
                    muxbit3 = false;
                    break;
                case 16:
                    muxbit0 = true;
                    muxbit1 = false;
                    muxbit2 = false;
                    muxbit3 = false;
                    break;
                default:
                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
            }

            cb_EIO2.Checked = muxbit0;
            cb_EIO3.Checked = muxbit1;
            cb_EIO4.Checked = muxbit2;
            cb_EIO5.Checked = muxbit3;
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
            for (int i = 0; i < _lbls_ADOs.Length; i++)
            {
                _lbls_ADOs[i].Text = "AD" + i + ": " + _ints_ADOS[i].ToString();
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

 