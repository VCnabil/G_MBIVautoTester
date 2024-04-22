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
        // Initialization of controls and variables
        Label[] Latest_updated_ADvalues;
        RadioButton[] RBS_CUR_LEVEL_Columns;
        Label[,] _labels2D_minmax;
        RadioButton[] RBS_CUR_AD_Rows;
        int _curIndx_Column_X = 0;
        int _curIndx_Row_Y = 0;
        int currentSample = 0;
        int totalSamples = 100;  // Number of samples to take per setting
        int minValue, maxValue;
        bool isTesting = false;
        Random random = new Random();
        public Form_Page3()
        {
            InitializeComponent();
            SetupControls();
        

        }
        private void SetupControls()
        {
            Latest_updated_ADvalues = new Label[] {  LBL_AD1, LBL_AD2, LBL_AD3, LBL_AD4, LBL_AD5 };
         
            RBS_CUR_LEVEL_Columns = new RadioButton[] { rb_0_low, rb_1_mid, rb_2_high };
           
            _labels2D_minmax = new Label[,]
            {
                { label_1_0, label_1_1, label_1_2 },
                { label_2_0, label_2_1, label_2_2 },
                { label_3_0, label_3_1, label_3_2 },
                { label_4_0, label_4_1, label_4_2 },
                { label_5_0, label_5_1, label_5_2 }
            };
            RBS_CUR_AD_Rows = new RadioButton[] { rb0, rb1, rb2, rb3, rb4 };

            rb_Default.Checked = true;

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

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            // Stop collecting data and update UI with results
            timer2_Secondary.Stop();
            _labels2D_minmax[_curIndx_Row_Y, _curIndx_Column_X].Text = $"Min-Max: {minValue}-{maxValue}";
            MoveToNextTest();
        }

        private void SecondaryTimer_Tick(object sender, EventArgs e)
        {
            int simulatedValue = Mock_GET_AD_Value(_curIndx_Row_Y, _curIndx_Column_X);
            Latest_updated_ADvalues[_curIndx_Row_Y].Text = simulatedValue.ToString();
            minValue = Math.Min(minValue, simulatedValue);
            maxValue = Math.Max(maxValue, simulatedValue);
        }

        private void MoveToNextTest()
        {



            _curIndx_Column_X += 1;

            // Check if the current row's columns are exhausted
            if (_curIndx_Column_X >= RBS_CUR_LEVEL_Columns.Length)
            {
                // Reset column index and move to the next row
                _curIndx_Column_X = 0;
                _curIndx_Row_Y += 1;

                // Check if all rows are completed
                if (_curIndx_Row_Y >= RBS_CUR_AD_Rows.Length)
                {
                    // All tests are complete, stop testing
                    StopTesting();
                    return;
                }
            }

            // Start the next test
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
            currentSample = 0;

            // Start collecting data samples
            timer2_Secondary.Start();
            // Start the test duration timer
            timer1_main.Start();
        }

        private void StopTesting()
        {
            timer1_main.Stop();
            timer2_Secondary.Stop();
            btn_startTst.BackColor = SystemColors.Control;
            isTesting = false;
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

    }
}
