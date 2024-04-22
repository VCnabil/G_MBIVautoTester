using G_MBIVautoTester._DataObjects.DataComm;
using G_MBIVautoTester.UI.Forms;
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
        Label[] floatingColumn;
        int indexFloating = 16;
        int _minFloating , _maxFloating;

        data_minmaxRow[] data_Minmaxes;

        public Form_Page2()
        {
            InitializeComponent();
            ConsoleStandaloneForm console = new ConsoleStandaloneForm();
            console.Show();
            SetupControls();
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
            data_Minmaxes[_curIndx_Row_Y].Set_row_minmax(_curIndx_Column_X, minValue, maxValue);
            floatingColumn[indexFloating].Text = $"Min-Max: {_minFloating}-{_maxFloating}";
            data_Minmaxes[indexFloating].Set_row_minmax(3, _minFloating, _maxFloating);
            MoveToNextTest();
        }

        private void SecondaryTimer_Tick(object sender, EventArgs e)
        {
            int simulatedValue = Mock_GET_AD_Value(_curIndx_Row_Y, _curIndx_Column_X);
            Latest_updated_ADvalues[_curIndx_Row_Y].Text = simulatedValue.ToString();
            minValue = Math.Min(minValue, simulatedValue);
            maxValue = Math.Max(maxValue, simulatedValue);


            int simulated_floatingValue = Mock_GET_Floating_Value(indexFloating);
            floatingColumn[indexFloating].Text = simulated_floatingValue.ToString();
            _minFloating = Math.Min(_minFloating, simulated_floatingValue);
            _maxFloating = Math.Max(_maxFloating, simulated_floatingValue);
        }

        private void MoveToNextTest()
        {

            _curIndx_Column_X += 1;
            // Check if the current row's columns are exhausted
            if (_curIndx_Column_X >= RBS_CUR_LEVEL_Columns.Length)
            {

                indexFloating += 1;
                if (indexFloating >= floatingColumn.Length)
                {
                    indexFloating = 1;
                }


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
             
            _minFloating = int.MaxValue;
            _maxFloating = int.MinValue;

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
        int Mock_GET_Floating_Value(int rowIndex)
        {
            return random.Next(4000, 4095);  // Return a simulated sensor value
        }

    }
}
