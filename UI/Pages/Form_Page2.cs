using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G_MBIVautoTester.UI.Pages
{
    public partial class Form_Page2 : Form
    {
        Label[] Latest_updated_ADvalues;
        RadioButton[] RBS_CUR_LEVEL_Columns;
        Label[,] _labels2D_minmax;
        RadioButton[] RBS_CUR_AD_Rows;
        int _curIndx_Column_X = 0;
        int _curIndx_Row_Y = 0;
        int minValue, maxValue;
        bool isTesting = false;
        Random random = new Random();


        public Form_Page2()
        {
            InitializeComponent();
            SetupControls();
        }

        private void SetupControls()
        {
            Latest_updated_ADvalues = new Label[] { LBL_AD0, LBL_AD1, LBL_AD2, LBL_AD3, LBL_AD4, LBL_AD5 , LBL_AD6, LBL_AD7, LBL_AD8, LBL_AD9, lbl_AD10, lbl_AD11, lbl_AD12, lbl_AD13, lbl_AD14, lbl_AD15, lbl_AD16 };

            RBS_CUR_LEVEL_Columns = new RadioButton[] { rb_0_low, rb_1_mid, rb_2_high };

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

          //  rb_Default.Checked = true;

            // Setting up timers
            timer1_main.Interval = 500;  // Each test phase lasts 2 seconds
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
