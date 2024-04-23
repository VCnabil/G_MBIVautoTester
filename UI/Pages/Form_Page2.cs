using G_MBIVautoTester._DataObjects;
using G_MBIVautoTester._DataObjects.DataComm;
using G_MBIVautoTester._DataObjects.DataTestReport;
using G_MBIVautoTester._Globalz;
using G_MBIVautoTester.UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G_MBIVautoTester.UI.Pages
{
    public partial class Form_Page2 : Form
    {
        Label[] Latest_updated_ADvalues;
        RadioButton[] RBS_CUR_LEVEL_Columns;
        double[] doubles_powerLavels = new double[3];
        double _RAW_DACTOSEND = 0.0;
        Label[,] _labels2D_minmax;
        RadioButton[] RBS_CUR_AD_Rows;
        int _curIndx_Column_X = 0;
        int _curIndx_Row_Y = 0;
        int minValue, maxValue;
        bool isTesting = false;
        Random random = new Random();
        Label[] floatingColumn;
        int indexFloating = 16;
        int _minFloating , _maxFloating;
        data_minmaxRow[] data_Minmaxes;


        DATA_RX DATA_RX;
        DATA_TX DATA_TX;
        DATA_LABJAK_v1 DATA_LABJAK;
        DATA_TESTREPORT DATA_testrep;
        bool muxbit0 = true;
        bool muxbit1 = true;
        bool muxbit2 = true;
        bool muxbit3 = true;

        double _xfer1Val = 1;
        double _xfer2Val = 1;
        double _dktr1Val = 1;
        double _dktr2Val = 1;
        private System.Windows.Forms.Timer delayTimer = new System.Windows.Forms.Timer();

        public Form_Page2()
        {
            InitializeComponent();
            ConsoleStandaloneForm console = new ConsoleStandaloneForm();
            console.Show();
            SetupControls();

            DATA_RX = new DATA_RX();
            DATA_TX = new DATA_TX();
            DATA_LABJAK = new DATA_LABJAK_v1();
            DATA_testrep = new DATA_TESTREPORT();
            MNGR_SERIAL.Instance.OpenPortDefault();
            MNGR_SERIAL.Instance.MessageReceived += Instance_MessageReceived;
            MNGR_LABJAK.Instance.Init_dataObj(DATA_LABJAK);

            timer0_COMM.Interval = 100;
            timer0_COMM.Tick += Timer0_COMM_Tick;
            timer0_COMM.Start();
        }

        private void Timer0_COMM_Tick(object sender, EventArgs e)
        {
            _mustUpdate_ComAndLabjack_Status();
            // 5. GET from LABJAK . doesnt need to be in a looping sample
            MNGR_LABJAK.Instance.READ_from_JAck();
            _mustUpdate_DigitalValuesRead_fromLAbjack();
        }

        void _mustUpdate_ComAndLabjack_Status()
        {

            if (MNGR_SERIAL.Instance.Get_CommIsOpen())
            {
                lbl_MBSerialStatus.BackColor = Color.SeaGreen;
                lbl_MBSerialStatus.Text = "COMM ON";
                lbl_MBSerialStatus.ForeColor = Color.Black;
            }
            else
            {
                lbl_MBSerialStatus.BackColor = Color.Salmon;
                lbl_MBSerialStatus.Text = "COMM OFF";
                lbl_MBSerialStatus.ForeColor = Color.White;
            }

            if (MNGR_LABJAK.Instance.GetIsOnBus())
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
        void _mustUpdate_DigitalValuesRead_fromLAbjack()
        {

            if (DATA_LABJAK.Lbjk_EIO0_Read_led1 == 0)
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
            else
            {

                lbl_Alarm_AIN0.BackColor = Color.Salmon;
                lbl_Alarm_AIN0.Text = "Alarm: OFF";
            }
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
                        {
                            DisplayMessage(message);
                        }
                    }));
                }
                else
                {
                    if (!this.IsDisposed)
                    {
                        DisplayMessage(message);
                    }
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




        void UpdateMux_per_channel(int arg_Channel) {
            switch(arg_Channel)
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

        private void SetupControls()
        {
            data_Minmaxes = new data_minmaxRow[17];
            for (int i = 0; i < data_Minmaxes.Length; i++)
            {
                data_Minmaxes[i] = new data_minmaxRow();
            }

            Latest_updated_ADvalues = new Label[] { LBL_AD0, LBL_AD1, LBL_AD2, LBL_AD3, LBL_AD4, LBL_AD5 , LBL_AD6, LBL_AD7, LBL_AD8, LBL_AD9, lbl_AD10, lbl_AD11, lbl_AD12, lbl_AD13, lbl_AD14, lbl_AD15, lbl_AD16 };

            RBS_CUR_LEVEL_Columns = new RadioButton[] { rb_0_low, rb_1_mid, rb_2_high };
            doubles_powerLavels = new double[] { 0.0, 2.5, 5.0 };

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
            RBS_CUR_AD_Rows = new RadioButton[] { rb_NA, rb1, rb2, rb3, rb4 , rb5, rb6, rb7, rb8, rb9, rb10,rb11, rb12, rb13, rb14,rb15,rb16};
            floatingColumn = new Label[] { label_0_3, label_1_3, label_2_3, label_3_3, label_4_3, label_5_3, label_6_3, label_7_3, label_8_3, label_9_3, label_10_3, label_11_3, label_12_3, label_13_3, label_14_3, label_15_3, label_16_3 };


            delayTimer.Interval = 1000;  // Delay of 1 second
            delayTimer.Tick += DelayTimer_Tick;  // Tick event for the timer

            // Setting up timers
            timer1_main.Interval = 2000;  // Each test phase lasts 2 seconds
            timer1_main.Tick += MainTimer_Tick;
            timer2_Secondary.Interval = 100;  // Data sampling every 100 ms
            timer2_Secondary.Tick += SecondaryTimer_Tick;

            btn_startTst.Click += BtnStartTest_Click;

            // Set the starting indices for rows and columns
            _curIndx_Column_X = 0;
            _curIndx_Row_Y = 1;

            // Ensure the corresponding radio buttons are checked to reflect this start point
            if (_curIndx_Row_Y < RBS_CUR_AD_Rows.Length)
                RBS_CUR_AD_Rows[_curIndx_Row_Y].Checked = true;
            if (_curIndx_Column_X < RBS_CUR_LEVEL_Columns.Length)
                RBS_CUR_LEVEL_Columns[_curIndx_Column_X].Checked = true;
        }
        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            delayTimer.Stop();  // Stop the timer as we only need a one-time delay
            MoveToNextTest();  // Proceed to move to the next test after the delay
        }
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            // Stop collecting data and update UI with results
            timer2_Secondary.Stop();
            _labels2D_minmax[_curIndx_Row_Y, _curIndx_Column_X].Text = $"{minValue}-{maxValue}";
            data_Minmaxes[_curIndx_Row_Y].Set_row_minmax(_curIndx_Column_X, minValue, maxValue);
            floatingColumn[indexFloating].Text = $"f:{_minFloating}-{_maxFloating}";
            data_Minmaxes[indexFloating].Set_row_minmax(3, _minFloating, _maxFloating);
            MoveToNextTest();
        }

        private void SecondaryTimer_Tick(object sender, EventArgs e)
        {
  


          
            // ------------------- this is used to request an ain chanel to be muxed to the labjack DAC0
            DATA_LABJAK.Labjack_EIO2_Write = muxbit0 ? 0 : 1;
            DATA_LABJAK.Labjack_EIO3_Write = muxbit1 ? 0 : 1;
            DATA_LABJAK.Labjack_EIO4_Write = muxbit2 ? 0 : 1;
            DATA_LABJAK.Labjack_EIO5_Write = muxbit3 ? 0 : 1;

            //------------------- this value is 100th of the trakbar value and is used to make the labjack DAC0 output a voltage to the muxed ain channel

            _RAW_DACTOSEND = doubles_powerLavels[_curIndx_Column_X];
            lbl_DAC0.Text = "DAC0: " + _RAW_DACTOSEND.ToString();
            DATA_LABJAK.Labjack_DAC0_Write = _RAW_DACTOSEND;

            //          3. setup the digital io values from the UI 
            _xfer1Val = cb_xfer1CMD.Checked ? 1 : 0;
            _xfer2Val = cb_xfer2CMD.Checked ? 1 : 0;
            _dktr1Val = cb_DK1CMD.Checked ? 1 : 0;
            _dktr2Val = cb_DK2CMD.Checked ? 1 : 0;

            //------------------ this is used to set the digital io values on the labjack and they will pull the digital MB pins high or low depending on the circuit
            DATA_LABJAK.Labjack_FIO0_Write = _xfer1Val;
            DATA_LABJAK.Labjack_FIO1_Write = _xfer2Val;
            DATA_LABJAK.Labjack_FIO2_Write = _dktr1Val;
            DATA_LABJAK.Labjack_FIO3_Write = _dktr2Val;


            // c. write the labjack object to the labjack
            MNGR_LABJAK.Instance.WRITE_to_JACK();



            int simulatedValue = DATA_RX.Get_Stored_AINVal(_curIndx_Row_Y);
            Latest_updated_ADvalues[_curIndx_Row_Y].Text = simulatedValue.ToString();
            minValue = Math.Min(minValue, simulatedValue);
            maxValue = Math.Max(maxValue, simulatedValue);


            int simulated_floatingValue = DATA_RX.Get_Stored_AINVal(indexFloating);
            floatingColumn[indexFloating].Text = simulated_floatingValue.ToString();
            _minFloating = Math.Min(_minFloating, simulated_floatingValue);
            _maxFloating = Math.Max(_maxFloating, simulated_floatingValue);

        }
        private void MoveToNextTestbad()
        {
            Task.Run(() =>
            {
                // Run the delay and test logic in a background thread
                Thread.Sleep(500);  // Delay for 1000 milliseconds (1 second)

                Invoke(new Action(() => {
                    // UI updates and logic must be invoked on the main thread
                    minValue = int.MaxValue;
                    maxValue = int.MinValue;

                    _minFloating = int.MaxValue;
                    _maxFloating = int.MinValue;

                    _curIndx_Column_X += 1;
                    if (_curIndx_Column_X >= RBS_CUR_LEVEL_Columns.Length)
                    {
                        indexFloating += 1;
                        if (indexFloating >= floatingColumn.Length)
                        {
                            indexFloating = 1;
                        }

                        _curIndx_Column_X = 0;
                        _curIndx_Row_Y += 1;
                        if (_curIndx_Row_Y >= RBS_CUR_AD_Rows.Length)
                        {
                            StopTesting();
                            return;
                        }
                    }

                    StartSingleTest();
                }));
            });
        }
        private void MoveToNextTest()
        {
            // Reset logic
            minValue = int.MaxValue;
            maxValue = int.MinValue;

            _minFloating = int.MaxValue;
            _maxFloating = int.MinValue;

            _curIndx_Column_X += 1;
            if (_curIndx_Column_X >= RBS_CUR_LEVEL_Columns.Length)
            {
                indexFloating += 1;
                if (indexFloating >= floatingColumn.Length)
                {
                    indexFloating = 1;
                }

                _curIndx_Column_X = 0;
                _curIndx_Row_Y += 1;
                if (_curIndx_Row_Y >= RBS_CUR_AD_Rows.Length)
                {
                    StopTesting();
                    return;
                }
            }

            StartSingleTest();
        }

        private void StartSingleTest()
        {
            // Check the radio button for the current row first
            RBS_CUR_AD_Rows[_curIndx_Row_Y].Checked = true;
            RBS_CUR_LEVEL_Columns[_curIndx_Column_X].Checked = true;

            // Reset min and max values for the new test
            minValue = int.MaxValue;
            maxValue = int.MinValue;
             
            _minFloating = int.MaxValue;
            _maxFloating = int.MinValue;

            // Start collecting data samples
            timer2_Secondary.Start();
            // Start the test duration timer
            timer1_main.Start();

            UpdateMux_per_channel(_curIndx_Row_Y);
        }

        private void StopTesting()
        {
            timer1_main.Stop();
            timer2_Secondary.Stop();
            btn_startTst.BackColor = SystemColors.Control;
            isTesting = false;


            // Display the min-max values for each row
            if (data_Minmaxes != null) {
                if (data_Minmaxes.Length > 0) {
                    for (int r = 0; r < data_Minmaxes.Length; r++) {
                        data_Minmaxes[r].DOPRINTMYVALUES();
                    }
                
                }
            
            }
        }

        private void BtnStartTest_Click(object sender, EventArgs e)
        {
            if (!isTesting)
            {
                isTesting = true;
                btn_startTst.BackColor = Color.Green;
                _curIndx_Row_Y = 1;
                _curIndx_Column_X = 0;
                StartSingleTest();
            }
            else
            {
                StopTesting();
            }
        }

        int Mock_GET_AD_Value(int rowIndex, int columnIndex)
        {
            return random.Next(0, 100);  // Return a simulated sensor value
        }

        int GET_AD_Value(int __ACTV__CHAN_INDX, int __ACTIV_POWERLEVEL_INDX)
        {
            int _curChan_mostUptodate_RXVal = DATA_RX.Get_Stored_AINVal(__ACTV__CHAN_INDX);
   

       
            return _curChan_mostUptodate_RXVal;
        }
        int Mock_GET_Floating_Value(int rowIndex)
        {
            return random.Next(4000, 4095);  // Return a simulated sensor value
        }

    }
}
