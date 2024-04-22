using G_MBIVautoTester._Globalz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_MBIVautoTester._DataObjects.DataComm
{
    public class data_minmaxRow
    {

        data_minmax _minmax_LOW;
        data_minmax _minmax_MID;
        data_minmax _minmax_HIGH;
        data_minmax _minmax_FLOATING;

        data_minmax[] _myRowMinmaxes= new data_minmax[4];
        public data_minmax Minmax_LOW
        {
            get { return _minmax_LOW; }
           private set { _minmax_LOW = value; }
        }
        public data_minmax Minmax_MID
        {
            get { return _minmax_MID; }
           private  set { _minmax_MID = value; }
        }
        public data_minmax Minmax_HIGH
        {
            get { return _minmax_HIGH; }
            private set { _minmax_HIGH = value; }
        }
        public data_minmax Minmax_FLOATING
        {
            get { return _minmax_FLOATING; }
           private set { _minmax_FLOATING = value; }
        }


        public void Set_row_minmax(int argLEVEL, int argMin, int argMax)
        {
            _myRowMinmaxes[argLEVEL].MinValue = argMin;
            _myRowMinmaxes[argLEVEL].MaxValue = argMax;
        }

        public int Get_row_LEVEL_min( int argLEVEL)
        {
           return _myRowMinmaxes[argLEVEL].MinValue;
        }
        public int Get_row_LEVEL_max(int argLEVEL)
        {
            return _myRowMinmaxes[argLEVEL].MaxValue;
        }
        public data_minmaxRow()
        {
            _minmax_LOW = new data_minmax();
            _minmax_MID = new data_minmax();
            _minmax_HIGH = new data_minmax();
            _minmax_FLOATING = new data_minmax();

            _myRowMinmaxes[0] = _minmax_LOW;
            _myRowMinmaxes[1] = _minmax_MID;
            _myRowMinmaxes[2] = _minmax_HIGH;
            _myRowMinmaxes[3] = _minmax_FLOATING;
        }


        public void DOPRINTMYVALUES() { 
        
             string myString = $"LOW: {_minmax_LOW.MinValue} - {_minmax_LOW.MaxValue} \n" +
            $"MID: {_minmax_MID.MinValue} - {_minmax_MID.MaxValue} \n" +
            $"HIGH: {_minmax_HIGH.MinValue} - {_minmax_HIGH.MaxValue} \n" +
            $"FLOATING: {_minmax_FLOATING.MinValue} - {_minmax_FLOATING.MaxValue} \n";
             
            EventsManagerLib.Call_LogConsole(myString);

        }


    }
}
