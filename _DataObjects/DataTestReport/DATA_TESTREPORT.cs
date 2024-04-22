/*
 
 

MBIV Kiyoon Box Test Report P5
DocID: T-Q-TN-KIYOONBOX_MBIV-020817-P5

Project:

Assembly PN:

Assembly SN:

PCB Revision / PN:

PCB SN:

XMega SW Version:
MBIV_Engine_Controller_v1-Rev5491.hex
MBIV Control Unit Kiyoon SW Version:
MBIV-1_11_Rev5712.bin
MBIV Kiyoon Test GUI SW Version:
MBIV_testbed_v2_20210101.exe
Multimeter #1 Model:

Multimeter #1 SN:

Multimeter #2 Model:

Multimeter #2 SN:



New
Field Return

Condition




Fit for Installation
Can be Repaired
Can NOT be Repaired
Diagnosis









First Testers Name

Signature

Date





Second Testers Name

Signature

Date

Revision Notes
Rev.
Date
Change
Author
Approved
P1
11/12/2017
Initial release
NCA

P2
07/10/2020
Updated engine output test
TMQ

P3
12/28/2020
Updated test jumper configurations  
WEL

P4
10/07/2021
Updated Software and Test GUI versions
EJF

P5
12/05/2022
Overall formatting and consolidation
Added More clear instructions and diagrams
Removed Table of Contents
Modified Rev. Block
TL
TMQ


Jumper Reference Diagram
 Section 1: Setup

Check
Connect the MB to 24VDC and a computer running Atmel Studio via Atmel-ICE

Power up MB, select Atmel ICE and ATxMEGA32E5, and press Apply, Read, Read

In the Memories tab, load MBIV_Engine_Controller_v1-Rev5491.hex
Location: U:\Code_Executables\Atmel_XMega\MBIV_Engine_Controller

Connect the MB to the Kiyoon Test Bed

In Tera Term, go to Setup > Serial port… and change the baud rate to 115200

Turn on the power and quickly press [Esc], [Q] and then [U]

Press [1], and go to File > Transfer > YMODEM > Send… and select MBIV-1_11_Rev5712.bin
Location: U:\Code_Executables\MBIV\MBIV-Full-Kiyoon-1_11_FULL-RevXXXX

Power down the MB and Set up jumpers as described in the Jumper Reference Diagram

Go to Setup > Serial port… and change the baud rate to 19200

Launch the test software and turn on Kiyoon Box

  Section 2: Power Test










Test Point (TP) Locations
Using the 10V/24V switch, set DC in to 24V and with a multimeter, record the voltage levels at the following labeled jacks on the test bed. Repeat with the 10V/24V switch set to 10V.
Jacks
Expected
DC In – 24V
DC In – 10V
N1 (+) – N2 (–)
5V
±0.5V


P1 (+) – P2 (–)
5V
±0.5V


W1 (+) – W2 (–)
5V
±0.5V


X1 (+) – X2 (–)
5V
±0.5V



Supply Voltage Testing Jack Locations

Section 3: Analog Inputs
Using the test GUI, verify that each Analog input is within the following expected ranges

Check
Set 10V/24V DC In switch to 24V

Ensure the 5V switch is in the ON position

Verify the following jumpers: JA03: 3–5, 4–6, 9–11, 10–12 | JA04: 1–3, 2–4



Floating (Off)
0V
2.5V
4.8V

4050 – 4095
0 – 50
2010 – 2090
4050 – 4095
AIN 01




AIN 02




AIN 03




AIN 04




AIN 05




AIN 06




AIN 07




AIN 08




AIN 09




AIN 10




AIN 11




AIN 12




AIN 13




AIN 14




AIN 15




AIN 16






Check
Set the Analog Input Voltage Knob (right) to the 0-5V mode (4th position)

Ensure all AIN switches are in the ON position

Sweep the AIN 0-5V knob (left) from end to end and observe all AIN values on the test GUI

All AIN channel readings consistently change without jumps during sweep


Section 4: Digital Outputs

Check
Verify the following jumpers: JH01: 1–3, 2–4 | JA05: 1–2 | JA06: 1–2

On the test GUI, select Continuously Transmit

Select and deselect the following outputs and observe the corresponding LEDs


LED
On
Off
Alarm


STA 1


STA 2


Section 5: CAN

Check
Verify the following jumpers: J321: 5–7, 6–8

Using a special CAN jumper, connect to the following jumper block pins. Observe the CAN packet received through CAN King. 
CAN Ch
CAN H Pin A
CAN L Pin B
CAN Packet Received
1
JA03 Pin 2
JA03 Pin 1

2
JA01 Pin 1
JA01 Pin 2

3
JA02 Pin 3
JA02 Pin 4


Section 6: Non-Volatile Memory (FRAM)

Check
Using the special CAN jumper, connect a Kvaser CAN dongle to the CAN 2 Channel
CAN H – JA01 Pin 1 / CAN L – JA01 Pin 2

Using the Test GUI, enter a value between 0 – 255 in the Value slot

Press Write, cycle power and press Read to verify the value entered was set


Selected FRAM Address
Value Stored
Value Read










Section 7: Digital Inputs	

Check
Verify the following jumpers: JA01: 3–5, 4–6

Toggle the following switches on the test bed ONE AT A TIME and observe the reaction in the test GUI.
Input
On
Off
ST 1 Transfer


ST 2 Transfer


ST 1 DK / TR


ST 2 DK / TR


Port Clutch (Ch 1)


Stbd Clutch (Ch 2)



Section 8: Engine Outputs
NOTE: Measure Engine 1 through B2(+) & B3(–) and Engine 2 through C2(+) & C3(–)

Check
Verify the following jumpers: J7A1: 1–2 | J7A3: 1–2 | J7X1: 1–2 | J7X3: 1–2


Section 8.1: Engine Configuration A: 24V in, 0-5V out

Check
Turn off the test bed

Set DC In switch to 24V

Set both Eng 1 & 2 switches to 24V

Set engine jumper block to Engine Configuration A

Set up multimeter to measure Voltage

Turn on test bed


Engine-drive
0 %
25 %
50 %
75 %
100 %
Expected Voltage
0.0 – 0.1
1.19 – 1.31
2.38 – 2.63
3.56 – 3.94
4.75 – 5.25
ENG 1





ENG 2






Section 8.2: Engine Configuration D: 4-20mA

Check
Turn off the test bed

Set engine jumper block to Engine Configuration D

Set up multimeter to measure milliamps

Turn on test bed


Engine-drive
0 %
25 %
50 %
75 %
100 %
Expected mA
0.0 – 0.5
5.75 – 6.24
11.5 – 12.5
17.28 – 18.72
23.1 – 24.9
ENG 1





ENG 2






Section 8.3: Engine Configuration F: Isolated 24Vin, 0-5Vpp PWM Output

Check
Turn off the test bed

Set engine jumper block to Engine Configuration F

Set both Eng 1 & 2 switches to 5V

Set up multimeter to measure % duty-cycle

Turn on test bed


Engine-drive
0 %
25 %
50 %
75 %
100 %
Expected %
0 – 1%
21 – 26%
45 – 50%
70 – 75 %
96 – 100%
ENG 1





ENG 2







Check
Port Frequency is ≈ 666Hz

Stbd Frequency is ≈ 666Hz


Section 8.4: Engine Configuration E: 5Vin, 0-5Vout

Check
Turn off the test bed

Set engine jumper block to Engine Configuration E

Ensure both Eng 1 & 2 switches to 5V

Set up multimeter to measure voltage

Turn on test bed


Engine-drive
0 %
25 %
50 %
75 %
100 %
Expected Voltage
0.0 – 0.1
1.19 – 1.31
2.38 – 2.63
3.56 – 3.94
4.75 – 5.25
ENG 1





ENG 2






Section 9: Auto Pilot Tests
Section 9.1: Auto Pilot Configuration A – Isolated 24V in, 0-5V out

Check

Turn off the test bed


Verify the following jumpers: JA02: 1–2, 5–6


Set auto pilot jumpers to configuration A


Switch AP Config to A/B


Switch AP Mode to A 0-5V


Switch Autopilot to ON


Turn on test bed


AP Input Knob
Position
Expected
Command Signal
Observed
Command Signal
Min
0 – 60

Max
4050 – 4095

Move the knob from min to max: AP signal changes at a constant rate


Section 9.2: Auto Pilot Configuration B – Isolated 24V in ±10V out

Check
Turn off the test bed

Set auto pilot jumpers to configuration B

Switch AP Config to A/B

Switch AP Mode to B ±10V

Ensure Autopilot is ON

Turn on test bed


AP Input Knob
Position
Expected
Command Signal
Observed
Command Signal
Min
370 – 470

Max
3550 – 3725

Move the knob from min to max: AP signal changes at a constant rate


Section 9.3 Auto Pilot Configuration C – Non-Isolated, Port & Stbd Switch Input

Check
Turn off the test bed

Set auto pilot jumpers to configuration C

Switch AP Config to C

Turn on test bed


Toggle the following switches on the test bed and observe the reaction in the test GUI

ON
OFF
Auto Pilot ON / OFF


Port AP Switch


Stbd AP Switch



Section 10: Power Outputs
Section 10.1: Power Output Faults
With the valve drives all set to 100%, short each output and verify that the H-Bridge fault checkboxes are triggered in the test GUI.
Set the 10V/24V switch to 10V and verify that the H-Bridge fault checkboxes are all triggered

Fault Triggered on
Short-Circuit
Fault Triggered on
Under-Voltage
PORT BKT


PORT NOZ


PORT INT


STBD BKT


STBD NOZ


STBD INT




Section 10.2: Driving Power Outputs
Connect the load coil to the appropriate jack on the side of the test bed. Measure the current through and the voltage across each driver at the following throttle positions and record each value in the following table along with the H-Bridge feedback as displayed in the test GUI.



0
50
100
-50
-100


V
I
FDBK
V
I
FDBK
V
I
FDBK
V
I
FDBK
V
I
FDBK
Expected
Range
24
0
±1.4
0
±0.1
0
100
10.0
12.0
0.40
0.50
250
350
20.5
23.0
1.3
1.5
1000
1200
-10.0
-12.0
-0.4
-0.5
250
350
-20.5
-23.0
-1.3
-1.5
1000
1200

10
0
±1.4
0
±0.1
0
100
4.00
6.00
0.15
0.25
100
200
8.0
10.0
0.5
0.65
400
550
-4.0
-6.0
-0.15
-0.25
100
200
-8.0
-10.0
-0.5
-0.65
400
550
PORT
BKT
24
















10















PORT
NOZ
24
















10















PORT
INT
24
















10















STBD
BKT
24
















10















STBD
NOZ
24
















10















STBD
INT
24
















10
















 
 
 */using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_MBIVautoTester._DataObjects.DataTestReport
{
    public class DATA_TESTREPORT
    {

        string _docID = "";
        string _project = "";
        string _assemblyPN = "";
        string _assemblySN = "";
        string _pcbRevisionPN = "";
        string _pcbSN = "";
        string _xMegaSWVersion = "";
        string _mbivControlUnitKiyoonSWVersion = "";
        string _mbivKiyoonTestGUISWVersion = "";
        string _multimeter1Model = "";
        string _multimeter1SN = "";
        string _multimeter2Model = "";
        string _multimeter2SN = "";
        string _condition = ""; //New, Field Return
        string _diagnosis = ""; //Fit for Installation, Can be Repaired, Can NOT be Repaired
        string _firstTestersName = "";
        string _firstTestersSignature = "";
        string _firstTestersDate = "";
        string _secondTestersName = "";
        string _secondTestersSignature = "";
        string _secondTestersDate = "";
        string _revisionNotes = ""; //Rev., Date, Change, Author, Approved
        #region frontpage
        public string DocID
        {
            get { return _docID; }
            set { _docID = value; }
        }

        public string Project
        {
            get { return _project; }
            set { _project = value; }
        }

        public string AssemblyPN
        {
            get { return _assemblyPN; }
            set { _assemblyPN = value; }
        }

        public string AssemblySN
        {
            get { return _assemblySN; }
            set { _assemblySN = value; }
        }

        public string PCBRevisionPN
        {
            get { return _pcbRevisionPN; }
            set { _pcbRevisionPN = value; }
        }

        public string PCBSN
        {
            get { return _pcbSN; }
            set { _pcbSN = value; }
        }


        public string XMegaSWVersion
        {
            get { return _xMegaSWVersion; }
            set { _xMegaSWVersion = value; }
        }

        public string MBIVControlUnitKiyoonSWVersion
        {
            get { return _mbivControlUnitKiyoonSWVersion; }
            set { _mbivControlUnitKiyoonSWVersion = value; }
        }

        public string MBIVKiyoonTestGUISWVersion
        {
            get { return _mbivKiyoonTestGUISWVersion; }
            set { _mbivKiyoonTestGUISWVersion = value; }
        }


        public string Multimeter1Model
        {
            get { return _multimeter1Model; }
            set { _multimeter1Model = value; }
        }

        public string Multimeter1SN
        {
            get { return _multimeter1SN; }
            set { _multimeter1SN = value; }
        }

        public string Multimeter2Model
        {
            get { return _multimeter2Model; }
            set { _multimeter2Model = value; }
        }

        public string Multimeter2SN
        {
            get { return _multimeter2SN; }
            set { _multimeter2SN = value; }
        }

        public string Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        public string Diagnosis
        {
            get { return _diagnosis; }
            set { _diagnosis = value; }
        }

        public string FirstTestersName
        {
            get { return _firstTestersName; }
            set { _firstTestersName = value; }
        }

        public string FirstTestersSignature
        {
            get { return _firstTestersSignature; }
            set { _firstTestersSignature = value; }
        }

        public string FirstTestersDate
        {
            get { return _firstTestersDate; }
            set { _firstTestersDate = value; }
        }

        public string SecondTestersName
        {
            get { return _secondTestersName; }
            set { _secondTestersName = value; }
        }

        public string SecondTestersSignature
        {
            get { return _secondTestersSignature; }
            set { _secondTestersSignature = value; }
        }

        public string SecondTestersDate
        {
            get { return _secondTestersDate; }
            set { _secondTestersDate = value; }
        }

        public string RevisionNotes
        {
            get { return _revisionNotes; }
            set { _revisionNotes = value; }
        }
        #endregion



        //section 1: setup

        //section 3: Analog Inputs 



        // lowMin lowMax     midMin midMax        highMin highMax      floatingMin dloatingMax
        int[] _ain_BaseReading = new int[8] { 0, 50, 2010, 2090, 4050, 4095, 4057, 4085 }; // Base readings initialization
        int[] _ain_1_measures = new int[8];
        int[] _ain_2_measures = new int[8];
        int[] _ain_3_measures = new int[8];
        int[] _ain_4_measures = new int[8];
        int[] _ain_5_measures = new int[8];
        int[] _ain_6_measures = new int[8];
        int[] _ain_7_measures = new int[8];
        int[] _ain_8_measures = new int[8];
        int[] _ain_9_measures = new int[8];
        int[] _ain_10_measures = new int[8];
        int[] _ain_11_measures = new int[8];
        int[] _ain_12_measures = new int[8];
        int[] _ain_13_measures = new int[8];
        int[] _ain_14_measures = new int[8];
        int[] _ain_15_measures = new int[8];
        int[] _ain_16_measures = new int[8];

        // Define an array of arrays to hold all measurement arrays
        int[][] allMeasurements = new int[17][];
        public int[][] AllMeasurements
        {
            get { return allMeasurements; }
            private set { allMeasurements = value; }
        }


        public int[] AIN_1_Measures
        {
            get { return _ain_1_measures; }
           private set { _ain_1_measures = value; }
        }

        public void Update_AIN_1_Measures(int[] argMeasures)
        {
            _ain_1_measures = argMeasures;
        }


        public DATA_TESTREPORT() {
            allMeasurements[0] = _ain_BaseReading;
            allMeasurements[1] = _ain_1_measures;
            allMeasurements[2] = _ain_2_measures;
            allMeasurements[3] = _ain_3_measures;
            allMeasurements[4] = _ain_4_measures;
            allMeasurements[5] = _ain_5_measures;
            allMeasurements[6] = _ain_6_measures;
            allMeasurements[7] = _ain_7_measures;
            allMeasurements[8] = _ain_8_measures;
            allMeasurements[9] = _ain_9_measures;
            allMeasurements[10] = _ain_10_measures;
            allMeasurements[11] = _ain_11_measures;
            allMeasurements[12] = _ain_12_measures;
            allMeasurements[13] = _ain_13_measures;
            allMeasurements[14] = _ain_14_measures;
            allMeasurements[15] = _ain_15_measures;
            allMeasurements[16] = _ain_16_measures;

        }
        ~DATA_TESTREPORT() { }


    }


}
