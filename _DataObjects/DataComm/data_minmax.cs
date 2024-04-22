using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_MBIVautoTester._DataObjects.DataComm
{
    public class data_minmax
    {
        int _minValue;
        int _maxValue;

        public int MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        public int MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }
        public data_minmax()
        {
            _minValue = 0;
            _maxValue = 0;
        }

    }
}
