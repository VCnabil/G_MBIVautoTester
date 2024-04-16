using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_MBIVautoTester._DataObjects
{
    public class DATA_RX
    {
        string _version;
        int _ain1;
        int _ain2;
        int _ain3;
        int _ain4;
        int _ain5;
        int _ain6;
        int _ain7;
        int _ain8;
        int _ain9;
        int _ain10;
        int _ain11;
        int _ain12;
        int _ain13;
        int _ain14;
        int _ain15;
        int _ain16;

        bool _gp0;
        bool _gp1;
        bool _gp2;
        bool _gp3;
        bool _gp4;
        bool _gp5;
        bool _gp6;
        bool _gp7;
        bool _apdi;

        bool _mbiv_Serial_Fault;
        string _apInput;

        int _pnoz_FDBK;
        int _snoz_FDBK;
        int _pint_FDBK;
        int _sbuck_FDBK;
        int _pbuck_FDBK;
        int _sint_FDBK;

        bool _pb_fault;
        bool _si_fault;
        bool _pi_fault;
        bool _sb_fault;
        bool _pn_fault;
        bool _sn_fault;


        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        public int AIN1
        {
            get { return _ain1; }
            set {

                if (value < 0)
                {
                    _ain1 = 0;
                }
                else if (value > 4095)
                {
                    _ain1 = 4095;
                }
                else
                {
                    _ain1 = value;
                }

            }
        }
        public int AIN2
        {
            get { return _ain2; }
            set
            {

                if (value < 0)
                {
                    _ain2 = 0;
                }
                else if (value > 4095)
                {
                    _ain2 = 4095;
                }
                else
                {
                    _ain2 = value;
                }

            }
        }
        public int AIN3
        {
            get { return _ain3; }
            set
            {

                if (value < 0)
                {
                    _ain3 = 0;
                }
                else if (value > 4095)
                {
                    _ain3 = 4095;
                }
                else
                {
                    _ain3 = value;
                }

            }
        }

        public int AIN4
        {
            get { return _ain4; }
            set
            {

                if (value < 0)
                {
                    _ain4 = 0;
                }
                else if (value > 4095)
                {
                    _ain4 = 4095;
                }
                else
                {
                    _ain4 = value;
                }

            }
        }
        public int AIN5
        {
            get { return _ain5; }
            set
            {

                if (value < 0)
                {
                    _ain5 = 0;
                }
                else if (value > 4095)
                {
                    _ain5 = 4095;
                }
                else
                {
                    _ain5 = value;
                }

            }
        }
        public int AIN6
        {
            get { return _ain6; }
            set
            {

                if (value < 0)
                {
                    _ain6 = 0;
                }
                else if (value > 4095)
                {
                    _ain6 = 4095;
                }
                else
                {
                    _ain6 = value;
                }

            }
        }
        public int AIN7
        {
            get { return _ain7; }
            set
            {

                if (value < 0)
                {
                    _ain7 = 0;
                }
                else if (value > 4095)
                {
                    _ain7 = 4095;
                }
                else
                {
                    _ain7 = value;
                }

            }
        }
        public int AIN8
        {
            get { return _ain8; }
            set
            {

                if (value < 0)
                {
                    _ain8 = 0;
                }
                else if (value > 4095)
                {
                    _ain8 = 4095;
                }
                else
                {
                    _ain8 = value;
                }

            }
        }
        public int AIN9
        {
            get { return _ain9; }
            set
            {

                if (value < 0)
                {
                    _ain9 = 0;
                }
                else if (value > 4095)
                {
                    _ain9 = 4095;
                }
                else
                {
                    _ain9 = value;
                }

            }
        }
        public int AIN10
        {
            get { return _ain10; }
            set
            {

                if (value < 0)
                {
                    _ain10 = 0;
                }
                else if (value > 4095)
                {
                    _ain10 = 4095;
                }
                else
                {
                    _ain10 = value;
                }

            }
        }
        public int AIN11
        {
            get { return _ain11; }
            set
            {

                if (value < 0)
                {
                    _ain11 = 0;
                }
                else if (value > 4095)
                {
                    _ain11 = 4095;
                }
                else
                {
                    _ain11 = value;
                }

            }
        }
        public int AIN12
        {
            get { return _ain12; }
            set
            {

                if (value < 0)
                {
                    _ain12 = 0;
                }
                else if (value > 4095)
                {
                    _ain12 = 4095;
                }
                else
                {
                    _ain12 = value;
                }

            }
        }
        public int AIN13
        {
            get { return _ain13; }
            set
            {

                if (value < 0)
                {
                    _ain13 = 0;
                }
                else if (value > 4095)
                {
                    _ain13 = 4095;
                }
                else
                {
                    _ain13 = value;
                }

            }
        }
        public int AIN14
        {
            get { return _ain14; }
            set
            {

                if (value < 0)
                {
                    _ain14 = 0;
                }
                else if (value > 4095)
                {
                    _ain14 = 4095;
                }
                else
                {
                    _ain14 = value;
                }

            }
        }
        public int AIN15
        {
            get { return _ain15; }
            set
            {

                if (value < 0)
                {
                    _ain15 = 0;
                }
                else if (value > 4095)
                {
                    _ain15 = 4095;
                }
                else
                {
                    _ain15 = value;
                }

            }
        }
        public int AIN16
        {
            get { return _ain16; }
            set
            {

                if (value < 0)
                {
                    _ain16 = 0;
                }
                else if (value > 4095)
                {
                    _ain16 = 4095;
                }
                else
                {
                    _ain16 = value;
                }

            }
        }

        public bool GP0
        {
            get { return _gp0; }
           private set { _gp0 = value; }
        }
        public bool GP1
        {
            get { return _gp1; }
            private set { _gp1 = value; }
        }

        public bool GP2
        {
            get { return _gp2; }
            private set { _gp2 = value; }
        }
        public bool GP3
        {
            get { return _gp3; }
            private set { _gp3 = value; }
        }
        public bool GP4
        {
            get { return _gp4; }
            private set { _gp4 = value; }
        }
        public bool GP5
        {
            get { return _gp5; }
            private set { _gp5 = value; }
        }
        public bool GP6
        {
            get { return _gp6; }
            private set { _gp6 = value; }
        }
        public bool GP7
        {
            get { return _gp7; }
            private set { _gp7 = value; }
        }
        public bool APDI
        {
            get { return _apdi; }
            private set { _apdi = value; }
        }

        public void SetGP_bools_18(int arg_18) {
        
            _gp0 = (arg_18 & 0x01) == 0x01;
            _gp1 = (arg_18 & 0x02) == 0x02;
            _gp2 = (arg_18 & 0x04) == 0x04;
            _gp3 = (arg_18 & 0x08) == 0x08;
            _gp4 = (arg_18 & 0x10) == 0x10;
            _gp5 = (arg_18 & 0x20) == 0x20;
            _gp6 = (arg_18 & 0x40) == 0x40;
            _gp7 = (arg_18 & 0x80) == 0x80;
            _apdi = (arg_18 & 0x100) == 0x100;
        }

        public bool MBIV_Serial_Fault_19
        {
            get { return _mbiv_Serial_Fault; }
            set { _mbiv_Serial_Fault = value; }
        }
        public string APInput_20
        {
            get { return _apInput; }
            set { _apInput = value; }
        }

        public int PNOZ_FDBK_21
        {       
            get { return _pnoz_FDBK; }
                   set
            {
                if (value < 0)
                {
                    _pnoz_FDBK = 0;
                }
                else if (value > 255)
                {
                    _pnoz_FDBK = 255;
                }
                else
                {
                    _pnoz_FDBK = value;
                }
            }
        }
       
        public int SNOZ_FDBK_22
        {
            get { return _snoz_FDBK; }
            set
            {
                if (value < 0)
                {
                    _snoz_FDBK = 0;
                }
                else if (value > 255)
                {
                    _snoz_FDBK = 255;
                }
                else
                {
                    _snoz_FDBK = value;
                }
            }
        }

        public int PINT_FDBK_23
        {
            get { return _pint_FDBK; }
            set
            {
                if (value < 0)
                {
                    _pint_FDBK = 0;
                }
                else if (value > 255)
                {
                    _pint_FDBK = 255;
                }
                else
                {
                    _pint_FDBK = value;
                }
            }
        }

        public int SBKT_FDBK_24
        {
            get { return _sbuck_FDBK; }
            set
            {
                if (value < 0)
                {
                    _sbuck_FDBK = 0;
                }
                else if (value > 255)
                {
                    _sbuck_FDBK = 255;
                }
                else
                {
                    _sbuck_FDBK = value;
                }
            }
        }
        public int PBKT_FDBK_25
        {
            get { return _pbuck_FDBK; }
            set
            {
                if (value < 0)
                {
                    _pbuck_FDBK = 0;
                }
                else if (value > 255)
                {
                    _pbuck_FDBK = 255;
                }
                else
                {
                    _pbuck_FDBK = value;
                }
            }
        }
        public int SINT_FDBK_26
        {
            get { return _sint_FDBK; }
            set
            {
                if (value < 0)
                {
                    _sint_FDBK = 0;
                }
                else if (value > 255)
                {
                    _sint_FDBK = 255;
                }
                else
                {
                    _sint_FDBK = value;
                }
            }
        }

        public bool PB_Fault
        {
            get { return _pb_fault; }
           private set { _pb_fault = value; }
        }
        public bool SI_Fault
        {
            get { return _si_fault; }
            private set { _si_fault = value; }
        }
        public bool PI_Fault
        {
            get { return _pi_fault; }
            private set { _pi_fault = value; }
        }
        public bool SB_Fault
        {
            get { return _sb_fault; }
            private set { _sb_fault = value; }
        }
        public bool PN_Fault
        {
            get { return _pn_fault; }
            private set { _pn_fault = value; }
        }
        public bool SN_Fault
        {
            get { return _sn_fault; }
            private set { _sn_fault = value; }
        }
        public void Set_boolFaults_27(int arg27) {

            _pb_fault = (arg27 & 0x01) == 0x01;
            _si_fault = (arg27 & 0x02) == 0x02;
            _pi_fault = (arg27 & 0x04) == 0x04;
            _sb_fault = (arg27 & 0x08) == 0x08;
            _pn_fault = (arg27 & 0x10) == 0x10;
            _sn_fault = (arg27 & 0x20) == 0x20;
        }

        public DATA_RX()
        {
 
        }
        ~DATA_RX()
        {
 
        }

    }
}
