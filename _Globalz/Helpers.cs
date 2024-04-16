using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_MBIVautoTester._Globalz
{
    public static class Helpers
    {
        //$VCIA,1.11_Rev5712,4049,4062,4062,4063,4038,4037,4058,4054,4053,4056,4050,4041,4043,4056,4055,4063,511,1,6,26,23,46,32,25,23,63*09
        //$VCIA,1.11_Rev5712,4049,4062,4062,4063,4038,4037,4058,4054,4053,4056,4050,4041,4043,4056,4055,4063,511,1,6,26,23,46,32,25,23,63*09

        //$AAAA,0,12,21,30,42,51,68,1000,2000,1*0D
        //$AAAA,0,100,100,100,100,100,100,0,0,1*01
        //$AAAA,0,100,79,141,141,100,100,0,0,1*3E
        //$AAAA,7,100,79,141,141,100,100,0,0,1*39


        public static bool Vealidate_Checksum(string argMessageBody, string arg_receivedChecksum)
        {
             
            string __calculatedChecksum = Generate_Checksum_fromBody(argMessageBody);
            if (__calculatedChecksum != arg_receivedChecksum)
            {
                return false;
            }
            return true;
        }



        public static string FormatData(int argDio, int argPb, int argPN, int argPI, int argSB, int argSN, int argSI, int argP, int argS, bool argb)
        {
           

            // Ensure values do not exceed their maximum allowed values
            argPb = Math.Min(argPb, 255);
            argPN = Math.Min(argPN, 255);
            argPI = Math.Min(argPI, 255);
            argSB = Math.Min(argSB, 255);
            argSN = Math.Min(argSN, 255);
            argSI = Math.Min(argSI, 255);

            argP = Math.Min(argP, 2000);
            argS = Math.Min(argS, 2000);

            // Convert bool to int for string formatting (assuming 1 for true and 0 for false)
            int boolAsInt = argb ? 1 : 0;

            // Create the formatted string
            string formattedStringBODY = $"$AAAA,{argDio},{argPb},{argPN},{argPI},{argSB},{argSN},{argSI},{argP},{argS},{boolAsInt}";

            string checksum = Generate_Checksum_fromBody(formattedStringBODY);
            string formattedString = $"{formattedStringBODY}*{checksum}";
            return formattedString;
        }
        public static string FormatData_bools(bool b1, bool b2, bool b3, int argPb, int argPN, int argPI, int argSB, int argSN, int argSI, int argP, int argS, bool argb)
        {
            int result = 0;

            // Set bits based on boolean values
            if (b1) result |= 1 << 0;  // Set the first bit if b1 is true
            if (b2) result |= 1 << 1;  // Set the second bit if b2 is true
            if (b3) result |= 1 << 2;  // Set the third bit if b3 is true

            // Ensure values do not exceed their maximum allowed values
            argPb = Math.Min(argPb, 255);
            argPN = Math.Min(argPN, 255);
            argPI = Math.Min(argPI, 255);
            argSB = Math.Min(argSB, 255);
            argSN = Math.Min(argSN, 255);
            argSI = Math.Min(argSI, 255);

            argP = Math.Min(argP, 2000);
            argS = Math.Min(argS, 2000);

            // Convert bool to int for string formatting (assuming 1 for true and 0 for false)
            int boolAsInt = argb ? 1 : 0;

            // Create the formatted string
            string formattedStringBODY = $"$AAAA,{result},{argPb},{argPN},{argPI},{argSB},{argSN},{argSI},{argP},{argS},{boolAsInt}";
           
            string checksum = Generate_Checksum_fromBody(formattedStringBODY);
            string formattedString = $"{formattedStringBODY}*{checksum}";
            return formattedString;
        }
        private static string Generate_Checksum_fromBody(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            short csum = 0;
            byte[] strg;
            int idx = 1;  // Start after the '$' character to compute checksum correctly

            strg = Encoding.ASCII.GetBytes(input);

            //while not end of string only 
            while (idx < strg.Length)
            {
                csum ^= strg[idx];  // XOR each byte with the checksum
                idx++;
            }
            return csum.ToString("X2");  // Convert the checksum to a hex string
        }
    }
}
