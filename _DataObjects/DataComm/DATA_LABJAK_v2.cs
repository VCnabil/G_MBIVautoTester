using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_MBIVautoTester._DataObjects.DataComm
{
    public class DATA_LABJAK_v2
    {
        #region MUXDAC vars
        const int ConstEntries= 5;
        int _num_MUXDAC_enries = 0;
        public int Num_MUXDAC_enries
        {
            get { return _num_MUXDAC_enries; }
            private set { _num_MUXDAC_enries = value; }
        }
        public string[] MUXDAC_names;
        public double[] MUXDAC_values;
        #endregion

        #region AINs vars
        const int ConstAINs = 4;
        int _num_AINs = 0;
        public int Num_AINs
        {
            get { return _num_AINs; }
            private set { _num_AINs = value; }
        }
        public string[] AINs_names;
        public double[] AINs_values;
        #endregion

        #region DIO ShitRegisters Vars
        const int ConstDIOs = 4;
        int _num_DIOs = 0;
        public int Num_DIOs
        {
            get { return _num_DIOs; }
            private set { _num_DIOs = value; }
        }
        public string[] DIOs_names;
        public double[] DIOs_values;
        #endregion

        public DATA_LABJAK_v2() {
            #region MUXDAC init
            _num_MUXDAC_enries = ConstEntries;
            MUXDAC_names = new string[ConstEntries];
            MUXDAC_names[0] = "EIO2";
            MUXDAC_names[1] = "EIO3";
            MUXDAC_names[2] = "EIO4";
            MUXDAC_names[3] = "EIO5";
            MUXDAC_names[4] = "DAC0";
            MUXDAC_values = new double[ConstEntries];
            MUXDAC_values[0] = 0;
            MUXDAC_values[1] = 0;
            MUXDAC_values[2] = 0;
            MUXDAC_values[3] = 0;
            MUXDAC_values[4] = 0;
            #endregion

            #region AINs init
            _num_AINs = ConstAINs;
            AINs_names = new string[ConstAINs];
            AINs_names[0] = "AIN0";
            AINs_names[1] = "AIN1";
            AINs_names[2] = "EIO0";
            AINs_names[3] = "EIO1";
            //AINs_names[4] = "AIN4";
            //AINs_names[5] = "AIN5";
            //AINs_names[6] = "AIN6";
            //AINs_names[7] = "AIN7";
            //AINs_names[8] = "AIN8";
            //AINs_names[9] = "AIN9";
            //AINs_names[10] = "AIN10";
            //AINs_names[11] = "AIN11";
            //AINs_names[12] = "AIN12";
            AINs_values = new double[ConstAINs];
            AINs_values[0] = 0;
            AINs_values[1] = 0;
            AINs_values[2] = 0;
            AINs_values[3] = 0;
            //AINs_values[4] = 0;
            //AINs_values[5] = 0;
            //AINs_values[6] = 0;
            //AINs_values[7] = 0;
            //AINs_values[8] = 0;
            //AINs_values[9] = 0;
            //AINs_values[10] = 0;
            //AINs_values[11] = 0;
            //AINs_values[12] = 0;
            #endregion

            #region DIOs init
            _num_DIOs = ConstDIOs;
            DIOs_names = new string[ConstDIOs];
            DIOs_names[0] = "EIO2";
            DIOs_names[1] = "EIO3";
            DIOs_names[2] = "EIO4";
            DIOs_names[3] = "EIO5";
            DIOs_values = new double[ConstDIOs];
            DIOs_values[0] = 0;
            DIOs_values[1] = 0;
            DIOs_values[2] = 0;
            DIOs_values[3] = 0;
            #endregion
        }
        ~DATA_LABJAK_v2() { }
    }
}
