using G_MBIVautoTester._DataObjects;
using G_MBIVautoTester._DataObjects.DataTestReport;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace G_MBIVautoTester.UI.Pages
{
    public partial class Form_Page1 : Form
    {

        bool AUTOMAJUK_mode = true;
        bool Started_TheReadTEst=false;
        bool Finished_TheReadTEst = false;
        DATA_RX DATA_RX;
        DATA_TX DATA_TX;
        DATA_LABJAK_v1 DATA_LABJAK;
        DATA_TESTREPORT DATA_testrep;
        double _RAW_DACTOSEND = 0.0;
        private int direction = 100; // Direction of slider movement: 1 for forward, -1 for backward
       // private int __ACTV_CHAN_RADIOINDEX = 0; // Index of currently selected radio button
        private int cycles = 0; // Count of complete back-and-forth cycles
        private int maxCycles = 5; // Total cycles to perform for each radio button

        bool muxbit0 = true;
        bool muxbit1 = true;
        bool muxbit2 = true;
        bool muxbit3 = true;

        double _xfer1Val=1;
        double _xfer2Val=1;
        double _dktr1Val=1;
        double _dktr2Val=1;


        private int sliderMin = int.MaxValue;
        private int sliderMax = int.MinValue;

        private int __ACTV__CHAN_INDX = 1;
        int __ACTIV_POWERLEVEL_INDX = -1;
        int _MAX_POWERLEVEL = 4; //the last state is floating pont , we must do that separatey

        System.Windows.Forms.Label[,] lblsMatrix = new System.Windows.Forms.Label[17, 4];

        int __MAX_Samples_For_minMax = 10;
        int _cur_SampleIndex = 0;



        System.Windows.Forms.RadioButton[] radio_Levels = new System.Windows.Forms.RadioButton[4];
        System.Windows.Forms.Label[] lbls_ACTIVE_and_NonActive_CHAN_VALS = new System.Windows.Forms.Label[17];
       System.Windows.Forms.RadioButton[] radio_channel= new System.Windows.Forms.RadioButton[17];
        private int timeCounter_Linger = 0;
        private int minReading = int.MaxValue;
        private int maxReading = int.MinValue;
        public Form_Page1()
        {
            DATA_RX = new DATA_RX();
            DATA_TX = new DATA_TX();
            DATA_LABJAK = new DATA_LABJAK_v1();
            DATA_testrep = new DATA_TESTREPORT();

            InitializeComponent();


            ConsoleStandaloneForm console = new ConsoleStandaloneForm();
            console.Show();
            
            MNGR_SERIAL.Instance.OpenPortDefault();
            MNGR_SERIAL.Instance.MessageReceived += Instance_MessageReceived;
       

            MNGR_LABJAK.Instance.Init_dataObj(DATA_LABJAK);

            cb_cmdDiO_0_led1.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            cb_cmdDiO_1_led2.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            cb_cmdDiO_2_alarm.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);
            cb_cmd_diosafety_9.CheckedChanged += new EventHandler(a_Dio_cmd_CheckChanged);

       


            lbl_MBSerialStatus.BackColor = Color.Salmon;
            lbl_LabjackStatus.BackColor = Color.Salmon;
            lbl_MBSerialStatus.Text = "COMM OFF";
            lbl_LabjackStatus.Text = "Labjack OFF ";//lolz
            lbl_MBSerialStatus.ForeColor = Color.White;
            lbl_LabjackStatus.ForeColor = Color.White;


            rb_NA.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb1.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb2.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            rb3.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
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
            tkb_DAC0.ValueChanged += new EventHandler(tkb_DAC0_ValueChanged);
            rb_NA.Checked = true;

            button1.Click += new EventHandler(button1_Click);
            button2.Click += new EventHandler(button2_Click);
            button3.Click += new EventHandler(button3_Click);
            button4.Click += new EventHandler(button4_Click);
            btn_startTst.Click += new EventHandler(button5_STARTTEST_Click);
            button6.Click += new EventHandler(button6_Click);

            Init_CurAINvalLabels();
            Init_LabelsMatrix();
            Init_radioButtonsLevelPoints();
            Init_radioChannels_with_br_naFirst();

            TMR_MAIN.Tick += new EventHandler(TMR_MAIN_Tick);
            TMR_MAIN.Interval = 1000;
            TMR_MAIN.Start();

        }

        private void TMR_MAIN_Tick(object sender, EventArgs e)
        {
            //MAIN_LOOP();

            if(Started_TheReadTEst == true)
            {
                testMAIN_LOOP();
            }   
               
        }

        void testMAIN_LOOP() {
            for (int channelIndex = 1; channelIndex <= 17; channelIndex++)
            {
                for (int powerLevelIndex = 0; powerLevelIndex < 3; powerLevelIndex++)
                {
                    testLoop(channelIndex, powerLevelIndex, 100);
                }

            }
        }

        int _cnt_debug = 0;

        void testLoop(int argIndex, int argLevel, int arg_samples)
        {
            for (int i = 0; i < argIndex; i++)
            {
                for (int j = 0; j < argLevel; j++) { 
                    for (int k = 0; k < arg_samples; k++)
                    {
                        __ACTV__CHAN_INDX = argIndex;
                        __ACTIV_POWERLEVEL_INDX = argLevel;

                        // b. update the LABJAK object to send requests 

                        //      0. WE decide WHatCHannel To manipulate
                        SetReadChannel_index_andRadioON_withACTIVCAN();

                        //      1.s et the STATE (low mid highg floating) for the current channel
                        Set_ReadPowLevel_index_radios_and_update_RAW_DACTOSEND();

                        //      2. setup the weird muxbits depending on __ACTV__CHAN_INDX
                        Update_muxbits_by_Reading_active__chan();

                        _cnt_debug++;
                        lbl_debug.Text = _cnt_debug.ToString();

                    }
                }
               
            }


        }   




        private void button5_STARTTEST_Click(object sender, EventArgs e)
        {
            if(Started_TheReadTEst == true)
            {
                statusLabel.Text = "yo already started yo ";
                return;
            }
            Started_TheReadTEst = true;
            statusLabel.Text = "Running... autotest";
             
        }

        void _mustUpdate_ComAndLabjack_Status() {

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
        void _mustUpdate_DigitalValuesRead_fromLAbjack() {

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



        void LoopingTest() { 
            for(int channelindex=1; channelindex < 17; channelindex++)
            {
                __ACTV__CHAN_INDX=channelindex;
                SetReadChannel_index_andRadioON_withACTIVCAN(); 
                for(int powerlevelindex=0; powerlevelindex < 4; powerlevelindex++)
                {
                    Set_ReadPowLevel_index_radios_and_update_RAW_DACTOSEND();
                    Update_muxbits_by_Reading_active__chan();
                    DATA_LABJAK.Labjack_EIO2_Write = muxbit0 ? 0 : 1;
                    DATA_LABJAK.Labjack_EIO3_Write = muxbit1 ? 0 : 1;
                    DATA_LABJAK.Labjack_EIO4_Write = muxbit2 ? 0 : 1;
                    DATA_LABJAK.Labjack_EIO5_Write = muxbit3 ? 0 : 1;
                    DATA_LABJAK.Labjack_DAC0_Write = _RAW_DACTOSEND;
                    MNGR_LABJAK.Instance.WRITE_to_JACK();
                   
                    update_UI_CurAIN_READ(channelindex);
                }
            }
        
        }
        void MAIN_LOOP()
        {
            _mustUpdate_ComAndLabjack_Status();
            //1. write data to Motherboard via SERIAL
            // a. gather digital io ui values and update the TX object 
            DATA_TX.SetDIO(cb_cmdDiO_0_led1.Checked, cb_cmdDiO_1_led2.Checked, cb_cmdDiO_2_alarm.Checked, cb_cmd_diosafety_9.Checked);
            // b. create the full string for TX
            string Full_TX = DATA_TX.CREATE_FullString_for_TX();
            //2. write the full string to the serial port
            MNGR_SERIAL.Instance.WriteData(Full_TX);

            // DATA_RX will be populated by the serial port data received event and we can grab the latest data from it

            //3. SEND to LABJAK
            // a. update the UI with the latest data from the RX object
            lbl_version.Text = DATA_RX.Version;

            cb_DKtr1.Checked = DATA_RX.GP3_Dktr1;
            cb_DKtr2.Checked = DATA_RX.GP4_DKtr2;
            cb_Xfer1.Checked = DATA_RX.GP5_Xfer1;
            cb_Xfer2.Checked = DATA_RX.GP6_Xfer2;


            if (Started_TheReadTEst)
            {
                for (int channelIndex = 1; channelIndex < 17; channelIndex++)
                {
                    for (int powerLevelIndex = 0; powerLevelIndex < 4; powerLevelIndex++)
                    {
                        Loop_(channelIndex, powerLevelIndex);
                    }

                }
            }
            else
            {
                for (int powerLevelIndex = 0; powerLevelIndex < 4; powerLevelIndex++)
                {
                    Loop_(1, powerLevelIndex);
                }
            }



        }
        void Loop_(int arg_ch, int arg_lvl) {

            if (arg_ch < 1) return;


   

        __ACTV__CHAN_INDX = arg_ch;
        __ACTIV_POWERLEVEL_INDX = arg_lvl;



        // b. update the LABJAK object to send requests 

        //      0. WE decide WHatCHannel To manipulate
        SetReadChannel_index_andRadioON_withACTIVCAN();

        //      1.s et the STATE (low mid highg floating) for the current channel
        Set_ReadPowLevel_index_radios_and_update_RAW_DACTOSEND();

        //      2. setup the weird muxbits depending on __ACTV__CHAN_INDX
        Update_muxbits_by_Reading_active__chan();

        // ------------------- this is used to request an ain chanel to be muxed to the labjack DAC0
        DATA_LABJAK.Labjack_EIO2_Write = muxbit0 ? 0 : 1;
        DATA_LABJAK.Labjack_EIO3_Write = muxbit1 ? 0 : 1;
        DATA_LABJAK.Labjack_EIO4_Write = muxbit2 ? 0 : 1;
        DATA_LABJAK.Labjack_EIO5_Write = muxbit3 ? 0 : 1;

        //------------------- this value is 100th of the trakbar value and is used to make the labjack DAC0 output a voltage to the muxed ain channel
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

        //4 get value of active chan from messagread RX
        Update_UI_ACTIVE_CHAN_color_andTEXTfromDATARX();

        int _curChan_mostUptodate_RXVal= DATA_RX.Get_Stored_AINVal(__ACTV__CHAN_INDX);
        if (_curChan_mostUptodate_RXVal < minReading) minReading = _curChan_mostUptodate_RXVal;
        if (_curChan_mostUptodate_RXVal > maxReading) maxReading = _curChan_mostUptodate_RXVal;

            for (int t = 0; t < 8; t++) {

                
                // Check if 8 ticks have passed (80 ms)
                if (t >= 7)
                {
                    // Update the label matrix with min and max values
                    lblsMatrix[__ACTV__CHAN_INDX, __ACTIV_POWERLEVEL_INDX].Text = $"{minReading} - {maxReading}";


                    DATA_testrep.AllMeasurements[__ACTV__CHAN_INDX][__ACTIV_POWERLEVEL_INDX] = (int)minReading;
                    DATA_testrep.AllMeasurements[__ACTV__CHAN_INDX][__ACTIV_POWERLEVEL_INDX + 1] = (int)maxReading;
                    // Reset readings and tick counter
                    minReading = int.MaxValue;
                    maxReading = int.MinValue;
                    tickCount = 0;


                }
            }

    


            // 5. GET from LABJAK . doesnt need to be in a looping sample
            MNGR_LABJAK.Instance.READ_from_JAck();

        //update the UI with the latest data from the labjack object
        _mustUpdate_DigitalValuesRead_fromLAbjack();



 
     }

        void update_UI_CurAIN_READ(int arg_chindex) { 
        if(arg_chindex <1) return; //the first index is just for BASE stuff and default values . the physical cjannels start at 1 anyway

            
            switch (arg_chindex)
            { 
            case    1:
                LBL_AD1.Text = "AD..01:" + DATA_RX.AIN1.ToString();
                break;
            
            
            }
        
        }

        void Update_UI_ACTIVE_CHAN_color_andTEXTfromDATARX() { 
        if(__ACTV__CHAN_INDX <1) return; //the first index is just for BASE stuff and default values . the physical cjannels start at 1 anyway

            for(int i = 1; i < 17; i++)
            {
                if(i == __ACTV__CHAN_INDX)
                {
                    lbls_ACTIVE_and_NonActive_CHAN_VALS[i].BackColor = Color.SeaGreen;
                    lbls_ACTIVE_and_NonActive_CHAN_VALS[i].Text = DATA_RX.Get_Stored_AINVal(i).ToString();

                }
                else
                {
                    lbls_ACTIVE_and_NonActive_CHAN_VALS[i].BackColor = Color.Salmon;
                }
            }
        
        }
        private void SERIAL_TIMER_Tick(object sender, EventArgs e)
        {


            //label1.Text = MNGR_SERIAL.Instance.GetLatest_Valide_MessageBody();
            string Full_TX = DATA_TX.CREATE_FullString_for_TX();
            lbl_TX.Text = Full_TX;
            MNGR_SERIAL.Instance.WriteData(Full_TX);

            //lbl_RX.Text = MNGR_SERIAL.Instance.GetLatest_Valide_MessageBody();
            lbl_version.Text = DATA_RX.Version;
            LBL_AD1.Text = "AD01:" + DATA_RX.AIN1.ToString();
            LBL_AD2.Text = "AD02:" + DATA_RX.AIN2.ToString();
            LBL_AD3.Text = "AD03:" + DATA_RX.AIN3.ToString();
            LBL_AD4.Text = "AD04:" + DATA_RX.AIN4.ToString();
            LBL_AD5.Text = "AD05:" + DATA_RX.AIN5.ToString();
            LBL_AD6.Text = "AD06:" + DATA_RX.AIN6.ToString();
            LBL_AD7.Text = "AD07:" + DATA_RX.AIN7.ToString();
            LBL_AD8.Text = "AD08:" + DATA_RX.AIN8.ToString();
            lbl_AD9.Text = "AD09:" + DATA_RX.AIN9.ToString();
            lbl_AD10.Text = "AD10:" + DATA_RX.AIN10.ToString();
            lbl_AD11.Text = "AD11:" + DATA_RX.AIN11.ToString();
            lbl_AD12.Text = "AD12:" + DATA_RX.AIN12.ToString();
            lbl_AD13.Text = "AD13:" + DATA_RX.AIN13.ToString();
            lbl_AD14.Text = "AD14:" + DATA_RX.AIN14.ToString();
            lbl_AD15.Text = "AD15:" + DATA_RX.AIN15.ToString();
            lbl_AD16.Text = "AD16:" + DATA_RX.AIN16.ToString();
            cb_DKtr1.Checked = DATA_RX.GP3_Dktr1;
            cb_DKtr2.Checked = DATA_RX.GP4_DKtr2;
            cb_Xfer1.Checked = DATA_RX.GP5_Xfer1;
            cb_Xfer2.Checked = DATA_RX.GP6_Xfer2;

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

            //   MNGR_LABJAK.Instance.Update_dataObj_withAINdata();
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


        private void Timer2_Tick(object sender, EventArgs e)
        {

            if (!Started_TheReadTEst)
            {
                return;  // No need to execute anything if the test hasn't started
            }

            if (Finished_TheReadTEst)
            {
               // timer2_autoReadLinger.Stop();  // Stop the timer as the test is finished
                statusLabel.Text = "Test completed";
                return;
            }

            //Measure2();
        }
        private int tickCount = 0;
        void Messure2() {
            if(Started_TheReadTEst == false)
            {
                return;
            }
            if (Finished_TheReadTEst == true)
            {
                statusLabel.Text = "yo i m done... finished";
                return;
            }
            // Get current channel and measurement point readings
          int currentReading = DATA_RX.Get_Stored_AINVal(__ACTV__CHAN_INDX);

            // Update min and max readings
            if (currentReading < minReading) minReading = currentReading;
            if (currentReading > maxReading) maxReading = currentReading;

            // Update the tick counter
            tickCount++;

            // Check if 8 ticks have passed (80 ms)
            if (tickCount >= 8)
            {
                // Update the label matrix with min and max values
                lblsMatrix[__ACTV__CHAN_INDX, __ACTIV_POWERLEVEL_INDX].Text = $"{minReading} - {maxReading}";
               

                DATA_testrep.AllMeasurements[__ACTV__CHAN_INDX][__ACTIV_POWERLEVEL_INDX] = (int)minReading;
                DATA_testrep.AllMeasurements[__ACTV__CHAN_INDX][__ACTIV_POWERLEVEL_INDX + 1] = (int)maxReading;
                // Reset readings and tick counter
                minReading = int.MaxValue;
                maxReading = int.MinValue;
                tickCount = 0;

                // Move to the next measurement point or channel
                if (__ACTIV_POWERLEVEL_INDX < radio_Levels.Length - 2)
                {
                    __ACTIV_POWERLEVEL_INDX++;
                }
                else
                {
                    __ACTIV_POWERLEVEL_INDX = 0;
                    __ACTV__CHAN_INDX = (__ACTV__CHAN_INDX + 1) % radio_channel.Length;
                }

                // Update UI for new channel and measurement point
                radio_Levels[__ACTIV_POWERLEVEL_INDX].Checked = true;
                radio_channel[__ACTV__CHAN_INDX].Checked = true;
            }

            statusLabel.Text = "Running... finished";
            Finished_TheReadTEst = true;
            Started_TheReadTEst = false;


            if(Finished_TheReadTEst == true)
            {

                StringBuilder SB = new StringBuilder();
                string RowData = "ain x :  0-50  2010-290  4050-4095  4085-4090";

                for(int i = 0; i < DATA_testrep.AllMeasurements.Length; i++)
                {
                    string row = "";
                    for(int j = 0; j < DATA_testrep.AllMeasurements[i].Length; j++)
                    {
                        row += DATA_testrep.AllMeasurements[i][j].ToString() + " ";
                    }
                    RowData += row;
                    SB.AppendLine(RowData);
                }

                EventsManagerLib.Call_LogConsole(SB.ToString());
            }
        }
        void MEarures_01() {
            SetReadChannel_index_andRadioON(__ACTV__CHAN_INDX); // Set the current channel to read from
            // Get current channel and measurement point readings
            int currentReading =(int) chReading(__ACTV__CHAN_INDX);

            // Update min and max readings
            if (currentReading < minReading) minReading = currentReading;
            if (currentReading > maxReading) maxReading = currentReading;

            // Update the timer counter
           // timeCounter_Linger += timer2_autoReadLinger.Interval;

            // Check if 1 second has passed
            if (timeCounter_Linger >= 1000)
            {
                // Update the label matrix with min and max values
                lblsMatrix[__ACTV__CHAN_INDX, __ACTIV_POWERLEVEL_INDX].Text = $"{minReading} - {maxReading}";

                // Reset readings and counter
                minReading = int.MaxValue;
                maxReading = int.MinValue;
                timeCounter_Linger = 0;

                // Move to the next measurement point or channel
                if (__ACTIV_POWERLEVEL_INDX < radio_Levels.Length - 2)
                {
                    __ACTIV_POWERLEVEL_INDX++;
                }
                else
                {
                    __ACTIV_POWERLEVEL_INDX = 0;
                    __ACTV__CHAN_INDX = (__ACTV__CHAN_INDX + 1) % radio_channel.Length;
                }

                // Update UI for new channel and measurement point
                radio_Levels[__ACTIV_POWERLEVEL_INDX].Checked = true;
                radio_channel[__ACTV__CHAN_INDX].Checked = true;

                Update_tkbr_value_with_measurepointIndex(__ACTIV_POWERLEVEL_INDX);
            }
        }

        private double chReading(int cur_auto_channelIndex)
        {
            if(DATA_RX == null ) return 0.0;

            switch(cur_auto_channelIndex)
            {
                case 0:
                    return -1.1;
                case 1:
                    return DATA_RX.AIN1;
                case 2:
                    return DATA_RX.AIN2;
                case 3:
                    return DATA_RX.AIN3;
                case 4:
                    return DATA_RX.AIN4;
                case 5:
                    return DATA_RX.AIN5;
                case 6:
                    return DATA_RX.AIN6;
                case 7:
                    return DATA_RX.AIN7;
                case 8:
                    return DATA_RX.AIN8;
                case 9:
                    return DATA_RX.AIN9;
                case 10:
                    return DATA_RX.AIN10;
                case 11:
                    return DATA_RX.AIN11;
                case 12:
                    return DATA_RX.AIN12;
                case 13:
                    return DATA_RX.AIN13;
                case 14:
                    return DATA_RX.AIN14;
                case 15:
                    return DATA_RX.AIN15;
                case 16:
                    return DATA_RX.AIN16;
                default:
                    return 0.0;
            }
        }

        void Init_CurAINvalLabels() {

            lbls_ACTIVE_and_NonActive_CHAN_VALS[0] =LBL_AD0;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[1] = LBL_AD1;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[2] = LBL_AD2;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[3] = LBL_AD3;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[4] = LBL_AD4;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[5] = LBL_AD5;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[6] = LBL_AD6;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[7] = LBL_AD7;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[8] = LBL_AD8;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[9] = lbl_AD9;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[10] = lbl_AD10;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[11] = lbl_AD11;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[12] = lbl_AD12;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[13] = lbl_AD13;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[14] = lbl_AD14;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[15] = lbl_AD15;
            lbls_ACTIVE_and_NonActive_CHAN_VALS[16] = lbl_AD16;
                    
        }

        void Init_LabelsMatrix() {

            lblsMatrix[0, 0] = label_t_0_float;
            lblsMatrix[0, 1] = label_t_1_low;
            lblsMatrix[0, 2] = label_t_2_mid;
            lblsMatrix[0, 3] = label_t_3_high;
            lblsMatrix[1, 0] = label_1_0;
            lblsMatrix[1, 1] = label_1_1;
            lblsMatrix[1, 2] = label_1_2;
            lblsMatrix[1, 3] = label_1_3;
            lblsMatrix[2, 0] = label_2_0;
            lblsMatrix[2, 1] = label_2_1;
            lblsMatrix[2, 2] = label_2_2;
            lblsMatrix[2, 3] = label_2_3;
            lblsMatrix[3, 0] = label_3_0;
            lblsMatrix[3, 1] = label_3_1;
            lblsMatrix[3, 2] = label_3_2;
            lblsMatrix[3, 3] = label_3_3;
            lblsMatrix[4, 0] = label_4_0;
            lblsMatrix[4, 1] = label_4_1;
            lblsMatrix[4, 2] = label_4_2;
            lblsMatrix[4, 3] = label_4_3;
            lblsMatrix[5, 0] = label_5_0;
            lblsMatrix[5, 1] = label_5_1;
            lblsMatrix[5, 2] = label_5_2;
            lblsMatrix[5, 3] = label_5_3;
            lblsMatrix[6, 0] = label_6_0;
            lblsMatrix[6, 1] = label_6_1;
            lblsMatrix[6, 2] = label_6_2;
            lblsMatrix[6, 3] = label_6_3;
            lblsMatrix[7, 0] = label_7_0;
            lblsMatrix[7, 1] = label_7_1;
            lblsMatrix[7, 2] = label_7_2;
            lblsMatrix[7, 3] = label_7_3;
            lblsMatrix[8, 0] = label_8_0;
            lblsMatrix[8, 1] = label_8_1;
            lblsMatrix[8, 2] = label_8_2;
            lblsMatrix[8, 3] = label_8_3;
            lblsMatrix[9, 0] = label_9_0;
            lblsMatrix[9, 1] = label_9_1;
            lblsMatrix[9, 2] = label_9_2;
            lblsMatrix[9, 3] = label_9_3;
            lblsMatrix[10, 0] = label_10_0;
            lblsMatrix[10, 1] = label_10_1;
            lblsMatrix[10, 2] = label_10_2;
            lblsMatrix[10, 3] = label_10_3;
            lblsMatrix[11, 0] = label_11_0;
            lblsMatrix[11, 1] = label_11_1;
            lblsMatrix[11, 2] = label_11_2;
            lblsMatrix[11, 3] = label_11_3;
            lblsMatrix[12, 0] = label_12_0;
            lblsMatrix[12, 1] = label_12_1;
            lblsMatrix[12, 2] = label_12_2;
            lblsMatrix[12, 3] = label_12_3;
            lblsMatrix[13, 0] = label_13_0;
            lblsMatrix[13, 1] = label_13_1;
            lblsMatrix[13, 2] = label_13_2;
            lblsMatrix[13, 3] = label_13_3;
            lblsMatrix[14, 0] = label_14_0;
            lblsMatrix[14, 1] = label_14_1;
            lblsMatrix[14, 2] = label_14_2;
            lblsMatrix[14, 3] = label_14_3;
            lblsMatrix[15, 0] = label_15_0;
            lblsMatrix[15, 1] = label_15_1;
            lblsMatrix[15, 2] = label_15_2;
            lblsMatrix[15, 3] = label_15_3;
            lblsMatrix[16, 0] = label_16_0;
            lblsMatrix[16, 1] = label_16_1;
            lblsMatrix[16, 2] = label_16_2;
            lblsMatrix[16, 3] = label_16_3;

                




        }

        void Init_radioButtonsLevelPoints()
        {
            radio_Levels[0] = rb_0_low;
            radio_Levels[1] = rb_1_mid;
            radio_Levels[2] = rb_2_high;
            radio_Levels[3] = rb_3_float;

        }

        void Init_radioChannels_with_br_naFirst()
        {
            radio_channel[0] = rb_NA;
            radio_channel[1] = rb1;
            radio_channel[2] = rb2;
            radio_channel[3] = rb3;
            radio_channel[4] = rb4;
            radio_channel[5] = rb5;
            radio_channel[6] = rb6;
            radio_channel[7] = rb7;
            radio_channel[8] = rb8;
            radio_channel[9] = rb9;
            radio_channel[10] = rb10;
            radio_channel[11] = rb11;
            radio_channel[12] = rb12;
            radio_channel[13] = rb13;
            radio_channel[14] = rb14;
            radio_channel[15] = rb15;
            radio_channel[16] = rb16;   
        }

        void SetReadChannel_index_andRadioON_withACTIVCAN() {
            if (__ACTV__CHAN_INDX < 0 || __ACTV__CHAN_INDX >= radio_channel.Length) return;
            radio_channel[__ACTV__CHAN_INDX].Checked = true;
        }


        void Set_ReadPowLevel_index_radios_and_update_RAW_DACTOSEND() { 
            
            if(__ACTIV_POWERLEVEL_INDX < 0 || __ACTIV_POWERLEVEL_INDX > radio_Levels.Length - 1) return;
            radio_Levels[__ACTIV_POWERLEVEL_INDX].Checked = true;
            int Lowestval = 0;
            int Midval = 250;
            int Highval = 500;


            switch (__ACTIV_POWERLEVEL_INDX)
            {
                case 0:
                    tkb_DAC0.Value = Lowestval;
                    break;
                case 1:
                    tkb_DAC0.Value = Midval;
                    break;
                case 2:
                    tkb_DAC0.Value = Highval;
                    break;
                case 3:
                    tkb_DAC0.Value = 0;
                    break;
                default:
                    break;
            }

            _RAW_DACTOSEND = (double)tkb_DAC0.Value / 100;
            lbl_DAC0.Text = "rawDAC0: " + _RAW_DACTOSEND.ToString();

        }



        void SetState_LEVEL_for_currentChannel_Udate_RAWDAC(int arg_state)
        {
            if (arg_state < 0 || arg_state >= radio_Levels.Length - 1) return;
            radio_Levels[arg_state].Checked = true;


            int Lowestval = 0;
            int Midval = 250;
            int Highval = 500;
 

            switch (arg_state)
            {
                case 0:
                    tkb_DAC0.Value = Lowestval;
                    break;
                case 1:
                    tkb_DAC0.Value = Midval;
                    break;
                case 2:
                    tkb_DAC0.Value = Highval;
                    break;
                case 3:
                    tkb_DAC0.Value = 0;
                    break;
                default:
                    break;
            }
           
            _RAW_DACTOSEND = (double)tkb_DAC0.Value / 100;
            lbl_DAC0.Text = "rawDAC0: " + _RAW_DACTOSEND.ToString();
          

        }

        void Update_tkbr_value_with_measurepointIndex(int arg_measurepointIndex)
        {
            if (arg_measurepointIndex < 0 || arg_measurepointIndex >= radio_Levels.Length-1) return;

            int Lowestval = 0;
            int Midval = 250;
            int Highval = 500;

            switch (arg_measurepointIndex)
            {
                case 0:
                    tkb_DAC0.Value = Lowestval;
                    break;
                case 1:
                    tkb_DAC0.Value = Midval;
                    break;
                case 2:
                    tkb_DAC0.Value = Highval;
                    break;
                case 3:
                    tkb_DAC0.Value = 0;
                    break;
                default:
                    break;
            }
        }

        void SetReadChannel_index_andRadioON(int arg_channelIndex)
        {
            if (arg_channelIndex < 0 || arg_channelIndex >= radio_channel.Length) return;
            radio_channel[arg_channelIndex].Checked = true;
        }   
        void Update_muxbits_by_Reading_active__chan() {

            switch (__ACTV__CHAN_INDX)
            {
                case 0:
                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
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
                    break;
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
          
        }
        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
             
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void tkb_DAC0_ValueChanged(object sender, EventArgs e)
        {

            
            double val = tkb_DAC0.Value;
            double Converted = val / 100;
            lbl_DAC0.Text = "DAC0: " + Converted.ToString();
            DATA_LABJAK.Labjack_DAC0_Write = Converted;

        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rb15.Checked)
            {
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
            if (rb3.Checked)
            {

                muxbit0 = true;
                muxbit1 = false;
                muxbit2 = true;
                muxbit3 = true;
            }

            if (rb2.Checked)
            {
                muxbit0 = false;
                muxbit1 = true;
                muxbit2 = true;
                muxbit3 = true;
            }

            if (rb1.Checked)
            {

                muxbit0 = true;
                muxbit1 = true;
                muxbit2 = true;
                muxbit3 = true;
            }
            if(rb_NA.Checked)
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

        private void a_Dio_cmd_CheckChanged(object sender, EventArgs e)
        {
            DATA_TX.SetDIO(cb_cmdDiO_0_led1.Checked, cb_cmdDiO_1_led2.Checked, cb_cmdDiO_2_alarm.Checked, cb_cmd_diosafety_9.Checked);
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

    }
}
